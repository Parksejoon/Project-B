using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer	charSprite;             // 캐릭터 스프라이트

	private IEnumerator		blinkingSprite;          // 스프라이트 깜빡거림 코루틴


	// 초기화
	private void Awake()
	{
		blinkingSprite = BlinkingSpriteCoroutine();

		if (charSprite == null)
		{
			GetComponentInChildren<SpriteRenderer>();
		}
	}

	// 블링킹 시작
	public void StartBlinking()
	{
		// 기존 블링킹 코루틴 중지
		StopBlinking();

		// 새로운 블링킹 코루틴 시작
		blinkingSprite = BlinkingSpriteCoroutine();
		StartCoroutine(blinkingSprite);
	}

	// 블링킹 중지
	public void StopBlinking()
	{
		// 기존 블링킹 코루틴 중지
		StopCoroutine(blinkingSprite);

		// 스프라이트 활성화
		charSprite.enabled = true;
	}

	// 블링킹 코루틴
	private IEnumerator BlinkingSpriteCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);

			charSprite.enabled = !charSprite.enabled;
		}
	}
}
