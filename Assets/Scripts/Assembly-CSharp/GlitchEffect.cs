using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000386 RID: 902
[AddComponentMenu("Image Effects/GlitchEffect")]
public class GlitchEffect : ImageEffectBase
{
	// Token: 0x06001638 RID: 5688 RVA: 0x00045A98 File Offset: 0x00043E98
	private void OnEnable()
	{
		this.isPlaying = true;
		this.updateEachCurr = 0f;
		this.showForCurr = this.showFor.x;
		this.showEachCurr = this.showEach.x;
		base.material.SetTexture("_DispTex", this.displacementMap);
		base.material.SetInt("_X", (!this.vertical) ? 1 : 0);
		base.material.SetInt("_Y", (!this.vertical) ? 0 : 1);
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x00045B34 File Offset: 0x00043F34
	private IEnumerator LateDisable()
	{
		yield return null;
		base.enabled = false;
		yield break;
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x00045B50 File Offset: 0x00043F50
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_MainTex", source);
		if (!this.isPlaying)
		{
			base.material.SetFloat("displace", 0f);
			Graphics.Blit(source, destination, base.material);
			base.StartCoroutine(this.LateDisable());
			return;
		}
		if (this.showForCurr > 0f)
		{
			this.showForCurr -= Time.deltaTime;
			this.updateEachCurr -= Time.deltaTime;
			if (this.updateEachCurr <= 0f)
			{
				base.material.SetFloat("displace", UnityEngine.Random.Range(-this.displacement, this.displacement));
				base.material.SetFloat("scale", this.scaleCurr * UnityEngine.Random.Range(1f - this.scaleShift, 1f + this.scaleShift));
				base.material.SetFloat("originalAlpha", UnityEngine.Random.Range(0.3f, 1f));
				int num = UnityEngine.Random.Range(0, 3);
				base.material.SetColor("addColor", new Color((num != 0) ? 0f : (UnityEngine.Random.value * this.colorMaxAdd), (num != 1) ? 0f : (UnityEngine.Random.value * this.colorMaxAdd), (num != 2) ? 0f : (UnityEngine.Random.value * this.colorMaxAdd)));
				this.shiftDistCurr += this.shiftSpeedCurr;
				base.material.SetFloat("shift", this.shiftRndDist + this.shiftDistCurr);
				this.updateEachCurr = this.updateEach;
			}
			if (this.showForCurr <= 0f)
			{
				this.showEachCurr = UnityEngine.Random.Range(this.showEach.x, this.showEach.y);
				this.scaleCurr = UnityEngine.Random.Range(this.scale.x, this.scale.y);
				this.shiftRndDist = UnityEngine.Random.value * 10f;
				this.shiftSpeedCurr = UnityEngine.Random.Range(-this.shiftSpeedMax, this.shiftSpeedMax) * this.scaleCurr;
				this.shiftDistCurr = 0f;
				base.material.SetFloat("displace", 0f);
			}
		}
		if (this.showEachCurr > 0f && this.showForCurr <= 0f)
		{
			this.showEachCurr -= Time.deltaTime;
			if (this.showEachCurr <= 0f)
			{
				this.showForCurr = UnityEngine.Random.Range(this.showFor.x, this.showFor.y);
			}
		}
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x00045E23 File Offset: 0x00044223
	public void stopEffect()
	{
		this.isPlaying = false;
	}

	// Token: 0x040013CF RID: 5071
	public Texture2D displacementMap;

	// Token: 0x040013D0 RID: 5072
	[Tooltip("If true - effect will be vertical")]
	public bool vertical;

	// Token: 0x040013D1 RID: 5073
	[Space(10f)]
	[Tooltip("Wait this long between each displacement event")]
	public Vector2 showEach = new Vector2(0.2f, 1.5f);

	// Token: 0x040013D2 RID: 5074
	private float showEachCurr;

	// Token: 0x040013D3 RID: 5075
	[Tooltip("How long to show displacement event")]
	public Vector2 showFor = new Vector2(0.01f, 0.6f);

	// Token: 0x040013D4 RID: 5076
	private float showForCurr = 0.1f;

	// Token: 0x040013D5 RID: 5077
	[Tooltip("Update displacement event each this much seconds")]
	public float updateEach = 0.05f;

	// Token: 0x040013D6 RID: 5078
	private float updateEachCurr;

	// Token: 0x040013D7 RID: 5079
	[Space(10f)]
	[Tooltip("How big to scale displacement texture")]
	public Vector2 scale = new Vector2(0.01f, 0.4f);

	// Token: 0x040013D8 RID: 5080
	[Tooltip("While displacement is going - change scale this much on each update (%)")]
	public float scaleShift = 0.1f;

	// Token: 0x040013D9 RID: 5081
	private float scaleCurr;

	// Token: 0x040013DA RID: 5082
	[Space(10f)]
	[Tooltip("How far to shift with each update")]
	public float displacement = 0.03f;

	// Token: 0x040013DB RID: 5083
	[Space(10f)]
	[Tooltip("How fast to move displacement event")]
	public float shiftSpeedMax = 0.1f;

	// Token: 0x040013DC RID: 5084
	private float shiftRndDist;

	// Token: 0x040013DD RID: 5085
	private float shiftDistCurr;

	// Token: 0x040013DE RID: 5086
	private float shiftSpeedCurr = 0.1f;

	// Token: 0x040013DF RID: 5087
	[Space(10f)]
	[Tooltip("How much color to shift on each displacement")]
	public float colorMaxAdd = 0.3f;

	// Token: 0x040013E0 RID: 5088
	private bool isPlaying = true;
}
