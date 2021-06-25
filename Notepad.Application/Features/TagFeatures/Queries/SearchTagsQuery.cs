using MediatR;
using Notepad.Application.Interfaces;
using Notepad.Application.Models;
using Notepad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.TagFeatures.Queries
{
    public class SearchTagsQuery : IRequest<IEnumerable<TagResponse>>
    {
        public string SearchString { get; set; }

        public class SearchTagsQueryHandler : IRequestHandler<SearchTagsQuery, IEnumerable<TagResponse>>
        {
            private readonly INotepadReadDbConnection _dbConnection;
            private readonly IUserContext _userContext;
            public SearchTagsQueryHandler(INotepadReadDbConnection dbConnection, IUserContext userContext)
            {
                _dbConnection = dbConnection;
                _userContext = userContext;
            }
            public async Task<IEnumerable<TagResponse>> Handle(SearchTagsQuery query, CancellationToken cancellationToken)
            {
                var sql = @"
                    SELECT  
	                    t.*, 
	                    CASE WHEN (select TOP 1 TagId from NoteTag where TagId = t.Id) IS NULL THEN 1 ELSE 0 END AS CanDelete, 
	                    tu.Id as TagUserId, tu.*
                    FROM Tags t
                    INNER JOIN Users tu on tu.Id = t.CreatedById
                    WHERE @SearchString IS NULL OR (
                          t.Name LIKE CONCAT('%',@SearchString,'%')
                    ) AND t.UserId = @UserId
                ";
                var types = new Type[] { typeof(TagResponse), typeof(UserResponse) };
                var parameters = new { query.SearchString, _userContext.UserId };

                TagResponse map(TagResponse tagResponse, UserResponse userResponse)
                {
                    tagResponse.CreatedBy = userResponse;

                    return tagResponse;
                }

                return await _dbConnection.QueryAsync<TagResponse, UserResponse, TagResponse>(sql, map, splitOn: "TagUserId", param: parameters);
            }

        }
    }
}
