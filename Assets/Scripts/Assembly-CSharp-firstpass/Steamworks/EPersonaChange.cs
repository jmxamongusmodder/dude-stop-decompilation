using System;

namespace Steamworks
{
	// Token: 0x020001DA RID: 474
	[Flags]
	public enum EPersonaChange
	{
		// Token: 0x04000927 RID: 2343
		k_EPersonaChangeName = 1,
		// Token: 0x04000928 RID: 2344
		k_EPersonaChangeStatus = 2,
		// Token: 0x04000929 RID: 2345
		k_EPersonaChangeComeOnline = 4,
		// Token: 0x0400092A RID: 2346
		k_EPersonaChangeGoneOffline = 8,
		// Token: 0x0400092B RID: 2347
		k_EPersonaChangeGamePlayed = 16,
		// Token: 0x0400092C RID: 2348
		k_EPersonaChangeGameServer = 32,
		// Token: 0x0400092D RID: 2349
		k_EPersonaChangeAvatar = 64,
		// Token: 0x0400092E RID: 2350
		k_EPersonaChangeJoinedSource = 128,
		// Token: 0x0400092F RID: 2351
		k_EPersonaChangeLeftSource = 256,
		// Token: 0x04000930 RID: 2352
		k_EPersonaChangeRelationshipChanged = 512,
		// Token: 0x04000931 RID: 2353
		k_EPersonaChangeNameFirstSet = 1024,
		// Token: 0x04000932 RID: 2354
		k_EPersonaChangeFacebookInfo = 2048,
		// Token: 0x04000933 RID: 2355
		k_EPersonaChangeNickname = 4096,
		// Token: 0x04000934 RID: 2356
		k_EPersonaChangeSteamLevel = 8192
	}
}
