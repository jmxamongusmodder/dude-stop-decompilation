using System;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class CupStone_Stone : MonoBehaviour
{
	// Token: 0x0600158A RID: 5514 RVA: 0x00041E0C File Offset: 0x0004020C
	private void Update()
	{
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x00041E0E File Offset: 0x0004020E
	public void EndOfAnimation()
	{
		base.GetComponent<Animator>().enabled = false;
		this.GetComponentInPuzzleStats<CupStone_Cup>().enabled = true;
		this.GetComponentInPuzzleStats<CupStone_Cup>().GetComponent<Collider2D>().enabled = true;
	}
}
