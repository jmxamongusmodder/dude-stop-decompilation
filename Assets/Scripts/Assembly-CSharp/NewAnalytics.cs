using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class NewAnalytics : MonoBehaviour
{
	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06001711 RID: 5905 RVA: 0x0004BF46 File Offset: 0x0004A346
	// (set) Token: 0x06001712 RID: 5906 RVA: 0x0004BF71 File Offset: 0x0004A371
	public static NewAnalytics self
	{
		get
		{
			if (NewAnalytics._self == null)
			{
				NewAnalytics._self = GameObject.FindGameObjectWithTag("Global").GetComponent<NewAnalytics>();
			}
			return NewAnalytics._self;
		}
		set
		{
			NewAnalytics._self = value;
		}
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x0004BF7C File Offset: 0x0004A37C
	public static void GameStarted(string resolution)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"level",
				resolution
			}
		};
		NewAnalytics.self.SendToPatomkin("START", null, null);
		NewAnalytics.self.SendToPatomkin("Start resolution", data, null);
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x0004BFE1 File Offset: 0x0004A3E1
	public static void GameEnded()
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		NewAnalytics.self.SendToPatomkin("END", null, null);
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x0004C018 File Offset: 0x0004A418
	public static void LevelAborted(string level)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"level",
				level
			}
		};
		NewAnalytics.self.SendToPatomkin("Level aborted", data, null);
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x0004C06C File Offset: 0x0004A46C
	public static void LevelFinished(string level, bool monsterEnding, float time, int clicks)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"level",
				level
			},
			{
				"monster ending",
				monsterEnding
			},
			{
				"time",
				time
			},
			{
				"clicks",
				clicks
			}
		};
		NewAnalytics.self.SendToPatomkin("Level finished", data, null);
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x0004C0F4 File Offset: 0x0004A4F4
	public static void ResolutionChanged(string newResolution)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"resolution",
				newResolution
			}
		};
		NewAnalytics.self.SendToPatomkin("Resolution changed", data, null);
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x0004C148 File Offset: 0x0004A548
	public static void SoundChanged(int sound, int music)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"sound",
				sound
			},
			{
				"music",
				music
			}
		};
		NewAnalytics.self.SendToPatomkin("Sound changed", data, null);
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x0004C1B4 File Offset: 0x0004A5B4
	public static void FullscreenChanged(bool fullscreen)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"fullscreen",
				fullscreen
			}
		};
		NewAnalytics.self.SendToPatomkin("Fullscreen changed", data, null);
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x0004C20C File Offset: 0x0004A60C
	public static bool SendFeedback(string feedback, Action<bool> callback)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return false;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"feedback",
				feedback
			}
		};
		return NewAnalytics.self.SendToPatomkin("Feedback", data, callback);
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x0004C260 File Offset: 0x0004A660
	public static void PackFinished(string name, int times)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"times",
				times
			},
			{
				"name",
				name
			}
		};
		NewAnalytics.self.SendToPatomkin("Pack finished", data, null);
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x0004C2C4 File Offset: 0x0004A6C4
	public static void GreenlightScreenClosed(bool agreed)
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		Dictionary<string, object> data = new Dictionary<string, object>
		{
			{
				"agreed",
				agreed
			}
		};
		NewAnalytics.self.SendToPatomkin("Greenlight screen", data, null);
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x0004C31C File Offset: 0x0004A71C
	public static void FocusLost()
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		NewAnalytics.self.SendToPatomkin("Focus lost", null, null);
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x0004C350 File Offset: 0x0004A750
	public static void FocusRegained()
	{
		if (NewAnalytics.self == null || !NewAnalytics.self.sendAnalytics)
		{
			return;
		}
		NewAnalytics.self.SendToPatomkin("Focus regained", null, null);
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x0004C384 File Offset: 0x0004A784
	private void OnEnable()
	{
		Application.logMessageReceived += this.OnLog;
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x0004C397 File Offset: 0x0004A797
	private void OnDisable()
	{
		Application.logMessageReceived -= this.OnLog;
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x0004C3AC File Offset: 0x0004A7AC
	private void OnLog(string condition, string trace, LogType type)
	{
		if (!this.sendAnalytics)
		{
			return;
		}
		bool flag = false;
		string value = string.Empty;
		if (this.consoleMessages.ContainsKey(condition))
		{
			if (this.consoleMessages[condition] > 999)
			{
				return;
			}
			Dictionary<string, int> dictionary;
			(dictionary = this.consoleMessages)[condition] = dictionary[condition] + 1;
		}
		else
		{
			this.consoleMessages.Add(condition, 1);
		}
		switch (type)
		{
		case LogType.Error:
			flag = this.logError;
			value = "error";
			break;
		case LogType.Assert:
			flag = this.logAssert;
			value = "assert";
			break;
		case LogType.Warning:
			flag = this.logWarning;
			value = "warning";
			break;
		case LogType.Log:
			flag = this.logMessage;
			value = "log";
			break;
		case LogType.Exception:
			flag = this.logException;
			value = "exception";
			break;
		}
		if (flag)
		{
			Dictionary<string, object> data = new Dictionary<string, object>
			{
				{
					"condition",
					condition
				},
				{
					"trace",
					trace
				},
				{
					"count",
					this.consoleMessages[trace]
				},
				{
					"type",
					value
				}
			};
			this.SendToPatomkin("Console message", data, null);
		}
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x0004C500 File Offset: 0x0004A900
	private bool SendToPatomkin(string eventName, Dictionary<string, object> data = null, Action<bool> callback = null)
	{
		if (!this.sendToPatomkin)
		{
			return false;
		}
		if (eventName != "Console message")
		{
			this.consoleMessages.Clear();
		}
		WWWForm wwwform = new WWWForm();
		wwwform.AddField("event", eventName);
		wwwform.AddField("user", Global.self.playerID);
		wwwform.AddField("session", Global.self.sessionID);
		wwwform.AddField("version", VersionControl.GetVersion());
		if (data != null)
		{
			foreach (KeyValuePair<string, object> keyValuePair in data)
			{
				wwwform.AddField(keyValuePair.Key, keyValuePair.Value.ToString());
			}
		}
		WWW www = new WWW(this.address, wwwform);
		base.StartCoroutine(this.ProcessReport(www, callback));
		return true;
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x0004C600 File Offset: 0x0004AA00
	private IEnumerator ProcessReport(WWW www, Action<bool> callback = null)
	{
		yield return null;
		yield return www;
		bool result = true;
		if (www.error != null)
		{
			this.sendToPatomkin = false;
			Debug.Log(www.error);
			result = false;
		}
		else if (www.text != string.Empty)
		{
			Debug.Log(www.text);
			result = false;
		}
		if (callback != null)
		{
			callback(result);
		}
		yield break;
	}

	// Token: 0x040014DF RID: 5343
	public bool sendAnalytics;

	// Token: 0x040014E0 RID: 5344
	public bool sendToPatomkin = true;

	// Token: 0x040014E1 RID: 5345
	public bool sendToUnity;

	// Token: 0x040014E2 RID: 5346
	public string address = string.Empty;

	// Token: 0x040014E3 RID: 5347
	public bool logAssert;

	// Token: 0x040014E4 RID: 5348
	public bool logError;

	// Token: 0x040014E5 RID: 5349
	public bool logException;

	// Token: 0x040014E6 RID: 5350
	public bool logWarning;

	// Token: 0x040014E7 RID: 5351
	public bool logMessage;

	// Token: 0x040014E8 RID: 5352
	private Dictionary<string, int> consoleMessages = new Dictionary<string, int>();

	// Token: 0x040014E9 RID: 5353
	private static NewAnalytics _self;
}
