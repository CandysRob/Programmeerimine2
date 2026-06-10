using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KooliProjekt.WpfApplication
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        private readonly IApiClient _apiClient;
        private readonly IDialogProvider _dialogProvider;
        private readonly ObservableCollection<toologi> _data;
        public ICommand AddNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private toologi _selectedItem;        

        public MainWindowViewModel() : this(new ApiClient(), new DialogProvider())
        {
            
        }

        public MainWindowViewModel(IApiClient apiClient, IDialogProvider dialogProvider)
        {
            _data = new ObservableCollection<toologi>();
            _dialogProvider = dialogProvider;
            _apiClient = apiClient;

            AddNewCommand = new RelayCommand<toologi>(
                todoList =>
                {
                    SelectedItem = new toologi();
                });
            SaveCommand = new RelayCommand<toologi>(
                async todoList =>
                {
                    var result = await _apiClient.Save(todoList);
                    if (result.HasErrors)
                    {
                        ShowError("Cannot save data", result);
                        return;
                    }

                    SelectedItem = null;
                    await LoadData();
                },
                todoList =>
                {
                    return SelectedItem != null;
                });
            DeleteCommand = new RelayCommand<toologi>(
                async todoList =>
                {
                    var canDelete = _dialogProvider.Confirm("Are you sure you want to delete this item?");
                    if (!canDelete)
                    {
                        return;
                    }

                    var result = await _apiClient.Delete(todoList.Id);
                    if (result.HasErrors)
                    {
                        ShowError("Cannot delete data", result);
                        return;
                    }
                    SelectedItem = null;
                    await LoadData();
                },
                todoList =>
                {
                    return SelectedItem != null && SelectedItem.Id != 0;
                });
        }

        public async Task LoadData()
        {
            var data = await _apiClient.List(1, 50);
            _data.Clear();

            if (data.HasErrors)
            {
                ShowError("Cannot load data", data);
                return;
            }

            foreach (var item in data.Value.Results)
            {
                _data.Add(item);
            }
        }

        public ObservableCollection<toologi> Data
        {
            get
            {
                return _data;
            }
        }

        public toologi SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        public void ShowError(string message, OperationResult result)
        {
            var error = message + "\r\n";
            var apiErrors = "";
            var propertyErrors = "";

            if (result.Errors != null)
            {
                foreach (var apiError in result.Errors)
                {
                    apiErrors += apiError + "\r\n";
                }
            }

            if (result.PropertyErrors != null)
            {
                foreach (var propertyError in result.PropertyErrors)
                {
                    propertyErrors += propertyError.Key + ": " + propertyError.Value;
                }
            }

            if (!string.IsNullOrEmpty(apiErrors))
            {
                error += "\r\n" + apiErrors + "\r\n";
            }

            if (!string.IsNullOrEmpty(propertyErrors))
            {
                error += "\r\n" + propertyErrors;
            }

            error = error.Trim();

            _dialogProvider.ShowError(error);
        }
    }
}