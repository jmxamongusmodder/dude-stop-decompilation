using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000555 RID: 1365
public class JigSawUICounter : MonoBehaviour
{
	// Token: 0x06001F47 RID: 8007 RVA: 0x000956A4 File Offset: 0x00093AA4
	private void Awake()
	{
		base.GetComponent<Text>().text = Global.self.unlockedJigsawPieces + "/" + 20;
		if (Global.self.unlockedJigsawPieces != Global.self.jigsawPuzzlePieces.Count)
		{
			this.icon.SetActive(true);
		}
	}

	// Token: 0x04002283 RID: 8835
	public GameObject icon;
}
