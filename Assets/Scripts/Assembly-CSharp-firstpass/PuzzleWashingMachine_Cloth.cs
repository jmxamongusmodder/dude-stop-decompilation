using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public class PuzzleWashingMachine_Cloth : Draggable
{
	// Token: 0x06001D03 RID: 7427 RVA: 0x0007E3C0 File Offset: 0x0007C7C0
	private void Start()
	{
		this.parent = base.transform.parent;
		this.child = base.transform.GetChild(0);
		this.sprite = base.GetComponent<SpriteRenderer>();
		this.thisCollider = base.GetComponent<Collider2D>();
		base.snapEnabled = false;
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x0007E410 File Offset: 0x0007C810
	private void Update()
	{
		if (!this.dragged && this.insideMachine && Vector2.Distance(base.transform.position, this.spinner.position) > this.allowedDistance)
		{
			base.transform.position = this.spinner.position;
		}
	}

	// Token: 0x06001D05 RID: 7429 RVA: 0x0007E479 File Offset: 0x0007C879
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!this.dragged || !collision.isTrigger || collision.tag != "SuccessCollider" || !base.snapEnabled)
		{
			return;
		}
		this.insideMachine = true;
	}

	// Token: 0x06001D06 RID: 7430 RVA: 0x0007E4B9 File Offset: 0x0007C8B9
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!this.dragged || !collision.isTrigger || collision.tag != "SuccessCollider" || !base.snapEnabled)
		{
			return;
		}
		this.insideMachine = false;
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x0007E4F9 File Offset: 0x0007C8F9
	public override void OnMouseDown()
	{
		if (!this.dragEnabled || !base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.PickedUp(true);
	}

	// Token: 0x06001D08 RID: 7432 RVA: 0x0007E520 File Offset: 0x0007C920
	public override void OnMouseUp()
	{
		if (!this.dragEnabled || !base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		this.PickedUp(false);
		if (this.insideMachine)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Front");
			base.transform.SetParent(this.spinner);
			base.GetComponent<Renderer>().sortingOrder = -1;
		}
		else
		{
			base.gameObject.layer = LayerMask.NameToLayer("Back");
			base.transform.SetParent(this.parent);
			base.GetComponent<Renderer>().sortingOrder = 10;
		}
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x0007E5C6 File Offset: 0x0007C9C6
	private void PickedUp(bool status)
	{
		this.limit.limit = status;
		this.child.gameObject.SetActive(status);
		this.sprite.enabled = !status;
		this.thisCollider.enabled = !status;
	}

	// Token: 0x04001BA2 RID: 7074
	public Transform spinner;

	// Token: 0x04001BA3 RID: 7075
	public bool isRed;

	// Token: 0x04001BA4 RID: 7076
	public float allowedDistance = 1f;

	// Token: 0x04001BA5 RID: 7077
	private Transform parent;

	// Token: 0x04001BA6 RID: 7078
	private Transform child;

	// Token: 0x04001BA7 RID: 7079
	private Collider2D thisCollider;

	// Token: 0x04001BA8 RID: 7080
	private SpriteRenderer sprite;

	// Token: 0x04001BA9 RID: 7081
	private bool insideMachine;
}
