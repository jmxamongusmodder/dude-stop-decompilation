using System;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class AudioVoice_Pack1_NoReward : AudioVoice
{
	// Token: 0x06001242 RID: 4674 RVA: 0x00026EF4 File Offset: 0x000252F4
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		int num = Global.self.CountPackPlayedTimes(0) - 2;
		if (num < 0 || num > this.list.Length - 1)
		{
			return;
		}
		this.canExit = false;
		this.voice = Audio.self.playVoice(this.list[num]);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<ScreenEndCard_Pack01>().buttonContinue.setActive(false);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.canExit = true;
			base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<ScreenEndCard_Pack01>().buttonContinue.setActive(true);
		});
		this.voice.start(true);
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x00026FB1 File Offset: 0x000253B1
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "Achievement")
		{
			Debug.Log("Get achievement");
		}
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x00026FD5 File Offset: 0x000253D5
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		return this.active && this.canExit;
	}

	// Token: 0x04000F52 RID: 3922
	public StandaloneLevelVoice[] list;

	// Token: 0x04000F53 RID: 3923
	private bool canExit = true;
}
