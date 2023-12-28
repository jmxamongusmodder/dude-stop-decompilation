using System;
using System.Linq;
using UnityEngine;

// Token: 0x0200044B RID: 1099
public class PuzzleShopClothes_Shirt : EnhancedDraggable
{
	// Token: 0x06001C23 RID: 7203 RVA: 0x00076265 File Offset: 0x00074665
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawPoint(this.hanger.position + this.shift, 0.5f);
	}

	// Token: 0x06001C24 RID: 7204 RVA: 0x0007628C File Offset: 0x0007468C
	private void Start()
	{
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.hanger.position + this.shift, this.snapDist, this.hanger), true);
		this.hanged = base.transform.GetChild(0);
		this.taken = base.transform.GetChild(1);
		this.dropped = base.transform.GetChild(2);
		foreach (Collider2D collider2D in base.GetComponents<Collider2D>())
		{
			if (collider2D.isTrigger)
			{
				this.hangedCollider = collider2D;
			}
			else
			{
				this.droppedCollider = collider2D;
			}
		}
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x00076340 File Offset: 0x00074740
	protected override void BeforeMouseUpped()
	{
		if (base.Snapped())
		{
			base.RemoveSnapPoint(base.GetSnapPoint());
			this.SetStatus(PuzzleShopClothes_Shirt.Status.Hanged);
			Audio.self.playOneShot("de921bbf-c8f5-4a1c-bac6-6fe8d6d51939", 1f);
		}
		else
		{
			this.SetStatus(PuzzleShopClothes_Shirt.Status.Dropped);
			Audio.self.playOneShot("6fb9a9c5-f124-4789-9a9c-6c76e053bb9a", 1f);
			base.body.bodyType = RigidbodyType2D.Dynamic;
		}
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x000763B0 File Offset: 0x000747B0
	protected override void MouseDowned()
	{
		this.GenerateImage();
		if (base.Snapped())
		{
			this.startingPoint = base.GetSnapPoint();
			base.AddSnapPoint(this.startingPoint, false);
			this.SetStatus(PuzzleShopClothes_Shirt.Status.Hanged);
			if (this.GetComponentsInPuzzleStats(false).Count((PuzzleShopClothes_Shirt x) => x.status == PuzzleShopClothes_Shirt.Status.Dropped) > 0)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_ShopClothes>().takeOtherIfExistsOnTheFloor();
			}
		}
		base.body.bodyType = RigidbodyType2D.Kinematic;
		this.SetStatus(PuzzleShopClothes_Shirt.Status.Taken);
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x00076444 File Offset: 0x00074844
	private void OnCollisionEnter2D(Collision2D collision)
	{
		this.CheckFloorCollision(collision);
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x0007644D File Offset: 0x0007484D
	private void OnTriggerStay2D(Collider2D other)
	{
		this.CheckIfShirtIsOverTheCart(other);
		this.CheckVictoryConditions(other);
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x00076460 File Offset: 0x00074860
	private void GenerateImage()
	{
		if (this.imageGenerated)
		{
			return;
		}
		if (PuzzleShopClothes_Flier.images.Count > 0)
		{
			int item = PuzzleShopClothes_Flier.images[UnityEngine.Random.Range(0, PuzzleShopClothes_Flier.images.Count)];
			PuzzleShopClothes_Flier.images.Remove(item);
		}
		else
		{
			base.gameObject.layer = LayerMask.NameToLayer("Prlx 2");
			this.dropped.GetComponent<SpriteRenderer>().sortingOrder = 9;
		}
		this.imageGenerated = true;
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x000764E3 File Offset: 0x000748E3
	protected override void OnUnsnap(SnapPoint point)
	{
		Audio.self.playOneShot("1e40096c-eaaf-4f0d-99d9-1945c78c5e87", 1f);
	}

	// Token: 0x06001C2B RID: 7211 RVA: 0x000764FA File Offset: 0x000748FA
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag != "SuccessCollider")
		{
			return;
		}
		this.waitTimer = 0f;
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x00076520 File Offset: 0x00074920
	private void CheckIfShirtIsOverTheCart(Collider2D other)
	{
		if (!base.enabled || other.tag != "Finish")
		{
			return;
		}
		this.overTheCart = (Mathf.Abs(base.body.velocity.x) < this.overTheCartThreshold);
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x00076574 File Offset: 0x00074974
	private void CheckVictoryConditions(Collider2D other)
	{
		if (!base.enabled || other.tag != "SuccessCollider")
		{
			return;
		}
		if (base.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > this.minimalMagnitude)
		{
			return;
		}
		this.waitTimer = Mathf.MoveTowards(this.waitTimer, this.waitTime, Time.deltaTime);
		if (this.waitTimer < this.waitTime - Mathf.Epsilon)
		{
			return;
		}
		bool flag = true;
		foreach (PuzzleShopClothes_Shirt puzzleShopClothes_Shirt in from x in this.GetComponentsInPuzzleStats(false)
		where x != this
		select x)
		{
			if (!puzzleShopClothes_Shirt.overTheCart && puzzleShopClothes_Shirt.status == PuzzleShopClothes_Shirt.Status.Dropped)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			Global.LevelFailed(0f, true);
		}
		else
		{
			Global.LevelCompleted(0f, true);
		}
	}

	// Token: 0x06001C2E RID: 7214 RVA: 0x00076690 File Offset: 0x00074A90
	private void CheckFloorCollision(Collision2D collision)
	{
		if (collision.transform.tag != "GlobalCollider")
		{
			return;
		}
		if (this.collidedWithFloor)
		{
			return;
		}
		this.collidedWithFloor = true;
		this.GetComponentInPuzzleStats<AudioVoice_ShopClothes>().dropShirt();
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x000766CC File Offset: 0x00074ACC
	private void SetStatus(PuzzleShopClothes_Shirt.Status newStatus)
	{
		PuzzleShopClothes_Shirt.Status status = this.status;
		if (status != PuzzleShopClothes_Shirt.Status.Taken)
		{
			if (status != PuzzleShopClothes_Shirt.Status.Dropped)
			{
				if (status == PuzzleShopClothes_Shirt.Status.Hanged)
				{
					this.hanged.gameObject.SetActive(false);
					this.hangedCollider.enabled = false;
				}
			}
			else
			{
				this.dropped.gameObject.SetActive(false);
				this.droppedCollider.enabled = false;
				base.GetComponent<Rigidbody2D>().isKinematic = true;
			}
		}
		else
		{
			this.taken.gameObject.SetActive(false);
		}
		this.status = newStatus;
		PuzzleShopClothes_Shirt.Status status2 = this.status;
		if (status2 != PuzzleShopClothes_Shirt.Status.Taken)
		{
			if (status2 != PuzzleShopClothes_Shirt.Status.Dropped)
			{
				if (status2 == PuzzleShopClothes_Shirt.Status.Hanged)
				{
					this.hanged.gameObject.SetActive(true);
					this.hangedCollider.enabled = true;
				}
			}
			else
			{
				this.dropped.gameObject.SetActive(true);
				this.droppedCollider.enabled = true;
				base.GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
		else
		{
			this.taken.gameObject.SetActive(true);
		}
	}

	// Token: 0x04001A71 RID: 6769
	[Header("Shirt stuff")]
	public Transform hanger;

	// Token: 0x04001A72 RID: 6770
	public float hangerForce;

	// Token: 0x04001A73 RID: 6771
	public float snapDist = 0.3f;

	// Token: 0x04001A74 RID: 6772
	public Vector3 shift;

	// Token: 0x04001A75 RID: 6773
	public float overTheCartThreshold = 0.3f;

	// Token: 0x04001A76 RID: 6774
	private bool imageGenerated;

	// Token: 0x04001A77 RID: 6775
	private Transform hanged;

	// Token: 0x04001A78 RID: 6776
	private Collider2D hangedCollider;

	// Token: 0x04001A79 RID: 6777
	private Transform taken;

	// Token: 0x04001A7A RID: 6778
	private Transform dropped;

	// Token: 0x04001A7B RID: 6779
	private Collider2D droppedCollider;

	// Token: 0x04001A7C RID: 6780
	private SnapPoint startingPoint;

	// Token: 0x04001A7D RID: 6781
	private PuzzleShopClothes_Shirt.Status status;

	// Token: 0x04001A7E RID: 6782
	[Header("Level end")]
	public float waitTime = 1f;

	// Token: 0x04001A7F RID: 6783
	public float minimalMagnitude;

	// Token: 0x04001A80 RID: 6784
	private float waitTimer;

	// Token: 0x04001A81 RID: 6785
	private bool overTheCart;

	// Token: 0x04001A82 RID: 6786
	private bool collidedWithFloor;

	// Token: 0x0200044C RID: 1100
	private enum Status
	{
		// Token: 0x04001A85 RID: 6789
		Hanged,
		// Token: 0x04001A86 RID: 6790
		Taken,
		// Token: 0x04001A87 RID: 6791
		Dropped
	}
}
