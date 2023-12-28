using System;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class CupToyMachine_Button : MonoBehaviour
{
	// Token: 0x0600158D RID: 5517 RVA: 0x00041E41 File Offset: 0x00040241
	private void Start()
	{
		this.claw = this.GetComponentInPuzzleStats<CupToyMachine_Claw>();
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x00041E4F File Offset: 0x0004024F
	private void OnMouseEnter()
	{
		base.GetComponent<SpriteRenderer>().enabled = true;
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x00041E5D File Offset: 0x0004025D
	private void OnMouseExit()
	{
		base.GetComponent<SpriteRenderer>().enabled = false;
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x00041E6B File Offset: 0x0004026B
	private void OnMouseDown()
	{
		if (this.claw != null)
		{
			this.claw.Move(this.moveLeft);
		}
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x00041E8F File Offset: 0x0004028F
	private void OnMouseUp()
	{
		if (this.claw != null)
		{
			this.claw.Stop();
		}
	}

	// Token: 0x04001346 RID: 4934
	public bool moveLeft;

	// Token: 0x04001347 RID: 4935
	private CupToyMachine_Claw claw;
}
