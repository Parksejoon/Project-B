using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
	private static Dictionary<string, Stack<GameObject>> objectPools = new Dictionary<string, Stack<GameObject>>();


	private void Awake()
	{
		Debug.Log(gameObject.name);
	}

	// 오브젝트 생성
	public static void Create(string name, GameObject prefab, int size, Transform parent)
	{
		// 이미 풀에 있으면 삭제 후 재생성
		if (objectPools.ContainsKey(name) == true)
		{
			Remove(name);
			Debug.Log("ObjPool: Same object had created.");
		}

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

			// 만약 vector의 요소가 inf이면 position값을 변경하지 않음
			if (position.x != float.PositiveInfinity)
			{
				Debug.Log(position);
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
