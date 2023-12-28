using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public class AudioVoice_ScrollableEnd : AudioVoice
{
	// Token: 0x060012E3 RID: 4835 RVA: 0x0002D0CC File Offset: 0x0002B4CC
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (Global.self.pack10CutsceneActive)
		{
			this.active = false;
		}
		if (!this.active)
		{
			return;
		}
		if (this.allIsDone)
		{
			if (this.noItemsFound)
			{
				this.playVoice(this.allGood);
			}
			else
			{
				this.playVoice(this.someItems);
			}
		}
		else
		{
			this.playVoice(this.notFinished);
		}
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x0002D146 File Offset: 0x0002B546
	public void setCompletion(bool allDone, bool noItems)
	{
		this.allIsDone = allDone;
		this.noItemsFound = noItems;
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x0002D156 File Offset: 0x0002B556
	public void setBadProgress(float prog)
	{
		this.badEnd = ((double)prog >= 0.5);
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x0002D170 File Offset: 0x0002B570
	private void playVoice(StandaloneLevelVoice line)
	{
		if (!SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(line.levelVoiceId))
		{
			return;
		}
		this.canExit = false;
		this.voice = Audio.self.playVoice(line);
		this.voice.subscribeToStopped(this, delegate(VoiceLine l)
		{
			this.canExit = true;
		});
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x0002D1F3 File Offset: 0x0002B5F3
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "badEnd" && !this.badEnd)
		{
			this.voice.stop();
		}
	}

	// Token: 0x04000FE2 RID: 4066
	public StandaloneLevelVoice notFinished;

	// Token: 0x04000FE3 RID: 4067
	public StandaloneLevelVoice someItems;

	// Token: 0x04000FE4 RID: 4068
	public StandaloneLevelVoice allGood;

	// Token: 0x04000FE5 RID: 4069
	private bool allIsDone;

	// Token: 0x04000FE6 RID: 4070
	private bool noItemsFound;

	// Token: 0x04000FE7 RID: 4071
	private bool badEnd;

	// Token: 0x04000FE8 RID: 4072
	[HideInInspector]
	public bool canExit = true;
}
