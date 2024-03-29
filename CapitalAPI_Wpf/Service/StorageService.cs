using SKCOMLib;
using System.IO;

namespace CapitalAPI_Wpf.Service
{
	public class StorageService
	{
		public StorageService(QuoteService quoteService)
		{
			quoteService.delegateQuote += QuoteService_delegateQuote;
		}

		private void QuoteService_delegateQuote(SKSTOCKLONG pSKStockLONG)
		{
			string currentDate = DateTime.Now.ToString("yyyyMMdd");
			string currentTime = DateTime.Now.ToString("HH:mm:ss.fff");
			string stockNo = pSKStockLONG.bstrStockNo;

			string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", currentDate);
			string filePath = Path.Combine(dirPath, $"{stockNo}.csv");

			try
			{
				if (!Directory.Exists(dirPath))
				{
					Directory.CreateDirectory(dirPath);
				}

				string line = $"{currentTime},{pSKStockLONG.nBid * 0.01},{pSKStockLONG.nBc},{pSKStockLONG.nAsk * 0.01},{pSKStockLONG.nAc},{pSKStockLONG.nClose * 0.01},{pSKStockLONG.nTickQty},{pSKStockLONG.nTQty}";

				using (StreamWriter writer = new StreamWriter(filePath, true))
				{
					writer.WriteLine(line);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error writing to file: {ex.Message}");
			}
		}
	}
}