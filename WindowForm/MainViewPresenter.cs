using System;
using System.Threading.Tasks;
using WindowForm.Api;

namespace WindowForm
{
    public class MainViewPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IMainView _mainView;

        private Arve _arve;

        public MainViewPresenter(IApiClient apiClient, IMainView mainView)
        {
            _apiClient = apiClient;
            _mainView = mainView;
            _mainView.SetPresenter(this);
        }

        public async Task LoadData()
        {
            try
            {
                var response = await _apiClient.List(1, 20);
                if (response == null || response.HasErrors)
                {
                    _mainView.ShowError("Viga andmete laadimisel", response ?? new OperationResult().AddError("API tagastas tühja vastuse."));
                    _mainView.DataSource = null;
                }
                else
                {
                    _mainView.DataSource = response.Value?.Results;
                }
            }
            catch (Exception ex)
            {
                _mainView.ShowError("Viga laadimisel: " + ex.Message, new OperationResult());
            }
        }

        public void SetSelection(Arve selectedList)
        {
            _arve = selectedList;
            if (_arve == null)
            {
                _mainView.CurrentId = 0;
                _mainView.CurrentArveOmanik = 0;
                _mainView.CurrentRendiAeg = 0;
                _mainView.CurrentSumma = 0;
            }
            else
            {
                _mainView.CurrentId = _arve.Id;
                _mainView.CurrentArveOmanik = _arve.arve_omanik;
                _mainView.CurrentRendiAeg = _arve.rendi_aeg;
                _mainView.CurrentSumma = _arve.summa;
            }
        }

        public async Task Save()
        {
            var arve = new Arve();
            arve.Id = _mainView.CurrentId;
            arve.arve_omanik = _mainView.CurrentArveOmanik;
            arve.rendi_aeg = _mainView.CurrentRendiAeg;
            arve.summa = _mainView.CurrentSumma;

            var result = await _apiClient.Save(arve);
            if (result.HasErrors)
            {
                _mainView.ShowError("Viga salvestamisel", result);
                return;
            }

            await LoadData();
        }

        public async Task Delete()
        {
            if (!_mainView.ConfirmDelete())
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
