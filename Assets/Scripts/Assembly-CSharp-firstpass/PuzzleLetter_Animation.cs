using System;
using UnityEngine;

// Token: 0x0200042A RID: 1066
public class PuzzleLetter_Animation : MonoBehaviour
{
	// Token: 0x06001B30 RID: 6960 RVA: 0x0006ED9D File Offset: 0x0006D19D
	private void Awake()
	{
		this.GetPuzzleStats().GetComponent<AudioVoice_LongCup>().mailObject = base.gameObject;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x0006EDC1 File Offset: 0x0006D1C1
	public void endAnimation()
	{
		base.gameObject.SetActive(false);
		this.letter.SetActive(true);
	}

	// Token: 0x0400196A RID: 6506
	public GameObject letter;
}
