using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class Background : MonoBehaviour
{
	// Token: 0x06001445 RID: 5189 RVA: 0x00034694 File Offset: 0x00032A94
	private void OnValidate()
	{
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x000346A4 File Offset: 0x00032AA4
	private void setDelay(bool dark, List<int> list, float interval)
	{
		if (this.timeBetweenLayers != 0f)
		{
			interval = this.timeBetweenLayers;
		}
		for (int i = 0; i < list.Count; i++)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					BackgroundLines component = transform.GetComponent<BackgroundLines>();
					if (component.order == list[i] && dark == component.colorAlter < 0f)
					{
						component.delayShow = (float)i * interval + component.customShowDelay;
						component.delayHide = Mathf.Max(0f, (float)(list.Count - i - 1) * interval + component.customHideDelay);
						if (dark == this.delayLayers < 0f)
						{
							component.delayShow += Mathf.Abs(this.delayLayers);
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
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x000347C0 File Offset: 0x00032BC0
	private List<int> removeRepeats(List<int> list)
	{
		int i = 1;
		while (i < list.Count)
		{
			if (list[i] == list[i - 1])
			{
				list.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
		return list;
	}

	// Token: 0x04001176 RID: 4470
	[Tooltip("Just a temp to force OnValidate function to trigger")]
	public bool TEMP;

	// Token: 0x04001177 RID: 4471
	[Tooltip("Color of the dackground")]
	public Color defaultColor;

	// Token: 0x04001178 RID: 4472
	[Tooltip("Color of the bright lines")]
	public Color brightColor;

	// Token: 0x04001179 RID: 4473
	[Tooltip("Color of the dark lines")]
	public Color darkColor;

	// Token: 0x0400117A RID: 4474
	[Tooltip("Custom time to wait between layers scaling. Default is in BackgroundControl")]
	[Range(0f, 0.25f)]
	public float timeBetweenLayers;

	// Token: 0x0400117B RID: 4475
	[Tooltip("Left side is to play dark parts after bright. Right side to delay bright lines and play them after dark")]
	[Range(-2f, 2f)]
	public float delayLayers;
}
