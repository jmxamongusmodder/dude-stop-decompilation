using System;
using System.Collections.Generic;
using System.IO;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000297 RID: 663
public class AudioLiveDebug : MonoBehaviour
{
	// Token: 0x06001037 RID: 4151 RVA: 0x00014D34 File Offset: 0x00013134
	public bool loadOnlyNewBank()
	{
		if (this.banksLoaded && !this.reloadBanks)
		{
			return false;
		}
		bool result = false;
		foreach (string text in Audio.self.AllBankList)
		{
			if (AudioLiveDebug.isNewBankExists(text))
			{
				RuntimeManager.UnloadBank(text);
				RuntimeManager.LoadBank(text, false);
				result = true;
				UIControl.addNewChatText("<color=green>New Bank file loaded for: " + text + "</color>");
			}
		}
		this.banksLoaded = result;
		return result;
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x00014DB8 File Offset: 0x000131B8
	public void similarLoopExist(Guid guid, Transform obj, string path)
	{
		int num = 0;
		string text = "There are few ({0}) similar loops playing for " + path + ":/n";
		foreach (KeyValuePair<string, EventInstance> keyValuePair in Audio.self.loopingSounds)
		{
			if (keyValuePair.Key.Contains(guid.ToString()))
			{
				text = text + keyValuePair.Key + "/n";
				num++;
			}
		}
		if (num > 1)
		{
			text = text + "Total sounds found: " + num;
			UnityEngine.Debug.LogWarning(string.Format(text, num));
		}
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x00014E84 File Offset: 0x00013284
	public bool getEventDescription(Guid guid, out string str)
	{
		str = guid.ToString();
		EventDescription eventDescription;
		if (RuntimeManager.StudioSystem.getEventByID(guid, out eventDescription) == RESULT.OK)
		{
			eventDescription.getPath(out str);
			return true;
		}
		return false;
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x00014EC3 File Offset: 0x000132C3
	public static string noAudioError(string guid)
	{
		return "<color=red>No event found: " + guid + ";</color>";
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x00014ED5 File Offset: 0x000132D5
	public static string noVoiceError(string levelName, string voiceName)
	{
		return string.Concat(new string[]
		{
			"<color=red>No voice found in: ",
			levelName,
			", voice: ",
			voiceName,
			";</color>"
		});
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x00014F04 File Offset: 0x00013304
	public static string getBanksNewPath(string bankName, string path)
	{
		string text = AudioLiveDebug.addFolderToPath(path);
		if (AudioLiveDebug.isNewBankNew(text, bankName))
		{
			DateTime lastWriteTime = File.GetLastWriteTime(text);
			if (!AudioLiveDebug.lastTimeModifiedBank.ContainsKey(bankName))
			{
				AudioLiveDebug.lastTimeModifiedBank.Add(bankName, lastWriteTime);
			}
			else
			{
				AudioLiveDebug.lastTimeModifiedBank[bankName] = lastWriteTime;
			}
			return text;
		}
		if (File.Exists(text))
		{
			return text;
		}
		return path;
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x00014F68 File Offset: 0x00013368
	public static bool isNewBankExists(string bankName)
	{
		string bankPath = RuntimeUtils.GetBankPath(bankName);
		string newPath = AudioLiveDebug.addFolderToPath(bankPath);
		return AudioLiveDebug.isNewBankNew(newPath, bankName);
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x00014F94 File Offset: 0x00013394
	private static bool isNewBankNew(string newPath, string bankName)
	{
		if (File.Exists(newPath))
		{
			DateTime lastWriteTime = File.GetLastWriteTime(newPath);
			DateTime d = default(DateTime);
			if (AudioLiveDebug.lastTimeModifiedBank.ContainsKey(bankName))
			{
				d = AudioLiveDebug.lastTimeModifiedBank[bankName];
			}
			if (d != lastWriteTime)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x00014FEB File Offset: 0x000133EB
	private static string addFolderToPath(string oldPath)
	{
		return oldPath;
	}

	// Token: 0x04000D4A RID: 3402
	public bool reloadBanks = true;

	// Token: 0x04000D4B RID: 3403
	private bool banksLoaded;

	// Token: 0x04000D4C RID: 3404
	public static Dictionary<string, DateTime> lastTimeModifiedBank = new Dictionary<string, DateTime>();
}
