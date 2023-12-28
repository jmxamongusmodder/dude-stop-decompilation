using System;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class BoxOrToy_Cat : Draggable
{
	// Token: 0x0600173A RID: 5946 RVA: 0x0004C897 File Offset: 0x0004AC97
	private void Update()
	{
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x0004C899 File Offset: 0x0004AC99
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		base.GetComponent<BoxCollider2D>().enabled = false;
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x0004C8AD File Offset: 0x0004ACAD
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		base.GetComponent<BoxCollider2D>().enabled = true;
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x0004C8C1 File Offset: 0x0004ACC1
	private void Meow()
	{
	}

	// Token: 0x04001527 RID: 5415
	public Transform meow;
}
