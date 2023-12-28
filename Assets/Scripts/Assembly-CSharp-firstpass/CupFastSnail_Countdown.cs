using System;
using UnityEngine;

// Token: 0x0200034F RID: 847
public class CupFastSnail_Countdown : MonoBehaviour
{
	// Token: 0x06001497 RID: 5271 RVA: 0x0003852A File Offset: 0x0003692A
	public void Ding()
	{
		Audio.self.playOneShot("b0650508-694a-4b50-a255-1e3669a552a7", 1f);
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x00038541 File Offset: 0x00036941
	public void Dooooong()
	{
		Audio.self.playOneShot("a7cee6dc-3734-43c0-b3f7-e3521606a6ad", 1f);
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x00038558 File Offset: 0x00036958
	public void AnimationEnded()
	{
		base.gameObject.SetActive(false);
	}
}
