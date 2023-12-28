using System;
using UnityEngine;

// Token: 0x020003B6 RID: 950
public class Old_PuzzlePainting : MonoBehaviour
{
	// Token: 0x060017A4 RID: 6052 RVA: 0x0004F938 File Offset: 0x0004DD38
	private void OnDrawGizmos()
	{
		if (!base.enabled)
		{
			return;
		}
		Gizmos.color = Color.red;
		float f = base.transform.eulerAngles.z * 0.017453292f;
		Vector3 a = new Vector2(Mathf.Sin(f), -Mathf.Cos(f));
		Gizmos.DrawLine(base.transform.position, base.transform.position + a * 5f);
		Gizmos.color = Color.yellow;
		f = this.angleFrom * 0.017453292f;
		a = new Vector2(Mathf.Sin(f), -Mathf.Cos(f));
		Gizmos.DrawLine(base.transform.position, a * 4f);
		a.x *= -1f;
		Gizmos.DrawLine(base.transform.position, a * 4f);
		f = this.angleTo * 0.017453292f;
		a = new Vector2(Mathf.Sin(f), -Mathf.Cos(f));
		Gizmos.DrawLine(base.transform.position, a * 4f);
		a.x *= -1f;
		Gizmos.DrawLine(base.transform.position, a * 4f);
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x0004FAA0 File Offset: 0x0004DEA0
	private void OnDisable()
	{
		if (this.body == null)
		{
			return;
		}
		this.body.isKinematic = true;
		this.body.velocity = Vector2.zero;
		this.body.angularVelocity = 0f;
		this.body.gravityScale = 0f;
		this.joint.enabled = false;
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x0004FB07 File Offset: 0x0004DF07
	private void Start()
	{
		this.UpdateReferences();
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x0004FB10 File Offset: 0x0004DF10
	private void Update()
	{
		if (!this.joint)
		{
			this.UpdateReferences();
		}
		if (this.dragged)
		{
			this.body.angularVelocity = 0f;
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.localPosition);
			Vector2 vector2 = new Vector2(mousePosition.x - vector.x, mousePosition.y - vector.y);
			float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
			num += 90f;
			if (this.delta == -1f)
			{
				this.delta = num - base.transform.eulerAngles.z;
			}
			else
			{
				num -= this.delta;
				this.body.MoveRotation(num);
				float num2 = Mathf.Abs(Mathf.DeltaAngle(num, 0f));
				if (num2 > this.angleFrom && num2 < this.angleTo && Mathf.Abs(this.body.velocity.x) < 2f)
				{
					this.success = true;
				}
				else
				{
					this.success = false;
				}
			}
		}
		else if (this.success)
		{
			Global.LevelCompleted(0f, true);
		}
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x0004FC74 File Offset: 0x0004E074
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!this.joint.enabled)
		{
			this.body.isKinematic = false;
			this.joint.enabled = true;
		}
		this.dragged = true;
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x0004FCB1 File Offset: 0x0004E0B1
	private void OnMouseUp()
	{
		this.dragged = false;
		this.delta = -1f;
	}

	// Token: 0x060017AA RID: 6058 RVA: 0x0004FCC5 File Offset: 0x0004E0C5
	private void UpdateReferences()
	{
		this.body = base.GetComponent<Rigidbody2D>();
		this.joint = base.GetComponent<HingeJoint2D>();
	}

	// Token: 0x0400156D RID: 5485
	public float angleFrom = 10f;

	// Token: 0x0400156E RID: 5486
	public float angleTo = 20f;

	// Token: 0x0400156F RID: 5487
	private bool dragged;

	// Token: 0x04001570 RID: 5488
	private bool success;

	// Token: 0x04001571 RID: 5489
	private float delta = -1f;

	// Token: 0x04001572 RID: 5490
	private float timer;

	// Token: 0x04001573 RID: 5491
	private HingeJoint2D joint;

	// Token: 0x04001574 RID: 5492
	private Rigidbody2D body;
}
