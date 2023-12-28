using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class AudioVoice_Pack08_FirstScreen : AudioVoice
{
	// Token: 0x06001223 RID: 4643 RVA: 0x000262ED File Offset: 0x000246ED
	private void Awake()
	{
		base.StartCoroutine(this.delayedHide());
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x000262FC File Offset: 0x000246FC
	private IEnumerator delayedHide()
	{
		yield return null;
		SerializablePuzzleStats sps = SerializablePuzzleStats.Get(base.transform.name);
		if (sps.loadedTimes == 0)
		{
			this.playLine = true;
			global::Console.self.canOpen = false;
		}
		this.setButton(false);
		yield break;
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x00026318 File Offset: 0x00024718
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (this.playLine && this.voiceLine != null)
		{
			this.voice = Audio.self.playVoice(this.voiceLine);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.setButton(true);
			});
			this.voice.start(true);
		}
		else
		{
			VoiceToPlay voiceTypeToPlay = AudioVoice.getVoiceTypeToPlay(SerializablePuzzleStats.Get(base.transform.name).loadedTimes);
			if (voiceTypeToPlay != VoiceToPlay.silent)
			{
				StandaloneLevelVoiceGuid notRepeatingVoice = AudioVoice.getNotRepeatingVoice(base.transform.name, this.onLoad, LevelVoice.Type.Start, null);
				if (notRepeatingVoice != null)
				{
					this.voice = Audio.self.playVoice(notRepeatingVoice);
					this.voice.subscribeToStopped(this, delegate(VoiceLine line)
					{
						this.setButton(true);
					});
					this.voice.start(true);
				}
			}
		}
		if (this.voice == null)
		{
			this.setButton(true);
		}
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x0002641D File Offset: 0x0002481D
	private void setButton(bool on)
	{
		if (this.start)
		{
			base.ps.UIScreenCurr.GetComponent<timeLineCardStart>().setButton(on);
		}
		else
		{
			base.ps.UIScreenCurr.GetComponent<timeLineCardEnd>().setButton(on);
		}
	}

	// Token: 0x04000F3F RID: 3903
	private bool playLine;

	// Token: 0x04000F40 RID: 3904
	[Header("Rnd Lines")]
	public StandaloneLevelVoice onLoad;

	// Token: 0x04000F41 RID: 3905
	[Space(10f)]
	public bool start = true;
}
