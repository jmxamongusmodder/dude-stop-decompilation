using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000479 RID: 1145
[Serializable]
public class SerializablePackSavedStats
{
	// Token: 0x06001D74 RID: 7540 RVA: 0x000814BA File Offset: 0x0007F8BA
	public SerializablePackSavedStats(string name)
	{
		this.packName = name;
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06001D76 RID: 7542 RVA: 0x000814DD File Offset: 0x0007F8DD
	// (set) Token: 0x06001D75 RID: 7541 RVA: 0x000814C9 File Offset: 0x0007F8C9
	public int bestBadSolvedPuzzleCount
	{
		get
		{
			return this._bestBadSolvedPuzzleCount;
		}
		set
		{
			this._bestBadSolvedPuzzleCount = Mathf.Max(value, this._bestBadSolvedPuzzleCount);
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06001D78 RID: 7544 RVA: 0x000814F9 File Offset: 0x0007F8F9
	// (set) Token: 0x06001D77 RID: 7543 RVA: 0x000814E5 File Offset: 0x0007F8E5
	public int bestGoodSolvedPuzzleCount
	{
		get
		{
			return this._bestGoodSolvedPuzzleCount;
		}
		set
		{
			this._bestGoodSolvedPuzzleCount = Mathf.Max(value, this._bestGoodSolvedPuzzleCount);
		}
	}

	// Token: 0x06001D79 RID: 7545 RVA: 0x00081504 File Offset: 0x0007F904
	public static SerializablePackSavedStats Get(string name)
	{
		SerializablePackSavedStats serializablePackSavedStats = Global.self.packSavedStats.SingleOrDefault((SerializablePackSavedStats x) => x.packName == name);
		if (serializablePackSavedStats == null)
		{
			serializablePackSavedStats = new SerializablePackSavedStats(name);
			Global.self.packSavedStats.Add(serializablePackSavedStats);
		}
		return serializablePackSavedStats;
	}

	// Token: 0x06001D7A RID: 7546 RVA: 0x00081560 File Offset: 0x0007F960
	public static SerializablePackSavedStats Get(int index)
	{
		string name = Global.self.levelPackMenu[index].name;
		return SerializablePackSavedStats.Get(name);
	}

	// Token: 0x04001C25 RID: 7205
	public string packName;

	// Token: 0x04001C26 RID: 7206
	public int completedTimes;

	// Token: 0x04001C27 RID: 7207
	public int solvedAsBad;

	// Token: 0x04001C28 RID: 7208
	public int solvedAsGood;

	// Token: 0x04001C29 RID: 7209
	public int jigSawPiecesFound;

	// Token: 0x04001C2A RID: 7210
	public bool packClickedOn;

	// Token: 0x04001C2B RID: 7211
	public bool packShowedOnce;

	// Token: 0x04001C2C RID: 7212
	public int badEndCount;

	// Token: 0x04001C2D RID: 7213
	public int goodEndCount;

	// Token: 0x04001C2E RID: 7214
	private int _bestBadSolvedPuzzleCount;

	// Token: 0x04001C2F RID: 7215
	private int _bestGoodSolvedPuzzleCount;
}
