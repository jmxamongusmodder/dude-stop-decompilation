using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000531 RID: 1329
public class ConsoleSubMenu_Pack11_File : ConsoleSubMenu
{
	// Token: 0x06001E7D RID: 7805 RVA: 0x0008CE74 File Offset: 0x0008B274
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

	// Token: 0x06001E7E RID: 7806 RVA: 0x0008CED8 File Offset: 0x0008B2D8
	public override IEnumerator showMenu()
	{
		global::Console.self.hideOldMenu();
		if (this.loadingLine != null)
		{
			this.loadingLine.gameObject.SetActive(true);
			yield return new WaitForSeconds(1f);
			bool[] array = new bool[2];
			array[0] = true;
			bool[] list = array;
			if (AwardController.self != null)
			{
				list = AwardController.self.getSolvedOrder().ToArray();
			}
			int ind = 0;
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform item = (Transform)obj;
					if (!item.gameObject.activeInHierarchy)
					{
						if (ind >= list.Length)
						{
							break;
						}
						bool[] array2 = list;
						int num;
						ind = (num = ind) + 1;
						if (array2[num])
						{
							item.gameObject.SetActive(true);
							yield return new WaitForSeconds(0.1f);
						}
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
		yield return null;
		yield break;
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x0008CEF3 File Offset: 0x0008B2F3
	public void bFile(int index)
	{
		global::Console.self.hideConsole();
		Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<Pack11_Intro_3>().onFile(index);
	}

	// Token: 0x040021C8 RID: 8648
	public Transform loadingLine;
}
