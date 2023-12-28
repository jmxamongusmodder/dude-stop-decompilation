using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class PuzzleMarcoPolo_Person : MonoBehaviour
{
	// Token: 0x06000121 RID: 289 RVA: 0x0000B010 File Offset: 0x00009210
	private void Start()
	{
		this.rends = base.transform.GetComponentsInChildren<SpriteRenderer>();
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000B023 File Offset: 0x00009223
	private void OnMouseUp()
	{
		PuzzleMarcoPolo_Controller.person = base.transform;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000B030 File Offset: 0x00009230
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		foreach (SpriteRenderer spriteRenderer in this.rends)
		{
			spriteRenderer.material.SetFloat("_Alpha", this.whiten);
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000B080 File Offset: 0x00009280
	private void OnMouseExit()
	{
		foreach (SpriteRenderer spriteRenderer in this.rends)
		{
			spriteRenderer.material.SetFloat("_Alpha", 0f);
		}
	}

	// Token: 0x040001B7 RID: 439
	public float whiten;

	// Token: 0x040001B8 RID: 440
	private SpriteRenderer[] rends;
}
