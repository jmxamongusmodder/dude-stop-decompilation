using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200043B RID: 1083
public class PuzzlePhoneCharge : Draggable
{
	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000727A0 File Offset: 0x00070BA0
	// (set) Token: 0x06001B9F RID: 7071 RVA: 0x000727A8 File Offset: 0x00070BA8
	private bool charging
	{
		get
		{
			return this._charging;
		}
		set
		{
			if (this._charging != value)
			{
				if (value)
				{
					Audio.self.playOneShot("3d13b564-a4be-4676-a4e5-84b0c8abdcc6", 1f);
					Global.self.currPuzzle.GetComponent<AudioVoice_PhoneCharge>().chargerIn();
				}
				else
				{
					Audio.self.playOneShot("439ab4c0-9100-4e76-9bd9-ac29ea7ba723", 1f);
					Global.self.currPuzzle.GetComponent<AudioVoice_PhoneCharge>().chargerOut();
				}
			}
			this._charging = value;
		}
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x00072828 File Offset: 0x00070C28
	private void Awake()
	{
		if (Global.self.CountPackPlayedTimes(0) > 0)
		{
			this.percent = this.percentOnSecondRun;
		}
		this.phonePercent.GetComponent<Text>().text = string.Concat(new object[]
		{
			this.percent / 10,
			".",
			this.percent % 10,
			"%"
		});
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x0007289F File Offset: 0x00070C9F
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		GizmosExtension.DrawHorizontalLine(this.snapPointY, -10f, 10f);
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x000728C0 File Offset: 0x00070CC0
	private void Start()
	{
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.Y, this.snapPointY, this.snapDistance), false);
		this.startingPercent = this.percent;
		this.phoneChargeMin = this.phoneCharge.localScale.x;
		base.StartCoroutine(this.InsertionCoroutine());
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x00072918 File Offset: 0x00070D18
	private void Update()
	{
		if (this.charging)
		{
			if (this.percent < 1000)
			{
				this.permilleChangeTimer = Mathf.MoveTowards(this.permilleChangeTimer, this.permilleChangeTime, Time.deltaTime);
				if (this.permilleChangeTimer == this.permilleChangeTime)
				{
					this.percent++;
					this.phonePercent.GetComponent<Text>().text = string.Concat(new object[]
					{
						this.percent / 10,
						".",
						this.percent % 10,
						"%"
					});
					this.permilleChangeTimer = 0f;
					float x = this.phoneChargeMin + (float)(this.percent - this.startingPercent) / (1000f - (float)this.startingPercent) * (this.phoneChargeMax - this.phoneChargeMin);
					this.phoneCharge.localScale = new Vector3(x, 1f);
				}
			}
			else
			{
				Audio.self.playOneShot("b74590fe-1749-48b6-bacb-bee536da4b74", 1f);
				Global.LevelFailed(0f, true);
			}
			if (this.noChargeTimer > this.noChargeWait)
			{
				this.noChargeTimer = Mathf.MoveTowards(this.noChargeTimer, this.noChargeWait, Time.deltaTime);
				this.UpdateColor();
			}
			else
			{
				this.noChargeTimer = 0f;
			}
		}
		else
		{
			this.noChargeTimer = Mathf.MoveTowards(this.noChargeTimer, this.noChargeWait + this.noChargeChangeTime, Time.deltaTime);
			this.UpdateColor();
		}
		this.charging = (base.transform.position.y >= this.snapPointY);
		if (this.dragged && base.transform.position.y > this.snapPointY)
		{
			base.transform.position = new Vector3(base.transform.position.x, this.snapPointY);
		}
		if (!this.dragged && !this.charging && this.percent == 999)
		{
			Global.LevelCompleted(0f, true);
		}
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x00072B58 File Offset: 0x00070F58
	private void UpdateColor()
	{
		if (this.noChargeTimer < this.noChargeWait)
		{
			return;
		}
		float t = (this.noChargeTimer - this.noChargeWait) / this.noChargeChangeTime;
		Color color = Color.Lerp(this.chargingColor, this.noChargeColor, t);
		foreach (SpriteRenderer spriteRenderer in this.phoneCharge.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.color = color;
		}
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x00072BD0 File Offset: 0x00070FD0
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (base.transform.position.y > this.snapPointY)
		{
			base.transform.position = new Vector3(base.transform.position.x, this.snapPointY);
		}
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x00072C2C File Offset: 0x0007102C
	private IEnumerator InsertionCoroutine()
	{
		this._charging = false;
		this.dragEnabled = false;
		float startY = base.transform.localPosition.y;
		float animTime = this.startAnimation.GetAnimationLength();
		for (float timer = 0f; timer < animTime; timer = Mathf.MoveTowards(timer, animTime, Time.deltaTime))
		{
			base.transform.SetY(startY + this.startAnimation.Evaluate(timer), true);
			yield return null;
		}
		base.transform.SetY(startY + this.startAnimation.Evaluate(animTime), true);
		this.noChargeTimer = this.noChargeWait + this.noChargeChangeTime;
		this.charging = true;
		this.dragEnabled = true;
		yield break;
	}

	// Token: 0x040019E1 RID: 6625
	[Header("Cable")]
	public AnimationCurve startAnimation;

	// Token: 0x040019E2 RID: 6626
	public Transform phonePercent;

	// Token: 0x040019E3 RID: 6627
	public int percent = 985;

	// Token: 0x040019E4 RID: 6628
	public int percentOnSecondRun = 995;

	// Token: 0x040019E5 RID: 6629
	public float permilleChangeTime = 1f;

	// Token: 0x040019E6 RID: 6630
	public Transform phoneCharge;

	// Token: 0x040019E7 RID: 6631
	public float phoneChargeMax = 1.29f;

	// Token: 0x040019E8 RID: 6632
	public Color chargingColor;

	// Token: 0x040019E9 RID: 6633
	public Color noChargeColor;

	// Token: 0x040019EA RID: 6634
	public float noChargeWait;

	// Token: 0x040019EB RID: 6635
	public float noChargeChangeTime;

	// Token: 0x040019EC RID: 6636
	public float snapPointY;

	// Token: 0x040019ED RID: 6637
	public float snapDistance;

	// Token: 0x040019EE RID: 6638
	private float phoneChargeMin;

	// Token: 0x040019EF RID: 6639
	private int startingPercent;

	// Token: 0x040019F0 RID: 6640
	private float permilleChangeTimer;

	// Token: 0x040019F1 RID: 6641
	private float noChargeTimer;

	// Token: 0x040019F2 RID: 6642
	private bool _charging;
}
