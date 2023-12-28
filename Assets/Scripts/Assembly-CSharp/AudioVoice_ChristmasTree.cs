using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A5 RID: 677
public class AudioVoice_ChristmasTree : AudioVoiceDefault
{
	// Token: 0x0600108E RID: 4238 RVA: 0x00019444 File Offset: 0x00017844
	public void tearOff()
	{
		if ((this.voice != null && this.voice.isPlaying()) || this.monthInd == 0)
		{
			this.monthInd++;
			return;
		}
		if (this.monthInd > this.tearOffLines.Length)
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.tearOffLines[this.monthInd - 1].levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.tearOffLines[this.monthInd - 1]);
			this.voice.start(true);
		}
		this.monthInd++;
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x00019501 File Offset: 0x00017901
	public void onDrag()
	{
		if (this.monthInd == 0)
		{
			return;
		}
		if (this.holding != null || this.waitingInd >= this.wait.Length)
		{
			return;
		}
		this.holding = base.StartCoroutine(this.holdingTree());
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x00019540 File Offset: 0x00017940
	public void onDrop()
	{
		if (this.holding == null)
		{
			return;
		}
		base.StopCoroutine(this.holding);
		this.holding = null;
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x00019564 File Offset: 0x00017964
	private IEnumerator holdingTree()
	{
		yield return new WaitForSeconds(8f);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (this.waitingInd >= this.wait.Length || !base.enabled || !this.active)
		{
			yield break;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.wait[this.waitingInd].levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.wait[this.waitingInd]);
			this.voice.start(true);
		}
		this.waitingInd++;
		this.holding = null;
		if (this.waitingInd < this.wait.Length)
		{
			this.holding = base.StartCoroutine(this.holdingTree());
		}
		yield break;
	}

	// Token: 0x04000D90 RID: 3472
	[Space(10f)]
	public StandaloneLevelVoice[] wait;

	// Token: 0x04000D91 RID: 3473
	public StandaloneLevelVoice[] tearOffLines;

	// Token: 0x04000D92 RID: 3474
	private int monthInd;

	// Token: 0x04000D93 RID: 3475
	private Coroutine holding;

	// Token: 0x04000D94 RID: 3476
	private int waitingInd;
}
