using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : NPC
{
	[SerializeField]
	private SceneNumber portalTargetScene;          // 이동할 씬


	// 프로퍼티 셋
	public void SetProp(TextPrinter textPrinter, ConverseSelection converseSelection, SceneNumber sceneNumber)
	{
		textPrinterWindow = textPrinter;
		converseSelectionWindow = converseSelection;
		portalTargetScene = sceneNumber;
	}

	// 대화 시작
	public override void StartConverse()
	{
		converseSelectionWindow.SetChoices(new ConverseSelection.ChoiceCallback[] { ChangeScene, ExitConverse },
																	new string[] { "Back To " + portalTargetScene, "Exit" });

		converseSelectionWindow.EnableChoices();
	}

	// 추가 상호 작용
	public override void ExtraInteract()
	{
		ChangeScene();
	}

	// 이동 확인
	public void ChangeScene()
	{
		EndInteract();
		SceneManager.LoadScene((int)portalTargetScene);
	}

	// 이동 취소
	public void ExitConverse()
	{
		EndInteract();
	}
}
