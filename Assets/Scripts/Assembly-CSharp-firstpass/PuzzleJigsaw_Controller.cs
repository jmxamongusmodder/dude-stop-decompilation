using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class PuzzleJigsaw_Controller : MonoBehaviour, TransitionProcessor
{
	// Token: 0x06001AF2 RID: 6898 RVA: 0x0006C77D File Offset: 0x0006AB7D
	public void Awake()
	{
		this.SetShaderParameters();
		this.LoadFromGlobal();
		this.Shuffle(true);
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x0006C794 File Offset: 0x0006AB94
	private void SetShaderParameters()
	{
		Vector2 vector = Camera.main.ViewportToWorldPoint(Vector2.zero);
		base.transform.position = new Vector2(vector.x, base.transform.position.y);
		foreach (SpriteRenderer spriteRenderer in this.GetComponentsInPuzzleStats(false))
		{
			if (!(spriteRenderer.material.shader.name != "Custom/Line"))
			{
				spriteRenderer.material.SetFloat("_Angle", -1.5707964f);
				spriteRenderer.material.SetFloat("_Left", 0f);
				spriteRenderer.material.SetFloat("_Top", 0f);
				spriteRenderer.material.SetFloat("_Distance", 0f);
			}
		}
	}

	// Token: 0x06001AF4 RID: 6900 RVA: 0x0006C888 File Offset: 0x0006AC88
	private void LoadFromGlobal()
	{
		if (Global.self.DEBUG && Input.GetKey(KeyCode.LeftControl))
		{
			Global.self.unlockedJigsawPieces = 20;
		}
		for (int i = Global.self.unlockedJigsawPieces; i < this.pieces.Count; i++)
		{
			this.pieces[i].gameObject.SetActive(false);
		}
		this.groups.Clear();
		using (List<SerializableJigsawPiece>.Enumerator enumerator = Global.self.jigsawPuzzlePieces.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SerializableJigsawPiece pieceData = enumerator.Current;
				JigsawPiece jigsawPiece = this.pieces.First((JigsawPiece x) => x.name == pieceData.name);
				jigsawPiece.GetComponent<SpriteRenderer>().sortingOrder = pieceData.order;
				jigsawPiece.transform.localPosition = new Vector3(pieceData.x, pieceData.y, pieceData.z);
				if (pieceData.interchangeable)
				{
					jigsawPiece.transform.GetComponent<JigsawPiece_Interchangeable>().SetUsedSet(pieceData.interchangeableSetStatus);
				}
				if (pieceData.group != 0)
				{
					Transform transform = this.GetPuzzleStats().transform.Find("Group " + pieceData.group);
					if (transform == null)
					{
						transform = new GameObject().transform;
						transform.SetParent(this.GetPuzzleStats().transform);
						transform.name = "Group " + pieceData.group;
						transform.tag = "Finish";
						this.groups.Add(transform);
					}
					jigsawPiece.transform.SetParent(transform);
				}
				jigsawPiece.finished = pieceData.finished;
			}
		}
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x0006CAB4 File Offset: 0x0006AEB4
	public void Reset()
	{
		Global.self.jigsawPuzzlePieces.Clear();
		Global.self.Save();
		foreach (JigsawPiece jigsawPiece in this.GetComponentsInPuzzleStats(false))
		{
			jigsawPiece.Reset();
		}
		IEnumerator enumerator = this.GetPuzzleStats().transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.tag == "Finish")
				{
					UnityEngine.Object.Destroy(transform.gameObject);
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
		Audio.self.playOneShot("7d5d5f89-93d1-4dbc-bad1-16a0ab44e269", 1f);
		this.groups.Clear();
		this.Shuffle(false);
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x0006CBA4 File Offset: 0x0006AFA4
	public void SaveJigsaw(bool finished = false)
	{
		Global.self.jigsawPuzzlePieces.Clear();
		int num = 1;
		using (List<JigsawPiece>.Enumerator enumerator = this.pieces.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JigsawPiece piece = enumerator.Current;
				if (piece.gameObject.activeSelf)
				{
					if ((from x in Global.self.jigsawPuzzlePieces
					where x.name == piece.name
					select x).Count<SerializableJigsawPiece>() <= 0)
					{
						if (piece.transform.parent.GetComponent<PuzzleStats>() == null)
						{
							foreach (JigsawPiece jigsawPiece in piece.transform.parent.GetComponentsInChildren<JigsawPiece>())
							{
								if (finished)
								{
									jigsawPiece.finished = true;
								}
								Global.self.jigsawPuzzlePieces.Add(new SerializableJigsawPiece(jigsawPiece, num));
							}
							num++;
						}
						else
						{
							if (finished)
							{
								piece.finished = true;
							}
							Global.self.jigsawPuzzlePieces.Add(new SerializableJigsawPiece(piece, 0));
						}
					}
				}
			}
		}
		Global.self.Save();
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x0006CD20 File Offset: 0x0006B120
	private void Shuffle(bool instant = true)
	{
		Vector2 vector = Camera.main.ViewportToWorldPoint(Vector2.one);
		Vector3 min = new Vector3(-vector.x + this.screenOffset, -vector.y + this.bottomOffset, -5f);
		Vector3 max = new Vector3(vector.x - this.screenOffset, vector.y - this.screenOffset, 5f);
		Bounds bounds = default(Bounds);
		bounds.SetMinMax(min, max);
		this.index = 0;
		int num = 0;
		int total = this.GetComponentsInPuzzleStats(false).Count<JigSaw_piece>();
		using (IEnumerator<JigsawPiece> enumerator = (from x in this.pieces
		orderby x.transform.position.sqrMagnitude descending
		select x).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JigsawPiece piece = enumerator.Current;
				if (piece.gameObject.activeSelf)
				{
					if ((from x in Global.self.jigsawPuzzlePieces
					where x.name == piece.name
					select x).Count<SerializableJigsawPiece>() <= 0)
					{
						float num2 = Extensions.Random(this.randomOffset);
						Vector2 v;
						if (UnityEngine.Random.value > 0.5f)
						{
							float num3 = (UnityEngine.Random.value <= 0.5f) ? bounds.max.y : bounds.min.y;
							v = new Vector2(UnityEngine.Random.Range(bounds.min.x, bounds.max.x), num3 + num2);
						}
						else
						{
							float num4 = (UnityEngine.Random.value <= 0.5f) ? bounds.max.x : bounds.min.x;
							v = new Vector2(num4 + num2, UnityEngine.Random.Range(bounds.min.y, bounds.max.y));
						}
						if (instant)
						{
							piece.transform.position = v;
							base.StartCoroutine(this.NewPieceAppearingCoroutine(piece, (float)num++ * this.waitBetweenUnlocks));
						}
						else
						{
							base.StartCoroutine(this.PieceShufflingCoroutine(piece.transform, v, num++, total, this.waitBetweenPieces));
						}
					}
				}
			}
		}
		foreach (JigsawPiece jigsawPiece in this.pieces)
		{
			if (jigsawPiece.gameObject.activeSelf)
			{
				if (!(jigsawPiece.transform.parent.tag == "Finish"))
				{
					if (instant)
					{
						jigsawPiece.transform.localPosition += bounds.ClosestPoint(jigsawPiece.transform.localPosition) - jigsawPiece.transform.localPosition;
					}
				}
			}
		}
		foreach (Transform transform in this.groups)
		{
			bool flag = false;
			IEnumerator enumerator4 = transform.GetEnumerator();
			try
			{
				while (enumerator4.MoveNext())
				{
					object obj = enumerator4.Current;
					Transform transform2 = (Transform)obj;
					flag |= bounds.Contains(transform2.localPosition);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator4 as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			if (!flag)
			{
				float num5 = 999f;
				Vector2 shift = Vector2.zero;
				IEnumerator enumerator5 = transform.GetEnumerator();
				try
				{
					while (enumerator5.MoveNext())
					{
						object obj2 = enumerator5.Current;
						Transform transform3 = (Transform)obj2;
						Vector2 a = bounds.ClosestPoint(transform3.localPosition);
						if (a.sqrMagnitude < num5)
						{
							num5 = a.sqrMagnitude;
							shift = a - transform3.localPosition;
						}
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator5 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
				transform.GetChild(0).GetComponent<JigsawPiece>().Shift(shift);
			}
		}
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x0006D24C File Offset: 0x0006B64C
	[ContextMenu("Randomize pieces")]
	public void RandomizePieces()
	{
		this.pieces.Clear();
		this.pieces.AddRange(this.GetComponentsInPuzzleStats(false));
		this.pieces.Shuffle<JigsawPiece>();
		foreach (JigsawPiece_Interchangeable item in this.GetComponentsInPuzzleStats(false))
		{
			this.pieces.Remove(item);
			this.pieces.Add(item);
		}
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x0006D2BC File Offset: 0x0006B6BC
	private IEnumerator PieceShufflingCoroutine(Transform piece, Vector3 finalPos, int current, int total, float waitTime)
	{
		this.index++;
		yield return new WaitForSeconds((float)current * waitTime);
		float timer = 0f;
		Vector3 start = piece.position;
		Vector3 center = Vector3.zero + UnityEngine.Random.insideUnitCircle;
		center.z = start.z;
		while (timer != this.shuffleMove.GetAnimationLength())
		{
			timer = Mathf.MoveTowards(timer, this.shuffleMove.GetAnimationLength(), Time.deltaTime);
			piece.position = Vector3.Lerp(start, center, this.shuffleMove.Evaluate(timer));
			yield return null;
		}
		yield return new WaitForSeconds((float)(total - current) * waitTime + this.waitBeforeExplosion);
		timer = 0f;
		start = piece.position;
		finalPos.z = start.z;
		while (timer != this.shuffleMove.GetAnimationLength())
		{
			timer = Mathf.MoveTowards(timer, this.shuffleMove.GetAnimationLength(), Time.deltaTime * 2f);
			piece.position = Vector3.Lerp(start, finalPos, this.shuffleMove.Evaluate(timer));
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x0006D2FC File Offset: 0x0006B6FC
	private IEnumerator NewPieceAppearingCoroutine(JigsawPiece piece, float wait)
	{
		yield return null;
		piece.MoveToTop();
		piece.GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(Global.self.transitionTimeSide);
		yield return new WaitForSeconds(wait);
		Audio.self.playOneShot("ed83dd05-9f93-4414-bc70-67850798eb19", 1f);
		float timer = 0f;
		while (timer != this.unlockAnimation.GetAnimationLength())
		{
			timer = Mathf.MoveTowards(timer, this.unlockAnimation.GetAnimationLength(), Time.deltaTime);
			piece.transform.localScale = Vector2.one * this.unlockAnimation.Evaluate(timer);
			yield return null;
		}
		piece.GetComponent<Collider2D>().enabled = true;
		piece.transform.localScale = Vector2.one;
		yield break;
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x0006D328 File Offset: 0x0006B728
	public void TransitionUpdate()
	{
		float x = Camera.main.WorldToViewportPoint(base.transform.position).x;
		foreach (SpriteRenderer spriteRenderer in this.GetComponentsInPuzzleStats(false))
		{
			if (!(spriteRenderer.material.shader.name != "Custom/Line"))
			{
				spriteRenderer.material.SetFloat("_Left", x);
			}
		}
	}

	// Token: 0x0400192B RID: 6443
	[Header("Shuffling")]
	public float waitBetweenPieces;

	// Token: 0x0400192C RID: 6444
	public AnimationCurve shuffleMove;

	// Token: 0x0400192D RID: 6445
	public float screenOffset;

	// Token: 0x0400192E RID: 6446
	public float bottomOffset;

	// Token: 0x0400192F RID: 6447
	public Vector2 randomOffset;

	// Token: 0x04001930 RID: 6448
	public float waitBeforeExplosion = 0.5f;

	// Token: 0x04001931 RID: 6449
	private List<Transform> groups = new List<Transform>();

	// Token: 0x04001932 RID: 6450
	[Header("Piece unlock order")]
	public List<JigsawPiece> pieces = new List<JigsawPiece>();

	// Token: 0x04001933 RID: 6451
	public AnimationCurve unlockAnimation;

	// Token: 0x04001934 RID: 6452
	public float waitBetweenUnlocks = 0.05f;

	// Token: 0x04001935 RID: 6453
	private const int UNKNOWN_SET = 3;

	// Token: 0x04001936 RID: 6454
	private const int BASE_SET = 2;

	// Token: 0x04001937 RID: 6455
	private const int ALTERNATIVE_SET = 1;

	// Token: 0x04001938 RID: 6456
	private int index;
}
