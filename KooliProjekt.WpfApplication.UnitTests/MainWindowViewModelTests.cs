using Moq;

namespace KooliProjekt.WpfApplication.UnitTests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly Mock<IDialogProvider> _dialogProviderMock;
        private readonly MainWindowViewModel _viewModel;

        public MainWindowViewModelTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _dialogProviderMock = new Mock<IDialogProvider>();
            _viewModel = new MainWindowViewModel(_apiClientMock.Object, _dialogProviderMock.Object);
        }

        [Fact]
        public void SelectedItem_should_return_correct_item()
        {
            // Arrange
            var item = new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" };

            // Act
            _viewModel.SelectedItem = item;

            // Assert
            Assert.Equal(item, _viewModel.SelectedItem);
        }

        [Fact]
        public void SelectedItem_should_call_notify_property_changed()
        {
            // Arrange
            var item = new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" };
            var propertyChangedRaised = false;
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(MainWindowViewModel.SelectedItem))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            _viewModel.SelectedItem = item;

            // Assert
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public async Task LoadData_should_load_data_from_api_client()
        {
            // Arrange
            var apiResult = new OperationResult<PagedResult<toologi>>
            {
                Value = new PagedResult<toologi>
                {
                    Results = new List<toologi>
                    {
                        new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" },
                        new toologi { Id = 2, Nimi = "Toomas", starttime = 420, endtime = 9999, Kirjeldus = "Test description 2" }
                    }
                }
            };

            _apiClientMock.Setup(client => client.List(1, 50))
                .ReturnsAsync(apiResult)
                .Verifiable();

            // Act            
            await _viewModel.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            Assert.Equal(2, _viewModel.Data.Count);
            Assert.Equal(1, _viewModel.Data[0].Id);
            Assert.Equal(2, _viewModel.Data[1].Id);
        }

        [Fact]
        public async Task LoadData_should_show_error_when_api_client_fails()
        {
            // Arrange
            var apiResult = new OperationResult<PagedResult<toologi>>
            {
                Errors = new List<string> { "Error" }
            };

            _apiClientMock.Setup(client => client.List(1, 50))
                .ReturnsAsync(apiResult)
                .Verifiable();

            // Act            
            await _viewModel.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            Assert.Empty(_viewModel.Data);
        }

        [Fact]
        public void AddNew_Command_Should_Set_Empty_SelectedItem()
        {
            var item = new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" };
            _viewModel.SelectedItem = item;
            _viewModel.AddNewCommand.Execute(null); 
            Assert.Equal(0, _viewModel.SelectedItem.Id);
        }

        [Fact]
        public void SaveCommand_should_load_data_if_no_errors()
        {
            // Arrange
            var loadDataApiResult = new OperationResult<PagedResult<toologi>>
            {
                Value = new PagedResult<toologi>
                {
                    Results = new List<toologi>
                    {
                        new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" },
                        new toologi { Id = 2, Nimi = "Toomas", starttime = 420, endtime = 9999, Kirjeldus = "Test 2" },
                    }
                }
            };
            var saveDataApiResult = new OperationResult();
            var item = new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" };

            _apiClientMock.Setup(client => client.Save(It.IsAny<toologi>()))
                .ReturnsAsync(saveDataApiResult)
                .Verifiable();
            _apiClientMock.Setup(client => client.List(1, 50))
                .ReturnsAsync(loadDataApiResult)
                .Verifiable();

            // Act
            _viewModel.SaveCommand.Execute(item);

            // Arrange
            _apiClientMock.VerifyAll();
        }

        [Fact]
        public async Task SaveCommand_should_return_when_api_gave_error()
        {
            var apiResult = new OperationResult<PagedResult<toologi>>
            {
                Errors = new List<string> { "Error" }
            };
            var item = new toologi { Id = 0, Nimi = "", starttime = 0, endtime = 0, Kirjeldus = "" };
            _apiClientMock.Setup(client => client.Save(It.IsAny<toologi>()))
                .ReturnsAsync(apiResult)
                .Verifiable();

            // Act
            _viewModel.SaveCommand.Execute(item);

            // Arrange
            _apiClientMock.VerifyAll();
        }

        [Fact]
        public async Task SaveCommand_can_execute_when_selected_item_is_not_null()
        {
            // Arrange
            var loadDataApiResult = new OperationResult<PagedResult<toologi>>
            {
                Value = new PagedResult<toologi>
                {
                    Results = new List<toologi>
                    {
                        new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" },
                        new toologi { Id = 2, Nimi = "Toomas", starttime = 420, endtime = 9999, Kirjeldus = "Test 2" },
                    }
                }
            };
            var saveDataApiResult = new OperationResult();
            var item = new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" };

            _viewModel.SelectedItem = item;

            _apiClientMock.Setup(client => client.Save(It.IsAny<toologi>()))
                .ReturnsAsync(saveDataApiResult)
                .Verifiable();
            _apiClientMock.Setup(client => client.List(1, 50))
                .ReturnsAsync(loadDataApiResult)
                .Verifiable();

            // Act
            _viewModel.SaveCommand.Execute(_viewModel.SelectedItem);

            // Arrange
            _apiClientMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteCommand_should_return_when_no_confirmation()
        {
            // Arrange
            var item = new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" };
            _viewModel.SelectedItem = item;

            _dialogProviderMock
                .Setup(dialog => dialog.Confirm(It.IsAny<string>()))
                .Returns(false)
                .Verifiable();

            // Act
            _viewModel.DeleteCommand.Execute(item);

            // Assert
            _dialogProviderMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteCommand_should_load_data_if_no_errors()
        {
            // Arrange
            var loadDataApiResult = new OperationResult<PagedResult<toologi>>
            {
                Value = new PagedResult<toologi> { Results = new List<toologi>() }
            };
            var saveDataApiResult = new OperationResult();
            var item = new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" };

            _viewModel.SelectedItem = item;

            _dialogProviderMock
                .Setup(dialog => dialog.Confirm("Are you sure you want to delete this item?"))
                .Returns(true)
                .Verifiable();

            _apiClientMock.Setup(client => client.Delete(item.Id))
                .ReturnsAsync(saveDataApiResult)
                .Verifiable();

            _apiClientMock.Setup(client => client.List(1, 50))
                .ReturnsAsync(loadDataApiResult)
                .Verifiable();

            // Act
            _viewModel.DeleteCommand.Execute(item);

            // Arrange
            _apiClientMock.VerifyAll();
            _dialogProviderMock.VerifyAll();
            Assert.Null(_viewModel.SelectedItem);
        }

        [Fact]
        public async Task DeleteCommand_should_return_when_api_gave_error()
        {
            var apiResult = new OperationResult<PagedResult<toologi>>
            {
                Errors = new List<string> { "Error" }
            };
            var item = new toologi { Id = 0, Nimi = "Test", starttime = 0, endtime = 0, Kirjeldus = "Test description" };
            _dialogProviderMock
                .Setup(dialog => dialog.Confirm("Are you sure you want to delete this item?"))
                .Returns(true)
                .Verifiable();

            _apiClientMock.Setup(client => client.Delete(It.IsAny<int>()))
                .ReturnsAsync(apiResult)
                .Verifiable();

            // Act
            _viewModel.DeleteCommand.Execute(item);

            // Arrange
            _apiClientMock.VerifyAll();
            _dialogProviderMock.VerifyAll();
        }

        [Fact]
        public void DeleteCommand_can_execute_when_selected_item_is_not_null_and_id_is_not_zero()
        {
            // Arrange
            var item = new toologi { Id = 1, Nimi = "Test", starttime = 69, endtime = 999, Kirjeldus = "Test description" };

            // Act & Assert 1: Should be true when item is selected and has Id > 0
            _viewModel.SelectedItem = item;
            var canExecuteWithValidItem = _viewModel.DeleteCommand.CanExecute(item);
            Assert.True(canExecuteWithValidItem);
        }
    }
}
