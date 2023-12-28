using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020003B0 RID: 944
public class JigsawPiece : Draggable
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600176F RID: 5999 RVA: 0x0004DF28 File Offset: 0x0004C328
	// (set) Token: 0x06001770 RID: 6000 RVA: 0x0004DF30 File Offset: 0x0004C330
	public Vector2 index { get; private set; }

	// Token: 0x06001771 RID: 6001 RVA: 0x0004DF3C File Offset: 0x0004C33C
	public virtual void Awake()
	{
		string[] array = base.transform.name.Split(new char[]
		{
			'-'
		});
		this.index = new Vector2((float)int.Parse(array[0]), (float)int.Parse(array[1]));
		foreach (JigsawPiece jigsawPiece in this.GetComponentsInPuzzleStats(true))
		{
			this.totalPieces++;
			SpriteRenderer component = jigsawPiece.GetComponent<SpriteRenderer>();
			if (component.sortingOrder == 0)
			{
				component.sortingOrder = this.totalPieces;
				jigsawPiece.transform.position = new Vector3(jigsawPiece.transform.position.x, jigsawPiece.transform.position.y, -0.1f * (float)this.totalPieces);
			}
		}
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x0004E016 File Offset: 0x0004C416
	public void Start()
	{
		this.CollectNeighbours();
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x0004E01E File Offset: 0x0004C41E
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.CheckParent();
		this.MoveToTop();
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x0004E03E File Offset: 0x0004C43E
	public override void OnMouseUp()
	{
		if (!base.enabled || !this.dragged)
		{
			return;
		}
		base.OnMouseUp();
		this.CheckNeighbourSnap();
		this.CheckVictoryConditions();
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x0004E069 File Offset: 0x0004C469
	public virtual void Reset()
	{
		this.finished = false;
		this.CollectNeighbours();
		base.transform.SetParent(this.GetPuzzleStats().transform);
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x0004E090 File Offset: 0x0004C490
	public void Shift(Vector2 shift)
	{
		this.AnnounceMovement(base.transform.position, base.transform.position + shift);
		base.transform.position += shift;
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x0004E0E0 File Offset: 0x0004C4E0
	private void CheckParent()
	{
		if (base.transform.parent.GetComponent<PuzzleStats>() != null)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.position = Vector2.zero;
			gameObject.transform.SetParent(base.transform.parent);
			gameObject.tag = "Finish";
			base.transform.SetParent(gameObject.transform);
		}
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x0004E158 File Offset: 0x0004C558
	public void MoveToTop()
	{
		if (base.transform.parent.tag == "Finish")
		{
			foreach (SpriteRenderer spriteRenderer in base.transform.parent.GetComponentsInChildren<SpriteRenderer>())
			{
				if (!(spriteRenderer.transform == base.transform))
				{
					this.MoveToTop(spriteRenderer);
				}
			}
		}
		this.MoveToTop(base.GetComponent<SpriteRenderer>());
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x0004E1DC File Offset: 0x0004C5DC
	private void MoveToTop(SpriteRenderer rend)
	{
		int sortingOrder = rend.sortingOrder;
		foreach (SpriteRenderer spriteRenderer in this.GetComponentsInPuzzleStats(false))
		{
			if (spriteRenderer.sortingOrder >= sortingOrder && !(spriteRenderer == rend))
			{
				spriteRenderer.sortingOrder--;
				spriteRenderer.transform.position += 0.1f * Vector3.forward;
			}
		}
		rend.sortingOrder = this.totalPieces;
		rend.transform.position = new Vector3(rend.transform.position.x, rend.transform.position.y, -0.1f * (float)this.totalPieces);
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x0004E2B4 File Offset: 0x0004C6B4
	private void CheckNeighbourSnap()
	{
		Transform[] componentsInChildren = base.transform.parent.GetComponentsInChildren<Transform>();
		foreach (Transform transform in componentsInChildren)
		{
			if (!(transform.tag == "Finish"))
			{
				if (transform.GetComponent<JigsawPiece>().CheckNeighbours())
				{
					break;
				}
			}
		}
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x0004E31C File Offset: 0x0004C71C
	private void CheckVictoryConditions()
	{
		if (this.finished)
		{
			return;
		}
		if (base.transform.parent.childCount == this.totalPieces)
		{
			if ((from x in this.GetComponentsInPuzzleStats(false)
			where x.switched
			select x).Count<JigsawPiece_Interchangeable>() > 0)
			{
				base.StartCoroutine(this.EndingCoroutine(true));
			}
			else
			{
				base.StartCoroutine(this.EndingCoroutine(false));
			}
		}
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x0004E3A8 File Offset: 0x0004C7A8
	protected override void AnnounceMovement(Vector3 currPos, Vector3 nextPos)
	{
		Vector3 b = nextPos - currPos;
		IEnumerator enumerator = base.transform.parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (!(transform == base.transform))
				{
					transform.position += b;
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

	// Token: 0x0600177D RID: 6013 RVA: 0x0004E43C File Offset: 0x0004C83C
	protected virtual List<KeyValuePair<Transform, Vector2>> GetNeighbours()
	{
		return this.neighbours;
	}

	// Token: 0x0600177E RID: 6014 RVA: 0x0004E444 File Offset: 0x0004C844
	protected virtual void RemoveNeighbour(Transform neighbour)
	{
		this.neighbours.RemoveAll((KeyValuePair<Transform, Vector2> x) => x.Key == neighbour);
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x0004E478 File Offset: 0x0004C878
	public virtual void RemoveNeighbour(Transform neighbour, Vector2 values)
	{
		this.neighbours.RemoveAll((KeyValuePair<Transform, Vector2> x) => x.Key == neighbour && x.Value == values);
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x0004E4B1 File Offset: 0x0004C8B1
	public void AddNeighbour(Transform neighbour, Vector2 values)
	{
		this.neighbours.Add(new KeyValuePair<Transform, Vector2>(neighbour, values));
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x0004E4C8 File Offset: 0x0004C8C8
	protected virtual void CollectNeighbours()
	{
		foreach (JigsawPiece jigsawPiece in this.GetComponentsInPuzzleStats(false))
		{
			if ((jigsawPiece.index.x == this.index.x && Mathf.Abs(jigsawPiece.index.y - this.index.y) == 1f) || (jigsawPiece.index.y == this.index.y && Mathf.Abs(jigsawPiece.index.x - this.index.x) == 1f))
			{
				Vector2 values = new Vector2(jigsawPiece.index.y - this.index.y, this.index.x - jigsawPiece.index.x);
				this.AddNeighbour(jigsawPiece.transform, values);
			}
		}
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x0004E5EC File Offset: 0x0004C9EC
	private bool CheckNeighbours()
	{
		List<Transform> list = new List<Transform>();
		List<GameObject> list2 = new List<GameObject>();
		bool result = false;
		foreach (KeyValuePair<Transform, Vector2> keyValuePair in this.GetNeighbours())
		{
			Transform key = keyValuePair.Key;
			if (key.transform.parent == base.transform.parent)
			{
				list.Add(key);
			}
			else if (this.NeighbourMatches(key, keyValuePair.Value))
			{
				list.Add(key);
				JigsawPiece component = key.GetComponent<JigsawPiece>();
				Vector2 neighbourPosition = component.GetNeighbourPosition(base.transform.position);
				this.AnnounceMovement(base.transform.position, neighbourPosition);
				base.transform.position = neighbourPosition;
				if (key.parent.tag == "Finish")
				{
					Transform parent = key.parent;
					Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
					foreach (Transform transform in componentsInChildren)
					{
						if (!(transform.tag == "Finish") && !(transform == key))
						{
							transform.SetParent(base.transform.parent);
						}
					}
					key.SetParent(base.transform.parent);
					list2.Add(parent.gameObject);
				}
				else
				{
					key.SetParent(base.transform.parent);
				}
				result = true;
				component.GotMatched(key, keyValuePair.Value);
				this.MatchedSomething(key, keyValuePair.Value);
				Audio.self.playOneShot("b7d47f7c-c12c-4bb5-adb7-d70a5f7dd6c0", 1f);
				break;
			}
		}
		foreach (Transform neighbour in list)
		{
			this.RemoveNeighbour(neighbour);
		}
		foreach (GameObject obj in list2)
		{
			UnityEngine.Object.Destroy(obj);
		}
		return result;
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x0004E898 File Offset: 0x0004CC98
	protected virtual bool NeighbourMatches(Transform neighbour, Vector2 signs)
	{
		bool flag = true;
		bool flag2 = true;
		Vector2 vector = new Vector2(Mathf.Abs(neighbour.position.x - base.transform.position.x), Mathf.Abs(neighbour.position.y - base.transform.position.y));
		if (vector.x > 1.866f || vector.y > 1.866f)
		{
			return false;
		}
		int num = (int)signs.x;
		if (num != 0)
		{
			if (num == -1 || num == 1)
			{
				flag &= (neighbour.position.x * signs.x > base.transform.position.x * signs.x);
				flag &= (vector.x > 1.466f);
				flag &= (vector.x < 1.866f);
			}
		}
		else
		{
			flag &= (vector.x < 0.2f);
		}
		int num2 = (int)signs.y;
		if (num2 != 0)
		{
			if (num2 == -1 || num2 == 1)
			{
				flag2 &= (neighbour.position.y * signs.y > base.transform.position.y * signs.y);
				flag2 &= (vector.y > 1.466f);
				flag2 &= (vector.y < 1.866f);
			}
		}
		else
		{
			flag2 &= (vector.y < 0.2f);
		}
		return flag && flag2;
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x0004EA68 File Offset: 0x0004CE68
	protected virtual Vector2 GetNeighbourPosition(Vector2 nPos)
	{
		Vector2 vector = new Vector2(Mathf.Abs(nPos.x - base.transform.position.x), Mathf.Abs(nPos.y - base.transform.position.y));
		float num;
		if (vector.x < 1.466f)
		{
			num = 0f;
		}
		else
		{
			num = Mathf.Sign(nPos.x - base.transform.position.x);
		}
		float num2;
		if (vector.y < 1.466f)
		{
			num2 = 0f;
		}
		else
		{
			num2 = Mathf.Sign(nPos.y - base.transform.position.y);
		}
		nPos.x = base.transform.position.x + 1.666f * num;
		nPos.y = base.transform.position.y + 1.666f * num2;
		return nPos;
	}

	// Token: 0x06001785 RID: 6021 RVA: 0x0004EB88 File Offset: 0x0004CF88
	private IEnumerator EndingCoroutine(bool monster)
	{
		Global.self.canBePaused = false;
		this.GetComponentsInPuzzleStats(false).ToList<JigsawPiece>().ForEach(delegate(JigsawPiece x)
		{
			x.enabled = false;
		});
		this.GetPuzzleStats().UIScreenSecondary.GetComponent<AbstractUIScreen>().removeScreen();
		Audio.self.playOneShot("8628485e-6f83-4dc8-9a4d-d9fc2183c79b", 1f);
		Vector2 start = base.transform.parent.localPosition;
		JigsawPiece center = (from x in this.GetComponentsInPuzzleStats(false)
		where x.center
		select x).FirstOrDefault<JigsawPiece>();
		Vector2 end = center.transform.position + center.centerOffset * 1.666f;
		end *= -1f;
		float movingTime = center.centerMovement.keys[center.centerMovement.length - 1].time;
		float timer = 0f;
		while (timer != movingTime)
		{
			timer = Mathf.MoveTowards(timer, movingTime, Time.deltaTime);
			base.transform.parent.localPosition = Vector2.Lerp(start, end, center.centerMovement.Evaluate(timer));
			yield return null;
		}
		this.GetComponentInPuzzleStats<PuzzleJigsaw_Controller>().SaveJigsaw(true);
		if (monster)
		{
			JigsawPiece_Interchangeable[] pieces = this.GetComponentsInPuzzleStats(false);
			foreach (JigsawPiece_Interchangeable jigsawPiece_Interchangeable in pieces)
			{
				jigsawPiece_Interchangeable.transform.GetChild(0).gameObject.SetActive(true);
			}
			float lerpingTime = 2f;
			timer = 0f;
			while (timer != lerpingTime)
			{
				timer = Mathf.MoveTowards(timer, lerpingTime, Time.deltaTime);
				foreach (JigsawPiece_Interchangeable jigsawPiece_Interchangeable2 in pieces)
				{
					SpriteRenderer component = jigsawPiece_Interchangeable2.GetComponent<SpriteRenderer>();
					Color color = component.color;
					color.a = 1f - timer / lerpingTime;
					component.color = color;
				}
				yield return null;
			}
		}
		if (monster)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x0004EBAA File Offset: 0x0004CFAA
	protected virtual void GotMatched(Transform neighbour, Vector2 signs)
	{
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x0004EBAC File Offset: 0x0004CFAC
	protected virtual void MatchedSomething(Transform neighbour, Vector2 signs)
	{
	}

	// Token: 0x0400154E RID: 5454
	[HideInInspector]
	public bool grouped;

	// Token: 0x0400154F RID: 5455
	[HideInInspector]
	public bool finished;

	// Token: 0x04001550 RID: 5456
	public bool center;

	// Token: 0x04001551 RID: 5457
	public Vector2 centerOffset;

	// Token: 0x04001552 RID: 5458
	public AnimationCurve centerMovement;

	// Token: 0x04001553 RID: 5459
	protected List<KeyValuePair<Transform, Vector2>> neighbours = new List<KeyValuePair<Transform, Vector2>>();

	// Token: 0x04001555 RID: 5461
	protected int totalPieces;

	// Token: 0x04001556 RID: 5462
	protected const float HORIZONTAL_DELTA = 0.2f;

	// Token: 0x04001557 RID: 5463
	protected const float VERTICAL_DELTA = 0.2f;

	// Token: 0x04001558 RID: 5464
	protected const float HORIZONTAL_DISTANCE = 1.666f;

	// Token: 0x04001559 RID: 5465
	protected const float VERTICAL_DISTANCE = 1.666f;
}
