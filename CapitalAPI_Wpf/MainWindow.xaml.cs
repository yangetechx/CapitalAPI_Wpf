using CapitalAPI_Wpf.Command;
using CapitalAPI_Wpf.Service;
using CapitalAPI_Wpf.ViewModel;
using System;
using System.Windows;

namespace CapitalAPI_Wpf
{
	public partial class MainWindow : Window
	{
		private MainViewModel _mainViewModel;
		private LoginService _loginService;
		private Config _config;
		private TimeSpan _resetTime;

		public MainWindow(MainViewModel mainViewModel, LoginService loginService, Config config, LogoutCommand logoutCommand)
		{
			InitializeComponent();
			_mainViewModel = mainViewModel;
			_loginService = loginService;
			_config = config;
			logoutCommand.delegateLogout += ResetTask;
			// 解析重置時間
			_resetTime = TimeSpan.Parse(_config.ResetTime);
		}

		

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = _mainViewModel;

			// 如果 ID 和 PW 都不為空，則自動登入
			if (!string.IsNullOrEmpty(_config.ID) && !string.IsNullOrEmpty(_config.PW))
			{
				_loginService.Login(_config.ID, _config.PW);
			}

			// 計算下一個重置時間點
			DateTime nextResetTime = DateTime.Today.Add(_resetTime);
			if (DateTime.Now > nextResetTime)
			{
				nextResetTime = nextResetTime.AddDays(1);
			}

			// 設置定時器來執行每日重置任務
			TimeSpan delay = nextResetTime - DateTime.Now;
			TimerCallback resetTask = new TimerCallback(ResetTask);
			Timer timer = new Timer(resetTask, null, delay, TimeSpan.FromDays(1));
		}
	
		private void ResetTask(object state)
		{
			// 執行每日重置任務
			System.Reflection.Assembly.GetEntryAssembly();
			string startpath = System.IO.Directory.GetCurrentDirectory();
			System.Diagnostics.Process.Start(startpath + "\\CapitalAPI_Wpf.exe");
			this.Dispatcher.Invoke(new Action(() => { Application.Current.Shutdown(); }));
		}
	}
}