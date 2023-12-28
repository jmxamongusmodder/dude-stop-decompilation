using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BD RID: 701
public class AudioVoice_CupStone : AudioVoice
{
	// Token: 0x06001139 RID: 4409 RVA: 0x0001E758 File Offset: 0x0001CB58
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.onLoad);
		this.voice.start(true);
		base.StartCoroutine(this.waiting());
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x0001E7A8 File Offset: 0x0001CBA8
	private IEnumerator waiting()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(20f);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (!this.canWait)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.waitLine);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x0001E7C4 File Offset: 0x0001CBC4
	private void OnApplicationFocus(bool focus)
	{
		if (!this.active || !this.canLooseFocus || focus)
		{
			return;
		}
		if (this.voice != null)
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.alttabLine);
		this.voice.start(true);
		this.canLooseFocus = false;
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x0001E830 File Offset: 0x0001CC30
	public void insertStone()
	{
		if (this.insertInd >= this.insertLine.Length)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.insertLine[this.insertInd++]);
		this.voice.start(true);
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x0001E8AC File Offset: 0x0001CCAC
	public void insertLast()
	{
		this.canWait = false;
		this.canLooseFocus = false;
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.endLine);
		this.voice.start(true);
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x0001E90F File Offset: 0x0001CD0F
	public override void subsctibeToEnding(endTextControl item)
	{
		item.SetEnding(LevelVoice.getEndText(this.endText, Global.self.currLanguage), false);
	}

	// Token: 0x04000E3E RID: 3646
	[Space(10f)]
	public StandaloneLevelVoice onLoad;

	// Token: 0x04000E3F RID: 3647
	public StandaloneLevelVoice waitLine;

	// Token: 0x04000E40 RID: 3648
	public StandaloneLevelVoice alttabLine;

	// Token: 0x04000E41 RID: 3649
	public StandaloneLevelVoice[] insertLine;

	// Token: 0x04000E42 RID: 3650
	public StandaloneLevelVoice endLine;

	// Token: 0x04000E43 RID: 3651
	public StandaloneLevelVoice endText;

	// Token: 0x04000E44 RID: 3652
	private bool canWait = true;

	// Token: 0x04000E45 RID: 3653
	private bool canLooseFocus = true;

	// Token: 0x04000E46 RID: 3654
	private int insertInd;
}
