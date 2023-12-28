using System;

namespace Steamworks
{
	// Token: 0x020001D8 RID: 472
	public enum EUserRestriction
	{
		// Token: 0x0400091A RID: 2330
		k_nUserRestrictionNone,
		// Token: 0x0400091B RID: 2331
		k_nUserRestrictionUnknown,
		// Token: 0x0400091C RID: 2332
		k_nUserRestrictionAnyChat,
		// Token: 0x0400091D RID: 2333
		k_nUserRestrictionVoiceChat = 4,
		// Token: 0x0400091E RID: 2334
		k_nUserRestrictionGroupChat = 8,
		// Token: 0x0400091F RID: 2335
		k_nUserRestrictionRating = 16,
		// Token: 0x04000920 RID: 2336
		k_nUserRestrictionGameInvites = 32,
		// Token: 0x04000921 RID: 2337
		k_nUserRestrictionTrading = 64
	}
}
