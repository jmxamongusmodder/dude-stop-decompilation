using System;
using UnityEngine;

// Token: 0x02000540 RID: 1344
public class DuckInventory_Draggable : InventoryDraggable
{
	// Token: 0x06001EB3 RID: 7859 RVA: 0x0008EAAC File Offset: 0x0008CEAC
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		this.returnToPoint = true;
		if (!this.soundPlayed)
		{
			Audio.self.playOneShot("904fdf37-800e-4db3-88de-443c9ded75cc", 1f);
			this.soundPlayed = true;
		}
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x0008EAFC File Offset: 0x0008CEFC
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		if (!this.dragged && this.returnToPoint)
		{
			base.transform.Rotate(Vector3.forward * this.rotationSpeed * Time.deltaTime);
		}
	}

	// Token: 0x06001EB5 RID: 7861 RVA: 0x0008EB4C File Offset: 0x0008CF4C
	public override void OnReturnPositionReached()
	{
		base.OnReturnPositionReached();
		this.particles.transform.SetParent(base.transform.parent);
		this.particles.transform.position = base.transform.position;
		this.particles.transform.localScale = Vector3.one;
		this.particles.SetActive(true);
		Audio.self.playOneShot("aa626233-4379-4159-b838-9f7c4ea4d3b7", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_Pack09>().duckReached();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040021DA RID: 8666
	public float rotationSpeed;

	// Token: 0x040021DB RID: 8667
	private bool soundPlayed;

	// Token: 0x040021DC RID: 8668
	public GameObject particles;
}
