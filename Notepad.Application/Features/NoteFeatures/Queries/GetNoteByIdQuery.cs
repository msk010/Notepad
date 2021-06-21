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
    public class GetNoteByIdQuery : IRequest<Note>
    {
        public GetNoteByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
        public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, Note>
        {
            private readonly INotepadReadDbConnection _dbConnection;
            public GetNoteByIdQueryHandler(INotepadReadDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public async Task<Note> Handle(GetNoteByIdQuery query, CancellationToken cancellationToken)
            {
                var sql = $"SELECT * FROM Notes WHERE Id = @Id";
                var note = await _dbConnection.QuerySingleAsync<Note>(sql, query.Id);

                return note;
            }
        }
    }
}
