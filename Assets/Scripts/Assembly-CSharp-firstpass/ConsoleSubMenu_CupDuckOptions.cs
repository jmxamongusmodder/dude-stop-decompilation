using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200052E RID: 1326
public class ConsoleSubMenu_CupDuckOptions : ConsoleSubMenu
{
	// Token: 0x06001E6F RID: 7791 RVA: 0x0008B974 File Offset: 0x00089D74
	public override void setMenu()
	{
		ButtonTemplate[] componentsInChildren = base.GetComponentsInChildren<ButtonTemplate>(true);
		foreach (ButtonTemplate buttonTemplate in componentsInChildren)
		{
			buttonTemplate.callbackMouseOn = new Action<RectTransform>(global::Console.self.mouseOver);
			if (this.loadingLine != null)
			{
				buttonTemplate.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x0008B9D8 File Offset: 0x00089DD8
	public override IEnumerator showMenu()
	{
		global::Console.self.hideOldMenu();
		if (this.loadingLine != null)
		{
			this.loadingLine.gameObject.SetActive(true);
			base.StartCoroutine(this.showRestOfTheOption());
		}
		yield return null;
		yield break;
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x0008B9F4 File Offset: 0x00089DF4
	private IEnumerator showRestOfTheOption()
	{
		AudioVoice_CupDuck audio = Global.self.currPuzzle.GetComponent<AudioVoice_CupDuck>();
		while (!audio.showRestOptions)
		{
			yield return null;
		}
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform item = (Transform)obj;
				item.gameObject.SetActive(true);
				yield return new WaitForSeconds(0.03f);
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
		yield break;
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x0008BA0F File Offset: 0x00089E0F
	public void bDuckOptions()
	{
		Global.self.currPuzzle.GetComponent<AudioVoice_CupDuck>().clickDuckOptions();
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x0008BA25 File Offset: 0x00089E25
	public void bDestroyDuck()
	{
		Global.self.currPuzzle.GetComponent<AudioVoice_CupDuck>().clickDestroyDuck();
	}

	// Token: 0x040021BF RID: 8639
	public Transform loadingLine;
}
