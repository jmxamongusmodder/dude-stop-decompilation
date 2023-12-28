using System;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class CupRoboCup_Part : Draggable
{
	// Token: 0x06001558 RID: 5464 RVA: 0x0003EFAD File Offset: 0x0003D3AD
	private void Update()
	{
		this.CheckReturn();
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x0003EFB8 File Offset: 0x0003D3B8
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.CheckSnapPointInit();
		base.OnMouseDown();
		this.CheckPrerequisite();
		base.body.isKinematic = false;
		this.EnableHiddenSprite(true);
		this.lockSnapPoint = false;
		foreach (SpriteRenderer spriteRenderer in base.transform.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = "Top";
		}
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x0003F02C File Offset: 0x0003D42C
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (base.Snapped() && !this.prerequisitesMatched)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_CupRoboCup>().cantSnapNow();
		}
		else if (base.Snapped())
		{
			this.lockSnapPoint = true;
			base.body.isKinematic = true;
			if (base.GetSnapPoint().transform != null)
			{
				this.EnableHiddenSprite(false);
				this.correctSpot.gameObject.SetActive(true);
				base.gameObject.SetActive(false);
				Audio.self.playOneShot("b9c1aae6-07a7-43e7-96f2-2de92fc35415", 1f);
				Global.self.currPuzzle.GetComponent<AudioVoice_CupRoboCup>().placePart();
			}
		}
		else if (this.onBoard)
		{
			base.body.velocity = Vector2.zero;
			base.body.isKinematic = true;
		}
		foreach (SpriteRenderer spriteRenderer in base.transform.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = "Default";
		}
		this.CheckVictoryCondition();
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x0003F150 File Offset: 0x0003D550
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "FailCollider" && this.dragged)
		{
			this.onBoard = true;
		}
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x0003F179 File Offset: 0x0003D579
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.onBoard = false;
		}
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x0003F198 File Offset: 0x0003D598
	private void CheckReturn()
	{
		if (!this.onBoard || this.dragged)
		{
			return;
		}
		if (base.WasMoved())
		{
			base.transform.position = Vector2.Lerp(base.transform.position, this.startingPosition, this.returnLerpSpeed * Time.deltaTime);
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.startingPosition, this.returnMoveSpeed * Time.deltaTime);
		}
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x0003F240 File Offset: 0x0003D640
	private void EnableHiddenSprite(bool status)
	{
		Transform transform = base.transform.Find("hide");
		if (transform != null)
		{
			transform.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x0003F278 File Offset: 0x0003D678
	private void CheckPrerequisite()
	{
		if (this.prerequisite == null && this.anotherPrerequisite)
		{
			return;
		}
		bool flag = true;
		if (this.prerequisite != null && !this.prerequisite.Snapped())
		{
			flag = false;
		}
		if (this.anotherPrerequisite != null && !this.anotherPrerequisite.Snapped())
		{
			flag = false;
		}
		this.prerequisitesMatched = flag;
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x0003F2F8 File Offset: 0x0003D6F8
	private void CheckVictoryCondition()
	{
		bool flag = true;
		foreach (CupRoboCup_Part cupRoboCup_Part in this.GetComponentsInPuzzleStats(false))
		{
			flag &= (cupRoboCup_Part.Snapped() && cupRoboCup_Part.GetSnapPoint().transform != null && cupRoboCup_Part.lockSnapPoint);
		}
		if (flag)
		{
			Global.CupAcquired(this.cup);
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x0003F365 File Offset: 0x0003D765
	private void CheckSnapPointInit()
	{
		if (this.snapInit)
		{
			return;
		}
		this.snapInit = true;
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.correctSpot.position, this.snapDist, this.correctSpot), false);
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x0003F3A3 File Offset: 0x0003D7A3
	protected override void OnUnsnap(SnapPoint point)
	{
		base.transform.Find("shadow").gameObject.SetActive(false);
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x0003F3C0 File Offset: 0x0003D7C0
	protected override void OnSnap(SnapPoint point)
	{
		base.transform.Find("shadow").gameObject.SetActive(true);
	}

	// Token: 0x04001306 RID: 4870
	public Transform cup;

	// Token: 0x04001307 RID: 4871
	public Transform correctSpot;

	// Token: 0x04001308 RID: 4872
	public CupRoboCup_Part prerequisite;

	// Token: 0x04001309 RID: 4873
	public CupRoboCup_Part anotherPrerequisite;

	// Token: 0x0400130A RID: 4874
	public float snapDist = 0.3f;

	// Token: 0x0400130B RID: 4875
	public float returnMoveSpeed = 2f;

	// Token: 0x0400130C RID: 4876
	public float returnLerpSpeed = 1f;

	// Token: 0x0400130D RID: 4877
	private bool snapInit;

	// Token: 0x0400130E RID: 4878
	private bool onBoard;

	// Token: 0x0400130F RID: 4879
	private bool prerequisitesMatched;
}
