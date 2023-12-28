using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200035E RID: 862
public class CupLego_Global : MonoBehaviour
{
	// Token: 0x06001512 RID: 5394 RVA: 0x0003C4CD File Offset: 0x0003A8CD
	private void Awake()
	{
		this.PositionLegos(Global.self.legoCupPieces);
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06001513 RID: 5395 RVA: 0x0003C4DF File Offset: 0x0003A8DF
	public static int totalLegoPieces
	{
		get
		{
			return Global.self.totalLegoPieces;
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06001514 RID: 5396 RVA: 0x0003C4EB File Offset: 0x0003A8EB
	// (set) Token: 0x06001515 RID: 5397 RVA: 0x0003C4FC File Offset: 0x0003A8FC
	public static List<LegoCupPiece> legoData
	{
		get
		{
			return CupLego_Global.GetActualSet(Global.self.legoCupPieces);
		}
		set
		{
			Global.self.legoCupPieces = value;
		}
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x0003C50C File Offset: 0x0003A90C
	private void PositionLegos(List<LegoCupPiece> pieces)
	{
		if (pieces == null || pieces.Count == 0)
		{
			return;
		}
		List<Transform> list = new List<Transform>();
		foreach (LegoCupPiece legoCupPiece in from x in pieces
		where x.snapped
		select x)
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
			Transform transform = UnityEngine.Object.Instantiate<Transform>(original);
			transform.SetParent(base.transform);
			transform.localScale = Vector3.one;
			transform.localPosition = legoCupPiece.position;
			transform.name = legoCupPiece.index.ToString();
			transform.gameObject.SetActive(true);
			Color color = this.GetColor(pieces, legoCupPiece);
			foreach (SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer.material.SetColor("_SpriteColor", color);
				if (this.parentAward != null)
				{
					list.Add(spriteRenderer.transform);
				}
			}
		}
		if (this.parentAward != null)
		{
			this.parentAward.mouseOverList = this.parentAward.mouseOverList.Concat(list).ToArray<Transform>();
		}
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x0003C6D4 File Offset: 0x0003AAD4
	private Color GetColor(List<LegoCupPiece> pieces, LegoCupPiece piece)
	{
		int num = (from x in pieces
		where x.points == piece.points
		orderby x.index
		select x).ToList<LegoCupPiece>().FindIndex((LegoCupPiece x) => x.index == piece.index);
		return this.colors[num % this.colors.Length];
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x0003C753 File Offset: 0x0003AB53
	public static int UnusedPieces()
	{
		return (from x in CupLego_Global.legoData
		where !x.snapped
		select x).Count<LegoCupPiece>();
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x0003C784 File Offset: 0x0003AB84
	public static List<LegoCupPiece> GetActualSet(List<LegoCupPiece> set)
	{
		if (set != null && set.Count == CupLego_Global.totalLegoPieces)
		{
			return set;
		}
		if (set == null || set.Count == 0)
		{
			set = CupLego_Global.GenerateInitialSet();
		}
		int i;
		for (i = 0; i < CupLego_Global.totalLegoPieces; i++)
		{
			if ((from x in set
			where x.index == i
			select x).Count<LegoCupPiece>() <= 0)
			{
				set.Add(new LegoCupPiece
				{
					index = i
				});
			}
		}
		CupLego_Global.legoData = set;
		return set;
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x0003C834 File Offset: 0x0003AC34
	public static List<LegoCupPiece> GenerateInitialSet()
	{
		return new List<LegoCupPiece>
		{
			new LegoCupPiece
			{
				index = 0,
				x = 0.66501f,
				y = 1.876976f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 1,
				x = -0.01001009f,
				y = 1.876976f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 2,
				x = -0.6649821f,
				y = 2.120818f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 4,
				x = 0f,
				y = 0.9016097f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 9,
				x = 0f,
				y = 0.657768f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 10,
				x = -0.6649801f,
				y = 1.633135f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 12,
				x = 0f,
				y = 1.145451f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 14,
				x = 0.3224694f,
				y = 2.120818f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 15,
				x = 2.500415E-05f,
				y = 1.389293f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 16,
				x = 0f,
				y = 0.4139264f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 17,
				x = -0.6650101f,
				y = 1.876976f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 18,
				x = 0.33253f,
				y = 1.633135f,
				snapped = true
			},
			new LegoCupPiece
			{
				index = 19,
				x = 2.500415E-05f,
				y = 0.1700845f,
				snapped = true
			}
		};
	}

	// Token: 0x040012A8 RID: 4776
	public const int MAX_PIECES_IN_CUP = 35;

	// Token: 0x040012A9 RID: 4777
	public Color[] colors;

	// Token: 0x040012AA RID: 4778
	public Transform twoPiece;

	// Token: 0x040012AB RID: 4779
	public Transform threePiece;

	// Token: 0x040012AC RID: 4780
	public Transform fourPiece;

	// Token: 0x040012AD RID: 4781
	[Space(20f)]
	public Award parentAward;
}
