using UnityEngine;

public class BallGeneratorDisabled : MonoBehaviour
{
	private		GameObject		ballGenerator;              // 볼 제네레이터


	// 초기화
	private void Awake()
	{
		ballGenerator = transform.GetChild(0).gameObject;
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
		{
			EnableGenerator();
		}
	}

	// 제네레이터 활성화
	private void EnableGenerator()
	{
		ballGenerator.SetActive(true);
		Destroy(this);
	}
}
