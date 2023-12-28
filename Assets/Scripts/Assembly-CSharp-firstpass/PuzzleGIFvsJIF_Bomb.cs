using System;
using UnityEngine;

// Token: 0x0200040F RID: 1039
public class PuzzleGIFvsJIF_Bomb : MonoBehaviour
{
	// Token: 0x06001A60 RID: 6752 RVA: 0x00067523 File Offset: 0x00065923
	private void Start()
	{
		this.bombBeeping = true;
		Audio.self.playLoopSound("54beadf1-8999-4e02-8690-fa04447aca05");
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x0006753B File Offset: 0x0006593B
	private void OnDisable()
	{
		this.DisableBeeping(true);
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x00067544 File Offset: 0x00065944
	public void DisableBeeping(bool fadeOut = false)
	{
		if (!this.bombBeeping)
		{
			return;
		}
		this.bombBeeping = false;
		Audio.self.stopLoopSound("54beadf1-8999-4e02-8690-fa04447aca05", fadeOut);
	}

	// Token: 0x04001876 RID: 6262
	private bool bombBeeping;
}
