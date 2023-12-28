using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000395 RID: 917
public class LegoPiece : PivotDraggable
{
	// Token: 0x060016EA RID: 5866 RVA: 0x0003CC28 File Offset: 0x0003B028
	private void OnDrawGizmos()
	{
		foreach (Vector3 vector in this.GetPoints())
		{
			Gizmos.DrawLine(vector, vector + base.transform.up * this.length);
			GizmosExtension.DrawLine(vector, vector - base.transform.up * this.length, Color.magenta);
		}
		foreach (Vector3 v in this.availablePoints)
		{
			GizmosExtension.DrawPoint(v, Color.blue, 0.1f);
		}
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x0003CD20 File Offset: 0x0003B120
	public virtual void Start()
	{
		this.body = base.GetComponent<Rigidbody2D>();
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x0003CD2E File Offset: 0x0003B12E
	public override void FixedUpdate()
	{
		if (this.dragged)
		{
			this.Upd();
		}
		base.FixedUpdate();
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x0003CD48 File Offset: 0x0003B148
	public override void OnMouseDown()
	{
		if (!base.enabled || !this.dragEnabled)
		{
			return;
		}
		if (this.recheckPoints)
		{
			this.AssemblePoints(true);
			this.CheckSnapPoints();
			this.recheckPoints = false;
		}
		if (this.lockX && this.GetPoints().Count != this.GetAvailablePoints(false).Count)
		{
			return;
		}
		if (this.lockX)
		{
			this.allowThrow = true;
		}
		this.AssemblePoints(false);
		this.UnlockPimpochkas();
		this.body.isKinematic = false;
		base.OnMouseDown();
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x0003CDE4 File Offset: 0x0003B1E4
	public override void OnMouseUp()
	{
		if (!base.enabled || !this.dragEnabled)
		{
			return;
		}
		this.CheckSnapPoints();
		this.CheckLock();
		this.LockPimpochkas();
		if (this.lockX && !this.soundPlayed)
		{
			this.PlaySnapSound();
		}
		if (this.lockX)
		{
			this.allowThrow = false;
		}
		base.OnMouseUp();
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x0003CE4E File Offset: 0x0003B24E
	private void UnlockPimpochkas()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Front");
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x0003CE65 File Offset: 0x0003B265
	private void LockPimpochkas()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Default");
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x0003CE7C File Offset: 0x0003B27C
	private void CheckLock()
	{
		if (!this.lockX)
		{
			return;
		}
		this.body.isKinematic = true;
		this.body.MoveRotation(0f);
		this.body.velocity = Vector2.zero;
		this.body.angularVelocity = 0f;
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x0003CED1 File Offset: 0x0003B2D1
	protected void Upd()
	{
		this.CheckSnapPoints();
		this.CheckRotation();
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x0003CEE0 File Offset: 0x0003B2E0
	private void CheckRotation()
	{
		if (base.transform.eulerAngles.z == 0f)
		{
			return;
		}
		float angle = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, 0f, this.rotationSpeed * Time.deltaTime);
		this.body.MoveRotation(angle);
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x0003CF44 File Offset: 0x0003B344
	private void AssemblePoints(bool ignoreFirstHit = false)
	{
		this.availablePoints.Clear();
		foreach (LegoPiece legoPiece in from x in this.GetComponentsInPuzzleStats(false)
		where x != this
		select x)
		{
			this.availablePoints.AddRange(legoPiece.GetAvailablePoints(ignoreFirstHit));
		}
		this.availablePoints.AddRange(this.occupiedPoints);
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x0003CFD8 File Offset: 0x0003B3D8
	protected List<Vector3> GetPoints()
	{
		List<Vector3> list = new List<Vector3>();
		float num = this.distanceFromCenter;
		for (int i = 0; i < this.points; i++)
		{
			int num2 = (i % 2 != 0) ? 1 : -1;
			list.Add(base.transform.position + base.transform.right * num * (float)num2);
			if (i % 2 == 1)
			{
				num += this.distanceBetweenRays;
			}
		}
		return list;
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x0003D05C File Offset: 0x0003B45C
	private List<Vector3> GetAvailablePoints(bool ignoreFirstHit = false)
	{
		List<Vector3> list = new List<Vector3>();
		if ((this.body != null && !this.body.isKinematic) || !this.lockX)
		{
			return list;
		}
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		int layerMask2 = 1 << LayerMask.NameToLayer("Individual");
		base.gameObject.layer = LayerMask.NameToLayer("Individual");
		foreach (Vector3 vector in this.GetPoints())
		{
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, base.transform.up, this.length, layerMask);
			if (ignoreFirstHit || !(raycastHit2D.collider != null) || !raycastHit2D.transform.GetComponent<Rigidbody2D>().isKinematic)
			{
				list.Add(Physics2D.Raycast(vector + base.transform.up * this.length, -base.transform.up, this.length * 1.1f, layerMask2).point);
			}
		}
		base.gameObject.layer = LayerMask.NameToLayer("Default");
		return list;
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x0003D1E4 File Offset: 0x0003B5E4
	private void CheckSnapPoints()
	{
		if (Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 0f)) > this.checkAngle)
		{
			this.lockX = false;
			return;
		}
		bool flag = false;
		int layer = base.gameObject.layer;
		base.gameObject.layer = LayerMask.NameToLayer("Individual");
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		int count = this.occupiedPoints.Count;
		this.occupiedPoints.Clear();
		foreach (Vector3 v in this.GetPoints())
		{
			RaycastHit2D raycastHit2D = Physics2D.Raycast(v, -base.transform.up, this.length, layerMask);
			if (!(raycastHit2D.collider == null))
			{
				float num = v.x - base.transform.position.x;
				foreach (Vector3 vector in this.availablePoints)
				{
					if (Vector3.Distance(vector, raycastHit2D.point) < this.lockDist)
					{
						if (this.occupiedPoints.Count<Vector3>() == 0)
						{
							base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
							base.transform.position = new Vector3(vector.x - num, vector.y + this.GetHeight());
							flag |= (raycastHit2D.distance < this.soundDistance);
						}
						this.occupiedPoints.Add(vector);
					}
				}
			}
		}
		if (!this.soundPlayed && flag)
		{
			this.PlaySnapSound();
		}
		else if (this.soundPlayed && !flag)
		{
			base.GetComponent<PhysicsSound>().enable = true;
			this.soundPlayed = false;
		}
		this.lockX = (this.occupiedPoints.Count<Vector3>() > 0);
		base.gameObject.layer = layer;
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x0003D46C File Offset: 0x0003B86C
	private void PlaySnapSound()
	{
		if (!this.recheckPoints)
		{
			Audio.self.playOneShot("87fd6f45-3a3a-4011-b57f-eb389b183f24", 1f);
		}
		base.GetComponent<PhysicsSound>().enable = false;
		this.soundPlayed = true;
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x0003D4A4 File Offset: 0x0003B8A4
	protected virtual float GetHeight()
	{
		return base.GetComponent<SpriteRenderer>().bounds.extents.y;
	}

	// Token: 0x04001494 RID: 5268
	[Header("Lego stuff")]
	public bool recheckPoints;

	// Token: 0x04001495 RID: 5269
	public int points;

	// Token: 0x04001496 RID: 5270
	public float distanceFromCenter;

	// Token: 0x04001497 RID: 5271
	public float distanceBetweenRays;

	// Token: 0x04001498 RID: 5272
	public float length;

	// Token: 0x04001499 RID: 5273
	public float lockDist;

	// Token: 0x0400149A RID: 5274
	public float soundDistance = 0.175f;

	// Token: 0x0400149B RID: 5275
	public float rotationSpeed;

	// Token: 0x0400149C RID: 5276
	public float checkAngle;

	// Token: 0x0400149D RID: 5277
	public Collider2D pimpochka;

	// Token: 0x0400149E RID: 5278
	private List<Vector3> availablePoints = new List<Vector3>();

	// Token: 0x0400149F RID: 5279
	private List<Vector3> occupiedPoints = new List<Vector3>();

	// Token: 0x040014A0 RID: 5280
	private new Rigidbody2D body;

	// Token: 0x040014A1 RID: 5281
	private bool soundPlayed;
}
