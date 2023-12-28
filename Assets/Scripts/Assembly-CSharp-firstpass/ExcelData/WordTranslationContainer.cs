using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExcelData
{
	// Token: 0x0200037B RID: 891
	public class WordTranslationContainer
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00043E94 File Offset: 0x00042294
		public static WordTranslationContainer container
		{
			get
			{
				if (WordTranslationContainer._container == null)
				{
					WordTranslationContainer._container = WordTranslationContainer.Load();
				}
				return WordTranslationContainer._container;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00043EB0 File Offset: 0x000422B0
		public static char errorChar
		{
			get
			{
				if (Global.self.currLanguage == "RU")
				{
					return "аеорсху"[UnityEngine.Random.Range(0, "аеорсху".Length)];
				}
				return "aeopcxy"[UnityEngine.Random.Range(0, "aeopcxy".Length)];
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00043F0C File Offset: 0x0004230C
		public static Dictionary<char, string> errorSwapList
		{
			get
			{
				if (WordTranslationContainer._errorSwapList == null)
				{
					WordTranslationContainer._errorSwapList = new Dictionary<char, string>();
					WordTranslationContainer._errorSwapList.Add('a', "└╚╣");
					WordTranslationContainer._errorSwapList.Add('e', "┴╔║");
					WordTranslationContainer._errorSwapList.Add('o', "┬╩╗");
					WordTranslationContainer._errorSwapList.Add('p', "├╦");
					WordTranslationContainer._errorSwapList.Add('c', "─╠");
					WordTranslationContainer._errorSwapList.Add('x', "┼═");
					WordTranslationContainer._errorSwapList.Add('y', "┐╬");
					WordTranslationContainer._errorSwapList.Add('а', "└╚╣");
					WordTranslationContainer._errorSwapList.Add('е', "┴╔║");
					WordTranslationContainer._errorSwapList.Add('о', "┬╩╗");
					WordTranslationContainer._errorSwapList.Add('р', "├╦");
					WordTranslationContainer._errorSwapList.Add('с', "─╠");
					WordTranslationContainer._errorSwapList.Add('х', "┼═");
					WordTranslationContainer._errorSwapList.Add('у', "┐╬");
				}
				return WordTranslationContainer._errorSwapList;
			}
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00044038 File Offset: 0x00042438
		public static string Get(WordTranslationContainer.Theme theme, string id, string lang)
		{
			WordTranslation wordTranslation = (from x in WordTranslationContainer.container.translations
			where x.theme == theme.ToString() && x.id == id
			select x).FirstOrDefault<WordTranslation>();
			if (wordTranslation == null)
			{
				return null;
			}
			WordTranslationEntry wordTranslationEntry = (from x in wordTranslation.langs
			where x.lang == lang
			select x).FirstOrDefault<WordTranslationEntry>();
			if (wordTranslationEntry == null)
			{
				return null;
			}
			if (wordTranslationEntry.text == null)
			{
				return string.Empty;
			}
			return wordTranslationEntry.text;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000440C8 File Offset: 0x000424C8
		public static string GetRandomGuid(WordTranslationContainer.Theme theme, string lang)
		{
			List<WordTranslation> list = (from x in WordTranslationContainer.container.translations
			where x.theme == theme.ToString()
			select x).ToList<WordTranslation>();
			if (list.Count == 0)
			{
				return null;
			}
			return list.GetRandom<WordTranslation>().id;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0004411B File Offset: 0x0004251B
		public static WordTranslationContainer Load()
		{
			return LocalizationLoader.self.LoadTextData();
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00044127 File Offset: 0x00042527
		public static void ResetLanguage()
		{
			WordTranslationContainer._container = WordTranslationContainer.Load();
		}

		// Token: 0x040013A1 RID: 5025
		public List<WordTranslation> translations = new List<WordTranslation>();

		// Token: 0x040013A2 RID: 5026
		private static WordTranslationContainer _container;

		// Token: 0x040013A3 RID: 5027
		private const string errorCharListEN = "aeopcxy";

		// Token: 0x040013A4 RID: 5028
		private const string errorCharListRU = "аеорсху";

		// Token: 0x040013A5 RID: 5029
		private static Dictionary<char, string> _errorSwapList;

		// Token: 0x0200037C RID: 892
		public enum Theme
		{
			// Token: 0x040013A7 RID: 5031
			MENU,
			// Token: 0x040013A8 RID: 5032
			AWARD,
			// Token: 0x040013A9 RID: 5033
			PACK_MENU,
			// Token: 0x040013AA RID: 5034
			SPOILER_CUP,
			// Token: 0x040013AB RID: 5035
			EXAM_PACK,
			// Token: 0x040013AC RID: 5036
			PUZZLE,
			// Token: 0x040013AD RID: 5037
			RAPID_FIRE,
			// Token: 0x040013AE RID: 5038
			CONSOLE,
			// Token: 0x040013AF RID: 5039
			CREDITS
		}
	}
}
