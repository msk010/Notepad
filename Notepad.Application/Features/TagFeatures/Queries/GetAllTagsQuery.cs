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
    public class GetAllTagsQuery : IRequest<IEnumerable<TagResponse>>
    {
        public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, IEnumerable<TagResponse>>
        {
            private readonly INotepadReadDbConnection _dbConnection;
            public GetAllTagsQueryHandler(INotepadReadDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            public async Task<IEnumerable<TagResponse>> Handle(GetAllTagsQuery query, CancellationToken cancellationToken)
            {
                var sql = @"
                    SELECT  t.*, tu.Id as TagUserId, tu.*
                    FROM Tags t
                    INNER JOIN Users tu on tu.Id = t.CreatedById                   
                ";
                var types = new Type[] { typeof(TagResponse), typeof(UserResponse) };

                TagResponse map(TagResponse tagResponse, UserResponse userResponse)
                {
                    tagResponse.CreatedBy = userResponse;

                    return tagResponse;
                }

                return await _dbConnection.QueryAsync<TagResponse, UserResponse, TagResponse>(sql, map, splitOn: "TagUserId");
            }
        }
    }
}
