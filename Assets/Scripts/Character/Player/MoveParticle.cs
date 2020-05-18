using UnityEngine;

// 이동 파티클
// 파티클 조정(정지, 다시움직임)
public class MoveParticle : MonoBehaviour
{
	// 이동 플래그의 타입
	// ex) flagArray[OnAir] = true -> 공중에 떠있다
	public enum MoveFlagType
	{
		NoMove = 0,
		OnAir = 1
	}

	[SerializeField]
	private ParticleSystem		targetParticle;			// 타겟 파티클
	[SerializeField]
	private int					flagSize;				// 플래그 크기
	
	private bool[]				flagArray;              // 플래그 배열


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

	// 파티클 설정
	public void SetParticleFlag(MoveFlagType moveFlagType, bool isEnable)
	{
		flagArray[(int)moveFlagType] = isEnable;
	}
}
