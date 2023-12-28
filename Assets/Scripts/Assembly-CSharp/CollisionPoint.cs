using System;
using UnityEngine;

// Token: 0x02000343 RID: 835
public struct CollisionPoint
{
	// Token: 0x0600145F RID: 5215 RVA: 0x0003538C File Offset: 0x0003378C
	public CollisionPoint(Vector2 pos, Vector2 norm, float t, Vector2 posOnObj, float ang, Vector2 lastPos)
	{
		this.point = pos;
		this.normal = norm;
		this.time = t;
		this.distance = 0f;
		this.pointOnObj = posOnObj;
		this.distanceOnObj = 0f;
		this.angle = ang;
		this.distanceAngle = 0f;
		this.pointCount = 1;
	}

	// Token: 0x040011A2 RID: 4514
	public Vector2 point;

	// Token: 0x040011A3 RID: 4515
	public Vector2 normal;

	// Token: 0x040011A4 RID: 4516
	public Vector2 pointOnObj;

	// Token: 0x040011A5 RID: 4517
	public float angle;

	// Token: 0x040011A6 RID: 4518
	public float time;

	// Token: 0x040011A7 RID: 4519
	public float distance;

	// Token: 0x040011A8 RID: 4520
	public float distanceOnObj;

	// Token: 0x040011A9 RID: 4521
	public float distanceAngle;

	// Token: 0x040011AA RID: 4522
	public int pointCount;
}
