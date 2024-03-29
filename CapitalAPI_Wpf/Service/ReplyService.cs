using SKCOMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace CapitalAPI_Wpf.Service
{
	public class ReplyService
	{
		private SKReplyLib _sKReplyLib;

		public ReplyService(SKReplyLib sKReplyLib)
		{
			_sKReplyLib = sKReplyLib;
			_sKReplyLib.OnReplyMessage += OnAnnouncement;
		}

		private void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
		{
			nConfirmCode = -1;
		}
	}
}