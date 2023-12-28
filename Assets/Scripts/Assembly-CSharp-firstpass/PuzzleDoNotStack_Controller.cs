using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020003FC RID: 1020
public class PuzzleDoNotStack_Controller : MonoBehaviour
{
	// Token: 0x17000056 RID: 86
	// (set) Token: 0x060019E0 RID: 6624 RVA: 0x00062B10 File Offset: 0x00060F10
	private bool doNotEndRapidFire
	{
		set
		{
			if (this._doNotEndRapidFire == value)
			{
				return;
			}
			if (value)
			{
				this.GetPuzzleStats().doNotEndRapidFire = value;
			}
			if (value && this.waitingCoroutine != null)
			{
				base.StopCoroutine(this.waitingCoroutine);
				this.waitingCoroutine = null;
			}
			else if (!value && this.waitingCoroutine == null)
			{
				this.waitingCoroutine = base.StartCoroutine(this.WaitBeforeEndCoroutine());
			}
			this._doNotEndRapidFire = value;
		}
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x00062B90 File Offset: 0x00060F90
	public void TimeHasEnded()
	{
		foreach (PuzzleDoNotStack_Box puzzleDoNotStack_Box in this.boxes)
		{
			puzzleDoNotStack_Box.OnMouseUp();
			puzzleDoNotStack_Box.dragEnabled = false;
		}
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x00062BF4 File Offset: 0x00060FF4
	private void Awake()
	{
		this.InstantiateBoxes();
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x00062BFC File Offset: 0x00060FFC
	private void Update()
	{
		this.doNotEndRapidFire = ((from x in this.boxes
		where x.IsDragged() || x.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > this.magnitudeThreshold
		select x).Count<PuzzleDoNotStack_Box>() > 0);
		this.GetPuzzleStats().goBadAfterTime = this.ShouldGoBad();
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x00062C34 File Offset: 0x00061034
	private void InstantiateBoxes()
	{
		List<Transform> list = new List<Transform>();
		foreach (MultiplePuzzleSolutionChooser multiplePuzzleSolutionChooser in this.GetComponentsInPuzzleStats(true))
		{
			list.Clear();
			IEnumerator enumerator = multiplePuzzleSolutionChooser.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.tag == "Player")
					{
						list.Add(transform);
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
			foreach (Transform transform2 in list)
			{
				Transform transform3 = UnityEngine.Object.Instantiate<Transform>(this.boxPrefab, transform2.position, transform2.rotation, multiplePuzzleSolutionChooser.transform);
				transform3.gameObject.SetActive(true);
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x00062D60 File Offset: 0x00061160
	private bool ShouldGoBad()
	{
		bool result = false;
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		foreach (PuzzleDoNotStack_Box puzzleDoNotStack_Box in this.boxes)
		{
			Bounds bounds = puzzleDoNotStack_Box.GetComponent<PolygonCollider2D>().bounds;
			Vector2 origin = bounds.center + new Vector2(-bounds.extents.x + this.xShift, bounds.extents.y + 0.1f);
			Vector2 origin2 = bounds.center + new Vector2(bounds.extents.x - this.xShift, bounds.extents.y + 0.1f);
			RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, this.raycastLength, layerMask);
			RaycastHit2D hit2 = Physics2D.Raycast(origin2, Vector2.up, this.raycastLength, layerMask);
			if ((hit && hit.transform != null) || (hit2 && hit2.transform != null))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x00062EDC File Offset: 0x000612DC
	private IEnumerator WaitBeforeEndCoroutine()
	{
		yield return new WaitForSeconds(this.waitTime);
		this.GetPuzzleStats().doNotEndRapidFire = this._doNotEndRapidFire;
		yield break;
	}

	// Token: 0x040017EB RID: 6123
	public Transform boxPrefab;

	// Token: 0x040017EC RID: 6124
	public List<PuzzleDoNotStack_Box> boxes;

	// Token: 0x040017ED RID: 6125
	public float magnitudeThreshold = 1f;

	// Token: 0x040017EE RID: 6126
	public float waitTime = 0.1f;

	// Token: 0x040017EF RID: 6127
	public float xShift = 0.1f;

	// Token: 0x040017F0 RID: 6128
	public float raycastLength = 1f;

	// Token: 0x040017F1 RID: 6129
	private Coroutine waitingCoroutine;

	// Token: 0x040017F2 RID: 6130
	private bool _doNotEndRapidFire;
}
