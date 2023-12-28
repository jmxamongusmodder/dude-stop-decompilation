using System;
using UnityEngine;

// Token: 0x020003D4 RID: 980
public class PuzzleBurger_Ingredient : EnhancedDraggable
{
	// Token: 0x0600188A RID: 6282 RVA: 0x00056A04 File Offset: 0x00054E04
	private void Update()
	{
		this.RecordDistance();
		if (this.dragged && Mathf.Abs(base.transform.position.x - this.bun.position.x) < this.snapDistance)
		{
			base.transform.position = new Vector3(this.bun.position.x, base.transform.position.y);
			base.body.velocity = Vector3.zero;
			this.snapped = true;
		}
		else
		{
			this.snapped = false;
		}
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x00056AB8 File Offset: 0x00054EB8
	protected override void MouseUpped()
	{
		if (this.snapped)
		{
			base.transform.position = new Vector3(this.bun.position.x, base.transform.position.y);
			base.body.velocity = Vector3.zero;
		}
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x00056B1B File Offset: 0x00054F1B
	private void OnDisable()
	{
		if (base.body != null)
		{
			base.body.velocity = Vector3.zero;
		}
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x00056B44 File Offset: 0x00054F44
	private void RecordDistance()
	{
		if (!this.recordingStarted)
		{
			this.recordingStarted = true;
			this.lastRecordedPosition = base.transform.localPosition;
			this.bunScript = this.bun.GetComponent<PuzzleBurger_Bun>();
			return;
		}
		this.bunScript.RecordDistance(Vector2.Distance(base.transform.localPosition, this.lastRecordedPosition));
		this.lastRecordedPosition = base.transform.localPosition;
	}

	// Token: 0x04001680 RID: 5760
	public Transform bun;

	// Token: 0x04001681 RID: 5761
	public float snapDistance = 0.5f;

	// Token: 0x04001682 RID: 5762
	private bool snapped;

	// Token: 0x04001683 RID: 5763
	private PuzzleBurger_Bun bunScript;

	// Token: 0x04001684 RID: 5764
	private Vector2 lastRecordedPosition;

	// Token: 0x04001685 RID: 5765
	private bool recordingStarted;
}
