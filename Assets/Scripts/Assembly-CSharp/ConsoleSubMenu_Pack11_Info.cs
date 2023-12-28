using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000532 RID: 1330
public class ConsoleSubMenu_Pack11_Info : ConsoleSubMenu
{
	// Token: 0x06001E81 RID: 7809 RVA: 0x0008D1C8 File Offset: 0x0008B5C8
	public override void setMenu()
	{
		ButtonTemplate[] componentsInChildren = base.GetComponentsInChildren<ButtonTemplate>(true);
		foreach (ButtonTemplate buttonTemplate in componentsInChildren)
		{
			buttonTemplate.callbackMouseOn = new Action<RectTransform>(global::Console.self.mouseOver);
		}
	}

	// Token: 0x06001E82 RID: 7810 RVA: 0x0008D210 File Offset: 0x0008B610
	public override IEnumerator showMenu()
	{
		global::Console.self.hideOldMenu();
		yield return new WaitForSeconds(0.5f);
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform item = (Transform)obj;
				if (item.GetSiblingIndex() == base.transform.childCount - 1)
				{
					yield return new WaitForSeconds(2f);
				}
				item.gameObject.SetActive(true);
				yield return new WaitForSeconds(0.1f);
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
		yield return null;
		yield break;
	}

	// Token: 0x06001E83 RID: 7811 RVA: 0x0008D22B File Offset: 0x0008B62B
	public void bClose()
	{
		global::Console.self.hideConsole();
		Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<Pack11_Intro_2>().onConsoleClose();
	}
}
