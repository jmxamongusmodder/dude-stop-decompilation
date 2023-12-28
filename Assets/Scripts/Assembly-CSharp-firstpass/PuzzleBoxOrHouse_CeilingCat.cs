using System;
using UnityEngine;

// Token: 0x020003CE RID: 974
public class PuzzleBoxOrHouse_CeilingCat : MonoBehaviour
{
	// Token: 0x0600186B RID: 6251 RVA: 0x00055CA7 File Offset: 0x000540A7
	public void StartLongAnimation()
	{
		Audio.self.playOneShot("01f598fc-01c2-4a7b-9997-d3e99f102103", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_CatBoxOrHouse>().showCat();
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x00055CD2 File Offset: 0x000540D2
	public void StartShortAnimation()
	{
		Audio.self.playOneShot("4f6826a2-6fe2-434f-9f62-68f12688909f", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_CatBoxOrHouse>().showCat();
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x00055D00 File Offset: 0x00054100
	public void AnimationEnded()
	{
		this.cat.position = base.transform.position;
		this.cat.gameObject.SetActive(true);
		this.cat.localScale = new Vector2(-1f, 1f);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04001659 RID: 5721
	public Transform cat;
}
