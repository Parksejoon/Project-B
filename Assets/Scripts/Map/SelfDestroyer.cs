using System.Collections;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private float			destroyTime = 3f;                   // 파괴 시간
	[SerializeField]
	private string			objectName = "";					// 오브젝트 이름


	// 시작
	private void OnEnable()
	{
		StartCoroutine(DestroyCoroutine());
	}

	// 삭제 코루틴
	private IEnumerator DestroyCoroutine()
	{
		yield return new WaitForSeconds(destroyTime);

		ObjectPoolManager.Release(objectName, gameObject);
	}
}
