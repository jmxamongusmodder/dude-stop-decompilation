using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

// Token: 0x020003A5 RID: 933
public class ParticleTrigger : MonoBehaviour
{
	// Token: 0x06001725 RID: 5925 RVA: 0x0004C771 File Offset: 0x0004AB71
	private void Awake()
	{
		this.ps = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x0004C780 File Offset: 0x0004AB80
	private void OnParticleTrigger()
	{
		int triggerParticles = this.ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, this.enter);
		for (int i = 0; i < triggerParticles; i++)
		{
			Audio.self.playOneShot(this.sound, 1f);
		}
	}

	// Token: 0x040014EA RID: 5354
	[EventRef]
	public string sound;

	// Token: 0x040014EB RID: 5355
	private ParticleSystem ps;

	// Token: 0x040014EC RID: 5356
	private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
}
