using UnityEngine;

public class BottomBlock : MonoBehaviour
{
	[SerializeField]
	private float		speed;				// 속도


	// 고정 프레임
	private void FixedUpdate()
	{
		transform.Translate(Vector2.up * speed * Time.smoothDeltaTime);
	}
}
