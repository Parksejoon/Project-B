using UnityEngine;

public class MouseChase : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private	float		zPosition = 0;                  // z 포지션
	[SerializeField]
	private float		speed = 1;						// 속도


	// 고정 프레임
	private void FixedUpdate()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 tempVector;

		if (Vector2.Distance(transform.position, position) < 1)
		{
			tempVector = Vector2.Lerp(transform.position, new Vector2(position.x, position.y), Time.deltaTime * speed);
		}
		else
		{
			tempVector = Vector2.MoveTowards(transform.position, new Vector2(position.x, position.y), Time.deltaTime * speed);
		}

		transform.position = new Vector3(tempVector.x, tempVector.y, zPosition);
	}
}
