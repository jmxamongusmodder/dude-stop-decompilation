using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000238 RID: 568
	public class ISteamMatchmakingServerListResponse
	{
		// Token: 0x06000D58 RID: 3416 RVA: 0x0000FEFC File Offset: 0x0000E2FC
		public ISteamMatchmakingServerListResponse(ISteamMatchmakingServerListResponse.ServerResponded onServerResponded, ISteamMatchmakingServerListResponse.ServerFailedToRespond onServerFailedToRespond, ISteamMatchmakingServerListResponse.RefreshComplete onRefreshComplete)
		{
			if (onServerResponded == null || onServerFailedToRespond == null || onRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_RefreshComplete = onRefreshComplete;
			this.m_VTable = new ISteamMatchmakingServerListResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingServerListResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingServerListResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond),
				m_VTRefreshComplete = new ISteamMatchmakingServerListResponse.InternalRefreshComplete(this.InternalOnRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingServerListResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0000FFC4 File Offset: 0x0000E3C4
		~ISteamMatchmakingServerListResponse()
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

		// Token: 0x06000D5A RID: 3418 RVA: 0x00010028 File Offset: 0x0000E428
		private void InternalOnServerResponded(HServerListRequest hRequest, int iServer)
		{
			this.m_ServerResponded(hRequest, iServer);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00010037 File Offset: 0x0000E437
		private void InternalOnServerFailedToRespond(HServerListRequest hRequest, int iServer)
		{
			this.m_ServerFailedToRespond(hRequest, iServer);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00010046 File Offset: 0x0000E446
		private void InternalOnRefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response)
		{
			this.m_RefreshComplete(hRequest, response);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00010055 File Offset: 0x0000E455
		public static explicit operator IntPtr(ISteamMatchmakingServerListResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000C4E RID: 3150
		private ISteamMatchmakingServerListResponse.VTable m_VTable;

		// Token: 0x04000C4F RID: 3151
		private IntPtr m_pVTable;

		// Token: 0x04000C50 RID: 3152
		private GCHandle m_pGCHandle;

		// Token: 0x04000C51 RID: 3153
		private ISteamMatchmakingServerListResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000C52 RID: 3154
		private ISteamMatchmakingServerListResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x04000C53 RID: 3155
		private ISteamMatchmakingServerListResponse.RefreshComplete m_RefreshComplete;

		// Token: 0x02000239 RID: 569
		// (Invoke) Token: 0x06000D5F RID: 3423
		public delegate void ServerResponded(HServerListRequest hRequest, int iServer);

		// Token: 0x0200023A RID: 570
		// (Invoke) Token: 0x06000D63 RID: 3427
		public delegate void ServerFailedToRespond(HServerListRequest hRequest, int iServer);

		// Token: 0x0200023B RID: 571
		// (Invoke) Token: 0x06000D67 RID: 3431
		public delegate void RefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x0200023C RID: 572
		// (Invoke) Token: 0x06000D6B RID: 3435
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalServerResponded(HServerListRequest hRequest, int iServer);

		// Token: 0x0200023D RID: 573
		// (Invoke) Token: 0x06000D6F RID: 3439
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalServerFailedToRespond(HServerListRequest hRequest, int iServer);

		// Token: 0x0200023E RID: 574
		// (Invoke) Token: 0x06000D73 RID: 3443
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalRefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x0200023F RID: 575
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000C54 RID: 3156
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000C55 RID: 3157
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;

			// Token: 0x04000C56 RID: 3158
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalRefreshComplete m_VTRefreshComplete;
		}
	}
}
