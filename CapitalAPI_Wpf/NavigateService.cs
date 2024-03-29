using CapitalAPI_Wpf.Command;
using CapitalAPI_Wpf.Service;
using CapitalAPI_Wpf.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SKCOMLib;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace CapitalAPI_Wpf
{
	public class NavigateService
	{
		private static readonly Lazy<NavigateService> instance = new Lazy<NavigateService>();

		public static NavigateService Current => instance.Value;
		private ServiceProvider provider;
		private bool inited = false;
		Config Config;
		private void Initialized()
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.Build();

			Config = new Config();
			configuration.GetSection("Config").Bind(Config);

			var sc = new ServiceCollection();

			sc.AddSingleton<SKCenterLib>(c => new SKCenterLib());
			sc.AddSingleton<SKQuoteLib>(c => new SKQuoteLib());
			sc.AddSingleton<SKReplyLib>(c => new SKReplyLib());
			sc.AddTransient<HashSet<string>>();
			sc.AddSingleton<LoginService>();
			sc.AddSingleton<ReplyService>();
			sc.AddSingleton<QuoteService>(c =>
			new QuoteService(
				c.GetRequiredService<SKQuoteLib>(),
				c.GetRequiredService<LoginService>(),
				c.GetRequiredService<HashSet<string>>(),
				Config.SymbolList));
			sc.AddSingleton<StorageService>();

			sc.AddSingleton<LoginCommand>();
			sc.AddSingleton<AddSymbolCommand>();
			sc.AddSingleton<RemoveSymbolCommand>();
			sc.AddSingleton<LogoutCommand>();
			sc.AddSingleton<LoginViewModel>();
			sc.AddSingleton<ReplyViewModel>();
			sc.AddTransient<ObservableCollection<QuoteViewModel>>();
			sc.AddSingleton<MainViewModel>();

			provider = sc.BuildServiceProvider();
			inited = true;
		}

		public Window Navigate()
		{
			if (!inited)
				Initialized();

			return new MainWindow(
				provider.GetRequiredService<MainViewModel>(),
				provider.GetRequiredService<LoginService>(),
				Config,
				provider.GetRequiredService<LogoutCommand>()
				);
		}
	}
}