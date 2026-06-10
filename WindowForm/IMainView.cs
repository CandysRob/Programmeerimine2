using System.Collections.Generic;

namespace WindowForm
{
    public interface IMainView
    {
        IList<Arve> DataSource { get; set; }

        void SetPresenter(MainViewPresenter presenter);
        void ShowError(string message, OperationResult result);
        int CurrentId { get; set; }
        int CurrentArveOmanik { get; set; }
        int CurrentRendiAeg { get; set; }
        int CurrentSumma { get; set; }
        bool ConfirmDelete();
    }
}
