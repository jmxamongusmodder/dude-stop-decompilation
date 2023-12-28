using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public class PuzzleShopClothes_Flier : PuzzleBurger_Recipe
{
	// Token: 0x06001C20 RID: 7200 RVA: 0x000761C4 File Offset: 0x000745C4
	private void Awake()
	{
		PuzzleShopClothes_Flier.images.Clear();
		PuzzleShopClothes_Flier.correctImage = UnityEngine.Random.Range(0, 3);
		for (int i = 0; i < 3; i++)
		{
			if (i != PuzzleShopClothes_Flier.correctImage)
			{
				PuzzleShopClothes_Flier.images.Add(i);
			}
		}
		base.transform.GetChild(0).GetChild(PuzzleShopClothes_Flier.correctImage).gameObject.SetActive(true);
	}

	// Token: 0x04001A6F RID: 6767
	public static List<int> images = new List<int>();

	// Token: 0x04001A70 RID: 6768
	public static int correctImage;
}
