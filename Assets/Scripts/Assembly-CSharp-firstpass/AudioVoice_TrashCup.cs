using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class AudioVoice_TrashCup : AudioVoiceParentChange
{
	// Token: 0x06001319 RID: 4889 RVA: 0x0002E2B6 File Offset: 0x0002C6B6
	public override void setActive(bool on)
	{
		base.setActive(on);
		Global.self.isGameIntroJustFinished = true;
		Global.self.canExitEndScreen = true;
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x0002E2D5 File Offset: 0x0002C6D5
	protected override void whenNewVoiceStarts()
	{
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x0002E308 File Offset: 0x0002C708
	protected override void whenPreviousVoiceStops(VoiceLine line)
	{
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.start(true);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
		this.voice.setParameter("Skip", 1f);
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x0002E37D File Offset: 0x0002C77D
	private void voiceStopped(VoiceLine line)
	{
		this.canGetCup = true;
		base.StartCoroutine(this.Wait());
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x0002E394 File Offset: 0x0002C794
	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(5f);
		if (this.levelEnded)
		{
			yield break;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.waitLine);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x0002E3AF File Offset: 0x0002C7AF
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (markerName == "StartMusic")
			{
				Audio.self.StartSoloSnapshot(MusicTypes.MenuMusic, true);
			}
		}
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x0002E3E8 File Offset: 0x0002C7E8
	public override void subsctibeToEnding(endTextControl item)
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			UIControl.self.hideSubtitles();
			this.voice.stop();
		}
		this.levelEnded = true;
		this.voice = Audio.self.playVoice(this.endLine);
		base.subscribeToMarkers(item, true);
		this.voice.start(false);
	}

	// Token: 0x04001014 RID: 4116
	public StandaloneLevelVoice waitLine;

	// Token: 0x04001015 RID: 4117
	public StandaloneLevelVoice endLine;

	// Token: 0x04001016 RID: 4118
	public bool canGetCup;

	// Token: 0x04001017 RID: 4119
	private bool levelEnded;
}
