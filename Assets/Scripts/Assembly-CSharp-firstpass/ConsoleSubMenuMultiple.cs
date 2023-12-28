using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000537 RID: 1335
public class ConsoleSubMenuMultiple : ConsoleSubMenu
{
	// Token: 0x06001E93 RID: 7827 RVA: 0x0008A880 File Offset: 0x00088C80
	public override IEnumerator showMenu()
	{
		base.StartCoroutine(this.showText());
		yield return null;
		yield break;
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x0008A89C File Offset: 0x00088C9C
	protected virtual IEnumerator showText()
	{
		yield return null;
		yield break;
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x0008A8B0 File Offset: 0x00088CB0
	protected IEnumerator typeLine(ConsoleSubMenuButtonList item, Vector2 typeSpeed)
	{
		yield return base.StartCoroutine(global::Console.self.typeCommand(item.typeLine.text, typeSpeed.x, typeSpeed.y));
		global::Console.self.hideOldMenu();
		item.typeLine.gameObject.SetActive(true);
		global::Console.self.resetInputField(false);
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x0008A8DC File Offset: 0x00088CDC
	protected IEnumerator showResponse(ConsoleSubMenuButtonList item)
	{
		foreach (GameObject obj in item.showLines)
		{
			obj.SetActive(true);
			ConsoleSystsemMessage mess = obj.GetComponent<ConsoleSystsemMessage>();
			if (mess != null)
			{
				while (mess.isLoading)
				{
					yield return null;
				}
			}
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.03f));
		}
		yield break;
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x0008A8F8 File Offset: 0x00088CF8
	public override IEnumerator hideConsole()
	{
		yield return null;
		yield break;
	}

	// Token: 0x040021CD RID: 8653
	public ConsoleSubMenuButtonList[] list;
}
