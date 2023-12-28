using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class AudioVoiceReceive : AudioVoiceDefault
{
	// Token: 0x0600135C RID: 4956 RVA: 0x00015F1A File Offset: 0x0001431A
	public virtual void receiveVoice(VoiceLine line)
	{
		this.voice = line;
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x00015F23 File Offset: 0x00014323
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.waitForVoiceToEnd());
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x00015F48 File Offset: 0x00014348
	private IEnumerator waitForVoiceToEnd()
	{
		bool wasVoice = false;
		while (this.voice != null && this.voice.isPlaying())
		{
			wasVoice = true;
			yield return null;
		}
		if (wasVoice)
		{
			yield return new WaitForSeconds(0.5f);
		}
		this.voice = null;
		this.setActiveAfterVoice();
		yield break;
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x00015F63 File Offset: 0x00014363
	protected override void setActiveDefault()
	{
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x00015F65 File Offset: 0x00014365
	protected virtual void setActiveAfterVoice()
	{
		base.playStartVoice();
	}
}
