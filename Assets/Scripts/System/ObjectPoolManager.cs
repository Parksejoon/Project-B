using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
	public static Hashtable objectPools = new Hashtable();


	// 오브젝트 생성
	public static void Create(string name, GameObject prefab, int size)
	{
		Stack<GameObject> objects = new Stack<GameObject>(size);

		for (int i = 0; i < size; i++)
		{
			GameObject gameObj = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

			gameObj.SetActive(false);

			objects.Push(gameObj);
		}

		objectPools.Add(name, objects);
	}

	// 오브젝트 가져오기
	public static GameObject GetGameObject(string name, Vector3 position)
	{
		Stack<GameObject> objects = (Stack<GameObject>)objectPools[name];
		GameObject gameObj = objects.Pop();

		gameObj.SetActive(true);
		gameObj.transform.position = position;

		return gameObj;
	}

	// 오브젝트 해제
	public static void Release(string name, GameObject gameObj)
	{
		Stack<GameObject> objects = (Stack<GameObject>)objectPools[name];

		gameObj.SetActive(false);

		objects.Push(gameObj);
	}
}
