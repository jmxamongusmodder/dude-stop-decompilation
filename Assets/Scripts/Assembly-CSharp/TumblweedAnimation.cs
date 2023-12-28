using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200058D RID: 1421
public class TumblweedAnimation : MonoBehaviour
{
	// Token: 0x060020AF RID: 8367 RVA: 0x000A0AFF File Offset: 0x0009EEFF
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.animator.SetTrigger(this.animationName);
		base.StartCoroutine(this.RepeatAnimation());
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x000A0B2B File Offset: 0x0009EF2B
	public void OnAnimationEnd()
	{
		base.StartCoroutine(this.RepeatAnimation());
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x000A0B3C File Offset: 0x0009EF3C
	private IEnumerator RepeatAnimation()
	{
		this.animator.enabled = false;
		yield return new WaitForSeconds(Extensions.Random(this.animationDelay));
		this.animator.enabled = true;
		yield break;
	}

	// Token: 0x04002401 RID: 9217
	private Animator animator;

	// Token: 0x04002402 RID: 9218
	public string animationName;

	// Token: 0x04002403 RID: 9219
	public Vector2 animationDelay;
}
