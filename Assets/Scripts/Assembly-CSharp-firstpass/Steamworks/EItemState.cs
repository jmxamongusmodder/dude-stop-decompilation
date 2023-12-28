using System;

namespace Steamworks
{
	// Token: 0x020001F7 RID: 503
	[Flags]
	public enum EItemState
	{
		// Token: 0x04000A25 RID: 2597
		k_EItemStateNone = 0,
		// Token: 0x04000A26 RID: 2598
		k_EItemStateSubscribed = 1,
		// Token: 0x04000A27 RID: 2599
		k_EItemStateLegacyItem = 2,
		// Token: 0x04000A28 RID: 2600
		k_EItemStateInstalled = 4,
		// Token: 0x04000A29 RID: 2601
		k_EItemStateNeedsUpdate = 8,
		// Token: 0x04000A2A RID: 2602
		k_EItemStateDownloading = 16,
		// Token: 0x04000A2B RID: 2603
		k_EItemStateDownloadPending = 32
	}
}
