using CapitalAPI_Wpf.Command;
using CapitalAPI_Wpf.Service;
using CapitalAPI_Wpf.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CapitalAPI_Wpf.ViewModel
{
	public class MainViewModel : NotifyPropertyBase
	{
		public MainViewModel(LoginViewModel loginViewModel,
							 ReplyViewModel replyViewModel,
							 ObservableCollection<QuoteViewModel> quoteViewModels,
							 LoginService loginService,
							 QuoteService quoteService,
							 StorageService storageService,
							 AddSymbolCommand addSymbolCommand,
							 RemoveSymbolCommand removeSymbolCommand,
							 LogoutCommand logoutCommand)
		{
			_loginViewModel = loginViewModel;
			_replyViewModel = replyViewModel;
			_quoteViewModels = quoteViewModels;
			_addSymbolCommand = addSymbolCommand;
			_removeSymbolCommand = removeSymbolCommand;
			_logoutCommand = logoutCommand;
			loginService.delegateLoginCompleted += LoginCompletedEvent;
			quoteService.delegateAddSymbol += QuoteService_delegateAddSymbol;
			quoteService.delegateRemoveSymbol += QuoteService_delegateRemoveSymbol;
			quoteService.delegateQuote += QuoteService_delegateQuote;
			Visibility = Visibility.Hidden;
			BindingOperations.EnableCollectionSynchronization(QuoteViewModels, new object());
		}

		private void QuoteService_delegateRemoveSymbol(string symbol)
		{
			var quote = QuoteViewModels.FirstOrDefault(x => x.Symbol == symbol);
			QuoteViewModels.Remove(quote);
		}

		private void QuoteService_delegateQuote(SKCOMLib.SKSTOCKLONG pSKStockLONG)
		{
			var symbol = QuoteViewModels.FirstOrDefault(x => x.Symbol == pSKStockLONG.bstrStockNo);
			if (symbol != null)
			{
				symbol.Time = DateTime.Now.ToString("HH:mm:ss.fff");
				symbol.Volume = pSKStockLONG.nTQty;
				symbol.BID = pSKStockLONG.nBid * 0.01m;
				symbol.ASK = pSKStockLONG.nAsk * 0.01m;
				symbol.LAST = pSKStockLONG.nClose * 0.01m;
			}
		}

		private void QuoteService_delegateAddSymbol(HashSet<string> uniqueSymbols)
		{
			IEnumerable<string> symbol = uniqueSymbols.Except(QuoteViewModels.Select(x => x.Symbol));
			foreach (var item in symbol)
			{
				var qv = new QuoteViewModel() { Symbol = item };
				QuoteViewModels.Add(qv);
			}
		}

		private LoginViewModel _loginViewModel;

		public LoginViewModel LoginViewModel
		{
			get { return _loginViewModel; }
			set { SetProperty(ref _loginViewModel, value); }
		}

		private ObservableCollection<QuoteViewModel> _quoteViewModels;

		public ObservableCollection<QuoteViewModel> QuoteViewModels
		{
			get { return _quoteViewModels; }
			set { SetProperty(ref _quoteViewModels, value); }
		}

		private ReplyViewModel _replyViewModel;

		public ReplyViewModel ReplyViewModel
		{
			get { return _replyViewModel; }
			set { SetProperty(ref _replyViewModel, value); }
		}

		private Visibility _visibility;

		public Visibility Visibility
		{
			get { return _visibility; }
			set { SetProperty(ref _visibility, value); }
		}

		private AddSymbolCommand _addSymbolCommand;

		public ICommand AddSymbolCommand
		{
			get { return _addSymbolCommand; }
		}

		private RemoveSymbolCommand _removeSymbolCommand;

		public ICommand RemoveSymbolCommand
		{
			get { return _removeSymbolCommand; }
		}

		private LogoutCommand _logoutCommand;

		public ICommand LogoutCommand
		{
			get { return _logoutCommand; }
		}

		private void LoginCompletedEvent(bool result)
		{
			if (result)
				Visibility = Visibility.Visible;
		}
	}
}