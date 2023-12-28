using System;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public class PuzzleShapeInBox_Object : EnhancedDraggable
{
	// Token: 0x06001C02 RID: 7170 RVA: 0x000752A5 File Offset: 0x000736A5
	public virtual void Start()
	{
		this.lid = this.GetComponentInPuzzleStats<PuzzleShapeInBox_Lid>();
	}

	// Token: 0x06001C03 RID: 7171 RVA: 0x000752B4 File Offset: 0x000736B4
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag != "SuccessCollider")
		{
			return;
		}
		if (this.dragged)
		{
			return;
		}
		if (!base.Snapped() && !this.missedOnce && base.snapEnabled)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_ShapesInBox>().missHole();
			this.missedOnce = true;
		}
		if (!this.hitPlayed)
		{
			Audio.self.playOneShot("d8144d8a-a9b3-4003-98b1-805a44b89c7b", 1f);
			this.hitPlayed = true;
		}
		base.gameObject.SetActive(false);
		this.FinishedFalling();
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x00075358 File Offset: 0x00073758
	protected virtual void FinishedFalling()
	{
		this.lid.ShapeFinishedFalling();
	}

	// Token: 0x04001A62 RID: 6754
	protected PuzzleShapeInBox_Lid lid;

	// Token: 0x04001A63 RID: 6755
	private bool missedOnce;

	// Token: 0x04001A64 RID: 6756
	private bool hitPlayed;
}
