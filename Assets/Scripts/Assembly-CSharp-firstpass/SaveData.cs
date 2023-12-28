using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// Token: 0x02000473 RID: 1139
[Serializable]
public class SaveData : ISerializable
{
	// Token: 0x06001D4A RID: 7498 RVA: 0x00080769 File Offset: 0x0007EB69
	public SaveData()
	{
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x00080774 File Offset: 0x0007EB74
	public SaveData(SerializationInfo info, StreamingContext ctxt)
	{
		this.cupList = new Dictionary<AwardName, CupStatus>();
		AwardName[] array = (AwardName[])Enum.GetValues(typeof(AwardName));
		foreach (AwardName awardName in array)
		{
			if (awardName != AwardName.None)
			{
				CupStatus cupStatus = this.getCupStatus(info, awardName.ToString(), CupStatus.Empty);
				if (cupStatus == CupStatus.ShowAnimation)
				{
					cupStatus = CupStatus.Exist;
				}
				this.cupList.Add(awardName, cupStatus);
			}
		}
		this.legoCupPieces = info.GetValue("legoCupPieces");
		this.jigsawPieces = info.GetValue("jigsawPieces");
		this.unlockedJigsawPieces = info.GetValue("unlockedJigsawPieces");
		this.packSavedStats = info.GetValue("packSavedStats");
		this.puzzleSavedStats = info.GetValue("puzzleSavedStats");
		this.lastPlayed = info.GetValue("lastPlayed");
		this.gameStats = info.GetValue("gameStats");
		this.saveFileGUID = info.GetValue("saveFileGUID");
		this.totalPlayedTime = info.GetValue("totalPlayedTime");
		if (this.puzzleSavedStats != null && this.puzzleSavedStats.Count > 0)
		{
			foreach (SerializablePuzzleStats serializablePuzzleStats in this.puzzleSavedStats)
			{
				serializablePuzzleStats.resetVoices();
			}
		}
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x00080904 File Offset: 0x0007ED04
	private CupStatus getCupStatus(SerializationInfo info, string name, CupStatus val = CupStatus.Empty)
	{
		CupStatus result;
		try
		{
			result = (CupStatus)info.GetValue(name, typeof(CupStatus));
		}
		catch
		{
			result = val;
		}
		return result;
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x00080948 File Offset: 0x0007ED48
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		Dictionary<AwardName, CupStatus>.KeyCollection keys = this.cupList.Keys;
		foreach (AwardName key in keys)
		{
			info.AddValue(key.ToString(), this.cupList[key]);
		}
		info.AddValue("legoCupPieces", this.legoCupPieces, typeof(List<LegoCupPiece>));
		info.AddValue("jigsawPieces", this.jigsawPieces, typeof(List<SerializableJigsawPiece>));
		info.AddValue("unlockedJigsawPieces", this.unlockedJigsawPieces, typeof(int));
		info.AddValue("puzzleSavedStats", this.puzzleSavedStats, typeof(List<SerializablePuzzleStats>));
		info.AddValue("packSavedStats", this.packSavedStats, typeof(List<SerializablePackSavedStats>));
		info.AddValue("lastPlayed", this.lastPlayed, typeof(DateTime));
		info.AddValue("gameStats", this.gameStats, typeof(SerializableGameStats));
		info.AddValue("saveFileGUID", this.saveFileGUID, typeof(string));
		info.AddValue("totalPlayedTime", this.totalPlayedTime, typeof(uint));
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x00080ACC File Offset: 0x0007EECC
	public void Populate()
	{
		this.cupList = Global.self.cupList;
		this.PopulateCupList();
		this.legoCupPieces = this.Default<List<LegoCupPiece>>(Global.self.legoCupPieces, null);
		this.jigsawPieces = this.Default<List<SerializableJigsawPiece>>(Global.self.jigsawPuzzlePieces, null);
		this.unlockedJigsawPieces = this.Default<int>(Global.self.unlockedJigsawPieces, 0);
		this.puzzleSavedStats = this.Default<List<SerializablePuzzleStats>>(Global.self.puzzleSavedStats, null);
		this.packSavedStats = this.Default<List<SerializablePackSavedStats>>(Global.self.packSavedStats, null);
		this.lastPlayed = DateTime.Now;
		this.gameStats = this.Default<SerializableGameStats>(Global.self.gameStats, null);
		this.saveFileGUID = this.Default<string>(Global.self.saveFileGUID, null);
		this.totalPlayedTime = this.Default<uint>(Global.self.totalPlayedTime, 0U);
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x00080BB4 File Offset: 0x0007EFB4
	private void PopulateCupList()
	{
		AwardName[] array = (AwardName[])Enum.GetValues(typeof(AwardName));
		foreach (AwardName key in array)
		{
			if (!this.cupList.ContainsKey(key))
			{
				this.cupList.Add(key, CupStatus.Empty);
			}
		}
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x00080C13 File Offset: 0x0007F013
	private T Default<T>(T val, T def = default(T))
	{
		return (val == null) ? def : val;
	}

	// Token: 0x04001BFA RID: 7162
	public bool corrupted;

	// Token: 0x04001BFB RID: 7163
	public Dictionary<AwardName, CupStatus> cupList;

	// Token: 0x04001BFC RID: 7164
	public List<SerializablePackSavedStats> packSavedStats;

	// Token: 0x04001BFD RID: 7165
	public List<LegoCupPiece> legoCupPieces;

	// Token: 0x04001BFE RID: 7166
	public List<SerializableJigsawPiece> jigsawPieces;

	// Token: 0x04001BFF RID: 7167
	public List<SerializablePuzzleStats> puzzleSavedStats;

	// Token: 0x04001C00 RID: 7168
	public int unlockedJigsawPieces;

	// Token: 0x04001C01 RID: 7169
	public DateTime lastPlayed;

	// Token: 0x04001C02 RID: 7170
	public SerializableGameStats gameStats;

	// Token: 0x04001C03 RID: 7171
	public string saveFileGUID;

	// Token: 0x04001C04 RID: 7172
	public uint totalPlayedTime;
}
