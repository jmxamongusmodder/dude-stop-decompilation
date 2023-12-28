using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200035A RID: 858
public class CupLastCup_Cup : EnhancedDraggable
{
	// Token: 0x060014E9 RID: 5353 RVA: 0x0003B27C File Offset: 0x0003967C
	private void Start()
	{
		if (this.removeOnTimer)
		{
			base.StartCoroutine(this.RemoveOnTimer());
		}
		else
		{
			base.StartCoroutine(this.RemovalCoroutine());
		}
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x0003B2A8 File Offset: 0x000396A8
	private void OnTriggerExit2D(Collider2D other)
	{
		if (this.collided)
		{
			return;
		}
		if (other.GetComponent<CupLastCup_Sign>() == null)
		{
			return;
		}
		base.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		base.GetComponent<SpriteRenderer>().sortingOrder = 0;
		base.gameObject.layer = LayerMask.NameToLayer("Front");
		base.GetComponent<Collider2D>().isTrigger = false;
		this.collided = true;
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x0003B318 File Offset: 0x00039718
	private IEnumerator RemoveOnTimer()
	{
		yield return new WaitForSeconds(this.removeTimer);
		base.GetComponent<Collider2D>().isTrigger = true;
		yield break;
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x0003B334 File Offset: 0x00039734
	private IEnumerator RemovalCoroutine()
	{
		AudioVoice_LastCup voice = Global.self.currPuzzle.GetComponent<AudioVoice_LastCup>();
		while (!voice.canRemoveCup)
		{
			yield return null;
		}
		base.GetComponent<Collider2D>().isTrigger = true;
		yield break;
	}

	// Token: 0x0400127B RID: 4731
	[Space(10f)]
	public bool removeOnTimer;

	// Token: 0x0400127C RID: 4732
	public float removeTimer = 2f;

	// Token: 0x0400127D RID: 4733
	private bool collided;
}
