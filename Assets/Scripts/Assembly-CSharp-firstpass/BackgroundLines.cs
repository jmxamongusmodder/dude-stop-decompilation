using System;
using UnityEngine;

// Token: 0x0200033F RID: 831
public class BackgroundLines : MonoBehaviour
{
	// Token: 0x06001453 RID: 5203 RVA: 0x00034F2E File Offset: 0x0003332E
	public void updateColor()
	{
		this.OnValidate();
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x00034F38 File Offset: 0x00033338
	private void OnValidate()
	{
	}

	// Token: 0x0400118E RID: 4494
	[Header("Order")]
	[Tooltip("Order on when to show/hide this line. Closer to camera (+) - later to show")]
	[Range(-10f, 10f)]
	public int order;

	// Token: 0x0400118F RID: 4495
	[Header("Color")]
	[Tooltip("Alter color between Default, Dark and Bright setted in the BackgroundControl")]
	[Range(-1f, 1f)]
	public float colorAlter;

	// Token: 0x04001190 RID: 4496
	[Tooltip("Start with this color, instead of the current Background color")]
	public Color startingColor;

	// Token: 0x04001191 RID: 4497
	[Tooltip("Do not use AlterColor, use sprite color instead")]
	public bool customColor;

	// Token: 0x04001192 RID: 4498
	[Tooltip("Set starting color as sprite color (color will not be lerped on this line)")]
	public bool sameStartingColor;

	// Token: 0x04001193 RID: 4499
	[HideInInspector]
	public bool customStartingColor;

	// Token: 0x04001194 RID: 4500
	[Header("Delay")]
	[Tooltip("Add this much to delay for showing animation")]
	[Range(0f, 1f)]
	public float customShowDelay;

	// Token: 0x04001195 RID: 4501
	[Tooltip("Add this much to delay for hiding animation")]
	[Range(-1f, 1f)]
	public float customHideDelay;

	// Token: 0x04001196 RID: 4502
	[HideInInspector]
	public Color color;

	// Token: 0x04001197 RID: 4503
	[HideInInspector]
	public Vector3 scale;

	// Token: 0x04001198 RID: 4504
	[HideInInspector]
	public float delayShow;

	// Token: 0x04001199 RID: 4505
	[HideInInspector]
	public float delayHide;
}
