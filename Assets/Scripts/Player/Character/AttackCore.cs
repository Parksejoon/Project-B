using UnityEngine;

public class AttackCore : MonoBehaviour
{
	private int damage;		// 이 공격의 대미지


	// 대미지 설정
	public void SetDamage(int _damage)
	{
		damage = _damage;
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Monster"))
		{
			collision.GetComponent<Monster>().Dealt(damage);
		}
	}
}
