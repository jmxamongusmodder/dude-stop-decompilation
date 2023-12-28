using System;
using UnityEngine;

// Token: 0x020003AF RID: 943
public class DrawingPenPoint : MonoBehaviour
{
	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06001762 RID: 5986 RVA: 0x0004DC95 File Offset: 0x0004C095
	// (set) Token: 0x06001763 RID: 5987 RVA: 0x0004DC9D File Offset: 0x0004C09D
	public int availablePixels
	{
		get
		{
			return this._availablePixels;
		}
		set
		{
			this._availablePixels = value;
			this.pixelsLeft = value;
		}
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x0004DCAD File Offset: 0x0004C0AD
	private void OnDisable()
	{
		if (this.paper != null)
		{
			this.paper.DropLastPixel();
		}
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x0004DCCC File Offset: 0x0004C0CC
	private void Update()
	{
		if (!this.depleted && this.paper != null && this.pen != null && this.pen.IsDrawing())
		{
			this.DrawPixel();
		}
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x0004DD1C File Offset: 0x0004C11C
	public void DropPixel()
	{
		if (this.paper != null)
		{
			this.paper.DropLastPixel();
		}
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x0004DD3C File Offset: 0x0004C13C
	private void OnTriggerEnter2D(Collider2D other)
	{
		DrawingCanvas component = other.GetComponent<DrawingCanvas>();
		if (component != null)
		{
			if (this.paper != null)
			{
				this.paper.DropLastPixel();
			}
			this.paper = component;
			foreach (DrawingPen drawingPen in base.transform.parent.GetComponents<DrawingPen>())
			{
				if (drawingPen.enabled)
				{
					this.pen = drawingPen;
				}
			}
		}
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x0004DDBA File Offset: 0x0004C1BA
	private void OnTriggerExit2D(Collider2D other)
	{
		if (this.paper != null && other.transform == this.paper.transform)
		{
			this.paper.DropLastPixel();
			this.paper = null;
		}
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x0004DDFA File Offset: 0x0004C1FA
	public bool Depleted()
	{
		return this.depleted;
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x0004DE02 File Offset: 0x0004C202
	public DrawingCanvas GetPaper()
	{
		return this.paper;
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x0004DE0C File Offset: 0x0004C20C
	private void DrawPixel()
	{
		if (this.depleted)
		{
			return;
		}
		this.paper.SetLimitedColorProvider(new Func<Color>(this.GetColor), new Action(this.PixelDrawn));
		int pixelsDrawn = this.paper.DrawPixel(base.transform.position, this.GetColor(), true);
		this.pen.PixelDrawn(this.paper, pixelsDrawn);
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x0004DE78 File Offset: 0x0004C278
	private void PixelDrawn()
	{
		if (--this.pixelsLeft == 0)
		{
			this.depleted = true;
			this.pen.Depleted();
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x0004DEB0 File Offset: 0x0004C2B0
	private Color GetColor()
	{
		if (this.depleted)
		{
			return Color.clear;
		}
		if (this.availablePixels != 0)
		{
			Color result = this.color;
			result.a = Mathf.Lerp(result.a, 0f, 1f - (float)this.pixelsLeft / (float)this.availablePixels);
			return result;
		}
		return this.color;
	}

	// Token: 0x04001547 RID: 5447
	private int _availablePixels;

	// Token: 0x04001548 RID: 5448
	public Color color;

	// Token: 0x04001549 RID: 5449
	private bool depleted;

	// Token: 0x0400154A RID: 5450
	private int pixelsLeft;

	// Token: 0x0400154B RID: 5451
	private bool textureCreated;

	// Token: 0x0400154C RID: 5452
	private DrawingPen pen;

	// Token: 0x0400154D RID: 5453
	private DrawingCanvas paper;
}
