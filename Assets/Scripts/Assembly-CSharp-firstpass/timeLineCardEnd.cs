using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200058A RID: 1418
public class timeLineCardEnd : AbstractUIScreen
{
	// Token: 0x0600209A RID: 8346 RVA: 0x000A043F File Offset: 0x0009E83F
	protected override void cancelPressed()
	{
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x000A0444 File Offset: 0x0009E844
	public override void setScreen(Transform item)
	{
		UIControl.self.endTimeLine();
		this.timerAnimation = base.StartCoroutine(timeLineCardStart.countTimer(this.timer, Time.unscaledTime - timeLineCardStart.packStartTime));
		base.StopCoroutine(this.timerAnimation);
		Audio.self.RevertMusicParameterOverTime("ab175daa-8759-4af9-b3b0-74df51ee0d24", "Pack08 Pitch", this.timeToDecreaseMusicPitch);
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x000A04A4 File Offset: 0x0009E8A4
	public void bContinue()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.gotoNextLevel(false, null);
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x000A04D1 File Offset: 0x0009E8D1
	public void setButton(bool on)
	{
		this.bStart.GetComponent<ButtonTemplate>().setActive(on);
	}

	// Token: 0x040023F3 RID: 9203
	public GameObject bStart;

	// Token: 0x040023F4 RID: 9204
	public Text timer;

	// Token: 0x040023F5 RID: 9205
	private Coroutine timerAnimation;

	// Token: 0x040023F6 RID: 9206
	[Space(10f)]
	public float timeToDecreaseMusicPitch = 5f;
}
