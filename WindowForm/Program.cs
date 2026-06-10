using System;
using System.Windows.Forms;
using WindowForm.Api;

namespace WindowForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IApiClient apiClient = new ApiClient();
            
            var view = new Form1(apiClient);
            var presenter = new MainViewPresenter(apiClient, view);

            Application.Run(view);
        }
    }
}
