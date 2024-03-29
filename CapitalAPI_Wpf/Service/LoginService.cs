using CapitalAPI_Wpf.Utility;
using SKCOMLib;

namespace CapitalAPI_Wpf.Service
{
	public class LoginService
	{
		private SKCenterLib _sKCenterLib;

		public delegate void delegateOnLoginCompletedHandler(bool result);

		public event delegateOnLoginCompletedHandler delegateLoginCompleted;

		public LoginService(SKCenterLib sKCenterLib)
		{
			_sKCenterLib = sKCenterLib;
		}

		public void Login(string id, string pw)
		{
			int m_nCode = _sKCenterLib.SKCenterLib_Login(id, pw);

			if (m_nCode == 0)
			{
				delegateLoginCompleted?.Invoke(true);
				ConfigUtility.AddOrUpdateAppSetting("Config:ID", id);
				ConfigUtility.AddOrUpdateAppSetting("Config:PW", pw);
			}
			else
			{
				delegateLoginCompleted?.Invoke(false);
				ConfigUtility.AddOrUpdateAppSetting("Config:ID", "");
				ConfigUtility.AddOrUpdateAppSetting("Config:PW", "");
			}
				
		}
	}
}