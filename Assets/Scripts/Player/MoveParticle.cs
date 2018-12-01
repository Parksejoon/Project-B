using UnityEngine;

public class MoveParticle : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem		targetParticle;			// 타겟 파티클
	[SerializeField]
	private int					flagSize;				// 플래그 크기
	
	[HideInInspector]
	public	bool[]				flagArray;              // 플래그 배열


	// 초기화
	private void Awake()
	{
		if (targetParticle == null)
		{
			targetParticle = GetComponent<ParticleSystem>();
		}
	}

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
