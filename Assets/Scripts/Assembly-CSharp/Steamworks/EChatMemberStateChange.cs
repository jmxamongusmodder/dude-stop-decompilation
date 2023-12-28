using System;

namespace Steamworks
{
	// Token: 0x020001E3 RID: 483
	[Flags]
	public enum EChatMemberStateChange
	{
		// Token: 0x04000985 RID: 2437
		k_EChatMemberStateChangeEntered = 1,
		// Token: 0x04000986 RID: 2438
		k_EChatMemberStateChangeLeft = 2,
		// Token: 0x04000987 RID: 2439
		k_EChatMemberStateChangeDisconnected = 4,
		// Token: 0x04000988 RID: 2440
		k_EChatMemberStateChangeKicked = 8,
		// Token: 0x04000989 RID: 2441
		k_EChatMemberStateChangeBanned = 16
	}
}
