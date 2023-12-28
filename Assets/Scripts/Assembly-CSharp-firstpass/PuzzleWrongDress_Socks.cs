using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000470 RID: 1136
public class PuzzleWrongDress_Socks : PuzzleWrongDress_Item
{
	// Token: 0x06001D34 RID: 7476 RVA: 0x00080134 File Offset: 0x0007E534
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (this.dragged)
		{
			int num = (from x in this.GetComponentsInPuzzleStats(false)
			where x.IsSnapped() && x.transform != base.transform
			select x).Count<PuzzleWrongDress_Item>();
			if (num == 3)
			{
				base.snapEnabled = false;
			}
		}
	}

	// Token: 0x06001D35 RID: 7477 RVA: 0x00080180 File Offset: 0x0007E580
	public override void OnMouseUp()
	{
		if (this.dragged)
		{
			base.snapEnabled = true;
		}
		base.OnMouseUp();
		if (this.supermanned || !base.enabled || !base.Snapped())
		{
			return;
		}
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		if ((this.boots1.IsSnapped() && this.boots1.GetComponent<SpriteRenderer>().sortingOrder < component.sortingOrder) || (this.boots2.IsSnapped() && this.boots2.GetComponent<SpriteRenderer>().sortingOrder < component.sortingOrder))
		{
			this.supermanned = true;
			Global.self.currPuzzle.GetComponent<AudioVoice_WrongDress>().socksAboveShoes();
		}
	}

	// Token: 0x04001BDB RID: 7131
	public PuzzleWrongDress_Item boots1;

	// Token: 0x04001BDC RID: 7132
	public PuzzleWrongDress_Item boots2;

	// Token: 0x04001BDD RID: 7133
	private bool supermanned;
}
