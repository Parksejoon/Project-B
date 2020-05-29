using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDictionary
{
	class Program
	{
		static void Main(string[] args)
		{
			JsonDataMap jdataMap = new JsonDataMap();

			jdataMap.SetDataMap();
			jdataMap.SaveData();
		}
	}
}
