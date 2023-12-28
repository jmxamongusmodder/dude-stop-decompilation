using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002AD RID: 685
public class AudioVoice_CupDuck : AudioVoice
{
	// Token: 0x060010C3 RID: 4291 RVA: 0x0001B090 File Offset: 0x00019490
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		global::Console.self.canOpen = false;
		Global.self.DuckInPack07IsActive = false;
		Global.self.DuckEnabled = true;
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_OnLoad);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.openConsole));
		this.voice.start(true);
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x0001B10C File Offset: 0x0001950C
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		switch (markerName)
		{
		case "showOptions":
			this.showRestOptions = true;
			break;
		case "Enable":
			base.StartCoroutine(this.waitForConsole());
			break;
		case "showBlue":
			this.showBlueButton = true;
			break;
		case "showBlack":
			base.StartCoroutine(this.playingIseeScreen());
			break;
		case "rotateCup":
			base.StartCoroutine(this.rotateCup());
			break;
		case "showScreen":
			this.hideQuack = true;
			break;
		case "enableClick":
			GlitchEffectController.self.startGlitch(0.1f);
			this.duckSprite.GetComponent<CupGetWithClick>().active = true;
			break;
		}
		if (this.callbackExist)
		{
			this.callbackYes(markerName);
		}
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x0001B261 File Offset: 0x00019661
	private void openConsole(VoiceLine line)
	{
		global::Console.self.showConsole(global::Console.self.cupDuck);
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0001B277 File Offset: 0x00019677
	public void playNextLoad()
	{
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_OnLoad2);
		this.voice.start(true);
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x0001B29A File Offset: 0x0001969A
	public bool isVoicePlaying()
	{
		return this.voice != null && this.voice.isPlaying();
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0001B2B8 File Offset: 0x000196B8
	public void playYESLine(Action<string> callback)
	{
		VoiceLine voiceLine = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_TypingYes);
		voiceLine.start(true);
		voiceLine.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		voiceLine.subscribeToStopped(this, new Action<VoiceLine>(this.endYesVoice));
		this.callbackYes = callback;
		this.callbackExist = true;
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x0001B311 File Offset: 0x00019711
	private void endYesVoice(VoiceLine line)
	{
		this.callbackExist = false;
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0001B31A File Offset: 0x0001971A
	public void showHideBlackScreen()
	{
		base.StartCoroutine(this.showBlackScreen());
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0001B32C File Offset: 0x0001972C
	private IEnumerator showBlackScreen()
	{
		UIControl.self.hideSubtitles();
		GameObject obj = UIControl.self.makeBlackScreen();
		yield return new WaitForSeconds(0.1f);
		Transform item = UnityEngine.Object.Instantiate<Transform>(this.quackText);
		item.SetParent(UIControl.self.secondCanvas.transform, false);
		yield return new WaitForSeconds(2f);
		UnityEngine.Object.Destroy(item.gameObject);
		yield return new WaitForSeconds(1f);
		GlitchEffectController.self.startGlitch(1f);
		UIControl.self.makeNoConnectionScreen(0f);
		Image img = obj.GetComponent<Image>();
		Color c = img.color;
		bool said = false;
		while (c.a > 0f)
		{
			c.a = Mathf.MoveTowards(c.a, 0f, Time.deltaTime * 5f);
			img.color = c;
			if (c.a < 0.4f && !said)
			{
				said = true;
				this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_WhatHappened);
				this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
				this.voice.start(true);
			}
			yield return null;
		}
		UnityEngine.Object.Destroy(obj);
		yield break;
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0001B348 File Offset: 0x00019748
	private IEnumerator waitForConsole()
	{
		float waitToSay = 5f;
		while (!Input.GetButtonDown("Cancel"))
		{
			if (waitToSay > 0f)
			{
				waitToSay = Mathf.MoveTowards(waitToSay, 0f, Time.deltaTime);
				if (waitToSay == 0f)
				{
					this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_PressESC);
					this.voice.start(true);
					waitToSay = -1f;
				}
			}
			yield return null;
		}
		global::Console.self.showConsole(global::Console.self.cupDuck_DuckOptions);
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_PressedESC);
			this.voice.start(true);
		}
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_PressDuckOptions);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		this.uselessOptionsCount = 0;
		while (this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(8f);
		if (this.uselessOptionsCount == -1)
		{
			while (this.uselessOptionsCount == -1)
			{
				yield return null;
			}
			yield return new WaitForSeconds(2f);
		}
		if (!this.duckOptionsCLicked)
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_WaitDuckOptions);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0001B364 File Offset: 0x00019764
	public void ClickUselessOptionsButton()
	{
		if (this.uselessOptionsCount < 0)
		{
			return;
		}
		this.uselessOptionsCount++;
		if (this.uselessOptionsCount == 3)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.uselessOptionsCount = -1;
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_KeepInMind);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.uselessOptionsCount = -2;
			});
			this.voice.start(true);
		}
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0001B400 File Offset: 0x00019800
	public void clickDuckOptions()
	{
		this.duckOptionsCLicked = true;
		global::Console.self.switchMenu(global::Console.self.cupDuck_DestroyDuck);
		if (this.voice != null && this.voice.isPlaying())
		{
			if (this.uselessOptionsCount == -1)
			{
				return;
			}
			this.voice.stop();
		}
		if (this.uselessOptionsCount < 0)
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_PressDestroyDuck);
		}
		else
		{
			this.uselessOptionsCount = -1;
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_KeepInMind);
		}
		this.voice.start(true);
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0001B4AC File Offset: 0x000198AC
	public void clickDestroyDuck()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		global::Console.self.switchMenu(global::Console.self.cupDuck_BlueButton);
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_PressBlue);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0001B530 File Offset: 0x00019930
	public void clickColorButton()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		global::Console.self.switchMenu(global::Console.self.cupDuck_LastScreen);
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_DidYouDo);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		Audio.self.playOneShot("c83efd47-b889-429f-b8aa-7ed347a52931", 1f);
		GlitchEffectController.self.startGlitch(0f, 2f);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0001B5DC File Offset: 0x000199DC
	private IEnumerator rotateCup()
	{
		float time = 0.2f;
		while (this.voice != null && this.voice.isPlaying())
		{
			time -= 0.05f;
			time = Mathf.Max(time, 0f);
			this.cupSprite.transform.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-20f, 20f));
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f + time, 0.1f + time));
		}
		yield break;
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x0001B5F8 File Offset: 0x000199F8
	private IEnumerator playingIseeScreen()
	{
		GlitchEffectController.self.stopGlitch();
		GameObject obj = UIControl.self.makeBlackScreen();
		while (!this.hideQuack)
		{
			yield return null;
		}
		UIControl.self.hideNoConnectionScreen();
		global::Console.self.resetConsole();
		this.cupSprite.SetActive(false);
		this.duckSprite.SetActive(true);
		UnityEngine.Object.Destroy(obj);
		GlitchEffectController.self.startGlitch(0.5f);
		base.StartCoroutine(this.waitingToRemind());
		yield break;
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x0001B614 File Offset: 0x00019A14
	private IEnumerator waitingToRemind()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		if (this.cupCollected)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Cup_WaitClick);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0001B630 File Offset: 0x00019A30
	public override void subsctibeToEnding(endTextControl item)
	{
		this.cupCollected = true;
		StandaloneLevelVoiceEnd voice = LevelVoice.getVoice(Voices.VoicePack07_Duck.Cup_End, LevelVoice.Type.End, new bool?(true), Global.self.currLanguage);
		if (this.voice != null)
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(voice.entry);
		base.subscribeToMarkers(item, true);
		this.voice.start(true);
		global::Console.self.canOpen = true;
	}

	// Token: 0x04000DCD RID: 3533
	[Space(10f)]
	public GameObject duckSprite;

	// Token: 0x04000DCE RID: 3534
	public GameObject cupSprite;

	// Token: 0x04000DCF RID: 3535
	public Transform quackText;

	// Token: 0x04000DD0 RID: 3536
	private Action<string> callbackYes;

	// Token: 0x04000DD1 RID: 3537
	private bool callbackExist;

	// Token: 0x04000DD2 RID: 3538
	private bool duckOptionsCLicked;

	// Token: 0x04000DD3 RID: 3539
	[HideInInspector]
	public bool showBlueButton;

	// Token: 0x04000DD4 RID: 3540
	private bool hideQuack;

	// Token: 0x04000DD5 RID: 3541
	private bool cupCollected;

	// Token: 0x04000DD6 RID: 3542
	[HideInInspector]
	public bool showRestOptions;

	// Token: 0x04000DD7 RID: 3543
	private int uselessOptionsCount = -1;
}
