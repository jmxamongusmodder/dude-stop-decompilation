using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AC RID: 940
[RequireComponent(typeof(SpriteRenderer))]
public class DrawingCanvas : MonoBehaviour
{
	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06001743 RID: 5955 RVA: 0x0004C976 File Offset: 0x0004AD76
	public float fill
	{
		get
		{
			return (float)this.filledCells / (float)this.totalCells;
		}
	}

	// Token: 0x06001744 RID: 5956 RVA: 0x0004C987 File Offset: 0x0004AD87
	public virtual void Awake()
	{
		this.CreateTexture();
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x0004C990 File Offset: 0x0004AD90
	public void DropLastPixel()
	{
		this.previousPixel = null;
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x0004C9AC File Offset: 0x0004ADAC
	public void EndDrawing()
	{
		this.drawingEnded = true;
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x0004C9B8 File Offset: 0x0004ADB8
	[Obsolete("Please use the other method - this one is not guaranteed to work")]
	public void DrawPixel(Vector3 pixelPosition, Color[] colors)
	{
		int width = this.GetWidth(colors.Length);
		Texture2D texture = this.renderer.sprite.texture;
		Vector3 vector = pixelPosition - base.transform.position + this.coll.bounds.size / 2f;
		vector.x = vector.x / this.coll.bounds.size.x * (float)texture.width;
		vector.y = vector.y / this.coll.bounds.size.y * (float)texture.height;
		vector = new Vector2(Mathf.Clamp(vector.x - (float)(width / 2), 0f, (float)(texture.width - width)), Mathf.Clamp(vector.y - (float)(width / 2), 0f, (float)(texture.height - width)));
		texture.SetPixels((int)vector.x, (int)vector.y, width, width, colors);
		texture.Apply();
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x0004CAE4 File Offset: 0x0004AEE4
	public void SetLimitedColorProvider(Func<Color> provider, Action depleter)
	{
		this.colorProvider = provider;
		this.colorDepleter = depleter;
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x0004CAF4 File Offset: 0x0004AEF4
	public virtual int DrawPixel(Vector3 pixelPosition, Color color, bool apply = true)
	{
		if (!base.enabled || this.drawingEnded)
		{
			return 0;
		}
		Texture2D texture = this.renderer.sprite.texture;
		Vector3 a = this.coll.size;
		a.x *= base.transform.lossyScale.x;
		a.y *= base.transform.lossyScale.y;
		Vector3 vector = pixelPosition - base.transform.position;
		vector = Quaternion.Inverse(base.transform.rotation) * vector + a / 2f;
		vector.x = (float)((int)(vector.x / a.x * (float)this.gridWidth - 0.2f) * this.cellSize);
		vector.y = (float)((int)(vector.y / a.y * (float)this.gridHeight - 0.2f) * this.cellSize);
		Vector2 vector2 = vector;
		int num = 0;
		if (this.simple && this.cellSize == 1)
		{
			this.DrawSimplePixel(vector2, texture, color);
			Vector2? vector3 = this.previousPixel;
			if (vector3 != null)
			{
				Vector2? vector4 = this.previousPixel;
				foreach (Vector2 pixel in this.DrawBresenhamsLine(vector4.Value, vector2, 1))
				{
					Color lhs = this.ChooseColor(color, this.colorProvider);
					if (lhs != Color.clear)
					{
						this.DrawSimplePixel(pixel, texture, color);
						num++;
					}
				}
			}
		}
		else
		{
			Color[] array = new Color[this.cellSize * this.cellSize];
			Vector2? vector5 = this.previousPixel;
			Color color2;
			if (vector5 != null)
			{
				Vector2? vector6 = this.previousPixel;
				foreach (Vector2 pixel2 in this.DrawBresenhamsLine(vector6.Value, vector2, this.cellSize))
				{
					color2 = this.ChooseColor(color, this.colorProvider);
					if (color2 != array[0])
					{
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = color2;
						}
					}
					if (color2 != Color.clear && this.DrawPixel(pixel2, texture, array))
					{
						num++;
						if (this.colorDepleter != null)
						{
							this.colorDepleter();
						}
					}
				}
			}
			color2 = this.ChooseColor(color, this.colorProvider);
			if (color2 != Color.clear)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = color2;
				}
				if (this.DrawPixel(vector2, texture, array))
				{
					num++;
					if (this.colorDepleter != null)
					{
						this.colorDepleter();
					}
				}
			}
		}
		this.previousPixel = new Vector2?(vector2);
		if (apply)
		{
			texture.Apply();
		}
		return num;
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x0004CE8C File Offset: 0x0004B28C
	private Color ChooseColor(Color color, Func<Color> provider)
	{
		return (provider != null) ? provider() : color;
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x0004CEA0 File Offset: 0x0004B2A0
	private void DrawSimplePixel(Vector2 pixel, Texture2D texture, Color color)
	{
		pixel.x = Mathf.Clamp(pixel.x, 0f, this.textureSize.x - (float)this.cellSize);
		pixel.y = Mathf.Clamp(pixel.y, 0f, this.textureSize.y - (float)this.cellSize);
		texture.SetPixel((int)pixel.x, (int)pixel.y, color);
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x0004CF1C File Offset: 0x0004B31C
	private bool DrawPixel(Vector2 pixel, Texture2D texture, Color[] brush)
	{
		pixel.x = Mathf.Clamp(pixel.x, 0f, (float)(texture.width - this.cellSize));
		pixel.y = Mathf.Clamp(pixel.y, 0f, (float)(texture.height - this.cellSize));
		Color pixel2 = texture.GetPixel((int)pixel.x, (int)pixel.y);
		if (pixel2 == brush[0])
		{
			return false;
		}
		Vector4 a = new Vector4(pixel2.r, pixel2.g, pixel2.b, pixel2.a);
		float num = 0.1f;
		float magnitude = (a - this.fillColorV).magnitude;
		if (magnitude > num && !this.overwrite)
		{
			return false;
		}
		if (magnitude < num)
		{
			this.filledCells++;
		}
		texture.SetPixels((int)pixel.x, (int)pixel.y, this.cellSize, this.cellSize, brush);
		return true;
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x0004D034 File Offset: 0x0004B434
	private IEnumerable<Vector2> DrawBresenhamsLine(Vector2 from, Vector2 to, int cellSize = 1)
	{
		int x = (int)from.x;
		int y = (int)from.y;
		int x2 = (int)to.x;
		int y2 = (int)to.y;
		int w = (x2 - x) / cellSize;
		int h = (y2 - y) / cellSize;
		int dx = 0;
		int dy = 0;
		int dx2 = 0;
		int dy2 = 0;
		if (w < 0)
		{
			dx = -1;
		}
		else if (w > 0)
		{
			dx = 1;
		}
		if (h < 0)
		{
			dy = -1;
		}
		else if (h > 0)
		{
			dy = 1;
		}
		if (w < 0)
		{
			dx2 = -1;
		}
		else if (w > 0)
		{
			dx2 = 1;
		}
		int longest = Mathf.Abs(w);
		int shortest = Mathf.Abs(h);
		if (longest <= shortest)
		{
			longest = Mathf.Abs(h);
			shortest = Mathf.Abs(w);
			if (h < 0)
			{
				dy2 = -1;
			}
			else if (h > 0)
			{
				dy2 = 1;
			}
			dx2 = 0;
		}
		int numerator = longest >> 1;
		for (int i = 0; i <= longest; i++)
		{
			yield return new Vector2((float)x, (float)y);
			numerator += shortest;
			if (numerator >= longest)
			{
				numerator -= longest;
				x += dx * cellSize;
				y += dy * cellSize;
			}
			else
			{
				x += dx2 * cellSize;
				y += dy2 * cellSize;
			}
		}
		yield break;
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x0004D068 File Offset: 0x0004B468
	protected void CreateTexture()
	{
		Vector2 realSize = this.CalculateTextureSize();
		realSize = this.ResizeToPredefinedTextureSize(realSize);
		this.gridWidth = (int)(realSize.x / (float)this.cellSize);
		this.gridHeight = (int)(realSize.y / (float)this.cellSize);
		this.totalCells = this.gridWidth * this.gridHeight;
		Texture2D texture2D = new Texture2D((int)realSize.x, (int)realSize.y);
		texture2D.filterMode = FilterMode.Point;
		Color[] pixels = texture2D.GetPixels();
		this.fillColor = Color.clear;
		this.fillColorV = new Vector4(this.fillColor.r, this.fillColor.g, this.fillColor.b, this.fillColor.a);
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i] = this.fillColor;
		}
		texture2D.wrapMode = TextureWrapMode.Clamp;
		texture2D.SetPixels(pixels);
		texture2D.Apply();
		this.renderer.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), Vector2.one / 2f);
		this.renderer.color = Color.white;
		this.renderer.enabled = true;
		this.textureSize.x = (float)texture2D.width;
		this.textureSize.y = (float)texture2D.height;
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x0004D1E0 File Offset: 0x0004B5E0
	protected void ClearPixels(Texture2D texture)
	{
		if (this.clearTexture == null)
		{
			this.clearTexture = this.renderer.sprite.texture.GetPixels();
			for (int i = 0; i < this.clearTexture.Length; i++)
			{
				this.clearTexture[i] = this.fillColor;
			}
		}
		texture.SetPixels(this.clearTexture);
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x0004D24F File Offset: 0x0004B64F
	protected void ClearPixels(bool apply = true)
	{
		this.ClearPixels(this.renderer.sprite.texture);
		if (apply)
		{
			this.renderer.sprite.texture.Apply();
		}
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x0004D284 File Offset: 0x0004B684
	private Vector2 ResizeToPredefinedTextureSize(Vector2 realSize)
	{
		if (!this.usePredefinedSize)
		{
			return realSize;
		}
		Vector2 vector = new Vector2(realSize.x / this.predefinedSize.x, realSize.y / this.predefinedSize.y);
		base.transform.localScale = new Vector2(base.transform.localScale.x * vector.x, base.transform.localScale.y * vector.y);
		this.coll.size = new Vector2(this.coll.size.x / vector.x, this.coll.size.y / vector.y);
		return this.predefinedSize;
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x0004D364 File Offset: 0x0004B764
	private Vector2 CalculateTextureSize()
	{
		this.renderer = base.transform.GetComponent<SpriteRenderer>();
		Vector2 vector = new Vector2((float)this.renderer.sprite.texture.width, (float)this.renderer.sprite.texture.height);
		vector.x *= base.transform.localScale.x;
		vector.y *= base.transform.localScale.y;
		Vector2 result = vector;
		result.x -= result.x % (float)this.cellSize;
		result.y -= result.y % (float)this.cellSize;
		Vector2 v = new Vector2(vector.x / result.x, vector.y / result.y);
		Vector2 size = this.renderer.sprite.bounds.size;
		size.x = size.x * base.transform.localScale.x / v.x;
		size.y = size.y * base.transform.localScale.y / v.y;
		this.coll = base.gameObject.AddComponent<BoxCollider2D>();
		this.coll.size = size;
		this.coll.isTrigger = true;
		base.transform.localScale = v;
		return result;
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x0004D50C File Offset: 0x0004B90C
	private int GetWidth(int length)
	{
		switch (length)
		{
		case 1:
			return 1;
		default:
			if (length == 9)
			{
				return 3;
			}
			if (length == 16)
			{
				return 4;
			}
			if (length != 25)
			{
				return 0;
			}
			return 5;
		case 4:
			return 2;
		}
	}

	// Token: 0x04001528 RID: 5416
	public int cellSize;

	// Token: 0x04001529 RID: 5417
	public bool overwrite;

	// Token: 0x0400152A RID: 5418
	public bool usePredefinedSize;

	// Token: 0x0400152B RID: 5419
	public Vector2 predefinedSize;

	// Token: 0x0400152C RID: 5420
	[Tooltip("If true, the canvas always overwrites and does not count filled pixels")]
	public bool simple;

	// Token: 0x0400152D RID: 5421
	private int filledCells;

	// Token: 0x0400152E RID: 5422
	private int gridWidth;

	// Token: 0x0400152F RID: 5423
	private int gridHeight;

	// Token: 0x04001530 RID: 5424
	private int totalCells;

	// Token: 0x04001531 RID: 5425
	private bool drawingEnded;

	// Token: 0x04001532 RID: 5426
	private Vector4 fillColorV;

	// Token: 0x04001533 RID: 5427
	protected SpriteRenderer renderer;

	// Token: 0x04001534 RID: 5428
	private BoxCollider2D coll;

	// Token: 0x04001535 RID: 5429
	private Vector2? previousPixel;

	// Token: 0x04001536 RID: 5430
	private Func<Color> colorProvider;

	// Token: 0x04001537 RID: 5431
	private Action colorDepleter;

	// Token: 0x04001538 RID: 5432
	private Color[] clearTexture;

	// Token: 0x04001539 RID: 5433
	private Color fillColor = default(Color);

	// Token: 0x0400153A RID: 5434
	private Vector2 textureSize;
}
