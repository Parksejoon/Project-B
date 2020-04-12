using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 꿈의 마녀 NPC
// 던전 진입, 꿈 조각 설정 NPC 
public class DreamWitch : NPC
{
	// NPC와 대화
	public override TextPrinter Converse()
	{
		Queue<string> textQueue = new Queue<string>();

		textQueue.Enqueue("Ahhhhhhhhhhhh!\nYou so fuckin' precious when you smile\nABC");
		textQueue.Enqueue("Im waking up.\nI feel it in my bones.\nEnough to make my systems blow.");

		OnConverseWindow(textQueue, "DreamWitch");

		return TextPrinter;
	}
}
