using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000428 RID: 1064
public class PuzzleLego_Piece : LegoPiece
{
	// Token: 0x06001B1B RID: 6939 RVA: 0x0006E59A File Offset: 0x0006C99A
	public override void Start()
	{
		base.Start();
		this.pieces = (from x in this.GetComponentsInPuzzleStats(false)
		where x.dragEnabled
		select x).ToArray<PuzzleLego_Piece>();
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x0006E5D6 File Offset: 0x0006C9D6
	private void Update()
	{
		this.CheckVictoryConditions();
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x0006E5DE File Offset: 0x0006C9DE
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onTheFloor = true;
		}
		else if (other.tag == "FailCollider")
		{
			this.inTheBasket = true;
		}
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x0006E61D File Offset: 0x0006CA1D
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onTheFloor = false;
		}
		else if (other.tag == "FailCollider")
		{
			this.inTheBasket = false;
		}
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x0006E65C File Offset: 0x0006CA5C
	private void CheckVictoryConditions()
	{
		if (!this.pieces.Contains(this))
		{
			return;
		}
		if (this.OnTopOfTower())
		{
			Debug.Log("On top of the tower!");
			this.towerCalled = true;
			this.GetPuzzleStats().GetComponent<AudioVoice_Lego>().playTower();
		}
		else if (this.IsAPenis())
		{
			Debug.Log("oh come on");
			this.penisCalled = true;
			this.GetPuzzleStats().GetComponent<AudioVoice_Lego>().playWeirdShape();
		}
		if ((from x in this.pieces
		where (x.dragEnabled && !x.inTheBasket && !x.onTheFloor) || x.dragged || x.body.velocity.magnitude > Mathf.Epsilon * 3f
		select x).Count<PuzzleLego_Piece>() > 0)
		{
			return;
		}
		if ((from x in this.pieces
		where x.onTheFloor
		select x).Count<PuzzleLego_Piece>() > 0)
		{
			if ((from x in this.pieces
			where x.onTheFloor && Mathf.Abs(Mathf.DeltaAngle(x.transform.eulerAngles.z, 0f)) < 10f
			select x).Count<PuzzleLego_Piece>() > 0)
			{
				Global.LevelCompleted(0f, true);
			}
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x0006E790 File Offset: 0x0006CB90
	private bool IsAPenis()
	{
		if ((from x in this.pieces
		where !x.body.isKinematic || x.penisCalled
		select x).Count<PuzzleLego_Piece>() > 0)
		{
			return false;
		}
		List<PuzzleLego_Piece> list = new List<PuzzleLego_Piece>(this.pieces);
		RaycastHit2D[] array = Physics2D.RaycastAll(base.transform.position, -base.transform.up);
		foreach (RaycastHit2D raycastHit2D in array)
		{
			list.Remove(raycastHit2D.transform.GetComponent<PuzzleLego_Piece>());
		}
		if (list.Count<PuzzleLego_Piece>() != 2)
		{
			return false;
		}
		foreach (Vector3 v in base.GetPoints())
		{
			array = Physics2D.RaycastAll(v, -base.transform.up);
			foreach (RaycastHit2D raycastHit2D2 in array)
			{
				if (list.Remove(raycastHit2D2.transform.GetComponent<PuzzleLego_Piece>()))
				{
					break;
				}
			}
		}
		return list.Count<PuzzleLego_Piece>() == 0;
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x0006E910 File Offset: 0x0006CD10
	private bool OnTopOfTower()
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = true;
		foreach (PuzzleLego_Piece puzzleLego_Piece in this.pieces)
		{
			flag |= puzzleLego_Piece.onTheFloor;
			flag2 |= puzzleLego_Piece.dragged;
			flag3 |= (puzzleLego_Piece.body.velocity.magnitude > Mathf.Epsilon * 3f);
			flag4 |= puzzleLego_Piece.towerCalled;
			flag5 &= puzzleLego_Piece.IsHorizontal();
		}
		if (!flag || flag2 || flag3 || flag4 || !flag5)
		{
			return false;
		}
		List<PuzzleLego_Piece> list = new List<PuzzleLego_Piece>(this.pieces);
		RaycastHit2D[] array2 = Physics2D.RaycastAll(base.transform.position, -base.transform.up);
		foreach (RaycastHit2D raycastHit2D in array2)
		{
			list.Remove(raycastHit2D.transform.GetComponent<PuzzleLego_Piece>());
		}
		return list.Count<PuzzleLego_Piece>() == 0;
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x0006EA44 File Offset: 0x0006CE44
	private bool IsHorizontal()
	{
		return Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 0f)) < 10f || Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 180f)) < 10f;
	}

	// Token: 0x0400195B RID: 6491
	private bool inTheBasket;

	// Token: 0x0400195C RID: 6492
	private bool onTheFloor;

	// Token: 0x0400195D RID: 6493
	private PuzzleLego_Piece[] pieces;

	// Token: 0x0400195E RID: 6494
	private bool towerCalled;

	// Token: 0x0400195F RID: 6495
	private bool penisCalled;
}
