using System;
using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000589 RID: 1417
public class SpriteBlinking : MonoBehaviour
{
	// Token: 0x06002097 RID: 8343 RVA: 0x000A0204 File Offset: 0x0009E604
	private void Start()
	{
		bool flag = false;
		if (base.GetComponent<Image>())
		{
			this.image = base.GetComponent<Image>();
			base.StartCoroutine(this.blink());
			flag = true;
		}
		if (base.GetComponent<SpriteRenderer>())
		{
			this.sprite = base.GetComponent<SpriteRenderer>();
			if (!flag)
			{
				base.StartCoroutine(this.blink());
			}
		}
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x000A0270 File Offset: 0x0009E670
	private IEnumerator blink()
	{
		while (base.enabled)
		{
			if (this.image != null)
			{
				this.image.enabled = true;
			}
			if (this.sprite != null)
			{
				this.sprite.enabled = true;
			}
			if (!string.IsNullOrEmpty(this.blinkSound))
			{
				Audio.self.playOneShot(this.blinkSound, 1f);
			}
			yield return new WaitForSeconds(this.waitTime);
			if (this.image != null)
			{
				this.image.enabled = false;
			}
			if (this.sprite != null)
			{
				this.sprite.enabled = false;
			}
			yield return new WaitForSeconds(this.waitTime);
		}
		yield break;
	}

	// Token: 0x040023EF RID: 9199
	public float waitTime = 1f;

	// Token: 0x040023F0 RID: 9200
	private Image image;

	// Token: 0x040023F1 RID: 9201
	private SpriteRenderer sprite;

	// Token: 0x040023F2 RID: 9202
	[EventRef]
	public string blinkSound;
}
