using System;
using UnityEngine;

// Token: 0x0200054E RID: 1358
public class GizmosExtension
{
	// Token: 0x06001F25 RID: 7973 RVA: 0x000943EC File Offset: 0x000927EC
	public static void DrawRect(Vector3 from, Vector3 to, Color color)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Gizmos.DrawLine(new Vector2(from.x, from.y), new Vector2(to.x, from.y));
		Gizmos.DrawLine(new Vector2(to.x, from.y), new Vector2(to.x, to.y));
		Gizmos.DrawLine(new Vector2(to.x, to.y), new Vector2(from.x, to.y));
		Gizmos.DrawLine(new Vector2(from.x, to.y), new Vector2(from.x, from.y));
		Gizmos.color = color2;
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x000944DF File Offset: 0x000928DF
	public static void DrawRect(Rect rect, Color color)
	{
		GizmosExtension.DrawRect(new Vector2(rect.xMin, rect.yMin), new Vector2(rect.xMax, rect.yMax), color);
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x00094518 File Offset: 0x00092918
	public static void DrawRect(RectTransform t, Color color)
	{
		Vector3 from = new Vector3(t.position.x - t.rect.width / 2f, t.position.y + t.rect.height / 2f + 4f);
		Vector3 to = new Vector3(t.position.x + t.rect.width / 2f, t.position.y - t.rect.height / 2f + 4f);
		GizmosExtension.DrawRect(from, to, color);
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x000945DC File Offset: 0x000929DC
	public static void DrawHorizontalLine(float y, Color color, float fromX = -10f, float toX = 10f)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		GizmosExtension.DrawHorizontalLine(y, fromX, toX);
		Gizmos.color = color2;
	}

	// Token: 0x06001F29 RID: 7977 RVA: 0x00094603 File Offset: 0x00092A03
	public static void DrawHorizontalLine(float y, float fromX = -10f, float toX = 10f)
	{
		Gizmos.DrawLine(new Vector3(fromX, y), new Vector3(toX, y));
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x00094618 File Offset: 0x00092A18
	public static void DrawVerticalLine(float x)
	{
		Gizmos.DrawLine(new Vector3(x, 10f), new Vector3(x, -10f));
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x00094638 File Offset: 0x00092A38
	public static void DrawVerticalLine(float x, Color color)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		GizmosExtension.DrawVerticalLine(x);
		Gizmos.color = color2;
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x00094660 File Offset: 0x00092A60
	public static void DrawLine(Vector3 start, Vector3 end, Color color)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Gizmos.DrawLine(start, end);
		Gizmos.color = color2;
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x00094688 File Offset: 0x00092A88
	public static void DrawPoint(Vector2 point, float len = 0.5f)
	{
		Gizmos.DrawLine(new Vector2(point.x + len, point.y), new Vector2(point.x - len, point.y));
		Gizmos.DrawLine(new Vector2(point.x, point.y + len), new Vector2(point.x, point.y - len));
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x00094708 File Offset: 0x00092B08
	public static void DrawPoint(Vector2 point, Color color, float len = 0.5f)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		GizmosExtension.DrawPoint(point, len);
		Gizmos.color = color2;
	}
}
