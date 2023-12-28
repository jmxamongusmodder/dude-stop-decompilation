using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003CA RID: 970
public class PuzzleBoxOrHouse_AnimationFinish : MonoBehaviour
{
	// Token: 0x0600184A RID: 6218 RVA: 0x00054BF1 File Offset: 0x00052FF1
	public virtual void StartAnimation()
	{
		Audio.self.playOneShot("65c92fa6-9235-4a8a-bdd2-7eb981bf66c1", 1f);
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x00054C08 File Offset: 0x00053008
	public virtual void EndAnimation()
	{
		base.StartCoroutine(this.EndLevel(this.monster));
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x00054C20 File Offset: 0x00053020
	private IEnumerator EndLevel(bool good)
	{
		if (Global.self.CountPackPlayedTimes(0) == 0)
		{
			yield return new WaitForSeconds(this.waitAfterEnd);
		}
		else
		{
			yield return new WaitForSeconds(this.waitAfterEndSecond);
		}
		AudioVoice_CatBoxOrHouse voice = Global.self.currPuzzle.GetComponent<AudioVoice_CatBoxOrHouse>();
		voice.gotoNext();
		while (!voice.canExit)
		{
			yield return null;
		}
		Global.self.gotoNextLevel(false, null);
		yield break;
	}

	// Token: 0x04001630 RID: 5680
	public bool monster;

	// Token: 0x04001631 RID: 5681
	public float waitAfterEnd = 1f;

	// Token: 0x04001632 RID: 5682
	public float waitAfterEndSecond = 1f;
}
