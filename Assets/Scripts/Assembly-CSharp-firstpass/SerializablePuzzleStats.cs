using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x0200047A RID: 1146
[Serializable]
public class SerializablePuzzleStats
{
	// Token: 0x06001D7B RID: 7547 RVA: 0x000815A0 File Offset: 0x0007F9A0
	public SerializablePuzzleStats(string name)
	{
		this.puzzleName = name;
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06001D7C RID: 7548 RVA: 0x000815AF File Offset: 0x0007F9AF
	public int playedTimes
	{
		get
		{
			return this.solvedAsBad + this.solvedAsGood;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06001D7D RID: 7549 RVA: 0x000815BE File Offset: 0x0007F9BE
	public int loadedTimes
	{
		get
		{
			return this.loadedTimesAfterBad + this.loadedTimesAfterGood + this.loadedTimesAfterNoEnding;
		}
	}

	// Token: 0x06001D7E RID: 7550 RVA: 0x000815D4 File Offset: 0x0007F9D4
	public void resetVoices()
	{
		this.usedBadEnding = string.Empty;
		this.usedGoodEnding = string.Empty;
		this.usedEndBadVoice = string.Empty;
		this.usedEndGoodVoice = string.Empty;
		this.usedStartBadVoice = string.Empty;
		this.usedStartGoodVoice = string.Empty;
		this.usedStartNullVoice = string.Empty;
		this.usedStartLast = string.Empty;
		this.usedUnknownLast = string.Empty;
	}

	// Token: 0x06001D7F RID: 7551 RVA: 0x00081644 File Offset: 0x0007FA44
	public bool tryUseOneTime(string fmodName)
	{
		if (this.oneTimeUse == null)
		{
			this.oneTimeUse = new List<string>();
		}
		if (this.oneTimeUse.Contains(fmodName))
		{
			return false;
		}
		this.oneTimeUse.Add(fmodName);
		return true;
	}

	// Token: 0x06001D80 RID: 7552 RVA: 0x0008167C File Offset: 0x0007FA7C
	public string getPrevEnding(bool monster)
	{
		if (monster)
		{
			return this.usedBadEnding;
		}
		return this.usedGoodEnding;
	}

	// Token: 0x06001D81 RID: 7553 RVA: 0x00081691 File Offset: 0x0007FA91
	public void savePrevEnding(string ending, bool monster)
	{
		if (monster)
		{
			this.usedBadEnding = ending;
		}
		else
		{
			this.usedGoodEnding = ending;
		}
	}

	// Token: 0x06001D82 RID: 7554 RVA: 0x000816AC File Offset: 0x0007FAAC
	public bool isFmodNameUsed(string fmodName, LevelVoice.Type type, bool? monster)
	{
		if (type == LevelVoice.Type.End)
		{
			if (monster == true)
			{
				if (fmodName != this.usedEndBadVoice)
				{
					this.usedEndBadVoice = fmodName;
					return false;
				}
			}
			else if (fmodName != this.usedEndGoodVoice)
			{
				this.usedEndGoodVoice = fmodName;
				return false;
			}
		}
		else if (type == LevelVoice.Type.Start)
		{
			if (fmodName == this.usedStartLast)
			{
				return true;
			}
			this.usedStartLast = fmodName;
			if (monster == true)
			{
				if (fmodName != this.usedStartBadVoice)
				{
					this.usedStartBadVoice = fmodName;
					return false;
				}
			}
			else if (monster == false)
			{
				if (fmodName != this.usedStartGoodVoice)
				{
					this.usedStartGoodVoice = fmodName;
					return false;
				}
			}
			else if (fmodName != this.usedStartNullVoice)
			{
				this.usedStartNullVoice = fmodName;
				return false;
			}
		}
		else if (fmodName != this.usedUnknownLast)
		{
			this.usedUnknownLast = fmodName;
			return false;
		}
		return true;
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x000817E4 File Offset: 0x0007FBE4
	public void addLoadedTimes(bool? monster)
	{
		if (monster == true)
		{
			this.loadedTimesAfterBad++;
		}
		else if (monster == false)
		{
			this.loadedTimesAfterGood++;
		}
		else
		{
			this.loadedTimesAfterNoEnding++;
		}
	}

	// Token: 0x06001D84 RID: 7556 RVA: 0x00081858 File Offset: 0x0007FC58
	public int getLoadedTimes(bool? monster)
	{
		if (monster == true)
		{
			return this.loadedTimesAfterBad;
		}
		if (monster == false)
		{
			return this.loadedTimesAfterGood;
		}
		return this.loadedTimesAfterNoEnding;
	}

	// Token: 0x06001D85 RID: 7557 RVA: 0x000818AC File Offset: 0x0007FCAC
	public static SerializablePuzzleStats Get(string name)
	{
		name = name.Replace("(Clone)", string.Empty);
		SerializablePuzzleStats serializablePuzzleStats = Global.self.puzzleSavedStats.SingleOrDefault((SerializablePuzzleStats x) => x.puzzleName == name);
		if (serializablePuzzleStats == null)
		{
			serializablePuzzleStats = new SerializablePuzzleStats(name);
			Global.self.puzzleSavedStats.Add(serializablePuzzleStats);
		}
		return serializablePuzzleStats;
	}

	// Token: 0x04001C30 RID: 7216
	private string puzzleName;

	// Token: 0x04001C31 RID: 7217
	public int jigSawPiecesFound;

	// Token: 0x04001C32 RID: 7218
	public int solvedAsGood;

	// Token: 0x04001C33 RID: 7219
	public int solvedAsBad;

	// Token: 0x04001C34 RID: 7220
	private int loadedTimesAfterBad;

	// Token: 0x04001C35 RID: 7221
	private int loadedTimesAfterGood;

	// Token: 0x04001C36 RID: 7222
	private int loadedTimesAfterNoEnding;

	// Token: 0x04001C37 RID: 7223
	private string usedBadEnding;

	// Token: 0x04001C38 RID: 7224
	private string usedGoodEnding;

	// Token: 0x04001C39 RID: 7225
	private string usedEndBadVoice;

	// Token: 0x04001C3A RID: 7226
	private string usedEndGoodVoice;

	// Token: 0x04001C3B RID: 7227
	private string usedStartBadVoice;

	// Token: 0x04001C3C RID: 7228
	private string usedStartGoodVoice;

	// Token: 0x04001C3D RID: 7229
	private string usedStartNullVoice;

	// Token: 0x04001C3E RID: 7230
	private string usedStartLast;

	// Token: 0x04001C3F RID: 7231
	private string usedUnknownLast;

	// Token: 0x04001C40 RID: 7232
	private List<string> oneTimeUse;
}
