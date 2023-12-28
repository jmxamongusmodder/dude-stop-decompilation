using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x02000445 RID: 1093
public class PuzzleShapeInBox_Lid : EnhancedDraggable
{
	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06001BEF RID: 7151 RVA: 0x000758E4 File Offset: 0x00073CE4
	public bool prismStupid
	{
		get
		{
			return base.transform.localPosition.x > this.prismFrom;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0007590C File Offset: 0x00073D0C
	public bool prismCanBeSnapped
	{
		get
		{
			return base.transform.localPosition.x < this.prismTo || base.transform.localPosition.x > this.prismFrom;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00075958 File Offset: 0x00073D58
	public bool cylinderStupid
	{
		get
		{
			return base.transform.localPosition.x > this.cylinderFrom;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00075980 File Offset: 0x00073D80
	public bool cylinderCanBeSnapped
	{
		get
		{
			return base.transform.localPosition.x < this.cylinderTo || base.transform.localPosition.x > this.cylinderFrom;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000759CC File Offset: 0x00073DCC
	private bool closed
	{
		get
		{
			return base.transform.localPosition.x <= Mathf.Epsilon;
		}
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x000759F8 File Offset: 0x00073DF8
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
		if (base.transform.localPosition.x == 0f)
		{
			this.CheckVictoryConditions();
		}
		else if (base.transform.localPosition.x < this.autoClosingDistance)
		{
			this.activeCoroutine = base.StartCoroutine(this.ClosingCoroutine());
		}
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x00075A7F File Offset: 0x00073E7F
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		if (this.dragged && this.activeCoroutine != null)
		{
			base.StopCoroutine(this.activeCoroutine);
			this.activeCoroutine = null;
		}
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x00075ABC File Offset: 0x00073EBC
	private void Start()
	{
		this.maxLayer = Mathf.Max(new int[]
		{
			this.cube.sortingOrder,
			this.prism.sortingOrder,
			this.cylinder.sortingOrder
		});
		this.bottomCover = (from x in base.transform.GetComponentsInChildren<Collider2D>()
		where !x.isTrigger
		select x).First<Collider2D>().transform;
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x00075B44 File Offset: 0x00073F44
	private void Update()
	{
		this.SortShapes();
		if (this.dragged)
		{
			float num = base.transform.position.x - this.limit.leftVal;
			float num2 = this.limit.rightVal - this.limit.leftVal;
			if (num / num2 > 0.15f)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_ShapesInBox>().onLidOpen();
			}
		}
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x00075BBB File Offset: 0x00073FBB
	public void TriangleInside()
	{
		this.dragEnabled = false;
		this.prismInside = true;
	}

	// Token: 0x06001BF9 RID: 7161 RVA: 0x00075BCB File Offset: 0x00073FCB
	public void CylinderInside()
	{
		this.dragEnabled = false;
		this.cylinderInside = true;
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x00075BDB File Offset: 0x00073FDB
	public void ShapeFinishedFalling()
	{
		this.dragEnabled = true;
		this.CheckVictoryConditions();
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x00075BEC File Offset: 0x00073FEC
	private void SortShapes()
	{
		int num = this.maxLayer;
		int num2 = this.maxLayer - 2;
		int num3 = this.maxLayer - 2;
		if (this.GetBounds(this.cube).min.y < this.GetBounds(this.cylinder).max.y)
		{
			num--;
			num2++;
		}
		if (this.GetBounds(this.cube).min.y < this.GetBounds(this.prism).max.y)
		{
			num--;
			num3++;
		}
		if (this.prism.transform.position.y < this.cylinder.transform.position.y)
		{
			num2++;
		}
		else
		{
			num3++;
		}
		this.prism.sortingOrder = num3;
		this.cylinder.sortingOrder = num2;
		this.cube.sortingOrder = num;
		this.ChangeSortingLayer(this.prism);
		this.ChangeSortingLayer(this.cylinder);
		this.ChangeSortingLayer(this.cube);
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x00075D30 File Offset: 0x00074130
	private void ChangeSortingLayer(SpriteRenderer obj)
	{
		if (obj.transform.position.y > this.bottomCover.position.y + this.bottomCoverOffset)
		{
			obj.sortingLayerName = "Front";
		}
		else
		{
			obj.sortingLayerName = "Default";
		}
	}

	// Token: 0x06001BFD RID: 7165 RVA: 0x00075D8A File Offset: 0x0007418A
	private Bounds GetBounds(SpriteRenderer rend)
	{
		return rend.transform.parent.GetComponent<BoxCollider2D>().bounds;
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x00075DA4 File Offset: 0x000741A4
	private void CheckVictoryConditions()
	{
		if (!this.closed || !base.enabled)
		{
			return;
		}
		int num = this.GetComponentsInPuzzleStats(false).Count<PuzzleShapeInBox_Object>();
		int num2 = this.GetComponentsInPuzzleStats(false).Count<PuzzleShapeInBox_SnappableObject>();
		if (num > 1 || num2 > 0)
		{
			return;
		}
		if (num == 0)
		{
			if (this.prismInside && this.cylinderInside)
			{
				Global.LevelFailed(0f, true);
			}
			else
			{
				Global.LevelCompleted(0f, true);
			}
		}
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x00075E28 File Offset: 0x00074228
	private IEnumerator ClosingCoroutine()
	{
		while (base.transform.localPosition.x != 0f)
		{
			base.transform.SetLocalX(Mathf.MoveTowards(base.transform.localPosition.x, 0f, this.autoClosingSpeed * Time.deltaTime));
			yield return null;
		}
		this.CheckVictoryConditions();
		yield break;
	}

	// Token: 0x04001A52 RID: 6738
	public SpriteRenderer cube;

	// Token: 0x04001A53 RID: 6739
	public SpriteRenderer prism;

	// Token: 0x04001A54 RID: 6740
	public SpriteRenderer cylinder;

	// Token: 0x04001A55 RID: 6741
	public float autoClosingDistance = 0.15f;

	// Token: 0x04001A56 RID: 6742
	public float autoClosingSpeed = 0.1f;

	// Token: 0x04001A57 RID: 6743
	public float bottomCoverOffset;

	// Token: 0x04001A58 RID: 6744
	private Transform bottomCover;

	// Token: 0x04001A59 RID: 6745
	[Header("Shape stuff")]
	public float prismTo;

	// Token: 0x04001A5A RID: 6746
	public float prismFrom;

	// Token: 0x04001A5B RID: 6747
	private bool prismInside;

	// Token: 0x04001A5C RID: 6748
	public float cylinderTo;

	// Token: 0x04001A5D RID: 6749
	public float cylinderFrom;

	// Token: 0x04001A5E RID: 6750
	private bool cylinderInside;

	// Token: 0x04001A5F RID: 6751
	private int maxLayer;

	// Token: 0x04001A60 RID: 6752
	private Coroutine activeCoroutine;
}
