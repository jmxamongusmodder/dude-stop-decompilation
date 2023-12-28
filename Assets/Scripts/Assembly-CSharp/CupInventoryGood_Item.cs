using System;
using System.Linq;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class CupInventoryGood_Item : InventoryDraggable
{
	// Token: 0x0600009E RID: 158 RVA: 0x000081E3 File Offset: 0x000063E3
	private void Awake()
	{
		base.GetComponent<CupInventory_InventoryItem>().lockMouse = true;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000081F4 File Offset: 0x000063F4
	private void Start()
	{
		base.GetComponent<Collider2D>().enabled = false;
		this.returnToInventory = true;
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.snapItem.transform.position, this.snapDist, this.snapItem.transform), false);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00008248 File Offset: 0x00006448
	private void Update()
	{
		if (!base.Snapped() && this.wasSnapped)
		{
			this.snapItem.snapped = false;
		}
		else if (base.Snapped() && !this.wasSnapped)
		{
			this.snapItem.snapped = true;
		}
		this.wasSnapped = base.Snapped();
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000082AA File Offset: 0x000064AA
	public void GetPartner()
	{
		this.otherSnapItem = (from x in this.GetComponentsInPuzzleStats(false)
		where x.snapItem == this.snapItem && x != this
		select x).FirstOrDefault<CupInventoryGood_Item>();
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x000082CF File Offset: 0x000064CF
	public override void OnMouseDown()
	{
		if (this.otherSnapItem.Snapped())
		{
			base.snapEnabled = false;
		}
		else
		{
			base.snapEnabled = true;
		}
		base.OnMouseDown();
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000082FC File Offset: 0x000064FC
	public override void OnMouseUp()
	{
		if (base.Snapped())
		{
			base.transform.SetParent(this.snapItem.transform);
			this.returnToInventory = false;
		}
		else
		{
			this.returnToInventory = true;
		}
		base.OnMouseUp();
		this.CheckVictoryConditions();
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00008349 File Offset: 0x00006549
	protected override void ChangeLooks()
	{
		if (this.firstAppearance)
		{
			base.GetComponent<Collider2D>().enabled = true;
			this.firstAppearance = false;
		}
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00008369 File Offset: 0x00006569
	protected override void MoveBackToInventory()
	{
		if (this.wasSnapped)
		{
			this.snapItem.snapped = false;
		}
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00008382 File Offset: 0x00006582
	private void CheckVictoryConditions()
	{
		if ((from x in UnityEngine.Object.FindObjectsOfType<CupInventoryGood_Item>()
		where x.correctItem && x.Snapped()
		select x).Count<CupInventoryGood_Item>() == 4)
		{
			this.GetComponentInPuzzleStats<CupInventoryGood_Controller>().StartEndAnimation();
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000083C1 File Offset: 0x000065C1
	protected override void OnSnap(SnapPoint point)
	{
		Audio.self.playOneShot("b416b8b1-54ec-4423-aee6-6d6749f1ac2f", 1f);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000083D8 File Offset: 0x000065D8
	protected override void OnUnsnap(SnapPoint point)
	{
		Audio.self.playOneShot("1bf2d979-b26c-45e7-b1dc-f1082ff4c792", 1f);
	}

	// Token: 0x04000125 RID: 293
	public bool correctItem;

	// Token: 0x04000126 RID: 294
	public CupInventoryGood_Silhouette snapItem;

	// Token: 0x04000127 RID: 295
	public float snapDist;

	// Token: 0x04000128 RID: 296
	private CupInventoryGood_Item otherSnapItem;

	// Token: 0x04000129 RID: 297
	private bool wasSnapped;

	// Token: 0x0400012A RID: 298
	private bool firstAppearance = true;
}
