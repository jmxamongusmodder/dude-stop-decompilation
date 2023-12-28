using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class AwardController : MonoBehaviour
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0003198D File Offset: 0x0002FD8D
	protected virtual Transform resultScreen
	{
		get
		{
			return this.rewardExistPuzzle;
		}
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x00031998 File Offset: 0x0002FD98
	private void Start()
	{
		this.awardCondList = base.transform.GetComponents<AwardConditionAbstract>();
		AwardAllPuzzlesInPack[] components = AwardController.self.GetComponents<AwardAllPuzzlesInPack>();
		foreach (AwardAllPuzzlesInPack awardAllPuzzlesInPack in components)
		{
			awardAllPuzzlesInPack.setList();
		}
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x000319E4 File Offset: 0x0002FDE4
	public void startPack()
	{
		foreach (AwardConditionAbstract awardConditionAbstract in this.awardCondList)
		{
			awardConditionAbstract.startPack();
		}
		this.previousBestBad = this.getBestProgress(true);
		this.previousBestGood = this.getBestProgress(false);
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x00031A30 File Offset: 0x0002FE30
	public void setJigSawPieces(List<Transform> list)
	{
		if (Global.self.unlockedJigsawPieces >= 20 && !Global.self.DEBUG)
		{
			this.jigsawSpawnChance = 0f;
			return;
		}
		int num = 0;
		foreach (Transform transform in list)
		{
			if (!(transform.GetComponent<PuzzleStats>() == null))
			{
				if (transform.GetComponent<PuzzleStats>().hasJigSawPieces)
				{
					if (SerializablePuzzleStats.Get(transform.name).jigSawPiecesFound > 0)
					{
						this.jigsawCurrentCount++;
					}
					else
					{
						num++;
					}
				}
			}
		}
		if (num <= 0 || this.jigsawCurrentCount >= this.jigsawPerPackCount)
		{
			this.jigsawSpawnChance = 0f;
			return;
		}
		float num2 = (float)(this.jigsawSpreadAccrossRuns - Global.self.CountPackPlayedTimes(0));
		if (num2 == 0f)
		{
			num2 = 1f;
		}
		else if (num2 < 0f)
		{
			num2 = 1f / Mathf.Abs(num2 - 1f);
		}
		this.jigsawSpawnChance = (float)(this.jigsawPerPackCount - this.jigsawCurrentCount) / (float)num / num2;
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x00031B88 File Offset: 0x0002FF88
	public float getJigSawSpawnChance()
	{
		if (this.jigsawCurrentCount >= this.jigsawPerPackCount)
		{
			return 0f;
		}
		return this.jigsawSpawnChance;
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x00031BA8 File Offset: 0x0002FFA8
	public static void removeOldAwardController()
	{
		if (AwardController.self == null)
		{
			return;
		}
		if (AwardController.self.transform.parent == null)
		{
			UnityEngine.Object.Destroy(AwardController.self.gameObject);
		}
		AwardController.self = null;
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x00031BF5 File Offset: 0x0002FFF5
	public static void setAwardController(AwardController controller)
	{
		AwardController.self = controller;
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x00031C00 File Offset: 0x00030000
	public Transform makeAwardLevel()
	{
		if (this.rewardLevelCreated)
		{
			return null;
		}
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
		IAwardProgress component = base.GetComponent<IAwardProgress>();
		if (component != null)
		{
			component.calculate();
		}
		this.rewardLevelCreated = true;
		bool flag = false;
		Global.self.CountPackPlayedTimes(1);
		Global.self.lastPackCompletionState = CompletionState.Mixed;
		Transform transform = null;
		foreach (AwardConditionAbstract awardConditionAbstract in this.awardCondList)
		{
			awardConditionAbstract.setAward();
			flag = awardConditionAbstract.isPackSaved();
			if (transform == null)
			{
				transform = awardConditionAbstract.getNextPuzzle();
			}
		}
		if (!flag)
		{
			Global.self.Save();
		}
		if (transform == null && this.resultScreen != null)
		{
			AnalyticsComponent.PuzzleStarted(this.resultScreen.name);
			return this.resultScreen;
		}
		if (transform != null)
		{
			AnalyticsComponent.CupPuzzleStarted(transform.name);
		}
		return transform;
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x00031CFE File Offset: 0x000300FE
	public void addBadEndingCount()
	{
		this.badEndingCount++;
		SerializablePackSavedStats.Get(Global.self.currentLevelPack).badEndCount++;
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x00031D2A File Offset: 0x0003012A
	public void addGoodEndingCount()
	{
		this.goodEndingCount++;
		SerializablePackSavedStats.Get(Global.self.currentLevelPack).goodEndCount++;
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x00031D56 File Offset: 0x00030156
	public void solveAsBad(Transform puzzle = null)
	{
		this.solvedAsBad++;
		this.solvePuzzleFromTheList(puzzle, true);
		this.solvedOrder.Enqueue(true);
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x00031D7A File Offset: 0x0003017A
	public void solveAsGood(Transform puzzle = null)
	{
		this.solvedAsGood++;
		this.solvePuzzleFromTheList(puzzle, false);
		this.solvedOrder.Enqueue(false);
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x00031DA0 File Offset: 0x000301A0
	public void removeBadSolution()
	{
		this.solvedAsBad--;
		this.badEndingCount--;
		SerializablePackSavedStats.Get(Global.self.currentLevelPack).badEndCount--;
		this.solvedOrder = new Queue<bool>(this.solvedOrder.Take(this.solvedOrder.Count - 1));
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x00031E08 File Offset: 0x00030208
	private void solvePuzzleFromTheList(Transform puzzle, bool solvedAsBad)
	{
		AwardForListOfPuzzles[] components = base.GetComponents<AwardForListOfPuzzles>();
		foreach (AwardForListOfPuzzles awardForListOfPuzzles in components)
		{
			awardForListOfPuzzles.setPuzzle(puzzle, solvedAsBad);
		}
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x00031E40 File Offset: 0x00030240
	public int getProgress(bool good)
	{
		float num;
		if (good)
		{
			if (this.goodEndingCount == 0)
			{
				return 0;
			}
			num = (float)this.solvedAsGood / (float)this.goodEndingCount;
		}
		else
		{
			if (this.badEndingCount == 0)
			{
				return 0;
			}
			num = (float)this.solvedAsBad / (float)this.badEndingCount;
		}
		return Mathf.RoundToInt(num * 100f);
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x00031EA4 File Offset: 0x000302A4
	public int getCurrentProgress(bool monster)
	{
		if (monster)
		{
			return Mathf.RoundToInt((float)this.solvedAsBad / (float)(this.solvedAsBad + this.solvedAsGood) * 100f);
		}
		return Mathf.RoundToInt((float)this.solvedAsGood / (float)(this.solvedAsBad + this.solvedAsGood) * 100f);
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x00031EFA File Offset: 0x000302FA
	public bool isAllPuzzlesSolvedBad()
	{
		return (from x in this.solvedOrder
		where !x
		select x).Count<bool>() == 0;
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x00031F2C File Offset: 0x0003032C
	public Queue<bool> getSolvedOrder()
	{
		return this.solvedOrder;
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x00031F34 File Offset: 0x00030334
	public void setBestProgress(bool good)
	{
		if (good)
		{
			SerializablePackSavedStats.Get(Global.self.currentLevelPack).bestGoodSolvedPuzzleCount = this.solvedAsGood;
		}
		else
		{
			SerializablePackSavedStats.Get(Global.self.currentLevelPack).bestBadSolvedPuzzleCount = this.solvedAsBad;
		}
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x00031F80 File Offset: 0x00030380
	public int getBestProgress(bool monster)
	{
		if (monster)
		{
			return SerializablePackSavedStats.Get(Global.self.currentLevelPack).bestBadSolvedPuzzleCount;
		}
		return SerializablePackSavedStats.Get(Global.self.currentLevelPack).bestGoodSolvedPuzzleCount;
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x00031FB4 File Offset: 0x000303B4
	public string getBestProgressProc(bool monster)
	{
		float num = (float)this.getBestProgress(monster);
		float num2;
		if (monster)
		{
			num2 = (float)SerializablePackSavedStats.Get(Global.self.currentLevelPack).badEndCount;
		}
		else
		{
			num2 = (float)SerializablePackSavedStats.Get(Global.self.currentLevelPack).goodEndCount;
		}
		if (num2 == 0f)
		{
			return "0";
		}
		return (num / num2 * 100f).ToString("F0");
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x00032030 File Offset: 0x00030430
	public int getNeededProc(bool monster, bool allPuzzles = false)
	{
		AwardBadGoodCondition[] components = base.GetComponents<AwardBadGoodCondition>();
		AwardBadGoodCondition awardBadGoodCondition = (from x in components
		where x.goodReward == !monster && (!allPuzzles || (allPuzzles && x.rewardPuzzle == null))
		select x).FirstOrDefault<AwardBadGoodCondition>();
		if (awardBadGoodCondition == null)
		{
			return 0;
		}
		return awardBadGoodCondition.toGetReward;
	}

	// Token: 0x04001089 RID: 4233
	public static AwardController self;

	// Token: 0x0400108A RID: 4234
	[Tooltip("If player has this reward - next pack is unlocked.")]
	public AwardName toUnlockNextPack;

	// Token: 0x0400108B RID: 4235
	public AwardName toUnlockNextPack2;

	// Token: 0x0400108C RID: 4236
	[Tooltip("Puzzle to show when reward is already exist")]
	public Transform rewardExistPuzzle;

	// Token: 0x0400108D RID: 4237
	private bool rewardLevelCreated;

	// Token: 0x0400108E RID: 4238
	private int badEndingCount;

	// Token: 0x0400108F RID: 4239
	private int solvedAsBad;

	// Token: 0x04001090 RID: 4240
	private int goodEndingCount;

	// Token: 0x04001091 RID: 4241
	private int solvedAsGood;

	// Token: 0x04001092 RID: 4242
	private Queue<bool> solvedOrder = new Queue<bool>();

	// Token: 0x04001093 RID: 4243
	[HideInInspector]
	public int previousBestBad;

	// Token: 0x04001094 RID: 4244
	[HideInInspector]
	public int previousBestGood;

	// Token: 0x04001095 RID: 4245
	private AwardConditionAbstract[] awardCondList;

	// Token: 0x04001096 RID: 4246
	[Header("JigSaw pieces")]
	[Tooltip("How much pieces is going to spawn in this pack")]
	public int jigsawPerPackCount = 4;

	// Token: 0x04001097 RID: 4247
	[Tooltip("In how many runs to show all this pieces?")]
	[Range(1f, 3f)]
	public int jigsawSpreadAccrossRuns = 2;

	// Token: 0x04001098 RID: 4248
	[HideInInspector]
	public int jigsawCurrentCount;

	// Token: 0x04001099 RID: 4249
	private float jigsawSpawnChance;
}
