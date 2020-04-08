using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class InputManager : MonoBehaviour
{
	// input 종류
	public enum InputType
	{
		KeyOrMouseButton,
		MouseMovement,
		JoystickAxis,
	};

	// 싱글톤
	public static InputManager instance;

	// 입력 상수 목록
	private static Dictionary<string, bool> inputCoefficient = new Dictionary<string, bool>();


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		ReadAxes();
	}

	// axis들을 불러오는 함수
	public static void ReadAxes()
	{
		var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];

		SerializedObject obj = new SerializedObject(inputManager);

		SerializedProperty axisArray = obj.FindProperty("m_Axes");

		if (axisArray.arraySize == 0)
		{
			Debug.Log("No Axes");
		}

		for (int i = 0; i < axisArray.arraySize; ++i)
		{
			var axis = axisArray.GetArrayElementAtIndex(i);
			var name = axis.FindPropertyRelative("m_Name").stringValue;
			
			inputCoefficient.Add(name, true);
		}
	}

	// GetAxis
	public static float GetAxis(string axisName)
	{
		return Input.GetAxis(axisName) * (inputCoefficient[axisName] ? 1 : 0);
	}

	// GetButtonDown
	public static bool GetButtonDown(string axisName)
	{
		return Input.GetButtonDown(axisName) && inputCoefficient[axisName];
	}

	// GetButton
	public static bool GetButton(string axisName)
	{
		return Input.GetButton(axisName) && inputCoefficient[axisName];
	}
}
