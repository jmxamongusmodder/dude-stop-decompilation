using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200052F RID: 1327
public class ConsoleSubMenu_HundredPhotosDuck : ConsoleSubMenuMultiple
{
	// Token: 0x06001E75 RID: 7797 RVA: 0x0008BD08 File Offset: 0x0008A108
	protected override IEnumerator showText()
	{
		this.voice = Global.self.currPuzzle.GetComponent<AudioVoice_HundredPhotos>();
		Vector2 typeSpeed = new Vector2(0.05f, 0.3f);
		while (this.voice.isVoicePlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		this.voice.StartCoroutine(this.voice.playVoice(Voices.VoicePack07_Duck.Hundred_Iwarned1, 0f));
		yield return base.StartCoroutine(base.typeLine(this.list[this.listInd], typeSpeed));
		yield return base.StartCoroutine(base.showResponse(this.list[this.listInd]));
		this.voice.StartCoroutine(this.voice.playVoice(Voices.VoicePack07_Duck.Hundred_Iwarned2, 0f));
		yield return base.StartCoroutine(base.typeLine(this.list[++this.listInd], typeSpeed));
		this.voice.StartCoroutine(this.voice.playVoice(Voices.VoicePack07_Duck.Hundred_Iwarned3, 0f));
		yield return base.StartCoroutine(base.showResponse(this.list[this.listInd]));
		while (this.voice.isVoicePlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		this.voice.StartCoroutine(this.voice.playVoice(Voices.VoicePack07_Duck.Hundred_Iwarned4, 0f));
		yield return base.StartCoroutine(base.typeLine(this.list[++this.listInd], typeSpeed));
		yield return base.StartCoroutine(base.showResponse(this.list[this.listInd]));
		yield return new WaitForSeconds(0.5f);
		global::Console.self.defaultInputField.SetActive(false);
		global::Console.self.inputField.gameObject.SetActive(true);
		global::Console.self.inputField.text = string.Empty;
		this.yes = this.list[++this.listInd].typeLine.text;
		this.typedYes = string.Empty;
		this.voice.playYESLine(new Action<string>(this.markerReached));
		yield break;
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x0008BD24 File Offset: 0x0008A124
	private void markerReached(string markerName)
	{
		if (markerName != null)
		{
			if (!(markerName == "type"))
			{
				if (!(markerName == "remove"))
				{
					if (markerName == "enter")
					{
						this.list[this.listInd].typeLine.gameObject.SetActive(true);
						Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
						base.StartCoroutine(this.glitchCoroutine());
					}
				}
				else
				{
					this.typedYes = this.yes.Substring(0, this.yes.Length - 1);
					global::Console.self.inputField.text = this.typedYes;
					Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
				}
			}
			else
			{
				switch (this.typeIndex)
				{
				case 0:
					this.typedYes = this.yes[0].ToString();
					Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					break;
				case 1:
					this.typedYes = this.yes.Substring(0, this.yes.Length - 1);
					if (this.typedYes.Length > 1)
					{
						Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					}
					break;
				case 2:
					this.typedYes = WordTranslationContainer.Get(WordTranslationContainer.Theme.CONSOLE, "HUNDRED_PHOTOS_DUCK_WRONG_YES", Global.self.currLanguage);
					Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					break;
				case 3:
					this.typedYes = this.yes;
					Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					break;
				}
				global::Console.self.inputField.text = this.typedYes;
				this.typeIndex++;
			}
		}
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x0008BF28 File Offset: 0x0008A328
	private IEnumerator glitchCoroutine()
	{
		Audio.self.playLoopSound("0221e0b9-6db2-4fac-8026-65fe599940ba");
		global::Console.self.resetInputField(false);
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(false);
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
		yield return new WaitForSeconds(0.2f);
		GlitchEffectController.self.startGlitch();
		foreach (GameObject gameObject in this.list[++this.listInd].showLines)
		{
			gameObject.SetActive(true);
		}
		this.duckLoading.value = 0f;
		yield return base.StartCoroutine(this.loadDuck(0.1f, 2f));
		while (this.voice.isVoicePlaying())
		{
			yield return null;
		}
		yield return base.StartCoroutine(this.loadDuck(0.25f, 2f));
		this.voice.StartCoroutine(this.voice.playVoice(Voices.VoicePack07_Duck.Hundred_DontWorry, 0f));
		yield return base.StartCoroutine(this.loadDuck(0.4f, 3f));
		while (this.voice.isVoicePlaying())
		{
			yield return null;
		}
		yield return base.StartCoroutine(this.loadDuck(0.45f, 3f));
		this.voice.StartCoroutine(this.voice.playVoice(Voices.VoicePack07_Duck.Hundred_WhySoMany, 0f));
		yield return base.StartCoroutine(this.loadDuck(0.6f, 3f));
		while (this.voice.isVoicePlaying())
		{
			yield return null;
		}
		yield return base.StartCoroutine(this.loadDuck(0.7f, 3f));
		this.voice.StartCoroutine(this.voice.playVoice(Voices.VoicePack07_Duck.Hundred_DidIBreak, 0f));
		yield return base.StartCoroutine(this.loadDuck(0.8f, 2f));
		while (this.voice.isVoicePlaying())
		{
			yield return null;
		}
		yield return base.StartCoroutine(this.loadDuck(0.9f, 1.5f));
		base.StartCoroutine(this.loadDuck(1f, 1f));
		this.voice.endConsoleAnimation();
		yield break;
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x0008BF44 File Offset: 0x0008A344
	private IEnumerator loadDuck(float amount, float time)
	{
		float currValue = this.duckLoading.value;
		Queue<float> parts = new Queue<float>();
		int count = Mathf.Clamp(Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 5) + time * 2f), 4, 10);
		float min = 1f / (float)count;
		float max = min * 2f;
		min *= 0.5f;
		while (parts.Sum() < 1f)
		{
			parts.Enqueue(UnityEngine.Random.Range(min, max));
		}
		float sum = 0f;
		while (parts.Count > 0)
		{
			float number = parts.Dequeue();
			sum += number;
			this.duckLoading.value = Mathf.Min(currValue + (amount - currValue) * sum, currValue + amount);
			yield return new WaitForSeconds(number * time);
		}
		yield break;
	}

	// Token: 0x040021C0 RID: 8640
	private string yes = string.Empty;

	// Token: 0x040021C1 RID: 8641
	private string typedYes = string.Empty;

	// Token: 0x040021C2 RID: 8642
	private int typeIndex;

	// Token: 0x040021C3 RID: 8643
	private int listInd;

	// Token: 0x040021C4 RID: 8644
	private AudioVoice_HundredPhotos voice;

	// Token: 0x040021C5 RID: 8645
	public Slider duckLoading;
}
