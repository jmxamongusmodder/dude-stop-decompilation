using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000448 RID: 1096
public class PuzzleShapeInBox_Shape : Draggable
{
	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00075F7E File Offset: 0x0007437E
	// (set) Token: 0x06001C0B RID: 7179 RVA: 0x00075F86 File Offset: 0x00074386
	public bool insideTheBox { get; private set; }

	// Token: 0x06001C0C RID: 7180 RVA: 0x00075F8F File Offset: 0x0007438F
	private void Start()
	{
		this.otherShape = base.transform.parent.GetComponentsInChildren<PuzzleShapeInBox_Shape>().First((PuzzleShapeInBox_Shape x) => x != this);
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x00075FB8 File Offset: 0x000743B8
	private void Update()
	{
		if (!this.dragged && this.insideTheBox && !this.otherShape.insideTheBox)
		{
			base.enabled = false;
		}
		if (Mathf.Abs(base.transform.position.x - this.hole.position.x) <= this.dist)
		{
			if (base.transform.position.y > this.hole.position.y)
			{
				base.gameObject.layer = LayerMask.NameToLayer("Default");
				this.throughTheLid = false;
			}
			else
			{
				base.gameObject.layer = LayerMask.NameToLayer("Back");
				this.throughTheLid = true;
			}
		}
		else if (this.dragged)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Default");
			this.throughTheLid = false;
		}
		else
		{
			base.gameObject.layer = LayerMask.NameToLayer("Default");
		}
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x000760D8 File Offset: 0x000744D8
	protected override Vector3 ProcessMousePosition(Vector3 mouse, Vector3 delta)
	{
		mouse -= delta;
		if (Mathf.Abs(base.transform.position.x - this.hole.position.x) <= this.dist && base.transform.position.y < this.hole.position.y)
		{
			mouse.x = this.hole.position.x;
		}
		mouse += delta;
		return mouse;
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x00076175 File Offset: 0x00074575
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.insideTheBox = true;
		}
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x00076193 File Offset: 0x00074593
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.insideTheBox = false;
		}
	}

	// Token: 0x04001A65 RID: 6757
	public Transform hole;

	// Token: 0x04001A66 RID: 6758
	public float dist;

	// Token: 0x04001A67 RID: 6759
	private PuzzleShapeInBox_Shape otherShape;

	// Token: 0x04001A68 RID: 6760
	public bool throughTheLid;
}
