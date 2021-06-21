using MediatR;
using Notepad.Application.Interfaces;
using Notepad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Features.NoteFeatures.Queries
{
    public class GetAllTagsQuery : IRequest<IEnumerable<Tag>>
    {
        public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, IEnumerable<Tag>>
        {
            private readonly INotepadReadDbConnection _dbConnection;
            public GetAllTagsQueryHandler(INotepadReadDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            public async Task<IEnumerable<Tag>> Handle(GetAllTagsQuery query, CancellationToken cancellationToken)
            {
                var sql = $"SELECT * FROM Tags";
                var notes = await _dbConnection.QueryAsync<Tag>(sql);

                return notes;
            }
        }
    }
}
