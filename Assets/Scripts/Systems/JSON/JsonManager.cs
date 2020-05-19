using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class JsonManager
{
	/// <summary>
	/// save json data to file
	/// </summary>
	/// <param name="createPath">json file location</param>
	/// <param name="fileName">json file name</param>
	/// <param name="jsonData">target json data string</param>
	public static void CreateJsonFile(string createPath, string fileName, string jsonData)
	{
		FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
		byte[] data = Encoding.UTF8.GetBytes(jsonData);

		fileStream.Write(data, 0, data.Length);
		fileStream.Close();
	}

	/// <summary>
	/// load json data from file
	/// </summary>
	/// <typeparam name="T">converted target object type</typeparam>
	/// <param name="loadPath">json file location</param>
	/// <param name="fileName">json file name</param>
	/// <returns>T type of the converted json data</returns>
	public static T LoadJsonFile<T>(string loadPath, string fileName)
	{
		FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
		byte[] data = new byte[fileStream.Length];

		fileStream.Read(data, 0, data.Length);
		fileStream.Close();

		string jsonData = Encoding.UTF8.GetString(data);

		return JsonConvert.DeserializeObject<T>(jsonData);
	}

	/// <summary>
	/// convert object to json data string
	/// </summary>
	/// <param name="obj">target object</param>
	/// <returns>converted object</returns>
	public static string ObjectToJson(object obj)
	{
		return JsonConvert.SerializeObject(obj);
	}

	/// <summary>
	/// convert json data string to object
	/// </summary>
	/// <typeparam name="T">return data type</typeparam>
	/// <param name="jsonData">target json data string</param>
	/// <returns>json data converted to type T.</returns>
	public static T JsonToOject<T>(string jsonData)
	{
		return JsonConvert.DeserializeObject<T>(jsonData);
	}
}
