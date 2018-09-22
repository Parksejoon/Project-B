using UnityEngine;

public class BottomBlock : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private float		speed;				// 속도


	// 고정 프레임
	private void FixedUpdate()
	{
		transform.Translate(Vector2.up * speed * Time.smoothDeltaTime);
	}
}
