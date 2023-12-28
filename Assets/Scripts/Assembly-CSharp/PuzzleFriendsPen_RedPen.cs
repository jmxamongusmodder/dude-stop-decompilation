using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200040E RID: 1038
public class PuzzleFriendsPen_RedPen : DrawingPen
{
	// Token: 0x06001A5A RID: 6746 RVA: 0x00067260 File Offset: 0x00065660
	public override void OnEnable()
	{
		this.hand = this.GetComponentInPuzzleStats<PuzzleFriendsPen_Hand>();
		PuzzleMotherCall_BlackPen componentInPuzzleStats = this.GetComponentInPuzzleStats<PuzzleMotherCall_BlackPen>();
		if (componentInPuzzleStats != null && componentInPuzzleStats.holder == null)
		{
			PuzzleMotherCall_RedPen component = base.GetComponent<PuzzleMotherCall_RedPen>();
			componentInPuzzleStats.holder = component.holder;
		}
		base.OnEnable();
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x000672B8 File Offset: 0x000656B8
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (this.depleted)
		{
			base.StartCoroutine(this.showHandAfterVoice());
			base.GetComponent<Collider2D>().enabled = false;
			base.GetComponent<Rigidbody2D>().gravityScale = 1f;
			UnityEngine.Object.Destroy(base.gameObject, 5f);
		}
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x00067310 File Offset: 0x00065710
	private IEnumerator showHandAfterVoice()
	{
		Global.PauseArrows(-1f);
		while (Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().isPlaying())
		{
			yield return null;
		}
		Global.PauseArrows(-1f);
		yield return new WaitForSeconds(0.5f);
		Global.UnpauseArrows();
		this.hand.gameObject.SetActive(true);
		this.hand.MoveOut();
		Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().showNewPen();
		yield break;
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x0006732C File Offset: 0x0006572C
	protected override void ChangeLooks()
	{
		InventoryControl.self.mouseOnItemUp();
		UnityEngine.Object.Destroy(base.GetComponent<InventoryItem>());
		foreach (InventoryDraggable inventoryDraggable in base.GetComponents<InventoryDraggable>())
		{
			if (inventoryDraggable != this)
			{
				UnityEngine.Object.Destroy(inventoryDraggable);
			}
		}
		base.GetComponentInChildren<DrawingPenPoint>().availablePixels = this.availablePixels;
		base.transform.localScale = new Vector3(this.scale, this.scale);
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x000673B0 File Offset: 0x000657B0
	public override void Depleted()
	{
		foreach (DrawingCanvas drawingCanvas in this.GetComponentsInPuzzleStats(false))
		{
			drawingCanvas.overwrite = true;
		}
		this.depleted = true;
		Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().endPen();
	}

	// Token: 0x04001872 RID: 6258
	public float scale;

	// Token: 0x04001873 RID: 6259
	public int availablePixels;

	// Token: 0x04001874 RID: 6260
	protected PuzzleFriendsPen_Hand hand;

	// Token: 0x04001875 RID: 6261
	private bool depleted;
}
