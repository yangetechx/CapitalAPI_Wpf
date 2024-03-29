using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;
using System.Windows;

namespace CapitalAPI_Wpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			MainWindow = NavigateService.Current.Navigate();
			MainWindow.Show();
		}
	}
}