using System;
using UnityEngine;

// Token: 0x0200047D RID: 1149
[Serializable]
public class SnapPoint
{
	// Token: 0x06001D89 RID: 7561 RVA: 0x000819DF File Offset: 0x0007FDDF
	public SnapPoint(Draggable.Snap type, Vector2 point, float distance, Transform transform)
	{
		this.type = type;
		this.coord2D = point;
		this.distance = distance;
		this.transform = transform;
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x00081A0B File Offset: 0x0007FE0B
	public SnapPoint(Draggable.Snap type, float point, float distance, Transform transform)
	{
		this.type = type;
		this.coord = point;
		this.distance = distance;
		this.transform = transform;
	}

	// Token: 0x06001D8B RID: 7563 RVA: 0x00081A37 File Offset: 0x0007FE37
	public SnapPoint(Draggable.Snap type, Vector2 point, float distance) : this(type, point, distance, null)
	{
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x00081A43 File Offset: 0x0007FE43
	public SnapPoint(Draggable.Snap type, float point, float distance) : this(type, point, distance, null)
	{
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x00081A50 File Offset: 0x0007FE50
	public override string ToString()
	{
		return string.Format("Type: {0}; Position: {1}; Distance: {2}", this.type, (this.type != Draggable.Snap.XY) ? this.coord.ToString() : this.coord2D.ToString(), this.distance);
	}

	// Token: 0x04001C4B RID: 7243
	public Draggable.Snap type;

	// Token: 0x04001C4C RID: 7244
	public float coord;

	// Token: 0x04001C4D RID: 7245
	public Vector2 coord2D;

	// Token: 0x04001C4E RID: 7246
	public float distance;

	// Token: 0x04001C4F RID: 7247
	public Transform transform;

	// Token: 0x04001C50 RID: 7248
	public bool enabled = true;
}
