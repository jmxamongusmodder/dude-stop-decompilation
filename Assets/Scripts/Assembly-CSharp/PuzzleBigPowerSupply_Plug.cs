using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020003C4 RID: 964
public class PuzzleBigPowerSupply_Plug : EnhancedDraggable
{
	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600181B RID: 6171 RVA: 0x00053395 File Offset: 0x00051795
	protected List<SnapPoint> globalSnapPoints
	{
		get
		{
			if (this._globalSnapPoints == null)
			{
				this._globalSnapPoints = this.GetComponentInPuzzleStats<PuzzleBigPowerSupply_SnapPoints>();
			}
			return this._globalSnapPoints.globalSnapPoints;
		}
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000533C0 File Offset: 0x000517C0
	public virtual void Start()
	{
		this.inserted = true;
		this.InitGlobalSnapPoints();
		this.InitLocalSnapPoint();
		this.snapEnablingY = this.globalSnapPoints[0].transform.position.y;
		this.savedLocalPosition = base.transform.localPosition;
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x0005341C File Offset: 0x0005181C
	public virtual void Update()
	{
		this.PlayInsertionSound();
		base.snapEnabled = (base.transform.position.y < this.snapEnablingY);
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x00053450 File Offset: 0x00051850
	protected override void MouseDowned()
	{
		if (this.lockedSnapPoint != null)
		{
			this.lockSnapPoint = false;
		}
		this.InitMouseDownSnapPoints();
		this.ShiftSprites();
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x00053470 File Offset: 0x00051870
	protected override void BeforeMouseUpped()
	{
		this.LockSnapPoint();
		this.Update();
		this.FixedUpdate();
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x00053484 File Offset: 0x00051884
	protected override void MouseUpped()
	{
		this.savedLocalPosition = base.transform.localPosition;
		if (base.Snapped())
		{
			base.StartCoroutine(this.InsertionCoroutine(base.GetSnapPoint()));
		}
		else
		{
			this.lockSnapPoint = false;
		}
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x000534D1 File Offset: 0x000518D1
	protected override void OnSnap(SnapPoint point)
	{
		this.limit.bottomVal = this.finalPos;
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x000534E4 File Offset: 0x000518E4
	protected override void OnUnsnap(SnapPoint point)
	{
		this.limit.bottomVal = this.defaultBottomLimit;
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x000534F8 File Offset: 0x000518F8
	private void PlayInsertionSound()
	{
		if (this.inserted && base.transform.position.y > this.removeY)
		{
			this.inserted = false;
			Audio.self.playOneShot("468332a9-9a38-43f1-908d-ba5482bcb26e", 1f);
		}
		else if (base.Snapped() && !this.inserted && base.transform.position.y < this.insertY)
		{
			this.inserted = true;
			Audio.self.playOneShot("93877586-f9db-41d9-a8c3-dca1c6359133", 1f);
		}
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x000535A0 File Offset: 0x000519A0
	private void ShiftSprites()
	{
		int sortingOrder = base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
		foreach (PuzzleBigPowerSupply_Plug puzzleBigPowerSupply_Plug in this.GetComponentsInPuzzleStats(false))
		{
			if (!(puzzleBigPowerSupply_Plug == this))
			{
				if (puzzleBigPowerSupply_Plug.transform.position.z <= base.transform.position.z)
				{
					puzzleBigPowerSupply_Plug.transform.position = new Vector3(puzzleBigPowerSupply_Plug.transform.position.x, puzzleBigPowerSupply_Plug.transform.position.y, puzzleBigPowerSupply_Plug.transform.position.z + 0.1f);
				}
				foreach (SpriteRenderer spriteRenderer in puzzleBigPowerSupply_Plug.GetComponentsInChildren<SpriteRenderer>())
				{
					if (spriteRenderer.sortingOrder >= sortingOrder)
					{
						spriteRenderer.sortingOrder -= 2;
					}
				}
			}
		}
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -0.1f);
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;
		base.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = -1;
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x00053723 File Offset: 0x00051B23
	protected virtual void LockSnapPoint()
	{
		if (!base.Snapped())
		{
			return;
		}
		this.lockSnapPoint = true;
		this.lockedSnapPoint = base.GetSnapPoint();
		this.globalSnapPoints.Remove(base.GetSnapPoint());
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x00053758 File Offset: 0x00051B58
	protected virtual bool CheckVictoryCondition()
	{
		if (this.globalSnapPoints == null || this.globalSnapPoints.Count > 0)
		{
			return false;
		}
		if (this.GetComponentsInPuzzleStats(false).Count((PuzzleBigPowerSupply_Plug x) => x.lockedSnapPoint == null) > 0)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
		return true;
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x000537D0 File Offset: 0x00051BD0
	private void InitGlobalSnapPoints()
	{
		if (this.globalSnapPoints != null && this.globalSnapPoints.Count > 0)
		{
			return;
		}
		IEnumerator enumerator = this.snapPointParent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform t = (Transform)enumerator.Current;
				if ((from x in this.globalSnapPoints
				where x.transform == t
				select x).Count<SnapPoint>() <= 0)
				{
					SnapPoint item = new SnapPoint(Draggable.Snap.X, t.position.x, this.snapDist, t);
					this.globalSnapPoints.Add(item);
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
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x000538B4 File Offset: 0x00051CB4
	private void InitLocalSnapPoint()
	{
		this.lockedSnapPoint = null;
		SnapPoint snapPoint = (from x in this.globalSnapPoints
		where x.transform == this.startingPoint
		select x).FirstOrDefault<SnapPoint>();
		if (snapPoint == null)
		{
			return;
		}
		this.globalSnapPoints.Remove(snapPoint);
		this.lockedSnapPoint = snapPoint;
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x00053900 File Offset: 0x00051D00
	private IEnumerator InsertionCoroutine(SnapPoint lockedSnapPoint)
	{
		base.GetComponent<Collider2D>().enabled = false;
		float timer = 0f;
		Vector2 start = base.transform.localPosition;
		Vector2 end = start;
		end.y = this.finalPos;
		if (start != end)
		{
			while (timer != this.insertionTime)
			{
				timer = Mathf.MoveTowards(timer, this.insertionTime, Time.deltaTime);
				float t = Mathf.Sin(timer / this.insertionTime * 3.1415927f * 0.5f);
				base.transform.localPosition = Vector2.Lerp(start, end, t);
				this.savedLocalPosition = base.transform.localPosition;
				yield return null;
			}
		}
		this.savedLocalPosition = end;
		this.CheckVictoryCondition();
		base.GetComponent<Collider2D>().enabled = true;
		yield break;
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x0005391C File Offset: 0x00051D1C
	protected virtual void InitMouseDownSnapPoints()
	{
		base.RemoveAllSnapPoints();
		foreach (SnapPoint point in this.globalSnapPoints)
		{
			base.AddSnapPoint(point, false);
		}
		if (this.lockedSnapPoint != null)
		{
			base.AddSnapPoint(this.lockedSnapPoint, true);
			this.globalSnapPoints.Add(this.lockedSnapPoint);
			this.lockedSnapPoint = null;
		}
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x000539B0 File Offset: 0x00051DB0
	protected override Vector3 ProcessMousePosition(Vector3 mouse, Vector3 delta)
	{
		Vector3 result = base.ProcessMousePosition(mouse, delta);
		if (result.y < this.limit.bottomVal)
		{
			result.y = this.limit.bottomVal;
		}
		return result;
	}

	// Token: 0x0400160C RID: 5644
	public Transform snapPointParent;

	// Token: 0x0400160D RID: 5645
	public Transform startingPoint;

	// Token: 0x0400160E RID: 5646
	public float insertionTime = 0.5f;

	// Token: 0x0400160F RID: 5647
	public float removingTime = 0.2f;

	// Token: 0x04001610 RID: 5648
	public float snapDist = 0.3f;

	// Token: 0x04001611 RID: 5649
	public float finalPos = -0.057f;

	// Token: 0x04001612 RID: 5650
	public float defaultBottomLimit = 0.7f;

	// Token: 0x04001613 RID: 5651
	private PuzzleBigPowerSupply_SnapPoints _globalSnapPoints;

	// Token: 0x04001614 RID: 5652
	[HideInInspector]
	public SnapPoint lockedSnapPoint;

	// Token: 0x04001615 RID: 5653
	private float snapEnablingY;

	// Token: 0x04001616 RID: 5654
	[HideInInspector]
	public Vector2 savedLocalPosition;

	// Token: 0x04001617 RID: 5655
	[Header("Insertion sound")]
	public float removeY;

	// Token: 0x04001618 RID: 5656
	public float insertY;

	// Token: 0x04001619 RID: 5657
	protected bool inserted = true;
}
