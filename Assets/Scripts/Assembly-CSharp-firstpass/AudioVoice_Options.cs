using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class AudioVoice_Options : AudioVoice
{
	// Token: 0x060011F2 RID: 4594 RVA: 0x00024A88 File Offset: 0x00022E88
	public void OnVoiceVolumeChanged(float newVolume)
	{
		if (!this.active)
		{
			return;
		}
		if (this.listIndex >= this.list.Length)
		{
			return;
		}
		if (this.oldValue == -1f && newVolume > 0f)
		{
			this.oldValue = newVolume;
			return;
		}
		if (this.oldValue == newVolume)
		{
			return;
		}
		this.oldValue = newVolume;
		if (!this.canPlayNext)
		{
			return;
		}
		if (base.playVoice(this.list[this.listIndex], false, false))
		{
			this.canPlayNext = false;
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				base.StartCoroutine(this.CountCooldown());
			});
			this.listIndex++;
		}
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x00024B40 File Offset: 0x00022F40
	private IEnumerator CountCooldown()
	{
		yield return new WaitForSeconds(this.cooldownTimer);
		this.canPlayNext = true;
		yield break;
	}

	// Token: 0x04000F08 RID: 3848
	[Space(10f)]
	public StandaloneLevelVoice[] list;

	// Token: 0x04000F09 RID: 3849
	private int listIndex;

	// Token: 0x04000F0A RID: 3850
	private float oldValue = -1f;

	// Token: 0x04000F0B RID: 3851
	public float cooldownTimer = 1f;

	// Token: 0x04000F0C RID: 3852
	private bool canPlayNext = true;
}
