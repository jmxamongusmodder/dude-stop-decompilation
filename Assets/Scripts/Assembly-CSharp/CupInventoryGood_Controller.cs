using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class CupInventoryGood_Controller : MonoBehaviour
{
	// Token: 0x06000099 RID: 153 RVA: 0x00007F40 File Offset: 0x00006140
	private void Start()
	{
		base.StartCoroutine(this.AddingToInventoryCoroutine());
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00007F50 File Offset: 0x00006150
	public void StartEndAnimation()
	{
		Audio.self.playOneShot("b9f0e8f5-da28-41ef-a481-d61db6727d9e", 1f);
		foreach (CupInventoryGood_Item cupInventoryGood_Item in this.GetComponentsInPuzzleStats(false))
		{
			cupInventoryGood_Item.enabled = false;
		}
		InventoryControl.self.removeInventory();
		base.GetComponent<Animator>().enabled = true;
		this.GetPuzzleStats().GetComponent<AudioVoice_CupInventoryGood>().interruptVoice();
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00007FBF File Offset: 0x000061BF
	public void AnimationEnded()
	{
		base.GetComponent<Animator>().enabled = false;
		this.cup.parent = base.transform.parent;
		Global.CupAcquired(this.cup);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00007FF0 File Offset: 0x000061F0
	private IEnumerator AddingToInventoryCoroutine()
	{
		yield return new WaitForSeconds(0.1f);
		CupInventoryGood_Item[] items = this.GetComponentsInPuzzleStats(false);
		foreach (CupInventoryGood_Item cupInventoryGood_Item in items)
		{
			cupInventoryGood_Item.GetPartner();
		}
		foreach (InventoryItem item in this.GetComponentsInPuzzleStats(false))
		{
			item.moveBackToInventory();
			yield return new WaitForSeconds(this.itemAppearanceTime);
		}
		foreach (CupInventoryGood_Item cupInventoryGood_Item2 in items)
		{
			cupInventoryGood_Item2.GetComponent<CupInventory_InventoryItem>().lockMouse = false;
		}
		yield break;
	}

	// Token: 0x04000123 RID: 291
	public float itemAppearanceTime = 0.33f;

	// Token: 0x04000124 RID: 292
	public Transform cup;
}
