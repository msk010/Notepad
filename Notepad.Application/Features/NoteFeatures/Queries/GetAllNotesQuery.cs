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
    public class GetAllNotesQuery : IRequest<IEnumerable<Note>>
    {
        public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, IEnumerable<Note>>
        {
            private readonly INotepadReadDbConnection _dbConnection;
            public GetAllNotesQueryHandler(INotepadReadDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            public async Task<IEnumerable<Note>> Handle(GetAllNotesQuery query, CancellationToken cancellationToken)
            {
                var lookup = new Dictionary<int, Note>();

                var sql = @"
                    SELECT n.*, t.*
                    FROM Notes n
                    INNER JOIN NoteTag nt ON n.Id = nt.NoteId   
                    INNER JOIN Tags t ON t.Id = nt.TagId                   
                ";
                await _dbConnection.QueryAsync<Note, Tag, Note>(sql, (n, t) => {
                    if (!lookup.TryGetValue(n.Id, out Note note))
                    {
                        lookup.Add(n.Id, note = n);
                    }
                    var noteTag = new NoteTag(t, null);
                    note.NoteTags.Add(noteTag);

                    return note;
                });

                var resultList = lookup.Values;

                return resultList;
            }
        }
    }
}
