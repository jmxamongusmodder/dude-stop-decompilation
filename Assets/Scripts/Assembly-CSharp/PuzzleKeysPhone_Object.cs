using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000426 RID: 1062
[Serializable]
public class PuzzleKeysPhone_Object : InventoryDraggable
{
	// Token: 0x06001B00 RID: 6912 RVA: 0x0006D988 File Offset: 0x0006BD88
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawHorizontalLine(this.pocketY, Color.red, -10f, 10f);
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(new Vector3(this.leftSnapX, 6f), new Vector3(this.leftSnapX, -6f));
		Gizmos.DrawLine(new Vector3(this.rightSnapX, 6f), new Vector3(this.rightSnapX, -6f));
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(new Vector3(6f, this.minSnapY), new Vector3(-6f, this.minSnapY));
		Gizmos.DrawLine(new Vector3(6f, this.minMoveY), new Vector3(-6f, this.minMoveY));
		Gizmos.color = Color.green;
		Gizmos.DrawLine(new Vector3(6f, this.successY), new Vector3(-6f, this.successY));
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x0006DA88 File Offset: 0x0006BE88
	private void Start()
	{
		base.SetSnapPoints(new List<SnapPoint>
		{
			new SnapPoint(Draggable.Snap.X, this.leftSnapX, this.snapDistance),
			new SnapPoint(Draggable.Snap.X, this.rightSnapX, this.snapDistance),
			new SnapPoint(Draggable.Snap.XY, new Vector2(this.leftSnapX, this.pocketY), this.snapDistance),
			new SnapPoint(Draggable.Snap.XY, new Vector2(this.rightSnapX, this.pocketY), this.snapDistance)
		});
		Renderer component = base.GetComponent<Renderer>();
		if (component == null)
		{
			Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
			foreach (Renderer item in componentsInChildren)
			{
				this.rend.Add(item);
			}
		}
		else
		{
			this.rend.Add(component);
		}
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x0006DB71 File Offset: 0x0006BF71
	private void Update()
	{
		this.ProcessSnapPoints();
		this.CheckInventorySlots();
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x0006DB7F File Offset: 0x0006BF7F
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		this.FindOtherObject();
	}

	// Token: 0x06001B04 RID: 6916 RVA: 0x0006DB90 File Offset: 0x0006BF90
	public override void OnMouseUp()
	{
		if (!base.enabled || !this.dragEnabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped() && base.snapPoint.type == Draggable.Snap.XY)
		{
			base.StartCoroutine(this.InsertionCoroutine());
			return;
		}
		if (this.lockSnapPoint && base.transform.position.y <= this.successY)
		{
			this.bottom = true;
		}
		else
		{
			this.bottom = false;
		}
		if (this.lockSnapPoint && base.transform.position.y < this.minMoveY)
		{
			base.transform.position = new Vector3(base.transform.position.x, this.minMoveY);
		}
		this.inventoryOn = false;
		this.CheckStatus(this.GetPuzzleStats().transform);
	}

	// Token: 0x06001B05 RID: 6917 RVA: 0x0006DC8C File Offset: 0x0006C08C
	protected override void InventoryItemMouseDown()
	{
		if (!this.lockSnapPoint || base.transform.position.y > this.successY)
		{
			base.InventoryItemMouseDown();
		}
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x0006DCC8 File Offset: 0x0006C0C8
	private void CheckInventorySlots()
	{
		if (!this.dragged)
		{
			return;
		}
		if (!this.inventoryOn && !this.lockSnapPoint)
		{
			InventoryControl.self.mouseOnItemDown(base.transform);
			this.inventoryOn = true;
		}
		else if (this.inventoryOn && this.lockSnapPoint && base.transform.position.y <= this.successY)
		{
			InventoryControl.self.mouseOnItemDown(null);
			this.inventoryOn = false;
		}
		else if (!this.inventoryOn && this.lockSnapPoint && base.transform.position.y > this.successY)
		{
			InventoryControl.self.mouseOnItemDown(base.transform);
			this.inventoryOn = true;
		}
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x0006DDAC File Offset: 0x0006C1AC
	private void FindOtherObject()
	{
		PuzzleKeysPhone_Object[] componentsInChildren = base.transform.parent.GetComponentsInChildren<PuzzleKeysPhone_Object>();
		if (componentsInChildren.Length == 1)
		{
			this.otherObject = null;
		}
		else
		{
			this.otherObject = (from x in componentsInChildren
			where x != this
			select x).First<PuzzleKeysPhone_Object>();
		}
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x0006DDFC File Offset: 0x0006C1FC
	private void CheckStatus(Transform puzzle)
	{
		if (this.bottom && this.otherObject != null && this.otherObject.bottom && !this.otherObject.dragged)
		{
			if (this.otherObject.pocket == this.pocket)
			{
				Global.setCompletionState(CompletionState.Monster, puzzle);
				Global.self.currPuzzle.GetComponent<AudioVoice_KeysAndPhone>().phoneWithKeys();
			}
			else
			{
				Global.setCompletionState(CompletionState.Good, puzzle);
				Global.self.currPuzzle.GetComponent<AudioVoice_KeysAndPhone>().solveGood();
			}
		}
		else
		{
			Global.setCompletionState(CompletionState.None, puzzle);
			Global.self.currPuzzle.GetComponent<AudioVoice_KeysAndPhone>().takeOutAfterBad();
		}
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x0006DEBC File Offset: 0x0006C2BC
	private void ProcessSnapPoints()
	{
		if (!this.dragged)
		{
			return;
		}
		if (base.Snapped() && base.GetSnapPoint().type == Draggable.Snap.X)
		{
			if (base.transform.position.y < this.minSnapY)
			{
				this.lockSnapPoint = true;
			}
			else
			{
				this.lockSnapPoint = false;
			}
			if (base.transform.position.y < this.minMoveY)
			{
				base.transform.SetY(this.minMoveY, false);
			}
		}
		else if (!base.Snapped())
		{
			if (base.transform.position.y < this.minSnapY)
			{
				this.EnableVerticalSnap(false);
			}
			else
			{
				this.EnableVerticalSnap(true);
			}
		}
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x0006DF94 File Offset: 0x0006C394
	public override void DroppedInInventory()
	{
		if (this.smallSprite != null)
		{
			this.smallSprite.gameObject.SetActive(false);
			base.GetComponent<BoxCollider2D>().enabled = false;
		}
		this.bottom = false;
		this.lockSnapPoint = false;
		this.ReturnToNormal();
		this.SetLayer("Front");
		base.snapPoint = null;
		Global.setCompletionState(CompletionState.None, null);
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x0006E000 File Offset: 0x0006C400
	protected override void ChangeLooks()
	{
		if (this.bigSprites != null && this.smallSprite != null)
		{
			this.bigSprites.gameObject.SetActive(false);
			this.smallSprite.gameObject.SetActive(true);
			base.GetComponent<BoxCollider2D>().enabled = true;
		}
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x0006E060 File Offset: 0x0006C460
	private void SetLayer(string name)
	{
		foreach (Renderer renderer in this.rend)
		{
			renderer.sortingLayerName = name;
		}
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x0006E0BC File Offset: 0x0006C4BC
	private void EnableVerticalSnap(bool on)
	{
		(from x in this.snapPoints
		where x.type == Draggable.Snap.X
		select x).ToList<SnapPoint>().ForEach(delegate(SnapPoint x)
		{
			x.enabled = on;
		});
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x0006E114 File Offset: 0x0006C514
	private void ChangePockets(SnapPoint newPocket)
	{
		(from x in this.snapPoints
		where x.type == Draggable.Snap.XY
		select x).ToList<SnapPoint>().ForEach(delegate(SnapPoint x)
		{
			x.enabled = false;
		});
		if (Mathf.Sign(newPocket.coord) < 0f)
		{
			this.pocket = PuzzleKeysPhone_Object.Pocket.Left;
		}
		else
		{
			this.pocket = PuzzleKeysPhone_Object.Pocket.Right;
		}
		this.SetLayer("Background");
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x0006E1A3 File Offset: 0x0006C5A3
	protected override void OnSnap(SnapPoint point)
	{
		if (point.type != Draggable.Snap.XY)
		{
			this.ChangePockets(point);
		}
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x0006E1BD File Offset: 0x0006C5BD
	protected override void OnSnapChange(SnapPoint oldPoint, SnapPoint newPoint)
	{
		if (newPoint.type == Draggable.Snap.X)
		{
			this.ChangePockets(newPoint);
		}
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x0006E1D2 File Offset: 0x0006C5D2
	protected override void OnUnsnap(SnapPoint point)
	{
		if (point.type != Draggable.Snap.XY)
		{
			this.ReturnToNormal();
		}
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x0006E1EC File Offset: 0x0006C5EC
	private void ReturnToNormal()
	{
		(from x in this.snapPoints
		where x.type == Draggable.Snap.XY
		select x).ToList<SnapPoint>().ForEach(delegate(SnapPoint x)
		{
			x.enabled = true;
		});
		this.pocket = PuzzleKeysPhone_Object.Pocket.None;
		this.SetLayer("Top");
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x0006E25C File Offset: 0x0006C65C
	private IEnumerator InsertionCoroutine()
	{
		Global.self.canBePaused = false;
		this.dragged = false;
		this.dragEnabled = false;
		float timer = 0f;
		float startY = base.transform.position.y;
		float animationTime = this.insertionCurve.GetAnimationLength();
		Transform puzzle = this.GetPuzzleStats().transform;
		Global.self.scrollableUI.GetComponent<scrollablePackArrows>().pauseScrolling(animationTime + 0.1f);
		while (timer < animationTime)
		{
			timer = Mathf.MoveTowards(timer, animationTime, Time.deltaTime);
			float newY = startY + this.insertionCurve.Evaluate(timer);
			if (timer / animationTime > 0.5f)
			{
				newY = Mathf.Clamp(newY, this.minMoveY, float.PositiveInfinity);
				this.SetLayer("Background");
			}
			base.transform.SetY(newY, false);
			yield return null;
		}
		this.pocket = ((base.transform.localPosition.x >= 0f) ? PuzzleKeysPhone_Object.Pocket.Right : PuzzleKeysPhone_Object.Pocket.Left);
		base.snapPoint = (from x in this.snapPoints
		where x.type == Draggable.Snap.X && Mathf.Sign(x.coord) == Mathf.Sign(base.transform.localPosition.x)
		select x).FirstOrDefault<SnapPoint>();
		base.snapPoint.enabled = true;
		this.lockSnapPoint = true;
		this.dragEnabled = true;
		this.bottom = true;
		this.CheckStatus(puzzle);
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x04001943 RID: 6467
	[Header("Pockets")]
	public float leftSnapX;

	// Token: 0x04001944 RID: 6468
	public float rightSnapX;

	// Token: 0x04001945 RID: 6469
	public float pocketY;

	// Token: 0x04001946 RID: 6470
	public float snapDistance = 0.3f;

	// Token: 0x04001947 RID: 6471
	public AnimationCurve insertionCurve;

	// Token: 0x04001948 RID: 6472
	[Tooltip("Minimum height at which the object must be to snap to the line")]
	public float minSnapY;

	// Token: 0x04001949 RID: 6473
	[Tooltip("Minimum height the object can reach while snapped")]
	public float minMoveY;

	// Token: 0x0400194A RID: 6474
	public float successY;

	// Token: 0x0400194B RID: 6475
	[Header("Interchangeable inventory stuff")]
	public Transform smallSprite;

	// Token: 0x0400194C RID: 6476
	public Transform bigSprites;

	// Token: 0x0400194D RID: 6477
	private PuzzleKeysPhone_Object otherObject;

	// Token: 0x0400194E RID: 6478
	private PuzzleKeysPhone_Object.Pocket pocket;

	// Token: 0x0400194F RID: 6479
	private bool bottom;

	// Token: 0x04001950 RID: 6480
	private bool inventoryOn;

	// Token: 0x04001951 RID: 6481
	private List<Renderer> rend = new List<Renderer>();

	// Token: 0x02000427 RID: 1063
	private enum Pocket
	{
		// Token: 0x04001958 RID: 6488
		None,
		// Token: 0x04001959 RID: 6489
		Left,
		// Token: 0x0400195A RID: 6490
		Right
	}
}
