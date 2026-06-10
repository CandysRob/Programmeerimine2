namespace KooliProjekt.WindowsForms
{
    public interface IMainView
    {
        IList<toologi> DataSource { get; set; }
        toologi SelectedItem { get; set; }
        void SetPresenter(MainViewPresenter presenter);
        void ShowError(string message, OperationResult result);
        int CurrentId { get; set; }
        string CurrentNimi { get; set; }
        int CurrentStartTime { get; set; }
        int CurrentEndTime { get; set; }
        string CurrentKirjeldus { get; set; }
        bool ConfirmDelete();
    }
}
