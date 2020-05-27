using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StartScene : EditorWindow
{
	[MenuItem("SceneManager/PlayStartScene _%h")]
	public static void PlayStartScene()
	{
		if (!EditorApplication.isPlaying)
		{
			if (EditorSceneManager.GetActiveScene().isDirty)
			{
				EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			}

			string currentSceneName = EditorSceneManager.GetActiveScene().name;
			File.WriteAllText(".lastScene", currentSceneName);

			
			EditorSceneManager.OpenScene("Assets/Scenes/InitScene.unity");
			EditorApplication.isPlaying = true;
		}

		if (EditorApplication.isPlaying)
		{
			EditorApplication.isPlaying = false;
			EditorApplication.isPaused = false;

			// 콜백 추가
			EditorApplication.playModeStateChanged += (PlayModeStateChange state) =>
			{
				if (state == PlayModeStateChange.EnteredEditMode)
				{
					ReturnPreviousScene();
				}
			};
		}
	}

	[MenuItem("SceneManager/ReturnPreviousScene _%g")]
	public static void ReturnPreviousScene()
	{
		string lastScene = File.ReadAllText(".lastScene");

		EditorSceneManager.OpenScene("Assets/Scenes/" + lastScene + ".unity");
	}
}
