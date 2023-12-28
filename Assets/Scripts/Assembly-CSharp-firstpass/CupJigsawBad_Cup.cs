using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class CupJigsawBad_Cup : MonoBehaviour
{
	// Token: 0x060014BB RID: 5307 RVA: 0x00039FC4 File Offset: 0x000383C4
	private void OnDrawGizmos()
	{
		GlobalCollider componentInPuzzleStats = this.GetComponentInPuzzleStats<GlobalCollider>();
		GizmosExtension.DrawHorizontalLine(componentInPuzzleStats.bottom.position + this.floorOffset, -10f, 10f);
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x00039FF9 File Offset: 0x000383F9
	private void Start()
	{
		this.body = base.GetComponent<Rigidbody2D>();
		this.anvilBody = this.anvil.GetComponent<Rigidbody2D>();
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x0003A018 File Offset: 0x00038418
	private void Update()
	{
		if (!this.anvilBody.isKinematic && this.anvil.position.y <= this.minimalAnvilPosition)
		{
			this.anvilBody.isKinematic = true;
			this.anvilBody.velocity = Vector2.zero;
			this.anvilDust.Emit();
			this.ChangeFloorLevel();
		}
		if (Mathf.Abs(this.anvil.position.y - base.transform.position.y) < this.anvilHitDistance && this.clicks == 2)
		{
			this.AnvilHit();
		}
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x0003A0CC File Offset: 0x000384CC
	private void OnMouseDown()
	{
		if (!base.enabled || !this.responds || this.body.velocity.magnitude > 1f)
		{
			return;
		}
		switch (this.clicks)
		{
		case 0:
			Audio.self.playOneShot("849d55f6-4694-4d31-b3ca-8fd86c96dd3b", 1f);
			this.GetPuzzleStats().GetComponent<AudioVoice_CupJigSawBad>().onClick();
			this.DropCup();
			break;
		case 1:
			Audio.self.playOneShot("849d55f6-4694-4d31-b3ca-8fd86c96dd3b", 1f);
			Audio.self.playOneShot("69b15269-6bbd-47a4-8ab4-ee11196d8025", 1f);
			this.GetPuzzleStats().GetComponent<AudioVoice_CupJigSawBad>().onClick();
			base.StartCoroutine(this.DropShelfCoroutine());
			break;
		case 2:
			Audio.self.playOneShot("849d55f6-4694-4d31-b3ca-8fd86c96dd3b", 1f);
			Audio.self.playOneShot("477b56ba-b868-47b9-8afc-6670a4e4c78b", 1f);
			this.GetPuzzleStats().GetComponent<AudioVoice_CupJigSawBad>().onClick();
			this.DropAnvil();
			break;
		case 3:
			this.body.isKinematic = true;
			Global.CupAcquired(base.transform);
			this.clicks++;
			break;
		}
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x0003A21A File Offset: 0x0003861A
	private void DropCup()
	{
		this.shelf.gameObject.layer = LayerMask.NameToLayer("Individual");
		this.body.isKinematic = false;
		this.body.AddTorque(this.torque);
		this.responds = false;
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x0003A25C File Offset: 0x0003865C
	private void DropAnvil()
	{
		this.responds = false;
		this.anvil.gameObject.SetActive(true);
		this.anvil.position = new Vector3(base.transform.position.x, this.anvil.position.y);
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x0003A2B8 File Offset: 0x000386B8
	private IEnumerator DropShelfCoroutine()
	{
		float lastKey = this.rotationCurve.keys[this.rotationCurve.length - 1].time;
		float timer = 0f;
		this.responds = false;
		while (timer < lastKey)
		{
			timer = Mathf.MoveTowards(timer, lastKey, Time.deltaTime);
			this.shelf.rotation = Quaternion.Euler(0f, 0f, this.rotationCurve.Evaluate(timer));
			yield return null;
		}
		yield return new WaitForSeconds(this.waitAfterRotation);
		this.shelf.gameObject.layer = LayerMask.NameToLayer("Default");
		this.shelf.GetComponent<Rigidbody2D>().isKinematic = false;
		foreach (SpriteRenderer spriteRenderer in this.shelf.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = "Top";
		}
		yield break;
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x0003A2D4 File Offset: 0x000386D4
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "GlobalCollider" && this.clicks == 0)
		{
			this.particles.Emit();
			this.responds = true;
			this.clicks++;
			Audio.self.playOneShot("e862f8e8-97aa-4aa3-8f6a-4dd02b0a30cc", 1f);
			this.ThrowAPiece();
		}
		else if (collision.transform.tag == "SuccessCollider" && this.clicks == 1)
		{
			this.ShelfHit();
		}
		else if (!(collision.transform.tag == "FailCollider") || this.clicks == 2)
		{
		}
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x0003A3A0 File Offset: 0x000387A0
	private void ThrowAPiece()
	{
		Transform transform = this.pieces[this.currentPiece];
		transform.SetParent(base.transform.parent);
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetComponent<Collider2D>().enabled = true;
		Rigidbody2D component = transform.GetComponent<Rigidbody2D>();
		component.isKinematic = false;
		component.AddForce(this.pieceForces[this.currentPiece]);
		component.AddTorque(this.pieceTorques[this.currentPiece]);
		transform.gameObject.layer = LayerMask.NameToLayer("No touching");
		foreach (SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = "Background";
		}
		this.currentPiece++;
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x0003A47C File Offset: 0x0003887C
	private void ShelfHit()
	{
		this.shelf.gameObject.layer = LayerMask.NameToLayer("No touching");
		foreach (SpriteRenderer spriteRenderer in this.shelf.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = "Background";
		}
		this.particles.Emit();
		this.responds = true;
		this.body.AddForce(this.shelfForce);
		this.clicks++;
		this.ThrowAPiece();
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x0003A50C File Offset: 0x0003890C
	private void AnvilHit()
	{
		this.ThrowAPiece();
		this.particles.Emit();
		foreach (SpriteRenderer spriteRenderer in this.anvil.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = "Background";
		}
		this.body.AddForce(this.anvilForce);
		this.body.AddTorque(this.anvilTorque);
		this.responds = true;
		this.clicks++;
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x0003A590 File Offset: 0x00038990
	private void ChangeFloorLevel()
	{
		GlobalCollider componentInPuzzleStats = this.GetComponentInPuzzleStats<GlobalCollider>();
		foreach (BoxCollider2D boxCollider2D in componentInPuzzleStats.GetComponentsInChildren<BoxCollider2D>())
		{
			if (boxCollider2D.offset.x == 0f && boxCollider2D.offset.y < 0f)
			{
				boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, boxCollider2D.offset.y + this.floorOffset);
			}
		}
		this.anvilBody.isKinematic = true;
		this.shelf.GetComponent<Rigidbody2D>().isKinematic = true;
		for (int j = 0; j < this.pieces.Length - 1; j++)
		{
			this.pieces[j].GetComponent<Collider2D>().enabled = false;
			this.pieces[j].GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

	// Token: 0x04001246 RID: 4678
	[Header("Piece stuff")]
	public ParticleSystem particles;

	// Token: 0x04001247 RID: 4679
	public Transform[] pieces;

	// Token: 0x04001248 RID: 4680
	public Vector2[] pieceForces;

	// Token: 0x04001249 RID: 4681
	public float[] pieceTorques;

	// Token: 0x0400124A RID: 4682
	private int currentPiece;

	// Token: 0x0400124B RID: 4683
	[Header("Falling off the shelf")]
	public float torque;

	// Token: 0x0400124C RID: 4684
	[Header("Dropping the shelf")]
	public Transform shelf;

	// Token: 0x0400124D RID: 4685
	public ParticleSystem shelfDust;

	// Token: 0x0400124E RID: 4686
	public AnimationCurve rotationCurve;

	// Token: 0x0400124F RID: 4687
	public float waitAfterRotation;

	// Token: 0x04001250 RID: 4688
	public Vector2 shelfForce;

	// Token: 0x04001251 RID: 4689
	[Header("Dropping the anvil")]
	public Transform anvil;

	// Token: 0x04001252 RID: 4690
	public ParticleSystem anvilDust;

	// Token: 0x04001253 RID: 4691
	public float minimalAnvilPosition;

	// Token: 0x04001254 RID: 4692
	public float anvilHitDistance;

	// Token: 0x04001255 RID: 4693
	public Vector2 anvilForce;

	// Token: 0x04001256 RID: 4694
	public float anvilTorque;

	// Token: 0x04001257 RID: 4695
	public float floorOffset;

	// Token: 0x04001258 RID: 4696
	private Rigidbody2D anvilBody;

	// Token: 0x04001259 RID: 4697
	private bool responds = true;

	// Token: 0x0400125A RID: 4698
	private int clicks;

	// Token: 0x0400125B RID: 4699
	private Rigidbody2D body;
}
