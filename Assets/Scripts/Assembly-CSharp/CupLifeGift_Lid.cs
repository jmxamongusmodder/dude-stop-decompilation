using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class CupLifeGift_Lid : Draggable
{
	// Token: 0x060000C8 RID: 200 RVA: 0x0000947E File Offset: 0x0000767E
	private void Update()
	{
		this.CheckPosition();
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00009486 File Offset: 0x00007686
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (!this.lockX)
		{
			this.dragEnabled = false;
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000094AC File Offset: 0x000076AC
	private void CheckPosition()
	{
		if (!this.lockX)
		{
			return;
		}
		if (base.transform.position.y < this.openY)
		{
			return;
		}
		this.lockX = false;
		this.GetComponentInPuzzleStats<CupLifeGift_Box>().Enable();
		base.gameObject.layer = LayerMask.NameToLayer("Prlx 1");
		foreach (BoxCollider2D boxCollider2D in this.box.GetComponents<BoxCollider2D>())
		{
			boxCollider2D.isTrigger = false;
		}
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = "Background";
		}
		base.transform.position += Vector3.forward;
	}

	// Token: 0x04000153 RID: 339
	public float openY;

	// Token: 0x04000154 RID: 340
	public Transform box;
}
