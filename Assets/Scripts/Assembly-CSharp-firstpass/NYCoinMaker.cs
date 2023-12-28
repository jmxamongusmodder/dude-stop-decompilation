using System;
using UnityEngine;

// Token: 0x02000561 RID: 1377
public class NYCoinMaker : MonoBehaviour
{
	// Token: 0x06001FA7 RID: 8103 RVA: 0x000984B4 File Offset: 0x000968B4
	private void Awake()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040022CF RID: 8911
	public Transform prefab;

	// Token: 0x040022D0 RID: 8912
	public AwardName linkedAward;
}
