using System;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class CupExamBad_Hat : Draggable
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x0600147D RID: 5245 RVA: 0x0003747D File Offset: 0x0003587D
	private Collider2D topColl
	{
		get
		{
			if (this._topColl == null)
			{
				this._topColl = this.topCollider.GetComponent<Collider2D>();
			}
			return this._topColl;
		}
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x000374A8 File Offset: 0x000358A8
	private void Awake()
	{
		this.coll = base.GetComponent<Collider2D>();
		this.body = base.GetComponent<Rigidbody2D>();
		this.sound = base.GetComponent<PhysicsSound>();
		Vector2 vector = Camera.main.ViewportToWorldPoint(new Vector3(this.minX, 0f));
		Vector2 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(this.maxX, 0f));
		this.minX = vector.x;
		this.maxX = vector2.x;
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x00037533 File Offset: 0x00035933
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x00037548 File Offset: 0x00035948
	private void Update()
	{
		bool flag = GeometryUtility.TestPlanesAABB(this.planes, this.goodSprite.bounds);
		this.sound.muteHit = !flag;
		if (!this.changed && this.topColl.bounds.size != Vector3.zero)
		{
			this.topBounds = this.topColl.bounds;
		}
		Vector2 v = base.transform.localPosition;
		if (v.x < this.minX || v.x > this.maxX)
		{
			this.body.velocity = new Vector2(0f, this.body.velocity.y);
			v.x = Mathf.Clamp(v.x, this.minX, this.maxX);
			base.transform.localPosition = v;
		}
		if (!this.changed && !flag)
		{
			this.goodSprite.gameObject.SetActive(false);
			this.badSprite.gameObject.SetActive(true);
			this.changed = true;
			this.GetPuzzleStats().GetComponent<AudioVoice_CupExamBad>().touched = true;
		}
		if (this.changed && flag && !this.fallen)
		{
			this.fallen = true;
			this.GetPuzzleStats().GetComponent<AudioVoice_CupExamBad>().hatFalls();
		}
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x000376C4 File Offset: 0x00035AC4
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!this.changed || !base.enabled)
		{
			return;
		}
		if (other.collider.offset.x != 0f || other.collider.offset.y < 0f)
		{
		}
	}

	// Token: 0x06001482 RID: 5250 RVA: 0x00037724 File Offset: 0x00035B24
	public override void OnMouseDown()
	{
		if (this.fallen)
		{
			Global.CupAcquired(base.transform);
			base.enabled = false;
			return;
		}
		if (this.changed)
		{
			return;
		}
		if (this.coll.bounds.Intersects(this.topBounds))
		{
			return;
		}
		base.OnMouseDown();
		this.topColl.enabled = true;
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x0003778C File Offset: 0x00035B8C
	public override void OnMouseUp()
	{
		if (!this.dragged || this.changed)
		{
			return;
		}
		base.OnMouseUp();
		this.topColl.enabled = false;
	}

	// Token: 0x040011DE RID: 4574
	public Transform topCollider;

	// Token: 0x040011DF RID: 4575
	public SpriteRenderer goodSprite;

	// Token: 0x040011E0 RID: 4576
	public SpriteRenderer badSprite;

	// Token: 0x040011E1 RID: 4577
	public float waitBeforeCompletion = 3f;

	// Token: 0x040011E2 RID: 4578
	private Collider2D _topColl;

	// Token: 0x040011E3 RID: 4579
	private Collider2D coll;

	// Token: 0x040011E4 RID: 4580
	private Bounds topBounds;

	// Token: 0x040011E5 RID: 4581
	private float minX = 0.19f;

	// Token: 0x040011E6 RID: 4582
	private float maxX = 0.81f;

	// Token: 0x040011E7 RID: 4583
	private new Rigidbody2D body;

	// Token: 0x040011E8 RID: 4584
	private Plane[] planes;

	// Token: 0x040011E9 RID: 4585
	private bool changed;

	// Token: 0x040011EA RID: 4586
	private bool fallen;

	// Token: 0x040011EB RID: 4587
	private PhysicsSound sound;
}
