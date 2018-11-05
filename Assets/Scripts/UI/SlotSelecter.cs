using UnityEngine;

public class SlotSelecter : MonoBehaviour
{
	// 인스펙터 노출 변수
	[SerializeField]
	private UITexture[]			selecterTextures;				// 선택 이미지 텍스쳐들


	// 해당 번호 슬롯으로 선택 이미지 설정
	public void SetSelecter(int slotNum)
	{
		foreach (UITexture texture in selecterTextures)
		{
			texture.alpha = 0f;
		}

		selecterTextures[slotNum - 1].alpha = 1f;
	}
}
