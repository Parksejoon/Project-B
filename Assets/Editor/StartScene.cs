using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

public class StartScene : EditorWindow
{
	[MenuItem("Play/PlayStartScene _%h")]
	public static void PlayStartScene()
	{
		if (EditorSceneManager.GetActiveScene().isDirty)
		{
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		}

		if (!EditorApplication.isPlaying)
		{
			string currentSceneName = EditorSceneManager.GetActiveScene().name;
			File.WriteAllText(".lastScene", currentSceneName);

			EditorSceneManager.OpenScene("Assets/Scenes/InitScene.unity");
			EditorApplication.isPlaying = true;
		}
		if (EditorApplication.isPlaying)
		{
			string lastScene = File.ReadAllText(".lastScene");
			
			EditorApplication.isPlaying = false;
			EditorSceneManager.LoadScene("Assets/Scenes/" + lastScene + ".unity");
		}
	}
}
