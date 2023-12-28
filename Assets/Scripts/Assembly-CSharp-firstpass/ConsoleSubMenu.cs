using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000529 RID: 1321
public abstract class ConsoleSubMenu : MonoBehaviour
{
	// Token: 0x06001E5E RID: 7774 RVA: 0x00089F70 File Offset: 0x00088370
	public virtual void setMenu()
	{
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
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x00089FDC File Offset: 0x000883DC
	public virtual IEnumerator showMenu()
	{
		yield return null;
		yield break;
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x00089FF0 File Offset: 0x000883F0
	public virtual IEnumerator hideConsole()
	{
		yield return null;
		yield break;
	}
}
