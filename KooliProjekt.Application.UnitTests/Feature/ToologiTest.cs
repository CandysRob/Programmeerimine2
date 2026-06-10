using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features._Toologi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Feature
{
    public class ToologiTest : ServiceTestBase
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task should_exception_if_page_less_than_0(int page)
        {
            // Arrange
            var query = new ListToologiQuery { Page = page, PageSize = 5 };
            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };
            var handler = new ListToologiQueryHandler(DbContext);
            await DbContext.Toologid.AddAsync(_toologi);
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
            var query = new ListToologiQuery { Page = 1, PageSize = page };
            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };
            var handler = new ListToologiQueryHandler(DbContext);
            await DbContext.Toologid.AddAsync(_toologi);
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
            var query = new ListToologiQuery { Page = 1, PageSize = page };
            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69
            };
            var handler = new ListToologiQueryHandler(DbContext);
            await DbContext.Toologid.AddAsync(_toologi);
            await DbContext.SaveChangesAsync();

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task should_return_argument_null_exeption_if_request_null()
        {
            var handler = new ListToologiQueryHandler(DbContext);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
        }


        [Fact]
        public async Task throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ListToologiQueryHandler(null);
            });
        }

        [Fact]
        public async Task should_return_object_if_object_exists()
        {
            // Arrange
            var query = new ListToologiQuery { Page = 1, PageSize = 5 };
            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };
            var handler = new ListToologiQueryHandler(DbContext);
            await DbContext.Toologid.AddAsync(_toologi);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.CurrentPage); // Cast to Arve before accessing Id
        }

        [Fact]
        public async Task should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new ListToologiQuery { Page = 101, PageSize = 10 };
            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };
            var handler = new ListToologiQueryHandler(DbContext);
            await DbContext.Toologid.AddAsync(_toologi);
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
            var query = new GetToologiQuery { Id = Id };
            var handler = new GetToologiQueryHandler(dbContext);
            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };
            await DbContext.Toologid.AddAsync(_toologi);
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
            var handler = new GetToologiQueryHandler(DbContext);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
        }


        [Fact]
        public async Task Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetToologiQueryHandler(null);
            });
            var query = new GetToologiQuery { Id = 0 };
            var handler = new GetToologiQueryHandler(DbContext);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new GetToologiQuery { Id = 1 };
            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };
            var handler = new GetToologiQueryHandler(DbContext);
            await DbContext.Toologid.AddAsync(_toologi);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id); // Cast to Arve before accessing Id
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new GetToologiQuery { Id = 101 };
            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };
            var handler = new GetToologiQueryHandler(DbContext);
            await DbContext.Toologid.AddAsync(_toologi);
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
                new SaveToologiCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (SaveToologiCommand)null;
            var handler = new SaveToologiCommandHandler(DbContext);

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
            var request = new SaveToologiCommand { Id = -10 };
            var handler = new SaveToologiCommandHandler(GetFaultyDbContext());

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
            var request = new SaveToologiCommand { Id = 0, Nimi = "Toomas", Kirjeldus = "Test", endtime = 999, starttime = 69 };
            var handler = new SaveToologiCommandHandler(DbContext);

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedToDoList = await DbContext.Toologid.SingleOrDefaultAsync(l => l.Id == 1);

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
            var listToAdd = new toologi { Id = 0, Nimi = "Toomas", Kirjeldus = "Test", endtime = 999, starttime = 69 };
            var request = new SaveToologiCommand { Id = 1, Nimi = "Toomas", Kirjeldus = "Test", endtime = 999, starttime = 69 };
            var handler = new SaveToologiCommandHandler(DbContext);

            await DbContext.Toologid.AddAsync(listToAdd);
            await DbContext.SaveChangesAsync();

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedToDoList = await DbContext.Toologid.SingleOrDefaultAsync(l => l.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedToDoList);
            Assert.Equal(request.Nimi, savedToDoList.Nimi);
        }

        [Fact]
        public async Task Save_should_return_error_if_list_does_not_exist()
        {
            // Arrange
            var listToAdd = new toologi { Id = 0, Nimi = "Toomas", Kirjeldus = "Test", endtime = 999, starttime = 69 };
            var request = new SaveToologiCommand { Id = 8, Nimi = "Toomas", Kirjeldus = "Test", endtime = 999, starttime = 69 };
            var handler = new SaveToologiCommandHandler(DbContext);

            await DbContext.Toologid.AddAsync(listToAdd);
            await DbContext.SaveChangesAsync();

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Theory]
        [InlineData("")]
        public void SaveValidator_should_return_false_when_title_is_invalid(string nimi)
        {
            // Arrange
            var validator = new SaveToologiCommandValidator(DbContext);
            var command = new SaveToologiCommand { Id = 0, Nimi = nimi, Kirjeldus = "Test", endtime = 999, starttime = 69 };
            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveToologiCommand.Nimi), result.Errors.First().PropertyName);
        }

        [Fact]
        public void SaveValidator_should_return_true_when_title_is_valid()
        {
            // Arrange
            var validator = new SaveToologiCommandValidator(DbContext);
            var command = new SaveToologiCommand { Id = 0, Nimi = "Toomas", Kirjeldus = "Test", endtime = 999, starttime = 69 };

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
                new DeleteToologiCommandHandler(db_context);
            });

            Assert.Equal(nameof(db_context), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (DeleteToologiCommand)null;
            var handler = new DeleteToologiCommandHandler(DbContext);

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
            var query = new DeleteToologiCommand { Id = Id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeleteToologiCommandHandler(faultyDbContext);

            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };
            await DbContext.Toologid.AddAsync(_toologi);
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
            var query = new DeleteToologiCommand { Id = 1 };
            var handler = new DeleteToologiCommandHandler(DbContext);

            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69
            };

            await DbContext.Toologid.AddAsync(_toologi);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var listTest = await DbContext.Toologid.FindAsync(query.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(listTest);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_list_does_not_exists()
        {
            // Arrange
            var query = new DeleteToologiCommand { Id = 101 };
            var handler = new DeleteToologiCommandHandler(DbContext);

            var _toologi = new toologi
            {
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69

            };

            await DbContext.Toologid.AddAsync(_toologi);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var listTest = await DbContext.Toologid.FindAsync(query.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(listTest);
        }
    }
}
