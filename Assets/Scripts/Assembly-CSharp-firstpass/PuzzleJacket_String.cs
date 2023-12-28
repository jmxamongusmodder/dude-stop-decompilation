using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public class PuzzleJacket_String : EnhancedDraggable
{
	// Token: 0x06001AE7 RID: 6887 RVA: 0x0006C471 File Offset: 0x0006A871
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawHorizontalLine(this.cutoffLine, Color.red, -10f, 10f);
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x0006C48D File Offset: 0x0006A88D
	private void Start()
	{
		this.otherString = (from x in this.GetComponentsInPuzzleStats(false)
		where x != this
		select x).First<PuzzleJacket_String>();
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x0006C4B2 File Offset: 0x0006A8B2
	private void Update()
	{
		this.CheckPosition();
		this.TugStrings();
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x0006C4C0 File Offset: 0x0006A8C0
	private void CheckPosition()
	{
		if (!this.dragged)
		{
			return;
		}
		if (base.transform.position.y > this.thisStringStartingPosition)
		{
			base.transform.SetY(this.thisStringStartingPosition, false);
		}
		this.GetPuzzleStats().goBadAfterTime = (Mathf.Abs(base.transform.position.y - this.otherString.transform.position.y) > this.failDist);
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x0006C550 File Offset: 0x0006A950
	public override void OnMouseDown()
	{
		if (Camera.main.GetMousePosition().y > this.cutoffLine)
		{
			return;
		}
		base.OnMouseDown();
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x0006C584 File Offset: 0x0006A984
	protected override void MouseDowned()
	{
		this.otherStringStartingPosition = this.otherString.transform.position.y;
		this.thisStringStartingPosition = base.transform.position.y;
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x0006C5C8 File Offset: 0x0006A9C8
	protected override void MouseUpped()
	{
		if (base.transform.position.y > this.thisStringStartingPosition)
		{
			base.transform.SetY(this.thisStringStartingPosition, false);
		}
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x0006C608 File Offset: 0x0006AA08
	protected override void LimitReached()
	{
		this.limit.bottomVal = this.limits[this.currentLimit++];
		this.otherString.currentLimit++;
		if (this.currentLimit == this.limits.Length)
		{
			base.GetComponent<BoxCollider2D>().enabled = false;
			base.body.bodyType = RigidbodyType2D.Dynamic;
			this.otherString.GetComponent<BoxCollider2D>().enabled = false;
			this.otherString.body.bodyType = RigidbodyType2D.Dynamic;
			this.otherString.body.gravityScale = -1f;
			Global.self.GetCup(AwardName.STRING);
		}
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x0006C6BC File Offset: 0x0006AABC
	private void TugStrings()
	{
		if (!this.dragged)
		{
			return;
		}
		float num = base.transform.position.y - this.thisStringStartingPosition;
		this.otherString.transform.SetY(this.otherStringStartingPosition - num, false);
		this.thisStringStartingPosition = base.transform.position.y;
		this.otherStringStartingPosition = this.otherString.transform.position.y;
	}

	// Token: 0x04001924 RID: 6436
	public float cutoffLine;

	// Token: 0x04001925 RID: 6437
	public float failDist = 0.1f;

	// Token: 0x04001926 RID: 6438
	public float[] limits;

	// Token: 0x04001927 RID: 6439
	private int currentLimit;

	// Token: 0x04001928 RID: 6440
	private PuzzleJacket_String otherString;

	// Token: 0x04001929 RID: 6441
	private float otherStringStartingPosition;

	// Token: 0x0400192A RID: 6442
	private float thisStringStartingPosition;
}
