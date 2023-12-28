using System;
using UnityEngine;

// Token: 0x020003D6 RID: 982
public class PuzzleCatBreaksGlass_Animator : MonoBehaviour
{
	// Token: 0x06001895 RID: 6293 RVA: 0x00056D80 File Offset: 0x00055180
	private void AnimationEnded()
	{
		this.GetComponentInPuzzleStats(true).DropoffFinished();
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x00056D8E File Offset: 0x0005518E
	private void CatHitsFloor()
	{
		Audio.self.playOneShot("3b1d3eea-0fc2-4419-a424-81bbc35db1fa", 1f);
	}
}
