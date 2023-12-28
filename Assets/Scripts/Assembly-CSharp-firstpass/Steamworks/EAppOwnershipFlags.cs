using System;

namespace Steamworks
{
	// Token: 0x0200020D RID: 525
	[Flags]
	public enum EAppOwnershipFlags
	{
		// Token: 0x04000B23 RID: 2851
		k_EAppOwnershipFlags_None = 0,
		// Token: 0x04000B24 RID: 2852
		k_EAppOwnershipFlags_OwnsLicense = 1,
		// Token: 0x04000B25 RID: 2853
		k_EAppOwnershipFlags_FreeLicense = 2,
		// Token: 0x04000B26 RID: 2854
		k_EAppOwnershipFlags_RegionRestricted = 4,
		// Token: 0x04000B27 RID: 2855
		k_EAppOwnershipFlags_LowViolence = 8,
		// Token: 0x04000B28 RID: 2856
		k_EAppOwnershipFlags_InvalidPlatform = 16,
		// Token: 0x04000B29 RID: 2857
		k_EAppOwnershipFlags_SharedLicense = 32,
		// Token: 0x04000B2A RID: 2858
		k_EAppOwnershipFlags_FreeWeekend = 64,
		// Token: 0x04000B2B RID: 2859
		k_EAppOwnershipFlags_RetailLicense = 128,
		// Token: 0x04000B2C RID: 2860
		k_EAppOwnershipFlags_LicenseLocked = 256,
		// Token: 0x04000B2D RID: 2861
		k_EAppOwnershipFlags_LicensePending = 512,
		// Token: 0x04000B2E RID: 2862
		k_EAppOwnershipFlags_LicenseExpired = 1024,
		// Token: 0x04000B2F RID: 2863
		k_EAppOwnershipFlags_LicensePermanent = 2048,
		// Token: 0x04000B30 RID: 2864
		k_EAppOwnershipFlags_LicenseRecurring = 4096,
		// Token: 0x04000B31 RID: 2865
		k_EAppOwnershipFlags_LicenseCanceled = 8192,
		// Token: 0x04000B32 RID: 2866
		k_EAppOwnershipFlags_AutoGrant = 16384,
		// Token: 0x04000B33 RID: 2867
		k_EAppOwnershipFlags_PendingGift = 32768,
		// Token: 0x04000B34 RID: 2868
		k_EAppOwnershipFlags_RentalNotActivated = 65536,
		// Token: 0x04000B35 RID: 2869
		k_EAppOwnershipFlags_Rental = 131072,
		// Token: 0x04000B36 RID: 2870
		k_EAppOwnershipFlags_SiteLicense = 262144
	}
}
