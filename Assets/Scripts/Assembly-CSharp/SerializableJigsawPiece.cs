using System;
using UnityEngine;

// Token: 0x02000425 RID: 1061
[Serializable]
public class SerializableJigsawPiece
{
	// Token: 0x06001AFD RID: 6909 RVA: 0x0006D8A9 File Offset: 0x0006BCA9
	public SerializableJigsawPiece()
	{
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x0006D8B4 File Offset: 0x0006BCB4
	public SerializableJigsawPiece(JigsawPiece p, int group = 0)
	{
		this.x = p.transform.position.x;
		this.y = p.transform.position.y;
		this.z = p.transform.position.z;
		this.order = p.GetComponent<SpriteRenderer>().sortingOrder;
		this.name = p.name;
		this.group = group;
		this.finished = p.finished;
		JigsawPiece_Interchangeable component = p.GetComponent<JigsawPiece_Interchangeable>();
		if (component != null)
		{
			this.interchangeable = true;
			this.interchangeableSetStatus = component.GetUsedSet();
		}
	}

	// Token: 0x0400193A RID: 6458
	public float x;

	// Token: 0x0400193B RID: 6459
	public float y;

	// Token: 0x0400193C RID: 6460
	public float z;

	// Token: 0x0400193D RID: 6461
	public int order;

	// Token: 0x0400193E RID: 6462
	public string name;

	// Token: 0x0400193F RID: 6463
	public int group;

	// Token: 0x04001940 RID: 6464
	public bool interchangeable;

	// Token: 0x04001941 RID: 6465
	public int interchangeableSetStatus;

	// Token: 0x04001942 RID: 6466
	public bool finished;
}
