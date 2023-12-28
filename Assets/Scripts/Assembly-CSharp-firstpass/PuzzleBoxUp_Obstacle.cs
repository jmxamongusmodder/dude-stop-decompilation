using System;
using UnityEngine;

// Token: 0x020003D1 RID: 977
public class PuzzleBoxUp_Obstacle : MonoBehaviour
{
	// Token: 0x0600187B RID: 6267 RVA: 0x0005642B File Offset: 0x0005482B
	private void Awake()
	{
		if (SerializablePackSavedStats.Get(Global.self.currentLevelPack).jigSawPiecesFound > 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x00056452 File Offset: 0x00054852
	private void OnMouseDown()
	{
		this.GetComponentInPuzzleStats<JigSaw_piece>().OnMouseDown();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
