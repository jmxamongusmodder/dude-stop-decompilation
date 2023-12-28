using System;

namespace Steamworks
{
	// Token: 0x02000108 RID: 264
	public static class SteamGameServerNetworking
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x0000AA75 File Offset: 0x00008E75
		public static bool SendP2PPacket(CSteamID steamIDRemote, byte[] pubData, uint cubData, EP2PSend eP2PSendType, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_SendP2PPacket(steamIDRemote, pubData, cubData, eP2PSendType, nChannel);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000AA87 File Offset: 0x00008E87
		public static bool IsP2PPacketAvailable(out uint pcubMsgSize, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_IsP2PPacketAvailable(out pcubMsgSize, nChannel);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000AA95 File Offset: 0x00008E95
		public static bool ReadP2PPacket(byte[] pubDest, uint cubDest, out uint pcubMsgSize, out CSteamID psteamIDRemote, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_ReadP2PPacket(pubDest, cubDest, out pcubMsgSize, out psteamIDRemote, nChannel);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000AAA7 File Offset: 0x00008EA7
		public static bool AcceptP2PSessionWithUser(CSteamID steamIDRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_AcceptP2PSessionWithUser(steamIDRemote);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000AAB4 File Offset: 0x00008EB4
		public static bool CloseP2PSessionWithUser(CSteamID steamIDRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_CloseP2PSessionWithUser(steamIDRemote);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000AAC1 File Offset: 0x00008EC1
		public static bool CloseP2PChannelWithUser(CSteamID steamIDRemote, int nChannel)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_CloseP2PChannelWithUser(steamIDRemote, nChannel);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000AACF File Offset: 0x00008ECF
		public static bool GetP2PSessionState(CSteamID steamIDRemote, out P2PSessionState_t pConnectionState)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_GetP2PSessionState(steamIDRemote, out pConnectionState);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000AADD File Offset: 0x00008EDD
		public static bool AllowP2PPacketRelay(bool bAllow)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_AllowP2PPacketRelay(bAllow);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000AAEA File Offset: 0x00008EEA
		public static SNetListenSocket_t CreateListenSocket(int nVirtualP2PPort, uint nIP, ushort nPort, bool bAllowUseOfPacketRelay)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetListenSocket_t)NativeMethods.ISteamGameServerNetworking_CreateListenSocket(nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000AAFF File Offset: 0x00008EFF
		public static SNetSocket_t CreateP2PConnectionSocket(CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetSocket_t)NativeMethods.ISteamGameServerNetworking_CreateP2PConnectionSocket(steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000AB14 File Offset: 0x00008F14
		public static SNetSocket_t CreateConnectionSocket(uint nIP, ushort nPort, int nTimeoutSec)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetSocket_t)NativeMethods.ISteamGameServerNetworking_CreateConnectionSocket(nIP, nPort, nTimeoutSec);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000AB28 File Offset: 0x00008F28
		public static bool DestroySocket(SNetSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_DestroySocket(hSocket, bNotifyRemoteEnd);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000AB36 File Offset: 0x00008F36
		public static bool DestroyListenSocket(SNetListenSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_DestroyListenSocket(hSocket, bNotifyRemoteEnd);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000AB44 File Offset: 0x00008F44
		public static bool SendDataOnSocket(SNetSocket_t hSocket, byte[] pubData, uint cubData, bool bReliable)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_SendDataOnSocket(hSocket, pubData, cubData, bReliable);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000AB54 File Offset: 0x00008F54
		public static bool IsDataAvailableOnSocket(SNetSocket_t hSocket, out uint pcubMsgSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_IsDataAvailableOnSocket(hSocket, out pcubMsgSize);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000AB62 File Offset: 0x00008F62
		public static bool RetrieveDataFromSocket(SNetSocket_t hSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_RetrieveDataFromSocket(hSocket, pubDest, cubDest, out pcubMsgSize);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000AB72 File Offset: 0x00008F72
		public static bool IsDataAvailable(SNetListenSocket_t hListenSocket, out uint pcubMsgSize, out SNetSocket_t phSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_IsDataAvailable(hListenSocket, out pcubMsgSize, out phSocket);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000AB81 File Offset: 0x00008F81
		public static bool RetrieveData(SNetListenSocket_t hListenSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize, out SNetSocket_t phSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_RetrieveData(hListenSocket, pubDest, cubDest, out pcubMsgSize, out phSocket);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000AB93 File Offset: 0x00008F93
		public static bool GetSocketInfo(SNetSocket_t hSocket, out CSteamID pSteamIDRemote, out int peSocketStatus, out uint punIPRemote, out ushort punPortRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_GetSocketInfo(hSocket, out pSteamIDRemote, out peSocketStatus, out punIPRemote, out punPortRemote);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000ABA5 File Offset: 0x00008FA5
		public static bool GetListenSocketInfo(SNetListenSocket_t hListenSocket, out uint pnIP, out ushort pnPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_GetListenSocketInfo(hListenSocket, out pnIP, out pnPort);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000ABB4 File Offset: 0x00008FB4
		public static ESNetSocketConnectionType GetSocketConnectionType(SNetSocket_t hSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_GetSocketConnectionType(hSocket);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000ABC1 File Offset: 0x00008FC1
		public static int GetMaxPacketSize(SNetSocket_t hSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerNetworking_GetMaxPacketSize(hSocket);
		}
	}
}
