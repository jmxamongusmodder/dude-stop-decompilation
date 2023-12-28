using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200054D RID: 1357
public class GhostMarker : MonoBehaviour
{
	// Token: 0x06001F22 RID: 7970 RVA: 0x0009426D File Offset: 0x0009266D
	private void Awake()
	{
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x00094270 File Offset: 0x00092670
	private IEnumerator MakeGhost()
	{
		yield return null;
		while (!Global.self.NoCurrentTransition)
		{
			yield return null;
		}
		yield return null;
		if (Global.self.ghostCountCurrent < Global.self.ghostCountMax && UnityEngine.Random.value > 0.7f && Global.self.cupList[AwardName.Halloween] == CupStatus.Empty)
		{
			UnityEngine.Object.Instantiate<Transform>(this.prefab, base.transform.parent);
			Audio.self.playOneShot("cd4ad629-61c7-4720-882a-abc4fce3ef4c", 1f);
		}
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04002265 RID: 8805
	public Transform prefab;
}
