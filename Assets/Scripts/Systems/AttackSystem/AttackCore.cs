using UnityEngine;

public class AttackCore : MonoBehaviour
{
	[SerializeField]
	private int			damage;     // 이 공격의 대미지
	[SerializeField]
	private string		targetTag;	// 이 공격의 대상 태그


	// 공격 설정
	public void SetAttack(int _damage, string _targetTag)
	{
		damage = _damage;
		targetTag = _targetTag;
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(targetTag))
		{
			collision.GetComponent<Character>().Dealt(damage, transform.position);
		}
	}
}
