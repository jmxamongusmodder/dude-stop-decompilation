using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public class PuzzleGreenlightSlide : MonoBehaviour
{
	// Token: 0x06001A7E RID: 6782 RVA: 0x000682A0 File Offset: 0x000666A0
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		IEnumerator enumerator = this.slide.parent.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<SpriteRenderer>().sortingOrder = 0;
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
		this.slide.GetComponent<SpriteRenderer>().sortingOrder = 1;
	}

	// Token: 0x040018A1 RID: 6305
	public Transform slide;
}
