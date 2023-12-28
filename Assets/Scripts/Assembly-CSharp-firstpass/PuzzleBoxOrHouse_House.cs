using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003CF RID: 975
[EnabledManually]
public class PuzzleBoxOrHouse_House : PivotDraggable
{
	// Token: 0x0600186F RID: 6255 RVA: 0x00055D66 File Offset: 0x00054166
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawPoint(this.tableSnap, 0.5f);
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x00055D78 File Offset: 0x00054178
	private void Start()
	{
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.tableSnap, this.snapDist), false);
		base.StartCoroutine(this.MovingOutCoroutine());
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x00055DA0 File Offset: 0x000541A0
	private void Update()
	{
		if (this.dragged)
		{
			if (Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 0f)) > 45f || Mathf.Abs(base.transform.position.x - this.tableSnap.x) > this.snapDist)
			{
				base.snapEnabled = false;
				this.leftLeg.gameObject.layer = LayerMask.NameToLayer("Default");
				this.rightLeg.gameObject.layer = LayerMask.NameToLayer("Default");
				this.nailFiller.layer = LayerMask.NameToLayer("Default");
			}
			else
			{
				base.snapEnabled = true;
				this.leftLeg.gameObject.layer = LayerMask.NameToLayer("Front");
				this.rightLeg.gameObject.layer = LayerMask.NameToLayer("Front");
				this.nailFiller.layer = LayerMask.NameToLayer("Front");
			}
		}
		if (base.Snapped() && base.transform.eulerAngles.z != 0f)
		{
			base.transform.rotation = Quaternion.Euler(Vector3.zero);
		}
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x00055EF8 File Offset: 0x000542F8
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.leftLeg.gameObject.layer = LayerMask.NameToLayer("Front");
		this.rightLeg.gameObject.layer = LayerMask.NameToLayer("Front");
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x00055F4C File Offset: 0x0005434C
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped() || Vector2.Distance(base.transform.position, this.tableSnap) < this.snapDist)
		{
			base.transform.position = this.tableSnap;
			base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			this.ceilingCat.gameObject.SetActive(true);
			this.ceilingCat.enabled = true;
			if (SerializablePuzzleStats.Get(this.GetPuzzleStats().transform.name).playedTimes > 0)
			{
				this.ceilingCat.SetTrigger("Short");
			}
			else
			{
				this.ceilingCat.SetTrigger("Long");
			}
			base.GetComponent<Rigidbody2D>().SetKinematic();
			base.enabled = false;
			Audio.self.playOneShot("e47c8a04-366f-4861-a2e0-567259b0eab3", 1f);
		}
		else
		{
			this.nailFiller.layer = LayerMask.NameToLayer("Default");
			this.leftLeg.gameObject.layer = LayerMask.NameToLayer("Default");
			this.rightLeg.gameObject.layer = LayerMask.NameToLayer("Default");
		}
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x000560A8 File Offset: 0x000544A8
	private IEnumerator MovingOutCoroutine()
	{
		this.dragEnabled = false;
		Vector2 startPosition = this.boxBottomCollider.position;
		Vector2 endPosition = startPosition + this.bottomColliderOffset * Vector2.up;
		float timer = 0f;
		while (timer != this.movingOutTime)
		{
			timer = Mathf.MoveTowards(timer, this.movingOutTime, Time.deltaTime);
			this.boxBottomCollider.position = Vector2.Lerp(startPosition, endPosition, timer / this.movingOutTime);
			yield return null;
		}
		this.dragEnabled = true;
		yield break;
	}

	// Token: 0x0400165A RID: 5722
	public Animator ceilingCat;

	// Token: 0x0400165B RID: 5723
	public Transform cat;

	// Token: 0x0400165C RID: 5724
	public Transform boxBottomCollider;

	// Token: 0x0400165D RID: 5725
	public float bottomColliderOffset;

	// Token: 0x0400165E RID: 5726
	public float movingOutTime;

	// Token: 0x0400165F RID: 5727
	public Vector2 tableSnap;

	// Token: 0x04001660 RID: 5728
	public float snapDist;

	// Token: 0x04001661 RID: 5729
	public Collider2D leftLeg;

	// Token: 0x04001662 RID: 5730
	public Collider2D rightLeg;

	// Token: 0x04001663 RID: 5731
	public GameObject nailFiller;
}
