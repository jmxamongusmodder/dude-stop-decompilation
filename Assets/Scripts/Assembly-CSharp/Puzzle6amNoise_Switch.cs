using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003BD RID: 957
public class Puzzle6amNoise_Switch : MonoBehaviour
{
	// Token: 0x060017C8 RID: 6088 RVA: 0x000508C0 File Offset: 0x0004ECC0
	private void Awake()
	{
		this.drill = this.GetComponentInPuzzleStats<Puzzle6amNoise_Drill>();
		for (int i = 0; i < this.times.Count; i++)
		{
			this.times[i] = this.times[i] * 60 + UnityEngine.Random.Range(0, 30);
		}
		this.clockText.text = Puzzle6amNoise_Switch.ConvertMinutesToTime(this.times[0]);
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x00050938 File Offset: 0x0004ED38
	private void Update()
	{
		this.dotsChangeTimer = Mathf.MoveTowards(this.dotsChangeTimer, this.dotsChangeTime, Time.deltaTime);
		if (this.dotsChangeTimer == this.dotsChangeTime && this.clockChangeTimer == -1f)
		{
			this.dotsEnabled = !this.dotsEnabled;
			this.dotsChangeTimer = 0f;
			if (this.dotsEnabled)
			{
				this.clockText.text = this.clockText.text.Replace("<color=#00000000>:</color>", ":");
			}
			else
			{
				this.clockText.text = this.clockText.text.Replace(":", "<color=#00000000>:</color>");
			}
		}
		if (this.fadeTimer >= 0f)
		{
			if (this.fadeTimer < this.fadeOutTime)
			{
				this.fadeTimer = Mathf.MoveTowards(this.fadeTimer, this.fadeOutTime, Time.deltaTime);
				float num = this.fadeTimer / this.fadeOutTime;
				num = Mathf.Sin(num * 3.1415927f * 0.5f);
				Color color = new Color(0f, 0f, 0f, num * this.maxAlpha);
				this.shadow.GetComponent<SpriteRenderer>().color = color;
			}
			else if (this.clockChangeTimer < this.clockChangeTime)
			{
				this.clockChangeTimer = Mathf.MoveTowards(this.clockChangeTimer, this.clockChangeTime, Time.deltaTime);
				float num2 = this.clockChangeTimer / this.clockChangeTime;
				num2 = Mathf.Sin(num2 * 3.1415927f * 0.5f);
				int minutes = (int)Mathf.Lerp((float)this.times[this.timeInd], (float)this.times[this.timeInd + 1], num2);
				this.clockText.text = Puzzle6amNoise_Switch.ConvertMinutesToTime(minutes);
			}
			else if (this.fadeTimer < this.fadeInTime + this.fadeOutTime)
			{
				this.fadeTimer = Mathf.MoveTowards(this.fadeTimer, this.fadeOutTime + this.fadeInTime, Time.deltaTime);
				float num3 = 1f - (this.fadeTimer - this.fadeOutTime) / this.fadeInTime;
				num3 = Mathf.Sin(num3 * 3.1415927f * 0.5f);
				Color color2 = new Color(0f, 0f, 0f, num3 * this.maxAlpha);
				this.shadow.GetComponent<SpriteRenderer>().color = color2;
			}
			else
			{
				this.clockChangeTimer = -1f;
				this.fadeTimer = -1f;
				this.timeInd++;
				this.drill.enabled = true;
				if (!this.evilTimeMet && this.timeInd == 3)
				{
					this.evilTimeMet = true;
					this.drill.evilTime = true;
				}
				else
				{
					this.drill.evilTime = false;
				}
			}
		}
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x00050C28 File Offset: 0x0004F028
	private void OnMouseDrag()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.clockChangeTimer >= 0f || this.timeInd + 1 == this.times.Count)
		{
			return;
		}
		if (this.hover)
		{
			base.transform.GetChild(1).gameObject.SetActive(true);
		}
		else
		{
			base.transform.GetChild(1).gameObject.SetActive(false);
		}
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x00050CA8 File Offset: 0x0004F0A8
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!this.hover)
		{
			return;
		}
		if (this.clockChangeTimer >= 0f)
		{
			return;
		}
		if (this.timeInd + 1 == this.times.Count)
		{
			return;
		}
		Audio.self.playOneShot("3ba57002-ede6-42ba-9fdc-f2b784d8149c", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_6AmNoise>().onSwitchPressed();
		this.clockChangeTimer = 0f;
		this.fadeTimer = 0f;
		this.drill.enabled = false;
		this.drill.GetComponent<Rigidbody2D>().velocity = Vector2.up * this.drill.GetComponent<Rigidbody2D>().velocity.y;
		base.transform.GetChild(1).gameObject.SetActive(false);
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x00050D8B File Offset: 0x0004F18B
	private void OnMouseExit()
	{
		this.hover = false;
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x00050D94 File Offset: 0x0004F194
	private void OnMouseEnter()
	{
		this.hover = true;
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x00050DA0 File Offset: 0x0004F1A0
	public static string ConvertMinutesToTime(int minutes)
	{
		bool flag = true;
		if (!Global.self.metricSystem && minutes > 720)
		{
			minutes %= 720;
			flag = false;
		}
		int num = minutes / 60;
		if (num == 0)
		{
			num = 12;
		}
		minutes %= 60;
		string text = (num >= 10) ? num.ToString() : ("0" + num.ToString());
		text += ":";
		text += ((minutes >= 10) ? minutes.ToString() : ("0" + minutes.ToString()));
		if (!Global.self.metricSystem)
		{
			text += ((!flag) ? "pm" : "am");
		}
		return text;
	}

	// Token: 0x04001597 RID: 5527
	private const int MINUTES_IN_TWELVE_HOURS = 720;

	// Token: 0x04001598 RID: 5528
	public Text clockText;

	// Token: 0x04001599 RID: 5529
	public List<int> times = new List<int>();

	// Token: 0x0400159A RID: 5530
	private int timeInd;

	// Token: 0x0400159B RID: 5531
	[Tooltip("How many seconds it takes for time to change")]
	public float clockChangeTime = 1f;

	// Token: 0x0400159C RID: 5532
	private float clockChangeTimer = -1f;

	// Token: 0x0400159D RID: 5533
	public Transform shadow;

	// Token: 0x0400159E RID: 5534
	[Range(0f, 1f)]
	public float maxAlpha = 0.96f;

	// Token: 0x0400159F RID: 5535
	public float fadeOutTime = 0.3f;

	// Token: 0x040015A0 RID: 5536
	public float fadeInTime = 0.7f;

	// Token: 0x040015A1 RID: 5537
	private bool hover;

	// Token: 0x040015A2 RID: 5538
	private Puzzle6amNoise_Drill drill;

	// Token: 0x040015A3 RID: 5539
	[Tooltip("How many seconds it takes for dots to turn on/off")]
	public float dotsChangeTime = 1f;

	// Token: 0x040015A4 RID: 5540
	private float dotsChangeTimer;

	// Token: 0x040015A5 RID: 5541
	private bool dotsEnabled = true;

	// Token: 0x040015A6 RID: 5542
	private float fadeTimer = -1f;

	// Token: 0x040015A7 RID: 5543
	private bool evilTimeMet;
}
