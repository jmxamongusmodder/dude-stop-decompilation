using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200035D RID: 861
public class CupLego_Controller : MonoBehaviour
{
	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06001501 RID: 5377 RVA: 0x0003BBED File Offset: 0x00039FED
	// (set) Token: 0x06001502 RID: 5378 RVA: 0x0003BBF4 File Offset: 0x00039FF4
	private List<LegoCupPiece> legoData
	{
		get
		{
			return CupLego_Global.legoData;
		}
		set
		{
			CupLego_Global.legoData = value;
		}
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x0003BBFC File Offset: 0x00039FFC
	private void Awake()
	{
		this.PositionLegos(this.legoData);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x0003BC0C File Offset: 0x0003A00C
	private void DebugPositions()
	{
		List<LegoCupPiece> source = this.SaveData();
		foreach (LegoCupPiece legoCupPiece in from x in source
		where x.snapped
		select x)
		{
			Debug.Log(string.Format("{0} -- {1}, {2}", legoCupPiece.index, legoCupPiece.x, legoCupPiece.y));
		}
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x0003BCB4 File Offset: 0x0003A0B4
	private List<LegoCupPiece> SaveData()
	{
		List<LegoCupPiece> list = new List<LegoCupPiece>();
		foreach (LegoPiece legoPiece in this.GetComponentsInPuzzleStats(false))
		{
			if (!(legoPiece.transform == this.legoBase))
			{
				LegoCupPiece legoCupPiece = new LegoCupPiece
				{
					index = int.Parse(legoPiece.name)
				};
				legoCupPiece.snapped = legoPiece.lockX;
				if (legoCupPiece.snapped)
				{
					legoCupPiece.position = legoPiece.transform.position - this.legoBase.position;
				}
				list.Add(legoCupPiece);
			}
		}
		return list;
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x0003BD68 File Offset: 0x0003A168
	public void Reset()
	{
		foreach (LegoPiece legoPiece in from x in this.GetComponentsInPuzzleStats(false)
		where x.lockX && x.transform != this.legoBase
		select x)
		{
			legoPiece.lockX = false;
			legoPiece.GetComponent<CupLego_Piece>().ReturnTo(this.GetRandomPoint());
			legoPiece.GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x0003BDF0 File Offset: 0x0003A1F0
	public bool AcquireCup()
	{
		List<LegoPiece> list = (from x in this.GetComponentsInPuzzleStats(false)
		where x.transform != this.legoBase && x.lockX
		select x).ToList<LegoPiece>();
		if (list.Count == 0)
		{
			return false;
		}
		this.legoData = this.SaveData();
		list.ForEach(delegate(LegoPiece x)
		{
			x.transform.SetParent(this.cup);
		});
		Rect rect = new Rect(base.transform.position, Vector2.zero);
		Bounds bounds = default(Bounds);
		foreach (LegoPiece legoPiece in this.cup.GetComponentsInChildren<LegoPiece>())
		{
			bounds = legoPiece.GetComponent<Collider2D>().bounds;
			if (rect.size == Vector2.zero)
			{
				rect = new Rect(new Vector2(bounds.min.x, bounds.max.y), bounds.size);
			}
			else
			{
				rect.xMax = Mathf.Max(rect.xMax, bounds.max.x);
				rect.xMin = Mathf.Min(rect.xMin, bounds.min.x);
				rect.yMax = Mathf.Max(rect.yMax, bounds.max.y);
				rect.yMin = Mathf.Min(rect.yMin, bounds.min.y);
			}
		}
		Vector3 b = new Vector3(rect.center.x, rect.size.y / 2f);
		foreach (LegoPiece legoPiece2 in this.cup.GetComponentsInChildren<LegoPiece>())
		{
			legoPiece2.transform.position -= b;
		}
		this.cup.position += b;
		Global.CupAcquired(this.cup);
		Global.self.Save();
		return true;
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x0003C024 File Offset: 0x0003A424
	public Vector2 GetRandomPoint()
	{
		int index = UnityEngine.Random.Range(0, this.spawn.childCount);
		Bounds bounds = this.spawn.GetChild(index).GetComponent<BoxCollider2D>().bounds;
		return new Vector2(UnityEngine.Random.Range(bounds.min.x, bounds.max.x), UnityEngine.Random.Range(bounds.min.y, bounds.max.y));
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x0003C0A8 File Offset: 0x0003A4A8
	public Vector2 GetRandomPoint(Vector2 position)
	{
		Bounds bounds = (from x in this.spawn.GetComponentsInChildren<BoxCollider2D>()
		orderby Vector2.Distance(x.bounds.center, position)
		select x).First<BoxCollider2D>().bounds;
		return new Vector2(UnityEngine.Random.Range(bounds.min.x, bounds.max.x), UnityEngine.Random.Range(bounds.min.y, bounds.max.y));
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x0003C138 File Offset: 0x0003A538
	private void PositionLegos(List<LegoCupPiece> pieces)
	{
		IEnumerator enumerator = this.legoContainer.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				UnityEngine.Object.Destroy(transform.gameObject);
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
		foreach (LegoCupPiece legoCupPiece in pieces)
		{
			Transform original;
			switch (legoCupPiece.points)
			{
			default:
				original = this.twoPiece;
				break;
			case 3:
				original = this.threePiece;
				break;
			case 4:
				original = this.fourPiece;
				break;
			}
			Transform transform2 = UnityEngine.Object.Instantiate<Transform>(original);
			transform2.SetParent(this.legoContainer);
			transform2.name = legoCupPiece.index.ToString();
			transform2.GetComponent<Rigidbody2D>().isKinematic = true;
			transform2.GetComponent<Rigidbody2D>().gravityScale = 0f;
			Color color = this.GetColor(pieces, legoCupPiece);
			foreach (SpriteRenderer spriteRenderer in transform2.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer.color = color;
			}
			if (legoCupPiece.snapped)
			{
				transform2.GetComponent<LegoPiece>().recheckPoints = true;
				transform2.GetComponent<LegoPiece>().lockX = true;
				transform2.position = this.legoBase.position + legoCupPiece.position;
				transform2.GetComponent<PhysicsSound>().enable = false;
				transform2.GetComponent<PhysicsSound>().disableAutomaticEnable = true;
			}
			else
			{
				transform2.position = this.GetRandomPoint();
				transform2.rotation = Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
				transform2.gameObject.layer = LayerMask.NameToLayer("No touching");
				transform2.GetComponent<CupLego_Piece>().SetColliderStatusTo(false);
			}
		}
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x0003C37C File Offset: 0x0003A77C
	private Color GetColor(List<LegoCupPiece> pieces, LegoCupPiece piece)
	{
		int num = (from x in pieces
		where x.points == piece.points
		orderby x.index
		select x).ToList<LegoCupPiece>().FindIndex((LegoCupPiece x) => x.index == piece.index);
		return this.colors[num % this.colors.Length];
	}

	// Token: 0x0400129D RID: 4765
	public Color[] colors;

	// Token: 0x0400129E RID: 4766
	public Transform twoPiece;

	// Token: 0x0400129F RID: 4767
	public Transform threePiece;

	// Token: 0x040012A0 RID: 4768
	public Transform fourPiece;

	// Token: 0x040012A1 RID: 4769
	public Transform legoBase;

	// Token: 0x040012A2 RID: 4770
	public Transform legoContainer;

	// Token: 0x040012A3 RID: 4771
	public Transform cup;

	// Token: 0x040012A4 RID: 4772
	public Transform spawn;

	// Token: 0x040012A5 RID: 4773
	public float torque;
}
