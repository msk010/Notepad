using MediatR;
using Notepad.Application.Interfaces;
using Notepad.Application.Models;
using Notepad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.NoteFeatures.Queries
{
    public class SearchNotesQuery : IRequest<IEnumerable<NoteResponse>>
    {
        public string SearchString { get; set; }

        public class SearchNotesQueryHandler : IRequestHandler<SearchNotesQuery, IEnumerable<NoteResponse>>
        {
            private readonly INotepadReadDbConnection _dbConnection;
            private readonly IUserContext _userContext;

            public SearchNotesQueryHandler(INotepadReadDbConnection dbConnection, IUserContext userContext)
            {
                _dbConnection = dbConnection;
                _userContext = userContext;
            }
            public async Task<IEnumerable<NoteResponse>> Handle(SearchNotesQuery query, CancellationToken cancellationToken)
            {
                var lookup = new Dictionary<int, NoteResponse>();

                var sql = @"
                    SELECT n.*, t.Id as TagId, t.*, tu.Id as TagUserId, tu.*, nu.Id as NoteUserId, nu.*
                    FROM Notes n
                    LEFT JOIN NoteTag nt ON n.Id = nt.NoteId   
                    LEFT JOIN Tags t ON t.Id = nt.TagId   
                    LEFT JOIN Users tu on tu.Id = t.CreatedById                 
                    LEFT JOIN Users nu on nu.Id = n.CreatedById 
                    WHERE @SearchString IS NULL OR (
                          n.Title LIKE CONCAT('%',@SearchString,'%') OR
                          n.Content LIKE CONCAT('%',@SearchString,'%') 
                    ) AND n.CreatedById = @UserId
                ";

                var parameters = new { query.SearchString, _userContext.UserId };

                NoteResponse map(NoteResponse note, TagResponse tag, UserResponse noteUser, UserResponse tagUser)
                {
                    if (!lookup.TryGetValue(note.Id, out NoteResponse result))
                    {
                        note.CreatedBy = noteUser;

                        lookup.Add(note.Id, result = note);
                    }

                    tag.CreatedBy = tagUser;

                    if (tag.Id > 0) 
                        result.Tags.Add(tag);

                    return result;
                }

                await _dbConnection.QueryAsync<NoteResponse, TagResponse, UserResponse, UserResponse, NoteResponse>(sql, map, parameters, splitOn: "TagId,TagUserId,NoteUserId");

                return lookup.Values;
            }

        }
    }
}
