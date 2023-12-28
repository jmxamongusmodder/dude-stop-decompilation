using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x020002B8 RID: 696
public class AudioVoice_CupMonster : AudioVoice
{
	// Token: 0x06001113 RID: 4371 RVA: 0x0001D4EF File Offset: 0x0001B8EF
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.onLoad);
		this.voice.start(true);
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0001D528 File Offset: 0x0001B928
	public void throwItem(CupMonsterItems type)
	{
		if (this.itemInd >= this.itemLines.Length)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		if (this.currCoroutine != null)
		{
			base.StopCoroutine(this.currCoroutine);
		}
		this.currCoroutine = base.StartCoroutine(this.playLine(type));
		this.itemInd++;
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x0001D5A8 File Offset: 0x0001B9A8
	private IEnumerator playLine(CupMonsterItems type)
	{
		if (this.itemInd == 0)
		{
			this.voice = Audio.self.playVoice(this.onFirstLine);
			this.voice.start(true);
		}
		if (this.itemInd == 3)
		{
			this.voice = Audio.self.playVoice(this.onThirdLine);
			this.voice.start(true);
		}
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		this.voice = Audio.self.playVoice((from x in this.itemLines
		where x.type == type
		select x).First<AudioVoice_CupMonster.Lines>().line);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x0001D5CA File Offset: 0x0001B9CA
	public void finish()
	{
		base.StartCoroutine(this.end());
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0001D5DC File Offset: 0x0001B9DC
	private IEnumerator end()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		this.voice = Audio.self.playVoice(this.endLine);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x0001D5F7 File Offset: 0x0001B9F7
	public override void subsctibeToEnding(endTextControl item)
	{
		item.SetEnding(LevelVoice.getEndText(this.endText, Global.self.currLanguage), false);
	}

	// Token: 0x04000E12 RID: 3602
	[Space(10f)]
	public StandaloneLevelVoice onLoad;

	// Token: 0x04000E13 RID: 3603
	public StandaloneLevelVoice onFirstLine;

	// Token: 0x04000E14 RID: 3604
	public StandaloneLevelVoice onThirdLine;

	// Token: 0x04000E15 RID: 3605
	public AudioVoice_CupMonster.Lines[] itemLines;

	// Token: 0x04000E16 RID: 3606
	public StandaloneLevelVoice endLine;

	// Token: 0x04000E17 RID: 3607
	public StandaloneLevelVoice endText;

	// Token: 0x04000E18 RID: 3608
	private int itemInd;

	// Token: 0x04000E19 RID: 3609
	private Coroutine currCoroutine;

	// Token: 0x020002B9 RID: 697
	[Serializable]
	public class Lines
	{
		// Token: 0x04000E1A RID: 3610
		public CupMonsterItems type;

		// Token: 0x04000E1B RID: 3611
		public StandaloneLevelVoice line;
	}
}
