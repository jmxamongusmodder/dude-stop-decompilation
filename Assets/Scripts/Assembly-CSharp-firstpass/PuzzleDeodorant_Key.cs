using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003F3 RID: 1011
public class PuzzleDeodorant_Key : InventoryDraggable, TransitionProcessor
{
	// Token: 0x06001991 RID: 6545 RVA: 0x0005FFCE File Offset: 0x0005E3CE
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		GizmosExtension.DrawPoint(this.keyhole, Color.blue, 0.5f);
		GizmosExtension.DrawPoint(this.realKeyhole, Color.blue, 0.5f);
		GizmosExtension.DrawVerticalLine(this.keyholeLine);
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x0006000C File Offset: 0x0005E40C
	public override void OnEnable()
	{
		base.OnEnable();
		this.door = this.GetComponentInPuzzleStats<PuzzleDeodorant_Door>();
		this.loneKeyMaterial = this.loneKeySprite.GetComponent<SpriteRenderer>().material;
		this.loneKeyMaterial.SetFloat("_Angle", 1.5707964f);
		this.loneKeyMaterial.SetFloat("_Distance", 0f);
		this.loneKeyMaterial.SetFloat("_Left", 1f);
		if (!this.doorOpened)
		{
			base.RemoveAllSnapPoints();
			base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.keyhole, this.keyholeSnapDist), false);
			this.CreateRealKeyholeSnapPoint();
		}
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x000600B0 File Offset: 0x0005E4B0
	private void Awake()
	{
		if (!this.doorOpened)
		{
			base.snapEnabled = true;
		}
	}

	// Token: 0x06001994 RID: 6548 RVA: 0x000600C4 File Offset: 0x0005E4C4
	private void CreateRealKeyholeSnapPoint()
	{
		if (this.doorOpened)
		{
			return;
		}
		this.realKeyholePoint = new SnapPoint(Draggable.Snap.XY, this.realKeyhole, this.keyholeSnapDist)
		{
			enabled = false
		};
		base.AddSnapPoint(this.realKeyholePoint, false);
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x0006010C File Offset: 0x0005E50C
	public override void OnMouseUp()
	{
		if ((base.Snapped() && !this.coroutineStarted) || this.forcedUnsnap)
		{
			this.forcedUnsnap = false;
			base.StartCoroutine(this.MovingCoroutine());
		}
		else
		{
			base.OnMouseUp();
		}
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x0006015C File Offset: 0x0005E55C
	protected override void ChangeLooks()
	{
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.gameObject.SetActive(false);
		}
		this.loneKeySprite.gameObject.SetActive(true);
		base.GetComponent<PolygonCollider2D>().enabled = false;
		base.GetComponent<BoxCollider2D>().enabled = true;
		this.returnToInventory = true;
	}

	// Token: 0x06001997 RID: 6551 RVA: 0x000601C4 File Offset: 0x0005E5C4
	public override void EnterInventory()
	{
		this.loneKeySprite.gameObject.SetActive(false);
		base.GetComponent<PolygonCollider2D>().enabled = true;
		base.GetComponent<BoxCollider2D>().enabled = false;
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x000601EF File Offset: 0x0005E5EF
	protected override void MoveBackToInventory()
	{
		this.EnterInventory();
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x000601F7 File Offset: 0x0005E5F7
	private void DisableInput()
	{
		this.returnToInventory = false;
		this.dragged = false;
		base.GetComponent<BoxCollider2D>().enabled = false;
		this.coroutineStarted = true;
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x0006021C File Offset: 0x0005E61C
	protected override void OnSnap(SnapPoint point)
	{
		if (point != this.realKeyholePoint)
		{
			this.realKeyholePoint.enabled = true;
			Vector2 vector = Camera.main.WorldToViewportPoint(new Vector2(this.keyholeLine, 0f));
			this.loneKeyMaterial.SetFloat("_Left", vector.x);
		}
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x0006027D File Offset: 0x0005E67D
	protected override void OnUnsnap(SnapPoint point)
	{
		if (point != this.realKeyholePoint)
		{
			this.realKeyholePoint.enabled = false;
			this.loneKeyMaterial.SetFloat("_Left", 1f);
		}
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x000602AC File Offset: 0x0005E6AC
	protected override void OnSnapChange(SnapPoint oldPoint, SnapPoint newPoint)
	{
		if (newPoint == this.realKeyholePoint)
		{
			this.forcedUnsnap = true;
			base.snapEnabled = false;
			base.RemoveAllSnapPoints();
			base.snapPoint = oldPoint;
			newPoint.coord2D = oldPoint.coord2D;
			this.realKeyholePoint.coord2D = oldPoint.coord2D;
			base.transform.position = oldPoint.coord2D;
			this.OnMouseUp();
		}
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x0006031C File Offset: 0x0005E71C
	private IEnumerator MovingCoroutine()
	{
		this.DisableInput();
		Global.self.canBePaused = false;
		Global.PauseArrows(this.waitBeforeUnlock + 1.2f);
		PuzzleFridgePaintings_Bin bin = this.GetComponentInPuzzleStats<PuzzleFridgePaintings_Bin>();
		if (bin != null)
		{
			bin.dragEnabled = false;
		}
		Vector2 endPosition = new Vector2(this.keyholeLine, this.keyhole.y);
		while (Vector2.Distance(base.transform.position, endPosition) > 0f)
		{
			base.transform.position = Vector2.Lerp(base.transform.position, endPosition, Time.deltaTime * this.keyholeSnapLerpSpeed);
			base.transform.position = Vector2.MoveTowards(base.transform.position, endPosition, Time.deltaTime * this.keyholeSnapMoveSpeed);
			yield return null;
		}
		yield return base.StartCoroutine(this.UnlockingCoroutine());
		yield break;
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x00060338 File Offset: 0x0005E738
	private IEnumerator UnlockingCoroutine()
	{
		this.DisableInput();
		Global.self.canBePaused = false;
		Global.PauseArrows(this.waitBeforeUnlock + 1f);
		PuzzleFridgePaintings_Bin bin = this.GetComponentInPuzzleStats<PuzzleFridgePaintings_Bin>();
		if (bin != null)
		{
			bin.dragEnabled = false;
		}
		Audio.self.playOneShot("2283442d-3814-4989-b5fa-263bec9708b7", 1f);
		yield return new WaitForSeconds(this.waitBeforeUnlock);
		Audio.self.playOneShot("82706555-0372-43c8-84b8-628265b5dfe3", 1f);
		this.door.RemoveLock();
		this.doorOpened = true;
		base.snapEnabled = false;
		this.returnToInventory = true;
		base.RemoveAllSnapPoints();
		this.loneKeySprite.GetComponent<SpriteRenderer>().material.SetFloat("_Left", 1f);
		yield return new WaitForSeconds(0.1f);
		base.GetComponent<PolygonCollider2D>().enabled = true;
		base.GetComponent<InventoryItem>().moveBackToInventory();
		if (bin != null)
		{
			bin.dragEnabled = true;
		}
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x00060354 File Offset: 0x0005E754
	public void TransitionUpdate()
	{
		if (this.loneKeyMaterial == null)
		{
			return;
		}
		Vector2 vector = Camera.main.WorldToViewportPoint(new Vector2(this.keyholeLine + this.GetPuzzleStats().transform.position.x, 0f));
		this.loneKeyMaterial.SetFloat("_Left", vector.x);
	}

	// Token: 0x04001797 RID: 6039
	public Transform loneKeySprite;

	// Token: 0x04001798 RID: 6040
	public Vector2 keyhole;

	// Token: 0x04001799 RID: 6041
	public float keyholeLine;

	// Token: 0x0400179A RID: 6042
	public float keyholeSnapDist;

	// Token: 0x0400179B RID: 6043
	public float keyholeSnapLerpSpeed;

	// Token: 0x0400179C RID: 6044
	public float keyholeSnapMoveSpeed;

	// Token: 0x0400179D RID: 6045
	public float waitBeforeUnlock = 1f;

	// Token: 0x0400179E RID: 6046
	public Vector2 realKeyhole;

	// Token: 0x0400179F RID: 6047
	private SnapPoint realKeyholePoint;

	// Token: 0x040017A0 RID: 6048
	private bool forcedUnsnap;

	// Token: 0x040017A1 RID: 6049
	private Material loneKeyMaterial;

	// Token: 0x040017A2 RID: 6050
	private PuzzleDeodorant_Door door;

	// Token: 0x040017A3 RID: 6051
	private bool doorOpened;

	// Token: 0x040017A4 RID: 6052
	private bool coroutineStarted;
}
