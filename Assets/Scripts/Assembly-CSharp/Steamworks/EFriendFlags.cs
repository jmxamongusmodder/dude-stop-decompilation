using System;

namespace Steamworks
{
	// Token: 0x020001D7 RID: 471
	[Flags]
	public enum EFriendFlags
	{
		// Token: 0x0400090D RID: 2317
		k_EFriendFlagNone = 0,
		// Token: 0x0400090E RID: 2318
		k_EFriendFlagBlocked = 1,
		// Token: 0x0400090F RID: 2319
		k_EFriendFlagFriendshipRequested = 2,
		// Token: 0x04000910 RID: 2320
		k_EFriendFlagImmediate = 4,
		// Token: 0x04000911 RID: 2321
		k_EFriendFlagClanMember = 8,
		// Token: 0x04000912 RID: 2322
		k_EFriendFlagOnGameServer = 16,
		// Token: 0x04000913 RID: 2323
		k_EFriendFlagRequestingFriendship = 128,
		// Token: 0x04000914 RID: 2324
		k_EFriendFlagRequestingInfo = 256,
		// Token: 0x04000915 RID: 2325
		k_EFriendFlagIgnored = 512,
		// Token: 0x04000916 RID: 2326
		k_EFriendFlagIgnoredFriend = 1024,
		// Token: 0x04000917 RID: 2327
		k_EFriendFlagChatMember = 4096,
		// Token: 0x04000918 RID: 2328
		k_EFriendFlagAll = 65535
	}
}
