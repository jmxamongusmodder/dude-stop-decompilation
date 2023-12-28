using System;
using UnityEngine;

// Token: 0x02000438 RID: 1080
[EnabledManually]
public class PuzzleNonWaitingBus_Driver : MonoBehaviour
{
	// Token: 0x06001B89 RID: 7049 RVA: 0x000720AC File Offset: 0x000704AC
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.door.ActivateDoor();
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x000720C8 File Offset: 0x000704C8
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.material.SetFloat("_Alpha", this.whiten);
		}
		foreach (SpriteRenderer spriteRenderer2 in this.panelOrDriver.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer2.material.SetFloat("_Alpha", this.whiten);
		}
		this.door.OnMouseEnter();
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x00072164 File Offset: 0x00070564
	private void OnMouseExit()
	{
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.material.SetFloat("_Alpha", 0f);
		}
		foreach (SpriteRenderer spriteRenderer2 in this.panelOrDriver.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer2.material.SetFloat("_Alpha", 0f);
		}
		this.door.OnMouseExit();
	}

	// Token: 0x040019CF RID: 6607
	public PuzzleNonWaitingBus_Door door;

	// Token: 0x040019D0 RID: 6608
	public Transform panelOrDriver;

	// Token: 0x040019D1 RID: 6609
	public float whiten = 0.35f;
}
