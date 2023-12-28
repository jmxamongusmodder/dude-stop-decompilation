using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000586 RID: 1414
public class ArrowButtonStats
{
	// Token: 0x06002084 RID: 8324 RVA: 0x000A0010 File Offset: 0x0009E410
	public ArrowButtonStats()
	{
	}

	// Token: 0x06002085 RID: 8325 RVA: 0x000A0018 File Offset: 0x0009E418
	public ArrowButtonStats(RectTransform b, bool hideToLeft = true)
	{
		this.button = b;
		this.start = b.anchoredPosition;
		this.end = b.anchoredPosition + Vector2.right * b.sizeDelta.x * 1.1f * ((!hideToLeft) ? 1f : -1f);
	}

	// Token: 0x06002086 RID: 8326 RVA: 0x000A008C File Offset: 0x0009E48C
	public void Update()
	{
		if (!this.moving)
		{
			return;
		}
		this.button.anchoredPosition = Vector2.MoveTowards(this.button.anchoredPosition, this.target, Time.deltaTime * 400f);
		if (Vector2.SqrMagnitude(this.button.anchoredPosition - this.target) < 0.1f)
		{
			this.moving = false;
			if (this.target == this.end)
			{
				this.button.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002087 RID: 8327 RVA: 0x000A0124 File Offset: 0x0009E524
	public void setButton(bool show)
	{
		this.button.GetComponent<Button>().interactable = show;
		if (show)
		{
			this.button.gameObject.SetActive(true);
			this.target = this.start;
		}
		else
		{
			this.target = this.end;
		}
		this.moving = true;
	}

	// Token: 0x040023DB RID: 9179
	private RectTransform button;

	// Token: 0x040023DC RID: 9180
	private Vector2 start;

	// Token: 0x040023DD RID: 9181
	private Vector2 end;

	// Token: 0x040023DE RID: 9182
	private bool moving;

	// Token: 0x040023DF RID: 9183
	private Vector2 target;
}
