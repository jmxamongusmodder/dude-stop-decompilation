using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public class PuzzleComicSans_OpenField : MonoBehaviour
{
	// Token: 0x06001962 RID: 6498 RVA: 0x0005E2FC File Offset: 0x0005C6FC
	private void Start()
	{
		IEnumerator enumerator = base.transform.parent.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				this.button = transform.GetComponent<PuzzleComicSans_Open>();
				if (this.button != null)
				{
					break;
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

	// Token: 0x06001963 RID: 6499 RVA: 0x0005E388 File Offset: 0x0005C788
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.button.Click();
	}

	// Token: 0x04001766 RID: 5990
	private PuzzleComicSans_Open button;
}
