using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000260 RID: 608
	// (Invoke) Token: 0x06000E18 RID: 3608
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void SteamAPI_CheckCallbackRegistered_t(int iCallbackNum);
}
