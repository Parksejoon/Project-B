using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractedHandler
{
	void Interact(Interacter interacter);       // 상호 작용
	void ExtraInteract();						// 추가 상호작용
}
