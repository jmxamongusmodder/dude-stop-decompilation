using System;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class GlobalCollider : MonoBehaviour
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x0600168B RID: 5771 RVA: 0x000488B6 File Offset: 0x00046CB6
	private float width
	{
		get
		{
			return this.screen.x;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600168C RID: 5772 RVA: 0x000488C3 File Offset: 0x00046CC3
	private float height
	{
		get
		{
			return this.screen.y;
		}
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x000488D0 File Offset: 0x00046CD0
	private void OnDrawGizmos()
	{
		this.DrawColliderGizmo(this.top, true, false);
		this.DrawColliderGizmo(this.bottom, true, true);
		this.DrawColliderGizmo(this.left, false, true);
		this.DrawColliderGizmo(this.right, false, false);
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x0004890A File Offset: 0x00046D0A
	private void SetColor(GlobalCollider.Collider coll)
	{
		if (coll.type == GlobalCollider.ColliderType.None || coll.type == GlobalCollider.ColliderType.Both)
		{
			return;
		}
		Gizmos.color = ((coll.type != GlobalCollider.ColliderType.Screen) ? Color.cyan : Color.magenta);
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x00048944 File Offset: 0x00046D44
	private void DrawColliderGizmo(GlobalCollider.Collider coll, bool vertical, bool negative)
	{
		this.SetColor(coll);
		if (coll.type != GlobalCollider.ColliderType.None)
		{
			if (coll.type == GlobalCollider.ColliderType.Screen)
			{
				float d = (!negative) ? (1f - coll.position) : coll.position;
				Vector2 v = (!vertical) ? (Vector2.right * d) : (Vector2.up * d);
				v = Camera.main.ViewportToWorldPoint(v);
				if (vertical)
				{
					GizmosExtension.DrawHorizontalLine(v.y, -10f, 10f);
				}
				else
				{
					GizmosExtension.DrawVerticalLine(v.x);
				}
			}
			else if (vertical)
			{
				GizmosExtension.DrawHorizontalLine(coll.position, -10f, 10f);
			}
			else
			{
				GizmosExtension.DrawVerticalLine(coll.position);
			}
		}
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x00048A28 File Offset: 0x00046E28
	private void Awake()
	{
		base.transform.position = Vector3.forward * base.transform.position.z;
		this.screen = this.GetScreenData();
		this.ProcessCollider(this.top, true, false);
		this.ProcessCollider(this.bottom, true, true);
		this.ProcessCollider(this.left, false, true);
		this.ProcessCollider(this.right, false, false);
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x00048AA4 File Offset: 0x00046EA4
	private void ProcessCollider(GlobalCollider.Collider coll, bool vertical, bool negative)
	{
		if (coll.type == GlobalCollider.ColliderType.None)
		{
			return;
		}
		Vector2 vector = this.screen;
		Vector2 zero = Vector2.zero;
		if (coll.type == GlobalCollider.ColliderType.Point)
		{
			if (vertical)
			{
				vector.x += coll.height;
				vector.y = ((!negative) ? (vector.y / 2f - coll.position) : (vector.y / 2f + coll.position));
				zero.x = 0f;
				zero.y = ((!negative) ? ((this.height - vector.y) / 2f) : (-(this.height - vector.y) / 2f));
			}
			else
			{
				vector.y += coll.height;
				vector.x = ((!negative) ? (vector.x / 2f - coll.position) : (vector.x / 2f + coll.position));
				zero.y = 0f;
				zero.x = ((!negative) ? ((this.width - vector.x) / 2f) : (-(this.width - vector.x) / 2f));
			}
		}
		else if (coll.type == GlobalCollider.ColliderType.Screen)
		{
			if (vertical)
			{
				vector.y *= coll.position;
				vector.y += coll.width;
				vector.x += coll.height;
				zero.x = 0f;
				zero.y = ((!negative) ? ((this.height - vector.y) / 2f) : (-(this.height - vector.y) / 2f));
				zero.y += ((!negative) ? coll.width : (-coll.width));
			}
			else
			{
				vector.x *= coll.position;
				vector.x += coll.width;
				vector.y += coll.height;
				zero.y = 0f;
				zero.x = ((!negative) ? ((this.width - vector.x) / 2f) : (-(this.width - vector.x) / 2f));
				zero.x += ((!negative) ? coll.width : (-coll.width));
			}
		}
		if (vector != Vector2.zero)
		{
			BoxCollider2D boxCollider2D = base.gameObject.AddComponent<BoxCollider2D>();
			boxCollider2D.size = vector;
			boxCollider2D.offset = zero;
			coll.collider = boxCollider2D;
		}
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x00048DAC File Offset: 0x000471AC
	private Vector2 GetScreenData()
	{
		Vector3 b = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
		Vector3 a = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
		float y = Mathf.Abs((a - b).y);
		float x = Mathf.Abs((a - b).x);
		return new Vector2(x, y);
	}

	// Token: 0x04001457 RID: 5207
	public GlobalCollider.Collider top = new GlobalCollider.Collider();

	// Token: 0x04001458 RID: 5208
	public GlobalCollider.Collider right = new GlobalCollider.Collider();

	// Token: 0x04001459 RID: 5209
	public GlobalCollider.Collider bottom = new GlobalCollider.Collider();

	// Token: 0x0400145A RID: 5210
	public GlobalCollider.Collider left = new GlobalCollider.Collider();

	// Token: 0x0400145B RID: 5211
	private Vector2 screen = Vector2.zero;

	// Token: 0x0200038A RID: 906
	public enum ColliderType
	{
		// Token: 0x0400145D RID: 5213
		None,
		// Token: 0x0400145E RID: 5214
		Screen,
		// Token: 0x0400145F RID: 5215
		Point,
		// Token: 0x04001460 RID: 5216
		Both
	}

	// Token: 0x0200038B RID: 907
	[Serializable]
	public class Collider
	{
		// Token: 0x04001461 RID: 5217
		public GlobalCollider.ColliderType type = GlobalCollider.ColliderType.Screen;

		// Token: 0x04001462 RID: 5218
		public float position = 0.05f;

		// Token: 0x04001463 RID: 5219
		public float width = 1f;

		// Token: 0x04001464 RID: 5220
		public float height = 1.5f;

		// Token: 0x04001465 RID: 5221
		public BoxCollider2D collider;
	}
}
