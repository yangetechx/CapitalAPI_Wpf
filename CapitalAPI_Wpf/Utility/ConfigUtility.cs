using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalAPI_Wpf.Utility
{
	public static class ConfigUtility
	{
		public static void AddOrUpdateAppSetting<T>(string key, T value)
		{
			try
			{
				var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
				string json = File.ReadAllText(filePath);
				dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

				var sectionPath = key.Split(":")[0];

				if (!string.IsNullOrEmpty(sectionPath))
				{
					var keyPath = key.Split(":")[1];
					jsonObj[sectionPath][keyPath] = value;
				}
				else
				{
					jsonObj[sectionPath] = value; // if no sectionpath just set the value
				}

				string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
				File.WriteAllText(filePath, output);
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error writing app settings");
			}
		}
	}
}