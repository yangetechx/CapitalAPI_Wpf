using CapitalAPI_Wpf.Service;

namespace CapitalAPI_Wpf.ViewModel
{
	public class ReplyViewModel
	{
		private ReplyService _replyService;

		public ReplyViewModel(ReplyService replyService)
		{
			_replyService = replyService;
		}
	}
}