using GameOfLife.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private GameViewModel _viewModel = null!;
        private MainWindow _view = null!;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _viewModel = new GameViewModel();
                

            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();
        }
    }

}
