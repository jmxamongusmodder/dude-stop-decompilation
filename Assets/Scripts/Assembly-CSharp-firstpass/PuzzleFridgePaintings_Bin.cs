using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000407 RID: 1031
[EnabledManually]
public class PuzzleFridgePaintings_Bin : InventoryDraggable
{
	// Token: 0x06001A38 RID: 6712 RVA: 0x00065E28 File Offset: 0x00064228
	protected override void ChangeLooks()
	{
		foreach (PolygonCollider2D polygonCollider2D in base.GetComponents<PolygonCollider2D>())
		{
			if (!polygonCollider2D.isTrigger)
			{
				polygonCollider2D.enabled = true;
			}
			else
			{
				polygonCollider2D.enabled = false;
			}
		}
		foreach (PuzzleFridgePaintings puzzleFridgePaintings in this.GetComponentsInPuzzleStats(false))
		{
			puzzleFridgePaintings.AddBin(this.inventoryReturnPoint.x, this.inventoryReturnPoint.y);
		}
		base.transform.GetChild(1).gameObject.SetActive(false);
		base.transform.GetChild(2).gameObject.SetActive(true);
		base.StartCoroutine(this.RotationCoroutine());
		this.returnToPoint = true;
	}

	// Token: 0x06001A39 RID: 6713 RVA: 0x00065EF8 File Offset: 0x000642F8
	public override void OnReturnPositionReached()
	{
		this.dragEnabled = false;
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x00065F04 File Offset: 0x00064304
	private IEnumerator RotationCoroutine()
	{
		float rotationTimer = 0f;
		float startingAngle = base.transform.eulerAngles.z;
		while (rotationTimer != this.rotationTime)
		{
			rotationTimer = Mathf.MoveTowards(rotationTimer, this.rotationTime, Time.deltaTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(startingAngle, 0f, rotationTimer / this.rotationTime));
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001840 RID: 6208
	public Transform sprites;

	// Token: 0x04001841 RID: 6209
	public float rotationTime = 0.5f;
}
