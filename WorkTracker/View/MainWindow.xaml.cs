using System.Threading.Tasks;
using AutoMapper;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using WorkTracker.Data.DAL;
using WorkTracker.WPF.ViewModel;

namespace WorkTracker.WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(IMapper mapper, IWorkRepository workRepository)
        {
            this.DataContext = new WorkVM(mapper, workRepository);
            InitializeComponent();

            WorkVM.ConfirmDeletion += MahAppsOkAndCancelMessageBox;
        }

        private async Task<bool> MahAppsOkAndCancelMessageBox(string heading, string message)
        {
            if (await this.ShowMessageAsync(heading, message, MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                return true;
            else
                return false;
        }
    }
}
