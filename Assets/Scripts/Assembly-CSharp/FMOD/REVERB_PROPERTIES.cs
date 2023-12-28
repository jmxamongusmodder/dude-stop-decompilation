using System;

namespace FMOD
{
	// Token: 0x02000058 RID: 88
	public struct REVERB_PROPERTIES
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00004744 File Offset: 0x00002B44
		public REVERB_PROPERTIES(float decayTime, float earlyDelay, float lateDelay, float hfReference, float hfDecayRatio, float diffusion, float density, float lowShelfFrequency, float lowShelfGain, float highCut, float earlyLateMix, float wetLevel)
		{
			this.DecayTime = decayTime;
			this.EarlyDelay = earlyDelay;
			this.LateDelay = lateDelay;
			this.HFReference = hfReference;
			this.HFDecayRatio = hfDecayRatio;
			this.Diffusion = diffusion;
			this.Density = density;
			this.LowShelfFrequency = lowShelfFrequency;
			this.LowShelfGain = lowShelfGain;
			this.HighCut = highCut;
			this.EarlyLateMix = earlyLateMix;
			this.WetLevel = wetLevel;
		}

		// Token: 0x0400025A RID: 602
		public float DecayTime;

		// Token: 0x0400025B RID: 603
		public float EarlyDelay;

		// Token: 0x0400025C RID: 604
		public float LateDelay;

		// Token: 0x0400025D RID: 605
		public float HFReference;

		// Token: 0x0400025E RID: 606
		public float HFDecayRatio;

		// Token: 0x0400025F RID: 607
		public float Diffusion;

		// Token: 0x04000260 RID: 608
		public float Density;

		// Token: 0x04000261 RID: 609
		public float LowShelfFrequency;

		// Token: 0x04000262 RID: 610
		public float LowShelfGain;

		// Token: 0x04000263 RID: 611
		public float HighCut;

		// Token: 0x04000264 RID: 612
		public float EarlyLateMix;

		// Token: 0x04000265 RID: 613
		public float WetLevel;
	}
}
