using UnityEngine;

public class MouseChase : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private	float		zPosition = 0;					// z 포지션


	// 프레임
	private void Update()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		position.z = zPosition;
		transform.position = position;
	}
}
