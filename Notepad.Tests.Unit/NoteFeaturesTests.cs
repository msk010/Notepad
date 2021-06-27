using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Dapper;
using Notepad.Application.Context;
using Notepad.Application.Features.NoteFeatures.Commands;
using Notepad.Application.Features.NoteFeatures.Queries;
using Notepad.Application.Interfaces;
using Notepad.Application.Models;
using Notepad.Domain.Entities;
using Notepad.Infrastructure.Dapper.Connection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Tests.Unit
{
    public class NoteFeaturesTests
    {
        private Mock<IUserContext> _userContextMock;
        private Mock<INotepadDbContext> _dbContextMock;
        private Mock<INotepadReadDbConnection> _readDbConnection;

        private Mock<DbSet<T>> DbSetMockSetup<T>(IQueryable<T> data = null) where T : class
        {
            if(data == null)
            {
                data = new List<T>().AsQueryable();
            }

            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return dbSetMock;
        }

        [SetUp]
        public void Setup()
        {
            _userContextMock = new Mock<IUserContext>();
            _dbContextMock = new Mock<INotepadDbContext>();

            var currentUser = new User("login", "email", "firstName", "secondName");
            currentUser.GetType().GetProperty(nameof(User.Id)).SetValue(currentUser, 1);

            var data = new List<User>
            {
                currentUser
            }.AsQueryable();

            var userSetMock = DbSetMockSetup(data);
            _dbContextMock.Setup(m => m.Users).Returns(userSetMock.Object);

            _userContextMock.Setup(x => x.UserId).Returns(currentUser.Id);

            _readDbConnection = new Mock<INotepadReadDbConnection>();
        }

        [Test]
        public async Task CreateNoteCommand_Test()
        {
            //Arrange.
            var noteSetMock = DbSetMockSetup<Note>();
            _dbContextMock.Setup(m => m.Notes).Returns(noteSetMock.Object);

            var commandHandler = new CreateNoteCommand.CreateNoteCommandHandler(_dbContextMock.Object, _userContextMock.Object);
            var command = new CreateNoteCommand()
            {
                Title = "TEST TITLE",
                Content = "TEST CONTENT",
            };

            //Act.
            var result = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Assert.
            _dbContextMock.Verify(x => x.Notes.Add(It.IsAny<Note>()), Times.Once());
            _dbContextMock.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public async Task DeleteNoteCommand_Test()
        {
            //Arrange.
            var noteSetMock = DbSetMockSetup<Note>();
            _dbContextMock.Setup(m => m.Notes).Returns(noteSetMock.Object);

            var commandHandler = new DeleteNoteByIdCommand.DeleteProductByIdCommandHandler(_dbContextMock.Object);
            var command = new DeleteNoteByIdCommand(1);

            //Act.
            var result = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Assert.
            _dbContextMock.Verify(x => x.Notes.Remove(It.IsAny<Note>()), Times.Once());
            _dbContextMock.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public async Task UpdateNoteCommand_Test()
        {
            //Arrange.
            var noteSetMock = DbSetMockSetup<Note>();
            _dbContextMock.Setup(m => m.Notes).Returns(noteSetMock.Object);

            var commandHandler = new UpdateNoteCommand.UpdateNoteCommandHandler(_dbContextMock.Object);
            var command = new UpdateNoteCommand
            {
                Content = "UPDATE CONTENT",
                Title = "UPDATE TITLE"
            };

            //Act.
            var result = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Assert.
            _dbContextMock.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public async Task GetNoteByIdQuery_Test()
        {
            //Arrange.
            var expected = new Note("TITLE", "CONTENT", new List<int> { }, 1);
            var connection = new Mock<IDbConnection>(); 
            connection.SetupDapperAsync(c => c.QuerySingleAsync<Note>(It.IsAny<string>(), null, null, null, null))
              .ReturnsAsync(expected);           

            var commandHandler = new GetNoteByIdQuery.GetNoteByIdQueryHandler(new NotepadReadDbConnection(connection.Object));
            var command = new GetNoteByIdQuery(1);

            //Act.
            var result = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Assert.
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task SearchNotesQuery_Test()
        {
            //Arrange.
            var expected = new NoteResponse[] 
            { 
                new NoteResponse { 
                    Content = "CONTENT", Title = "TITLE", CreatedOn = DateTimeOffset.Now, 
                        CreatedBy = new UserResponse {Id = 1, FirstName = "FIRST_NAME", SecondName = "SECOND_NAME" }, 
                        Tags = new List<TagResponse> { } 
                } 
            };

            var parameters = new { SearchString = "CONTENT", UserId = _userContextMock.Object.UserId };

            var connection = new Mock<IDbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync(It.IsAny<string>(), It.IsAny<Func<NoteResponse, TagResponse, UserResponse, UserResponse, NoteResponse>>(), parameters, null, It.IsAny<bool>(), It.IsAny<string>(), null, null))
              .ReturnsAsync(expected);

            var commandHandler = new SearchNotesQuery.SearchNotesQueryHandler(new NotepadReadDbConnection(connection.Object), _userContextMock.Object);
            var command = new SearchNotesQuery
            {
                SearchString = parameters.SearchString
            };

            //Act.
            var result = await commandHandler.Handle(command, new System.Threading.CancellationToken());

            //Assert.
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(expected.Length));
            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}