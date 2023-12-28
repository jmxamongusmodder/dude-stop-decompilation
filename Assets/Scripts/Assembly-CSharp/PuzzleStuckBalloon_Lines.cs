using System;
using UnityEngine;

// Token: 0x02000457 RID: 1111
[EnabledManually]
public class PuzzleStuckBalloon_Lines : DrawingCanvas, TransitionProcessor
{
	// Token: 0x06001C81 RID: 7297 RVA: 0x00079B38 File Offset: 0x00077F38
	public override void Awake()
	{
		base.Awake();
		this.mainSprite = this.renderer.sprite;
		base.CreateTexture();
		this.secSprite = this.renderer.sprite;
		this.DrawBalloonLines();
	}

	// Token: 0x06001C82 RID: 7298 RVA: 0x00079B6E File Offset: 0x00077F6E
	private void OnDrawGizmosSelected()
	{
		GizmosExtension.DrawPoint(base.transform.position + this.handOffset, 0.5f);
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x00079B9A File Offset: 0x00077F9A
	private void FixedUpdate()
	{
		this.DrawBalloonLines();
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x00079BA4 File Offset: 0x00077FA4
	private void DrawBalloonLines()
	{
		this.SwitchTexture();
		this.DrawPixel(base.transform.position + this.handOffset, this.color, false);
		this.DrawPixel(this.balloon1.transform.position, this.color, false);
		base.DropLastPixel();
		this.DrawPixel(base.transform.position + this.handOffset, this.color, false);
		this.DrawPixel(this.balloon2.transform.position, this.color, false);
		base.DropLastPixel();
		this.DrawPixel(base.transform.position + this.handOffset, this.color, false);
		this.DrawPixel(this.balloon3.transform.position, this.color, true);
		base.DropLastPixel();
	}

	// Token: 0x06001C85 RID: 7301 RVA: 0x00079CA0 File Offset: 0x000780A0
	private void SwitchTexture()
	{
		if (this.renderer.sprite == this.mainSprite)
		{
			this.renderer.sprite = this.secSprite;
			base.ClearPixels(this.mainSprite);
			this.mainSprite.texture.Apply();
		}
		else
		{
			this.renderer.sprite = this.mainSprite;
			base.ClearPixels(this.secSprite);
			this.secSprite.texture.Apply();
		}
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x00079D34 File Offset: 0x00078134
	public void TransitionUpdate()
	{
		this.balloon1.connectedAnchor = base.transform.position + this.handOffset;
		this.balloon2.connectedAnchor = base.transform.position + this.handOffset;
		this.balloon3.connectedAnchor = base.transform.position + this.handOffset;
		base.enabled = true;
	}

	// Token: 0x04001AEB RID: 6891
	public DistanceJoint2D balloon1;

	// Token: 0x04001AEC RID: 6892
	public DistanceJoint2D balloon2;

	// Token: 0x04001AED RID: 6893
	public DistanceJoint2D balloon3;

	// Token: 0x04001AEE RID: 6894
	public Vector2 handOffset;

	// Token: 0x04001AEF RID: 6895
	public Sprite mainSprite;

	// Token: 0x04001AF0 RID: 6896
	public Sprite secSprite;

	// Token: 0x04001AF1 RID: 6897
	public Color color;
}
