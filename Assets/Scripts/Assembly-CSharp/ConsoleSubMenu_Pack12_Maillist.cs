using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000534 RID: 1332
public class ConsoleSubMenu_Pack12_Maillist : ConsoleSubMenu
{
	// Token: 0x06001E89 RID: 7817 RVA: 0x0008D764 File Offset: 0x0008BB64
	public override void setMenu()
	{
		ButtonTemplate[] componentsInChildren = base.GetComponentsInChildren<ButtonTemplate>(true);
		foreach (ButtonTemplate buttonTemplate in componentsInChildren)
		{
			buttonTemplate.callbackMouseOn = new Action<RectTransform>(global::Console.self.mouseOver);
		}
	}

	// Token: 0x06001E8A RID: 7818 RVA: 0x0008D7AC File Offset: 0x0008BBAC
	public override IEnumerator showMenu()
	{
		global::Console.self.hideOldMenu();
		yield return new WaitForSeconds(2f);
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform item = (Transform)obj;
				if (item.GetSiblingIndex() == base.transform.childCount - 1)
				{
					break;
				}
				if (!item.gameObject.activeInHierarchy)
				{
					item.gameObject.SetActive(true);
					yield return new WaitForSeconds(0.5f);
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
		AudioVoice_Pack12_Duck voice = Global.self.currPuzzle.GetComponent<AudioVoice_Pack12_Duck>();
		while (!voice.showClose)
		{
			yield return null;
		}
		base.transform.GetChild(base.transform.childCount - 1).gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06001E8B RID: 7819 RVA: 0x0008D7C7 File Offset: 0x0008BBC7
	public void bClose()
	{
		global::Console.self.hideConsole();
		Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<Pack12_Duck>().onConsoleClose();
	}
}
