using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000571 RID: 1393
public class PackListToolControl : MonoBehaviour
{
	// Token: 0x0600200C RID: 8204 RVA: 0x0009C13F File Offset: 0x0009A53F
	private void Start()
	{
		this.scale = this.initialScale;
		base.transform.localScale = Vector3.one * this.scale;
		this.rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x0009C174 File Offset: 0x0009A574
	private void Update()
	{
		if (Input.mouseScrollDelta.y != 0f)
		{
			this.scale += Input.mouseScrollDelta.y * 0.1f;
			this.scale = Mathf.Clamp(this.scale, this.minScale, this.maxScale);
			base.transform.localScale = Vector3.one * this.scale;
			if (this.scale == this.minScale)
			{
				this.dragIsOn = false;
				this.rect.anchoredPosition = Vector2.zero;
				this.lastDragPos = Vector2.zero;
				this.lastObjPos = Vector2.zero;
			}
		}
		if (Input.GetMouseButtonDown(2) && !this.dragIsOn)
		{
			this.dragIsOn = true;
			this.lastDragPos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(2) && this.dragIsOn)
		{
			this.dragIsOn = false;
			this.lastObjPos = this.rect.anchoredPosition;
		}
		if (this.dragIsOn)
		{
			Vector2 b = this.lastDragPos - Input.mousePosition;
			Vector2 anchoredPosition = this.lastObjPos - b;
			anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, this.maxShiftX.x, this.maxShiftX.y);
			anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, this.maxShiftY.x, this.maxShiftY.y);
			this.rect.anchoredPosition = anchoredPosition;
		}
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x0009C318 File Offset: 0x0009A718
	public void updateParentListGroups()
	{
		foreach (Transform transform in this.listParent)
		{
			GridLayoutGroup component = transform.GetComponent<GridLayoutGroup>();
			component.enabled = false;
			component.enabled = true;
		}
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x0009C35C File Offset: 0x0009A75C
	public void updateSizeOnAll()
	{
		RectTransform component = this.itemPrefab.GetComponent<RectTransform>();
		foreach (Transform transform in this.listParent)
		{
			IEnumerator enumerator = transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform2 = (Transform)obj;
					RectTransform component2 = transform2.GetComponent<RectTransform>();
					component2.sizeDelta = component.sizeDelta;
					transform2.localScale = Vector3.one;
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
	}

	// Token: 0x06002010 RID: 8208 RVA: 0x0009C40C File Offset: 0x0009A80C
	public void showAllColors(PackListToolColors type)
	{
		int num = 0;
		foreach (Transform transform in this.listParent)
		{
			IEnumerator enumerator = transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform2 = (Transform)obj;
					transform2.GetComponent<PackListToolItem>().setColor(type, false);
					num++;
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
		this.currentSelect.text = "Showing colors for " + num.ToString() + " items: " + type.ToString();
		this.lastSelectedColor = type;
	}

	// Token: 0x06002011 RID: 8209 RVA: 0x0009C4DC File Offset: 0x0009A8DC
	public void createAllPuzzleItems()
	{
	}

	// Token: 0x04002339 RID: 9017
	[Space(10f)]
	public Color selectColor;

	// Token: 0x0400233A RID: 9018
	public PackListToolColors lastSelectedColor;

	// Token: 0x0400233B RID: 9019
	[Space(10f)]
	public Text currentSelect;

	// Token: 0x0400233C RID: 9020
	public Transform itemPrefab;

	// Token: 0x0400233D RID: 9021
	public Transform[] listParent;

	// Token: 0x0400233E RID: 9022
	public GameObject[] puzzleList;

	// Token: 0x0400233F RID: 9023
	[Header("In Game Control")]
	public float minScale = 0.5f;

	// Token: 0x04002340 RID: 9024
	public float maxScale = 2f;

	// Token: 0x04002341 RID: 9025
	private float scale = 1f;

	// Token: 0x04002342 RID: 9026
	public float initialScale = 0.6f;

	// Token: 0x04002343 RID: 9027
	[Space(10f)]
	public Vector2 maxShiftX;

	// Token: 0x04002344 RID: 9028
	public Vector2 maxShiftY;

	// Token: 0x04002345 RID: 9029
	private RectTransform rect;

	// Token: 0x04002346 RID: 9030
	private Vector2 lastDragPos;

	// Token: 0x04002347 RID: 9031
	private Vector2 lastObjPos = Vector3.zero;

	// Token: 0x04002348 RID: 9032
	private bool dragIsOn;
}
