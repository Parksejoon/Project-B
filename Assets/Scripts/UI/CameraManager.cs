using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public static Camera uiCamera;


	private void Awake()
	{
		uiCamera = GetComponent<Camera>();
	}
}
