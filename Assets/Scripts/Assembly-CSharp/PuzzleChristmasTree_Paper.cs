using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class PuzzleChristmasTree_Paper : EnhancedDraggable
{
	// Token: 0x060000CC RID: 204 RVA: 0x00009590 File Offset: 0x00007790
	private void Update()
	{
		if (this.insideBox && !this.dragged)
		{
			this.destructionTimer = Mathf.MoveTowards(this.destructionTimer, 0f, Time.deltaTime);
			if (this.destructionTimer == 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x000095E9 File Offset: 0x000077E9
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.layer != LayerMask.NameToLayer("Front"))
		{
			return;
		}
		if (!this.insideBox)
		{
			this.destructionTimer = this.waitBeforeDestruction;
		}
		this.insideBox = true;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00009624 File Offset: 0x00007824
	private void OnTriggerExit2D()
	{
		this.insideBox = false;
	}

	// Token: 0x04000155 RID: 341
	public float waitBeforeDestruction;

	// Token: 0x04000156 RID: 342
	private float destructionTimer;

	// Token: 0x04000157 RID: 343
	private bool insideBox;
}
