using CapitalAPI_Wpf.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CapitalAPI_Wpf.Command
{
	public class LogoutCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		public delegate void delegateOnLogoutHandler(object state);

		public event delegateOnLogoutHandler delegateLogout;

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object? parameter)
		{
			ConfigUtility.AddOrUpdateAppSetting("Config:ID", "");
			ConfigUtility.AddOrUpdateAppSetting("Config:PW", "");
			delegateLogout?.Invoke("");
		}
	}
}