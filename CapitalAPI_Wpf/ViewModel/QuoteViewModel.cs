using CapitalAPI_Wpf.Service;
using CapitalAPI_Wpf.Utility;
using System.Windows;

namespace CapitalAPI_Wpf.ViewModel
{
	public class QuoteViewModel : NotifyPropertyBase
	{
		private string _symbol = string.Empty;

		public string Symbol
		{
			get { return _symbol; }
			set { SetProperty(ref _symbol, value); }
		}

		private string _time = string.Empty;

		public string Time
		{
			get { return _time; }
			set { SetProperty(ref _time, value); }
		}

		private decimal _ask;

		public decimal ASK
		{
			get { return _ask; }
			set { SetProperty(ref _ask, value); }
		}

		private decimal _bid;

		public decimal BID
		{
			get { return _bid; }
			set { SetProperty(ref _bid, value); }
		}

		private decimal _last;

		public decimal LAST
		{
			get { return _last; }
			set { SetProperty(ref _last, value); }
		}

		public int _volume;

		public int Volume
		{
			get { return _volume; }
			set { SetProperty(ref _volume, value); }
		}
	}
}