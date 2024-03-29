using CapitalAPI_Wpf.Command;
using CapitalAPI_Wpf.Service;
using CapitalAPI_Wpf.Utility;
using System.Windows;
using System.Windows.Input;

namespace CapitalAPI_Wpf.ViewModel
{
	public class LoginViewModel : NotifyPropertyBase
	{
		public LoginViewModel(LoginService loginService, LoginCommand loginCommand)
		{
			_loginCommand = loginCommand;
			loginService.delegateLoginCompleted += LoginCompletedEvent;
		}

		private LoginCommand _loginCommand;

		public ICommand LoginCommand
		{
			get { return _loginCommand; }
		}

		private Visibility _visibility;

		public Visibility Visibility
		{
			get { return _visibility; }
			set { SetProperty(ref _visibility, value); }
		}

		private void LoginCompletedEvent(bool result)
		{
			if (result)
				Visibility = Visibility.Hidden;
		}
	}
}