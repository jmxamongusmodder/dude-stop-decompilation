using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class RewardCupContainer : Draggable
{
	// Token: 0x060015AD RID: 5549 RVA: 0x00043318 File Offset: 0x00041718
	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(true);
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
}
