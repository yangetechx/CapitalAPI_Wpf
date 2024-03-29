using CapitalAPI_Wpf.Service;
using CapitalAPI_Wpf.ViewModel;
using System.Windows;

namespace CapitalAPI_Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainViewModel _mainViewModel;
		private LoginService _loginService;
		private Config _config;

		public MainWindow(MainViewModel mainViewModel, LoginService loginService, Config config)
		{
			InitializeComponent();
			_mainViewModel = mainViewModel;
			_loginService = loginService;
			_config = config;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = _mainViewModel;
			if(!string.IsNullOrEmpty(_config.ID) && !string.IsNullOrEmpty(_config.PW))
			{
				_loginService.Login(_config.ID, _config.PW);
			}
		}
	}
}