using System.Collections;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
	[SerializeField]
	private float			destroyTime = 3f;                   // 파괴 시간
	[SerializeField]
	private bool			useObjectPool;						// 오브젝트 풀 사용 여부
	[SerializeField]
	private string			objectName = "";                    // 오브젝트 이름


	// 시작
	private void OnEnable()
	{
		StartCoroutine(DestroyCoroutine());
	}

	// 삭제 코루틴
	private IEnumerator DestroyCoroutine()
	{
		yield return new WaitForSeconds(destroyTime);

		if (useObjectPool)
		{
			ObjectPoolManager.Release(objectName, gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
