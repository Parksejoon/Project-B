using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour
{
	private static Dictionary<string, GameObject> _cashe = new Dictionary<string, GameObject>();

	//private void Awake()
	//{
	//	if (instance == null)
	//	{
	//		instance = this;
	//	}
	//	else
	//	{
	//		Debug.Log("Multiple \"ResourceLoader\" objects have created.");
	//	}
	//}
	
	// 리소스 로드 함수
	public static void Load(string path)
	{
		// 폴더에서 로드
		object[] temp = Resources.LoadAll(path);

		// 캐쉬에 저장
		for (int i = 0; i < temp.Length; i++)
		{
			GameObject obj = (GameObject)temp[i];
			_cashe[obj.name] = obj;
		}
	}

	// 오브젝트를 받아오는 함수
	public static GameObject Get(string key)
	{
		return _cashe[key];
	}

	// 오브젝트를 인스턴스화 해서 반환하는 함수
	public static GameObject Instance(string key, string name, Vector3 position)
	{
		GameObject returnObject = Instantiate(_cashe[key], position, Quaternion.identity);

		returnObject.name = name;

		return returnObject;
	}

	// 캐쉬에서 삭제
	public static void Remove(params string[] arg)
	{
		foreach (string key in arg)
		{
			_cashe.Remove(key);
		}
	}

	// 캐쉬 초기화
	public static void ResetCashe()
	{
		_cashe = new Dictionary<string, GameObject>();
	}
}
