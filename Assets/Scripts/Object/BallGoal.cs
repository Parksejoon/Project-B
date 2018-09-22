using UnityEngine;

public class BallGoal : MonoBehaviour
{
	// 인스펙터 노출 변수
	[SerializeField]
	private		GameObject			targetDoor;				// 타겟 도어


	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
		{
			OpenDoor();

			Destroy(this);
		}
	}

	// 문 열기
	private void OpenDoor()
	{
		if (targetDoor != null)
		{
			Destroy(targetDoor);
		}
	}
}
