using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000587 RID: 1415
[RequireComponent(typeof(ButtonControl))]
public abstract class scrollMenuClass : AbstractUIScreen
{
	// Token: 0x06002089 RID: 8329 RVA: 0x0009711C File Offset: 0x0009551C
	private void Start()
	{
		this.setList(0);
		if (this.buttonList.childCount <= this.maxButtonCount)
		{
			this.prevButton.gameObject.SetActive(false);
			this.nextButton.gameObject.SetActive(false);
		}
		this.scrollView.verticalNormalizedPosition = 1f;
		this.buttonControl = base.GetComponent<ButtonControl>();
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x00097184 File Offset: 0x00095584
	public override void Update()
	{
		base.Update();
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject != null && currentSelectedGameObject.transform.parent == this.buttonList)
		{
			float y = Camera.main.WorldToViewportPoint(currentSelectedGameObject.GetComponent<RectTransform>().position).y;
			if (y <= this.topPosToScroll)
			{
				this.setScrollMovement(-1f);
			}
			else if (y >= this.botPotToScroll)
			{
				this.setScrollMovement(1f);
			}
		}
		if (this.scrollTimeCurr > 0f)
		{
			this.scrollTimeCurr -= Time.deltaTime;
			this.scrollView.verticalNormalizedPosition += this.scrollAmount * (this.scrollTimeCurr / this.scrollTimeMax * 10f) * Time.deltaTime;
		}
		if (this.buttonList.childCount > this.maxButtonCount)
		{
			if (this.scrollView.verticalNormalizedPosition < 1f - this.procToHideButtons)
			{
				this.prevButton.gameObject.SetActive(true);
				this.buttonControl.firstButton = this.prevButton.gameObject;
				if (Input.mouseScrollDelta.y > 0f)
				{
					this.setScrollMovement(1f);
					EventSystem.current.SetSelectedGameObject(null);
				}
			}
			else
			{
				this.prevButton.gameObject.SetActive(false);
				this.buttonControl.firstButton = this.buttonList.GetChild(0).gameObject;
				if (EventSystem.current.currentSelectedGameObject == this.prevButton.gameObject)
				{
					EventSystem.current.SetSelectedGameObject(null);
				}
			}
			if (this.scrollView.verticalNormalizedPosition > this.procToHideButtons)
			{
				this.nextButton.gameObject.SetActive(true);
				if (Input.mouseScrollDelta.y < 0f)
				{
					this.setScrollMovement(-1f);
					EventSystem.current.SetSelectedGameObject(null);
				}
			}
			else
			{
				this.nextButton.gameObject.SetActive(false);
				if (EventSystem.current.currentSelectedGameObject == this.nextButton.gameObject)
				{
					EventSystem.current.SetSelectedGameObject(null);
				}
			}
		}
		this.scrollView.verticalNormalizedPosition = Mathf.Clamp(this.scrollView.verticalNormalizedPosition, -this.extraScroll, 1f + this.extraScroll);
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x00097413 File Offset: 0x00095813
	private void setScrollMovement(float dir)
	{
		this.scrollTimeCurr = this.scrollTimeMax;
		this.scrollAmount = Mathf.Abs(this.scrollAmount) * dir;
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x00097434 File Offset: 0x00095834
	protected override void cancelPressed()
	{
		this.bBack();
	}

	// Token: 0x0600208D RID: 8333 RVA: 0x0009743C File Offset: 0x0009583C
	protected virtual void setList(int count)
	{
		if (count == 0)
		{
			Debug.LogError("Can't create 0 item list. Override setList method and set COUNT property");
			return;
		}
		this.makeListOfItems(count);
	}

	// Token: 0x0600208E RID: 8334 RVA: 0x00097458 File Offset: 0x00095858
	private void makeListOfItems(int count)
	{
		Transform child = this.buttonList.GetChild(0);
		this.setListItem(child, 0);
		for (int i = 1; i < count; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(child);
			transform.SetParent(child.parent);
			transform.localScale = Vector3.one;
			this.setListItem(transform, i);
		}
		VerticalLayoutGroup component = child.parent.GetComponent<VerticalLayoutGroup>();
		RectTransform component2 = child.parent.GetComponent<RectTransform>();
		component2.sizeDelta = new Vector2(component2.sizeDelta.x, this.buttonHeight * (float)count + (float)component.padding.top + (float)component.padding.bottom);
		this.scrollAmount /= (float)(count * count);
	}

	// Token: 0x0600208F RID: 8335
	protected abstract void setListItem(Transform item, int index);

	// Token: 0x06002090 RID: 8336 RVA: 0x0009751B File Offset: 0x0009591B
	public void bNext()
	{
		if (!this.active)
		{
			return;
		}
		this.setScrollMovement(-1f);
	}

	// Token: 0x06002091 RID: 8337 RVA: 0x00097534 File Offset: 0x00095934
	public void bPrev()
	{
		if (!this.active)
		{
			return;
		}
		this.setScrollMovement(1f);
	}

	// Token: 0x06002092 RID: 8338
	public abstract void bBack();

	// Token: 0x06002093 RID: 8339 RVA: 0x0009754D File Offset: 0x0009594D
	public override void setScreen(Transform item)
	{
	}

	// Token: 0x040023E0 RID: 9184
	[Tooltip("Button to go back")]
	public Button prevButton;

	// Token: 0x040023E1 RID: 9185
	[Tooltip("Button to go forward")]
	public Button nextButton;

	// Token: 0x040023E2 RID: 9186
	[Tooltip("List of resolution buttons")]
	public Transform buttonList;

	// Token: 0x040023E3 RID: 9187
	[Tooltip("Height of the resolution selection button")]
	public float buttonHeight = 20f;

	// Token: 0x040023E4 RID: 9188
	[Tooltip("At what number of buttons show scroll buttons")]
	public int maxButtonCount = 13;

	// Token: 0x040023E5 RID: 9189
	private float scrollTimeCurr;

	// Token: 0x040023E6 RID: 9190
	[Tooltip("How long to to apply scrollAmount")]
	public float scrollTimeMax = 0.5f;

	// Token: 0x040023E7 RID: 9191
	public float scrollAmount = 0.02f;

	// Token: 0x040023E8 RID: 9192
	[Tooltip("when buttons is higher that this point - scroll list")]
	public float topPosToScroll = 0.25f;

	// Token: 0x040023E9 RID: 9193
	public float botPotToScroll = 0.75f;

	// Token: 0x040023EA RID: 9194
	[Tooltip("Scroll rect with button list")]
	public ScrollRect scrollView;

	// Token: 0x040023EB RID: 9195
	[Tooltip("At what % hide Up or Down buttons. (5% means that if scroll view is <5% - hide Up button")]
	public float procToHideButtons = 0.05f;

	// Token: 0x040023EC RID: 9196
	[Tooltip("How far view can be extra scrolled up or down")]
	public float extraScroll = 0.1f;

	// Token: 0x040023ED RID: 9197
	private ButtonControl buttonControl;
}
