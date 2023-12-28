using System;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public class PuzzleComicSans_Font : MonoBehaviour
{
	// Token: 0x06001955 RID: 6485 RVA: 0x0005DDC4 File Offset: 0x0005C1C4
	private void Start()
	{
		this.selected = base.transform.parent.parent.Find("Field").Find("Selected").GetComponent<SpriteRenderer>();
		this.button = base.transform.parent.parent.Find("Field").Find("Button");
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x0005DE2C File Offset: 0x0005C22C
	private void Update()
	{
		if (this.clicked)
		{
			this.timer += Time.deltaTime;
			if (this.timer > this.waitBeforeTransition)
			{
				if (this.fail)
				{
					Global.LevelFailed(0f, true);
				}
				else
				{
					Global.LevelCompleted(0f, true);
				}
			}
		}
	}

	// Token: 0x06001957 RID: 6487 RVA: 0x0005DE90 File Offset: 0x0005C290
	private void OnMouseOver()
	{
		if (!PuzzleComicSans_Open.open)
		{
			return;
		}
		float num = 0.372f;
		float num2 = -0.694f;
		float y = num + (float)(this.order - 1) * num2;
		this.background.localPosition = new Vector3(this.background.localPosition.x, y, 0f);
		this.background.gameObject.SetActive(true);
		if (this.prevY != this.background.localPosition.y)
		{
			Audio.self.playOneShot("f53fb028-b301-49fd-aa3d-cc8f91e5487f", 1f);
		}
		this.prevY = this.background.localPosition.y;
	}

	// Token: 0x06001958 RID: 6488 RVA: 0x0005DF4A File Offset: 0x0005C34A
	private void OnMouseExit()
	{
		if (!PuzzleComicSans_Open.open)
		{
			return;
		}
		this.background.gameObject.SetActive(false);
		this.prevY = 0f;
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x0005DF74 File Offset: 0x0005C374
	private void OnMouseDown()
	{
		if (!PuzzleComicSans_Open.open)
		{
			return;
		}
		if (this.clicked)
		{
			return;
		}
		this.clicked = true;
		this.DisableOtherFonts();
		this.selected.sprite = base.GetComponent<SpriteRenderer>().sprite;
		this.selected.enabled = true;
		this.button.GetComponent<PuzzleComicSans_Open>().Click();
		this.button.GetComponent<PuzzleComicSans_Open>().respond = false;
		this.background.gameObject.SetActive(false);
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x0005DFFC File Offset: 0x0005C3FC
	private void DisableOtherFonts()
	{
		foreach (PuzzleComicSans_Font puzzleComicSans_Font in base.transform.parent.GetComponentsInChildren<PuzzleComicSans_Font>())
		{
			if (puzzleComicSans_Font != this)
			{
				puzzleComicSans_Font.enabled = false;
			}
		}
	}

	// Token: 0x04001754 RID: 5972
	public Transform background;

	// Token: 0x04001755 RID: 5973
	public int order;

	// Token: 0x04001756 RID: 5974
	public bool fail;

	// Token: 0x04001757 RID: 5975
	public float waitBeforeTransition = 2f;

	// Token: 0x04001758 RID: 5976
	private bool clicked;

	// Token: 0x04001759 RID: 5977
	private float timer;

	// Token: 0x0400175A RID: 5978
	private SpriteRenderer selected;

	// Token: 0x0400175B RID: 5979
	private Transform button;

	// Token: 0x0400175C RID: 5980
	private float prevY;
}
