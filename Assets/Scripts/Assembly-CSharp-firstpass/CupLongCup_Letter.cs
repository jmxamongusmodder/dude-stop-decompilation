using System;
using UnityEngine;

// Token: 0x02000363 RID: 867
public class CupLongCup_Letter : MonoBehaviour
{
	// Token: 0x0600153B RID: 5435 RVA: 0x0003DCF8 File Offset: 0x0003C0F8
	private void OnMouseDown()
	{
		if (this.opened)
		{
			return;
		}
		Audio.self.playOneShot("026a8c55-5591-41f2-82a2-7ba916558718", 1f);
		this.opened = true;
		this.open.gameObject.SetActive(true);
		this.closed.gameObject.SetActive(false);
		this.firstItem.gameObject.SetActive(true);
		foreach (Collider2D collider2D in base.GetComponents<Collider2D>())
		{
			if (!collider2D.isTrigger)
			{
				collider2D.enabled = false;
			}
		}
	}

	// Token: 0x040012CA RID: 4810
	public Transform open;

	// Token: 0x040012CB RID: 4811
	public Transform closed;

	// Token: 0x040012CC RID: 4812
	public CupLongCup_Draggable firstItem;

	// Token: 0x040012CD RID: 4813
	private bool opened;
}
