using System;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class RewardGiftLid : Draggable
{
	// Token: 0x060015B9 RID: 5561 RVA: 0x000437F8 File Offset: 0x00041BF8
	private void Update()
	{
		if (this.detached && !this.boxEnabled)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Front");
			this.box.gameObject.layer = LayerMask.NameToLayer("Back");
			this.box.gameObject.AddComponent<RewardCupContainer>();
			this.box.GetComponent<RewardCupContainer>().useRigidbody = true;
			base.body.constraints = RigidbodyConstraints2D.None;
			this.boxEnabled = true;
		}
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x0004387F File Offset: 0x00041C7F
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.detached = true;
		}
	}

	// Token: 0x04001370 RID: 4976
	public Transform box;

	// Token: 0x04001371 RID: 4977
	private bool detached;

	// Token: 0x04001372 RID: 4978
	private bool boxEnabled;
}
