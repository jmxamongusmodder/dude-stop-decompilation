using System;

namespace Steamworks
{
	// Token: 0x02000213 RID: 531
	[Flags]
	public enum EMarketingMessageFlags
	{
		// Token: 0x04000B71 RID: 2929
		k_EMarketingMessageFlagsNone = 0,
		// Token: 0x04000B72 RID: 2930
		k_EMarketingMessageFlagsHighPriority = 1,
		// Token: 0x04000B73 RID: 2931
		k_EMarketingMessageFlagsPlatformWindows = 2,
		// Token: 0x04000B74 RID: 2932
		k_EMarketingMessageFlagsPlatformMac = 4,
		// Token: 0x04000B75 RID: 2933
		k_EMarketingMessageFlagsPlatformLinux = 8,
		// Token: 0x04000B76 RID: 2934
		k_EMarketingMessageFlagsPlatformRestrictions = 14
	}
}
