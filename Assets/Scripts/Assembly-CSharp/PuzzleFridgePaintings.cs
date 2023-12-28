using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000406 RID: 1030
[RequireComponent(typeof(BoxCollider2D))]
public class PuzzleFridgePaintings : Draggable
{
	// Token: 0x06001A1D RID: 6685 RVA: 0x00064E99 File Offset: 0x00063299
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		GizmosExtension.DrawHorizontalLine(this.throwUpY, Color.magenta, -10f, 10f);
	}

	// Token: 0x06001A1E RID: 6686 RVA: 0x00064EBC File Offset: 0x000632BC
	private void Start()
	{
		this.scaleDelta = true;
		this.startingScale = base.transform.localScale.x;
		this.startingRotation = base.transform.localEulerAngles.z;
		List<SnapPoint> list = new List<SnapPoint>();
		foreach (Transform transform in this.magnets)
		{
			list.Add(new SnapPoint(Draggable.Snap.XY, transform.position, this.snapDist, transform));
		}
		base.SetSnapPoints(list);
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x00064F78 File Offset: 0x00063378
	private void Update()
	{
		if (base.transform.position.y < this.binY)
		{
			base.snapEnabled = false;
		}
		else
		{
			base.snapEnabled = true;
		}
		if (!this.dragged && !this.lockSnapPoint && base.WasMoved())
		{
			this.returnTimer = Mathf.MoveTowards(this.returnTimer, this.returnTime, Time.deltaTime);
			float num = this.returnTimer / this.returnTime;
			num = Mathf.Sin(num * 3.1415927f * 0.5f);
			base.transform.position = Vector3.Lerp(this.returnPosition, this.startingPosition, num);
			base.transform.localScale = Vector3.Lerp(new Vector2(this.returnScale, this.returnScale), new Vector2(this.startingScale, this.startingScale), num);
		}
		if (base.Snapped() && base.GetSnapPoint().type == Draggable.Snap.XY)
		{
			this.UpdateRotation(base.GetSnapPoint().transform.GetChild(0).localEulerAngles.z);
		}
		else if (!base.Snapped() && this.dragged)
		{
			this.UpdateRotation(0f);
		}
		else if (!this.dragged)
		{
			this.UpdateRotation(this.startingRotation);
		}
		this.UpdatePaintingScale();
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x000650F6 File Offset: 0x000634F6
	private void OnDisable()
	{
		if (this.dragged)
		{
			this.dragged = false;
		}
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x0006510C File Offset: 0x0006350C
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.SwitchLayers();
		if (this.lockSnapPoint && base.GetSnapPoint().type == Draggable.Snap.XY)
		{
			this.lockSnapPoint = false;
			this.usedMagnet = base.GetSnapPoint();
		}
	}

	// Token: 0x06001A22 RID: 6690 RVA: 0x00065160 File Offset: 0x00063560
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (this.usedMagnet != null)
		{
			this.AddMagnet(this.usedMagnet);
			this.usedMagnet = null;
		}
		if (!base.Snapped())
		{
			this.returnPosition = base.transform.position;
			this.returnTimer = 0f;
			this.returnScale = base.transform.localScale.x;
			if (Global.getCompletionState(null) != CompletionState.Monster)
			{
				Global.setCompletionState(CompletionState.None, null);
			}
		}
		else if (base.GetSnapPoint().type == Draggable.Snap.X)
		{
			base.StartCoroutine(this.ThrowingAwayCoroutine());
			this.lockSnapPoint = true;
		}
		else
		{
			this.RemoveMagnet(base.GetSnapPoint().transform);
			this.lockSnapPoint = true;
			int num = this.PaintingCount();
			if (num > 1 && this.AllPaintingsLocked())
			{
				Global.setCompletionState(CompletionState.Good, null);
				if (num == 3 && num == this.AlignedPaintingCount())
				{
					Global.self.GetCup(AwardName.FRIDGE);
					Global.self.currPuzzle.GetComponent<AudioVoice_Fridge>().hangPics(true);
				}
				else
				{
					Global.self.currPuzzle.GetComponent<AudioVoice_Fridge>().hangPics(false);
				}
			}
		}
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x000652A8 File Offset: 0x000636A8
	public void AddBin(float binX, float binY)
	{
		this.binY = binY;
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.X, binX, this.snapDist), false);
	}

	// Token: 0x06001A24 RID: 6692 RVA: 0x000652C8 File Offset: 0x000636C8
	private void SwitchLayers()
	{
		string sortingLayerName = base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName;
		if (sortingLayerName == "Top")
		{
			return;
		}
		if (sortingLayerName == "Default")
		{
			this.SwitchSortingLayers("Top", "Default", 0.1f);
		}
		else if (sortingLayerName == "Background")
		{
			this.SwitchSortingLayers("Default", "Background", 0.2f);
			this.SwitchSortingLayers("Top", "Default", 0.1f);
		}
		this.SetSortingLayer("Top", 0f);
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x00065374 File Offset: 0x00063774
	private void SwitchSortingLayers(string sourceLayer, string targetLayer, float zPos)
	{
		PuzzleFridgePaintings puzzleFridgePaintings = (from x in this.GetComponentsInPuzzleStats(false)
		where x.transform.GetComponentInChildren<SpriteRenderer>().sortingLayerName == sourceLayer
		select x).FirstOrDefault<PuzzleFridgePaintings>();
		if (puzzleFridgePaintings != null)
		{
			this.SetSortingLayer(puzzleFridgePaintings, targetLayer, zPos);
		}
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x000653C4 File Offset: 0x000637C4
	private void UpdatePaintingScale()
	{
		if (this.dragged && base.WasMoved())
		{
			if (base.transform.position.x <= this.startingPosition.x)
			{
				base.transform.localScale = new Vector3(this.startingScale, this.startingScale, this.startingScale);
			}
			else if (base.transform.position.x <= this.startingPosition.x + this.fridgeScaleDistance)
			{
				float t = (base.transform.position.x - this.startingPosition.x) / this.fridgeScaleDistance;
				base.transform.localScale = Vector3.Lerp(new Vector3(this.startingScale, this.startingScale), new Vector3(this.fridgeScale, this.fridgeScale), t);
			}
			else
			{
				base.transform.localScale = new Vector3(this.fridgeScale, this.fridgeScale);
			}
		}
	}

	// Token: 0x06001A27 RID: 6695 RVA: 0x000654D8 File Offset: 0x000638D8
	private void RemoveMagnet(Transform magnet)
	{
		foreach (PuzzleFridgePaintings puzzleFridgePaintings in base.transform.parent.GetComponentsInChildren<PuzzleFridgePaintings>())
		{
			if (puzzleFridgePaintings != this)
			{
				puzzleFridgePaintings.RemoveSnapPoint(magnet);
			}
		}
	}

	// Token: 0x06001A28 RID: 6696 RVA: 0x00065524 File Offset: 0x00063924
	private void AddMagnet(SnapPoint magnet)
	{
		foreach (PuzzleFridgePaintings puzzleFridgePaintings in base.transform.parent.GetComponentsInChildren<PuzzleFridgePaintings>())
		{
			if (puzzleFridgePaintings != this)
			{
				puzzleFridgePaintings.AddSnapPoint(magnet, false);
			}
		}
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x00065570 File Offset: 0x00063970
	private bool AllPaintingsLocked()
	{
		foreach (PuzzleFridgePaintings puzzleFridgePaintings in base.transform.parent.GetComponentsInChildren<PuzzleFridgePaintings>())
		{
			if (!puzzleFridgePaintings.lockSnapPoint)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x000655B4 File Offset: 0x000639B4
	private int LockedPaintingCount()
	{
		return (from x in base.transform.parent.GetComponentsInChildren<PuzzleFridgePaintings>()
		where x.lockSnapPoint
		select x).Count<PuzzleFridgePaintings>();
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x000655ED File Offset: 0x000639ED
	private int PaintingCount()
	{
		return base.transform.parent.GetComponentsInChildren<PuzzleFridgePaintings>().Length;
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x00065601 File Offset: 0x00063A01
	private int AlignedPaintingCount()
	{
		return (from x in base.transform.parent.GetComponentsInChildren<PuzzleFridgePaintings>()
		where x.Snapped() && x.GetSnapPoint().transform == x.idealMagnet
		select x).Count<PuzzleFridgePaintings>();
	}

	// Token: 0x06001A2D RID: 6701 RVA: 0x0006563A File Offset: 0x00063A3A
	private void SetSortingLayer(string layer, float zPos)
	{
		this.SetSortingLayer(this, layer, zPos);
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x00065648 File Offset: 0x00063A48
	private void SetSortingLayer(PuzzleFridgePaintings target, string layer, float zPos)
	{
		foreach (SpriteRenderer spriteRenderer in target.transform.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = layer;
		}
		target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, zPos);
		target.returnPosition.z = zPos;
		target.startingPosition.z = zPos;
		if (target.Snapped() && target.GetSnapPoint().transform != null)
		{
			target.GetSnapPoint().transform.GetComponent<SpriteRenderer>().sortingLayerName = layer;
		}
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x00065708 File Offset: 0x00063B08
	private void UpdateRotation(float target)
	{
		if (this.targetRotation != target)
		{
			this.targetRotation = target;
			this.returnRotation = base.transform.localEulerAngles.z;
			this.rotationTimer = 0f;
		}
		else if (this.rotationTimer != -1f)
		{
			this.rotationTimer = Mathf.MoveTowards(this.rotationTimer, this.rotationTime, Time.deltaTime);
			float num = this.rotationTimer / this.rotationTime;
			num = Mathf.Sin(num * 3.1415927f * 0.5f);
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.returnRotation, this.targetRotation, num));
			if (this.rotationTimer == this.rotationTime)
			{
				this.rotationTimer = -1f;
			}
		}
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x000657E8 File Offset: 0x00063BE8
	private IEnumerator ThrowingAwayCoroutine()
	{
		bool throwUp = base.transform.position.y < this.throwUpY;
		float overallTime = Mathf.Max(this.crumplingTime, this.binCurve.GetAnimationLength());
		Global.self.canBePaused = false;
		Global.self.scrollableUI.GetComponent<scrollablePackArrows>().pauseScrolling(overallTime + 0.1f);
		yield return base.StartCoroutine(this.CrumplingCoroutine(throwUp));
		this.CheckCompletionStatus();
		Global.self.canBePaused = true;
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x00065804 File Offset: 0x00063C04
	private IEnumerator CrumplingCoroutine(bool throwUp = false)
	{
		base.GetComponent<BoxCollider2D>().enabled = false;
		this.dragEnabled = false;
		float crumplingTimer = 0f;
		float startY = base.transform.position.y;
		Global.self.currPuzzle.GetComponent<AudioVoice_Fridge>().throwPic();
		Audio.self.playOneShot("2637b13b-602d-4b38-95c7-e84275ab33e8", 1f);
		while (crumplingTimer != this.crumplingTime)
		{
			crumplingTimer = Mathf.MoveTowards(crumplingTimer, this.crumplingTime, Time.deltaTime);
			float t = crumplingTimer / this.crumplingTime;
			base.transform.localScale = Vector3.Lerp(new Vector3(this.fridgeScale, this.fridgeScale), new Vector3(this.crumplingScale, this.crumplingScale), t);
			if (throwUp)
			{
				base.transform.SetY(startY + this.binCurve.Evaluate(crumplingTimer), false);
			}
			yield return null;
		}
		Transform crumpled = UnityEngine.Object.Instantiate<Transform>(this.crumpledPaper);
		crumpled.position = base.transform.position;
		crumpled.gameObject.SetActive(true);
		crumpled.SetParent(base.transform.parent);
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.enabled = false;
		}
		if (throwUp)
		{
			crumpled.GetComponent<Rigidbody2D>().isKinematic = true;
			while (crumplingTimer < this.binCurve.GetAnimationLength())
			{
				crumplingTimer = Mathf.MoveTowards(crumplingTimer, this.binCurve.GetAnimationLength(), Time.deltaTime);
				crumpled.SetY(startY + this.binCurve.Evaluate(crumplingTimer), false);
				yield return null;
			}
			crumpled.GetComponent<Rigidbody2D>().isKinematic = false;
		}
		crumpled.GetComponent<SpriteRenderer>().sortingOrder = -5;
		yield break;
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x00065828 File Offset: 0x00063C28
	private void CheckCompletionStatus()
	{
		int num = this.PaintingCount();
		if (num == 3)
		{
			if (this.LockedPaintingCount() == 2)
			{
				Global.setCompletionState(CompletionState.Good, null);
			}
		}
		else if (num == 2)
		{
			Global.setCompletionState(CompletionState.Monster, null);
		}
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x00065870 File Offset: 0x00063C70
	protected override void OnSnap(SnapPoint point)
	{
		if (point == null || point.transform == null)
		{
			return;
		}
		SpriteRenderer component = point.transform.GetComponent<SpriteRenderer>();
		component.sortingLayerName = base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName;
		component.sortingOrder = Mathf.Abs(component.sortingOrder);
		Audio.self.playOneShot("5dca2fc9-e00f-4421-9bd4-3c7bcf036293", 1f);
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x000658E4 File Offset: 0x00063CE4
	protected override void OnUnsnap(SnapPoint point)
	{
		if (point == null || point.transform == null)
		{
			return;
		}
		SpriteRenderer component = point.transform.GetComponent<SpriteRenderer>();
		component.sortingLayerName = "Background";
		component.sortingOrder = -Mathf.Abs(component.sortingOrder);
	}

	// Token: 0x04001828 RID: 6184
	[Header("Painting stuff")]
	public AnimationCurve binCurve;

	// Token: 0x04001829 RID: 6185
	public Transform crumpledPaper;

	// Token: 0x0400182A RID: 6186
	[Tooltip("Seconds required for the paper to crumple")]
	public float crumplingTime = 0.4f;

	// Token: 0x0400182B RID: 6187
	[Tooltip("Target scale before crumpling")]
	public float crumplingScale = 0.1f;

	// Token: 0x0400182C RID: 6188
	private float binY = -5f;

	// Token: 0x0400182D RID: 6189
	public List<Transform> magnets = new List<Transform>();

	// Token: 0x0400182E RID: 6190
	public Transform idealMagnet;

	// Token: 0x0400182F RID: 6191
	[Tooltip("Seconds required to rotate the object to magnet angle")]
	public float rotationTime = 0.7f;

	// Token: 0x04001830 RID: 6192
	[Tooltip("Distance for snap points")]
	public float snapDist = 0.3f;

	// Token: 0x04001831 RID: 6193
	public float throwUpY = 1.8f;

	// Token: 0x04001832 RID: 6194
	[Tooltip("Scale for fridge display")]
	public float fridgeScale = 1f;

	// Token: 0x04001833 RID: 6195
	[Tooltip("The distance the painting passes before reaching fridge scale")]
	public float fridgeScaleDistance = 2f;

	// Token: 0x04001834 RID: 6196
	[Tooltip("Seconds required for the painting to return back")]
	public float returnTime = 0.5f;

	// Token: 0x04001835 RID: 6197
	private float targetRotation;

	// Token: 0x04001836 RID: 6198
	private float rotationTimer;

	// Token: 0x04001837 RID: 6199
	private float startingScale;

	// Token: 0x04001838 RID: 6200
	private float startingRotation;

	// Token: 0x04001839 RID: 6201
	private float returnScale;

	// Token: 0x0400183A RID: 6202
	private float returnRotation;

	// Token: 0x0400183B RID: 6203
	private Vector3 returnPosition;

	// Token: 0x0400183C RID: 6204
	private float returnTimer;

	// Token: 0x0400183D RID: 6205
	private SnapPoint usedMagnet;
}
