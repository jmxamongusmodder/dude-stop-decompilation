using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class AudioVoice_Pack09 : AudioVoice
{
	// Token: 0x0600122A RID: 4650 RVA: 0x00026558 File Offset: 0x00024958
	private void Awake()
	{
		if ((Global.self.lastPackCompletionState == CompletionState.Monster && Global.self.CountPackPlayedTimes(true, 0) == 1) || (Input.GetKey(KeyCode.LeftAlt) && Global.self.DEBUG))
		{
			this.showDuck = true;
			this.duckLanded = false;
			base.StartCoroutine(this.hideButton(true));
			this.subtPos = base.ps.subtitlesYShift;
			base.ps.subtitlesYShift = 0f;
		}
		else if (Global.self.lastPackCompletionState == CompletionState.Good && Global.self.CountPackPlayedTimes(true, 0) <= 1 && Global.self.CountPackPlayedTimes(false, 0) == 1)
		{
			this.playGoodLine = true;
			base.StartCoroutine(this.hideButton(false));
		}
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x00026630 File Offset: 0x00024A30
	private IEnumerator hideButton(bool hide)
	{
		yield return null;
		if (hide)
		{
			base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startButton.gameObject.SetActive(false);
		}
		base.SetPackStartButton(false);
		yield break;
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x00026654 File Offset: 0x00024A54
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (this.showDuck)
		{
			this.voice = Audio.self.playVoice(this.badEnd);
		}
		else if (this.playGoodLine)
		{
			this.voice = Audio.self.playVoice(this.goodEnd);
		}
		if (this.voice == null)
		{
			return;
		}
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.allowExit = true;
			base.SetPackStartButton(true);
		});
		this.voice.start(true);
		this.allowExit = false;
		this.unlockPack = Global.self.unlockNextPack;
		Global.self.unlockNextPack = false;
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x00026728 File Offset: 0x00024B28
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "showDuck"))
			{
				if (!(markerName == "Exit"))
				{
					if (markerName == "showPopup")
					{
						base.StartCoroutine(this.showDuckText());
					}
				}
				else if (Global.self.CountPackPlayedTimes(true, 0) > 0)
				{
					this.voice.stop();
				}
			}
			else
			{
				this.makeDuck();
			}
		}
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x000267B8 File Offset: 0x00024BB8
	private IEnumerator showDuckText()
	{
		DuckPopup duck = UIControl.self.makePopupDuck(false);
		duck.setDuck(false);
		UIControl.self.StartCoroutine(UIControl.self.killDuckOnTransition(duck));
		string str = WordTranslationContainer.Get(WordTranslationContainer.Theme.CONSOLE, "DUCK_POPUP_PACK09", Global.self.currLanguage);
		yield return UIControl.self.StartCoroutine(duck.setTextSize(str));
		string[] list = str.Split(new char[]
		{
			'\n'
		});
		yield return UIControl.self.StartCoroutine(duck.setOneLine(list[0], new DuckPopupSettings[0]));
		yield return new WaitForSeconds(0.3f);
		yield return UIControl.self.StartCoroutine(duck.addNewLine(list[1], new DuckPopupSettings[0]));
		yield break;
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x000267CC File Offset: 0x00024BCC
	private void makeDuck()
	{
		this.inventoryDuck.SetActive(true);
		this.inventoryDuck.GetComponent<InventoryItem>().moveBackToInventory();
		float x = Camera.main.ViewportToWorldPoint(Vector2.right).x - 0.5f;
		Vector3 v = new Vector3(x, this.destinationY, base.transform.position.z);
		this.inventoryDuck.GetComponent<InventoryDraggable>().inventoryReturnPoint = v;
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x00026850 File Offset: 0x00024C50
	public void duckReached()
	{
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startButton.gameObject.SetActive(true);
		this.duckLanded = true;
		Global.self.unlockNextPack = this.unlockPack;
		base.ps.subtitlesYShift = this.subtPos;
		UIControl.positionSubtitles(null);
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x000268AB File Offset: 0x00024CAB
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (this.allowExit && this.duckLanded)
		{
			base.StartMusic(click);
			return true;
		}
		return false;
	}

	// Token: 0x04000F42 RID: 3906
	public StandaloneLevelVoice badEnd;

	// Token: 0x04000F43 RID: 3907
	public StandaloneLevelVoice goodEnd;

	// Token: 0x04000F44 RID: 3908
	public GameObject inventoryDuck;

	// Token: 0x04000F45 RID: 3909
	public float destinationY;

	// Token: 0x04000F46 RID: 3910
	private bool allowExit = true;

	// Token: 0x04000F47 RID: 3911
	private bool duckLanded = true;

	// Token: 0x04000F48 RID: 3912
	private bool unlockPack;

	// Token: 0x04000F49 RID: 3913
	private bool showDuck;

	// Token: 0x04000F4A RID: 3914
	private bool playGoodLine;

	// Token: 0x04000F4B RID: 3915
	private float subtPos;
}
