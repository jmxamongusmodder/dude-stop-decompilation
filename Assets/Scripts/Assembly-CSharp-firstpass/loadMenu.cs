using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Color;

// Token: 0x0200055C RID: 1372
public class loadMenu : scrollMenuClass
{
	// Token: 0x06001F84 RID: 8068 RVA: 0x0009756C File Offset: 0x0009596C
	protected override void setList(int count)
	{
		this.fileList = SaveLoad.getSaveFiles();
		if (this.fileList.Count == 0)
		{
			this.buttonList.gameObject.SetActive(false);
			return;
		}
		base.setList(this.fileList.Count);
		List<string> list = new List<string>();
		List<saveSlot> list2 = new List<saveSlot>();
		IEnumerator enumerator = this.buttonList.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				list2.Add(transform.GetComponent<saveSlot>());
				list.Add(list2[list2.Count - 1].lastTime.text);
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
		IEnumerable<string> source = list;
		if (cache0 == null)
		{
			cache0 = new Func<string, DateTime>(DateTime.Parse);
		}
		list = source.OrderByDescending(cache0).ToList<string>();
		int num = UnityEngine.Random.Range(0, this.colorList.Length - 1);
		foreach (saveSlot saveSlot in list2)
		{
			int siblingIndex = list.IndexOf(saveSlot.lastTime.text);
			saveSlot.transform.SetSiblingIndex(siblingIndex);
			// saveSlot.GetComponent<Image>().color = this.colorList[num++];
			if (num >= this.colorList.Length)
			{
				num = 0;
			}
		}
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x00097714 File Offset: 0x00095B14
	public override void bBack()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.left, true);
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x0009773C File Offset: 0x00095B3C
	public void bNewGame()
	{
		if (!this.active)
		{
			return;
		}
		loadMenu.StartNewGame();
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x0009774F File Offset: 0x00095B4F
	public static void StartNewGame()
	{
		Global.self.CreateNewGame();
		Global.self.makeNewLevel(Global.self.levelPackMenu[Global.self.currentLevelPack], Vector2.right, true);
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x0009778C File Offset: 0x00095B8C
	protected override void setListItem(Transform item, int index)
	{
		item.GetComponent<saveSlot>().setActive(this.fileList[index], this);
		item.name = index.ToString();
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x000977B9 File Offset: 0x00095BB9
	public bool isActive()
	{
		return this.active;
	}

	// Token: 0x040022BB RID: 8891
	[Space(10f)]
	public int listLenght = 1;

	// Token: 0x040022BC RID: 8892
	private List<string> fileList = new List<string>();

	// Token: 0x040022BD RID: 8893
	[Space(10f)]
	public Color[] colorList;

    // Token: 0x040022BE RID: 8894
    [CompilerGenerated]
	private static Func<string, DateTime> cache0;
}
