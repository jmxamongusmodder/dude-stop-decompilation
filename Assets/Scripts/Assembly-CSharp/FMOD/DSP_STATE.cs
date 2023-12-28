using System;

namespace FMOD
{
	// Token: 0x020000A9 RID: 169
	public struct DSP_STATE
	{
		// Token: 0x0400032A RID: 810
		public IntPtr instance;

		// Token: 0x0400032B RID: 811
		public IntPtr plugindata;

		// Token: 0x0400032C RID: 812
		public uint channelmask;

		// Token: 0x0400032D RID: 813
		public int source_speakermode;

		// Token: 0x0400032E RID: 814
		public IntPtr sidechaindata;

		// Token: 0x0400032F RID: 815
		public int sidechainchannels;

		// Token: 0x04000330 RID: 816
		public IntPtr functions;

		// Token: 0x04000331 RID: 817
		public int systemobject;
	}
}
