using System;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public class PuzzleGreenlight : MonoBehaviour
{
	// Token: 0x06001A7A RID: 6778 RVA: 0x0006821E File Offset: 0x0006661E
	private void OnMouseDown()
	{
		if (this.monster)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x00068246 File Offset: 0x00066646
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x0006826F File Offset: 0x0006666F
	private void OnMouseExit()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
	}

	// Token: 0x0400189F RID: 6303
	public float whiten = 0.1f;

	// Token: 0x040018A0 RID: 6304
	public bool monster;
}
