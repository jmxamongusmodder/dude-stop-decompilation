using System;
using System.Collections;
using DudeStop.Analytics;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class AnalyticsComponent : MonoBehaviour, IContainer
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00012EF1 File Offset: 0x000112F1
	public static AnalyticsComponent self
	{
		get
		{
			if (AnalyticsComponent._self == null)
			{
				AnalyticsComponent._self = Global.self.currentAnalytics;
			}
			return AnalyticsComponent._self;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00012F17 File Offset: 0x00011317
	// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x00012F23 File Offset: 0x00011323
	public static bool analyticsEnabled
	{
		get
		{
			return AnalyticsComponent.self._analyticsEnabled;
		}
		set
		{
			AnalyticsComponent.self._analyticsEnabled = value;
			AnalyticsComponent.self.Log("Analytics changed by PLAYER to: " + value);
			if (value)
			{
				AnalyticsComponent.self.ConnectAnalytics();
			}
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00012F5C File Offset: 0x0001135C
	private Analytics Analytics
	{
		get
		{
			if (this._analytics == null)
			{
				this._analytics = new Analytics(this);
			}
			if (this._analytics.address != this.address)
			{
				this._analytics.SetAddress(this.address);
			}
			return this._analytics;
		}
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00012FB2 File Offset: 0x000113B2
	private void Awake()
	{
		Application.logMessageReceived += this.OnLog;
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x00012FC8 File Offset: 0x000113C8
	private void OnLog(string condition, string trace, LogType type)
	{
		if (!this.internetAccessAllowed || !AnalyticsComponent.analyticsEnabled)
		{
			return;
		}
		string text = "LOG_MESSAGE";
		switch (type)
		{
		case LogType.Error:
			text = "LOG_ERROR";
			break;
		case LogType.Assert:
			text = "LOG_ASSERT";
			break;
		case LogType.Warning:
			text = "LOG_WARNING";
			break;
		case LogType.Exception:
			text = "LOG_EXCEPTION";
			break;
		}
		if (text == "LOG_MESSAGE")
		{
			return;
		}
		if (Global.self.currPuzzle != null)
		{
			this.Report(Global.self.currPuzzle.name, text, condition + "\n" + trace);
		}
		else
		{
			this.Report(string.Empty, text, condition + "\n" + trace);
		}
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x000130A1 File Offset: 0x000114A1
	private void Report(string puzzleName, string key, string value)
	{
		this.Log(string.Format("REPORTING: {0} {1} {2}", puzzleName, key, value));
		if (this.internetAccessAllowed && AnalyticsComponent.analyticsEnabled)
		{
			this.Analytics.Report(puzzleName, key, value);
		}
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x000130DC File Offset: 0x000114DC
	private void FlushAnalytics(bool immediate)
	{
		if (this.internetAccessAllowed && AnalyticsComponent.analyticsEnabled)
		{
			if (!this.Analytics.Initialized)
			{
				this.ConnectAnalytics();
			}
			else if (this.Analytics.Flush(this.flushEntryCount, immediate))
			{
				this.Log("Flushing data to the server...");
			}
		}
		else
		{
			this.Log("Flushing data to the server...");
		}
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x0001314B File Offset: 0x0001154B
	public void Coroutine(IEnumerator func)
	{
		base.StartCoroutine(func);
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x00013158 File Offset: 0x00011558
	public void Fail()
	{
		this.Log("Connection failed...");
		if (++this.fails == this.maxFailCount)
		{
			this.DisableInternetAccess();
		}
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x00013192 File Offset: 0x00011592
	public void DisableInternetAccess()
	{
		this.Log("ANALYTICS DISABLED");
		this.internetAccessAllowed = false;
		this._analytics = null;
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x000131B0 File Offset: 0x000115B0
	private void Log(string msg)
	{
		if (!this.showDebugInfo)
		{
			return;
		}
		if (this.internetAccessAllowed && AnalyticsComponent.analyticsEnabled)
		{
			Debug.Log("<color=red>ANALYTICS: " + msg + "</color>");
		}
		else if (AnalyticsComponent.analyticsEnabled && !this.internetAccessAllowed)
		{
			Debug.Log("<color=blue>ANALYTICS: " + msg + "</color>");
		}
		else if (!AnalyticsComponent.analyticsEnabled && !this.internetAccessAllowed)
		{
			Debug.Log("<color=grey>ANALYTICS: " + msg + "</color>");
		}
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x00013254 File Offset: 0x00011654
	private void ConnectAnalytics()
	{
		this.Log(string.Format("Connecting player {0} with session {1}", this.userID, this.sessionID));
		if (this.internetAccessAllowed && AnalyticsComponent.analyticsEnabled)
		{
			this.Analytics.Init(this.userID, this.sessionID, VersionControl.self.version);
		}
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x000132B4 File Offset: 0x000116B4
	public static void SetAnalytics(bool on, string userID, string sessionID)
	{
		AnalyticsComponent.self.Log("Load Analytics from PlayerPrefs. Set it to: " + on);
		AnalyticsComponent.self.userID = "...";
		AnalyticsComponent.self.sessionID = "...";
		AnalyticsComponent.self._analyticsEnabled = on;
		if (on)
		{
			AnalyticsComponent.self.ConnectAnalytics();
		}
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00013314 File Offset: 0x00011714
	public static void Flush(bool immediate = false)
	{
		AnalyticsComponent.self.FlushAnalytics(immediate);
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00013321 File Offset: 0x00011721
	public static void GameLoaded()
	{
		AnalyticsComponent.self.Report(string.Empty, "GAME_LOADED", string.Empty);
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x0001333C File Offset: 0x0001173C
	public static void GameClosed()
	{
		AnalyticsComponent.self.Report(string.Empty, "GAME_CLOSED", string.Empty);
		AnalyticsComponent.Flush(true);
		Application.logMessageReceived -= AnalyticsComponent.self.OnLog;
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00013372 File Offset: 0x00011772
	public static void NewGameStarted(string saveFileGUID)
	{
		AnalyticsComponent.self.Analytics.SetSaveID(saveFileGUID);
		AnalyticsComponent.self.Report(string.Empty, "SAVE_FILE_CREATED", saveFileGUID);
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00013399 File Offset: 0x00011799
	public static void SaveFileLoaded(string saveFileGUID)
	{
		AnalyticsComponent.self.Analytics.SetSaveID(saveFileGUID);
		AnalyticsComponent.self.Report(string.Empty, "SAVE_FILE_LOADED", saveFileGUID);
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x000133C0 File Offset: 0x000117C0
	public static void OptionsChanged(string optionsType, string value)
	{
		AnalyticsComponent.self.Report("OPTIONS_CHANGED", optionsType, value);
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x000133D3 File Offset: 0x000117D3
	public static void OptionsChanged(string optionsType, bool on)
	{
		AnalyticsComponent.OptionsChanged(optionsType, (!on) ? "FALSE" : "TRUE");
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x000133F0 File Offset: 0x000117F0
	public static void OptionsChanged(string optionsType, int value)
	{
		AnalyticsComponent.OptionsChanged(optionsType, value.ToString());
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x00013405 File Offset: 0x00011805
	public static void PuzzleStarted(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_STARTED", string.Empty);
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x0001341C File Offset: 0x0001181C
	public static void PuzzleFinished(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_FINISHED", string.Empty);
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00013434 File Offset: 0x00011834
	public static void PuzzleAborted(string puzzleName, int packIndex)
	{
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_ABORTED", string.Empty);
		AnalyticsComponent.self.Report("PACK_" + (packIndex + 1).ToString(), "PACK_ABORTED", string.Empty);
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00013485 File Offset: 0x00011885
	public static void PuzzleFinishedAsMonster(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_FINISHED", "MONSTER");
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0001349C File Offset: 0x0001189C
	public static void PuzzleFinishedAsGood(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_FINISHED", "GOOD");
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x000134B3 File Offset: 0x000118B3
	public static void PuzzleFinishedAsNone(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_FINISHED", "NONE");
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x000134CA File Offset: 0x000118CA
	public static void PuzzleFinishedStats(string puzzleName, int clicks, float seconds)
	{
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_FINISHED_CLICKS", clicks.ToString());
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_FINISHED_SECONDS", seconds.ToString("F2"));
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00013505 File Offset: 0x00011905
	public static void PuzzleAbortedStats(string puzzleName, int clicks, float seconds)
	{
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_ABORTED_CLICKS", clicks.ToString());
		AnalyticsComponent.self.Report(puzzleName, "PUZZLE_ABORTED_SECONDS", seconds.ToString("F2"));
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00013540 File Offset: 0x00011940
	public static void PuzzleFinishedAsMonsterExamPack(string question)
	{
		AnalyticsComponent.self.Report(question, "EXAM_PUZZLE_ANSWERED", "MONSTER");
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x00013557 File Offset: 0x00011957
	public static void PuzzleFinishedAsGoodExamPack(string question)
	{
		AnalyticsComponent.self.Report(question, "EXAM_PUZZLE_ANSWERED", "GOOD");
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0001356E File Offset: 0x0001196E
	public static void CupPuzzleStarted(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "CUP_PUZZLE_STARTED", string.Empty);
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x00013585 File Offset: 0x00011985
	public static void CupPuzzleFinished(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "CUP_PUZZLE_FINISHED", string.Empty);
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x0001359C File Offset: 0x0001199C
	public static void CupPuzzleAborted(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "CUP_PUZZLE_ABORTED", string.Empty);
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x000135B3 File Offset: 0x000119B3
	public static void PackStarted(int packIndex)
	{
		AnalyticsComponent.self.Report("PACK_" + packIndex, "PACK_STARTED", string.Empty);
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x000135D9 File Offset: 0x000119D9
	public static void PackFinished(int index)
	{
		AnalyticsComponent.self.Report("PACK_" + index, "PACK_FINISHED", string.Empty);
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x000135FF File Offset: 0x000119FF
	public static void JigSawCollected(string puzzleName)
	{
		AnalyticsComponent.self.Report(puzzleName, "JIGSAW_COLLECTED", string.Empty);
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00013616 File Offset: 0x00011A16
	public static void CupAcquired(string puzzleName, AwardName award)
	{
		AnalyticsComponent.self.Report(puzzleName, "CUP_ACQUIRED", award.ToString());
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x00013638 File Offset: 0x00011A38
	public static void CupAcquired(int packIndex, AwardName award)
	{
		AnalyticsComponent.self.Report("PACK_" + (packIndex + 1).ToString(), "CUP_ACQUIRED", award.ToString());
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0001367C File Offset: 0x00011A7C
	public static void DuckRebBlueButtonClick(bool red)
	{
		if (red)
		{
			AnalyticsComponent.self.Report("PACK_7", "PACK7_RED_BUTTON", string.Empty);
		}
		else
		{
			AnalyticsComponent.self.Report("PACK_7", "PACK7_BLUE_BUTTON", string.Empty);
		}
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x000136BB File Offset: 0x00011ABB
	public static void DuckEndButtonYes()
	{
		AnalyticsComponent.self.Report("PACK_12", "PACK12_END_BUTTON_YES", string.Empty);
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x000136D6 File Offset: 0x00011AD6
	public static void DuckEndButtonNo()
	{
		AnalyticsComponent.self.Report("PACK_12", "PACK12_END_BUTTON_NO", string.Empty);
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x000136F1 File Offset: 0x00011AF1
	public static void SocialMedia(string website)
	{
		AnalyticsComponent.self.Report(string.Empty, "SOC_MEDIA", website);
	}

	// Token: 0x04000CE1 RID: 3297
	private static AnalyticsComponent _self;

	// Token: 0x04000CE2 RID: 3298
	private bool _analyticsEnabled = true;

	// Token: 0x04000CE3 RID: 3299
	public bool internetAccessAllowed;

	// Token: 0x04000CE4 RID: 3300
	[SerializeField]
	private bool showDebugInfo;

	// Token: 0x04000CE5 RID: 3301
	[SerializeField]
	private int flushEntryCount = 25;

	// Token: 0x04000CE6 RID: 3302
	[SerializeField]
	private int maxFailCount = 5;

	// Token: 0x04000CE7 RID: 3303
	[SerializeField]
	private string address;

	// Token: 0x04000CE8 RID: 3304
	private int fails;

	// Token: 0x04000CE9 RID: 3305
	private Analytics _analytics;

	// Token: 0x04000CEA RID: 3306
	private string userID;

	// Token: 0x04000CEB RID: 3307
	private string sessionID;
}
