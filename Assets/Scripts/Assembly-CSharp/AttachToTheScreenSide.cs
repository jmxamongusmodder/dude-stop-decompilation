using System;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public class AttachToTheScreenSide : MonoBehaviour
{
	// Token: 0x06001736 RID: 5942 RVA: 0x0004C7D0 File Offset: 0x0004ABD0
	[ContextMenu("Attach now")]
	private void Awake()
	{
		this.CalculatePosition();
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x0004C7D8 File Offset: 0x0004ABD8
	private void OnValidate()
	{
		if (!this.activeInEditor)
		{
			return;
		}
		if (Camera.main == null)
		{
			return;
		}
		this.CalculatePosition();
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x0004C800 File Offset: 0x0004AC00
	public void CalculatePosition()
	{
		Vector2 v = (!this.leftSide) ? Vector2.one : Vector2.zero;
		v = Camera.main.ViewportToWorldPoint(v);
		float x = this.GetPuzzleStats().transform.position.x;
		base.transform.position = new Vector2(v.x + this.shiftX + x, base.transform.position.y);
	}

	// Token: 0x04001524 RID: 5412
	public bool leftSide;

	// Token: 0x04001525 RID: 5413
	public float shiftX;

	// Token: 0x04001526 RID: 5414
	public bool activeInEditor;
}
