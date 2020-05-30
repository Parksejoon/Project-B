using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject			portalPrefab;			// 포탈 프리팹
	[SerializeField]
	private TextPrinter			textPrinter;			// 텍스트 프린터 UI 
	[SerializeField]
	private ConverseSelection	converseSelection;		// 선택지 UI
	[SerializeField]
	private SceneNumber			targetScene;			// 타겟 씬


	// 포탈 생성
	public void SpawnPortal(Vector3 pos)
	{
		var portal = Instantiate(portalPrefab, pos, Quaternion.identity, transform).GetComponent<Portal>();

		portal.SetProp(textPrinter, converseSelection, targetScene);
	}
}
