using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features._Ylesanded;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Feature
{
    public class YlesanneTest : ServiceTestBase
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task should_exception_if_page_less_than_0(int page)
        {
            // Arrange
            var query = new ListYlesanneQuery { Page = page, PageSize = 5 };
            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            var handler = new ListYlesanneQueryHandler(DbContext);
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task should_exception_if_page_size_less_than_0(int page)
        {
            // Arrange
            var query = new ListYlesanneQuery { Page = 1, PageSize = page };
            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            var handler = new ListYlesanneQueryHandler(DbContext);
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Theory]
        [InlineData(69)]
        [InlineData(999)]
        public async Task should_exception_if_page_size_bigger_than_max(int page)
        {
            // Arrange
            var query = new ListYlesanneQuery { Page = 1, PageSize = page };
            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            var handler = new ListYlesanneQueryHandler(DbContext);
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task should_return_argument_null_exeption_if_request_null()
        {
            var handler = new ListYlesanneQueryHandler(DbContext);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
        }


        [Fact]
        public async Task throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ListYlesanneQueryHandler(null);
            });
        }

        [Fact]
        public async Task should_return_object_if_object_exists()
        {
            // Arrange
            var query = new ListYlesanneQuery { Page = 1, PageSize = 5 };
            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            var handler = new ListYlesanneQueryHandler(DbContext);
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.CurrentPage);
        }

        [Fact]
        public async Task should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new ListYlesanneQuery { Page = 101, PageSize = 10 };
            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            var handler = new ListYlesanneQueryHandler(DbContext);
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Empty(result.Value.Results);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task Get_should_return_null_request_id_is_zero_or_less(int Id)
        {
            // Arrange
            var dbContext = GetFaultyDbContext();
            var query = new GetYlesanneQuery { Id = Id };
            var handler = new GetYlesanneQueryHandler(dbContext);
            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_should_return_argument_null_exeption_if_request_null()
        {
            var handler = new GetYlesanneQueryHandler(DbContext);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
        }

        [Fact]
        public async Task Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetYlesanneQueryHandler(null);
            });
            var query = new GetYlesanneQuery { Id = 0 };
            var handler = new GetYlesanneQueryHandler(DbContext);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new GetYlesanneQuery { Id = 1 };
            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            var handler = new GetYlesanneQueryHandler(DbContext);
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new GetYlesanneQuery { Id = 101 };
            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            var handler = new GetYlesanneQueryHandler(DbContext);
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public void Save_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SaveYlesanneCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (SaveYlesanneCommand)null;
            var handler = new SaveYlesanneCommandHandler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Fact]
        public async Task Save_should_return_when_id_is_negative()
        {
            // Arrange
            var request = new SaveYlesanneCommand { Id = -10 };
            var handler = new SaveYlesanneCommandHandler(GetFaultyDbContext());

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var hasIdError = result.PropertyErrors.Any(e => e.Key == "Id");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.True(hasIdError);
        }

        [Fact]
        public async Task Save_should_save_new_list()
        {
            // Arrange
            var request = new SaveYlesanneCommand { Id = 0, Pealkiri = "Uus Ylesanne", Kirjeldus = "Test", Tahtaeg = DateTime.Now.AddDays(2), Staatus = "Loomisel", TunnidKokku = 5, ProjektId = 1, TootajaId = 1 };
            var handler = new SaveYlesanneCommandHandler(DbContext);

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedToDoList = await DbContext.Ylesanded.SingleOrDefaultAsync(l => l.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedToDoList);
            Assert.Equal(1, savedToDoList.Id);
        }

        [Fact]
        public async Task Save_should_save_existing_list()
        {
            // Arrange
            var listToAdd = new Ylesanne { Id = 0, Pealkiri = "Uus Ylesanne", Kirjeldus = "Test", Tahtaeg = DateTime.Now.AddDays(2), Staatus = "Loomisel", TunnidKokku = 5, ProjektId = 1, TootajaId = 1 };
            var request = new SaveYlesanneCommand { Id = 1, Pealkiri = "Uus Ylesanne 2", Kirjeldus = "Test 2", Tahtaeg = DateTime.Now.AddDays(2), Staatus = "Loomisel", TunnidKokku = 5, ProjektId = 1, TootajaId = 1 };
            var handler = new SaveYlesanneCommandHandler(DbContext);

            await DbContext.Ylesanded.AddAsync(listToAdd);
            await DbContext.SaveChangesAsync();

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedToDoList = await DbContext.Ylesanded.SingleOrDefaultAsync(l => l.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedToDoList);
            Assert.Equal(request.Pealkiri, savedToDoList.Pealkiri);
        }

        [Fact]
        public async Task Save_should_return_error_if_list_does_not_exist()
        {
            // Arrange
            var listToAdd = new Ylesanne { Id = 0, Pealkiri = "Uus Ylesanne", Kirjeldus = "Test", Tahtaeg = DateTime.Now.AddDays(2), Staatus = "Loomisel", TunnidKokku = 5, ProjektId = 1, TootajaId = 1 };
            var request = new SaveYlesanneCommand { Id = 8, Pealkiri = "Uus Ylesanne", Kirjeldus = "Test", Tahtaeg = DateTime.Now.AddDays(2), Staatus = "Loomisel", TunnidKokku = 5, ProjektId = 1, TootajaId = 1 };
            var handler = new SaveYlesanneCommandHandler(DbContext);

            await DbContext.Ylesanded.AddAsync(listToAdd);
            await DbContext.SaveChangesAsync();

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SaveValidator_should_return_false_when_title_is_invalid(string pealkiri)
        {
            // Arrange
            var validator = new SaveYlesanneCommandValidator(DbContext);
            var command = new SaveYlesanneCommand { Id = 0, Pealkiri = pealkiri, Kirjeldus = "Test", Tahtaeg = DateTime.Now.AddDays(2), Staatus = "Loomisel", TunnidKokku = 5, ProjektId = 1, TootajaId = 1 };
            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveYlesanneCommand.Pealkiri), result.Errors.First().PropertyName);
        }

        [Fact]
        public void SaveValidator_should_return_true_when_title_is_valid()
        {
            // Arrange
            var validator = new SaveYlesanneCommandValidator(DbContext);
            var command = new SaveYlesanneCommand { Id = 0, Pealkiri = "Uus Ylesanne", Kirjeldus = "Test", Tahtaeg = DateTime.Now.AddDays(2), Staatus = "Loomisel", TunnidKokku = 5, ProjektId = 1, TootajaId = 1 };

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Delete_should_throw_when_dbcontext_is_null()
        {
            var db_context = (ApplicationDbContext)null;
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new DeleteYlesanneCommandHandler(db_context);
            });

            Assert.Equal(nameof(db_context), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (DeleteYlesanneCommand)null;
            var handler = new DeleteYlesanneCommandHandler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Delete_should_return_when_request_id_is_null_or_negative(int Id)
        {
            // Arrange
            var query = new DeleteYlesanneCommand { Id = Id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeleteYlesanneCommandHandler(faultyDbContext);

            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };
            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var query = new DeleteYlesanneCommand { Id = 1 };
            var handler = new DeleteYlesanneCommandHandler(DbContext);

            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };

            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var listTest = await DbContext.Ylesanded.FindAsync(query.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(listTest);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_list_does_not_exists()
        {
            // Arrange
            var query = new DeleteYlesanneCommand { Id = 101 };
            var handler = new DeleteYlesanneCommandHandler(DbContext);

            var ylesanne = new Ylesanne
            {
                Pealkiri = "Uus Ylesanne",
                Kirjeldus = "Test",
                Tahtaeg = DateTime.Now.AddDays(2),
                Staatus = "Loomisel",
                TunnidKokku = 5,
                ProjektId = 1,
                TootajaId = 1
            };

            await DbContext.Ylesanded.AddAsync(ylesanne);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var listTest = await DbContext.Ylesanded.FindAsync(query.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(listTest);
        }
    }
}