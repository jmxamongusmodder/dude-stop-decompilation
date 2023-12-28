using System;

namespace FMOD
{
	// Token: 0x02000054 RID: 84
	public struct TAG
	{
		// Token: 0x04000225 RID: 549
		public TAGTYPE type;

		// Token: 0x04000226 RID: 550
		public TAGDATATYPE datatype;

		// Token: 0x04000227 RID: 551
		public StringWrapper name;

		// Token: 0x04000228 RID: 552
		public IntPtr data;

		// Token: 0x04000229 RID: 553
		public uint datalen;

		// Token: 0x0400022A RID: 554
		public bool updated;
	}
}
