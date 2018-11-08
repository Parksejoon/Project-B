using UnityEngine;

public class MoveParticle : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ParticleSystem		targetParticle;			// 타겟 파티클

	// 수치
	[SerializeField]
	private int					flagSize;				// 플래그 크기

	// 인스펙터 비노출 변수
	// 수치
	[HideInInspector]
	public	bool[]				flagArray;				// 플래그 배열


	// 시작
	private void Start()
	{
		flagArray = new bool[flagSize];

		targetParticle.Stop();
	}

	// 프레임
	private void FixedUpdate()
	{
		foreach (bool flag in flagArray)
		{
			if (!flag)
			{
				targetParticle.Stop();

				return;
			}
		}

		if (targetParticle.isStopped)
		{
			targetParticle.Play();
		}
	}
}
