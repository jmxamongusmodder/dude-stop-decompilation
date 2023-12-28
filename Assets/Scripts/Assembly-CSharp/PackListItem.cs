using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200056B RID: 1387
public class PackListItem : MonoBehaviour
{
	// Token: 0x06002001 RID: 8193 RVA: 0x0009BD5C File Offset: 0x0009A15C
	[ContextMenu("Auto Fill")]
	private void fillList()
	{
		int num = 0;
		bool flag = false;
		foreach (Transform transform in Global.self.levelPackMenu)
		{
			if (transform.name == base.transform.parent.name)
			{
				flag = true;
				break;
			}
			num++;
		}
		if (!flag)
		{
			Debug.LogError("Can't AutoFill - pack doesn't exist in Global.self.levelPackMenu list");
			return;
		}
		Transform transform2 = UIControl.self.packsGraph.GetComponent<PackListToolControl>().listParent[num];
		PuzzleStats[] array = Resources.FindObjectsOfTypeAll<PuzzleStats>();
		List<Transform> list = new List<Transform>();
		IEnumerator enumerator = transform2.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform3 = (Transform)obj;
				foreach (PuzzleStats puzzleStats in array)
				{
					if (transform3.name == puzzleStats.transform.name)
					{
						if (!puzzleStats.HasBadEnd && !puzzleStats.HasGoodEnd)
						{
							Debug.Log(puzzleStats.transform.name + " is a cup puzzle. Assigne it to AwardController.", puzzleStats.transform.gameObject);
						}
						else
						{
							list.Add(puzzleStats.transform);
						}
					}
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
		this.List = list.ToArray();
	}

	// Token: 0x06002002 RID: 8194 RVA: 0x0009BEEC File Offset: 0x0009A2EC
	[ContextMenu("Limit to 1 (all packs)")]
	private void limitTo1()
	{
		PackListItem[] array = Resources.FindObjectsOfTypeAll<PackListItem>();
		foreach (PackListItem packListItem in array)
		{
			if (packListItem.randomOrder == packRandomnes.None)
			{
				packListItem.Count = 1;
			}
		}
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x0009BF2C File Offset: 0x0009A32C
	[ContextMenu("Remove limit (all packs)")]
	private void removeLimit()
	{
		PackListItem[] array = Resources.FindObjectsOfTypeAll<PackListItem>();
		foreach (PackListItem packListItem in array)
		{
			if (packListItem.randomOrder == packRandomnes.None)
			{
				packListItem.Count = 0;
			}
		}
	}

	// Token: 0x0400231B RID: 8987
	[Tooltip("Skip this blog for debug reasons")]
	public bool skipInEditor;

	// Token: 0x0400231C RID: 8988
	[Range(0f, 15f)]
	[Tooltip("How much items to take from this list. 0 - all of them")]
	public int Count = 1;

	// Token: 0x0400231D RID: 8989
	[Tooltip("Take items in random order")]
	public packRandomnes randomOrder;

	// Token: 0x0400231E RID: 8990
	[Range(0f, 1f)]
	[Tooltip("Roll a dice to even consider this list or not")]
	public float Chance = 1f;

	// Token: 0x0400231F RID: 8991
	[Tooltip("Only add this levels ifCondition is true")]
	public ifCondition condition;

	// Token: 0x04002320 RID: 8992
	[Tooltip("Lit of all puzzles or cards that can be used")]
	public Transform[] List;
}
