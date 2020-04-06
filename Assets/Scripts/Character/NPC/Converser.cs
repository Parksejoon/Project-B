using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC와 대화가 가능하게 해주는 클래스
public class Converser : MonoBehaviour
{
	private bool interactionAxisInUse = false;      // 대화 입력 플래그
	private bool inConversation = false;            // 대화중인지 플래그

	private TextPrinter npcTextPrinter;				// 대화중인 NPC의 textprinter


	// 프레임
	private void Update()
	{
		// 입력 트리거
		if (Input.GetAxisRaw("Interaction") != 0)
		{
			if (interactionAxisInUse == false)
			{
				// 대화중이 아니면
				if (inConversation == false)
				{
					// 대화
					interactionAxisInUse = true;

					Converse();
				}
				// 이미 대화중이면
				else
				{
					if (npcTextPrinter != null)
					{
						// 스킵
						npcTextPrinter.SkipText();
					}
				}
			}
		}

		// 입력 해제 트리거
		if (Input.GetAxisRaw("Interaction") == 0)
		{
			interactionAxisInUse = false;
		}
	}

	// 주위에 NPC를 찾아서 대화를 거는 함수
	private void Converse()
	{
		Collider2D[] hitColliders2D = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0);

		// 모든 충돌체를 확인
		foreach (Collider2D collider in hitColliders2D)
		{
			// NPC가 있으면
			if (collider.CompareTag("NPC"))
			{
				// 대화
				npcTextPrinter = collider.GetComponent<NPC>().Converse();
				inConversation = true;

				break;
			}
		}
	}

	// 대화 종료
	public void EndConverse()
	{
		inConversation = false;
		npcTextPrinter = null;
	}
}
