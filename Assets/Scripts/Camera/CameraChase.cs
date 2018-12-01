using UnityEngine;

public class CameraChase : MonoBehaviour
{
	[SerializeField]
	private Transform targetObject;         // 카메라를 추적할 오브젝트
	[SerializeField]
	private float     speed;				// 속도
	[SerializeField]
	private float	  pocusX;				// 카메라의 초첨 ( X )
	[SerializeField]
	private float	  pocusY;               // 카메라의 초점 ( Y )
	

	// 프레임
	private void FixedUpdate()
	{
		Vector2 tempVector = Vector2.Lerp(transform.position, new Vector2(targetObject.transform.position.x - pocusX, targetObject.transform.position.y - pocusY), Time.deltaTime * speed);
		
		transform.position = new Vector3(tempVector.x, tempVector.y, transform.position.z);
	}
}
