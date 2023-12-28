using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000110 RID: 272
	public static class SteamMatchmakingServers
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x0000C975 File Offset: 0x0000AD75
		public static HServerListRequest RequestInternetServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestInternetServerList(iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000C999 File Offset: 0x0000AD99
		public static HServerListRequest RequestLANServerList(AppId_t iApp, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestLANServerList(iApp, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000C9B1 File Offset: 0x0000ADB1
		public static HServerListRequest RequestFriendsServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestFriendsServerList(iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000C9D5 File Offset: 0x0000ADD5
		public static HServerListRequest RequestFavoritesServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestFavoritesServerList(iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000C9F9 File Offset: 0x0000ADF9
		public static HServerListRequest RequestHistoryServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestHistoryServerList(iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000CA1D File Offset: 0x0000AE1D
		public static HServerListRequest RequestSpectatorServerList(AppId_t iApp, MatchMakingKeyValuePair_t[] ppchFilters, uint nFilters, ISteamMatchmakingServerListResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerListRequest)NativeMethods.ISteamMatchmakingServers_RequestSpectatorServerList(iApp, new MMKVPMarshaller(ppchFilters), nFilters, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0000CA41 File Offset: 0x0000AE41
		public static void ReleaseRequest(HServerListRequest hServerListRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_ReleaseRequest(hServerListRequest);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0000CA4E File Offset: 0x0000AE4E
		public static gameserveritem_t GetServerDetails(HServerListRequest hRequest, int iServer)
		{
			InteropHelp.TestIfAvailableClient();
			return (gameserveritem_t)Marshal.PtrToStructure(NativeMethods.ISteamMatchmakingServers_GetServerDetails(hRequest, iServer), typeof(gameserveritem_t));
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0000CA70 File Offset: 0x0000AE70
		public static void CancelQuery(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_CancelQuery(hRequest);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0000CA7D File Offset: 0x0000AE7D
		public static void RefreshQuery(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_RefreshQuery(hRequest);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0000CA8A File Offset: 0x0000AE8A
		public static bool IsRefreshing(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmakingServers_IsRefreshing(hRequest);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0000CA97 File Offset: 0x0000AE97
		public static int GetServerCount(HServerListRequest hRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMatchmakingServers_GetServerCount(hRequest);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000CAA4 File Offset: 0x0000AEA4
		public static void RefreshServer(HServerListRequest hRequest, int iServer)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_RefreshServer(hRequest, iServer);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0000CAB2 File Offset: 0x0000AEB2
		public static HServerQuery PingServer(uint unIP, ushort usPort, ISteamMatchmakingPingResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_PingServer(unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0000CACB File Offset: 0x0000AECB
		public static HServerQuery PlayerDetails(uint unIP, ushort usPort, ISteamMatchmakingPlayersResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_PlayerDetails(unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0000CAE4 File Offset: 0x0000AEE4
		public static HServerQuery ServerRules(uint unIP, ushort usPort, ISteamMatchmakingRulesResponse pRequestServersResponse)
		{
			InteropHelp.TestIfAvailableClient();
			return (HServerQuery)NativeMethods.ISteamMatchmakingServers_ServerRules(unIP, usPort, (IntPtr)pRequestServersResponse);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0000CAFD File Offset: 0x0000AEFD
		public static void CancelServerQuery(HServerQuery hServerQuery)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMatchmakingServers_CancelServerQuery(hServerQuery);
		}
	}
}
