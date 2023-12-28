using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BF RID: 703
public class AudioVoice_DogBarkAtNight : AudioVoiceStory
{
	// Token: 0x06001149 RID: 4425 RVA: 0x0001EE70 File Offset: 0x0001D270
	protected override IEnumerator mainCoroutine()
	{
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogBark_lesson3, -1));
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogBark_ListenFragment, -1));
		yield return new WaitForSeconds(0.5f);
		this.startFirstDay = true;
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogBark_TheDogWentOut, -1));
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogBark_TheNightCameBy, -1));
		yield return new WaitForSeconds(4f);
		this.voice = Audio.self.playVoice(Voices.VoicePack06.DogBark_HavingTime);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(base.voiceStopped));
		this.voice.start(true);
		this.voiceEndFound = false;
		yield return base.StartCoroutine(base.waitForVoiceEnd());
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogBark_AnswerQyestion, -1));
		if (this.monsterEnd)
		{
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogBark_Deserved, -1));
		}
		else
		{
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogBark_VeryGood, -1));
		}
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogBark_Continue, -1));
		this.canExitPuzzle = true;
		yield break;
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x0001EE8C File Offset: 0x0001D28C
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "ChooseNow" && !this.monsterEnd)
		{
			this.voice.setParameter(1f);
			base.ps.GetComponentsInChildren<PuzzleBarkAtNight_Dog>(true)[0].canBark = false;
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x0001EEE0 File Offset: 0x0001D2E0
	public void end(bool monster)
	{
		this.monsterEnd = monster;
	}

	// Token: 0x04000E50 RID: 3664
	private bool monsterEnd;

	// Token: 0x04000E51 RID: 3665
	public bool startFirstDay;
}
