using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000590 RID: 1424
public class UITimer : MonoBehaviour
{
	// Token: 0x060020F2 RID: 8434 RVA: 0x000A29A3 File Offset: 0x000A0DA3
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060020F3 RID: 8435 RVA: 0x000A29BD File Offset: 0x000A0DBD
	public void StartTimer(float maxTime = -1f)
	{
		if (!UIControl.self.timerEnabled)
		{
			return;
		}
		base.gameObject.SetActive(true);
		this.isActive = true;
		this.time = DateTime.Now;
		this.maxTime = maxTime;
	}

	// Token: 0x060020F4 RID: 8436 RVA: 0x000A29F4 File Offset: 0x000A0DF4
	public void StopTimer()
	{
		if (!this.isActive)
		{
			return;
		}
		this.isActive = false;
		base.StartCoroutine(this.HideTimer());
	}

	// Token: 0x060020F5 RID: 8437 RVA: 0x000A2A18 File Offset: 0x000A0E18
	public IEnumerator HideTimer()
	{
		yield return new WaitForSeconds(2f);
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060020F6 RID: 8438 RVA: 0x000A2A34 File Offset: 0x000A0E34
	private void Update()
	{
		if (!this.isActive)
		{
			return;
		}
		TimeSpan timeSpan = DateTime.Now - this.time;
		this.text.text = timeSpan.Seconds + "." + timeSpan.Milliseconds.ToString("000");
		if (this.maxTime != -1f && timeSpan.TotalSeconds > (double)this.maxTime)
		{
			this.StopTimer();
		}
	}

	// Token: 0x0400244A RID: 9290
	private Text text;

	// Token: 0x0400244B RID: 9291
	private bool isActive;

	// Token: 0x0400244C RID: 9292
	private DateTime time;

	// Token: 0x0400244D RID: 9293
	private float maxTime = -1f;
}
