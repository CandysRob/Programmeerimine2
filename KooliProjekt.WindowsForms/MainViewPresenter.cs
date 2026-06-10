using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public class MainViewPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IMainView _mainView;

        private toologi _selectedList;

        public MainViewPresenter(IApiClient apiClient, IMainView mainView)
        {
            _apiClient = apiClient;
            _mainView = mainView;
            _mainView.SetPresenter(this);
        }

        public async Task LoadData()
        {
            var response = await _apiClient.List(1, 50);
            if (response.HasErrors)
            {
                _mainView.ShowError("Viga andmete laadimisel", response);
                _mainView.DataSource = null;
                return;
            }

            _mainView.DataSource = response.Value.Results;
        }

        public void SetSelection(toologi selectedList)
        {
            _selectedList = selectedList;
            if (_selectedList == null)
            {
                _mainView.CurrentId = 0;
                _mainView.CurrentNimi = "";
                _mainView.CurrentStartTime = 0;
                _mainView.CurrentEndTime = 0;
                _mainView.CurrentKirjeldus = "";
            }
            else
            {
                _mainView.CurrentId = _selectedList.Id;
                _mainView.CurrentNimi = _selectedList.Nimi;
                _mainView.CurrentStartTime = _selectedList.starttime;
                _mainView.CurrentEndTime = _selectedList.endtime;
                _mainView.CurrentKirjeldus = _selectedList.Kirjeldus;
            }
        }

        public async Task Save()
        {
            var toolog = new toologi();
            toolog.Id = _mainView.CurrentId;
            toolog.Nimi = _mainView.CurrentNimi;
            toolog.starttime = _mainView.CurrentStartTime;
            toolog.endtime = _mainView.CurrentEndTime;
            toolog.Kirjeldus = _mainView.CurrentKirjeldus;

            var result = await _apiClient.Save(toolog);
            if (result.HasErrors)
            {
                _mainView.ShowError("Viga salvestamisel", result);
                return;
            }

            await LoadData();
        }

        public async Task Delete()
        {
            if(!_mainView.ConfirmDelete())
            {
                return;
            }

            var result = await _apiClient.Delete(_mainView.CurrentId);
            if (result.HasErrors)
            {
                _mainView.ShowError("Viga kustutamisel", result);
                return;
            }

            await LoadData();
        }
    }
}
