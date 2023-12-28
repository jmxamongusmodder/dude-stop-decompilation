using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200024E RID: 590
	public class ISteamMatchmakingRulesResponse
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x00010318 File Offset: 0x0000E718
		public ISteamMatchmakingRulesResponse(ISteamMatchmakingRulesResponse.RulesResponded onRulesResponded, ISteamMatchmakingRulesResponse.RulesFailedToRespond onRulesFailedToRespond, ISteamMatchmakingRulesResponse.RulesRefreshComplete onRulesRefreshComplete)
		{
			if (onRulesResponded == null || onRulesFailedToRespond == null || onRulesRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_RulesResponded = onRulesResponded;
			this.m_RulesFailedToRespond = onRulesFailedToRespond;
			this.m_RulesRefreshComplete = onRulesRefreshComplete;
			this.m_VTable = new ISteamMatchmakingRulesResponse.VTable
			{
				m_VTRulesResponded = new ISteamMatchmakingRulesResponse.InternalRulesResponded(this.InternalOnRulesResponded),
				m_VTRulesFailedToRespond = new ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond(this.InternalOnRulesFailedToRespond),
				m_VTRulesRefreshComplete = new ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete(this.InternalOnRulesRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingRulesResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x000103E0 File Offset: 0x0000E7E0
		~ISteamMatchmakingRulesResponse()
		{
			if (this.m_pVTable != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pVTable);
			}
			if (this.m_pGCHandle.IsAllocated)
			{
				this.m_pGCHandle.Free();
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00010444 File Offset: 0x0000E844
		private void InternalOnRulesResponded(IntPtr pchRule, IntPtr pchValue)
		{
			this.m_RulesResponded(InteropHelp.PtrToStringUTF8(pchRule), InteropHelp.PtrToStringUTF8(pchValue));
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0001045D File Offset: 0x0000E85D
		private void InternalOnRulesFailedToRespond()
		{
			this.m_RulesFailedToRespond();
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0001046A File Offset: 0x0000E86A
		private void InternalOnRulesRefreshComplete()
		{
			this.m_RulesRefreshComplete();
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00010477 File Offset: 0x0000E877
		public static explicit operator IntPtr(ISteamMatchmakingRulesResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000C67 RID: 3175
		private ISteamMatchmakingRulesResponse.VTable m_VTable;

		// Token: 0x04000C68 RID: 3176
		private IntPtr m_pVTable;

		// Token: 0x04000C69 RID: 3177
		private GCHandle m_pGCHandle;

		// Token: 0x04000C6A RID: 3178
		private ISteamMatchmakingRulesResponse.RulesResponded m_RulesResponded;

		// Token: 0x04000C6B RID: 3179
		private ISteamMatchmakingRulesResponse.RulesFailedToRespond m_RulesFailedToRespond;

		// Token: 0x04000C6C RID: 3180
		private ISteamMatchmakingRulesResponse.RulesRefreshComplete m_RulesRefreshComplete;

		// Token: 0x0200024F RID: 591
		// (Invoke) Token: 0x06000DB3 RID: 3507
		public delegate void RulesResponded(string pchRule, string pchValue);

		// Token: 0x02000250 RID: 592
		// (Invoke) Token: 0x06000DB7 RID: 3511
		public delegate void RulesFailedToRespond();

		// Token: 0x02000251 RID: 593
		// (Invoke) Token: 0x06000DBB RID: 3515
		public delegate void RulesRefreshComplete();

		// Token: 0x02000252 RID: 594
		// (Invoke) Token: 0x06000DBF RID: 3519
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalRulesResponded(IntPtr pchRule, IntPtr pchValue);

		// Token: 0x02000253 RID: 595
		// (Invoke) Token: 0x06000DC3 RID: 3523
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalRulesFailedToRespond();

		// Token: 0x02000254 RID: 596
		// (Invoke) Token: 0x06000DC7 RID: 3527
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalRulesRefreshComplete();

		// Token: 0x02000255 RID: 597
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000C6D RID: 3181
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesResponded m_VTRulesResponded;

			// Token: 0x04000C6E RID: 3182
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond m_VTRulesFailedToRespond;

			// Token: 0x04000C6F RID: 3183
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete m_VTRulesRefreshComplete;
		}
	}
}
