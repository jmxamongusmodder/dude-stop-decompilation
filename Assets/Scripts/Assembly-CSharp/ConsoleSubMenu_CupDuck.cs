using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200052B RID: 1323
public class ConsoleSubMenu_CupDuck : ConsoleSubMenuMultiple
{
	// Token: 0x06001E65 RID: 7781 RVA: 0x0008AD1C File Offset: 0x0008911C
	protected override IEnumerator showText()
	{
		this.voice = Global.self.currPuzzle.GetComponent<AudioVoice_CupDuck>();
		Vector2 typeSpeed = new Vector2(0.05f, 0.3f);
		yield return base.StartCoroutine(base.typeLine(this.list[0], typeSpeed));
		yield return base.StartCoroutine(base.showResponse(this.list[0]));
		yield return base.StartCoroutine(base.typeLine(this.list[1], typeSpeed));
		yield return base.StartCoroutine(base.showResponse(this.list[1]));
		this.voice.playNextLoad();
		while (this.voice.isVoicePlaying())
		{
			yield return null;
		}
		yield return base.StartCoroutine(base.typeLine(this.list[2], typeSpeed));
		GlitchEffectController.self.startGlitch(0.1f);
		yield return base.StartCoroutine(base.showResponse(this.list[2]));
		GlitchEffectController.self.startGlitch(0.1f);
		yield return new WaitForSeconds(0.5f);
		global::Console.self.defaultInputField.SetActive(false);
		global::Console.self.inputField.gameObject.SetActive(true);
		global::Console.self.inputField.text = string.Empty;
		this.yes = this.list[3].typeLine.text;
		this.voice.playYESLine(new Action<string>(this.markerReached));
		yield break;
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x0008AD38 File Offset: 0x00089138
	private void markerReached(string markerName)
	{
		if (markerName != null)
		{
			if (!(markerName == "type"))
			{
				if (!(markerName == "enter"))
				{
					if (markerName == "show")
					{
						base.StartCoroutine(this.showError());
					}
				}
				else
				{
					this.list[3].typeLine.gameObject.SetActive(true);
					global::Console.self.resetInputField(true);
					Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					Audio.self.playOneShot("ec20c8ca-e43a-408e-ae37-ce64c84e5306", 1f);
					GlitchEffectController.self.startGlitch();
				}
			}
			else
			{
				switch (this.typeIndex)
				{
				case 0:
					global::Console.self.inputField.text = this.yes[0].ToString();
					Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					break;
				case 1:
					global::Console.self.inputField.text = this.yes.Substring(0, this.yes.Length - 1);
					if (this.yes.Length > 2)
					{
						Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					}
					break;
				case 2:
					global::Console.self.inputField.text = this.yes;
					Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					break;
				case 3:
					Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
					break;
				}
				this.typeIndex++;
			}
		}
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x0008AF04 File Offset: 0x00089304
	private IEnumerator showError()
	{
		int index = 0;
		int count = 10;
		for (;;)
		{
			int num;
			index = (num = index) + 1;
			if (num >= count)
			{
				break;
			}
			Transform item = UnityEngine.Object.Instantiate<Transform>(this.list[3].showLines[0].transform);
			item.gameObject.SetActive(true);
			item.SetParent(this.list[3].showLines[0].transform.parent);
			item.localScale = Vector2.one;
			item.SetAsLastSibling();
			yield return new WaitForSeconds(1f / (float)count);
		}
		this.voice.showHideBlackScreen();
		global::Console.self.resetConsole();
		GlitchEffectController.self.stopGlitch();
		yield break;
	}

	// Token: 0x040021B8 RID: 8632
	private string yes = string.Empty;

	// Token: 0x040021B9 RID: 8633
	private int typeIndex;

	// Token: 0x040021BA RID: 8634
	private AudioVoice_CupDuck voice;
}
