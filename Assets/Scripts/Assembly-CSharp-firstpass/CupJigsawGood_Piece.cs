using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class CupJigsawGood_Piece : Draggable
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060014CB RID: 5323 RVA: 0x0003A983 File Offset: 0x00038D83
	private CupJigsawGood_Piece[] thisSidePieces
	{
		get
		{
			return (from x in this.allPieces
			where x.leftSide == this.leftSide
			select x).ToArray<CupJigsawGood_Piece>();
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060014CC RID: 5324 RVA: 0x0003A9A4 File Offset: 0x00038DA4
	private CupJigsawGood_Piece incorrectPiece
	{
		get
		{
			foreach (CupJigsawGood_Piece cupJigsawGood_Piece in this.thisSidePieces)
			{
				if (!cupJigsawGood_Piece.thisIsCorrectPiece)
				{
					return cupJigsawGood_Piece;
				}
			}
			return null;
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060014CD RID: 5325 RVA: 0x0003A9E0 File Offset: 0x00038DE0
	private bool correctPieceSnapped
	{
		get
		{
			foreach (CupJigsawGood_Piece cupJigsawGood_Piece in from x in this.thisSidePieces
			where x.thisIsCorrectPiece
			select x)
			{
				if (cupJigsawGood_Piece.Snapped())
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x0003AA6C File Offset: 0x00038E6C
	private void Start()
	{
		this.allPieces = this.GetComponentsInPuzzleStats(false);
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x0003AA7B File Offset: 0x00038E7B
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		this.CheckSnapPointInit();
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.lockSnapPoint = false;
		this.CheckSnapEnabled();
		this.ShiftSprites();
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x0003AAA8 File Offset: 0x00038EA8
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (base.Snapped())
		{
			this.lockSnapPoint = true;
			base.GetComponent<Rigidbody2D>().isKinematic = true;
		}
		this.CheckVictoryCondition();
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x0003AAD4 File Offset: 0x00038ED4
	private void CheckSnapPointInit()
	{
		if (this.snapInit)
		{
			return;
		}
		this.snapInit = true;
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.correctSpot.position, this.snapDist), false);
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x0003AB0C File Offset: 0x00038F0C
	private void CheckVictoryCondition()
	{
		bool flag = true;
		foreach (CupJigsawGood_Piece cupJigsawGood_Piece in from x in this.allPieces
		where x.thisIsCorrectPiece
		select x)
		{
			flag &= cupJigsawGood_Piece.Snapped();
		}
		if (flag)
		{
			this.GetComponentInPuzzleStats<CupJigsawGood_Cup>().GetComponent<Collider2D>().enabled = true;
			this.GetComponentInPuzzleStats<CupJigsawGood_Cup>().OnComplete();
		}
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x0003ABB0 File Offset: 0x00038FB0
	private void CheckSnapEnabled()
	{
		if (this.thisIsCorrectPiece)
		{
			if (this.incorrectPiece.Snapped())
			{
				base.snapEnabled = false;
			}
			else
			{
				base.snapEnabled = true;
			}
		}
		else if (this.correctPieceSnapped)
		{
			base.snapEnabled = false;
		}
		else
		{
			base.snapEnabled = true;
		}
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x0003AC10 File Offset: 0x00039010
	private void ShiftSprites()
	{
		if (!this.dragEnabled)
		{
			return;
		}
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		int sortingOrder = component.sortingOrder;
		foreach (CupJigsawGood_Piece cupJigsawGood_Piece in this.allPieces)
		{
			SpriteRenderer component2 = cupJigsawGood_Piece.GetComponent<SpriteRenderer>();
			if (component2.sortingOrder >= sortingOrder && !(component2 == component))
			{
				component2.sortingOrder--;
			}
		}
		component.sortingOrder = this.allPieces.Count<CupJigsawGood_Piece>();
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x0003ACA2 File Offset: 0x000390A2
	protected override void OnSnap(SnapPoint point)
	{
		Audio.self.playOneShot("356af150-468a-4948-8f69-1975c212c3b0", 1f);
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x0003ACB9 File Offset: 0x000390B9
	protected override void OnUnsnap(SnapPoint point)
	{
		Audio.self.playOneShot("c03063ed-86eb-4182-bb2f-c4d617fa0f03", 1f);
	}

	// Token: 0x0400125F RID: 4703
	public Transform correctSpot;

	// Token: 0x04001260 RID: 4704
	public float snapDist;

	// Token: 0x04001261 RID: 4705
	public bool thisIsCorrectPiece;

	// Token: 0x04001262 RID: 4706
	public bool leftSide;

	// Token: 0x04001263 RID: 4707
	private bool snapInit;

	// Token: 0x04001264 RID: 4708
	private CupJigsawGood_Piece[] allPieces;
}
