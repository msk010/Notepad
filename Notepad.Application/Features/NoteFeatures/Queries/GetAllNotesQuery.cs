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
    public class GetAllNotesQuery : IRequest<IEnumerable<NoteResponse>>
    {
        public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, IEnumerable<NoteResponse>>
        {
            private readonly INotepadReadDbConnection _dbConnection;
            public GetAllNotesQueryHandler(INotepadReadDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            public async Task<IEnumerable<NoteResponse>> Handle(GetAllNotesQuery query, CancellationToken cancellationToken)
            {
                var lookup = new Dictionary<int, NoteResponse>();

                var sql = @"
                    SELECT n.*, t.Id as TagId, t.*, tu.Id as TagUserId, tu.*, nu.Id as NoteUserId, nu.*
                    FROM Notes n
                    INNER JOIN NoteTag nt ON n.Id = nt.NoteId   
                    INNER JOIN Tags t ON t.Id = nt.TagId   
                    INNER JOIN Users tu on tu.Id = t.CreatedById                 
                    INNER JOIN Users nu on nu.Id = n.CreatedById                 
                ";
                var types = new Type[] { typeof(NoteResponse), typeof(TagResponse), typeof(UserResponse), typeof(UserResponse) };

                NoteResponse map(object[] objects)
                {
                    var note = (NoteResponse)objects[0];
                    if (!lookup.TryGetValue(note.Id, out NoteResponse result))
                    {
                        var noteUser = (UserResponse)objects[2];
                        note.CreatedBy = noteUser;

                        lookup.Add(note.Id, result = note);
                    }

                    var tag = (TagResponse)objects[1];
                    tag.CreatedBy = (UserResponse)objects[3];

                    result.Tags.Add(tag);

                    return result;
                }

                await _dbConnection.QueryAsync(sql, types, map, splitOn: "TagId,TagUserId,NoteUserId");

                return lookup.Values;
            }

        }
    }
}
