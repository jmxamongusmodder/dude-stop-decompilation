using System;
using UnityEngine;

// Token: 0x0200034E RID: 846
public class CupFastShoe_Shoe : MonoBehaviour
{
	// Token: 0x06001493 RID: 5267 RVA: 0x000384D9 File Offset: 0x000368D9
	private void OnMouseDown()
	{
		this.controller.shoeClicked = true;
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x000384E7 File Offset: 0x000368E7
	public void ActivateBadSprite()
	{
		this.goldenSprite.gameObject.SetActive(false);
		this.badSprite.gameObject.SetActive(true);
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x0003850B File Offset: 0x0003690B
	public void Swing()
	{
		Audio.self.playOneShot("021ed4e9-e3d9-4a86-9a50-013d1fa2de28", 1f);
	}

	// Token: 0x04001207 RID: 4615
	public CupFastShoe_Controller controller;

	// Token: 0x04001208 RID: 4616
	public Transform goldenSprite;

	// Token: 0x04001209 RID: 4617
	public Transform badSprite;
}
