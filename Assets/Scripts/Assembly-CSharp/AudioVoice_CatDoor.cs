using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A3 RID: 675
public class AudioVoice_CatDoor : AudioVoiceStory
{
	// Token: 0x0600107F RID: 4223 RVA: 0x0001861C File Offset: 0x00016A1C
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(Voices.VoicePack06.CatDoor_Lesson3);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x00018678 File Offset: 0x00016A78
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "Interrupt" || markerName == "Short")
		{
			if (this.ended != true)
			{
				return;
			}
			this.ended = new bool?(false);
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			base.StartCoroutine(this.playEndVoice(markerName == "Short"));
		}
		if (markerName == "EndCassete")
		{
			Audio.self.StopMusic("9034fc39-bcf6-4bc0-acc9-a51016e48790");
			Audio.self.playOneShot(LevelVoice.getVoice(Voices.VoicePack06.CatDoor_EndCassette).guid.ToString(), 1f);
			UIControl.self.SetSubtitlesYellow(false);
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00018770 File Offset: 0x00016B70
	private IEnumerator playEndVoice(bool Short)
	{
		yield return new WaitForSeconds(1f);
		if (Short)
		{
			if (this.monsterEnd)
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack06.CatDoor_JustThisOne);
			}
			else
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack06.CatDoor_ToGetAttention);
			}
		}
		else if (this.monsterEnd)
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack06.CatDoor_JustDim);
		}
		else
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack06.CatDoor_WantOutside);
		}
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			base.StartCoroutine(this.endVoice());
		});
		this.voice.start(true);
		yield break;
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x00018794 File Offset: 0x00016B94
	private IEnumerator endVoice()
	{
		yield return new WaitForSeconds(1f);
		this.voice = Audio.self.playVoice(Voices.VoicePack06.CatDoor_EndLesson);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.endLevel));
		if (SerializablePuzzleStats.Get(base.transform.name).loadedTimes > 2)
		{
			this.voice.setParameter(1f);
		}
		this.voice.start(true);
		yield break;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x000187AF File Offset: 0x00016BAF
	private void endLevel(VoiceLine line)
	{
		this.canExitPuzzle = true;
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x000187B8 File Offset: 0x00016BB8
	public void end(bool monster)
	{
		this.monsterEnd = monster;
		bool? flag = this.ended;
		if (flag == null)
		{
			this.ended = new bool?(true);
			if (this.voice == null || !this.voice.isPlaying())
			{
				base.StartCoroutine(this.playEndVoice(false));
			}
		}
	}

	// Token: 0x04000D87 RID: 3463
	private bool? ended;

	// Token: 0x04000D88 RID: 3464
	private bool monsterEnd;
}
