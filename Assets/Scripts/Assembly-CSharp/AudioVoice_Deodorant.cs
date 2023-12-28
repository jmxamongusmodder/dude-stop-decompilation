using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class AudioVoice_Deodorant : AudioVoiceScrollable
{
	// Token: 0x06001140 RID: 4416 RVA: 0x0001EC8B File Offset: 0x0001D08B
	public void sprayOnLock()
	{
		if (this.guyOnScreen)
		{
			return;
		}
		base.playVoice(this.onLock, false, true, false, true);
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x0001ECAA File Offset: 0x0001D0AA
	public void clickOnLock()
	{
		if (this.guyOnScreen)
		{
			return;
		}
		base.playVoice(this.clickLock, false, true, false, true);
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x0001ECC9 File Offset: 0x0001D0C9
	public void startSpray()
	{
		if (this.sprayTimeSaid || this.guyOnScreen)
		{
			return;
		}
		this.stopSpray();
		this.spraying = base.StartCoroutine(this.sprayingCoroutine());
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x0001ECFA File Offset: 0x0001D0FA
	public void stopSpray()
	{
		if (this.spraying != null)
		{
			base.StopCoroutine(this.spraying);
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x0001ED14 File Offset: 0x0001D114
	private IEnumerator sprayingCoroutine()
	{
		if (this.sprayTimeSaid)
		{
			this.spraying = null;
			yield break;
		}
		yield return new WaitForSeconds(3f);
		if (this.guyOnScreen)
		{
			yield break;
		}
		this.sprayTimeSaid = base.playVoice(this.ripOzone, false, true, false, true);
		yield break;
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x0001ED2F File Offset: 0x0001D12F
	public void showGuy()
	{
		this.guyOnScreen = (true && this.active);
		base.playVoice(this.guy, true, false, true, true);
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x0001ED57 File Offset: 0x0001D157
	public void sprayGuyGood()
	{
		base.playVoice(this.spray, true, false, true, true);
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x0001ED6A File Offset: 0x0001D16A
	public void sprayGuyBad()
	{
		base.playVoice(this.bad, true, false, true, true);
	}

	// Token: 0x04000E47 RID: 3655
	[Space(10f)]
	public StandaloneLevelVoice[] onLock;

	// Token: 0x04000E48 RID: 3656
	public StandaloneLevelVoice[] clickLock;

	// Token: 0x04000E49 RID: 3657
	public StandaloneLevelVoice[] ripOzone;

	// Token: 0x04000E4A RID: 3658
	public StandaloneLevelVoice[] guy;

	// Token: 0x04000E4B RID: 3659
	public StandaloneLevelVoice[] spray;

	// Token: 0x04000E4C RID: 3660
	public StandaloneLevelVoice[] bad;

	// Token: 0x04000E4D RID: 3661
	private Coroutine spraying;

	// Token: 0x04000E4E RID: 3662
	private bool sprayTimeSaid;

	// Token: 0x04000E4F RID: 3663
	private bool guyOnScreen;
}
