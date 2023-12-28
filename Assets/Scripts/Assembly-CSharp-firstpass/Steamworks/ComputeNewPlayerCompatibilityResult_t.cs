using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000141 RID: 321
	[CallbackIdentity(211)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ComputeNewPlayerCompatibilityResult_t
	{
		// Token: 0x0400056D RID: 1389
		public const int k_iCallback = 211;

		// Token: 0x0400056E RID: 1390
		public EResult m_eResult;

		// Token: 0x0400056F RID: 1391
		public int m_cPlayersThatDontLikeCandidate;

		// Token: 0x04000570 RID: 1392
		public int m_cPlayersThatCandidateDoesntLike;

		// Token: 0x04000571 RID: 1393
		public int m_cClanPlayersThatDontLikeCandidate;

		// Token: 0x04000572 RID: 1394
		public CSteamID m_SteamIDCandidate;
	}
}
