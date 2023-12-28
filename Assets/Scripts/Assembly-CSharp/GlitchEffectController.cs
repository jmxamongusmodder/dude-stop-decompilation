using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class GlitchEffectController : MonoBehaviour
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x0600163D RID: 5693 RVA: 0x00045EC2 File Offset: 0x000442C2
	public static GlitchEffectController self
	{
		get
		{
			if (GlitchEffectController._self == null)
			{
				GlitchEffectController._self = Camera.main.transform.GetComponent<GlitchEffectController>();
			}
			return GlitchEffectController._self;
		}
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x00045EED File Offset: 0x000442ED
	private void Awake()
	{
		this.list = base.GetComponents<GlitchEffect>();
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x00045EFB File Offset: 0x000442FB
	public void startGlitch(float time)
	{
		if (this.currentGlitch != null)
		{
			return;
		}
		this.currentGlitch = base.StartCoroutine(this.makeGlitch(time));
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x00045F1C File Offset: 0x0004431C
	public void startGlitch()
	{
		if (this.currentGlitch != null)
		{
			return;
		}
		this.enableGlitch();
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x00045F30 File Offset: 0x00044330
	public void startGlitch(float time, float fadeInTime)
	{
		if (this.currentGlitch != null)
		{
			return;
		}
		this.currentGlitch = base.StartCoroutine(this.fadeInGlitch(time, fadeInTime));
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x00045F54 File Offset: 0x00044354
	public void stopGlitch()
	{
		if (this.currentGlitch != null)
		{
			base.StopCoroutine(this.currentGlitch);
		}
		this.currentGlitch = null;
		this.list.ToList<GlitchEffect>().ForEach(delegate(GlitchEffect x)
		{
			x.stopEffect();
		});
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x00045FAC File Offset: 0x000443AC
	private IEnumerator fadeInGlitch(float totalTime, float fadeInTime)
	{
		this.enableGlitch();
		this.list.ToList<GlitchEffect>().ForEach(delegate(GlitchEffect x)
		{
			x.displacement = 0f;
		});
		float fade = 0f;
		bool loop = totalTime <= 0f;
		while (fade < fadeInTime)
		{
			float displ = fade / fadeInTime * 0.08f;
			this.list.ToList<GlitchEffect>().ForEach(delegate(GlitchEffect x)
			{
				x.displacement = displ;
			});
			fade += Time.deltaTime;
			totalTime -= Time.deltaTime;
			yield return null;
		}
		while (loop || totalTime > 0f)
		{
			totalTime -= Time.deltaTime;
			yield return null;
		}
		this.list.ToList<GlitchEffect>().ForEach(delegate(GlitchEffect x)
		{
			x.stopEffect();
		});
		this.currentGlitch = null;
		yield break;
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x00045FD8 File Offset: 0x000443D8
	private IEnumerator makeGlitch(float time)
	{
		Audio.self.playOneShot("f0e4627f-43f5-48a2-b313-d32528676246", 1f);
		this.enableGlitch();
		yield return new WaitForSeconds(time);
		this.list.ToList<GlitchEffect>().ForEach(delegate(GlitchEffect x)
		{
			x.stopEffect();
		});
		yield return new WaitForSeconds(0.5f);
		Audio.self.playOneShot("f0e4627f-43f5-48a2-b313-d32528676246", 1f);
		this.enableGlitch();
		yield return new WaitForSeconds(time * 0.5f);
		this.list.ToList<GlitchEffect>().ForEach(delegate(GlitchEffect x)
		{
			x.stopEffect();
		});
		this.currentGlitch = null;
		yield return null;
		yield break;
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x00045FFA File Offset: 0x000443FA
	private void enableGlitch()
	{
		this.list.ToList<GlitchEffect>().ForEach(delegate(GlitchEffect x)
		{
			x.showEach = new Vector2(0f, 0.1f);
			x.showFor = new Vector2(0.01f, 0.6f);
			x.updateEach = 0.02f;
			x.displacement = 0.08f;
			x.shiftSpeedMax = 0.1f;
			x.enabled = true;
		});
	}

	// Token: 0x040013E1 RID: 5089
	private static GlitchEffectController _self;

	// Token: 0x040013E2 RID: 5090
	private GlitchEffect[] list;

	// Token: 0x040013E3 RID: 5091
	private Coroutine currentGlitch;
}
