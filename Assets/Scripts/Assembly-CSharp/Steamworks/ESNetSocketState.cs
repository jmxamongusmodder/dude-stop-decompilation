using System;

namespace Steamworks
{
	// Token: 0x020001E7 RID: 487
	public enum ESNetSocketState
	{
		// Token: 0x0400099C RID: 2460
		k_ESNetSocketStateInvalid,
		// Token: 0x0400099D RID: 2461
		k_ESNetSocketStateConnected,
		// Token: 0x0400099E RID: 2462
		k_ESNetSocketStateInitiated = 10,
		// Token: 0x0400099F RID: 2463
		k_ESNetSocketStateLocalCandidatesFound,
		// Token: 0x040009A0 RID: 2464
		k_ESNetSocketStateReceivedRemoteCandidates,
		// Token: 0x040009A1 RID: 2465
		k_ESNetSocketStateChallengeHandshake = 15,
		// Token: 0x040009A2 RID: 2466
		k_ESNetSocketStateDisconnecting = 21,
		// Token: 0x040009A3 RID: 2467
		k_ESNetSocketStateLocalDisconnect,
		// Token: 0x040009A4 RID: 2468
		k_ESNetSocketStateTimeoutDuringConnect,
		// Token: 0x040009A5 RID: 2469
		k_ESNetSocketStateRemoteEndDisconnected,
		// Token: 0x040009A6 RID: 2470
		k_ESNetSocketStateConnectionBroken
	}
}
