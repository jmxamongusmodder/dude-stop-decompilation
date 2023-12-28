using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class InventorySlot : MonoBehaviour
{
	// Token: 0x060016D5 RID: 5845 RVA: 0x0004A0EA File Offset: 0x000484EA
	private void Update()
	{
		this.moveContainerTransform();
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x0004A0F2 File Offset: 0x000484F2
	public void removeSlot()
	{
		if (this.item != null)
		{
			this.item.GetComponent<InventoryItem>().enabled = false;
		}
		this.item = null;
		this.removeEmptySlot();
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x0004A124 File Offset: 0x00048524
	private void OnMouseDown()
	{
		if (this.item == null || !Global.self.NoCurrentTransition || this.removeEmpty)
		{
			return;
		}
		if (InventoryControl.self.mouseOnSlotDown(base.transform, this.item))
		{
			this.item = null;
		}
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x0004A180 File Offset: 0x00048580
	private void OnMouseUp()
	{
		if (this.removeEmpty)
		{
			return;
		}
		BoxCollider2D component = base.GetComponent<BoxCollider2D>();
		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		point.z = base.transform.position.z;
		if (component.bounds.Contains(point))
		{
			InventoryControl.self.simulateMouseUp(base.transform);
		}
		else
		{
			InventoryControl.self.simulateMouseUp(null);
		}
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x0004A200 File Offset: 0x00048600
	private void OnMouseEnter()
	{
		if (this.removeEmpty)
		{
			return;
		}
		if (this.item == null)
		{
			InventoryControl.self.mouseOver(base.transform);
		}
		this.destPos.y = 0.1f;
		this.moveContainer = true;
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x0004A251 File Offset: 0x00048651
	private void OnMouseExit()
	{
		if (this.removeEmpty)
		{
			return;
		}
		InventoryControl.self.mouseOver(null);
		this.destPos.y = 0f;
		this.moveContainer = true;
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x0004A281 File Offset: 0x00048681
	public void setItemToSlot(Transform item)
	{
		this.item = item;
		item.SetParent(base.transform);
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x0004A296 File Offset: 0x00048696
	public bool unremoveEmptySlot()
	{
		if (!this.removeEmpty)
		{
			return this.item == null;
		}
		this.destPos.y = 0f;
		this.removeEmpty = false;
		return true;
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x0004A2C8 File Offset: 0x000486C8
	public void removeEmptySlot()
	{
		if (this.item != null)
		{
			return;
		}
		this.destPos.y = InventoryControl.self.slotSize * -1.2f;
		this.moveContainer = true;
		this.removeEmpty = true;
		this.hideSlot = false;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x0004A318 File Offset: 0x00048718
	public void temporaryHideSlot()
	{
		if (this.item == null)
		{
			this.removeEmptySlot();
			return;
		}
		if (this.removeEmpty)
		{
			return;
		}
		this.destPos.y = InventoryControl.self.slotSize * -1.2f;
		this.moveContainer = true;
		this.removeEmpty = true;
		this.hideSlot = true;
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x0004A379 File Offset: 0x00048779
	public void showSlotBack()
	{
		if (this.item == null)
		{
			this.removeEmptySlot();
			return;
		}
		this.destPos.y = 0f;
		this.moveContainer = true;
		this.removeEmpty = false;
		this.hideSlot = false;
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x0004A3B8 File Offset: 0x000487B8
	public void showAnimation()
	{
		Vector3 localPosition = Vector3.forward * -5f;
		localPosition.x = InventoryControl.self.slotSize * (float)(InventoryControl.self.transform.childCount - 1);
		this.destPos = localPosition;
		localPosition.y = InventoryControl.self.slotSize * -1.2f;
		base.transform.localPosition = localPosition;
		this.moveContainer = true;
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x0004A42C File Offset: 0x0004882C
	private void moveContainerTransform()
	{
		if (!this.moveContainer)
		{
			return;
		}
		Vector3 vector = base.transform.localPosition;
		vector = Vector3.Lerp(vector, this.destPos, Time.deltaTime * this.moveUpSpeed);
		vector = Vector3.MoveTowards(vector, this.destPos, Time.deltaTime * 0.2f);
		base.transform.localPosition = vector;
		if (Vector3.SqrMagnitude(vector - this.destPos) < ((!this.removeEmpty) ? 0.0001f : 0.01f))
		{
			base.transform.localPosition = this.destPos;
			this.moveContainer = false;
			if (this.removeEmpty && !this.hideSlot)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				InventoryControl.self.repositionSlots();
			}
		}
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x0004A504 File Offset: 0x00048904
	public void positionSlot(int index)
	{
		Vector3 vector = this.destPos;
		vector.x = InventoryControl.self.slotSize * (float)index;
		this.destPos = vector;
		this.moveContainer = true;
	}

	// Token: 0x0400148D RID: 5261
	[Tooltip("Border around item in inventory")]
	public Transform border;

	// Token: 0x0400148E RID: 5262
	[Tooltip("How fast to move slot up from below the screen")]
	public float moveUpSpeed = 7f;

	// Token: 0x0400148F RID: 5263
	private bool moveContainer;

	// Token: 0x04001490 RID: 5264
	private bool removeEmpty;

	// Token: 0x04001491 RID: 5265
	private bool hideSlot;

	// Token: 0x04001492 RID: 5266
	private Vector3 destPos;

	// Token: 0x04001493 RID: 5267
	private Transform item;
}
