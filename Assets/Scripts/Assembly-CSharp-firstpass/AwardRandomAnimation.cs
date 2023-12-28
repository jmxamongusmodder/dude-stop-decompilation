using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032F RID: 815
public class AwardRandomAnimation : MonoBehaviour
{
	// Token: 0x0600141A RID: 5146 RVA: 0x0003366C File Offset: 0x00031A6C
	private void Awake()
	{
		base.GetComponent<Animator>().enabled = false;
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0003367A File Offset: 0x00031A7A
	private void Start()
	{
		base.StartCoroutine(this.startNewAnimation(true));
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x0003368A File Offset: 0x00031A8A
	public void EndAnimation()
	{
		this.particlesOnEnd.Play();
		base.StartCoroutine(this.startNewAnimation(false));
		Audio.self.playOneShot("efaee573-1ea8-4332-bf66-c805f96d14a8", 1f);
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x000336BC File Offset: 0x00031ABC
	private IEnumerator startNewAnimation(bool first = false)
	{
		if (this.repeatCounter <= 0 && UnityEngine.Random.value < this.repeatChance)
		{
			this.repeatCounter = Extensions.RandomInt(this.repeatAmount);
		}
		this.repeatCounter--;
		if (first)
		{
			yield return new WaitForSeconds(this.initialDelay);
		}
		else if (this.repeatCounter > 0)
		{
			yield return new WaitForSeconds(Extensions.Random(this.repeatDelay));
		}
		else
		{
			yield return new WaitForSeconds(Extensions.Random(this.showEach));
		}
		base.GetComponent<Animator>().enabled = true;
		if (base.enabled)
		{
			base.GetComponent<Animator>().SetTrigger("play");
		}
		yield break;
	}

	// Token: 0x04001135 RID: 4405
	public Vector2 showEach = new Vector2(0.5f, 5f);

	// Token: 0x04001136 RID: 4406
	public float repeatChance = 0.1f;

	// Token: 0x04001137 RID: 4407
	public Vector2 repeatAmount = new Vector2(2f, 4f);

	// Token: 0x04001138 RID: 4408
	public Vector2 repeatDelay = new Vector2(0f, 0.5f);

	// Token: 0x04001139 RID: 4409
	private int repeatCounter;

	// Token: 0x0400113A RID: 4410
	public ParticleSystem particlesOnEnd;

	// Token: 0x0400113B RID: 4411
	public float initialDelay = 2f;
}
