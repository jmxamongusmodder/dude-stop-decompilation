using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class LevelVoice
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060015FA RID: 5626 RVA: 0x000441E4 File Offset: 0x000425E4
	private static List<LevelVoice> data
	{
		get
		{
			if (LevelVoice._data == null)
			{
				LevelVoice._data = LevelVoice.Load();
			}
			return LevelVoice._data;
		}
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x000441FF File Offset: 0x000425FF
	private static List<LevelVoice> Load()
	{
		return LocalizationLoader.self.LoadVoiceData();
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x0004420B File Offset: 0x0004260B
	public static void ResetLanguage()
	{
		LevelVoice._data = LevelVoice.Load();
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x00044218 File Offset: 0x00042618
	public static StandaloneLevelVoiceGuid getVoice(StandaloneLevelVoice entry)
	{
		LevelVoice random = (from x in LevelVoice.data
		where x.bank == entry.bankName && x.id == entry.levelVoiceId
		select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		if (random == null)
		{
			return null;
		}
		return new StandaloneLevelVoiceGuid(entry, new Guid(random.voiceGuid), random.fmodName);
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x00044278 File Offset: 0x00042678
	public static StandaloneLevelVoiceGuid getVoice(StandaloneLevelVoice entry, string exclude)
	{
		LevelVoice random = (from x in LevelVoice.data
		where x.bank == entry.bankName && x.id == entry.levelVoiceId && x.fmodName != exclude
		select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		if (random == null)
		{
			return null;
		}
		return new StandaloneLevelVoiceGuid(entry, new Guid(random.voiceGuid), random.fmodName);
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x000442E0 File Offset: 0x000426E0
	public static StandaloneLevelVoiceGuid getVoice(StandaloneLevelVoice entry, LevelVoice.Type type, bool? monster = null)
	{
		LevelVoice random = (from x in LevelVoice.data
		where x.bank == entry.bankName && x.id == entry.levelVoiceId && x.monster == LevelVoice.ConvertBool(monster) && x.type == type
		select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		if (random == null)
		{
			return null;
		}
		return new StandaloneLevelVoiceGuid(entry, new Guid(random.voiceGuid), random.fmodName);
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x00044350 File Offset: 0x00042750
	public static List<LevelVoice> getVoiceList(StandaloneLevelVoice entry, LevelVoice.Type type, bool? monster)
	{
		List<LevelVoice> list = (from x in LevelVoice.data
		where x.bank == entry.bankName && x.id == entry.levelVoiceId && x.type == type && (x.monster == LevelVoice.ConvertBool(monster) || (type == LevelVoice.Type.Start && x.monster == LevelVoice.Bool.NotSet))
		select x).ToList<LevelVoice>();
		if (list == null)
		{
			return null;
		}
		return list;
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x000443A0 File Offset: 0x000427A0
	public static StandaloneLevelVoiceEnd getVoice(StandaloneLevelVoice entry, LevelVoice.Type type, bool? monster, string lang)
	{
		LevelVoice lv = (from x in LevelVoice.data
		where x.bank == entry.bankName && x.id == entry.levelVoiceId && x.monster == LevelVoice.ConvertBool(monster) && x.type == type
		select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		if (lv == null)
		{
			return null;
		}
		string endText = null;
		if (type != LevelVoice.Type.Start)
		{
			LevelVoice levelVoice;
			if (string.IsNullOrEmpty(lv.voiceGuid) || string.IsNullOrEmpty(lv.fmodName))
			{
				levelVoice = lv;
			}
			else
			{
				levelVoice = (from x in LevelVoice.data
				where x.voiceGuid == lv.voiceGuid && x.fmodName == lv.fmodName && x.monster == LevelVoice.ConvertBool(monster) && x.type != LevelVoice.Type.Start && (from y in x.texts
				where y.lang == lang
				select y).Count<LevelVoiceText>() > 0
				select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
			}
			if (levelVoice != null)
			{
				LevelVoiceText levelVoiceText = (from x in levelVoice.texts
				where x.lang == lang
				select x).FirstOrDefault<LevelVoiceText>();
				if (levelVoiceText != null)
				{
					endText = levelVoiceText.text;
				}
			}
		}
		return new StandaloneLevelVoiceEnd(entry, (!string.IsNullOrEmpty(lv.voiceGuid)) ? new Guid(lv.voiceGuid) : Guid.Empty, lv.fmodName, endText);
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x000444E0 File Offset: 0x000428E0
	public static string getSubtitles(string fmodName, string lang, int index = 0)
	{
		LevelVoice levelVoice = (from x in LevelVoice.data
		where x.fmodName == fmodName && (from y in x.subs
		where y.index == index
		select y).Count<LevelVoiceSubtitle>() > 0
		select x).FirstOrDefault<LevelVoice>();
		if (levelVoice == null)
		{
			return null;
		}
		return (from x in levelVoice.subs
		where x.lang == lang && x.index == index
		select x).FirstOrDefault<LevelVoiceSubtitle>().text;
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x00044550 File Offset: 0x00042950
	public static string getEndText(StandaloneLevelVoiceGuid entry, string lang, string endingId)
	{
		return LevelVoice.getEndText(entry, null, lang, endingId);
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x00044570 File Offset: 0x00042970
	public static string getEndText(StandaloneLevelVoiceGuid entry, bool? monster, string lang, string endingId)
	{
		LevelVoice random = (from x in LevelVoice.data
		where x.bank == entry.bankName && x.id == entry.levelVoiceId && x.voiceGuid == entry.guid.ToString() && x.fmodName == entry.fmodName && x.monster == LevelVoice.ConvertBool(monster) && x.endingId == endingId
		select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		if (random == null || random.texts.Count == 0)
		{
			return null;
		}
		LevelVoiceText levelVoiceText = (from x in random.texts
		where x.lang == lang
		select x).FirstOrDefault<LevelVoiceText>();
		return (levelVoiceText != null) ? levelVoiceText.text : null;
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x00044608 File Offset: 0x00042A08
	public static string getEndText(StandaloneLevelVoiceGuid entry, string[] exclude, bool monster, string lang)
	{
		bool exl = exclude != null;
		LevelVoice random = (from x in LevelVoice.data
		where x.monster == LevelVoice.ConvertBool(monster) && x.texts != null && x.texts.Count > 0 && x.bank == entry.bankName && x.id == entry.levelVoiceId && x.voiceGuid == entry.guid.ToString() && x.fmodName == entry.fmodName && (!exl || (exl && (from y in x.texts
		where exclude.Contains(y.text)
		select y).Count<LevelVoiceText>() == 0))
		select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		if (random == null)
		{
			if (Global.self.DEBUG && Global.self.DEBUG)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Can't find ending with this stats: \nBank: ",
					entry.bankName,
					"\nID: ",
					entry.levelVoiceId,
					"\nGUID: ",
					entry.guid,
					"\nFmod: ",
					entry.fmodName,
					"\nExclude: ",
					string.Join(",", exclude),
					"\nMonster: ",
					monster,
					"\nLang: ",
					lang
				}));
			}
			return null;
		}
		LevelVoiceText levelVoiceText = (from x in random.texts
		where x.lang == lang
		select x).FirstOrDefault<LevelVoiceText>();
		return (levelVoiceText != null) ? levelVoiceText.text : null;
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x00044774 File Offset: 0x00042B74
	public static string getEndText(StandaloneLevelVoice entry, string[] exclude, bool monster, string lang, bool includeAll = false)
	{
		bool exl = exclude != null;
		List<LevelVoice> list = (from x in LevelVoice.data
		where x.monster == LevelVoice.ConvertBool(monster) && x.texts != null && x.texts.Count > 0 && ((x.bank == entry.bankName && x.id == entry.levelVoiceId) || x.rnd != LevelVoice.Bool.False) && (!exl || (exl && (from y in x.texts
		where exclude.Contains(y.text)
		select y).Count<LevelVoiceText>() == 0))
		select x).ToList<LevelVoice>();
		if (list == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Can't find ending with this stats: \nBank: ",
				entry.bankName,
				"\nID: ",
				entry.levelVoiceId,
				"\nExclude: ",
				exclude,
				"\nMonster: ",
				monster,
				"\nLang: ",
				lang,
				"\nAll: ",
				includeAll
			}));
			return null;
		}
		LevelVoice levelVoice = null;
		if (!includeAll)
		{
			levelVoice = (from x in list
			where x.bank == entry.bankName && x.id == entry.levelVoiceId
			select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		}
		if (levelVoice == null)
		{
			levelVoice = list.GetRandom<LevelVoice>();
		}
		if (levelVoice == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Can't find ending with this stats: \nBank: ",
				entry.bankName,
				"\nID: ",
				entry.levelVoiceId,
				"\nExclude: ",
				exclude,
				"\nMonster: ",
				monster,
				"\nLang: ",
				lang,
				"\nAll: ",
				includeAll
			}));
			return null;
		}
		LevelVoiceText levelVoiceText = (from x in levelVoice.texts
		where x.lang == lang
		select x).FirstOrDefault<LevelVoiceText>();
		return (levelVoiceText != null) ? levelVoiceText.text : null;
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x00044958 File Offset: 0x00042D58
	public static string getEndText(bool monster, string lang)
	{
		LevelVoice random = (from x in LevelVoice.data
		where x.monster == LevelVoice.ConvertBool(monster) && x.rnd != LevelVoice.Bool.False && x.texts != null && x.texts.Count > 0
		select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		if (random == null || random.texts.Count == 0)
		{
			return null;
		}
		LevelVoiceText levelVoiceText = (from x in random.texts
		where x.lang == lang
		select x).FirstOrDefault<LevelVoiceText>();
		return (levelVoiceText != null) ? levelVoiceText.text : null;
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x000449E4 File Offset: 0x00042DE4
	public static string getEndText(StandaloneLevelVoice entry, string lang)
	{
		LevelVoice random = (from x in LevelVoice.data
		where x.bank == entry.bankName && x.id == entry.levelVoiceId && x.texts != null && x.texts.Count > 0
		select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
		if (random == null)
		{
			return null;
		}
		LevelVoiceText levelVoiceText = (from x in random.texts
		where x.lang == lang
		select x).FirstOrDefault<LevelVoiceText>();
		return (levelVoiceText != null) ? levelVoiceText.text : null;
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x00044A60 File Offset: 0x00042E60
	public static bool HasEndText(StandaloneLevelVoiceGuid entry, bool monster, string lang)
	{
		int num = (from x in LevelVoice.data
		where x.bank == entry.bankName && x.id == entry.levelVoiceId && x.voiceGuid == entry.guid.ToString() && x.fmodName == entry.fmodName && x.monster == LevelVoice.ConvertBool(monster) && x.texts != null && x.texts.Count > 0
		select x).Count<LevelVoice>();
		return num > 0;
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x00044AA1 File Offset: 0x00042EA1
	public static LevelVoice.Bool ConvertBool(bool @bool)
	{
		return (!@bool) ? LevelVoice.Bool.False : LevelVoice.Bool.True;
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x00044AB0 File Offset: 0x00042EB0
	public static LevelVoice.Bool ConvertBool(bool? @bool)
	{
		return (@bool != null) ? LevelVoice.ConvertBool(@bool.Value) : LevelVoice.Bool.NotSet;
	}

	// Token: 0x040013B6 RID: 5046
	public string bank;

	// Token: 0x040013B7 RID: 5047
	public string id;

	// Token: 0x040013B8 RID: 5048
	public string voiceGuid;

	// Token: 0x040013B9 RID: 5049
	public string fmodName;

	// Token: 0x040013BA RID: 5050
	public LevelVoice.Type type;

	// Token: 0x040013BB RID: 5051
	public LevelVoice.Bool rnd;

	// Token: 0x040013BC RID: 5052
	public LevelVoice.Bool monster;

	// Token: 0x040013BD RID: 5053
	public string endingId;

	// Token: 0x040013BE RID: 5054
	public List<LevelVoiceSubtitle> subs = new List<LevelVoiceSubtitle>();

	// Token: 0x040013BF RID: 5055
	public List<LevelVoiceText> texts = new List<LevelVoiceText>();

	// Token: 0x040013C0 RID: 5056
	private static List<LevelVoice> _data;

	// Token: 0x02000380 RID: 896
	public enum Type
	{
		// Token: 0x040013C2 RID: 5058
		NotSet,
		// Token: 0x040013C3 RID: 5059
		End,
		// Token: 0x040013C4 RID: 5060
		Start
	}

	// Token: 0x02000381 RID: 897
	public enum Bool
	{
		// Token: 0x040013C6 RID: 5062
		True,
		// Token: 0x040013C7 RID: 5063
		False,
		// Token: 0x040013C8 RID: 5064
		NotSet
	}
}
