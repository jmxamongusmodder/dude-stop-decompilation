using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200038F RID: 911
public class InventoryControl : MonoBehaviour
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00048FBD File Offset: 0x000473BD
	public static InventoryControl self
	{
		get
		{
			if (InventoryControl._self == null)
			{
				InventoryControl._self = GameObject.FindGameObjectWithTag("InventoryControl").GetComponent<InventoryControl>();
			}
			return InventoryControl._self;
		}
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x00048FE8 File Offset: 0x000473E8
	private void Start()
	{
		this.slotPref.gameObject.SetActive(false);
		this.slotPref.SetParent(null);
		this.slotPref.hideFlags = HideFlags.HideInHierarchy;
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x00049013 File Offset: 0x00047413
	private void Update()
	{
		this.moveParentTransform();
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x0004901B File Offset: 0x0004741B
	public void turnOffMouseInteractions()
	{
		this.mouseInteractionsAllowed = false;
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x00049024 File Offset: 0x00047424
	public void turnOnMouseInteractions()
	{
		this.mouseInteractionsAllowed = true;
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x00049030 File Offset: 0x00047430
	public void removeInventory()
	{
		this.hidden = false;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<InventorySlot>().removeSlot();
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000490A0 File Offset: 0x000474A0
	public void hideInventory()
	{
		this.hidden = true;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<InventorySlot>().temporaryHideSlot();
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x00049110 File Offset: 0x00047510
	public void showInventory()
	{
		this.hidden = false;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<InventorySlot>().showSlotBack();
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.positionInventory();
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x00049188 File Offset: 0x00047588
	public bool mouseOnSlotDown(Transform slot, Transform item)
	{
		if (!this.mouseInteractionsAllowed)
		{
			return false;
		}
		bool flag = item.GetComponent<InventoryItem>().mouseDownInInventory();
		if (flag)
		{
			this.mouseItem = item;
		}
		return flag;
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x000491BC File Offset: 0x000475BC
	public void simulateMouseUp(Transform slot)
	{
		if (this.mouseItem == null)
		{
			return;
		}
		this.mouseOverSlot = slot;
		this.mouseItem.GetComponent<InventoryItem>().onMouseUp();
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x000491E8 File Offset: 0x000475E8
	public void moveBackToInventory(Transform item)
	{
		Transform transform = this.addEmptySlot();
		transform.GetComponent<InventorySlot>().setItemToSlot(item);
		this.mouseItem = null;
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x00049210 File Offset: 0x00047610
	public bool mouseOnItemUp()
	{
		if (this.mouseOverSlot != null && this.mouseItem != null)
		{
			this.mouseOverSlot.GetComponent<InventorySlot>().setItemToSlot(this.mouseItem);
			this.mouseItem = null;
			return true;
		}
		this.mouseOnItemDown(null);
		return false;
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x00049268 File Offset: 0x00047668
	public void mouseOver(Transform slot)
	{
		this.mouseOverSlot = slot;
		if (this.mouseItem != null)
		{
			if (this.mouseOverSlot != null)
			{
				this.mouseItem.GetComponent<InventoryItem>().itemEnterInventory();
			}
			else
			{
				this.mouseItem.GetComponent<InventoryItem>().itemExitInventory();
			}
		}
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000492C3 File Offset: 0x000476C3
	public void mouseOnItemDown(Transform item)
	{
		this.mouseItem = item;
		if (item != null)
		{
			this.addEmptySlot();
		}
		else
		{
			this.removeEmptySlots();
		}
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000492EC File Offset: 0x000476EC
	private void moveParentTransform()
	{
		if (!this.moveParent)
		{
			return;
		}
		Vector3 position = base.transform.position;
		position.x = Mathf.Lerp(position.x, this.destX, Time.deltaTime * this.moveLeftSpeed);
		base.transform.position = position;
		if (Mathf.Abs(position.x - this.destX) < 0.05f)
		{
			this.moveParent = false;
		}
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x00049366 File Offset: 0x00047766
	public void repositionSlots()
	{
		base.StartCoroutine(this.repositionSlotsDelay());
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x00049378 File Offset: 0x00047778
	private IEnumerator repositionSlotsDelay()
	{
		yield return null;
		int ind = 0;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				InventorySlot component = transform.GetComponent<InventorySlot>();
				int index;
				ind = (index = ind) + 1;
				component.positionSlot(index);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.positionInventory();
		yield break;
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x00049394 File Offset: 0x00047794
	public void removeEmptySlots()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<InventorySlot>().removeEmptySlot();
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x00049400 File Offset: 0x00047800
	public Transform addEmptySlot()
	{
		Transform transform = null;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform2 = (Transform)obj;
				if (transform2.GetComponent<InventorySlot>().unremoveEmptySlot())
				{
					transform = transform2;
					break;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		if (transform == null)
		{
			transform = UnityEngine.Object.Instantiate<Transform>(this.slotPref);
			transform.SetParent(base.transform);
			transform.localScale = Vector3.one;
			transform.gameObject.SetActive(true);
			transform.GetComponent<InventorySlot>().showAnimation();
		}
		this.positionInventory();
		if (base.transform.childCount == 1)
		{
			Vector3 position = base.transform.position;
			position.x = this.destX;
			base.transform.position = position;
		}
		return transform;
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x000494FC File Offset: 0x000478FC
	private void positionInventory()
	{
		this.destX = (float)(base.transform.childCount - 1) * this.slotSize * -0.5f;
		this.moveParent = true;
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x00049528 File Offset: 0x00047928
	public bool isItemInInventory<T>()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				IEnumerator enumerator2 = transform.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						Transform transform2 = (Transform)obj2;
						if (transform2.GetComponent(typeof(T)))
						{
							return true;
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator2 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
		return false;
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x000495F8 File Offset: 0x000479F8
	public float inventoryHighestSlot()
	{
		if (base.transform.childCount == 0 || this.hidden)
		{
			return -1f;
		}
		float num = -1f;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				num = Mathf.Max(num, transform.localPosition.y);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		return num + 1f;
	}

	// Token: 0x0400146F RID: 5231
	private static InventoryControl _self;

	// Token: 0x04001470 RID: 5232
	private bool moveParent;

	// Token: 0x04001471 RID: 5233
	private float destX;

	// Token: 0x04001472 RID: 5234
	private bool hidden;

	// Token: 0x04001473 RID: 5235
	[Tooltip("Slot transform that will be used to create new slots")]
	public Transform slotPref;

	// Token: 0x04001474 RID: 5236
	[Tooltip("Size of the slot")]
	public float slotSize = 0.7f;

	// Token: 0x04001475 RID: 5237
	[Tooltip("Speed to move left/right this parent")]
	public float moveLeftSpeed = 4f;

	// Token: 0x04001476 RID: 5238
	private Transform mouseOverSlot;

	// Token: 0x04001477 RID: 5239
	private Transform mouseItem;

	// Token: 0x04001478 RID: 5240
	private bool mouseInteractionsAllowed = true;
}
