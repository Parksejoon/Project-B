using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
	private static Dictionary<string, Stack<GameObject>> objectPools = new Dictionary<string, Stack<GameObject>>();
	

	// 오브젝트 생성
	public static void Create(string name, GameObject prefab, int size, Transform parent)
	{
		Stack<GameObject> objects = new Stack<GameObject>(size);

		for (int i = 0; i < size; i++)
		{
			GameObject gameObj = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent) as GameObject;

			gameObj.SetActive(false);

			objects.Push(gameObj);
		}

		objectPools.Add(name, objects);
	}
	
	/// <summary>
	/// 오브젝트 가져오기
	/// </summary>
	/// <param name="name">name of object</param>
	/// <param name="position">if (position == Vector3.positiveInf) => dont change positon</param>
	/// <returns></returns>
	public static GameObject GetGameObject(string name, Vector3 position)
	{
		Stack<GameObject> objects = objectPools[name];

		if (objects.Count > 0)
		{
			GameObject gameObj = objects.Pop();

			gameObj.SetActive(true);

			if (position.x != Vector3.positiveInfinity.x)
			{
				Debug.Log(position);
				Debug.Log(Vector3.positiveInfinity);
				gameObj.transform.position = position;
			}

			return gameObj;
		}
		else
		{
			return null;
		}
	}

	// 오브젝트 해제
	public static void Release(string name, GameObject gameObj)
	{
		Stack<GameObject> objects = objectPools[name];

		gameObj.SetActive(false);

		objects.Push(gameObj);
	}

	// 풀에서 삭제
	public static void Remove(string name)
	{
		objectPools.Remove(name);
	}
}
