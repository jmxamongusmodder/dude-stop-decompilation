using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class CupExamGood_Hat : Draggable
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600005A RID: 90 RVA: 0x0000599D File Offset: 0x00003B9D
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

	// Token: 0x0600005B RID: 91 RVA: 0x000059C8 File Offset: 0x00003BC8
	private void Awake()
	{
		this.sprite = base.GetComponentInChildren<SpriteRenderer>();
		this.coll = base.GetComponent<Collider2D>();
		this.sound = base.GetComponent<PhysicsSound>();
		this.minX = Camera.main.ViewportToWorldPoint(new Vector3(this.minX, 0f)).x;
		this.maxX = Camera.main.ViewportToWorldPoint(new Vector3(this.maxX, 0f)).x;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00005A49 File Offset: 0x00003C49
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00005A5C File Offset: 0x00003C5C
	private void Update()
	{
		if (this.topColl.bounds.size != Vector3.zero)
		{
			this.topBounds = this.topColl.bounds;
		}
		Vector2 v = base.transform.localPosition;
		if (v.x < this.minX || v.x > this.maxX)
		{
			base.body.velocity = new Vector2(0f, base.body.velocity.y);
			v.x = Mathf.Clamp(v.x, this.minX, this.maxX);
			base.transform.localPosition = v;
		}
		bool flag = GeometryUtility.TestPlanesAABB(this.planes, this.sprite.bounds);
		this.sound.muteHit = !flag;
		if (this.touched && !this.activated && !flag)
		{
			this.GetComponentInPuzzleStats<CupExamGood_Controller>().nextStep = true;
			this.activated = true;
		}
		if (this.jumpsByItself && this.jumpingCoroutine == null)
		{
			this.jumpingCoroutine = base.StartCoroutine(this.JumpingCoroutine());
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00005BA8 File Offset: 0x00003DA8
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.coll.bounds.Intersects(this.topBounds))
		{
			return;
		}
		this.touched = true;
		base.OnMouseDown();
		this.topColl.enabled = true;
		if (this.jumpingCoroutine != null)
		{
			base.StopCoroutine(this.jumpingCoroutine);
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00005C10 File Offset: 0x00003E10
	public override void OnMouseUp()
	{
		if (!base.enabled || !this.dragged)
		{
			return;
		}
		base.OnMouseUp();
		this.topColl.enabled = false;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00005C3C File Offset: 0x00003E3C
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.offset.x == 0f && other.collider.offset.y < 0f)
		{
			this.touched = false;
			this.jumping = false;
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00005C94 File Offset: 0x00003E94
	private IEnumerator JumpingCoroutine()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.jumpTimer);
			float randVal = UnityEngine.Random.value * this.jumpRandomTimer;
			yield return new WaitForSeconds(randVal);
			base.body.AddForce(Vector2.up * this.jumpForce * base.body.mass);
			int sign = (UnityEngine.Random.value >= 0.5f) ? -1 : 1;
			base.body.AddTorque(this.jumpTorque * (float)sign);
			this.jumping = true;
			while (this.jumping)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		yield break;
	}

	// Token: 0x040000CF RID: 207
	public Transform topCollider;

	// Token: 0x040000D0 RID: 208
	public bool jumpsByItself;

	// Token: 0x040000D1 RID: 209
	public float jumpTimer;

	// Token: 0x040000D2 RID: 210
	public float jumpRandomTimer = 1f;

	// Token: 0x040000D3 RID: 211
	public float jumpForce;

	// Token: 0x040000D4 RID: 212
	public float jumpTorque;

	// Token: 0x040000D5 RID: 213
	private bool jumping;

	// Token: 0x040000D6 RID: 214
	private Coroutine jumpingCoroutine;

	// Token: 0x040000D7 RID: 215
	private Collider2D _topColl;

	// Token: 0x040000D8 RID: 216
	private SpriteRenderer sprite;

	// Token: 0x040000D9 RID: 217
	private Collider2D coll;

	// Token: 0x040000DA RID: 218
	private Bounds topBounds;

	// Token: 0x040000DB RID: 219
	private float minX = 0.05f;

	// Token: 0x040000DC RID: 220
	private float maxX = 0.95f;

	// Token: 0x040000DD RID: 221
	private Plane[] planes;

	// Token: 0x040000DE RID: 222
	private bool activated;

	// Token: 0x040000DF RID: 223
	private bool touched;

	// Token: 0x040000E0 RID: 224
	private PhysicsSound sound;
}
