using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000391 RID: 913
[RequireComponent(typeof(Draggable))]
public class InventoryItem : MonoBehaviour
{
	// Token: 0x060016C6 RID: 5830 RVA: 0x00049B44 File Offset: 0x00047F44
	private void Start()
	{
		this.setDraggableScripts();
		this.initialScale = base.transform.localScale;
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x00049B5D File Offset: 0x00047F5D
	private void OnEnable()
	{
		this.setDraggableScripts();
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x00049B65 File Offset: 0x00047F65
	private void Update()
	{
		this.mouseDrag();
		this.moveItemToSlot();
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x00049B74 File Offset: 0x00047F74
	private void moveItemToSlot()
	{
		if (!this.moveBackAnimation)
		{
			return;
		}
		Vector3 vector = base.transform.localPosition;
		Vector3 forward = Vector3.forward;
		vector = Vector3.Lerp(vector, forward, Time.deltaTime * this.moveBackSpeed);
		vector = Vector3.MoveTowards(vector, forward, Time.deltaTime * 0.2f);
		base.transform.localPosition = vector;
		if (Vector3.SqrMagnitude(vector - forward) < 0.001f)
		{
			this.moveBackAnimation = false;
		}
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x00049BF0 File Offset: 0x00047FF0
	public void moveBackToInventory()
	{
		InventoryControl.self.moveBackToInventory(base.transform);
		this.setSmallIcon(true);
		this.initialScale = base.transform.localScale;
		base.transform.localScale = Vector3.one * this.scale;
		this.moveBackAnimation = true;
		this.currentScript.enabled = false;
		Rigidbody2D component = base.GetComponent<Rigidbody2D>();
		if (component != null)
		{
			component.velocity = Vector2.zero;
		}
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x00049C74 File Offset: 0x00048074
	private void mouseDrag()
	{
		if (!this.dragWithMouse)
		{
			return;
		}
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		position.z = 0f;
		base.transform.position = position;
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x00049CB5 File Offset: 0x000480B5
	public void mouseDown()
	{
		InventoryControl.self.mouseOnItemDown(base.transform);
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x00049CC7 File Offset: 0x000480C7
	public virtual bool mouseDownInInventory()
	{
		this.dragWithMouse = true;
		base.transform.SetParent(Global.self.currPuzzle);
		Audio.self.playOneShot("7d78e4ac-955a-4d48-bd63-0a081ec53a86", 1f);
		return true;
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x00049CFB File Offset: 0x000480FB
	public void onMouseUp()
	{
		this.simulateClick = true;
		this.OnMouseUp();
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x00049D0C File Offset: 0x0004810C
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		if (InventoryControl.self.mouseOnItemUp() && this.dragWithMouse)
		{
			this.currentScript.enabled = false;
			this.dragWithMouse = false;
			Audio.self.playOneShot("2e2b0b58-194c-4f8c-91ec-b45b7e143a40", 1f);
			this.currentScript.DroppedInInventory();
			base.transform.localPosition = Vector3.forward;
		}
		else if (this.simulateClick)
		{
			this.currentScript.OnMouseUp();
			this.simulateClick = false;
		}
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x00049DA4 File Offset: 0x000481A4
	public void itemEnterInventory()
	{
		if (this.itemSprite == null || this.dragWithMouse)
		{
			return;
		}
		this.currentScript.EnterInventory();
		this.setSmallIcon(true);
		this.dragWithMouse = true;
		this.currentScript.enabled = false;
		this.initialScale = base.transform.localScale;
		base.transform.localScale = Vector3.one * this.scale;
		Rigidbody2D component = base.GetComponent<Rigidbody2D>();
		if (component != null)
		{
			this.previouslySimulated = new bool?(component.simulated);
			component.simulated = false;
			component.velocity = Vector2.zero;
		}
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x00049E58 File Offset: 0x00048258
	public void itemExitInventory()
	{
		if (this.itemSprite == null)
		{
			return;
		}
		this.setSmallIcon(false);
		this.dragWithMouse = false;
		base.transform.localScale = this.initialScale;
		this.moveBackAnimation = false;
		this.setDraggableScripts();
		this.currentScript.ExitInventory();
		Rigidbody2D component = base.GetComponent<Rigidbody2D>();
		if (component != null)
		{
			bool? flag = this.previouslySimulated;
			if (flag != null)
			{
				Rigidbody2D rigidbody2D = component;
				bool? flag2 = this.previouslySimulated;
				rigidbody2D.simulated = flag2.Value;
			}
		}
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x00049EE8 File Offset: 0x000482E8
	private void setSmallIcon(bool on)
	{
		if (this.cleanSprite != null)
		{
			if (on)
			{
				IEnumerator enumerator = base.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						transform.gameObject.SetActive(false);
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
			this.cleanSprite.gameObject.SetActive(!on);
		}
		else
		{
			IEnumerator enumerator2 = base.transform.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					Transform transform2 = (Transform)obj2;
					transform2.gameObject.SetActive(!on);
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
		}
		this.itemSprite.gameObject.SetActive(on);
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x00049FF8 File Offset: 0x000483F8
	private void setDraggableScripts()
	{
		this.currentScript = null;
		string b = string.Empty;
		if (Global.self.currPuzzle != null)
		{
			b = Global.self.currPuzzle.name;
		}
		Draggable[] components = base.GetComponents<Draggable>();
		foreach (Draggable draggable in components)
		{
			draggable.enabled = false;
		}
		for (int j = 0; j < this.puzzleList.Length; j++)
		{
			if (this.puzzleList[j].name == b)
			{
				this.currentScript = this.draggableList[j];
				break;
			}
		}
		if (this.currentScript == null)
		{
			this.currentScript = this.cleanDraggable;
		}
		this.currentScript.enabled = true;
	}

	// Token: 0x04001480 RID: 5248
	[Header("Icon")]
	[Tooltip("Icon inside inventory")]
	public Transform itemSprite;

	// Token: 0x04001481 RID: 5249
	[Tooltip("Scale icon inside inventory")]
	public float scale = 0.7f;

	// Token: 0x04001482 RID: 5250
	[Tooltip("How fast to move item back to slot after it's droped")]
	public float moveBackSpeed = 5f;

	// Token: 0x04001483 RID: 5251
	private bool moveBackAnimation;

	// Token: 0x04001484 RID: 5252
	protected bool dragWithMouse;

	// Token: 0x04001485 RID: 5253
	private bool simulateClick;

	// Token: 0x04001486 RID: 5254
	private Vector3 initialScale;

	// Token: 0x04001487 RID: 5255
	[Header("Link Puzzle+Draggable")]
	[Tooltip("Draggable script for puzzle where no snap and no triggers exist. Just dragg.")]
	public Transform cleanSprite;

	// Token: 0x04001488 RID: 5256
	public InventoryDraggable cleanDraggable;

	// Token: 0x04001489 RID: 5257
	public Transform[] puzzleList;

	// Token: 0x0400148A RID: 5258
	public InventoryDraggable[] draggableList;

	// Token: 0x0400148B RID: 5259
	private InventoryDraggable currentScript;

	// Token: 0x0400148C RID: 5260
	private bool? previouslySimulated;
}
