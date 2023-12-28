using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public class PuzzleFriendsPen_BlackPen : DrawingPen
{
	// Token: 0x06001A41 RID: 6721 RVA: 0x0006612C File Offset: 0x0006452C
	private void Start()
	{
		this.originalAngle = base.transform.rotation.eulerAngles.z;
		this.originalPuzzle = this.GetPuzzleStats().transform;
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x0006616C File Offset: 0x0006456C
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped())
		{
			Global.setCompletionState(CompletionState.Good, this.originalPuzzle);
			Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().givePenBack();
		}
		else if (base.snapEnabled && Vector2.Distance(base.transform.position, this.hand) < this.pullRadius)
		{
			base.StartCoroutine(this.PullingCoroutine());
			Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().givePenBack();
		}
		else
		{
			Global.setCompletionState(CompletionState.None, this.originalPuzzle);
			Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().takePen();
		}
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x00066234 File Offset: 0x00064634
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (!this.handMoved)
		{
			this.GetComponentInPuzzleStats<PuzzleFriendsPen_Hand>().MoveIn();
			this.handMoved = true;
		}
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x00066259 File Offset: 0x00064659
	public override void EnterInventory()
	{
		base.EnterInventory();
		Global.setCompletionState(CompletionState.None, this.originalPuzzle);
		Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().takePen();
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x00066282 File Offset: 0x00064682
	protected override void ChangeLooks()
	{
		base.transform.localScale = new Vector3(this.scale, this.scale);
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x000662A0 File Offset: 0x000646A0
	protected override void CheckRotation()
	{
		if (this.state == DrawingPen.State.ReleasedIdle)
		{
			return;
		}
		float num = (!this.dragged) ? this.defaultAngle : this.rotatedAngle;
		float num2 = Vector2.Distance(base.transform.position, this.hand);
		if (base.snapEnabled && num2 < this.rotationDistance)
		{
			num = Mathf.LerpAngle(num, this.originalAngle, 1f - num2 / this.rotationDistance);
		}
		float num3 = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, num, this.rotationSpeed * Time.deltaTime);
		base.body.MoveRotation(num3);
		float num4 = 0.1f;
		if (Mathf.Abs(Mathf.DeltaAngle(num3, num)) < num4)
		{
			this.state = ((!this.dragged) ? DrawingPen.State.ReleasedIdle : DrawingPen.State.DraggedIdle);
		}
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x0006638C File Offset: 0x0006478C
	private IEnumerator PullingCoroutine()
	{
		Global.self.scrollableUI.GetComponent<scrollablePackArrows>().pauseScrolling(0.3f);
		Global.self.canBePaused = false;
		while (base.transform.position != this.hand)
		{
			base.transform.position = Vector2.Lerp(base.transform.position, this.hand, Time.deltaTime * this.pullSpeed);
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.hand, Time.deltaTime * this.pullSpeed);
			this.state = DrawingPen.State.Released;
			yield return null;
		}
		Global.setCompletionState(CompletionState.Good, this.originalPuzzle);
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x04001842 RID: 6210
	[Header("Hand stuff")]
	[HideInInspector]
	public Vector2 hand;

	// Token: 0x04001843 RID: 6211
	public float rotationDistance;

	// Token: 0x04001844 RID: 6212
	public float pullRadius;

	// Token: 0x04001845 RID: 6213
	public float pullSpeed;

	// Token: 0x04001846 RID: 6214
	public float snapDistance = 0.3f;

	// Token: 0x04001847 RID: 6215
	[Header("Other stuff")]
	public float scale;

	// Token: 0x04001848 RID: 6216
	public float rotationTime = 0.5f;

	// Token: 0x04001849 RID: 6217
	[Range(0f, 1f)]
	public float requiredFill;

	// Token: 0x0400184A RID: 6218
	[HideInInspector]
	public Transform originalPuzzle;

	// Token: 0x0400184B RID: 6219
	private float originalAngle;

	// Token: 0x0400184C RID: 6220
	private bool handMoved;
}
