using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class SaveLoad
{
	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00080C2F File Offset: 0x0007F02F
	// (set) Token: 0x06001D53 RID: 7507 RVA: 0x00080C36 File Offset: 0x0007F036
	public static bool Saving { get; private set; }

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06001D54 RID: 7508 RVA: 0x00080C3E File Offset: 0x0007F03E
	// (set) Token: 0x06001D55 RID: 7509 RVA: 0x00080C45 File Offset: 0x0007F045
	public static bool Loading { get; private set; }

	// Token: 0x06001D56 RID: 7510 RVA: 0x00080C4D File Offset: 0x0007F04D
	public static void setInt(string item, int val)
	{
		PlayerPrefs.SetInt(item, val);
	}

	// Token: 0x06001D57 RID: 7511 RVA: 0x00080C56 File Offset: 0x0007F056
	public static void deleteKey(string item)
	{
		PlayerPrefs.DeleteKey(item);
	}

	// Token: 0x06001D58 RID: 7512 RVA: 0x00080C5E File Offset: 0x0007F05E
	public static void setStr(string item, string val)
	{
		PlayerPrefs.SetString(item, val);
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x00080C67 File Offset: 0x0007F067
	public static int getInt(string item, int val = -1)
	{
		return PlayerPrefs.GetInt(item, val);
	}

	// Token: 0x06001D5A RID: 7514 RVA: 0x00080C70 File Offset: 0x0007F070
	public static string getStr(string item, string val = null)
	{
		return PlayerPrefs.GetString(item, val);
	}

	// Token: 0x06001D5B RID: 7515 RVA: 0x00080C79 File Offset: 0x0007F079
	public static string GetStoragePath()
	{
		Directory.CreateDirectory(Application.dataPath + "/../Saves");
		return Application.dataPath + "/../Saves";
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x00080C9F File Offset: 0x0007F09F
	public static string filePath(string fileName)
	{
		return SaveLoad.GetStoragePath() + "/" + fileName;
	}

	// Token: 0x06001D5D RID: 7517 RVA: 0x00080CB1 File Offset: 0x0007F0B1
	public static string GetFullPath(string dir, string fileName)
	{
		return dir + "/" + fileName;
	}

	// Token: 0x06001D5E RID: 7518 RVA: 0x00080CBF File Offset: 0x0007F0BF
	public static string getFileName(int index)
	{
		return "SaveData" + index.ToString() + ".ds";
	}

	// Token: 0x06001D5F RID: 7519 RVA: 0x00080CE0 File Offset: 0x0007F0E0
	public static void GetTrueFileList()
	{
		List<SaveData> saves = new List<SaveData>();
		SaveLoad.LoadThreadWorker @object = new SaveLoad.LoadThreadWorker(saves, SaveLoad.GetStoragePath());
		ThreadStart threadStart = new ThreadStart(@object.Load);
		threadStart = (ThreadStart)Delegate.Combine(threadStart, new ThreadStart(delegate()
		{
			saves = (from x in saves
			where x != null && !x.corrupted
			select x).ToList<SaveData>();
		}));
		SaveLoad.thread = new Thread(threadStart);
		SaveLoad.thread.Start();
	}

	// Token: 0x06001D60 RID: 7520 RVA: 0x00080D4C File Offset: 0x0007F14C
	public static List<string> getSaveFiles()
	{
		FileInfo[] files = new DirectoryInfo(SaveLoad.GetStoragePath()).GetFiles();
		List<string> list = new List<string>();
		foreach (FileInfo fileInfo in files)
		{
			if (fileInfo.Name.StartsWith("SaveData") && fileInfo.Name.EndsWith("ds"))
			{
				list.Add(fileInfo.Name);
			}
		}
		return list;
	}

	// Token: 0x06001D61 RID: 7521 RVA: 0x00080DC8 File Offset: 0x0007F1C8
	public static bool hasSaveFiles()
	{
		FileInfo[] files = new DirectoryInfo(SaveLoad.GetStoragePath()).GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			if (fileInfo.Name.StartsWith("SaveData") && fileInfo.Name.EndsWith("ds"))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x00080E2C File Offset: 0x0007F22C
	public static string createNewSaveFile()
	{
		int num = 0;
		while (File.Exists(SaveLoad.filePath(SaveLoad.getFileName(num))))
		{
			num++;
		}
		return SaveLoad.getFileName(num);
	}

	// Token: 0x06001D63 RID: 7523 RVA: 0x00080E60 File Offset: 0x0007F260
	public static void Save(string fileName, SaveData data)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			return;
		}
		if (SaveLoad.thread != null && SaveLoad.thread.IsAlive)
		{
			SaveLoad.scheduleSave = true;
			return;
		}
		SaveLoad.canRepeatSave = false;
		SaveLoad.scheduleSave = false;
		SaveLoad.SaveLoadThreadWorker @object = new SaveLoad.SaveLoadThreadWorker(SaveLoad.filePath(fileName), data, SaveLoad.GetEncyptionKey());
		ThreadStart threadStart = new ThreadStart(@object.Save);
		threadStart = (ThreadStart)Delegate.Combine(threadStart, new ThreadStart(delegate()
		{
			SaveLoad.canRepeatSave = true;
		}));
		SaveLoad.thread = new Thread(threadStart);
		SaveLoad.thread.Start();
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x00080F04 File Offset: 0x0007F304
	public static SaveData Load(string fileName)
	{
		if (!File.Exists(SaveLoad.filePath(fileName)))
		{
			SaveData saveData = new SaveData();
			saveData.Populate();
			return saveData;
		}
		return SaveLoad.LoadData(fileName);
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x00080F38 File Offset: 0x0007F338
	private static SaveData LoadData(string fileName)
	{
		SaveData result = new SaveData();
		DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
		try
		{
			using (FileStream fileStream = new FileStream(SaveLoad.filePath(fileName), FileMode.Open, FileAccess.Read))
			{
				using (CryptoStream cryptoStream = new CryptoStream(fileStream, descryptoServiceProvider.CreateDecryptor(SaveLoad.GetEncyptionKey(), SaveLoad.GetEncyptionKey()), CryptoStreamMode.Read))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter
					{
						Binder = new VersionDeserializationBinder()
					};
					result = (SaveData)binaryFormatter.Deserialize(cryptoStream);
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		return result;
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x00080FFC File Offset: 0x0007F3FC
	private static byte[] GetEncyptionKey()
	{
		return new byte[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8
		};
	}

	// Token: 0x04001C05 RID: 7173
	public const string SOUND_VOLUME = "SoundVolume";

	// Token: 0x04001C06 RID: 7174
	public const string MUSIC_VOLUME = "MusicVolume";

	// Token: 0x04001C07 RID: 7175
	public const string VOICE_VOLUME = "VoiceVolume";

	// Token: 0x04001C08 RID: 7176
	public const string MASTER_VOLUME = "MasterVolume";

	// Token: 0x04001C09 RID: 7177
	public const string SUBTITLES_SIZE = "SubtitlesSize";

	// Token: 0x04001C0A RID: 7178
	public const string SUBTITLES = "Subtitles";

	// Token: 0x04001C0B RID: 7179
	public const string LANGUAGE = "Language";

	// Token: 0x04001C0C RID: 7180
	public const string LOCK_MOUSE = "LockMouse";

	// Token: 0x04001C0D RID: 7181
	public const string COLLECT_DATA = "CollectData";

	// Token: 0x04001C0E RID: 7182
	public const string MEASURE_UNITS = "MeasureUnits";

	// Token: 0x04001C0F RID: 7183
	public const string EXIT_ON_PACK12_ACHIEVMENT = "Data";

	// Token: 0x04001C10 RID: 7184
	private const string defaultFileName = "SaveData";

	// Token: 0x04001C11 RID: 7185
	private const string defaultFileExtension = "ds";

	// Token: 0x04001C12 RID: 7186
	private static Thread thread;

	// Token: 0x04001C13 RID: 7187
	public static bool canRepeatSave;

	// Token: 0x04001C14 RID: 7188
	public static bool scheduleSave;

	// Token: 0x02000475 RID: 1141
	private class SaveLoadThreadWorker
	{
		// Token: 0x06001D69 RID: 7529 RVA: 0x00081019 File Offset: 0x0007F419
		public SaveLoadThreadWorker(string filename, SaveData data, byte[] key)
		{
			this.filename = filename;
			this.data = data;
			this.key = key;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00081038 File Offset: 0x0007F438
		public void Save()
		{
			object obj = SaveLoad.SaveLoadThreadWorker.@lock;
			lock (obj)
			{
				SaveLoad.Saving = true;
				try
				{
					DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
					string text = this.filename + 't';
					string destinationBackupFileName = this.filename + 'b';
					using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
					{
						using (CryptoStream cryptoStream = new CryptoStream(fileStream, descryptoServiceProvider.CreateEncryptor(this.key, this.key), CryptoStreamMode.Write))
						{
							BinaryFormatter binaryFormatter = new BinaryFormatter
							{
								Binder = new VersionDeserializationBinder()
							};
							binaryFormatter.Serialize(cryptoStream, this.data);
						}
					}
					if (File.Exists(this.filename))
					{
						File.Replace(text, this.filename, destinationBackupFileName);
					}
					else
					{
						File.Move(text, this.filename);
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
				SaveLoad.Saving = false;
			}
		}

		// Token: 0x04001C18 RID: 7192
		public string filename;

		// Token: 0x04001C19 RID: 7193
		public SaveData data;

		// Token: 0x04001C1A RID: 7194
		public byte[] key;

		// Token: 0x04001C1B RID: 7195
		private static object @lock = new object();
	}

	// Token: 0x02000476 RID: 1142
	private class LoadThreadWorker
	{
		// Token: 0x06001D6C RID: 7532 RVA: 0x0008118C File Offset: 0x0007F58C
		public LoadThreadWorker(List<SaveData> saves, string path)
		{
			this.saves = saves;
			this.path = path;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000811A4 File Offset: 0x0007F5A4
		public void Load()
		{
			object obj = SaveLoad.LoadThreadWorker.@lock;
			lock (obj)
			{
				SaveLoad.Loading = true;
				try
				{
					FileInfo[] files = new DirectoryInfo(this.path).GetFiles();
					foreach (FileInfo fileInfo in files)
					{
						if (fileInfo.Name.StartsWith("SaveData") && fileInfo.Name.EndsWith("ds"))
						{
							this.saves.Add(this.Load(fileInfo.Name, true));
						}
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
				SaveLoad.Loading = false;
			}
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00081278 File Offset: 0x0007F678
		private SaveData Load(string filename, bool useBackup = true)
		{
			SaveData saveData = new SaveData();
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			try
			{
				using (FileStream fileStream = new FileStream(SaveLoad.GetFullPath(this.path, filename), FileMode.Open, FileAccess.Read))
				{
					using (CryptoStream cryptoStream = new CryptoStream(fileStream, descryptoServiceProvider.CreateDecryptor(SaveLoad.GetEncyptionKey(), SaveLoad.GetEncyptionKey()), CryptoStreamMode.Read))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter
						{
							Binder = new VersionDeserializationBinder()
						};
						saveData = (SaveData)binaryFormatter.Deserialize(cryptoStream);
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				if (useBackup)
				{
					string text = filename + "b";
					if (File.Exists(SaveLoad.GetFullPath(this.path, text)))
					{
						saveData = this.Load(text, false);
						if (saveData != null && !saveData.corrupted)
						{
							File.Copy(SaveLoad.GetFullPath(this.path, text), SaveLoad.GetFullPath(this.path, filename), true);
						}
					}
					else
					{
						saveData.corrupted = true;
					}
				}
				else
				{
					saveData.corrupted = true;
				}
				if (saveData == null || saveData.corrupted)
				{
					File.Move(SaveLoad.GetFullPath(this.path, filename), SaveLoad.GetFullPath(this.path, filename + "corrupt"));
				}
			}
			return saveData;
		}

		// Token: 0x04001C1C RID: 7196
		private static object @lock = new object();

		// Token: 0x04001C1D RID: 7197
		private List<SaveData> saves;

		// Token: 0x04001C1E RID: 7198
		private string path;
	}
}
