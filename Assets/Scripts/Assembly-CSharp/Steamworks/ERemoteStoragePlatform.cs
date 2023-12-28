using System;

namespace Steamworks
{
	// Token: 0x020001E9 RID: 489
	[Flags]
	public enum ERemoteStoragePlatform
	{
		// Token: 0x040009AC RID: 2476
		k_ERemoteStoragePlatformNone = 0,
		// Token: 0x040009AD RID: 2477
		k_ERemoteStoragePlatformWindows = 1,
		// Token: 0x040009AE RID: 2478
		k_ERemoteStoragePlatformOSX = 2,
		// Token: 0x040009AF RID: 2479
		k_ERemoteStoragePlatformPS3 = 4,
		// Token: 0x040009B0 RID: 2480
		k_ERemoteStoragePlatformLinux = 8,
		// Token: 0x040009B1 RID: 2481
		k_ERemoteStoragePlatformReserved2 = 16,
		// Token: 0x040009B2 RID: 2482
		k_ERemoteStoragePlatformAll = -1
	}
}
