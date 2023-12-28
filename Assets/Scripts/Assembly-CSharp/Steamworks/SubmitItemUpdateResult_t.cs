﻿using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A1 RID: 417
	[CallbackIdentity(3404)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SubmitItemUpdateResult_t
	{
		// Token: 0x040006E5 RID: 1765
		public const int k_iCallback = 3404;

		// Token: 0x040006E6 RID: 1766
		public EResult m_eResult;

		// Token: 0x040006E7 RID: 1767
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
