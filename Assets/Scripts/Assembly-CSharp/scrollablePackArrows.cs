using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000585 RID: 1413
public class scrollablePackArrows : AbstractUIScreen
{
	// Token: 0x06002076 RID: 8310 RVA: 0x0009FA7C File Offset: 0x0009DE7C
	private void Awake()
	{
		this.list = new ArrowButtonStats[4];
		this.list[0] = new ArrowButtonStats(this.prevArrow.GetComponent<RectTransform>(), true);
		this.list[1] = new ArrowButtonStats(this.nextArrow.GetComponent<RectTransform>(), false);
		this.list[2] = new ArrowButtonStats(this.firstArrow.GetComponent<RectTransform>(), true);
		this.list[3] = new ArrowButtonStats(this.lastArrow.GetComponent<RectTransform>(), false);
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x0009FAFC File Offset: 0x0009DEFC
	public override void Update()
	{
		base.Update();
		for (int i = 0; i < this.list.Length; i++)
		{
			this.list[i].Update();
		}
		if (this.delayButton > 0f)
		{
			this.delayButton -= Time.deltaTime;
			if (this.delayButton <= 0f)
			{
				this.delayButton = 0f;
			}
		}
		if (this.delayButton == 0f && this.canUseArrows)
		{
			if (this.buttonDelayed)
			{
				this.buttonDelayed = false;
			}
			this.delayButton = -1f;
		}
	}

	// Token: 0x06002078 RID: 8312 RVA: 0x0009FBAA File Offset: 0x0009DFAA
	public override void setActive(bool active)
	{
		base.setActive(active);
	}

	// Token: 0x06002079 RID: 8313 RVA: 0x0009FBB3 File Offset: 0x0009DFB3
	public override void removeScreen()
	{
		if (Global.self.packIsScrollable)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600207A RID: 8314 RVA: 0x0009FBD0 File Offset: 0x0009DFD0
	public override void setScreen(Transform item)
	{
		this.setArrow(true, false, null);
		this.list[0].setButton(false);
		this.list[2].setButton(false);
		this.list[3].setButton(false);
	}

	// Token: 0x0600207B RID: 8315 RVA: 0x0009FC18 File Offset: 0x0009E018
	protected override void cancelPressed()
	{
	}

	// Token: 0x0600207C RID: 8316 RVA: 0x0009FC1C File Offset: 0x0009E01C
	public void setArrow(bool left, bool on, bool? firstOn = null)
	{
		if (left)
		{
			this.list[0].setButton(on);
			this.list[2].setButton((firstOn != null) ? (firstOn == true) : on);
		}
		else
		{
			this.list[1].setButton(on);
			this.list[3].setButton((firstOn != null) ? (firstOn == true) : on);
		}
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x0009FCBC File Offset: 0x0009E0BC
	public void pauseScrolling(float time)
	{
		this.delayButton = time;
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x0009FCC8 File Offset: 0x0009E0C8
	private bool canContinueTransition()
	{
		AudioVoiceScrollable component = Global.self.currPuzzle.GetComponent<AudioVoiceScrollable>();
		return !(component != null) || component.onTransitionOut();
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x0009FCFC File Offset: 0x0009E0FC
	public void bPrev()
	{
		if (!this.checkIfAllowed(new bool?(true), null))
		{
			return;
		}
		if (!this.canContinueTransition())
		{
			return;
		}
		AnalyticsComponent.PuzzleFinished(Global.self.currPuzzle.name);
		Global.TellAnalyticsLevelFinished();
		Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr = null;
		this.setArrow(false, true, null);
		Global.self.gotoNextLevel(true, null);
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x0009FD84 File Offset: 0x0009E184
	public void bFirst()
	{
		if (!this.checkIfAllowed(null, new bool?(true)))
		{
			return;
		}
		if (!this.canContinueTransition())
		{
			return;
		}
		AnalyticsComponent.PuzzleFinished(Global.self.currPuzzle.name);
		Global.TellAnalyticsLevelFinished();
		Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr = null;
		this.setArrow(false, true, null);
		Global.self.gotoNextLevel(true, new bool?(false));
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x0009FE08 File Offset: 0x0009E208
	public void bNext()
	{
		if (!this.checkIfAllowed(new bool?(false), null))
		{
			return;
		}
		if (!this.canContinueTransition())
		{
			return;
		}
		AnalyticsComponent.PuzzleFinished(Global.self.currPuzzle.name);
		Global.TellAnalyticsLevelFinished();
		Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr = null;
		this.setArrow(true, true, null);
		Global.self.gotoNextLevel(false, null);
	}

	// Token: 0x06002082 RID: 8322 RVA: 0x0009FE90 File Offset: 0x0009E290
	public void bLast()
	{
		if (!this.checkIfAllowed(null, new bool?(false)))
		{
			return;
		}
		if (!this.canContinueTransition())
		{
			return;
		}
		AnalyticsComponent.PuzzleFinished(Global.self.currPuzzle.name);
		Global.TellAnalyticsLevelFinished();
		Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr = null;
		this.setArrow(true, true, null);
		Global.self.gotoNextLevel(false, new bool?(true));
	}

	// Token: 0x06002083 RID: 8323 RVA: 0x0009FF14 File Offset: 0x0009E314
	private bool checkIfAllowed(bool? left, bool? first)
	{
		if (!this.active || !Global.self.NoCurrentTransition)
		{
			return false;
		}
		if (InventoryControl.self.transform.childCount > 0)
		{
			IEnumerator enumerator = InventoryControl.self.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.GetComponentInChildren<InventoryItem>() == null)
					{
						return false;
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
		if (Input.GetMouseButton(0))
		{
			return false;
		}
		if (this.delayButton > 0f || !this.canUseArrows)
		{
			if (this.delayButton == -1f)
			{
				this.delayButton = 0f;
			}
			this.buttonDelayed = true;
			return false;
		}
		return true;
	}

	// Token: 0x040023D3 RID: 9171
	public Transform prevArrow;

	// Token: 0x040023D4 RID: 9172
	public Transform nextArrow;

	// Token: 0x040023D5 RID: 9173
	public Transform firstArrow;

	// Token: 0x040023D6 RID: 9174
	public Transform lastArrow;

	// Token: 0x040023D7 RID: 9175
	[HideInInspector]
	public bool canUseArrows = true;

	// Token: 0x040023D8 RID: 9176
	private float delayButton = -1f;

	// Token: 0x040023D9 RID: 9177
	private bool buttonDelayed;

	// Token: 0x040023DA RID: 9178
	private ArrowButtonStats[] list;
}
