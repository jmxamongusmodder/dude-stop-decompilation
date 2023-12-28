using System;
using UnityEngine;

// Token: 0x02000472 RID: 1138
public class PuzzleStats : MonoBehaviour
{
	// Token: 0x06001D3F RID: 7487 RVA: 0x000438BC File Offset: 0x00041CBC
	private void Awake()
	{
		if (this.isMenu)
		{
			Global.self.currentAwardAnimCount = 0;
			Award[] componentsInChildren = base.GetComponentsInChildren<Award>();
			foreach (Award award in componentsInChildren)
			{
				award.ps = this;
				if (Global.self.DEBUG)
				{
					CheatSetRewards component = Global.self.GetComponent<CheatSetRewards>();
					if (component.applyToAll)
					{
						Global.self.cupList[award.awardName] = component.cupStatus;
					}
					if (Input.GetKey(KeyCode.LeftControl))
					{
						Global.self.cupList[award.awardName] = CupStatus.ShowAnimation;
					}
					if (Input.GetKey(KeyCode.LeftShift))
					{
						Global.self.cupList[award.awardName] = CupStatus.Exist;
					}
				}
				award.setCupState();
			}
		}
		if (this.background != null)
		{
			this.background.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x000439BC File Offset: 0x00041DBC
	private void Update()
	{
		if (Input.GetButtonDown("Cancel") && this.active && !this.isMenu && Global.self.canBePaused && !Global.self.packHasTimeLine)
		{
			this.makePauseMenu();
		}
		if (this.UIScreenCurr != null)
		{
			this.positionUIScreen(this.UIScreenCurr);
		}
		if (this.UIScreenSecondary != null)
		{
			this.positionUIScreen(this.UIScreenSecondary);
		}
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x00043A4C File Offset: 0x00041E4C
	public void setSolvedAsMonster(bool bad = true)
	{
		this.solvedAsBad = new bool?(bad);
		Global.self.previousPuzzleSolvedAsMonster = new bool?(bad);
		if (bad)
		{
			SerializablePuzzleStats.Get(base.name).solvedAsBad++;
		}
		else
		{
			SerializablePuzzleStats.Get(base.name).solvedAsGood++;
		}
		Global.self.Save();
		if (this.jigSawPieceOnPuzzle != null)
		{
			this.jigSawPieceOnPuzzle.hide();
		}
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x00043AD8 File Offset: 0x00041ED8
	public void setActive(bool on)
	{
		this.active = on;
		if (this.UIScreenCurr != null)
		{
			this.UIScreenCurr.GetComponent<AbstractUIScreen>().setActive(this.active);
		}
		if (this.UIScreenSecondary != null)
		{
			this.UIScreenSecondary.GetComponent<AbstractUIScreen>().setActive(this.active);
		}
		this.activateAudioVoice(this.active);
		if (!this.active)
		{
			return;
		}
		if (!this.isMenu && (this.HasBadEnd || this.HasGoodEnd))
		{
			UIControl.self.showCompletionLine();
			UIControl.self.startTime(this.rapidFireTime);
			InventoryControl.self.showInventory();
			AnalyticsComponent.PuzzleStarted(base.name);
		}
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x00043BA4 File Offset: 0x00041FA4
	private void activateAudioVoice(bool on)
	{
		AudioVoice[] components = base.GetComponents<AudioVoice>();
		AudioVoice audioVoice = null;
		if (on)
		{
			this.activeAudioVoice = null;
		}
		foreach (AudioVoice audioVoice2 in components)
		{
			if (audioVoice2.enableCondition != ifCondition.IfNoOther)
			{
				audioVoice2.setActive(on);
				if (on && audioVoice2.isActive())
				{
					if (this.activeAudioVoice != null)
					{
						Debug.LogError("PuzzleStats transform can't have 2 or more active AudioVoice: remove one, or change ifCondition/n First: " + this.activeAudioVoice.name + "/n Second: " + audioVoice2.name);
					}
					this.activeAudioVoice = audioVoice2;
				}
			}
			else
			{
				if (!on && audioVoice2.enableCondition == ifCondition.IfNoOther)
				{
					audioVoice2.setActive(on);
				}
				if (audioVoice != null)
				{
					Debug.LogError("There can't be two IfNoOther AudioVoice scripts on the PuzzleStats. Remove one, or change it's ifCondition");
				}
				audioVoice = audioVoice2;
			}
		}
		if (!on)
		{
			return;
		}
		if (audioVoice != null)
		{
			if (this.activeAudioVoice == null)
			{
				audioVoice.setActive(true);
				this.activeAudioVoice = audioVoice;
				if (!audioVoice.isActive())
				{
					this.activeAudioVoice = null;
				}
			}
			else
			{
				audioVoice.setActive(false);
			}
		}
		if (this.activeAudioVoice == null && !this.isMenu && (this.HasBadEnd || this.HasGoodEnd))
		{
			this.activeAudioVoice = Audio.self.transform.GetComponent<AudioVoiceIfNoOther>();
			this.activeAudioVoice.setActive(true);
			Audio.self.transform.GetComponent<AudioVoiceIfNoOther>().setPuzzleStats(this);
		}
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x00043D35 File Offset: 0x00042135
	public bool isClickAllowed(ClickWhileVoice type)
	{
		return this.activeAudioVoice == null || this.activeAudioVoice.isClickAllowed(type);
	}

	// Token: 0x06001D45 RID: 7493 RVA: 0x00043D58 File Offset: 0x00042158
	public void positionUIScreen(Transform screen)
	{
		Vector2 sizeDelta = UIControl.self.GetComponent<RectTransform>().sizeDelta;
		Vector2 vector = Camera.main.WorldToViewportPoint(base.transform.position);
		screen.GetComponent<RectTransform>().anchoredPosition = new Vector2(sizeDelta.x * (vector.x - 0.5f), sizeDelta.y * (vector.y - 0.5f));
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x00043DCC File Offset: 0x000421CC
	public void removePuzzle()
	{
		base.enabled = false;
		if (this.UIScreenCurr != null)
		{
			this.UIScreenCurr.GetComponent<AbstractUIScreen>().removeScreen();
		}
		if (this.UIScreenSecondary != null)
		{
			this.UIScreenSecondary.GetComponent<AbstractUIScreen>().removeScreen();
		}
		if (!this.isMenu && Global.self.packIsScrollable)
		{
			base.gameObject.SetActive(false);
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x00043E54 File Offset: 0x00042254
	public virtual void makePauseMenu()
	{
		global::Console.self.openCloseConsole();
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x00043E60 File Offset: 0x00042260
	public virtual void TimeHasEnded()
	{
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x00043E62 File Offset: 0x00042262
	private void OnValidate()
	{
	}

	// Token: 0x04001BE7 RID: 7143
	[Header("Subtitles")]
	[Tooltip("Show subtitles on this puzzle on the bottom. False - top")]
	public bool subtitlesBottom = true;

	// Token: 0x04001BE8 RID: 7144
	[Tooltip("Shift subtitiles from the current pivot point in the subtitilesBottom direction.")]
	public float subtitlesYShift;

	// Token: 0x04001BE9 RID: 7145
	[Tooltip("How transparent is BG for the subtitles. 0 - default value. [0, 1]")]
	public float subtitlesBGalpha;

	// Token: 0x04001BEA RID: 7146
	[Header("BG Color")]
	[Tooltip("Transform with background pattern")]
	public Transform background;

	// Token: 0x04001BEB RID: 7147
	[HideInInspector]
	public bool active;

	// Token: 0x04001BEC RID: 7148
	[Header("UI for this puzzle")]
	[Tooltip("This is a menu - you can't complete it or exit with ESC")]
	public bool isMenu;

	// Token: 0x04001BED RID: 7149
	[Tooltip("True - load attached UI elements on the start")]
	public bool loadUIOnStart;

	// Token: 0x04001BEE RID: 7150
	[Tooltip("UI element atached to this puzzle. If null - assigned randomly")]
	public Transform UIScreen;

	// Token: 0x04001BEF RID: 7151
	[HideInInspector]
	public Transform UIScreenCurr;

	// Token: 0x04001BF0 RID: 7152
	[HideInInspector]
	public Transform UIScreenSecondary;

	// Token: 0x04001BF1 RID: 7153
	[HideInInspector]
	public AudioVoice activeAudioVoice;

	// Token: 0x04001BF2 RID: 7154
	[Header("Win Options")]
	[Tooltip("If puzzle can be solved as Monster")]
	public bool HasBadEnd = true;

	// Token: 0x04001BF3 RID: 7155
	[Tooltip("If puzzle can be solved Good")]
	public bool HasGoodEnd;

	// Token: 0x04001BF4 RID: 7156
	[HideInInspector]
	public bool? solvedAsBad;

	// Token: 0x04001BF5 RID: 7157
	[Header("Timed puzzle")]
	public bool goBadAfterTime = true;

	// Token: 0x04001BF6 RID: 7158
	[Tooltip("How long this puzzle will be showen in Rapid Fire. 0 - default")]
	public float rapidFireTime;

	// Token: 0x04001BF7 RID: 7159
	[Tooltip("Whether or not timeline controller is allowed to end the level")]
	public bool doNotEndRapidFire;

	// Token: 0x04001BF8 RID: 7160
	[Header("Jig Saw Pieces")]
	public bool hasJigSawPieces;

	// Token: 0x04001BF9 RID: 7161
	[HideInInspector]
	public JigSaw_piece jigSawPieceOnPuzzle;
}
