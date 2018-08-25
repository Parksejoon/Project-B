using System.Collections;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private float			destroyTime = 3f;                   // 파괴 시간


	// 시작
	private void Start()
	{
		StartCoroutine(DestroyCoroutine());
	}

	// 삭제 코루틴
	private IEnumerator DestroyCoroutine()
	{
		yield return new WaitForSeconds(destroyTime);

		Destroy(gameObject);
	}
}
