using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022D RID: 557
	[StructLayout(LayoutKind.Sequential)]
	internal class CCallbackBaseVTable
	{
		// Token: 0x04000C44 RID: 3140
		private const CallingConvention cc = CallingConvention.StdCall;

		// Token: 0x04000C45 RID: 3141
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public CCallbackBaseVTable.RunCRDel m_RunCallResult;

		// Token: 0x04000C46 RID: 3142
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public CCallbackBaseVTable.RunCBDel m_RunCallback;

		// Token: 0x04000C47 RID: 3143
		[NonSerialized]
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public CCallbackBaseVTable.GetCallbackSizeBytesDel m_GetCallbackSizeBytes;

		// Token: 0x0200022E RID: 558
		// (Invoke) Token: 0x06000D39 RID: 3385
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void RunCBDel(IntPtr pvParam);

		// Token: 0x0200022F RID: 559
		// (Invoke) Token: 0x06000D3D RID: 3389
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void RunCRDel(IntPtr pvParam, [MarshalAs(UnmanagedType.I1)] bool bIOFailure, ulong hSteamAPICall);

		// Token: 0x02000230 RID: 560
		// (Invoke) Token: 0x06000D41 RID: 3393
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int GetCallbackSizeBytesDel();
	}
}
