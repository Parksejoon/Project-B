using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCache<T> where T : struct
{
	private static Dictionary<string, object> dataMap = new Dictionary<string, object>();       // 임시로 저장할 데이터 맵


	// 데이터 저장
	public static void SaveData(string key, T obj)
	{
		dataMap[key] = obj;
	}

	// 배열로 데이터 저장
	public static void SaveArrayData(string key, T[] objs)
	{
		for (int i = 0; i < objs.Length; i++)
		{
			dataMap[key + "_" + i] = objs[i];
		}
	}

	// 데이터 삭제
	public static void RemoveData(string key)
	{
		if (dataMap.ContainsKey(key)) dataMap.Remove(key);
	}

	// 데이터 가져오기
	public static T GetData(string key)
	{
		return (T)(dataMap.ContainsKey(key) ? dataMap[key] : 0);
	}

	// 배열 데이터 가져오기
	public static T[] GetArrayData(string key)
	{
		List<T> returnList = new List<T>();

		for (int i = 0; dataMap.ContainsKey(key + "_" + i); i++)
		{
			returnList.Add((T)dataMap[key + "_" + i]);
		}

		return returnList.ToArray();
	}
}
