using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class AwadJigSawBlink : MonoBehaviour
{
	// Token: 0x0600140A RID: 5130 RVA: 0x00032A10 File Offset: 0x00030E10
	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(false);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x00032A94 File Offset: 0x00030E94
	private void Update()
	{
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			if (this.timer <= 0f)
			{
				this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
				this.currSprite = base.transform.GetChild(UnityEngine.Random.Range(0, base.transform.childCount)).GetComponent<SpriteRenderer>();
				this.currSprite.gameObject.SetActive(true);
				this.animLenCurr = this.animLen;
			}
		}
		if (this.animLenCurr >= 0f)
		{
			this.animLenCurr -= Time.deltaTime;
			if (this.animLenCurr <= this.animLen && this.animLenCurr > this.animLen * 0.5f)
			{
				Color color = this.currSprite.color;
				color.a = Mathf.Abs(this.animLenCurr - this.animLen) / (this.animLen * 0.5f) * this.alphaAmount;
				this.currSprite.color = color;
			}
			if (this.animLenCurr < this.animLen * 0.5f)
			{
				Color color2 = this.currSprite.color;
				color2.a = this.animLenCurr / (this.animLen * 0.5f) * this.alphaAmount;
				this.currSprite.color = color2;
			}
			if (this.animLenCurr < 0f)
			{
				this.currSprite.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0400110A RID: 4362
	private float timer;

	// Token: 0x0400110B RID: 4363
	[Tooltip("How often to show blink")]
	public float timerMin = 5f;

	// Token: 0x0400110C RID: 4364
	[Tooltip("How often to show blink")]
	public float timerMax = 15f;

	// Token: 0x0400110D RID: 4365
	[Tooltip("How long animation plays")]
	public float animLen = 2f;

	// Token: 0x0400110E RID: 4366
	private float animLenCurr = -1f;

	// Token: 0x0400110F RID: 4367
	[Tooltip("Amount of alpha to show")]
	public float alphaAmount = 0.6f;

	// Token: 0x04001110 RID: 4368
	private SpriteRenderer currSprite;
}
