using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000451 RID: 1105
public class PuzzleSlowLeftLine_Road : MonoBehaviour
{
	// Token: 0x06001C55 RID: 7253 RVA: 0x00077EFC File Offset: 0x000762FC
	private void Awake()
	{
		this.road = base.transform.GetChild(0);
		this.leftLines = base.transform.GetChild(1);
		this.rightLines = base.transform.GetChild(2);
		this.leftSidewalk = base.transform.GetChild(3);
		this.rightSidewalk = base.transform.GetChild(4);
		this.lastRoad = this.AlignObjects(this.road, 0f);
		this.lastLeftSidewalk = this.AlignObjects(this.leftSidewalk, 0f);
		this.lastRightSidewalk = this.AlignObjects(this.rightSidewalk, 0f);
		this.lastLeftLine = this.AlignObjects(this.leftLines, this.lineOffset);
		this.lastRightLine = this.AlignObjects(this.rightLines, this.lineOffset);
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x00077FE8 File Offset: 0x000763E8
	private void Start()
	{
		base.StartCoroutine(this.MovingCoroutine());
		Audio.self.playLoopSound("774dd5ef-11ab-474c-b4eb-42d73506fe9b");
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x00078006 File Offset: 0x00076406
	private void OnDisable()
	{
		if (this.soundPlaying)
		{
			Audio.self.stopLoopSound("774dd5ef-11ab-474c-b4eb-42d73506fe9b", true);
		}
	}

	// Token: 0x06001C58 RID: 7256 RVA: 0x00078024 File Offset: 0x00076424
	private Transform AlignObjects(Transform objects, float offset = 0f)
	{
		float num = Camera.main.ViewportToWorldPoint(Vector2.zero).y;
		Transform result = null;
		IEnumerator enumerator = objects.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				Bounds bounds = transform.GetComponent<Collider2D>().bounds;
				transform.position = new Vector3(transform.position.x, num + bounds.extents.y + offset);
				num += bounds.size.y + offset;
				result = transform;
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
		return result;
	}

	// Token: 0x06001C59 RID: 7257 RVA: 0x000780FC File Offset: 0x000764FC
	private Transform MoveObjects(Transform parent, Transform lastObject, float offset = 0f)
	{
		IEnumerator enumerator = parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.position += new Vector3(0f, -this.movementSpeed * Time.deltaTime);
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
		IEnumerator enumerator2 = parent.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				Transform transform2 = (Transform)obj2;
				Bounds bounds = transform2.GetComponent<Collider2D>().bounds;
				if (!GeometryUtility.TestPlanesAABB(this.planes, bounds) && transform2.position.y < 0f)
				{
					Bounds bounds2 = lastObject.GetComponent<Collider2D>().bounds;
					transform2.position = new Vector3(transform2.position.x, lastObject.position.y + bounds2.extents.y + bounds.extents.y + offset);
					lastObject = transform2;
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator2 as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
		return lastObject;
	}

	// Token: 0x06001C5A RID: 7258 RVA: 0x00078264 File Offset: 0x00076664
	private IEnumerator MovingCoroutine()
	{
		for (;;)
		{
			this.lastRoad = this.MoveObjects(this.road, this.lastRoad, 0f);
			this.lastLeftSidewalk = this.MoveObjects(this.leftSidewalk, this.lastLeftSidewalk, 0f);
			this.lastRightSidewalk = this.MoveObjects(this.rightSidewalk, this.lastRightSidewalk, 0f);
			this.lastLeftLine = this.MoveObjects(this.leftLines, this.lastLeftLine, this.lineOffset);
			this.lastRightLine = this.MoveObjects(this.rightLines, this.lastRightLine, this.lineOffset);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001AB0 RID: 6832
	public float movementSpeed;

	// Token: 0x04001AB1 RID: 6833
	public float lineOffset;

	// Token: 0x04001AB2 RID: 6834
	private Transform road;

	// Token: 0x04001AB3 RID: 6835
	private Transform leftLines;

	// Token: 0x04001AB4 RID: 6836
	private Transform rightLines;

	// Token: 0x04001AB5 RID: 6837
	private Transform leftSidewalk;

	// Token: 0x04001AB6 RID: 6838
	private Transform rightSidewalk;

	// Token: 0x04001AB7 RID: 6839
	private Plane[] planes;

	// Token: 0x04001AB8 RID: 6840
	private Transform lastRoad;

	// Token: 0x04001AB9 RID: 6841
	private Transform lastLeftLine;

	// Token: 0x04001ABA RID: 6842
	private Transform lastRightLine;

	// Token: 0x04001ABB RID: 6843
	private Transform lastLeftSidewalk;

	// Token: 0x04001ABC RID: 6844
	private Transform lastRightSidewalk;

	// Token: 0x04001ABD RID: 6845
	private bool soundPlaying;
}
