using System;
using UnityEngine;

// Token: 0x02000453 RID: 1107
public class PuzzleStepOnCracks_Animation : MonoBehaviour
{
	// Token: 0x06001C5F RID: 7263 RVA: 0x00078603 File Offset: 0x00076A03
	public void stepOnCrack()
	{
		this.shoe.cracks.SetPosition(new Vector2(1.1f, -0.4f));
		this.shoe.stepOnCrack();
		Global.self.currPuzzle.GetComponent<AudioVoice_StepOnCracks>().stepOn();
	}

	// Token: 0x06001C60 RID: 7264 RVA: 0x00078643 File Offset: 0x00076A43
	public void endAnimation()
	{
		GlitchEffectController.self.stopGlitch();
	}

	// Token: 0x04001AC3 RID: 6851
	public PuzzleStepOnCracks_Boot shoe;
}
