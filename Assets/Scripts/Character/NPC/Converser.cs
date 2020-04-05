using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC와 대화가 가능하게 해주는 클래스
public class Converser : MonoBehaviour
{


	// 프레임
	private void Update()
	{
		// (## NPC에게 상호작용/대화 걸기 ##)
		if (Input.GetKeyDown(KeyCode.R))
		{
			Converse();
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
				collider.GetComponent<NPC>().Converse();
				break;
			}
		}
	}
}
