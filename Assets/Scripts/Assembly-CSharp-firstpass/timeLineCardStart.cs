using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200058B RID: 1419
public class timeLineCardStart : AbstractUIScreen
{
	// Token: 0x0600209F RID: 8351 RVA: 0x000A04F7 File Offset: 0x0009E8F7
	protected override void cancelPressed()
	{
		if (!this.bStart.activeInHierarchy)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.levelPackMenu[Global.self.currentLevelPack], Vector2.left, true);
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x000A052F File Offset: 0x0009E92F
	public override void setScreen(Transform item)
	{
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x000A0534 File Offset: 0x0009E934
	public void bContinue()
	{
		if (!this.active)
		{
			return;
		}
		Audio.self.ChangeMusicParameterOverTime("ab175daa-8759-4af9-b3b0-74df51ee0d24", "Pack08 Pitch", this.timeToIncreaseMusicPitch);
		base.StartCoroutine(timeLineCardStart.countTimer(this.timer, 0f));
		timeLineCardStart.packStartTime = Time.unscaledTime;
		UIControl.self.startTimeLine();
		AudioVoiceRapidFire.puzzleIndex = 0;
		Global.self.gotoNextLevel(false, null);
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x000A05AC File Offset: 0x0009E9AC
	public static IEnumerator countTimer(Text textBox, float time)
	{
		float total = time;
		for (; ; )
		{
			total += Time.deltaTime;
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)total);
			textBox.text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", new object[]
			{
				timeSpan.Hours,
				timeSpan.Minutes,
				timeSpan.Seconds,
				Mathf.RoundToInt((float)timeSpan.Milliseconds * 0.099f)
			});
			yield return null;
		}
		yield break;
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x000A05CE File Offset: 0x0009E9CE
	public void setButton(bool on)
	{
		this.bStart.GetComponent<ButtonTemplate>().setActive(on);
	}

	// Token: 0x040023F7 RID: 9207
	public GameObject bStart;

	// Token: 0x040023F8 RID: 9208
	public Text timer;

	// Token: 0x040023F9 RID: 9209
	public static float packStartTime;

	// Token: 0x040023FA RID: 9210
	[Space(10f)]
	public float timeToIncreaseMusicPitch = 20f;
}
