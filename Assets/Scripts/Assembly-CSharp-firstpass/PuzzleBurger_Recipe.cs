using System;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class PuzzleBurger_Recipe : EnhancedDraggable
{
	// Token: 0x0600188F RID: 6287 RVA: 0x00056BDC File Offset: 0x00054FDC
	private void Start()
	{
		this.topPosition = base.transform.localPosition;
		this.topPosition.y = this.limit.topVal;
		this.bottomPosition = base.transform.localPosition;
		this.bottomPosition.y = this.limit.bottomVal;
		this.currentReturnTime = this.CalculateReturnTime();
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x00056C44 File Offset: 0x00055044
	private void Update()
	{
		if (this.dragged)
		{
			return;
		}
		if (this.currentReturnTime > 0f)
		{
			this.currentReturnTime = Mathf.MoveTowards(this.currentReturnTime, 0f, Time.deltaTime);
			float num = this.currentReturnTime / this.returnTime;
			num = 1f - Mathf.Cos(num * 3.1415927f * 0.5f);
			base.transform.localPosition = Vector3.Lerp(this.bottomPosition, this.topPosition, num);
		}
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x00056CCC File Offset: 0x000550CC
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (this.dragged)
		{
			Audio.self.playOneShot("13e96f05-bb54-40e7-babf-75168e9dd9cc", 1f);
		}
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x00056CF4 File Offset: 0x000550F4
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		this.currentReturnTime = this.CalculateReturnTime();
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x00056D14 File Offset: 0x00055114
	private float CalculateReturnTime()
	{
		float num = this.limit.topVal - this.limit.bottomVal;
		float num2 = base.transform.localPosition.y - this.limit.bottomVal;
		float num3 = Mathf.Acos(1f - num2 / num);
		num3 /= 1.5707964f;
		return this.returnTime * num3;
	}

	// Token: 0x04001686 RID: 5766
	public float returnTime = 0.5f;

	// Token: 0x04001687 RID: 5767
	private float currentReturnTime;

	// Token: 0x04001688 RID: 5768
	private Vector3 bottomPosition;

	// Token: 0x04001689 RID: 5769
	private Vector3 topPosition;
}
