using System;

namespace Steamworks
{
	// Token: 0x02000209 RID: 521
	public enum EAuthSessionResponse
	{
		// Token: 0x04000B01 RID: 2817
		k_EAuthSessionResponseOK,
		// Token: 0x04000B02 RID: 2818
		k_EAuthSessionResponseUserNotConnectedToSteam,
		// Token: 0x04000B03 RID: 2819
		k_EAuthSessionResponseNoLicenseOrExpired,
		// Token: 0x04000B04 RID: 2820
		k_EAuthSessionResponseVACBanned,
		// Token: 0x04000B05 RID: 2821
		k_EAuthSessionResponseLoggedInElseWhere,
		// Token: 0x04000B06 RID: 2822
		k_EAuthSessionResponseVACCheckTimedOut,
		// Token: 0x04000B07 RID: 2823
		k_EAuthSessionResponseAuthTicketCanceled,
		// Token: 0x04000B08 RID: 2824
		k_EAuthSessionResponseAuthTicketInvalidAlreadyUsed,
		// Token: 0x04000B09 RID: 2825
		k_EAuthSessionResponseAuthTicketInvalid,
		// Token: 0x04000B0A RID: 2826
		k_EAuthSessionResponsePublisherIssuedBan
	}
}
