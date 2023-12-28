using System;
using UnityEngine;

// Token: 0x020003E4 RID: 996
public class PuzzleCinemaPhone_Cross : MonoBehaviour
{
	// Token: 0x06001922 RID: 6434 RVA: 0x0005BF76 File Offset: 0x0005A376
	private void OnMouseDown()
	{
		this.settingsButton.GetComponent<PuzzleCinemaPhone_Settings>().Click();
	}

	// Token: 0x0400171C RID: 5916
	public Transform settingsButton;
}
