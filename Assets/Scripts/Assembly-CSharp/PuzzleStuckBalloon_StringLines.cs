using System;
using UnityEngine;

// Token: 0x02000458 RID: 1112
[EnabledManually]
public class PuzzleStuckBalloon_StringLines : DrawingCanvas, TransitionProcessor
{
	// Token: 0x06001C88 RID: 7304 RVA: 0x00079DDC File Offset: 0x000781DC
	private void FixedUpdate()
	{
		Vector3 position = (this.secondLine.position + this.thirdLine.position) / 2f;
		position.x -= position.x % 0.297f;
		position.y -= position.y % 0.189f;
		position.z = 2f;
		base.transform.position = position;
		this.DrawBalloonLines();
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x00079E63 File Offset: 0x00078263
	public void TransitionUpdate()
	{
		base.enabled = true;
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x00079E6C File Offset: 0x0007826C
	private void DrawBalloonLines()
	{
		base.ClearPixels(false);
		this.DrawPixel(this.fourthLine.position - this.fourthLine.up * this.lineHeight / 2f, this.color, false);
		this.DrawPixel(this.fourthLine.position + this.fourthLine.up * this.lineHeight / 2f, this.color, false);
		this.DrawPixel(this.thirdLine.position + this.thirdLine.up * this.lineHeight / 2f, this.color, false);
		this.DrawPixel(this.secondLine.position + this.secondLine.up * this.lineHeight / 2f, this.color, false);
		this.DrawPixel(this.balloon.position + this.balloon.up * this.balloonOffset, this.color, true);
		base.DropLastPixel();
	}

	// Token: 0x04001AF2 RID: 6898
	[Header("Line drawing")]
	public Color color;

	// Token: 0x04001AF3 RID: 6899
	public float lineHeight = 0.64f;

	// Token: 0x04001AF4 RID: 6900
	public Transform fourthLine;

	// Token: 0x04001AF5 RID: 6901
	public Transform thirdLine;

	// Token: 0x04001AF6 RID: 6902
	public Transform secondLine;

	// Token: 0x04001AF7 RID: 6903
	public Transform firstLine;

	// Token: 0x04001AF8 RID: 6904
	public Transform balloon;

	// Token: 0x04001AF9 RID: 6905
	public float balloonOffset;

	// Token: 0x04001AFA RID: 6906
	[Header("Level completion")]
	public Vector2 victoryPoint;

	// Token: 0x04001AFB RID: 6907
	public float pullDistance;

	// Token: 0x04001AFC RID: 6908
	public float pullSpeed;

	// Token: 0x04001AFD RID: 6909
	public float snapDistance;
}
