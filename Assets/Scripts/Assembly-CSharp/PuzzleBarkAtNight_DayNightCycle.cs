using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003BF RID: 959
public class PuzzleBarkAtNight_DayNightCycle : MonoBehaviour
{
	// Token: 0x060017DD RID: 6109 RVA: 0x0005190B File Offset: 0x0004FD0B
	public void NightTime(bool val)
	{
		PuzzleBarkAtNight_DayNightCycle.nightTime = val;
		base.StartCoroutine(this.NightAlphaCoroutine());
	}

	// Token: 0x060017DE RID: 6110 RVA: 0x00051920 File Offset: 0x0004FD20
	private IEnumerator NightAlphaCoroutine()
	{
		SpriteRenderer rend = this.night.GetComponent<SpriteRenderer>();
		Color initial = rend.color;
		Color target = initial;
		target.a = ((!PuzzleBarkAtNight_DayNightCycle.nightTime) ? 0f : this.nightAlpha);
		float timer = 0f;
		while (timer != this.alphaChangeTime)
		{
			timer = Mathf.MoveTowards(timer, this.alphaChangeTime, Time.deltaTime);
			float t = Mathf.Sin(timer / this.alphaChangeTime * 3.1415927f * 0.5f);
			rend.color = Color.Lerp(initial, target, t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x040015C5 RID: 5573
	[Header("Night values")]
	public static bool nightTime;

	// Token: 0x040015C6 RID: 5574
	public Transform night;

	// Token: 0x040015C7 RID: 5575
	public float nightAlpha;

	// Token: 0x040015C8 RID: 5576
	public float alphaChangeTime;
}
