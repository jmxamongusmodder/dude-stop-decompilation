using System;

// Token: 0x020002C6 RID: 710
public class AudioVoice_GameIntro_Rules : AudioVoiceParentChange
{
	// Token: 0x0600117B RID: 4475 RVA: 0x00020C24 File Offset: 0x0001F024
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.subtitlesShift = base.ps.subtitlesYShift;
		base.ps.subtitlesYShift = 30f;
		UIControl.positionSubtitles(null);
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x00020CA6 File Offset: 0x0001F0A6
	protected override void whenNewVoiceStarts()
	{
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x00020CA8 File Offset: 0x0001F0A8
	protected override void whenPreviousVoiceStops(VoiceLine line)
	{
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x00020CAC File Offset: 0x0001F0AC
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (markerName == "ShowStart")
			{
				base.ps.subtitlesYShift = this.subtitlesShift;
				UIControl.positionSubtitles(null);
				this.setStartButton(true);
			}
		}
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x00020CFF File Offset: 0x0001F0FF
	private void setStartButton(bool on)
	{
		base.ps.UIScreenCurr.GetComponent<gameIntro_rulesCard>().button.gameObject.SetActive(true);
	}

	// Token: 0x04000E8C RID: 3724
	private float subtitlesShift;
}
