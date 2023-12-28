using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class CupMonsterCup_Draggable : PivotDraggable
{
	// Token: 0x0600154C RID: 5452 RVA: 0x0003EA87 File Offset: 0x0003CE87
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		GizmosExtension.DrawHorizontalLine(this.minimalLine, -10f, 10f);
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x0003EAA4 File Offset: 0x0003CEA4
	private void Awake()
	{
		this.pot = this.GetComponentInPuzzleStats<CupMonsterCup_Pot>();
		this.rend = base.GetComponent<SpriteRenderer>();
		Vector2 vector = Camera.main.WorldToViewportPoint(Vector3.up * this.minimalLine);
		this.rend.material.SetFloat("_Top", vector.y);
		this.rend.material.SetFloat("_Left", 0f);
		this.rend.material.SetFloat("_Angle", 0f);
		this.rend.material.SetFloat("_Distance", 0f);
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x0003EB54 File Offset: 0x0003CF54
	private void Update()
	{
		if (this.splashTriggerTime != -1f)
		{
			this.splashTriggerTime += Time.deltaTime;
		}
		if (this.dropTriggerTime != -1f)
		{
			this.dropTriggerTime += Time.deltaTime;
		}
		if (this.dropped)
		{
			Vector2 velocity = base.body.velocity;
			velocity.y = Mathf.Min(0f, velocity.y);
			base.body.velocity = velocity;
			if (base.transform.position.y < this.pot.transform.position.y)
			{
				base.GetComponent<SpringJoint2D>().enabled = false;
			}
		}
		this.CheckColliderBounds(base.GetComponent<Collider2D>());
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x0003EC28 File Offset: 0x0003D028
	protected override void MouseDowned()
	{
		base.MouseDowned();
		base.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x0003EC3C File Offset: 0x0003D03C
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.splashTriggerTime = 0f;
		}
		else if (other.tag == "SuccessCollider")
		{
			this.dropTriggerTime = 0f;
		}
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x0003EC90 File Offset: 0x0003D090
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.splashTriggerTime = -1f;
		}
		else if (other.tag == "SuccessCollider")
		{
			this.dropTriggerTime = -1f;
		}
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x0003ECE4 File Offset: 0x0003D0E4
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "FailCollider" && !this.splashed && this.splashTriggerTime > this.waitBeforeSubmerging)
		{
			this.pot.IncreaseEmission();
			this.pot.Splash();
			Audio.self.playOneShot("16de15d2-ad0b-41bf-97c6-f46bf76c4ccf", 1f);
			this.splashed = true;
		}
		else if (other.tag == "SuccessCollider" && !this.dropped && this.dropTriggerTime > this.waitBeforeSubmerging)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_CupMonster>().throwItem(this.type);
			this.OnMouseUp();
			Vector2 velocity = base.body.velocity;
			base.body.velocity = new Vector2(velocity.x, Mathf.Clamp(velocity.y, velocity.y, 0f));
			this.allowThrow = false;
			this.dragEnabled = false;
			this.dropped = true;
			base.GetComponent<SpringJoint2D>().enabled = true;
			base.GetComponent<SpringJoint2D>().distance = 0.005f;
			base.GetComponent<PhysicsSound>().enable = false;
			int num = 0;
			int num2 = 0;
			foreach (CupMonsterCup_Draggable cupMonsterCup_Draggable in this.GetComponentsInPuzzleStats(true))
			{
				num++;
				if (cupMonsterCup_Draggable.dropped)
				{
					num2++;
				}
			}
			if (num2 == num)
			{
				this.cup.GetComponent<CupMonsterCup_Cup>().Arise();
				Global.self.currPuzzle.GetComponent<AudioVoice_CupMonster>().finish();
			}
		}
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x0003EE8C File Offset: 0x0003D28C
	private void CheckColliderBounds(Collider2D coll)
	{
		if (!this.dropped || !this.splashed)
		{
			return;
		}
		float cauldronTop = -1.48f;
		if (coll is PolygonCollider2D)
		{
			PolygonCollider2D polygonCollider2D = (PolygonCollider2D)coll;
			if ((from x in polygonCollider2D.points
			where x.y > cauldronTop
			select x).Count<Vector2>() == 0)
			{
				base.gameObject.SetActive(false);
			}
		}
		else if (coll.bounds.max.y < cauldronTop)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040012F0 RID: 4848
	public CupMonsterItems type;

	// Token: 0x040012F1 RID: 4849
	public Transform cup;

	// Token: 0x040012F2 RID: 4850
	public float minimalLine;

	// Token: 0x040012F3 RID: 4851
	public float waitBeforeSubmerging = 0.2f;

	// Token: 0x040012F4 RID: 4852
	private CupMonsterCup_Pot pot;

	// Token: 0x040012F5 RID: 4853
	private SpriteRenderer rend;

	// Token: 0x040012F6 RID: 4854
	private float splashTriggerTime = -1f;

	// Token: 0x040012F7 RID: 4855
	private float dropTriggerTime = -1f;

	// Token: 0x040012F8 RID: 4856
	private bool dropped;

	// Token: 0x040012F9 RID: 4857
	private bool splashed;
}
