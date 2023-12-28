using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003DE RID: 990
public class PuzzleCatOnBlackDress_catClimb : MonoBehaviour
{
	// Token: 0x060018F9 RID: 6393 RVA: 0x0005B0AE File Offset: 0x000594AE
	public void StartLongAnimation()
	{
		Audio.self.playOneShot("1fc5f2d9-ce34-4dc9-bfed-d2e99688822c", 1f);
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x0005B0C5 File Offset: 0x000594C5
	public void StartShortAnimation()
	{
		Audio.self.playOneShot("d8fa60a4-faeb-45c4-a686-fb91f8a2f633", 1f);
	}

	// Token: 0x060018FB RID: 6395 RVA: 0x0005B0DC File Offset: 0x000594DC
	public void endAnimation()
	{
		Global.self.currPuzzle.GetComponent<AudioVoice_CatOnBlackDress>().EndClimb();
		base.StartCoroutine(this.endLevel());
	}

	// Token: 0x060018FC RID: 6396 RVA: 0x0005B100 File Offset: 0x00059500
	private IEnumerator endLevel()
	{
		AudioVoice_CatOnBlackDress voice = Global.self.currPuzzle.GetComponent<AudioVoice_CatOnBlackDress>();
		while (!voice.canExit)
		{
			yield return null;
		}
		Global.self.gotoNextLevel(false, null);
		yield break;
	}
}
