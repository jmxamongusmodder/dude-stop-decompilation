using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000240 RID: 576
	public class ISteamMatchmakingPingResponse
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x0001006C File Offset: 0x0000E46C
		public ISteamMatchmakingPingResponse(ISteamMatchmakingPingResponse.ServerResponded onServerResponded, ISteamMatchmakingPingResponse.ServerFailedToRespond onServerFailedToRespond)
		{
			if (onServerResponded == null || onServerFailedToRespond == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_VTable = new ISteamMatchmakingPingResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingPingResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingPingResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPingResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00010114 File Offset: 0x0000E514
		~ISteamMatchmakingPingResponse()
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

		// Token: 0x06000D79 RID: 3449 RVA: 0x00010178 File Offset: 0x0000E578
		private void InternalOnServerResponded(gameserveritem_t server)
		{
			this.m_ServerResponded(server);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00010186 File Offset: 0x0000E586
		private void InternalOnServerFailedToRespond()
		{
			this.m_ServerFailedToRespond();
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00010193 File Offset: 0x0000E593
		public static explicit operator IntPtr(ISteamMatchmakingPingResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000C57 RID: 3159
		private ISteamMatchmakingPingResponse.VTable m_VTable;

		// Token: 0x04000C58 RID: 3160
		private IntPtr m_pVTable;

		// Token: 0x04000C59 RID: 3161
		private GCHandle m_pGCHandle;

		// Token: 0x04000C5A RID: 3162
		private ISteamMatchmakingPingResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000C5B RID: 3163
		private ISteamMatchmakingPingResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x02000241 RID: 577
		// (Invoke) Token: 0x06000D7D RID: 3453
		public delegate void ServerResponded(gameserveritem_t server);

		// Token: 0x02000242 RID: 578
		// (Invoke) Token: 0x06000D81 RID: 3457
		public delegate void ServerFailedToRespond();

		// Token: 0x02000243 RID: 579
		// (Invoke) Token: 0x06000D85 RID: 3461
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalServerResponded(gameserveritem_t server);

		// Token: 0x02000244 RID: 580
		// (Invoke) Token: 0x06000D89 RID: 3465
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalServerFailedToRespond();

		// Token: 0x02000245 RID: 581
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000C5C RID: 3164
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000C5D RID: 3165
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;
		}
	}
}
