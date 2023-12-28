using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

// Token: 0x0200030C RID: 780
[RequireComponent(typeof(ParticleSystem))]
public class ParticlesBurstSound : MonoBehaviour
{
	// Token: 0x06001377 RID: 4983 RVA: 0x0002F688 File Offset: 0x0002DA88
	private void Awake()
	{
		if (string.IsNullOrEmpty(this.burstSound))
		{
			return;
		}
		ParticleSystem component = base.GetComponent<ParticleSystem>();
		ParticleSystem.EmissionModule emission = component.emission;
		for (int i = 0; i < emission.burstCount; i++)
		{
			this.burstList.Add(emission.GetBurst(i).time);
		}
		this.totalTime = component.main.duration;
		if (this.startIn != 0f)
		{
			component.Stop();
		}
		base.StartCoroutine(this.LoopSunds());
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x0002F720 File Offset: 0x0002DB20
	private IEnumerator LoopSunds()
	{
		if (this.startIn != 0f)
		{
			yield return new WaitForSeconds(this.startIn);
			ParticleSystem particles = base.GetComponent<ParticleSystem>();
			particles.Play();
		}
		int ind = 0;
		float time = 0f;
		float lastPlayed = -1f;
		while (Global.self.NoCurrentTransition)
		{
			time += Time.deltaTime;
			if (ind < this.burstList.Count && time >= this.burstList[ind] && lastPlayed != this.burstList[ind])
			{
				Audio.self.playOneShot(this.burstSound, 1f);
				lastPlayed = this.burstList[ind];
				ind++;
			}
			if (time > this.totalTime)
			{
				time = 0f;
				ind = 0;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400104D RID: 4173
	[Tooltip("Use this when script is OFF from the start and can't count timer, but Particles are ON from the start. Add delay to match them.")]
	public float startIn;

	// Token: 0x0400104E RID: 4174
	[EventRef]
	public string burstSound;

	// Token: 0x0400104F RID: 4175
	private List<float> burstList = new List<float>();

	// Token: 0x04001050 RID: 4176
	private float totalTime;
}
