using CapitalAPI_Wpf.Service;
using System.Windows.Input;

namespace CapitalAPI_Wpf.Command
{
	public class AddSymbolCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		private QuoteService _quoteService;

		public AddSymbolCommand(QuoteService quoteService)
		{
			_quoteService = quoteService;
		}

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object? parameter)
		{
			if (parameter is string)
				_quoteService.AddSymbol((string)parameter);
		}
	}
}