using System;
using System.Linq;
using UnityEngine;

// Token: 0x020003B8 RID: 952
public class Old_PuzzleShapeInBox_Shape : Draggable
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0004FEFC File Offset: 0x0004E2FC
	// (set) Token: 0x060017B4 RID: 6068 RVA: 0x0004FF04 File Offset: 0x0004E304
	public bool insideTheBox { get; private set; }

	// Token: 0x060017B5 RID: 6069 RVA: 0x0004FF0D File Offset: 0x0004E30D
	private void Start()
	{
		this.otherShape = base.transform.parent.GetComponentsInChildren<Old_PuzzleShapeInBox_Shape>().First((Old_PuzzleShapeInBox_Shape x) => x != this);
	}

	// Token: 0x060017B6 RID: 6070 RVA: 0x0004FF38 File Offset: 0x0004E338
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

	// Token: 0x060017B7 RID: 6071 RVA: 0x00050058 File Offset: 0x0004E458
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

	// Token: 0x060017B8 RID: 6072 RVA: 0x000500F5 File Offset: 0x0004E4F5
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.insideTheBox = true;
		}
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x00050113 File Offset: 0x0004E513
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.insideTheBox = false;
		}
	}

	// Token: 0x0400157B RID: 5499
	public Transform hole;

	// Token: 0x0400157C RID: 5500
	public float dist;

	// Token: 0x0400157D RID: 5501
	private Old_PuzzleShapeInBox_Shape otherShape;

	// Token: 0x0400157E RID: 5502
	public bool throughTheLid;
}
