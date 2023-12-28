using System;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public class PuzzleDeodorant_Door : MonoBehaviour
{
	// Token: 0x0600197B RID: 6523 RVA: 0x0005F58F File Offset: 0x0005D98F
	private void Update()
	{
		this.clickOnLock();
		this.canNearLock();
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x0005F5A0 File Offset: 0x0005D9A0
	private void clickOnLock()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 b = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Vector2.Distance(this.lockPos, b) < this.lockSizeForClick)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_Deodorant>().clickOnLock();
			}
		}
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x0005F5F8 File Offset: 0x0005D9F8
	private void canNearLock()
	{
		Rect rect = new Rect(this.lockPos + this.offsetX, this.lockPosForSpray);
		if (rect.Contains(this.Can.position))
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_Deodorant>().sprayOnLock();
		}
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x0005F650 File Offset: 0x0005DA50
	public void RemoveLock()
	{
		Transform child = base.transform.GetChild(2);
		child.GetChild(1).gameObject.SetActive(false);
		child.GetChild(2).gameObject.SetActive(true);
		child.GetComponent<Rigidbody2D>().isKinematic = false;
		UnityEngine.Object.Destroy(child.gameObject, 3f);
		this.human.gameObject.SetActive(true);
		this.human.GetComponent<Animator>().SetBool("show", true);
		Global.self.currPuzzle.GetComponent<AudioVoice_Deodorant>().showGuy();
		this.GetComponentInPuzzleStats<PuzzleDeodorant_Can>().enableTracking = true;
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x0005F6F1 File Offset: 0x0005DAF1
	public void Open()
	{
		base.transform.GetChild(0).gameObject.SetActive(false);
		base.transform.GetChild(1).gameObject.SetActive(true);
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x0005F721 File Offset: 0x0005DB21
	public void Close()
	{
		base.transform.GetChild(0).gameObject.SetActive(true);
		base.transform.GetChild(1).gameObject.SetActive(false);
	}

	// Token: 0x04001784 RID: 6020
	public Transform human;

	// Token: 0x04001785 RID: 6021
	[Space(10f)]
	public Vector2 lockPos;

	// Token: 0x04001786 RID: 6022
	public float lockSizeForClick;

	// Token: 0x04001787 RID: 6023
	public Vector2 lockPosForSpray;

	// Token: 0x04001788 RID: 6024
	public Vector2 offsetX;

	// Token: 0x04001789 RID: 6025
	public Transform Can;
}
