using System;
using UnityEngine;

// Token: 0x0200035F RID: 863
[Serializable]
public class LegoCupPiece
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x0600151F RID: 5407 RVA: 0x0003CB3B File Offset: 0x0003AF3B
	// (set) Token: 0x06001520 RID: 5408 RVA: 0x0003CB4E File Offset: 0x0003AF4E
	public Vector2 position
	{
		get
		{
			return new Vector2(this.x, this.y);
		}
		set
		{
			this.x = value.x;
			this.y = value.y;
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06001521 RID: 5409 RVA: 0x0003CB6A File Offset: 0x0003AF6A
	public int points
	{
		get
		{
			return Mathf.Clamp(this.index % 4 + 1, 2, 4);
		}
	}

	// Token: 0x040012B1 RID: 4785
	public int index;

	// Token: 0x040012B2 RID: 4786
	public bool snapped;

	// Token: 0x040012B3 RID: 4787
	public float x;

	// Token: 0x040012B4 RID: 4788
	public float y;
}
