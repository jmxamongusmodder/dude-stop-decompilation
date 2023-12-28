using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class AudioVoiceStory : AudioVoice
{
	// Token: 0x0600136D RID: 4973 RVA: 0x00016C0E File Offset: 0x0001500E
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.mainCoroutine());
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x00016C30 File Offset: 0x00015030
	protected virtual IEnumerator mainCoroutine()
	{
		yield return null;
		yield break;
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x00016C44 File Offset: 0x00015044
	protected virtual IEnumerator tryInterrupt()
	{
		yield return null;
		yield break;
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x00016C58 File Offset: 0x00015058
	protected IEnumerator tryInterrupOverTime(float wait)
	{
		float time = Time.fixedTime;
		while (time + wait > Time.fixedTime && !this.interruptHappened)
		{
			yield return base.StartCoroutine(this.tryInterrupt());
		}
		yield break;
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x00016C7C File Offset: 0x0001507C
	protected IEnumerator loopLine(StandaloneLevelVoice line)
	{
		if (line == null)
		{
			yield break;
		}
		yield return base.StartCoroutine(this.tryInterrupOverTime(3f));
		if (this.interruptHappened)
		{
			yield break;
		}
		yield return base.StartCoroutine(this.playLine(Voices.VoicePack06.LoopStart_First, -1));
		yield return base.StartCoroutine(this.playLine(line, -1));
		yield return base.StartCoroutine(this.tryInterrupOverTime(4f));
		if (this.interruptHappened)
		{
			yield break;
		}
		yield return base.StartCoroutine(this.playLine(Voices.VoicePack06.LoopStart_Second, -1));
		yield return base.StartCoroutine(this.playLine(line, -1));
		this.repeatLine = null;
		yield break;
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x00016CA0 File Offset: 0x000150A0
	protected IEnumerator playLine(StandaloneLevelVoice line, int param = -1)
	{
		if (this.canExitPuzzle)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(line);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
		if (param != -1)
		{
			this.voice.setParameter((float)param);
		}
		this.voice.start(true);
		this.voiceEndFound = false;
		yield return base.StartCoroutine(this.waitForVoiceEnd());
		yield break;
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x00016CCC File Offset: 0x000150CC
	protected IEnumerator playLineWithoutCheck(StandaloneLevelVoice line)
	{
		this.voice = Audio.self.playVoice(line);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
		this.voice.start(true);
		this.voiceEndFound = false;
		yield return base.StartCoroutine(this.waitForVoiceEnd());
		yield break;
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x00016CEE File Offset: 0x000150EE
	protected void voiceStopped(VoiceLine line)
	{
		this.voiceEndFound = true;
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x00016CF8 File Offset: 0x000150F8
	protected IEnumerator waitForVoiceEnd()
	{
		while (!this.voiceEndFound)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001049 RID: 4169
	protected bool voiceEndFound;

	// Token: 0x0400104A RID: 4170
	[HideInInspector]
	public bool canExitPuzzle;

	// Token: 0x0400104B RID: 4171
	protected bool interruptHappened;

	// Token: 0x0400104C RID: 4172
	protected StandaloneLevelVoice repeatLine;
}
