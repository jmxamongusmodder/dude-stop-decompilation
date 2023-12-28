using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200046F RID: 1135
public class PuzzleWrongDress_Item : EnhancedDraggable
{
	// Token: 0x06001D22 RID: 7458 RVA: 0x0007FA2C File Offset: 0x0007DE2C
	private void Start()
	{
		this.snap = new SnapPoint(Draggable.Snap.XY, this.bodySnapPoint.position, this.snapDist);
		this.sprite = base.GetComponent<SpriteRenderer>();
		base.AddSnapPoint(this.snap, false);
		this.startPosition = base.transform.localPosition;
		this.allItems = this.GetComponentsInPuzzleStats(false).ToList<PuzzleWrongDress_Item>();
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x0007FA9C File Offset: 0x0007DE9C
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (this.dragged)
		{
			this.InitSpriteOrder();
			if (base.Snapped())
			{
				this.delayedMoveToFront = true;
			}
			else
			{
				this.MoveToFront();
			}
		}
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x0007FAD4 File Offset: 0x0007DED4
	public override void OnMouseUp()
	{
		this.CheckVictoryConditions();
		if (this.dragged)
		{
			this.delayedMoveToFront = false;
			base.transform.SetLocalZ((float)this.sprite.sortingOrder * -0.1f);
			base.body.velocity = Vector2.zero;
		}
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
		int num = 0;
		foreach (PuzzleWrongDress_Item puzzleWrongDress_Item in this.allItems)
		{
			if (Mathf.Abs(puzzleWrongDress_Item.transform.position.x) < 1.3f && Mathf.Abs(puzzleWrongDress_Item.transform.position.y) < 2.5f && ++num > 6)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_WrongDress>().alotOnHuman();
				break;
			}
		}
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x0007FBF0 File Offset: 0x0007DFF0
	private void InitSpriteOrder()
	{
		if (this.sprite.sortingOrder != 0)
		{
			return;
		}
		int sortingOrder = 1;
		foreach (PuzzleWrongDress_Item puzzleWrongDress_Item in this.allItems)
		{
			puzzleWrongDress_Item.sprite.sortingOrder = sortingOrder;
		}
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x0007FC64 File Offset: 0x0007E064
	private void MoveToFront()
	{
		foreach (PuzzleWrongDress_Item puzzleWrongDress_Item in from x in this.allItems
		where x != this && x.sprite.sortingOrder > this.sprite.sortingOrder
		select x)
		{
			puzzleWrongDress_Item.sprite.sortingOrder--;
			puzzleWrongDress_Item.transform.SetLocalZ(puzzleWrongDress_Item.transform.localPosition.z + 0.1f);
		}
		this.sprite.sortingOrder = 8;
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x0007FD0C File Offset: 0x0007E10C
	protected void CheckVictoryConditions()
	{
		if (!base.enabled || !this.dragged || !base.Snapped())
		{
			return;
		}
		List<PuzzleWrongDress_Item> list = (from x in this.allItems
		where x.Snapped()
		select x).ToList<PuzzleWrongDress_Item>();
		if (list.Count >= 2)
		{
			if (list.Find((PuzzleWrongDress_Item x) => x.tag == "SuccessCollider") != null)
			{
				if (list.Find((PuzzleWrongDress_Item x) => x.tag == "FailCollider") != null)
				{
					Global.self.currPuzzle.GetComponent<AudioVoice_WrongDress>().onWrongSnap();
				}
			}
		}
		if (list.Count != 4)
		{
			return;
		}
		foreach (PuzzleWrongDress_Item puzzleWrongDress_Item in from x in this.allItems
		where !x.Snapped()
		select x)
		{
			base.StartCoroutine(puzzleWrongDress_Item.ReturningCoroutine());
		}
		if (list.Find((PuzzleWrongDress_Item x) => x.tag == "SuccessCollider") != null)
		{
			if (list.Find((PuzzleWrongDress_Item x) => x.tag == "FailCollider") != null)
			{
				Global.LevelCompleted(0f, true);
				return;
			}
		}
		Global.LevelFailed(0f, true);
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x0007FEE0 File Offset: 0x0007E2E0
	protected override void OnSnap(SnapPoint point)
	{
		this.otherItem.snap.enabled = false;
		this.checkMark.shown = true;
		Audio.self.playOneShot("0d988fa3-db33-45f9-a236-007a64ca1c0c", 1f);
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x0007FF14 File Offset: 0x0007E314
	protected override void OnUnsnap(SnapPoint point)
	{
		this.otherItem.snap.enabled = true;
		this.checkMark.shown = false;
		if (this.delayedMoveToFront)
		{
			this.MoveToFront();
		}
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x0007FF44 File Offset: 0x0007E344
	public bool IsSnapped()
	{
		return base.Snapped();
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x0007FF4C File Offset: 0x0007E34C
	private IEnumerator ReturningCoroutine()
	{
		float timer = 0f;
		Vector2 start = base.transform.localPosition;
		while (timer < this.returnTime)
		{
			timer = Mathf.MoveTowards(timer, this.returnTime, Time.deltaTime);
			float t = Mathf.Sin(timer / this.returnTime * 3.1415927f * 0.5f);
			base.transform.localPosition = Vector2.Lerp(start, this.startPosition, t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001BCB RID: 7115
	public float returnTime = 0.5f;

	// Token: 0x04001BCC RID: 7116
	public float snapDist = 0.3f;

	// Token: 0x04001BCD RID: 7117
	public Transform bodySnapPoint;

	// Token: 0x04001BCE RID: 7118
	public PuzzleWrongDress_Item otherItem;

	// Token: 0x04001BCF RID: 7119
	public PuzzleWrongDress_Checkmark checkMark;

	// Token: 0x04001BD0 RID: 7120
	private SpriteRenderer sprite;

	// Token: 0x04001BD1 RID: 7121
	private List<PuzzleWrongDress_Item> allItems;

	// Token: 0x04001BD2 RID: 7122
	private Vector2 startPosition;

	// Token: 0x04001BD3 RID: 7123
	private SnapPoint snap;

	// Token: 0x04001BD4 RID: 7124
	private bool delayedMoveToFront;
}
