using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000246 RID: 582
	public class ISteamMatchmakingPlayersResponse
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x000101A8 File Offset: 0x0000E5A8
		public ISteamMatchmakingPlayersResponse(ISteamMatchmakingPlayersResponse.AddPlayerToList onAddPlayerToList, ISteamMatchmakingPlayersResponse.PlayersFailedToRespond onPlayersFailedToRespond, ISteamMatchmakingPlayersResponse.PlayersRefreshComplete onPlayersRefreshComplete)
		{
			if (onAddPlayerToList == null || onPlayersFailedToRespond == null || onPlayersRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_AddPlayerToList = onAddPlayerToList;
			this.m_PlayersFailedToRespond = onPlayersFailedToRespond;
			this.m_PlayersRefreshComplete = onPlayersRefreshComplete;
			this.m_VTable = new ISteamMatchmakingPlayersResponse.VTable
			{
				m_VTAddPlayerToList = new ISteamMatchmakingPlayersResponse.InternalAddPlayerToList(this.InternalOnAddPlayerToList),
				m_VTPlayersFailedToRespond = new ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond(this.InternalOnPlayersFailedToRespond),
				m_VTPlayersRefreshComplete = new ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete(this.InternalOnPlayersRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPlayersResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00010270 File Offset: 0x0000E670
		~ISteamMatchmakingPlayersResponse()
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

		// Token: 0x06000D8F RID: 3471 RVA: 0x000102D4 File Offset: 0x0000E6D4
		private void InternalOnAddPlayerToList(IntPtr pchName, int nScore, float flTimePlayed)
		{
			this.m_AddPlayerToList(InteropHelp.PtrToStringUTF8(pchName), nScore, flTimePlayed);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000102E9 File Offset: 0x0000E6E9
		private void InternalOnPlayersFailedToRespond()
		{
			this.m_PlayersFailedToRespond();
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000102F6 File Offset: 0x0000E6F6
		private void InternalOnPlayersRefreshComplete()
		{
			this.m_PlayersRefreshComplete();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00010303 File Offset: 0x0000E703
		public static explicit operator IntPtr(ISteamMatchmakingPlayersResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000C5E RID: 3166
		private ISteamMatchmakingPlayersResponse.VTable m_VTable;

		// Token: 0x04000C5F RID: 3167
		private IntPtr m_pVTable;

		// Token: 0x04000C60 RID: 3168
		private GCHandle m_pGCHandle;

		// Token: 0x04000C61 RID: 3169
		private ISteamMatchmakingPlayersResponse.AddPlayerToList m_AddPlayerToList;

		// Token: 0x04000C62 RID: 3170
		private ISteamMatchmakingPlayersResponse.PlayersFailedToRespond m_PlayersFailedToRespond;

		// Token: 0x04000C63 RID: 3171
		private ISteamMatchmakingPlayersResponse.PlayersRefreshComplete m_PlayersRefreshComplete;

		// Token: 0x02000247 RID: 583
		// (Invoke) Token: 0x06000D94 RID: 3476
		public delegate void AddPlayerToList(string pchName, int nScore, float flTimePlayed);

		// Token: 0x02000248 RID: 584
		// (Invoke) Token: 0x06000D98 RID: 3480
		public delegate void PlayersFailedToRespond();

		// Token: 0x02000249 RID: 585
		// (Invoke) Token: 0x06000D9C RID: 3484
		public delegate void PlayersRefreshComplete();

		// Token: 0x0200024A RID: 586
		// (Invoke) Token: 0x06000DA0 RID: 3488
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalAddPlayerToList(IntPtr pchName, int nScore, float flTimePlayed);

		// Token: 0x0200024B RID: 587
		// (Invoke) Token: 0x06000DA4 RID: 3492
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalPlayersFailedToRespond();

		// Token: 0x0200024C RID: 588
		// (Invoke) Token: 0x06000DA8 RID: 3496
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalPlayersRefreshComplete();

		// Token: 0x0200024D RID: 589
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000C64 RID: 3172
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalAddPlayerToList m_VTAddPlayerToList;

			// Token: 0x04000C65 RID: 3173
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond m_VTPlayersFailedToRespond;

			// Token: 0x04000C66 RID: 3174
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete m_VTPlayersRefreshComplete;
		}
	}
}
