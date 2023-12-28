using System;
using UnityEngine;

// Token: 0x020004F1 RID: 1265
public class SpriteCombiner : MonoBehaviour
{
	// Token: 0x06001D8F RID: 7567 RVA: 0x00081AF0 File Offset: 0x0007FEF0
	private void OnValidate()
	{
		if (!this.active)
		{
			return;
		}
		if (this.autoFind)
		{
			this.top = base.transform.Find("top");
			if (this.top == null)
			{
				this.top = base.transform.Find("Top");
			}
			this.bot = base.transform.Find("bot");
			if (this.bot == null)
			{
				this.bot = base.transform.Find("Bot");
			}
			this.mid = base.transform.Find("mid");
			if (this.mid == null)
			{
				this.mid = base.transform.Find("Mid");
			}
			if (this.mid == null)
			{
				Debug.LogWarning("MID sprite couldn't be found. Add one manually, or rename it to \"mid\"");
			}
		}
		if (this.vertical)
		{
			float num = 0f;
			float num2 = 0f;
			if (this.top != null)
			{
				this.top.localPosition = new Vector2(this.shiftX, this.shiftY + this.sizeY * 0.5f);
				num = this.top.GetComponent<SpriteRenderer>().bounds.size.y;
			}
			if (this.bot != null)
			{
				this.bot.localPosition = new Vector2(this.shiftX, this.shiftY - this.sizeY * 0.5f);
				num2 = this.bot.GetComponent<SpriteRenderer>().bounds.size.y;
			}
			float num3 = this.sizeY - num / 2f - num2 / 2f;
			float num4 = this.mid.GetComponent<SpriteRenderer>().bounds.size.y / this.mid.localScale.y;
			this.mid.localPosition = new Vector2(this.shiftX, this.shiftY + this.midExtraTop / 2f - this.midExtraBot / 2f);
			this.mid.localScale = new Vector2(1f, num3 / num4 + (this.midExtraTop + this.midExtraBot) / num4);
		}
		else
		{
			Debug.LogWarning("NIY");
		}
	}

	// Token: 0x04001E28 RID: 7720
	[Tooltip("Turn off this script")]
	public bool active = true;

	// Token: 0x04001E29 RID: 7721
	[Space(10f)]
	[Tooltip("Automatically look for sprites to use")]
	public bool autoFind = true;

	// Token: 0x04001E2A RID: 7722
	public Transform top;

	// Token: 0x04001E2B RID: 7723
	public Transform mid;

	// Token: 0x04001E2C RID: 7724
	public Transform bot;

	// Token: 0x04001E2D RID: 7725
	[Header("Vertical")]
	public bool vertical = true;

	// Token: 0x04001E2E RID: 7726
	[Tooltip("Height of the whole object")]
	public float sizeY = 1f;

	// Token: 0x04001E2F RID: 7727
	[Space(10f)]
	[Tooltip("Shift each sprite inside this object by this much")]
	public float shiftY;

	// Token: 0x04001E30 RID: 7728
	[Tooltip("Cut or add extra scale from the top of the MID sprite")]
	public float midExtraTop = 0.02f;

	// Token: 0x04001E31 RID: 7729
	[Tooltip("Cut or add extra scale from the bot of the MID sprite")]
	public float midExtraBot = 0.02f;

	// Token: 0x04001E32 RID: 7730
	[Header("Horizontal")]
	public float shiftX;
}
