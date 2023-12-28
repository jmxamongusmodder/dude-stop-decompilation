using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200046E RID: 1134
public class PuzzleWrongDress_Checkmark : MonoBehaviour
{
	// Token: 0x06001D1F RID: 7455 RVA: 0x0007F83F File Offset: 0x0007DC3F
	private void Start()
	{
		this.sprite = base.GetComponent<SpriteRenderer>();
		base.StartCoroutine(this.ShowingCoroutine());
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x0007F85C File Offset: 0x0007DC5C
	private IEnumerator ShowingCoroutine()
	{
		yield return null;
		float targetAlpha = 0f;
		bool shown = false;
		for (;;)
		{
			while (shown == this.shown)
			{
				yield return null;
			}
			shown = this.shown;
			targetAlpha = (float)((!shown) ? 0 : 1);
			while (shown == this.shown)
			{
				Color c = this.sprite.color;
				c.a = Mathf.MoveTowards(c.a, targetAlpha, this.appearSpeed * Time.deltaTime);
				this.sprite.color = c;
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x04001BC8 RID: 7112
	public bool shown;

	// Token: 0x04001BC9 RID: 7113
	public float appearSpeed = 150f;

	// Token: 0x04001BCA RID: 7114
	private SpriteRenderer sprite;
}
