using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ExcelData;
using UnityEngine;

// Token: 0x02000396 RID: 918
public class LocalizationLoader : MonoBehaviour
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060016FC RID: 5884 RVA: 0x0004B370 File Offset: 0x00049770
	public static LocalizationLoader self
	{
		get
		{
			if (LocalizationLoader._self == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("Localization");
				if (gameObject == null)
				{
					LocalizationLoader._self = null;
				}
				else
				{
					LocalizationLoader._self = gameObject.GetComponent<LocalizationLoader>();
				}
			}
			return LocalizationLoader._self;
		}
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x0004B3BF File Offset: 0x000497BF
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F11))
		{
			this.languageResetEnabled = true;
		}
		if (this.languageResetEnabled && Input.GetKeyDown(KeyCode.F2))
		{
			this.ResetLanguage();
		}
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x0004B3F8 File Offset: 0x000497F8
	public void ResetLanguage()
	{
		this.succesfullyLoaded = true;
		WordTranslationContainer.ResetLanguage();
		LevelVoice.ResetLanguage();
		AwardData.ResetLanguage();
		if (!this.succesfullyLoaded && !this.forceLoad)
		{
			Debug.LogError("Can't load " + Global.self.currLanguage + "; Switching back to default: " + Global.self.defaultLanguage);
			this.forceLoad = true;
			Global.self.currLanguage = Global.self.defaultLanguage;
			return;
		}
		this.forceLoad = false;
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x0004B47C File Offset: 0x0004987C
	public string GetLanguageList()
	{
		string localizationFolderPath = this.GetLocalizationFolderPath();
		string[] directories = Directory.GetDirectories(localizationFolderPath);
		string text = string.Empty;
		foreach (string path in directories)
		{
			if (Directory.GetFiles(path).Length >= 3)
			{
				if (text == string.Empty)
				{
					text += Path.GetFileName(path);
				}
				else
				{
					text = text + "," + Path.GetFileName(path);
				}
			}
		}
		return text;
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x0004B504 File Offset: 0x00049904
	private string GetPath(string fileName, bool printError = true)
	{
		string text = this.GetLocalizationFolderPath();
		text = Path.Combine(text, Global.self.currLanguage);
		text = Path.Combine(text, fileName);
		if (!File.Exists(text))
		{
			if (printError)
			{
				Debug.LogError("Can't find Localization folder/file at: " + text);
			}
			return null;
		}
		return text;
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x0004B555 File Offset: 0x00049955
	private string GetLocalizationFolderPath()
	{
		return Application.dataPath + "/../Localization";
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x0004B568 File Offset: 0x00049968
	private List<T> ReadFiles<T>(string name)
	{
		List<T> list = new List<T>();
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		int num = 0;
		string path;
		do
		{
			string fileName = string.Format(name, (num != 0) ? (num - 1).ToString() : string.Empty);
			path = this.GetPath(fileName, false);
			if (!string.IsNullOrEmpty(path))
			{
				try
				{
					using (FileStream fileStream = new FileStream(path, FileMode.Open))
					{
						list.Add((T)((object)xmlSerializer.Deserialize(fileStream)));
					}
				}
				catch
				{
					this.succesfullyLoaded = false;
					return null;
				}
			}
			num++;
		}
		while (!string.IsNullOrEmpty(path) || num <= 2);
		return list;
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x0004B648 File Offset: 0x00049A48
	public WordTranslationContainer LoadTextData()
	{
		List<LocalizationLoader.TextContainer> list = this.ReadFiles<LocalizationLoader.TextContainer>("Texts{0}.xml");
		if (list == null)
		{
			return null;
		}
		WordTranslationContainer wordTranslationContainer = new WordTranslationContainer();
		foreach (LocalizationLoader.TextContainer textContainer in list)
		{
			foreach (LocalizationLoader.TextEntry textEntry in textContainer.EntryList)
			{
				WordTranslation line = new WordTranslation
				{
					theme = textEntry.Theme,
					id = textEntry.ID
				};
				line.langs.Add(new WordTranslationEntry
				{
					lang = Global.self.currLanguage,
					text = textEntry.Word
				});
				if (!wordTranslationContainer.translations.Exists((WordTranslation x) => x.theme == line.theme && x.id == line.id))
				{
					wordTranslationContainer.translations.Add(line);
				}
			}
		}
		UIControl.self.useBackupFont = list[0].UsePCFont;
		return wordTranslationContainer;
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x0004B7AC File Offset: 0x00049BAC
	public List<AwardData> LoadAwardData()
	{
		List<LocalizationLoader.AwardContainer> list = this.ReadFiles<LocalizationLoader.AwardContainer>("Awards{0}.xml");
		if (list == null)
		{
			return null;
		}
		List<LocalizationLoader.AwardStats> list2 = null;
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(LocalizationLoader.AwardContainer));
		TextAsset textAsset;
		try
		{
			textAsset = (TextAsset)Resources.Load("AwardStats");
			xmlSerializer = new XmlSerializer(typeof(List<LocalizationLoader.AwardStats>));
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(textAsset.text)))
			{
				list2 = (List<LocalizationLoader.AwardStats>)xmlSerializer.Deserialize(xmlReader);
			}
		}
		catch
		{
			this.succesfullyLoaded = false;
			return null;
		}
		List<AwardData> list3 = new List<AwardData>();
		try
		{
			using (List<LocalizationLoader.AwardStats>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LocalizationLoader.AwardStats entry = enumerator.Current;
					int index = 0;
					LocalizationLoader.AwardEntry awardEntry;
					do
					{
						awardEntry = list[index].EntryList.FirstOrDefault((LocalizationLoader.AwardEntry x) => x.index == entry.index);
					}
					while (awardEntry == null && index++ < list.Count);
					AwardData item = new AwardData
					{
						index = entry.index,
						good = entry.good,
						reqPercent = entry.reqPercent,
						reqCount = entry.reqCount,
						cup100 = entry.cup100,
						achievement = entry.achievement,
						language = Global.self.currLanguage,
						titleAcquired = awardEntry.titleAcquired,
						titleNotAcquired = awardEntry.titleNotAcquired,
						descriptionAcquired = awardEntry.descriptionAcquired,
						descriptionNotAcquired = awardEntry.descriptionNotAcquired,
						shortDescriptionAcquired = awardEntry.shortDescriptionAcquired
					};
					list3.Add(item);
				}
			}
		}
		catch
		{
			this.succesfullyLoaded = false;
			return null;
		}
		Resources.UnloadAsset(textAsset);
		return list3;
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x0004BA30 File Offset: 0x00049E30
	public List<LevelVoice> LoadVoiceData()
	{
		List<LocalizationLoader.VoiceContainer> list = this.ReadFiles<LocalizationLoader.VoiceContainer>("Voices{0}.xml");
		if (list == null)
		{
			return null;
		}
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(LocalizationLoader.VoiceContainer));
		TextAsset textAsset;
		List<LocalizationLoader.VoiceStats> list2;
		try
		{
			xmlSerializer = new XmlSerializer(typeof(List<LocalizationLoader.VoiceStats>));
			textAsset = (TextAsset)Resources.Load("VoiceStats");
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(textAsset.text)))
			{
				list2 = (List<LocalizationLoader.VoiceStats>)xmlSerializer.Deserialize(xmlReader);
			}
		}
		catch
		{
			this.succesfullyLoaded = false;
			return null;
		}
		List<LevelVoice> list3 = new List<LevelVoice>();
		try
		{
			using (List<LocalizationLoader.VoiceStats>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LocalizationLoader.VoiceStats stat = enumerator.Current;
					int index = 0;
					LocalizationLoader.VoiceEntry voiceEntry;
					do
					{
						voiceEntry = list[index].EntryList.FirstOrDefault((LocalizationLoader.VoiceEntry x) => x.GUID == stat.guid);
					}
					while (voiceEntry == null && index++ < list.Count);
					int num = 0;
					int num2 = 0;
					bool flag = true;
					bool flag2 = true;
					bool flag3 = true;
					while (flag || flag2)
					{
						flag = true;
						flag2 = true;
						LevelVoice levelVoice = new LevelVoice
						{
							bank = stat.bank,
							id = stat.id,
							voiceGuid = stat.voiceGuid,
							fmodName = stat.fmodName,
							type = (LevelVoice.Type)((!flag3) ? LocalizationLoader.Type.NotSet : stat.type),
							monster = (LevelVoice.Bool)((!flag3) ? LocalizationLoader.Bool.NotSet : stat.monster),
							subs = null,
							texts = null,
							rnd = LevelVoice.Bool.NotSet
						};
						flag3 = false;
						if (num >= voiceEntry.Subtitles.Count)
						{
							flag = false;
						}
						else
						{
							if (levelVoice.subs == null)
							{
								levelVoice.subs = new List<LevelVoiceSubtitle>();
							}
							LocalizationLoader.VoiceSubtitles voiceSubtitles = voiceEntry.Subtitles[num];
							levelVoice.subs.Add(new LevelVoiceSubtitle
							{
								index = voiceSubtitles.Index,
								text = voiceSubtitles.Text,
								lang = Global.self.currLanguage
							});
						}
						if (num2 >= voiceEntry.EndingList.Count)
						{
							flag2 = false;
						}
						else
						{
							if (levelVoice.texts == null)
							{
								levelVoice.texts = new List<LevelVoiceText>();
							}
							LocalizationLoader.VoiceEnding voiceEnding = voiceEntry.EndingList[num2];
							levelVoice.texts.Add(new LevelVoiceText
							{
								lang = Global.self.currLanguage,
								text = voiceEnding.Text
							});
							levelVoice.rnd = (LevelVoice.Bool)voiceEnding.CanBeRndEnding;
							levelVoice.monster = (LevelVoice.Bool)voiceEnding.MonsterEnding;
							levelVoice.endingId = voiceEnding.ID;
						}
						if (flag || flag2)
						{
							list3.Add(levelVoice);
						}
						num++;
						num2++;
					}
				}
			}
		}
		catch
		{
			this.succesfullyLoaded = false;
			return null;
		}
		UIControl.self.subtitlesExtraWaitTime = list[0].TimeToShowSubtitles;
		Resources.UnloadAsset(textAsset);
		return list3;
	}

	// Token: 0x040014A2 RID: 5282
	private static LocalizationLoader _self;

	// Token: 0x040014A3 RID: 5283
	private bool succesfullyLoaded = true;

	// Token: 0x040014A4 RID: 5284
	private bool forceLoad;

	// Token: 0x040014A5 RID: 5285
	private bool languageResetEnabled;

	// Token: 0x02000397 RID: 919
	public class TextContainer
	{
		// Token: 0x040014A6 RID: 5286
		[XmlAttribute]
		public bool UsePCFont;

		// Token: 0x040014A7 RID: 5287
		public string Note;

		// Token: 0x040014A8 RID: 5288
		public List<LocalizationLoader.TextEntry> EntryList = new List<LocalizationLoader.TextEntry>();
	}

	// Token: 0x02000398 RID: 920
	public class TextEntry
	{
		// Token: 0x040014A9 RID: 5289
		[XmlAttribute]
		public string Theme;

		// Token: 0x040014AA RID: 5290
		[XmlAttribute]
		public string ID;

		// Token: 0x040014AB RID: 5291
		public string Comment;

		// Token: 0x040014AC RID: 5292
		public string Word;
	}

	// Token: 0x02000399 RID: 921
	public class AwardStats
	{
		// Token: 0x040014AD RID: 5293
		[XmlAttribute]
		public int index;

		// Token: 0x040014AE RID: 5294
		public bool good;

		// Token: 0x040014AF RID: 5295
		public bool reqPercent;

		// Token: 0x040014B0 RID: 5296
		public bool reqCount;

		// Token: 0x040014B1 RID: 5297
		public bool cup100;

		// Token: 0x040014B2 RID: 5298
		public bool achievement;
	}

	// Token: 0x0200039A RID: 922
	public class AwardContainer
	{
		// Token: 0x040014B3 RID: 5299
		public string Note;

		// Token: 0x040014B4 RID: 5300
		public List<LocalizationLoader.AwardEntry> EntryList = new List<LocalizationLoader.AwardEntry>();
	}

	// Token: 0x0200039B RID: 923
	public class AwardEntry
	{
		// Token: 0x040014B5 RID: 5301
		[XmlAttribute]
		public int index;

		// Token: 0x040014B6 RID: 5302
		public string comment;

		// Token: 0x040014B7 RID: 5303
		public string titleAcquired;

		// Token: 0x040014B8 RID: 5304
		public string titleNotAcquired;

		// Token: 0x040014B9 RID: 5305
		public string descriptionAcquired;

		// Token: 0x040014BA RID: 5306
		public string descriptionNotAcquired;

		// Token: 0x040014BB RID: 5307
		public string shortDescriptionAcquired;
	}

	// Token: 0x0200039C RID: 924
	public class VoiceStats
	{
		// Token: 0x040014BC RID: 5308
		[XmlAttribute]
		public string guid;

		// Token: 0x040014BD RID: 5309
		public string bank;

		// Token: 0x040014BE RID: 5310
		public string id;

		// Token: 0x040014BF RID: 5311
		public string voiceGuid;

		// Token: 0x040014C0 RID: 5312
		public string fmodName;

		// Token: 0x040014C1 RID: 5313
		public LocalizationLoader.Type type;

		// Token: 0x040014C2 RID: 5314
		public LocalizationLoader.Bool monster;
	}

	// Token: 0x0200039D RID: 925
	public enum Type
	{
		// Token: 0x040014C4 RID: 5316
		NotSet,
		// Token: 0x040014C5 RID: 5317
		End,
		// Token: 0x040014C6 RID: 5318
		Start
	}

	// Token: 0x0200039E RID: 926
	public enum Bool
	{
		// Token: 0x040014C8 RID: 5320
		True,
		// Token: 0x040014C9 RID: 5321
		False,
		// Token: 0x040014CA RID: 5322
		NotSet
	}

	// Token: 0x0200039F RID: 927
	public class VoiceContainer
	{
		// Token: 0x040014CB RID: 5323
		[XmlAttribute]
		public float TimeToShowSubtitles = 1f;

		// Token: 0x040014CC RID: 5324
		public string Note;

		// Token: 0x040014CD RID: 5325
		public List<LocalizationLoader.VoiceEntry> EntryList = new List<LocalizationLoader.VoiceEntry>();
	}

	// Token: 0x020003A0 RID: 928
	public class VoiceEntry
	{
		// Token: 0x040014CE RID: 5326
		[XmlAttribute]
		public string GUID;

		// Token: 0x040014CF RID: 5327
		public List<LocalizationLoader.VoiceSubtitles> Subtitles = new List<LocalizationLoader.VoiceSubtitles>();

		// Token: 0x040014D0 RID: 5328
		[XmlAttribute]
		public LocalizationLoader.AllowedType AllowedEndingType;

		// Token: 0x040014D1 RID: 5329
		public List<LocalizationLoader.VoiceEnding> EndingList = new List<LocalizationLoader.VoiceEnding>();
	}

	// Token: 0x020003A1 RID: 929
	public class VoiceSubtitles
	{
		// Token: 0x040014D2 RID: 5330
		[XmlAttribute]
		public int Index;

		// Token: 0x040014D3 RID: 5331
		public string Comment;

		// Token: 0x040014D4 RID: 5332
		public string Text;
	}

	// Token: 0x020003A2 RID: 930
	public class VoiceEnding
	{
		// Token: 0x040014D5 RID: 5333
		[XmlAttribute]
		public string ID;

		// Token: 0x040014D6 RID: 5334
		[XmlAttribute]
		public LocalizationLoader.Bool CanBeRndEnding;

		// Token: 0x040014D7 RID: 5335
		[XmlAttribute]
		public LocalizationLoader.Bool MonsterEnding;

		// Token: 0x040014D8 RID: 5336
		public string Comment;

		// Token: 0x040014D9 RID: 5337
		public string Text;
	}

	// Token: 0x020003A3 RID: 931
	public enum AllowedType
	{
		// Token: 0x040014DB RID: 5339
		None,
		// Token: 0x040014DC RID: 5340
		Mixed,
		// Token: 0x040014DD RID: 5341
		MonsterOnly,
		// Token: 0x040014DE RID: 5342
		GoodOnly
	}
}
