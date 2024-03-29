using CapitalAPI_Wpf.Utility;
using SKCOMLib;

namespace CapitalAPI_Wpf.Service
{
	public class QuoteService
	{
		public delegate void delegateOnAddSymbolHandler(HashSet<string> uniqueSymbols);

		public event delegateOnAddSymbolHandler delegateAddSymbol;

		public delegate void delegateOnQuoteHandler(SKSTOCKLONG pSKStockLONG);

		public event delegateOnQuoteHandler delegateQuote;

		private readonly SKQuoteLib _sKQuoteLib;
		private HashSet<string> _uniqueSymbols;
		private short sPage = 0;

		public QuoteService(SKQuoteLib sKQuoteLib, LoginService loginService, HashSet<string> uniqueSymbols, string symbols)
		{
			_sKQuoteLib = sKQuoteLib ?? throw new ArgumentNullException(nameof(sKQuoteLib));
			_uniqueSymbols = uniqueSymbols ?? new HashSet<string>();
			_sKQuoteLib.OnNotifyQuoteLONG += new _ISKQuoteLibEvents_OnNotifyQuoteLONGEventHandler(SKQuoteLib_OnNotifyQuote);
			_sKQuoteLib.OnConnection += new _ISKQuoteLibEvents_OnConnectionEventHandler(SKQuoteLib_OnConnection);
			loginService.delegateLoginCompleted += (result) => EnterMonitorAndRequestStocks(result);

			AddSymbolsFromString(symbols);
		}

		private void SKQuoteLib_OnNotifyQuote(short sMarketNo, int nStockIdx)
		{
			SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();

			_sKQuoteLib.SKQuoteLib_GetStockByIndexLONG(sMarketNo, nStockIdx, ref pSKStockLONG);

			delegateQuote?.Invoke(pSKStockLONG);
		}

		private void SKQuoteLib_OnConnection(int nKind, int nCode)
		{
			if (nKind == 3003 || nKind == 3036)
			{
				RequestStocks();
			}
		}

		private void EnterMonitorAndRequestStocks(bool loginResult)
		{
			if (loginResult)
			{
				int code = _sKQuoteLib.SKQuoteLib_EnterMonitorLONG();
			}
		}

		private void RequestStocks()
		{
			int code;

			foreach (string s in _uniqueSymbols)
			{
				SKSTOCKLONG pSKStockLONG = new SKSTOCKLONG();

				code = _sKQuoteLib.SKQuoteLib_GetStockByNoLONG(s.Trim(), ref pSKStockLONG);
			}

			code = _sKQuoteLib.SKQuoteLib_RequestStocks(ref sPage, string.Join(",", _uniqueSymbols));

			if (code != 0)
			{
				// 請求失敗，恢復原始的股票代碼集合
				_uniqueSymbols.ExceptWith(_uniqueSymbols);
				// 或者直接清空 _uniqueSymbols 集合
				// _uniqueSymbols.Clear();
			}

			delegateAddSymbol?.Invoke(_uniqueSymbols);

			ConfigUtility.AddOrUpdateAppSetting("Config:SymbolList", string.Join(",", _uniqueSymbols));
		}

		public bool AddSymbol(string symbol)
		{
			if (_uniqueSymbols.Contains(symbol))
				return true;

			_uniqueSymbols.Add(symbol);
			RequestStocks();
			return _uniqueSymbols.Contains(symbol);
		}

		private void AddSymbolsFromString(string symbols)
		{
			if (!string.IsNullOrEmpty(symbols))
			{
				_uniqueSymbols.UnionWith(symbols.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()));
			}
		}
	}
}