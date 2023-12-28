using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public class AudioVoice_HundredPhotos : AudioVoiceDefault
{
	// Token: 0x0600118A RID: 4490 RVA: 0x00020FC4 File Offset: 0x0001F3C4
	public override void setActive(bool on)
	{
		if (!AudioVoice_PhoneCharge.checkIfDuck())
		{
			base.setActive(on);
			Global.self.DuckInPack07IsActive = false;
		}
		else if (on)
		{
			this.active = on;
			this.duck = true;
			StandaloneLevelVoiceGuid voice = LevelVoice.getVoice(Voices.VoicePack07_Duck.Hundred_onLoad, LevelVoice.Type.Start, new bool?(true));
			this.voice = Audio.self.playVoice(voice);
			this.voice.start(true);
		}
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x00021038 File Offset: 0x0001F438
	private void PlayDuckEnd()
	{
		this.phone.disablePuzzle();
		global::Console.self.canOpen = false;
		Global.self.DuckInPack07IsActive = true;
		Global.self.queuePuzzleIndex = 0;
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Hundred_ArrNoEnough);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x000210AC File Offset: 0x0001F4AC
	public IEnumerator playVoice(StandaloneLevelVoice line, float delay = 0f)
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (delay != 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		this.voice = Audio.self.playVoice(line);
		this.voice.start(true);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x000210D5 File Offset: 0x0001F4D5
	public bool isVoicePlaying()
	{
		return this.voice != null && this.voice.isPlaying();
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000210F0 File Offset: 0x0001F4F0
	public void playYESLine(Action<string> callback)
	{
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Hundred_YES);
		this.voice.start(true);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.callbackYes = callback;
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x00021140 File Offset: 0x0001F540
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (this.callbackYes != null)
		{
			this.callbackYes(markerName);
		}
		if (markerName != null)
		{
			if (!(markerName == "Stop"))
			{
				if (!(markerName == "OpenConsole"))
				{
					if (!(markerName == "PauseMusic"))
					{
						if (markerName == "UnpauseMusic")
						{
							Audio.self.StopMusicParameterOverTime();
						}
					}
					else
					{
						Audio.self.ChangeMusicParameterOverTime("ab175daa-8759-4af9-b3b0-74df51ee0d24", "Pitch Stop", 2f);
					}
				}
				else
				{
					global::Console.self.showConsole(global::Console.self.hundredPhotosDuck);
				}
			}
			else
			{
				GlitchEffectController.self.stopGlitch();
				Audio.self.stopLoopSound("0221e0b9-6db2-4fac-8026-65fe599940ba", true);
				global::Console.self.resetConsole();
				Global.LevelCompleted(0f, true);
			}
		}
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x00021231 File Offset: 0x0001F631
	public void endConsoleAnimation()
	{
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Hundred_End);
		this.voice.start(true);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x00021270 File Offset: 0x0001F670
	public void takePhoto(int count)
	{
		if (!this.active)
		{
			return;
		}
		if (this.duck)
		{
			if (count == 56)
			{
				if (this.voice != null && this.voice.isPlaying())
				{
					this.voice.stop();
				}
				this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Hundred_OnClick1);
				this.voice.start(true);
			}
			if (count == 58)
			{
				if (this.voice != null && this.voice.isPlaying())
				{
					this.voice.stop();
				}
				this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Hundred_OnClick2);
				this.voice.start(true);
			}
			if (count == 59)
			{
				if (this.voice != null && this.voice.isPlaying())
				{
					this.voice.stop();
				}
				this.PlayDuckEnd();
			}
			return;
		}
		if (count == 58)
		{
			if (!SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.voiceOn4.levelVoiceId))
			{
				return;
			}
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.voiceOn4);
			this.voice.start(true);
		}
		if (count == 85)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.voiceOn85);
			this.voice.start(true);
		}
		else if (count == 120)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.voiceOn120);
			this.voice.start(true);
		}
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x00021484 File Offset: 0x0001F884
	public void onPickPhoneAfter4()
	{
		if (!this.active || this.pickOnce)
		{
			return;
		}
		if (!SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.voiceOnPickPhone.levelVoiceId))
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.voiceOnPickPhone);
		this.voice.start(true);
		this.pickOnce = true;
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x00021520 File Offset: 0x0001F920
	public override void subsctibeToEnding(endTextControl item)
	{
		if (this.duck)
		{
			if (base.ps.solvedAsBad == true)
			{
				base.subscribeToMarkers(item, false);
			}
			else
			{
				if (this.voice != null)
				{
					this.voice.stop();
				}
				StandaloneLevelVoiceGuid notRepeatingVoice = AudioVoice.getNotRepeatingVoice(base.transform.name, Voices.VoicePack07_Duck.Hundred_onLoad, LevelVoice.Type.End, new bool?(false));
				this.voice = Audio.self.playVoice(notRepeatingVoice);
				this.voice.start(true);
				string endText = LevelVoice.getEndText(notRepeatingVoice, null, false, Global.self.currLanguage);
				this.setEndText(item, endText, false);
			}
		}
		else
		{
			base.subsctibeToEnding(item);
		}
	}

	// Token: 0x04000E9A RID: 3738
	[Space(10f)]
	public StandaloneLevelVoice voiceOn85;

	// Token: 0x04000E9B RID: 3739
	public StandaloneLevelVoice voiceOn120;

	// Token: 0x04000E9C RID: 3740
	public StandaloneLevelVoice voiceOn4;

	// Token: 0x04000E9D RID: 3741
	public StandaloneLevelVoice voiceOnPickPhone;

	// Token: 0x04000E9E RID: 3742
	private bool pickOnce;

	// Token: 0x04000E9F RID: 3743
	[Header("DUCK")]
	public PuzzleHundredPhotos_Phone phone;

	// Token: 0x04000EA0 RID: 3744
	[HideInInspector]
	public bool duck;

	// Token: 0x04000EA1 RID: 3745
	private Action<string> callbackYes;
}
