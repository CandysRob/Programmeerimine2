using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features._Projektid;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class ProjektidTest : TestBase
    {
        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var url = "/api/Projektid/List?page=1&pageSize=5";

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<Projekt>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_list()
        {
            // Arrange
            var url = "/api/Projektid/Get/?id=1";

            var projekt = new Projekt
            {
                Nimi = "Toomas",
                Alguskuupaev = DateTime.Now,
                Kirjeldus = "Test",
                Lopetatuskuupaev = DateTime.Now.AddDays(1),
                Ylesanded = null

            };
            await DbContext.AddAsync(projekt);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<Projekt>>(url);
            
            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_not_found_for_missing_list()
        {
            // Arrange
            var url = "/api/Projektid/Get/?id=131";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var url = "/api/Projektid/Delete/";

            var projekt = new Projekt
            {
                Nimi = "Toomas",
                Alguskuupaev = DateTime.Now,
                Kirjeldus = "Test",
                Lopetatuskuupaev = DateTime.Now.AddDays(1),
                Ylesanded = null

            };
            await DbContext.AddAsync(projekt);
            await DbContext.SaveChangesAsync();

            // Act
            using var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id = projekt.Id })
            };
            using var response = await Client.SendAsync(request);            
            var listFromDb = await DbContext.Projektid
                .Where(list => list.Id == projekt.Id)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Null(listFromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_shouldwork_with_missing_list()
        {
            // Arrange
            var url = "/api/Projektid/Delete/";

            // Act
            using var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id  = 101 })
            };
            using var response = await Client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var url = "/api/Projektid/Save/";
            var command = new SaveProjektCommand { Id = 0, Nimi = "Toomas", Alguskuupaev = DateTime.Now, Kirjeldus = "Test", Lopetatuskuupaev = DateTime.Now.AddDays(1), Ylesanded = null };

            // Act
            using var response = await Client.PostAsJsonAsync<SaveProjektCommand>(url, command);
            var listFromDb = await DbContext.Projektid
                .Where(list => list.Id == 1)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(listFromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_missing_list()
        {
            // Arrange
            var url = "/api/Projektid/Save/";
            var command = new SaveProjektCommand { Id = 10, Nimi = "Toomas", Alguskuupaev = DateTime.Now, Kirjeldus = "Test", Lopetatuskuupaev = DateTime.Now.AddDays(1), Ylesanded = null };

            // Act
            using var response = await Client.PostAsJsonAsync<SaveProjektCommand>(url, command);
            var listFromDb = await DbContext.Projektid
                .Where(list => list.Id == 10)
                .FirstOrDefaultAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(listFromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_invalid_list()
        {
            // Arrange
            var url = "/api/Projektid/Save/";
            var command = new SaveProjektCommand { Id = 0, Nimi = "", Alguskuupaev = DateTime.Now, Kirjeldus = "", Lopetatuskuupaev = DateTime.Now.AddDays(1), Ylesanded = null };

            // Act
            using var response = await Client.PostAsJsonAsync<SaveProjektCommand>(url, command);
            var listFromDb = await DbContext.Projektid
                .Where(list => list.Id == 1)
                .FirstOrDefaultAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(listFromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.True(result.HasErrors);
        }
    }
}