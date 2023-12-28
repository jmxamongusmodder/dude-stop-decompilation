using System;

namespace FMOD.Studio
{
	// Token: 0x020000F6 RID: 246
	public struct COMMAND_INFO
	{
		// Token: 0x040004E1 RID: 1249
		private StringWrapper commandname;

		// Token: 0x040004E2 RID: 1250
		public int parentcommandindex;

		// Token: 0x040004E3 RID: 1251
		public int framenumber;

		// Token: 0x040004E4 RID: 1252
		public float frametime;

		// Token: 0x040004E5 RID: 1253
		public INSTANCETYPE instancetype;

		// Token: 0x040004E6 RID: 1254
		public INSTANCETYPE outputtype;

		// Token: 0x040004E7 RID: 1255
		public uint instancehandle;

		// Token: 0x040004E8 RID: 1256
		public uint outputhandle;
	}
}
