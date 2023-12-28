using System;
using UnityEngine;

// Token: 0x02000401 RID: 1025
public class PuzzleExam_StoryBoard : MonoBehaviour
{
	// Token: 0x06001A08 RID: 6664 RVA: 0x00064425 File Offset: 0x00062825
	public void showSlide(int ind)
	{
		this.slides[ind].gameObject.SetActive(true);
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x0006443A File Offset: 0x0006283A
	public void startCrossing()
	{
		this.animator.SetActive(true);
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x00064448 File Offset: 0x00062848
	public void showExam()
	{
		this.animator.SetActive(false);
		Transform uiscreenCurr = this.GetPuzzleStats().UIScreenCurr;
		uiscreenCurr.gameObject.SetActive(true);
		uiscreenCurr.GetComponent<examPackUI>().enabled = false;
		uiscreenCurr.GetComponent<Animator>().enabled = true;
		Audio.self.playOneShot("be6c6262-e4c9-4b2f-9124-0d19c1a0a4ad", 1f);
	}

	// Token: 0x04001815 RID: 6165
	public Transform[] slides;

	// Token: 0x04001816 RID: 6166
	public GameObject animator;
}
