using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	[SerializeField]
	private GameObject	explosion;          // 폭발


	// 시작
	private void Start()
	{
		StartCoroutine("BombCountDown");
	}

	// 폭발
	private void ComitBomb()
	{
		// 폭발 충돌체 소환
		Instantiate(explosion, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
	
	// 폭발 코루틴
	private IEnumerator BombCountDown()
	{
		yield return new WaitForSeconds(1f);

		ComitBomb();
	}
}
