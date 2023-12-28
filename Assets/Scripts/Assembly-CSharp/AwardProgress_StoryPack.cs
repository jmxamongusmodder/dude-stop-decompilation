using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
public class AwardProgress_StoryPack : MonoBehaviour, IAwardProgress
{
	// Token: 0x060013FF RID: 5119 RVA: 0x0003280E File Offset: 0x00030C0E
	public void calculate()
	{
		UIControl.self.hideCompletionLine(true);
	}
}
