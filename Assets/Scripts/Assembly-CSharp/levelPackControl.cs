using System;
using System.Collections.Generic;
using System.Linq;
using ExcelData;
using UnityEngine;

// Token: 0x02000557 RID: 1367
public class levelPackControl : MonoBehaviour
{
	// Token: 0x06001F53 RID: 8019 RVA: 0x00095A79 File Offset: 0x00093E79
	private void Awake()
	{
		Audio.self.StartSoloSnapshot(MusicTypes.MenuMusic, true);
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x00095A88 File Offset: 0x00093E88
	private void Start()
	{
		this.packList.gameObject.SetActive(false);
		Global.self.replayingCupPuzzle = false;
		AwardController.removeOldAwardController();
		AwardController.setAwardController(this.awardControllerScript);
		UIControl.self.SetSubtitlesYellow(false);
	}

	// Token: 0x06001F55 RID: 8021 RVA: 0x00095AC4 File Offset: 0x00093EC4
	public void startPack()
	{
		this.keepAwardControl();
		Global.self.lastPackCompletionState = CompletionState.None;
		Global.self.isThirdPackAfterGamesIntro = false;
		Global.self.playIWasRightOnPack07 = false;
		base.GetComponent<AudioBank>().startPack();
		Global.self.previousPuzzleSolvedAsMonster = null;
		Global.self.canExitEndScreen = true;
		List<Transform> list = this.makePuzzleList();
		this.awardControllerScript.setJigSawPieces(list);
		this.awardControllerScript.startPack();
		if (this.completionLine)
		{
			if (this.scrollablePack)
			{
				UIControl.self.startCompletionPack(list.GetRange(0, list.Count - 1));
			}
			else
			{
				UIControl.self.startCompletionPack(list);
			}
		}
		Global.self.makeLevelList(list);
		if (this.scrollablePack)
		{
			Global.self.startScrollablePack();
		}
		AnalyticsComponent.PackStarted(this.packIndex);
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x00095BAC File Offset: 0x00093FAC
	private List<Transform> makePuzzleList()
	{
		List<Transform> list = new List<Transform>();
		PackListItem[] components = this.packList.GetComponents<PackListItem>();
		PackListItem[] array = components;
		int i = 0;
		while (i < array.Length)
		{
			PackListItem packListItem = array[i];
			switch (packListItem.condition)
			{
			case ifCondition.Always:
				goto IL_140;
			case ifCondition.Never:
				break;
			case ifCondition.skipFirst:
				if (Global.self.CountPackPlayedTimes(0) != 0)
				{
					goto IL_140;
				}
				break;
			case ifCondition.onlyOnFirst:
				if (Global.self.CountPackPlayedTimes(0) == 0)
				{
					goto IL_140;
				}
				break;
			case ifCondition.showOnlyTwice:
				if (Global.self.CountPackPlayedTimes(0) <= 1)
				{
					goto IL_140;
				}
				break;
			case ifCondition.IfNoOther:
			case ifCondition.FirstTimeLoading:
			case ifCondition.onlyOnSecond:
			case ifCondition.SecondTimeLoading:
			case ifCondition.ThirdTimeLoading:
				goto IL_131;
			case ifCondition.onlyGameIntro:
				if (Global.self.isGameIntroActive)
				{
					goto IL_140;
				}
				break;
			case ifCondition.ShowAllOnce:
			{
				bool flag = false;
				foreach (Transform transform in packListItem.List)
				{
					if (SerializablePuzzleStats.Get(transform.name).loadedTimes == 0)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					goto IL_140;
				}
				break;
			}
			default:
				goto IL_131;
			}
			IL_322:
			i++;
			continue;
			IL_140:
			if (packListItem.Chance != 1f && UnityEngine.Random.Range(0f, 1f) > packListItem.Chance)
			{
				goto IL_322;
			}
			int num = (packListItem.Count != 0) ? Mathf.Min(packListItem.List.Count<Transform>(), packListItem.Count) : packListItem.List.Count<Transform>();
			packRandomnes randomOrder = packListItem.randomOrder;
			switch (randomOrder)
			{
			case packRandomnes.None:
				list.AddRange(packListItem.List.ToList<Transform>().GetRange(0, num));
				goto IL_322;
			case packRandomnes.RandomOrder:
			{
				List<Transform> list3 = packListItem.List.ToList<Transform>();
				list3.Shuffle<Transform>();
				list.AddRange(list3.GetRange(0, num));
				goto IL_322;
			}
			case packRandomnes.RapidFireRandom:
			{
				List<Transform> list4 = packListItem.List.ToList<Transform>();
				list4.Shuffle<Transform>();
				Dictionary<Transform, int> dictionary = list4.ToDictionary((Transform x) => x, (Transform x) => SerializablePuzzleStats.Get(x.name).playedTimes);
				int num2 = 0;
				bool goBad = Extensions.RandomBool();
				while (num2 < num && dictionary.Count > 0)
				{
					KeyValuePair<Transform, int> keyValuePair = (from x in dictionary
					orderby x.Value
					select x).FirstOrDefault((KeyValuePair<Transform, int> x) => x.Key.GetComponent<PuzzleStats>().goBadAfterTime == goBad);
					if (keyValuePair.Key != null)
					{
						list.Add(keyValuePair.Key);
						dictionary.Remove(keyValuePair.Key);
						num2++;
					}
					goBad = !goBad;
				}
				goto IL_322;
			}
			default:
				Debug.LogWarning("This Random order is not specified");
				goto IL_322;
			}
			IL_131:
			Debug.LogWarning("This IF condition is not specified");
			goto IL_140;
		}
		SerializablePackSavedStats.Get(Global.self.currentLevelPack).badEndCount = 0;
		SerializablePackSavedStats.Get(Global.self.currentLevelPack).goodEndCount = 0;
		list.ForEach(delegate(Transform x)
		{
			this.countGoodBadSolutions(x);
		});
		return list;
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x00095F28 File Offset: 0x00094328
	private void countGoodBadSolutions(Transform item)
	{
		PuzzleStats component = item.GetComponent<PuzzleStats>();
		if (component == null)
		{
			return;
		}
		if (component.HasBadEnd)
		{
			AwardController.self.addBadEndingCount();
		}
		if (component.HasGoodEnd)
		{
			AwardController.self.addGoodEndingCount();
		}
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x00095F73 File Offset: 0x00094373
	private void keepAwardControl()
	{
		this.awardController.SetParent(null);
		this.awardController.SetAsFirstSibling();
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x00095F8C File Offset: 0x0009438C
	public string translateHeader(bool debbug = false)
	{
		string text = LineTranslator.translateText(this.headerGUID, this.headerType, debbug, string.Empty);
		return text.Replace("(#)", this.packIndex.ToString());
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x00095FD0 File Offset: 0x000943D0
	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			return;
		}
		if (!string.IsNullOrEmpty(this.headerGUID))
		{
			this.headerGUID = this.headerGUID.ToUpper();
			this.headerGUID = this.headerGUID.Replace(" ", "_");
		}
		levelPackMenu levelPackMenu = UnityEngine.Object.FindObjectOfType<levelPackMenu>();
		if (levelPackMenu != null)
		{
			levelPackMenu.header.color = this.headerColor;
			levelPackMenu.header.text = this.translateHeader(true);
			MonoBehaviour.print("CHANGED to: " + levelPackMenu.header.text);
		}
		if (this.awardController != null)
		{
			this.awardControllerScript = this.awardController.GetComponent<AwardController>();
		}
	}

	// Token: 0x0400228A RID: 8842
	[Header("Links")]
	public Transform awardController;

	// Token: 0x0400228B RID: 8843
	public AwardController awardControllerScript;

	// Token: 0x0400228C RID: 8844
	public Transform packList;

	// Token: 0x0400228D RID: 8845
	[Header("Header")]
	public int packIndex;

	// Token: 0x0400228E RID: 8846
	public string headerGUID;

	// Token: 0x0400228F RID: 8847
	public string headerText = "No Name Found";

	// Token: 0x04002290 RID: 8848
	public WordTranslationContainer.Theme headerType;

	// Token: 0x04002291 RID: 8849
	[Header("Pack options")]
	public Color headerColor = Color.red;

	// Token: 0x04002292 RID: 8850
	[Tooltip("If levels in this pack can be solved in any order - arrows to go to prev puzzle")]
	public bool scrollablePack;

	// Token: 0x04002293 RID: 8851
	[Tooltip("Show line with that represents how levels are solved (bad/good)")]
	public bool completionLine;
}
