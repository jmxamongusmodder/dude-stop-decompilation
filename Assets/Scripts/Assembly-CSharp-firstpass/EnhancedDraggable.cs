using System;

// Token: 0x0200037A RID: 890
public class EnhancedDraggable : Draggable
{
	// Token: 0x060015ED RID: 5613 RVA: 0x0003B236 File Offset: 0x00039636
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x0003B24A File Offset: 0x0003964A
	public override void OnMouseUp()
	{
		if (!base.enabled || !this.dragged)
		{
			return;
		}
		base.OnMouseUp();
	}
}
