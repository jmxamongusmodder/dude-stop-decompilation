using System;
using UnityEngine;

// Token: 0x0200036E RID: 878
public class CupToyMachine_ButtonGo : MonoBehaviour
{
	// Token: 0x06001593 RID: 5523 RVA: 0x00041EB5 File Offset: 0x000402B5
	private void Start()
	{
		this.claw = this.GetComponentInPuzzleStats<CupToyMachine_Claw>();
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x00041EC3 File Offset: 0x000402C3
	private void OnMouseEnter()
	{
		base.GetComponent<SpriteRenderer>().enabled = true;
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x00041ED1 File Offset: 0x000402D1
	private void OnMouseExit()
	{
		base.GetComponent<SpriteRenderer>().enabled = false;
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x00041EDF File Offset: 0x000402DF
	private void OnMouseDown()
	{
		if (this.claw != null)
		{
			this.claw.Go();
		}
	}

	// Token: 0x04001348 RID: 4936
	private CupToyMachine_Claw claw;
}
