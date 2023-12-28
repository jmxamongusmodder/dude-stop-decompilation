using System;
using UnityEngine;

// Token: 0x0200042D RID: 1069
public class PuzzleMNM_CandyPhysicsSound : PhysicsSound
{
	// Token: 0x06001B3F RID: 6975 RVA: 0x0006F6F0 File Offset: 0x0006DAF0
	protected override void PlayHit(Collision2D obj, float volume)
	{
		if (obj.transform.tag == "SuccessCollider" || obj.transform.tag == "FailCollider")
		{
			Audio.self.playOneShot("e0df9923-45ab-4a14-92ea-8b70f86a11b1", volume);
		}
		else if (obj.transform.tag == "GlobalCollider")
		{
			Audio.self.playOneShot("3b32a7d2-6ca2-4021-8bfb-af3baebb95e6", volume);
		}
	}
}
