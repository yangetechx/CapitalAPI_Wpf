using CapitalAPI_Wpf.Service;
using CapitalAPI_Wpf.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace CapitalAPI_Wpf.Command
{
	public class LoginCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged;

		private LoginService _loginService;

		public LoginCommand(LoginService loginService)
		{
			_loginService = loginService;
		}

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			var values = (List<object>)parameter;
			string id = values[0].ToString();
			string pw = ((PasswordBox)values[1]).Password;

			_loginService.Login(id, pw);
		}
	}
}