using System;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class AudioVoice_CupRoboCup : AudioVoice
{
	// Token: 0x06001129 RID: 4393 RVA: 0x0001DFF8 File Offset: 0x0001C3F8
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.startLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x0001E054 File Offset: 0x0001C454
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "Show")
		{
			foreach (GameObject gameObject in this.cupPanel)
			{
				gameObject.SetActive(true);
			}
			Audio.self.playOneShot("f6756bdf-4694-4671-b609-1ad4f22fa8a1", 1f);
		}
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x0001E0B4 File Offset: 0x0001C4B4
	public void placePart()
	{
		this.checkLastPart();
		if (this.lastPartLeft)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			return;
		}
		if (this.partOnVoiceCount >= this.partLineList.Length)
		{
			return;
		}
		if (this.partSnappedCount > 1 && this.partOnVoiceCount == 0)
		{
			this.partOnVoiceCount = 1;
		}
		this.voice = Audio.self.playVoice(this.partLineList[this.partOnVoiceCount]);
		this.voice.start(true);
		this.partOnVoiceCount++;
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0001E158 File Offset: 0x0001C558
	private void checkLastPart()
	{
		this.partSnappedCount++;
		if (this.lastPartLeft)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.lastPart);
			this.voice.start(true);
		}
		else if (this.partSnappedCount == this.cupPanel.Length - 2)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.lastPartHint);
			this.voice.start(true);
			this.lastPartLeft = true;
		}
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x0001E230 File Offset: 0x0001C630
	public void cantSnapNow()
	{
		if (this.cantSnapPlayed)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.hintLine);
		this.voice.start(true);
		this.cantSnapPlayed = true;
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x0001E298 File Offset: 0x0001C698
	public override void subsctibeToEnding(endTextControl item)
	{
		item.SetEnding(LevelVoice.getEndText(this.endText, Global.self.currLanguage), false);
	}

	// Token: 0x04000E2D RID: 3629
	[Space(10f)]
	public StandaloneLevelVoice startLine;

	// Token: 0x04000E2E RID: 3630
	public StandaloneLevelVoice[] partLineList;

	// Token: 0x04000E2F RID: 3631
	public StandaloneLevelVoice hintLine;

	// Token: 0x04000E30 RID: 3632
	public StandaloneLevelVoice lastPartHint;

	// Token: 0x04000E31 RID: 3633
	public StandaloneLevelVoice lastPart;

	// Token: 0x04000E32 RID: 3634
	public StandaloneLevelVoice endText;

	// Token: 0x04000E33 RID: 3635
	[Space(10f)]
	public GameObject[] cupPanel;

	// Token: 0x04000E34 RID: 3636
	private int partOnVoiceCount;

	// Token: 0x04000E35 RID: 3637
	private int partSnappedCount;

	// Token: 0x04000E36 RID: 3638
	private bool lastPartLeft;

	// Token: 0x04000E37 RID: 3639
	private bool cantSnapPlayed;
}
