using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000533 RID: 1331
public class ConsoleSubMenu_Pack11_Port : ConsoleSubMenu
{
	// Token: 0x06001E85 RID: 7813 RVA: 0x0008D49C File Offset: 0x0008B89C
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

	// Token: 0x06001E86 RID: 7814 RVA: 0x0008D500 File Offset: 0x0008B900
	public override IEnumerator showMenu()
	{
		global::Console.self.hideOldMenu();
		if (this.loadingLine != null)
		{
			this.loadingLine.gameObject.SetActive(true);
			yield return new WaitForSeconds(1f);
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform item = (Transform)obj;
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
		}
		yield return null;
		yield break;
	}

	// Token: 0x06001E87 RID: 7815 RVA: 0x0008D51B File Offset: 0x0008B91B
	public void bSelectPort()
	{
		global::Console.self.hideConsole();
		Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<Pack11_Intro_2>().onConsoleClose();
	}

	// Token: 0x040021C9 RID: 8649
	public Transform loadingLine;
}
