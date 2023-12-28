using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class CheatSetRewards : MonoBehaviour
{
	// Token: 0x06001458 RID: 5208 RVA: 0x00034FC8 File Offset: 0x000333C8
	[ContextMenu("Save List to SaveFile")]
	public void saveAwards()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		foreach (AwardNameCheatList awardNameCheatList in this.list)
		{
			Global.self.cupList[awardNameCheatList.name] = awardNameCheatList.state;
			if (awardNameCheatList.packIndex > this.saveTillPack - 3)
			{
				break;
			}
		}
		Global.self.Save();
		Global.self.unlockNextPack = true;
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x00035070 File Offset: 0x00033470
	[ContextMenu("Set Completed Times to 1")]
	public void saveCompletedTimes()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		for (int i = 0; i < this.saveTillPack; i++)
		{
			SerializablePackSavedStats.Get(i).completedTimes = 2;
			SerializablePackSavedStats.Get(i).solvedAsBad = 1;
			SerializablePackSavedStats.Get(i).solvedAsGood = 1;
		}
		Global.self.Save();
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x000350D0 File Offset: 0x000334D0
	[ContextMenu("Save Unlock Till Pack to SaveFile")]
	public void saveTillPackAwards()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		foreach (AwardNameCheatList awardNameCheatList in this.list)
		{
			Global.self.cupList[awardNameCheatList.name] = CupStatus.Exist;
			if (awardNameCheatList.packIndex > this.saveTillPack - 3)
			{
				break;
			}
		}
		Global.self.Save();
		Global.self.unlockNextPack = true;
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x00035174 File Offset: 0x00033574
	[ContextMenu("Unlock All Awards")]
	public void saveUnlockAwards()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		foreach (AwardNameCheatList awardNameCheatList in this.list)
		{
			Global.self.cupList[awardNameCheatList.name] = CupStatus.Exist;
		}
		Global.self.Save();
		Global.self.unlockNextPack = true;
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x00035200 File Offset: 0x00033600
	[ContextMenu("Reset SaveFile (lock everything)")]
	public void saveLockAwards()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		for (int i = 0; i < Global.self.levelPackMenu.Length; i++)
		{
			SerializablePackSavedStats.Get(i).completedTimes = 0;
			SerializablePackSavedStats.Get(i).solvedAsBad = 0;
			SerializablePackSavedStats.Get(i).solvedAsGood = 0;
		}
		foreach (AwardNameCheatList awardNameCheatList in this.list)
		{
			Global.self.cupList[awardNameCheatList.name] = CupStatus.Empty;
		}
		Global.self.Save();
		Global.self.unlockNextPack = true;
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x000352CC File Offset: 0x000336CC
	[ContextMenu("Fill List With Settings")]
	public void fillList()
	{
		this.list = new List<AwardNameCheatList>();
		int num = 0;
		foreach (Transform transform in Global.self.levelPackMenu)
		{
			AwardController awardControllerScript = transform.GetComponent<levelPackControl>().awardControllerScript;
			AwardName toUnlockNextPack = awardControllerScript.toUnlockNextPack;
			AwardName toUnlockNextPack2 = awardControllerScript.toUnlockNextPack2;
			if (toUnlockNextPack != AwardName.None)
			{
				this.list.Add(new AwardNameCheatList(toUnlockNextPack, this.fillInState, num));
			}
			if (toUnlockNextPack2 != AwardName.None)
			{
				this.list.Add(new AwardNameCheatList(toUnlockNextPack2, this.fillInState, num));
			}
			num++;
		}
	}

	// Token: 0x0400119A RID: 4506
	[Header("Cups")]
	public bool applyToAll;

	// Token: 0x0400119B RID: 4507
	public CupStatus cupStatus;

	// Token: 0x0400119C RID: 4508
	[Header("Packs")]
	public int saveTillPack = 13;

	// Token: 0x0400119D RID: 4509
	public CupStatus fillInState;

	// Token: 0x0400119E RID: 4510
	[Space(10f)]
	public List<AwardNameCheatList> list;
}
