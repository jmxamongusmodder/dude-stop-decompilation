using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B1 RID: 945
public class JigsawPiece_Interchangeable : JigsawPiece
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600178A RID: 6026 RVA: 0x0004F017 File Offset: 0x0004D417
	public bool switched
	{
		get
		{
			return this.neighbourSet == JigsawPiece_Interchangeable.NeighbourSet.Alternative;
		}
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x0004F024 File Offset: 0x0004D424
	public override void Awake()
	{
		base.Awake();
		string[] array = this.alternativeIndex.Split(new char[]
		{
			'-'
		});
		this.alternativeIndeces = new Vector2((float)int.Parse(array[0]), (float)int.Parse(array[1]));
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x0004F06B File Offset: 0x0004D46B
	public int GetUsedSet()
	{
		return (int)this.neighbourSet;
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x0004F073 File Offset: 0x0004D473
	public void SetUsedSet(int set)
	{
		this.neighbourSet = (JigsawPiece_Interchangeable.NeighbourSet)set;
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x0004F07C File Offset: 0x0004D47C
	public override void Reset()
	{
		this.neighbourSet = JigsawPiece_Interchangeable.NeighbourSet.Unknown;
		base.Reset();
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x0004F08C File Offset: 0x0004D48C
	protected override void CollectNeighbours()
	{
		if ((this.neighbourSet & JigsawPiece_Interchangeable.NeighbourSet.Base) == JigsawPiece_Interchangeable.NeighbourSet.Base)
		{
			base.CollectNeighbours();
		}
		if ((this.neighbourSet & JigsawPiece_Interchangeable.NeighbourSet.Alternative) == JigsawPiece_Interchangeable.NeighbourSet.Alternative)
		{
			foreach (JigsawPiece jigsawPiece in this.GetComponentsInPuzzleStats(false))
			{
				if ((jigsawPiece.index.x == this.alternativeIndeces.x && Mathf.Abs(jigsawPiece.index.y - this.alternativeIndeces.y) == 1f) || (jigsawPiece.index.y == this.alternativeIndeces.y && Mathf.Abs(jigsawPiece.index.x - this.alternativeIndeces.x) == 1f))
				{
					Vector2 vector = new Vector2(jigsawPiece.index.y - this.alternativeIndeces.y, this.alternativeIndeces.x - jigsawPiece.index.x);
					this.alternativeNeighbours.Add(new KeyValuePair<Transform, Vector2>(jigsawPiece.transform, vector));
					jigsawPiece.AddNeighbour(base.transform, vector * -1f);
				}
			}
		}
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x0004F1DC File Offset: 0x0004D5DC
	protected override List<KeyValuePair<Transform, Vector2>> GetNeighbours()
	{
		List<KeyValuePair<Transform, Vector2>> list = new List<KeyValuePair<Transform, Vector2>>();
		if ((this.neighbourSet & JigsawPiece_Interchangeable.NeighbourSet.Base) == JigsawPiece_Interchangeable.NeighbourSet.Base)
		{
			foreach (KeyValuePair<Transform, Vector2> item in base.GetNeighbours())
			{
				list.Add(item);
			}
		}
		if ((this.neighbourSet & JigsawPiece_Interchangeable.NeighbourSet.Alternative) == JigsawPiece_Interchangeable.NeighbourSet.Alternative)
		{
			foreach (KeyValuePair<Transform, Vector2> keyValuePair in this.alternativeNeighbours)
			{
				list.Add(new KeyValuePair<Transform, Vector2>(keyValuePair.Key, keyValuePair.Value));
			}
		}
		return list;
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x0004F2BC File Offset: 0x0004D6BC
	private void SetNeighbourSet(Transform neighbour, Vector2 signs)
	{
		Debug.Log(this.neighbourSet);
		if (base.GetNeighbours().Exists((KeyValuePair<Transform, Vector2> x) => x.Key == neighbour && x.Value == signs))
		{
			this.neighbourSet = JigsawPiece_Interchangeable.NeighbourSet.Base;
		}
		else
		{
			this.neighbourSet = JigsawPiece_Interchangeable.NeighbourSet.Alternative;
		}
		Debug.Log(this.neighbourSet);
		foreach (JigsawPiece_Interchangeable jigsawPiece_Interchangeable in this.GetComponentsInPuzzleStats(false))
		{
			jigsawPiece_Interchangeable.neighbourSet = this.neighbourSet;
			if (this.neighbourSet == JigsawPiece_Interchangeable.NeighbourSet.Base)
			{
				jigsawPiece_Interchangeable.InformNeighbours(jigsawPiece_Interchangeable.alternativeNeighbours);
			}
			else
			{
				jigsawPiece_Interchangeable.woundUpSound = true;
				jigsawPiece_Interchangeable.InformNeighbours(jigsawPiece_Interchangeable.neighbours);
			}
		}
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x0004F388 File Offset: 0x0004D788
	private void InformNeighbours(List<KeyValuePair<Transform, Vector2>> values)
	{
		foreach (KeyValuePair<Transform, Vector2> keyValuePair in values)
		{
			keyValuePair.Key.GetComponent<JigsawPiece>().RemoveNeighbour(base.transform, keyValuePair.Value * -1f);
		}
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x0004F400 File Offset: 0x0004D800
	protected override void RemoveNeighbour(Transform neighbour)
	{
		if (this.neighbourSet == JigsawPiece_Interchangeable.NeighbourSet.Base)
		{
			base.RemoveNeighbour(neighbour);
		}
		else
		{
			this.alternativeNeighbours.RemoveAll((KeyValuePair<Transform, Vector2> x) => x.Key == neighbour);
		}
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x0004F450 File Offset: 0x0004D850
	protected override Vector2 GetNeighbourPosition(Vector2 nPos)
	{
		nPos = base.GetNeighbourPosition(nPos);
		if (this.neighbourSet == JigsawPiece_Interchangeable.NeighbourSet.Unknown)
		{
			foreach (KeyValuePair<Transform, Vector2> keyValuePair in this.GetNeighbours())
			{
				if (Vector2.Distance(keyValuePair.Key.position, nPos) < 0.2f)
				{
					Vector2 signs = (nPos - base.transform.position) / 1.666f;
					signs.x = (float)Mathf.RoundToInt(signs.x);
					signs.y = (float)Mathf.RoundToInt(signs.y);
					this.SetNeighbourSet(keyValuePair.Key, signs);
					break;
				}
			}
		}
		return nPos;
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x0004F538 File Offset: 0x0004D938
	protected override void GotMatched(Transform neighbour, Vector2 signs)
	{
		if (this.neighbourSet == JigsawPiece_Interchangeable.NeighbourSet.Unknown)
		{
			this.SetNeighbourSet(neighbour, signs);
		}
		if (this.woundUpSound)
		{
			this.woundUpSound = false;
			this.GetPuzzleStats().GetComponent<AudioVoice_JigSawPuzzle>().placeWrongPiece();
		}
	}

	// Token: 0x06001796 RID: 6038 RVA: 0x0004F570 File Offset: 0x0004D970
	protected override void MatchedSomething(Transform neighbour, Vector2 signs)
	{
		if (this.neighbourSet == JigsawPiece_Interchangeable.NeighbourSet.Unknown)
		{
			this.SetNeighbourSet(neighbour, signs);
		}
		if (this.woundUpSound)
		{
			this.woundUpSound = false;
			this.GetPuzzleStats().GetComponent<AudioVoice_JigSawPuzzle>().placeWrongPiece();
		}
	}

	// Token: 0x0400155B RID: 5467
	public string alternativeIndex;

	// Token: 0x0400155C RID: 5468
	private List<KeyValuePair<Transform, Vector2>> alternativeNeighbours = new List<KeyValuePair<Transform, Vector2>>();

	// Token: 0x0400155D RID: 5469
	private Vector2 alternativeIndeces;

	// Token: 0x0400155E RID: 5470
	private bool woundUpSound;

	// Token: 0x0400155F RID: 5471
	private JigsawPiece_Interchangeable.NeighbourSet neighbourSet = JigsawPiece_Interchangeable.NeighbourSet.Unknown;

	// Token: 0x020003B2 RID: 946
	private enum NeighbourSet
	{
		// Token: 0x04001561 RID: 5473
		Unknown = 3,
		// Token: 0x04001562 RID: 5474
		Base = 2,
		// Token: 0x04001563 RID: 5475
		Alternative = 1
	}
}
