using System;

// Token: 0x02000478 RID: 1144
[Serializable]
public class SerializableGameStats
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06001D73 RID: 7539 RVA: 0x000814AE File Offset: 0x0007F8AE
	public static SerializableGameStats self
	{
		get
		{
			return Global.self.gameStats;
		}
	}

	// Token: 0x04001C1F RID: 7199
	public bool pack10CutscenePlayed;

	// Token: 0x04001C20 RID: 7200
	public bool pack11Unlocked;

	// Token: 0x04001C21 RID: 7201
	public bool isGameFinished;

	// Token: 0x04001C22 RID: 7202
	public bool isGameFinishedJustNow;

	// Token: 0x04001C23 RID: 7203
	public bool pack09DuckShowed;

	// Token: 0x04001C24 RID: 7204
	public bool pack09CinemaDuckPlayed;
}
