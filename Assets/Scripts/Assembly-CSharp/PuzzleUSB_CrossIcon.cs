using System;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class PuzzleUSB_CrossIcon : MonoBehaviour
{
	// Token: 0x06001CD4 RID: 7380 RVA: 0x0007CE94 File Offset: 0x0007B294
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.popup.Close();
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x0007CEAD File Offset: 0x0007B2AD
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		Audio.self.playOneShot("0cdc7488-2657-4b4b-a66d-808b63d1cea1", 1f);
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x0007CEEB File Offset: 0x0007B2EB
	private void OnMouseExit()
	{
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
	}

	// Token: 0x04001B67 RID: 7015
	public PuzzleUSB_Popup popup;

	// Token: 0x04001B68 RID: 7016
	[Range(0f, 1f)]
	public float whiten = 0.15f;
}
