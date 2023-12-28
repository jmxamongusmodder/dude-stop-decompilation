using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class AudioVoice_Pack09_Intro_1 : AudioVoice
{
	// Token: 0x06001234 RID: 4660 RVA: 0x00026B5D File Offset: 0x00024F5D
	private void Awake()
	{
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x00026B6C File Offset: 0x00024F6C
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.start(true);
		global::Console.self.canOpen = false;
		AudioVoice_Pack09_Intro_1.escapePressed = false;
		base.StartCoroutine(this.escPressed());
		Audio.self.playLoopSound("ef7abcd3-2afb-49b8-8428-ace9ac9ae158");
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x00026BDC File Offset: 0x00024FDC
	private IEnumerator escPressed()
	{
		while (!Input.GetButtonDown("Cancel") || AudioVoice_Pack09_Intro_1.escapePressed || (this.voice != null && this.voice.isPlaying()))
		{
			yield return null;
		}
		this.voice = Audio.self.playVoice(this.ESCLine);
		this.voice.start(true);
		AudioVoice_Pack09_Intro_1.escapePressed = true;
		yield break;
		yield break;
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x00026BF8 File Offset: 0x00024FF8
	public void onContinue()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.continueLine);
		this.voice.start(true);
	}

	// Token: 0x04000F4C RID: 3916
	[Space(10f)]
	public StandaloneLevelVoice ESCLine;

	// Token: 0x04000F4D RID: 3917
	public StandaloneLevelVoice continueLine;

	// Token: 0x04000F4E RID: 3918
	public static bool escapePressed;
}
