using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000388 RID: 904
public class Global : MonoBehaviour
{
	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06001649 RID: 5705 RVA: 0x000465D4 File Offset: 0x000449D4
	public static Global self
	{
		get
		{
			if (Global._self == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("Global");
				if (gameObject == null)
				{
					Global._self = null;
				}
				else
				{
					Global._self = gameObject.GetComponent<Global>();
				}
			}
			return Global._self;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600164A RID: 5706 RVA: 0x00046623 File Offset: 0x00044A23
	// (set) Token: 0x0600164B RID: 5707 RVA: 0x00046647 File Offset: 0x00044A47
	public string currLanguage
	{
		get
		{
			if (string.IsNullOrEmpty(this._currLanguage))
			{
				this._currLanguage = this.defaultLanguage;
			}
			return this._currLanguage;
		}
		set
		{
			this._currLanguage = value;
			LocalizationLoader.self.ResetLanguage();
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600164C RID: 5708 RVA: 0x0004665A File Offset: 0x00044A5A
	public string languageList
	{
		get
		{
			return LocalizationLoader.self.GetLanguageList();
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600164E RID: 5710 RVA: 0x0004668A File Offset: 0x00044A8A
	// (set) Token: 0x0600164D RID: 5709 RVA: 0x00046666 File Offset: 0x00044A66
	[HideInInspector]
	public bool canBePaused
	{
		get
		{
			return this._canBePaused;
		}
		set
		{
			if (this._canBePaused && !value)
			{
				global::Console.self.TryToHideOptionsConsole();
			}
			this._canBePaused = value;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600164F RID: 5711 RVA: 0x00046692 File Offset: 0x00044A92
	// (set) Token: 0x06001650 RID: 5712 RVA: 0x0004669A File Offset: 0x00044A9A
	[HideInInspector]
	public string sessionID { get; private set; }

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06001651 RID: 5713 RVA: 0x000466A3 File Offset: 0x00044AA3
	// (set) Token: 0x06001652 RID: 5714 RVA: 0x000466AB File Offset: 0x00044AAB
	[HideInInspector]
	public string playerID { get; private set; }

	// Token: 0x06001653 RID: 5715 RVA: 0x000466B4 File Offset: 0x00044AB4
	private void Awake()
	{
		Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreen);
		string str = SaveLoad.getStr("Language", this.defaultLanguage);
		string[] source = this.languageList.Split(new char[]
		{
			','
		});
		if (!source.Contains(str))
		{
			str = this.defaultLanguage;
		}
		this.currLanguage = str;
		SaveLoad.GetTrueFileList();
		this.playerID = SystemInfo.deviceUniqueIdentifier;
		this.sessionID = Guid.NewGuid().ToString();
		AnalyticsComponent.SetAnalytics(SaveLoad.getInt("CollectData", 1) == 1, this.playerID, this.sessionID);
		AnalyticsComponent.GameLoaded();
		this.metricSystem = (SaveLoad.getInt("MeasureUnits", 1) == 1);
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x00046778 File Offset: 0x00044B78
	private void OnApplicationFocus(bool lostFocus)
	{
		if (!lostFocus)
		{
			if (this.lockMouse)
			{
				Cursor.lockState = CursorLockMode.Confined;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
			}
		}
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x0004679C File Offset: 0x00044B9C
	private void OnApplicationQuit()
	{
		AnalyticsComponent.GameClosed();
		if (this.openGreenlightPage)
		{
			Application.OpenURL("http://www.patomkin.com/ds2/greenlight.php");
		}
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x000467B8 File Offset: 0x00044BB8
	private void Start()
	{
		this.fullscreen = Screen.fullScreen;
		this.lockMouse = (SaveLoad.getInt("LockMouse", 1) == 1);
		if (this.lockMouse && this.fullscreen)
		{
			Cursor.lockState = CursorLockMode.Confined;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
		}
		this.puzzleParent = new GameObject("puzzleParent").transform;
		this.firstTimeLoadingGame = !SaveLoad.hasSaveFiles();
		if (this.loadOnStart)
		{
			this.LoadGame("SaveData.ds");
		}
		if (this.transitionFromDown)
		{
			this.makeNewLevel(this.firstPuzzle, Vector2.down, true);
		}
		else
		{
			this.makeNewLevel(this.firstPuzzle, Vector2.right, true);
		}
		this.lastPlayed = this.firstPuzzle;
		base.StartCoroutine(this.CountSeconds());
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x00046891 File Offset: 0x00044C91
	private void Update()
	{
		this.CHEATS();
		this.PollSaveLoad();
		this.UpdateTransition();
		Global.puzzleTimer += Time.deltaTime;
		if (Input.GetMouseButtonDown(0))
		{
			Global.clickCounter++;
		}
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x000468CC File Offset: 0x00044CCC
	private IEnumerator CountSeconds()
	{
		while (base.enabled)
		{
			this.totalPlayedTime += 1U;
			yield return new WaitForSeconds(1f);
		}
		yield break;
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x000468E8 File Offset: 0x00044CE8
	private void CHEATS()
	{
		if (this.currPuzzle == null || this.currPuzzle.name == "loadingMenu")
		{
			return;
		}
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.X) && this.NoCurrentTransition)
		{
			this.gotoNextLevel(false, null);
			Audio.self.stopAllVoices();
		}
		if (!this.packIsScrollable)
		{
			if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.Keypad1) && this.NoCurrentTransition)
			{
				if (this.currPuzzle != null && this.currPuzzle.GetComponent<PuzzleStats>().HasBadEnd)
				{
					Global.LevelCompleted(0f, true);
				}
				else
				{
					PuzzleCup[] componentsInChildren = this.currPuzzle.GetComponentsInChildren<PuzzleCup>(true);
					if (componentsInChildren.Length != 0)
					{
						Global.CupAcquired(componentsInChildren[0].transform);
					}
					else
					{
						Debug.LogWarning("Can't finish puzzle as Bad. No bad ending or no current puzzle");
					}
				}
			}
			if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.Keypad2) && this.NoCurrentTransition)
			{
				if (this.currPuzzle != null && this.currPuzzle.GetComponent<PuzzleStats>().HasGoodEnd)
				{
					Global.LevelFailed(0f, true);
				}
				else
				{
					Debug.LogWarning("Can't finish puzzle as Good. No good ending or no current puzzle");
				}
			}
		}
		else if (this.packHasCompletionLine && Global.self.DEBUG)
		{
			if (Input.GetKeyDown(KeyCode.Keypad1) && this.NoCurrentTransition)
			{
				UIControl.self.setCompletionPackPuzzleState(this.currPuzzle.name, CompletionState.Monster);
			}
			if (Input.GetKeyDown(KeyCode.Keypad3) && this.NoCurrentTransition)
			{
				UIControl.self.setCompletionPackPuzzleState(this.currPuzzle.name, CompletionState.None);
			}
			if (Input.GetKeyDown(KeyCode.Keypad2) && this.NoCurrentTransition)
			{
				UIControl.self.setCompletionPackPuzzleState(this.currPuzzle.name, CompletionState.Good);
			}
		}
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.Tab))
		{
			UIControl.self.packsGraph.SetActive(!UIControl.self.packsGraph.activeInHierarchy);
		}
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.Alpha0))
		{
			this.puzzleSavedStats = new List<SerializablePuzzleStats>();
			this.packSavedStats = new List<SerializablePackSavedStats>();
			this.gameStats = new SerializableGameStats();
			this.cupList[AwardName.Pack13_Bad] = CupStatus.Empty;
			UIControl.addNewChatText("<color=red>RESET GAME</color>");
		}
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.A))
		{
			this.cupList[AwardName.DOMINO] = CupStatus.Empty;
			this.GetCup(AwardName.DOMINO);
		}
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x00046BD6 File Offset: 0x00044FD6
	private void PollSaveLoad()
	{
		if (!SaveLoad.scheduleSave || !SaveLoad.canRepeatSave)
		{
			return;
		}
		this.Save();
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x00046BF3 File Offset: 0x00044FF3
	public void sendVoiceLineToNextPuzzle(AudioVoiceParentChange source)
	{
		this.voiceLineSource = source;
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x00046BFC File Offset: 0x00044FFC
	private void voiceLineChangeParents()
	{
		if (this.voiceLineSource == null)
		{
			return;
		}
		this.voiceLineSource.setVoiceToSend();
		AudioVoiceParentChange[] components = this.nextPuzzle.GetComponents<AudioVoiceParentChange>();
		foreach (AudioVoiceParentChange audioVoiceParentChange in components)
		{
			if (this.voiceLineSource.SendVoiceTo != null && audioVoiceParentChange.GetType().Name == this.voiceLineSource.SendVoiceTo.name)
			{
				audioVoiceParentChange.setExistingVoice(this.voiceLineSource.voiceToSend, this.voiceLineSource.voiceLineToSend);
				this.voiceLineSource = null;
				return;
			}
		}
		this.voiceLineSource.sendFailed();
		this.voiceLineSource = null;
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x00046CC0 File Offset: 0x000450C0
	public void resetPackLevelUI()
	{
		this.endScrollablePack();
		UIControl.self.endCompletionPack();
		UIControl.self.endTimeLine();
		InventoryControl.self.removeInventory();
		if (this.currentPackSelectUI != null)
		{
			this.currPuzzle.GetComponent<PuzzleStats>().UIScreenSecondary = this.currentPackSelectUI;
		}
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x00046D18 File Offset: 0x00045118
	public void LoadGame(string fileName)
	{
		this.saveFileName = fileName;
		SaveData saveData = SaveLoad.Load(this.saveFileName);
		this.cupList = saveData.cupList;
		this.legoCupPieces = saveData.legoCupPieces;
		this.packSavedStats = saveData.packSavedStats;
		this.jigsawPuzzlePieces = saveData.jigsawPieces;
		this.unlockedJigsawPieces = saveData.unlockedJigsawPieces;
		this.puzzleSavedStats = saveData.puzzleSavedStats;
		this.gameStats = saveData.gameStats;
		this.saveFileGUID = saveData.saveFileGUID;
		this.totalPlayedTime = saveData.totalPlayedTime;
		this.ghostCountCurrent = 0;
		this.nyCoinCurrent = 0;
		this.currentLevelPack = 0;
		this.isGameIntroActive = false;
		this.isGameIntroJustFinished = false;
		this.justFinishedExamGoodScore = -1f;
		this.unlockNextPack = false;
		this.currentAwardAnimation = AwardName.None;
		this.lastPackCompletionState = CompletionState.None;
		this.DuckEnabled = (SerializablePackSavedStats.Get(6).solvedAsBad > 0 && !SerializableGameStats.self.isGameFinished);
		this.playIWasRightOnPack07 = false;
		AnalyticsComponent.SaveFileLoaded(this.saveFileGUID);
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x00046E20 File Offset: 0x00045220
	public void Save()
	{
		SaveData saveData = new SaveData();
		saveData.Populate();
		SaveLoad.Save(this.saveFileName, saveData);
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x00046E48 File Offset: 0x00045248
	public void CreateNewGame()
	{
		this.saveFileName = SaveLoad.createNewSaveFile();
		this.cupList = new Dictionary<AwardName, CupStatus>();
		this.legoCupPieces = new List<LegoCupPiece>();
		this.jigsawPuzzlePieces = new List<SerializableJigsawPiece>();
		this.unlockedJigsawPieces = 0;
		this.puzzleSavedStats = new List<SerializablePuzzleStats>();
		this.packSavedStats = new List<SerializablePackSavedStats>();
		this.gameStats = new SerializableGameStats();
		this.saveFileGUID = Guid.NewGuid().ToString();
		this.totalPlayedTime = 0U;
		AnalyticsComponent.NewGameStarted(this.saveFileGUID);
		this.LoadGame(this.saveFileName);
		this.isGameIntroActive = true;
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x00046EE8 File Offset: 0x000452E8
	public int CountPackPlayedTimes(int add = 0)
	{
		SerializablePackSavedStats serializablePackSavedStats = SerializablePackSavedStats.Get(this.currentLevelPack);
		serializablePackSavedStats.completedTimes += add;
		if (add != 0)
		{
			AnalyticsComponent.PackFinished(this.currentLevelPack + 1);
		}
		return serializablePackSavedStats.completedTimes;
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x00046F28 File Offset: 0x00045328
	public int CountPackPlayedTimes(bool monster, int add = 0)
	{
		SerializablePackSavedStats serializablePackSavedStats = SerializablePackSavedStats.Get(this.currentLevelPack);
		if (monster)
		{
			serializablePackSavedStats.solvedAsBad += add;
			return serializablePackSavedStats.solvedAsBad;
		}
		serializablePackSavedStats.solvedAsGood += add;
		return serializablePackSavedStats.solvedAsGood;
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x00046F70 File Offset: 0x00045370
	public int CountPlayedPuzzlesInPack(out int good, out int bad)
	{
		PackListItem[] components = this.levelPackMenu[this.currentLevelPack].GetComponent<levelPackControl>().packList.GetComponents<PackListItem>();
		List<Transform> list = new List<Transform>();
		foreach (PackListItem packListItem in components)
		{
			list.AddRange(packListItem.List);
		}
		list = (from x in list
		where x.GetComponent<PuzzleStats>() != null && (x.GetComponent<PuzzleStats>().HasBadEnd || x.GetComponent<PuzzleStats>().HasGoodEnd)
		select x).ToList<Transform>();
		good = (from x in list
		where SerializablePuzzleStats.Get(x.name).solvedAsGood > 0
		select x).Count<Transform>();
		bad = (from x in list
		where SerializablePuzzleStats.Get(x.name).solvedAsBad > 0
		select x).Count<Transform>();
		return list.Count<Transform>();
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x00047050 File Offset: 0x00045450
	private void UpdateTransition()
	{
		if (this.NoCurrentTransition)
		{
			return;
		}
		if (this.transitionManualSpeed <= 0f)
		{
			return;
		}
		this.transitionFramesCurr++;
		this.transitionTimeCurr = Mathf.MoveTowards(this.transitionTimeCurr, 0f, Time.deltaTime * this.transitionManualSpeed);
		float num = this.transitionTimeCurr / this.transitionTimeMax;
		float num2 = this.transitionCurve.Evaluate(num);
		if (this.transitionExtraScreens > 1)
		{
			float num3 = num2 - this.transitionCurveLong.Evaluate(num);
			num3 *= (float)Mathf.Min(this.transitionExtraScreens - 1, 5) / 5f;
			num2 -= num3;
		}
		if (this.transitionUseStoryModeCurve)
		{
			num2 = this.transitionStoryMode.Evaluate(num);
		}
		this.puzzleParent.position = new Vector2(this.screenSize.x * num2 * this.transitionDirection.x * (float)this.transitionExtraScreens, this.screenSize.y * num2 * this.transitionDirection.y * (float)this.transitionExtraScreens);
		foreach (TransitionProcessor transitionProcessor in this.transitionProcessors)
		{
			transitionProcessor.TransitionUpdate();
		}
		if (this.transitionManualSpeed != 1f)
		{
			return;
		}
		if (num < 0.3f && !this.currentTransitionMiddle)
		{
			this.currentTransitionMiddle = true;
			UIControl.positionSubtitles(this.nextPuzzle);
			UIControl.self.resetTimeLine();
			if (this.prevPuzzle != null)
			{
				SerializablePuzzleStats.Get(this.prevPuzzle.name).addLoadedTimes(this.previousPuzzleSolvedAsMonster);
				this.Save();
			}
		}
		if (num <= 0f)
		{
			this.EndTransition();
		}
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x00047240 File Offset: 0x00045640
	private void EndTransition()
	{
		this.NoCurrentTransition = true;
		this.currentTransitionMiddle = false;
		this.transitionUseStoryModeCurve = false;
		this.transitionExtraScreens = 1;
		foreach (Transform transform in this.transitionExtraScreenList)
		{
			if (this.packIsScrollable)
			{
				transform.gameObject.SetActive(false);
			}
			else
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		this.transitionExtraScreenList.Clear();
		if (this.prevPuzzle)
		{
			this.prevPuzzle.GetComponent<PuzzleStats>().removePuzzle();
		}
		this.nextPuzzle.parent = null;
		this.nextPuzzle.position = Vector2.zero;
		this.prevPuzzle = null;
		this.currPuzzle = this.nextPuzzle;
		this.nextPuzzle = null;
		this.currPuzzle.position = Vector3.zero;
		this.setScripts(this.currPuzzle, true);
		this.setPhysicSounds(this.currPuzzle, true);
		Global.puzzleTimer = 0f;
		Global.clickCounter = 0;
		FPSRecorder.self.addEvent(this.currPuzzle.name + " on screen");
		if (this.packIsScrollable && AudioVoice_ScrollableController.self != null)
		{
			AudioVoice_ScrollableController.self.onTransitionEnd();
		}
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000473BC File Offset: 0x000457BC
	public void changeTransitionTime(float newTime)
	{
		this.transitionTimeCurr = newTime;
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x000473C8 File Offset: 0x000457C8
	private void startTransition()
	{
		if (this.nextPuzzle == null)
		{
			Debug.LogError("nextPuzzle can't be null");
			return;
		}
		if (this.packIsScrollable && this.nextPuzzle.GetComponentInChildren<PuzzleHomework_LastPaper>(true) != null)
		{
			this.nextPuzzle.GetComponentInChildren<PuzzleHomework_LastPaper>(true).TransitionStarted();
		}
		this.transitionProcessors.Clear();
		this.transitionProcessors = this.nextPuzzle.GetComponentsInChildren<TransitionProcessor>(true).ToList<TransitionProcessor>();
		if (this.prevPuzzle != null)
		{
			this.transitionProcessors.AddRange(this.prevPuzzle.GetComponentsInChildren<TransitionProcessor>(true).ToList<TransitionProcessor>());
		}
		this.transitionDirection = Vector3.zero - this.nextPuzzle.position;
		this.transitionDirection.Normalize();
		if (this.transitionDirection.x != 0f)
		{
			this.transitionTimeMax = this.transitionTimeSide;
		}
		else
		{
			this.transitionTimeMax = this.transitionTimeTop;
		}
		this.transitionTimeCurr = this.transitionTimeMax;
		this.NoCurrentTransition = false;
		this.transitionFramesCurr = 0;
		this.backgroundControl.startBGSwap(this.nextPuzzle.GetComponent<PuzzleStats>().background);
		EventSystem.current.SetSelectedGameObject(null);
		this.transitionCurrentSound = default(EventInstance);
		if (this.prevPuzzle != null)
		{
			if (this.prevPuzzle.GetComponent<levelPackControl>())
			{
				if (this.nextPuzzle.GetComponent<levelPackControl>())
				{
					this.transitionCurrentSound = Audio.self.playOneShot("1d864497-5ddc-47e1-a28a-76387fb24745", 1f);
				}
				else
				{
					Audio.self.playOneShot("9ee2bc76-238d-4444-a532-6d9d22daf508", 1f);
				}
			}
			else
			{
				this.transitionCurrentSound = Audio.self.playOneShot("1d864497-5ddc-47e1-a28a-76387fb24745", 1f);
			}
		}
		this.voiceLineChangeParents();
		this.puzzleParent.position = Vector2.zero;
		this.nextPuzzle.SetParent(this.puzzleParent);
		if (this.prevPuzzle)
		{
			this.prevPuzzle.SetParent(this.puzzleParent);
		}
		if (this.currPuzzle)
		{
			this.currPuzzle.SetParent(this.puzzleParent);
		}
		if (this.packIsScrollable && AudioVoice_ScrollableController.self != null)
		{
			AudioVoice_ScrollableController.self.onTransitionStart(this.transitionCurrentSound);
		}
		global::Console.self.TryToHideOptionsConsole();
		AnalyticsComponent.Flush(false);
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x00047658 File Offset: 0x00045A58
	public bool currPuzzleSubtitlesAtBottom(Transform puzzle)
	{
		if (puzzle == null)
		{
			puzzle = this.currPuzzle;
		}
		PuzzleStats component = puzzle.GetComponent<PuzzleStats>();
		return component.subtitlesBottom;
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x00047688 File Offset: 0x00045A88
	public float currPuzzleSubtitlesYShift(Transform puzzle)
	{
		if (puzzle == null)
		{
			puzzle = this.currPuzzle;
		}
		PuzzleStats component = puzzle.GetComponent<PuzzleStats>();
		return component.subtitlesYShift;
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x000476B6 File Offset: 0x00045AB6
	public void makeLevelList(Transform[] list)
	{
		this.makeLevelList(list.ToList<Transform>());
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x000476C4 File Offset: 0x00045AC4
	public void makeLevelList(List<Transform> list)
	{
		this.loopFirstPuzzle = false;
		this.puzzleList = list;
		this.gotoNextLevel(false, null);
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000476EF File Offset: 0x00045AEF
	public void resetLevelList()
	{
		this.puzzleList = new List<Transform>();
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000476FC File Offset: 0x00045AFC
	public void makeSameLevel()
	{
		if (this.puzzleList == null || this.lastPlayed == null)
		{
			return;
		}
		this.puzzleList.Insert(0, this.lastPlayed);
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x00047730 File Offset: 0x00045B30
	public void startScrollablePack()
	{
		this.packIsScrollable = true;
		this.scrollableEndReached = false;
		this.scrollableUI = UIControl.makeUI(this.UIforScrollable, null);
		this.prevPuzzleList.Clear();
		this.prevPuzzleList.Add(this.nextPuzzle);
		this.lastPlayed = this.nextPuzzle;
		this.prevPuzzleIndex = 1;
		PuzzleStats component = this.nextPuzzle.GetComponent<PuzzleStats>();
		component.UIScreenCurr = this.scrollableUI;
		component.positionUIScreen(component.UIScreenCurr);
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x000477B0 File Offset: 0x00045BB0
	public void stopScrollablePack()
	{
		if (this.scrollableEndReached)
		{
			return;
		}
		this.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr = this.scrollableUI;
		this.scrollableEndReached = true;
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x000477DC File Offset: 0x00045BDC
	public void endScrollablePack()
	{
		if (!this.packIsScrollable)
		{
			return;
		}
		this.scrollableEndReached = false;
		this.packIsScrollable = false;
		this.prevPuzzleList.Clear();
		this.puzzleList.RemoveAt(0);
		while (this.puzzleParent.childCount > 0)
		{
			Transform child = this.puzzleParent.GetChild(0);
			child.SetParent(null);
			UnityEngine.Object.Destroy(child.gameObject);
		}
		UnityEngine.Object.Destroy(this.scrollableUI.gameObject);
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x0004785F File Offset: 0x00045C5F
	public Transform getNextPuzzleToChangeVoiceParent()
	{
		return this.nextPuzzle;
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x00047868 File Offset: 0x00045C68
	public void gotoNextLevel(bool back = false, bool? last = null)
	{
		if (this.loopFirstPuzzle)
		{
			this.makeNewLevel(this.firstPuzzle, Vector2.right, true);
			return;
		}
		this.updateDynamicPuzzles();
		while (this.puzzleList.Count > 0 && !this.isPuzzleAllowed(this.puzzleList[0]))
		{
			this.puzzleList.RemoveAt(0);
		}
		if (this.packIsScrollable)
		{
			if (this.gotoNextScrollableLevel(back, last))
			{
				return;
			}
		}
		else if (this.puzzleList.Count > 0)
		{
			this.makeNewLevel(this.puzzleList[0], Vector2.right, true);
			this.lastPlayed = this.puzzleList[0];
			this.puzzleList.RemoveAt(0);
			UIControl.self.moveCompletionIndex(-1);
			return;
		}
		if (this.puzzleList.Count <= 0)
		{
			if (this.replayingCupPuzzle)
			{
				this.makeNewLevel(this.levelPackMenu[this.currentLevelPack], Vector2.left, true);
				return;
			}
			if (AwardController.self == null)
			{
				this.makeNewLevel(this.mainMenu, Vector2.left, true);
				Debug.LogWarning("AwardController couldn't be found. Exiting.");
				return;
			}
			this.lastPlayed = AwardController.self.makeAwardLevel();
			if (this.lastPlayed != null)
			{
				this.makeNewLevel(this.lastPlayed, Vector2.right, true);
			}
			else
			{
				this.makeNewLevel(this.levelPackMenu[this.currentLevelPack], Vector2.left, true);
			}
		}
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x000479F8 File Offset: 0x00045DF8
	private void updateDynamicPuzzles()
	{
		IDynamicPackListItem[] components = this.levelPackMenu[this.currentLevelPack].GetComponent<levelPackControl>().packList.GetComponents<IDynamicPackListItem>();
		if (components == null || components.Count<IDynamicPackListItem>() == 0)
		{
			return;
		}
		foreach (IDynamicPackListItem dynamicPackListItem in components)
		{
			Transform transform = dynamicPackListItem.addPuzzle();
			if (transform != null)
			{
				this.puzzleList.Insert(0, transform);
			}
		}
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x00047A74 File Offset: 0x00045E74
	private bool gotoNextScrollableLevel(bool back, bool? last)
	{
		int num = this.prevPuzzleIndex;
		this.prevPuzzleIndex += ((!back) ? 1 : -1);
		if (last != null)
		{
			if (last == false)
			{
				this.prevPuzzleIndex = 1;
			}
			else
			{
				this.prevPuzzleIndex = this.prevPuzzleList.Count;
			}
		}
		this.scrollableUI.GetComponent<scrollablePackArrows>().setArrow(true, true, new bool?(this.prevPuzzleIndex > 2));
		this.scrollableUI.GetComponent<scrollablePackArrows>().setArrow(false, true, new bool?(this.prevPuzzleIndex + 1 < this.prevPuzzleList.Count));
		if (this.prevPuzzleIndex > this.prevPuzzleList.Count)
		{
			if (this.puzzleList.Count <= 0)
			{
				return false;
			}
			this.makeNewLevel(this.puzzleList[0], Vector2.right, true);
			if (!this.scrollableEndReached)
			{
				this.lastPlayed = this.nextPuzzle;
				this.puzzleList.RemoveAt(0);
				this.prevPuzzleList.Add(this.nextPuzzle);
				UIControl.self.moveCompletionIndex(this.prevPuzzleIndex - 1);
			}
			else
			{
				this.lastPlayed = this.puzzleList[0];
				UIControl.self.hideCompletionLine(false);
			}
			return true;
		}
		else
		{
			if (this.prevPuzzleIndex > 0)
			{
				Transform transform = this.prevPuzzleList[this.prevPuzzleIndex - 1];
				transform.SetParent(null);
				transform.gameObject.SetActive(true);
				this.lastPlayed = transform;
				this.makeNewLevel(transform, (!back) ? Vector2.right : Vector2.left, false);
				if (this.prevPuzzleIndex == 1)
				{
					this.scrollableUI.GetComponent<scrollablePackArrows>().setArrow(true, false, new bool?(false));
				}
				UIControl.self.moveCompletionIndex(this.prevPuzzleIndex - 1);
				this.scrollMultipleScreens(this.prevPuzzleList.ToArray(), num - 1, this.prevPuzzleIndex - 1, (num >= this.prevPuzzleIndex) ? Vector2.left : Vector2.right, false);
				return true;
			}
			return false;
		}
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x00047CA4 File Offset: 0x000460A4
	private bool isPuzzleAllowed(Transform item)
	{
		ScreenCard component = item.GetComponent<ScreenCard>();
		return !component || component.checkIfAllowed();
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x00047CD4 File Offset: 0x000460D4
	public void scrollMultipleScreens(Transform[] list, int current, int target, Vector2 dir, bool newItem)
	{
		this.transitionExtraScreens = Mathf.Abs(current - target);
		if (this.transitionExtraScreens == 1)
		{
			return;
		}
		this.nextPuzzle.position = Vector2.Scale(dir, this.screenSize * (float)this.transitionExtraScreens);
		int num = current;
		current = (int)Mathf.MoveTowards((float)current, (float)target, 1f);
		this.transitionTimeMax = ((dir.x != 0f) ? (this.transitionTimeSide * ((float)Mathf.Abs(current - target) * this.transitionMultipleScreenSpeed + 1f)) : this.transitionTimeTop);
		this.transitionTimeCurr = this.transitionTimeMax;
		while (current != target)
		{
			Transform transform;
			if (newItem)
			{
				transform = UnityEngine.Object.Instantiate<Transform>(list[current]);
			}
			else
			{
				transform = list[current];
				transform.gameObject.SetActive(true);
			}
			transform.SetParent(this.puzzleParent);
			int num2 = Mathf.Abs(num - current);
			transform.position = Vector2.Scale(dir, this.screenSize * (float)num2);
			this.setScripts(transform, false);
			this.setPhysicSounds(transform, false);
			this.transitionExtraScreenList.Add(transform);
			current = (int)Mathf.MoveTowards((float)current, (float)target, 1f);
		}
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x00047E18 File Offset: 0x00046218
	public void makeNewLevel(Transform item, Vector2 dir, bool newItem = true)
	{
		if (this.currPuzzle)
		{
			this.setScripts(this.currPuzzle, false);
			this.setPhysicSounds(this.currPuzzle, false);
		}
		string text = string.Empty;
		if (item.GetComponent<PuzzleStats>() == null)
		{
			IEnumerator enumerator = item.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.GetComponent<PuzzleStats>() != null)
					{
						text = item.name;
						item = transform;
						break;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		Transform transform2;
		if (newItem)
		{
			transform2 = UnityEngine.Object.Instantiate<Transform>(item);
			if (text != string.Empty)
			{
				transform2.gameObject.name = text;
			}
			else
			{
				transform2.gameObject.name = item.name;
			}
		}
		else
		{
			transform2 = item;
		}
		transform2.localScale = Vector3.one;
		transform2.position = Vector2.Scale(dir, this.screenSize);
		this.setScripts(transform2, false);
		this.setPhysicSounds(transform2, false);
		if (transform2.GetComponent<PuzzleStats>().loadUIOnStart)
		{
			UIControl.makeUIScreen(transform2);
		}
		this.prevPuzzle = this.currPuzzle;
		this.currPuzzle = null;
		this.nextPuzzle = transform2;
		FPSRecorder.self.addEvent(transform2.name + " coming up");
		this.startTransition();
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x00047FA8 File Offset: 0x000463A8
	private void setScripts(Transform item, bool on = false)
	{
		if (item == null)
		{
			Debug.LogError("item is empty. this shouldn't happen");
			return;
		}
		List<MonoBehaviour> list = new List<MonoBehaviour>();
		MonoBehaviour[] componentsInChildren = item.GetComponentsInChildren<MonoBehaviour>();
		foreach (MonoBehaviour monoBehaviour in componentsInChildren)
		{
			if (!(monoBehaviour.transform is RectTransform))
			{
				if (!on || !Attribute.IsDefined(monoBehaviour.GetType(), typeof(EnabledManually)))
				{
					if (monoBehaviour is InventoryItem)
					{
						list.Add(monoBehaviour);
					}
					else
					{
						monoBehaviour.enabled = on;
					}
				}
			}
		}
		foreach (MonoBehaviour monoBehaviour2 in list)
		{
			monoBehaviour2.enabled = on;
		}
		item.GetComponent<PuzzleStats>().enabled = true;
		item.GetComponent<PuzzleStats>().setActive(on);
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000480B8 File Offset: 0x000464B8
	private void setPhysicSounds(Transform item, bool on = false)
	{
		if (item == null)
		{
			Debug.LogError("item is empty. this shouldn't happen");
			return;
		}
		PhysicsSound[] componentsInChildren = item.GetComponentsInChildren<PhysicsSound>();
		foreach (PhysicsSound physicsSound in componentsInChildren)
		{
			if (!(physicsSound.transform is RectTransform))
			{
				if (!physicsSound.disableAutomaticEnable)
				{
					physicsSound.enabled = on;
					physicsSound.enable = on;
				}
			}
		}
		if (!on)
		{
			Audio.self.resetLoopSounds();
		}
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x0004813C File Offset: 0x0004653C
	public void makePauseMenu()
	{
		if (this.packIsScrollable && this.scrollableUI.gameObject.activeInHierarchy)
		{
			this.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr = this.scrollableUI;
		}
		UIControl.self.hideCompletionLine(false);
		InventoryControl.self.hideInventory();
		this.makeNewLevel(this.pauseMenu, Vector2.left, true);
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x000481A8 File Offset: 0x000465A8
	public void unpauseGame()
	{
		if (this.lastPlayed == null)
		{
			this.gotoNextLevel(false, null);
		}
		else if (this.packIsScrollable && !this.scrollableEndReached)
		{
			this.lastPlayed.gameObject.SetActive(true);
			this.scrollableUI.gameObject.SetActive(true);
			this.lastPlayed.SetParent(null);
			this.makeNewLevel(this.lastPlayed, Vector2.right, false);
		}
		else
		{
			this.makeNewLevel(this.lastPlayed, Vector2.right, true);
		}
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x00048248 File Offset: 0x00046648
	public bool GetCup(AwardName nm)
	{
		if (this.cupList[nm] != CupStatus.Empty)
		{
			return false;
		}
		AwardData awardData = AwardData.Get(nm, this.currLanguage);
		if (awardData == null)
		{
			Debug.LogError("Can't find this Award in AwardData: " + awardData + ";");
		}
		else if (awardData.achievement && !SteamworksAPI.AcquireAchievement(nm.ToString()))
		{
			AchievementPopup.self.AddAchievement(nm);
		}
		this.cupList[nm] = CupStatus.ShowAnimation;
		this.Save();
		if (awardData != null && awardData.achievement)
		{
			AnalyticsComponent.CupAcquired(this.currPuzzle.name, nm);
		}
		else
		{
			AnalyticsComponent.CupAcquired(this.currentLevelPack, nm);
		}
		return true;
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x0004830C File Offset: 0x0004670C
	public static void CupAcquired(Transform cup)
	{
		Global.self.awardOnLevel = cup;
		Global.self.currPuzzle.GetComponent<PuzzleStats>().setSolvedAsMonster(true);
		Global.self.setScripts(Global.self.currPuzzle, false);
		Global.self.setPhysicSounds(Global.self.currPuzzle, false);
		UIControl.makeUIScreen(Global.self.currPuzzle);
		AnalyticsComponent.CupPuzzleFinished(Global.self.currPuzzle.name);
		AnalyticsComponent.PuzzleFinishedStats(Global.self.currPuzzle.name, Global.clickCounter, Global.puzzleTimer);
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x000483A4 File Offset: 0x000467A4
	public static void TellAnalyticsLevelFinished()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Global.clickCounter++;
		}
		AnalyticsComponent.PuzzleFinishedStats(Global.self.currPuzzle.name, Global.clickCounter, Global.puzzleTimer);
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x000483DB File Offset: 0x000467DB
	public static void TellAnalyticsLevelAborted()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Global.clickCounter++;
		}
		AnalyticsComponent.PuzzleAbortedStats(Global.self.currPuzzle.name, Global.clickCounter, Global.puzzleTimer);
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x00048414 File Offset: 0x00046814
	public static void LevelCompleted(float waitTime = 0f, bool turnOffScripts = true)
	{
		if (Input.GetMouseButtonDown(0))
		{
			Global.clickCounter++;
		}
		PuzzleStats component = Global.self.currPuzzle.GetComponent<PuzzleStats>();
		if (component.HasBadEnd && !Global.self.loopFirstPuzzle && (!Global.self.DEBUG || AwardController.self != null))
		{
			AwardController.self.solveAsBad(component.transform);
		}
		Global.self.cantExitEndScreenTimer = waitTime;
		component.setSolvedAsMonster(true);
		Global.self.canBePaused = true;
		if (turnOffScripts)
		{
			Global.self.setScripts(Global.self.currPuzzle, false);
		}
		AnalyticsComponent.PuzzleFinishedAsMonster(Global.self.currPuzzle.name);
		AnalyticsComponent.PuzzleFinishedStats(Global.self.currPuzzle.name, Global.clickCounter, Global.puzzleTimer);
		if (!Global.setCompletionState(CompletionState.Monster, null))
		{
			UIControl.makeUIScreen(Global.self.currPuzzle);
		}
		UIControl.self.stopTimeLine();
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x00048520 File Offset: 0x00046920
	public static void LevelFailed(float waitTime = 0f, bool turnOffScripts = true)
	{
		if (Input.GetMouseButtonDown(0))
		{
			Global.clickCounter++;
		}
		PuzzleStats component = Global.self.currPuzzle.GetComponent<PuzzleStats>();
		if (component.HasGoodEnd && !Global.self.loopFirstPuzzle && (!Global.self.DEBUG || AwardController.self != null))
		{
			AwardController.self.solveAsGood(component.transform);
		}
		Global.self.cantExitEndScreenTimer = waitTime;
		component.setSolvedAsMonster(false);
		Global.self.canBePaused = true;
		if (turnOffScripts)
		{
			Global.self.setScripts(Global.self.currPuzzle, false);
		}
		AnalyticsComponent.PuzzleFinishedAsGood(Global.self.currPuzzle.name);
		AnalyticsComponent.PuzzleFinishedStats(Global.self.currPuzzle.name, Global.clickCounter, Global.puzzleTimer);
		if (!Global.setCompletionState(CompletionState.Good, null))
		{
			UIControl.makeUIScreen(Global.self.currPuzzle);
		}
		UIControl.self.stopTimeLine();
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x0004862C File Offset: 0x00046A2C
	public static bool setCompletionState(CompletionState state, Transform puzzle = null)
	{
		if (!Global.self.packHasCompletionLine)
		{
			return false;
		}
		if (puzzle == null)
		{
			puzzle = Global.self.currPuzzle;
		}
		if (state != CompletionState.Good)
		{
			if (state != CompletionState.Monster)
			{
				if (state == CompletionState.None)
				{
					AnalyticsComponent.PuzzleFinishedAsNone(puzzle.name);
				}
			}
			else
			{
				AnalyticsComponent.PuzzleFinishedAsMonster(puzzle.name);
			}
		}
		else
		{
			AnalyticsComponent.PuzzleFinishedAsGood(puzzle.name);
		}
		UIControl.self.setCompletionPackPuzzleState(puzzle.name, state);
		return true;
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x000486BD File Offset: 0x00046ABD
	public static CompletionState getCompletionState(Transform puzzle = null)
	{
		if (!Global.self.packHasCompletionLine)
		{
			return CompletionState.None;
		}
		if (puzzle == null)
		{
			puzzle = Global.self.currPuzzle;
		}
		return UIControl.self.getCompletionPackPuzzleState(puzzle.name);
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x000486F8 File Offset: 0x00046AF8
	public static void PauseArrows(float time = -1f)
	{
		if (time == -1f)
		{
			Global.self.scrollableUI.GetComponent<scrollablePackArrows>().canUseArrows = false;
		}
		else if (time > 0f)
		{
			Global.self.scrollableUI.GetComponent<scrollablePackArrows>().pauseScrolling(time);
		}
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x0004874A File Offset: 0x00046B4A
	public static void UnpauseArrows()
	{
		Global.self.scrollableUI.GetComponent<scrollablePackArrows>().canUseArrows = true;
	}

	// Token: 0x040013E6 RID: 5094
	private static Global _self;

	// Token: 0x040013E7 RID: 5095
	[Tooltip("DUCK's website")]
	public string duckSolutionDatabase;

	// Token: 0x040013E8 RID: 5096
	[Tooltip("How big puzzle are - Height and Width")]
	public Vector2 screenSize = new Vector2(18f, 10f);

	// Token: 0x040013E9 RID: 5097
	[Tooltip("Is it imperial system or metrical? To show time and distances")]
	public bool metricSystem = true;

	// Token: 0x040013EA RID: 5098
	[Tooltip("Current language used")]
	public string defaultLanguage = "EN";

	// Token: 0x040013EB RID: 5099
	private string _currLanguage;

	// Token: 0x040013EC RID: 5100
	[Tooltip("Show some errors, or show some features for debugging")]
	public bool DEBUG = true;

	// Token: 0x040013ED RID: 5101
	[HideInInspector]
	public string saveFileName = string.Empty;

	// Token: 0x040013EE RID: 5102
	[HideInInspector]
	public string saveFileGUID = string.Empty;

	// Token: 0x040013EF RID: 5103
	[HideInInspector]
	public uint totalPlayedTime;

	// Token: 0x040013F0 RID: 5104
	[Tooltip("Load default save file on start")]
	public bool loadOnStart;

	// Token: 0x040013F1 RID: 5105
	[Header("Transition")]
	[Tooltip("Script that controls background transitions")]
	public BackgroundControl backgroundControl;

	// Token: 0x040013F2 RID: 5106
	[HideInInspector]
	public float cantExitEndScreenTimer;

	// Token: 0x040013F3 RID: 5107
	[HideInInspector]
	public bool canExitEndScreen = true;

	// Token: 0x040013F4 RID: 5108
	private List<TransitionProcessor> transitionProcessors = new List<TransitionProcessor>();

	// Token: 0x040013F5 RID: 5109
	[Tooltip("Graph on how to move screen between transitions")]
	public AnimationCurve transitionCurve;

	// Token: 0x040013F6 RID: 5110
	[Tooltip("Graph on how to move screen with long transitions")]
	public AnimationCurve transitionCurveLong;

	// Token: 0x040013F7 RID: 5111
	[Tooltip("Graph on how to move screen in story mode pack")]
	public AnimationCurve transitionStoryMode;

	// Token: 0x040013F8 RID: 5112
	[HideInInspector]
	public bool transitionUseStoryModeCurve;

	// Token: 0x040013F9 RID: 5113
	private Vector2 transitionDirection;

	// Token: 0x040013FA RID: 5114
	[Tooltip("How long side transition should take")]
	public float transitionTimeSide = 2f;

	// Token: 0x040013FB RID: 5115
	[Tooltip("How long vertical transition should take")]
	public float transitionTimeTop = 2f;

	// Token: 0x040013FC RID: 5116
	private float transitionTimeMax;

	// Token: 0x040013FD RID: 5117
	private float transitionTimeCurr;

	// Token: 0x040013FE RID: 5118
	[HideInInspector]
	public int transitionFramesCurr;

	// Token: 0x040013FF RID: 5119
	[HideInInspector]
	public bool NoCurrentTransition = true;

	// Token: 0x04001400 RID: 5120
	private bool currentTransitionMiddle;

	// Token: 0x04001401 RID: 5121
	private int transitionExtraScreens = 1;

	// Token: 0x04001402 RID: 5122
	[Tooltip("Add extra speed if there is more than 1 screen to transition")]
	public float transitionMultipleScreenSpeed = 0.08f;

	// Token: 0x04001403 RID: 5123
	private List<Transform> transitionExtraScreenList = new List<Transform>();

	// Token: 0x04001404 RID: 5124
	[HideInInspector]
	public float transitionManualSpeed = 1f;

	// Token: 0x04001405 RID: 5125
	[HideInInspector]
	public EventInstance transitionCurrentSound;

	// Token: 0x04001406 RID: 5126
	[Header("Puzzles")]
	[Tooltip("Load this level first. Should be mainMenu puzzle")]
	public Transform firstPuzzle;

	// Token: 0x04001407 RID: 5127
	public bool loopFirstPuzzle;

	// Token: 0x04001408 RID: 5128
	public bool transitionFromDown;

	// Token: 0x04001409 RID: 5129
	[Header("Pack progress")]
	[Tooltip("Is this a first time ever the game is starting?")]
	public bool firstTimeLoadingGame;

	// Token: 0x0400140A RID: 5130
	[Tooltip("GameIntro is a cutscene that you go through first time when you play this game")]
	public bool isGameIntroActive;

	// Token: 0x0400140B RID: 5131
	[Tooltip("TRUE - player just played game intro, and finished TrashCup on the previous screen")]
	public bool isGameIntroJustFinished;

	// Token: 0x0400140C RID: 5132
	[Tooltip("TRUE - Pack03 is loaded first time riight after game intro finished")]
	public bool isThirdPackAfterGamesIntro;

	// Token: 0x0400140D RID: 5133
	[Tooltip("TRUE - player just played and finished Exam pack")]
	public float justFinishedExamGoodScore = -1f;

	// Token: 0x0400140E RID: 5134
	[Tooltip("TRUE - this Menu screen is loaded right after player finished pack")]
	public CompletionState lastPackCompletionState;

	// Token: 0x0400140F RID: 5135
	[Tooltip("TRUE - DUCK is active - show it everywhere from now on")]
	public bool DuckInPack07IsActive;

	// Token: 0x04001410 RID: 5136
	[Tooltip("Puzzles Queue appearing time in the pack07 when Duck is enabled ")]
	public int queuePuzzleIndex;

	// Token: 0x04001411 RID: 5137
	[Tooltip("DUCK is active in the game (if pack07 solved bad at least once")]
	public bool DuckEnabled;

	// Token: 0x04001412 RID: 5138
	[HideInInspector]
	public bool skipFirstLineOnPack07;

	// Token: 0x04001413 RID: 5139
	[HideInInspector]
	public bool playIWasRightOnPack07;

	// Token: 0x04001414 RID: 5140
	[HideInInspector]
	public bool pack10CutsceneActive;

	// Token: 0x04001415 RID: 5141
	[Tooltip("TRUE - special voice is activated for the last portion of the pack12")]
	public bool endCutscenePack12;

	// Token: 0x04001416 RID: 5142
	[Header("Menu Screens/Puzzles")]
	public Transform mainMenu;

	// Token: 0x04001417 RID: 5143
	public Transform optionsMenu;

	// Token: 0x04001418 RID: 5144
	public Transform advancedOptionsMenu;

	// Token: 0x04001419 RID: 5145
	public Transform loadMenu;

	// Token: 0x0400141A RID: 5146
	public Transform creditsMenu;

	// Token: 0x0400141B RID: 5147
	public Transform resolutionMenu;

	// Token: 0x0400141C RID: 5148
	public Transform pauseMenu;

	// Token: 0x0400141D RID: 5149
	public Transform exitMenu;

	// Token: 0x0400141E RID: 5150
	public Transform loadingMenu;

	// Token: 0x0400141F RID: 5151
	public Transform UIforScrollable;

	// Token: 0x04001420 RID: 5152
	public Transform packSelectUI;

	// Token: 0x04001421 RID: 5153
	public Transform companyLogo;

	// Token: 0x04001422 RID: 5154
	public Transform[] levelPackMenu;

	// Token: 0x04001423 RID: 5155
	[HideInInspector]
	public int currentLevelPack;

	// Token: 0x04001424 RID: 5156
	[HideInInspector]
	public bool fullscreen = true;

	// Token: 0x04001425 RID: 5157
	[HideInInspector]
	public bool lockMouse = true;

	// Token: 0x04001426 RID: 5158
	private bool _canBePaused = true;

	// Token: 0x04001427 RID: 5159
	private Transform puzzleParent;

	// Token: 0x04001428 RID: 5160
	private Transform nextPuzzle;

	// Token: 0x04001429 RID: 5161
	[HideInInspector]
	public Transform currPuzzle;

	// Token: 0x0400142A RID: 5162
	private Transform prevPuzzle;

	// Token: 0x0400142B RID: 5163
	private List<Transform> puzzleList = new List<Transform>();

	// Token: 0x0400142C RID: 5164
	private List<Transform> prevPuzzleList = new List<Transform>();

	// Token: 0x0400142D RID: 5165
	[HideInInspector]
	public int prevPuzzleIndex;

	// Token: 0x0400142E RID: 5166
	[HideInInspector]
	public Transform lastPlayed;

	// Token: 0x0400142F RID: 5167
	[HideInInspector]
	public bool? previousPuzzleSolvedAsMonster;

	// Token: 0x04001430 RID: 5168
	[HideInInspector]
	public bool packIsScrollable;

	// Token: 0x04001431 RID: 5169
	[HideInInspector]
	public Transform scrollableUI;

	// Token: 0x04001432 RID: 5170
	[HideInInspector]
	public bool scrollableEndReached;

	// Token: 0x04001433 RID: 5171
	[HideInInspector]
	public bool packHasCompletionLine;

	// Token: 0x04001434 RID: 5172
	[HideInInspector]
	public Transform currentPackSelectUI;

	// Token: 0x04001435 RID: 5173
	[HideInInspector]
	public bool packHasTimeLine;

	// Token: 0x04001436 RID: 5174
	[HideInInspector]
	public bool replayingCupPuzzle;

	// Token: 0x04001437 RID: 5175
	[Header("Award")]
	[Tooltip("Collect ALL the cups, EXCEPT this list to get this reward")]
	public AwardName[] awardsToSkip;

	// Token: 0x04001438 RID: 5176
	[HideInInspector]
	public List<SerializablePackSavedStats> packSavedStats = new List<SerializablePackSavedStats>();

	// Token: 0x04001439 RID: 5177
	[HideInInspector]
	public List<SerializablePuzzleStats> puzzleSavedStats = new List<SerializablePuzzleStats>();

	// Token: 0x0400143A RID: 5178
	[HideInInspector]
	public SerializableGameStats gameStats;

	// Token: 0x0400143B RID: 5179
	public const string PREV_PUZZLE_STATS = "PreviousPuzzleStats";

	// Token: 0x0400143C RID: 5180
	public Dictionary<AwardName, CupStatus> cupList = new Dictionary<AwardName, CupStatus>();

	// Token: 0x0400143D RID: 5181
	[Header("Lego/JigSaw")]
	public int totalLegoPieces = 20;

	// Token: 0x0400143E RID: 5182
	[HideInInspector]
	public List<LegoCupPiece> legoCupPieces = new List<LegoCupPiece>();

	// Token: 0x0400143F RID: 5183
	[HideInInspector]
	public List<SerializableJigsawPiece> jigsawPuzzlePieces = new List<SerializableJigsawPiece>();

	// Token: 0x04001440 RID: 5184
	public const int JIGSAW_MAX = 20;

	// Token: 0x04001441 RID: 5185
	public int unlockedJigsawPieces = 5;

	// Token: 0x04001442 RID: 5186
	[HideInInspector]
	public AwardName currentAwardAnimation;

	// Token: 0x04001443 RID: 5187
	[HideInInspector]
	public int currentAwardAnimCount;

	// Token: 0x04001444 RID: 5188
	[HideInInspector]
	public Transform awardOnLevel;

	// Token: 0x04001445 RID: 5189
	[HideInInspector]
	public bool unlockNextPack;

	// Token: 0x04001446 RID: 5190
	[HideInInspector]
	public int lastSpoilerCupUpdateTime;

	// Token: 0x04001447 RID: 5191
	[HideInInspector]
	public string lastSpoilerCupTextID = string.Empty;

	// Token: 0x04001448 RID: 5192
	private AudioVoiceParentChange voiceLineSource;

	// Token: 0x04001449 RID: 5193
	[Header("Ghost")]
	public int ghostCountMax = 4;

	// Token: 0x0400144A RID: 5194
	public int ghostCountCurrent;

	// Token: 0x0400144B RID: 5195
	[Header("New Year Coins")]
	public int nyCoinMax = 100;

	// Token: 0x0400144C RID: 5196
	public int nyCoinsMaxFake = 4;

	// Token: 0x0400144D RID: 5197
	public int nyCoinCurrent;

	// Token: 0x0400144E RID: 5198
	[Header("Analytics")]
	public AnalyticsComponent currentAnalytics;

	// Token: 0x04001451 RID: 5201
	private static float puzzleTimer;

	// Token: 0x04001452 RID: 5202
	private static int clickCounter;

	// Token: 0x04001453 RID: 5203
	[HideInInspector]
	public bool openGreenlightPage;
}
