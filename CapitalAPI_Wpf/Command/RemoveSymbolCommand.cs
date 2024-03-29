using CapitalAPI_Wpf.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CapitalAPI_Wpf.Command
{
	public class RemoveSymbolCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		private QuoteService _quoteService;

		public RemoveSymbolCommand(QuoteService quoteService)
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
				_quoteService.RemoveSymbol((string)parameter);
		}
	}
}
