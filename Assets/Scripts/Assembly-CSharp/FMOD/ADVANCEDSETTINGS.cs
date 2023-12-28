using System;

namespace FMOD
{
	// Token: 0x0200005A RID: 90
	public struct ADVANCEDSETTINGS
	{
		// Token: 0x04000266 RID: 614
		public int cbSize;

		// Token: 0x04000267 RID: 615
		public int maxMPEGCodecs;

		// Token: 0x04000268 RID: 616
		public int maxADPCMCodecs;

		// Token: 0x04000269 RID: 617
		public int maxXMACodecs;

		// Token: 0x0400026A RID: 618
		public int maxVorbisCodecs;

		// Token: 0x0400026B RID: 619
		public int maxAT9Codecs;

		// Token: 0x0400026C RID: 620
		public int maxFADPCMCodecs;

		// Token: 0x0400026D RID: 621
		public int maxPCMCodecs;

		// Token: 0x0400026E RID: 622
		public int ASIONumChannels;

		// Token: 0x0400026F RID: 623
		public IntPtr ASIOChannelList;

		// Token: 0x04000270 RID: 624
		public IntPtr ASIOSpeakerList;

		// Token: 0x04000271 RID: 625
		public float HRTFMinAngle;

		// Token: 0x04000272 RID: 626
		public float HRTFMaxAngle;

		// Token: 0x04000273 RID: 627
		public float HRTFFreq;

		// Token: 0x04000274 RID: 628
		public float vol0virtualvol;

		// Token: 0x04000275 RID: 629
		public uint defaultDecodeBufferSize;

		// Token: 0x04000276 RID: 630
		public ushort profilePort;

		// Token: 0x04000277 RID: 631
		public uint geometryMaxFadeTime;

		// Token: 0x04000278 RID: 632
		public float distanceFilterCenterFreq;

		// Token: 0x04000279 RID: 633
		public int reverb3Dinstance;

		// Token: 0x0400027A RID: 634
		public int DSPBufferPoolSize;

		// Token: 0x0400027B RID: 635
		public uint stackSizeStream;

		// Token: 0x0400027C RID: 636
		public uint stackSizeNonBlocking;

		// Token: 0x0400027D RID: 637
		public uint stackSizeMixer;

		// Token: 0x0400027E RID: 638
		public DSP_RESAMPLER resamplerMethod;

		// Token: 0x0400027F RID: 639
		public uint commandQueueSize;

		// Token: 0x04000280 RID: 640
		public uint randomSeed;
	}
}
