using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020003C3 RID: 963
public class PuzzleBigPowerSupply_BigPlug : PuzzleBigPowerSupply_Plug
{
	// Token: 0x0600180C RID: 6156 RVA: 0x00053C1C File Offset: 0x0005201C
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawVerticalLine(base.transform.position.x + this.wobbleEffectDistance);
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x00053C48 File Offset: 0x00052048
	public override void Start()
	{
		base.Start();
		this.inserted = false;
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x00053C58 File Offset: 0x00052058
	public override void Update()
	{
		base.Update();
		if (this.dragged)
		{
			if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < this.wobbleMousePosition)
			{
				this.CheckWobble();
			}
			else
			{
				this.StopAllWobble();
			}
		}
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x00053CAE File Offset: 0x000520AE
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		this.StopAllWobble();
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x00053CBC File Offset: 0x000520BC
	protected override bool CheckVictoryCondition()
	{
		bool flag = base.CheckVictoryCondition();
		if (!flag && base.GetSnapPoint().transform.tag == "FailCollider")
		{
			this.GetComponentInPuzzleStats<AudioVoice_BigPowerSupply>().onWrongHole();
		}
		return flag;
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x00053D04 File Offset: 0x00052104
	private void CheckWobble()
	{
		foreach (PuzzleBigPowerSupply_Plug puzzleBigPowerSupply_Plug in from x in this.GetComponentsInPuzzleStats(false)
		where x != this
		select x)
		{
			if (puzzleBigPowerSupply_Plug.transform.position.x > base.transform.position.x && puzzleBigPowerSupply_Plug.transform.position.x < base.transform.position.x + this.wobbleEffectDistance && puzzleBigPowerSupply_Plug.lockedSnapPoint != null)
			{
				this.StartWobbling(puzzleBigPowerSupply_Plug);
				Global.self.currPuzzle.GetComponent<AudioVoice_BigPowerSupply>().onShake();
			}
			else
			{
				puzzleBigPowerSupply_Plug.transform.localPosition = puzzleBigPowerSupply_Plug.savedLocalPosition;
				this.wobbleListeners.Remove(puzzleBigPowerSupply_Plug);
			}
		}
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x00053E18 File Offset: 0x00052218
	private void StopAllWobble()
	{
		this.wobbling = false;
		foreach (PuzzleBigPowerSupply_Plug puzzleBigPowerSupply_Plug in this.wobbleListeners)
		{
			puzzleBigPowerSupply_Plug.transform.localPosition = puzzleBigPowerSupply_Plug.savedLocalPosition;
		}
		this.wobbleListeners.Clear();
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x00053E98 File Offset: 0x00052298
	protected override void LockSnapPoint()
	{
		if (!base.Snapped())
		{
			return;
		}
		this.lockedSnapPoint = base.GetSnapPoint();
		this.secondSnapPoint = this.GetNextPoint(this.lockedSnapPoint);
		this.thirdSnapPoint = this.GetNextPoint(this.secondSnapPoint);
		base.globalSnapPoints.Remove(this.lockedSnapPoint);
		base.globalSnapPoints.Remove(this.secondSnapPoint);
		base.globalSnapPoints.Remove(this.thirdSnapPoint);
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x00053F18 File Offset: 0x00052318
	protected override void InitMouseDownSnapPoints()
	{
		base.RemoveAllSnapPoints();
		if (this.lockedSnapPoint != null)
		{
			base.globalSnapPoints.Add(this.lockedSnapPoint);
			this.lockedSnapPoint = null;
		}
		if (this.secondSnapPoint != null)
		{
			base.globalSnapPoints.Add(this.secondSnapPoint);
			this.secondSnapPoint = null;
		}
		if (this.thirdSnapPoint != null)
		{
			base.globalSnapPoints.Add(this.thirdSnapPoint);
			this.thirdSnapPoint = null;
		}
		foreach (SnapPoint snapPoint in base.globalSnapPoints)
		{
			SnapPoint nextPoint = this.GetNextPoint(snapPoint);
			SnapPoint nextPoint2 = this.GetNextPoint(nextPoint);
			bool flag = false;
			flag |= this.LastPoint(snapPoint);
			flag |= (nextPoint != null && this.LastPoint(nextPoint));
			flag |= (nextPoint != null && nextPoint2 != null);
			if (flag)
			{
				base.AddSnapPoint(snapPoint, true);
			}
		}
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x00054034 File Offset: 0x00052434
	private bool LastPoint(SnapPoint p)
	{
		return p.transform.GetSiblingIndex() == p.transform.parent.childCount - 1;
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x00054058 File Offset: 0x00052458
	private SnapPoint GetNextPoint(SnapPoint p)
	{
		if (p == null)
		{
			return null;
		}
		Transform parent = p.transform.parent;
		if (p.transform.GetSiblingIndex() == parent.childCount - 1)
		{
			return null;
		}
		Transform next = parent.GetChild(p.transform.GetSiblingIndex() + 1);
		return (from x in base.globalSnapPoints
		where x.transform == next
		select x).FirstOrDefault<SnapPoint>();
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x000540CE File Offset: 0x000524CE
	private void StartWobbling(PuzzleBigPowerSupply_Plug plug)
	{
		if (!this.wobbling)
		{
			base.StartCoroutine(this.WobblingCoroutine());
		}
		if (!this.wobbleListeners.Contains(plug))
		{
			this.wobbleListeners.Add(plug);
		}
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x00054108 File Offset: 0x00052508
	private IEnumerator WobblingCoroutine()
	{
		if (this.wobbling)
		{
			yield break;
		}
		this.wobbling = true;
		float timer = 0f;
		int sign = 1;
		int wobbles = 0;
		while (this.wobbling)
		{
			while ((float)wobbles < this.wobblesBetweenRest)
			{
				timer = Mathf.MoveTowards(timer, this.wobblePeriod, Time.deltaTime);
				foreach (PuzzleBigPowerSupply_Plug puzzleBigPowerSupply_Plug in this.wobbleListeners)
				{
					puzzleBigPowerSupply_Plug.transform.localPosition = puzzleBigPowerSupply_Plug.savedLocalPosition + Vector2.right * (float)sign * this.wobbleAmplitude;
				}
				if (timer == this.wobblePeriod)
				{
					timer = 0f;
					sign *= -1;
					wobbles++;
				}
				yield return null;
			}
			wobbles = 0;
			foreach (PuzzleBigPowerSupply_Plug puzzleBigPowerSupply_Plug2 in this.wobbleListeners)
			{
				puzzleBigPowerSupply_Plug2.transform.localPosition = puzzleBigPowerSupply_Plug2.savedLocalPosition;
			}
			yield return new WaitForSeconds(this.wobbleRestTime);
		}
		yield break;
	}

	// Token: 0x04001602 RID: 5634
	[Header("Wobble stuff")]
	public float wobbleAmplitude;

	// Token: 0x04001603 RID: 5635
	public float wobblePeriod;

	// Token: 0x04001604 RID: 5636
	public float wobblesBetweenRest;

	// Token: 0x04001605 RID: 5637
	public float wobbleRestTime;

	// Token: 0x04001606 RID: 5638
	public float wobbleMousePosition;

	// Token: 0x04001607 RID: 5639
	public float wobbleEffectDistance = 3f;

	// Token: 0x04001608 RID: 5640
	private bool wobbling;

	// Token: 0x04001609 RID: 5641
	private List<PuzzleBigPowerSupply_Plug> wobbleListeners = new List<PuzzleBigPowerSupply_Plug>();

	// Token: 0x0400160A RID: 5642
	private SnapPoint secondSnapPoint;

	// Token: 0x0400160B RID: 5643
	private SnapPoint thirdSnapPoint;
}
