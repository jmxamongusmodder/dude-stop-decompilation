using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200042F RID: 1071
public class PuzzleMotherCall_BlackPen : PuzzleMotherCall_RedPen
{
	// Token: 0x06001B46 RID: 6982 RVA: 0x0007005A File Offset: 0x0006E45A
	public override void Start()
	{
		this.holder = this.FindChildWithTag(this.GetPuzzleStats().transform, "SuccessCollider");
		this.snapToHolder = false;
		base.Start();
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x00070085 File Offset: 0x0006E485
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (this.dragged && this.stateSet)
		{
			Global.setCompletionState(CompletionState.None, base.GetComponent<PuzzleFriendsPen_BlackPen>().originalPuzzle);
			this.stateSet = false;
		}
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x000700BC File Offset: 0x0006E4BC
	public override void OnMouseUp()
	{
		if (base.Snapped() && (base.GetSnapPoint().type == Draggable.Snap.XY || this.lockSnapPoint))
		{
			this.returnToInventory = false;
		}
		else
		{
			this.returnToInventory = true;
		}
		base.OnMouseUp();
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x00070109 File Offset: 0x0006E509
	protected override void FinishedInsertion()
	{
		if (!this.stateSet)
		{
			Global.setCompletionState(CompletionState.Monster, base.GetComponent<PuzzleFriendsPen_BlackPen>().originalPuzzle);
			this.stateSet = true;
			Global.self.currPuzzle.GetComponent<AudioVoice_MamaCall>().putPenIn();
		}
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x00070143 File Offset: 0x0006E543
	protected override void ChangeLooks()
	{
		base.transform.localScale = new Vector3(this.scale, this.scale);
		base.SetLayers(this.originalLayer);
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x00070170 File Offset: 0x0006E570
	private Transform FindChildWithTag(Transform root, string tag)
	{
		Transform transform = null;
		IEnumerator enumerator = root.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform2 = (Transform)obj;
				if (transform2.tag == tag)
				{
					transform = transform2;
				}
				else if (transform2.childCount > 0)
				{
					transform = this.FindChildWithTag(transform2, tag);
				}
				if (transform != null)
				{
					break;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		return transform;
	}

	// Token: 0x04001979 RID: 6521
	private bool stateSet;
}
