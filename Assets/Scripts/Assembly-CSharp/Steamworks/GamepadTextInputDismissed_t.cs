using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001C9 RID: 457
	[CallbackIdentity(714)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GamepadTextInputDismissed_t
	{
		// Token: 0x04000766 RID: 1894
		public const int k_iCallback = 714;

		// Token: 0x04000767 RID: 1895
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSubmitted;

		// Token: 0x04000768 RID: 1896
		public uint m_unSubmittedText;
	}
}
