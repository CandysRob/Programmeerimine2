using Moq;
using KooliProjekt.WindowsForms;
using KooliProjekt.WindowsForms.Api;
using Xunit;

namespace KooliProjekt.WindowsForms.UnitTests
{
    public class MainViewPresenterTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly Mock<IMainView> _mainViewMock;
        private readonly MainViewPresenter _presenter;

        public MainViewPresenterTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _mainViewMock = new Mock<IMainView>();
            _presenter = new MainViewPresenter(_apiClientMock.Object, _mainViewMock.Object);
        }

        [Fact]
        public async Task LoadData_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult<PagedResult<toologi>>();
            faultyResponse.Errors.Add("An error occurred while fetching data.");

            _apiClientMock
                .Setup(client => client.List(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(faultyResponse)
                .Verifiable();
            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();
            _mainViewMock
                .SetupSet(view => view.DataSource = null)
                .Verifiable();

            // Act
            await _presenter.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task LoadData_should_set_DataSource_with_valid_response()
        {
            // Arrange
            var validResponse = new OperationResult<PagedResult<toologi>>
            {
                Value = new PagedResult<toologi>
                {
                    Results = new List<toologi>
                    {
                        new toologi
                        {
                            Nimi = "Toomas",
                            Kirjeldus = "Test 1",
                            endtime = 9999,
                            starttime = 69

                        },
                        new toologi
                        {
                            Nimi = "Roberto",
                            Kirjeldus = "Test 2",
                            endtime = 999,
                            starttime = 420

                        }
                    }
                }
            };

            _apiClientMock
                .Setup(client => client.List(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(validResponse)
                .Verifiable();
            _mainViewMock
                .SetupSet(view => view.DataSource = validResponse.Value.Results)
                .Verifiable();

            // Act
            await _presenter.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public void SetSelection_should_clear_fields_with_null_selection()
        {
            // Arrange
            var toolog = (toologi)null;

            _mainViewMock.SetupSet(view => view.CurrentId = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentNimi = "").Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentStartTime = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentEndTime = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentKirjeldus = "").Verifiable();

            // Act
            _presenter.SetSelection(toolog);

            // Assert
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public void SetSelection_should_set_fields_with_valid_selection()
        {
            // Arrange
            var toolog = new toologi
            {
                Id = 1,
                Nimi = "Toomas",
                Kirjeldus = "Test",
                endtime = 999,
                starttime = 69
            };

            _mainViewMock.SetupSet(view => view.CurrentId = 1).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentNimi = "Toomas").Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentStartTime = 69).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentEndTime = 999).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentKirjeldus = "Test").Verifiable();

            // Act
            _presenter.SetSelection(toolog);

            // Assert
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult();
            faultyResponse.Errors.Add("An error occurred while saving data.");

            _apiClientMock
                .Setup(client => client.Save(It.IsAny<toologi>()))
                .ReturnsAsync(faultyResponse)
                .Verifiable();
            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();

            // Act
            await _presenter.Save();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_call_LoadData_with_valid_response()
        {
            // Arrange
            var validSaveResponse = new OperationResult();

            // Because LoadData() is called, we must provide a valid result for List() as well
            var validListResponse = new OperationResult<PagedResult<toologi>>
            {
                Value = new PagedResult<toologi>
                {
                    Results = new List<toologi>()
                }
            };

            _apiClientMock
                .Setup(client => client.Save(It.IsAny<toologi>()))
                .ReturnsAsync(validSaveResponse)
                .Verifiable();

            _apiClientMock
                .Setup(client => client.List(1, 50))
                .ReturnsAsync(validListResponse)
                .Verifiable();

            // Act
            await _presenter.Save();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_return_when_user_didnot_confirmed()
        {
            // Arrange
            _mainViewMock
                .Setup(view => view.ConfirmDelete())
                .Returns(false)
                .Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult();
            faultyResponse.Errors.Add("An error occurred while deleting data.");

            var _id = It.IsAny<int>();

            _mainViewMock
                .Setup(view => view.ConfirmDelete())
                .Returns(true) // Confirms the deletion to proceed
                .Verifiable();

            // Setup _mainView.CurrentId to be passed into the Delete call if desired
            _mainViewMock
                .SetupGet(view => view.CurrentId)
                .Returns(_id);

            _apiClientMock
                .Setup(client => client.Delete(_id)) // Matches the CurrentId
                .ReturnsAsync(faultyResponse)
                .Verifiable();

            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_call_LoadData_with_valid_response()
        {
            // Arrange
            var testId = 42;
            var validDeleteResponse = new OperationResult();
            
            var validListResponse = new OperationResult<PagedResult<toologi>>
            {
                Value = new PagedResult<toologi>
                {
                    Results = new List<toologi>()
                }
            };

            // Setup for the initial Delete confirmation and invocation
            _mainViewMock
                .Setup(view => view.ConfirmDelete())
                .Returns(true)
                .Verifiable();

            _mainViewMock
                .SetupGet(view => view.CurrentId)
                .Returns(testId)
                .Verifiable();

            _apiClientMock
                .Setup(client => client.Delete(testId))
                .ReturnsAsync(validDeleteResponse)
                .Verifiable();

            // Setup for the subsequent LoadData invocation
            _apiClientMock
                .Setup(client => client.List(1, 50))
                .ReturnsAsync(validListResponse)
                .Verifiable();

            _mainViewMock
                .SetupSet(view => view.DataSource = validListResponse.Value.Results)
                .Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }
    }
}
