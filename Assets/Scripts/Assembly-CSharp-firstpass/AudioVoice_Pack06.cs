using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class AudioVoice_Pack06 : AudioVoice
{
	// Token: 0x06001207 RID: 4615 RVA: 0x00025358 File Offset: 0x00023758
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.puzzleStats = SerializablePuzzleStats.Get(base.transform.name);
		if (this.puzzleStats.loadedTimes == 0)
		{
			this.firstTimeLoading();
		}
		else
		{
			this.otherTimeLoading();
		}
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x000253B0 File Offset: 0x000237B0
	private void firstTimeLoading()
	{
		SerializablePackSavedStats serializablePackSavedStats = SerializablePackSavedStats.Get(base.GetComponent<levelPackControl>().packIndex - 2);
		if (serializablePackSavedStats.solvedAsBad >= 1 && serializablePackSavedStats.solvedAsGood <= 0)
		{
			this.voice = Audio.self.playVoice(this.lastBad);
			base.StartCoroutine(this.playWaitVoice(this.badWait));
		}
		else
		{
			this.voice = Audio.self.playVoice(this.lastGood);
			base.StartCoroutine(this.playWaitVoice(this.goodWait));
		}
		base.SetPackStartButton(false);
		this.voiceIsPlaying = true;
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.voiceIsPlaying = false;
			base.SetPackStartButton(true);
		});
		this.voice.start(true);
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x00025474 File Offset: 0x00023874
	private void otherTimeLoading()
	{
		if (this.voice != null)
		{
			return;
		}
		CompletionState lastPackCompletionState = Global.self.lastPackCompletionState;
		if (lastPackCompletionState != CompletionState.Good)
		{
			if (lastPackCompletionState != CompletionState.Monster)
			{
				if (lastPackCompletionState == CompletionState.Mixed)
				{
					if (Global.self.CountPackPlayedTimes(0) == 2)
					{
						this.voice = Audio.self.playVoice(this.endMix);
					}
				}
			}
			else if (Global.self.CountPackPlayedTimes(true, 0) == 1 && Global.self.CountPackPlayedTimes(false, 0) == 0)
			{
				this.voice = Audio.self.playVoice(this.endBad);
			}
		}
		else if (Global.self.CountPackPlayedTimes(false, 0) == 1)
		{
			this.voice = Audio.self.playVoice(this.endGood);
		}
		if (this.voice == null)
		{
			return;
		}
		base.SetPackStartButton(false);
		this.voiceIsPlaying = true;
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.voiceIsPlaying = false;
			base.SetPackStartButton(true);
		});
		this.voice.start(true);
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x00025588 File Offset: 0x00023988
	private IEnumerator playWaitVoice(StandaloneLevelVoice line)
	{
		this.playWaitLine = true;
		while (this.voice.isPlaying())
		{
			if (!this.playWaitLine)
			{
				yield break;
			}
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		if (!this.voice.isPlaying() && this.playWaitLine)
		{
			this.voice = Audio.self.playVoice(line);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x000255AC File Offset: 0x000239AC
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "Start"))
			{
				if (!(markerName == "InsertCassete"))
				{
					if (markerName == "StartNoise")
					{
						Audio.self.StartMusic("9034fc39-bcf6-4bc0-acc9-a51016e48790");
					}
				}
				else
				{
					this.PlayCasseteSound();
				}
			}
			else
			{
				base.StartCoroutine(this.startPack(0f));
			}
		}
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x00025634 File Offset: 0x00023A34
	private void PlayCasseteSound()
	{
		VoiceLine voiceLine = Audio.self.playVoice(this.cassete);
		voiceLine.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		voiceLine.start(false);
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x00025670 File Offset: 0x00023A70
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.canExit || this.voiceIsPlaying)
		{
			return false;
		}
		if (!this.active)
		{
			return true;
		}
		this.playWaitLine = false;
		if (click != ClickWhileVoice.start || (this.voice != null && this.voice.isPlaying()))
		{
			if (click == ClickWhileVoice.start)
			{
				Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
			}
			return true;
		}
		if (this.puzzleStats.tryUseOneTime(this.onStartFirst.levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.onStartFirst);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			this.voice.start(true);
			base.SetPackStartButton(false);
			this.canExit = false;
			return false;
		}
		this.PlayCasseteSound();
		this.voice = Audio.self.playVoice(this.onStartClick);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			base.StartCoroutine(this.startPack(1f));
		});
		this.voice.start(true);
		base.SetPackStartButton(false);
		this.canExit = false;
		return false;
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x00025798 File Offset: 0x00023B98
	private IEnumerator startPack(float time = 0f)
	{
		yield return new WaitForSeconds(time);
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startPack();
		UIControl.self.SetSubtitlesYellow(true);
		yield break;
	}

	// Token: 0x04000F1E RID: 3870
	[Space(10f)]
	public StandaloneLevelVoice lastGood;

	// Token: 0x04000F1F RID: 3871
	public StandaloneLevelVoice goodWait;

	// Token: 0x04000F20 RID: 3872
	public StandaloneLevelVoice lastBad;

	// Token: 0x04000F21 RID: 3873
	public StandaloneLevelVoice badWait;

	// Token: 0x04000F22 RID: 3874
	public StandaloneLevelVoice onStartFirst;

	// Token: 0x04000F23 RID: 3875
	public StandaloneLevelVoice onStartClick;

	// Token: 0x04000F24 RID: 3876
	public StandaloneLevelVoice cassete;

	// Token: 0x04000F25 RID: 3877
	private bool voiceIsPlaying;

	// Token: 0x04000F26 RID: 3878
	private bool playWaitLine;

	// Token: 0x04000F27 RID: 3879
	private SerializablePuzzleStats puzzleStats;

	// Token: 0x04000F28 RID: 3880
	private bool canExit = true;

	// Token: 0x04000F29 RID: 3881
	[Space(10f)]
	public StandaloneLevelVoice endGood;

	// Token: 0x04000F2A RID: 3882
	public StandaloneLevelVoice endBad;

	// Token: 0x04000F2B RID: 3883
	public StandaloneLevelVoice endMix;
}
