using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class PuzzleLowBattery_Popup : MonoBehaviour
{
	// Token: 0x06001B3C RID: 6972 RVA: 0x0006F496 File Offset: 0x0006D896
	private void OnMouseDown()
	{
		base.StartCoroutine(this.DisappearingCoroutine());
	}

	// Token: 0x06001B3D RID: 6973 RVA: 0x0006F4A8 File Offset: 0x0006D8A8
	private IEnumerator DisappearingCoroutine()
	{
		base.GetComponent<Collider2D>().enabled = false;
		float popupAlphaTime = this.GetComponentInPuzzleStats<PuzzleLowBattery_Monitor>().popupAlphaTime;
		Dictionary<SpriteRenderer, float> alphas = new Dictionary<SpriteRenderer, float>();
		SpriteRenderer[] sprites = base.transform.GetComponentsInChildren<SpriteRenderer>(true);
		CanvasGroup group = base.transform.GetComponentInChildren<CanvasGroup>();
		foreach (SpriteRenderer spriteRenderer in sprites)
		{
			alphas.Add(spriteRenderer, spriteRenderer.color.a);
		}
		float timer = popupAlphaTime;
		while (timer != 0f)
		{
			timer = Mathf.MoveTowards(timer, 0f, Time.deltaTime);
			float t = Mathf.Sin(timer / popupAlphaTime * 3.1415927f * 0.5f);
			foreach (SpriteRenderer spriteRenderer2 in sprites)
			{
				Color color = spriteRenderer2.color;
				color.a = t * alphas[spriteRenderer2];
				spriteRenderer2.color = color;
			}
			group.alpha = t;
			yield return null;
		}
		yield break;
	}
}
