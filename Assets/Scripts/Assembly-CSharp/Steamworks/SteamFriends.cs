using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000104 RID: 260
	public static class SteamFriends
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x00009888 File Offset: 0x00007C88
		public static string GetPersonaName()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetPersonaName());
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000989C File Offset: 0x00007C9C
		public static SteamAPICall_t SetPersonaName(string pchPersonaName)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPersonaName))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamFriends_SetPersonaName(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000098E4 File Offset: 0x00007CE4
		public static EPersonaState GetPersonaState()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetPersonaState();
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000098F0 File Offset: 0x00007CF0
		public static int GetFriendCount(EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCount(iFriendFlags);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000098FD File Offset: 0x00007CFD
		public static CSteamID GetFriendByIndex(int iFriend, EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetFriendByIndex(iFriend, iFriendFlags);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00009910 File Offset: 0x00007D10
		public static EFriendRelationship GetFriendRelationship(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendRelationship(steamIDFriend);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000991D File Offset: 0x00007D1D
		public static EPersonaState GetFriendPersonaState(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendPersonaState(steamIDFriend);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000992A File Offset: 0x00007D2A
		public static string GetFriendPersonaName(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendPersonaName(steamIDFriend));
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000993C File Offset: 0x00007D3C
		public static bool GetFriendGamePlayed(CSteamID steamIDFriend, out FriendGameInfo_t pFriendGameInfo)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendGamePlayed(steamIDFriend, out pFriendGameInfo);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000994A File Offset: 0x00007D4A
		public static string GetFriendPersonaNameHistory(CSteamID steamIDFriend, int iPersonaName)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendPersonaNameHistory(steamIDFriend, iPersonaName));
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000995D File Offset: 0x00007D5D
		public static int GetFriendSteamLevel(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendSteamLevel(steamIDFriend);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000996A File Offset: 0x00007D6A
		public static string GetPlayerNickname(CSteamID steamIDPlayer)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetPlayerNickname(steamIDPlayer));
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000997C File Offset: 0x00007D7C
		public static int GetFriendsGroupCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendsGroupCount();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00009988 File Offset: 0x00007D88
		public static FriendsGroupID_t GetFriendsGroupIDByIndex(int iFG)
		{
			InteropHelp.TestIfAvailableClient();
			return (FriendsGroupID_t)NativeMethods.ISteamFriends_GetFriendsGroupIDByIndex(iFG);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000999A File Offset: 0x00007D9A
		public static string GetFriendsGroupName(FriendsGroupID_t friendsGroupID)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendsGroupName(friendsGroupID));
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000099AC File Offset: 0x00007DAC
		public static int GetFriendsGroupMembersCount(FriendsGroupID_t friendsGroupID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendsGroupMembersCount(friendsGroupID);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000099B9 File Offset: 0x00007DB9
		public static void GetFriendsGroupMembersList(FriendsGroupID_t friendsGroupID, CSteamID[] pOutSteamIDMembers, int nMembersCount)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_GetFriendsGroupMembersList(friendsGroupID, pOutSteamIDMembers, nMembersCount);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000099C8 File Offset: 0x00007DC8
		public static bool HasFriend(CSteamID steamIDFriend, EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_HasFriend(steamIDFriend, iFriendFlags);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000099D6 File Offset: 0x00007DD6
		public static int GetClanCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanCount();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000099E2 File Offset: 0x00007DE2
		public static CSteamID GetClanByIndex(int iClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanByIndex(iClan);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000099F4 File Offset: 0x00007DF4
		public static string GetClanName(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetClanName(steamIDClan));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00009A06 File Offset: 0x00007E06
		public static string GetClanTag(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetClanTag(steamIDClan));
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00009A18 File Offset: 0x00007E18
		public static bool GetClanActivityCounts(CSteamID steamIDClan, out int pnOnline, out int pnInGame, out int pnChatting)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanActivityCounts(steamIDClan, out pnOnline, out pnInGame, out pnChatting);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00009A28 File Offset: 0x00007E28
		public static SteamAPICall_t DownloadClanActivityCounts(CSteamID[] psteamIDClans, int cClansToRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_DownloadClanActivityCounts(psteamIDClans, cClansToRequest);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00009A3B File Offset: 0x00007E3B
		public static int GetFriendCountFromSource(CSteamID steamIDSource)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCountFromSource(steamIDSource);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00009A48 File Offset: 0x00007E48
		public static CSteamID GetFriendFromSourceByIndex(CSteamID steamIDSource, int iFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetFriendFromSourceByIndex(steamIDSource, iFriend);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00009A5B File Offset: 0x00007E5B
		public static bool IsUserInSource(CSteamID steamIDUser, CSteamID steamIDSource)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsUserInSource(steamIDUser, steamIDSource);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00009A69 File Offset: 0x00007E69
		public static void SetInGameVoiceSpeaking(CSteamID steamIDUser, bool bSpeaking)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_SetInGameVoiceSpeaking(steamIDUser, bSpeaking);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00009A78 File Offset: 0x00007E78
		public static void ActivateGameOverlay(string pchDialog)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDialog))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlay(utf8StringHandle);
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00009ABC File Offset: 0x00007EBC
		public static void ActivateGameOverlayToUser(string pchDialog, CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDialog))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlayToUser(utf8StringHandle, steamID);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00009B00 File Offset: 0x00007F00
		public static void ActivateGameOverlayToWebPage(string pchURL)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchURL))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlayToWebPage(utf8StringHandle);
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00009B44 File Offset: 0x00007F44
		public static void ActivateGameOverlayToStore(AppId_t nAppID, EOverlayToStoreFlag eFlag)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ActivateGameOverlayToStore(nAppID, eFlag);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00009B52 File Offset: 0x00007F52
		public static void SetPlayedWith(CSteamID steamIDUserPlayedWith)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_SetPlayedWith(steamIDUserPlayedWith);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00009B5F File Offset: 0x00007F5F
		public static void ActivateGameOverlayInviteDialog(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ActivateGameOverlayInviteDialog(steamIDLobby);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00009B6C File Offset: 0x00007F6C
		public static int GetSmallFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetSmallFriendAvatar(steamIDFriend);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00009B79 File Offset: 0x00007F79
		public static int GetMediumFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetMediumFriendAvatar(steamIDFriend);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00009B86 File Offset: 0x00007F86
		public static int GetLargeFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetLargeFriendAvatar(steamIDFriend);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00009B93 File Offset: 0x00007F93
		public static bool RequestUserInformation(CSteamID steamIDUser, bool bRequireNameOnly)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_RequestUserInformation(steamIDUser, bRequireNameOnly);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00009BA1 File Offset: 0x00007FA1
		public static SteamAPICall_t RequestClanOfficerList(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_RequestClanOfficerList(steamIDClan);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00009BB3 File Offset: 0x00007FB3
		public static CSteamID GetClanOwner(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanOwner(steamIDClan);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00009BC5 File Offset: 0x00007FC5
		public static int GetClanOfficerCount(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanOfficerCount(steamIDClan);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00009BD2 File Offset: 0x00007FD2
		public static CSteamID GetClanOfficerByIndex(CSteamID steamIDClan, int iOfficer)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanOfficerByIndex(steamIDClan, iOfficer);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00009BE5 File Offset: 0x00007FE5
		public static uint GetUserRestrictions()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetUserRestrictions();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00009BF4 File Offset: 0x00007FF4
		public static bool SetRichPresence(string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					result = NativeMethods.ISteamFriends_SetRichPresence(utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00009C58 File Offset: 0x00008058
		public static void ClearRichPresence()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ClearRichPresence();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00009C64 File Offset: 0x00008064
		public static string GetFriendRichPresence(CSteamID steamIDFriend, string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			string result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendRichPresence(steamIDFriend, utf8StringHandle));
			}
			return result;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00009CB0 File Offset: 0x000080B0
		public static int GetFriendRichPresenceKeyCount(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendRichPresenceKeyCount(steamIDFriend);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00009CBD File Offset: 0x000080BD
		public static string GetFriendRichPresenceKeyByIndex(CSteamID steamIDFriend, int iKey)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendRichPresenceKeyByIndex(steamIDFriend, iKey));
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00009CD0 File Offset: 0x000080D0
		public static void RequestFriendRichPresence(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_RequestFriendRichPresence(steamIDFriend);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00009CE0 File Offset: 0x000080E0
		public static bool InviteUserToGame(CSteamID steamIDFriend, string pchConnectString)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectString))
			{
				result = NativeMethods.ISteamFriends_InviteUserToGame(steamIDFriend, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00009D24 File Offset: 0x00008124
		public static int GetCoplayFriendCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetCoplayFriendCount();
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00009D30 File Offset: 0x00008130
		public static CSteamID GetCoplayFriend(int iCoplayFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetCoplayFriend(iCoplayFriend);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00009D42 File Offset: 0x00008142
		public static int GetFriendCoplayTime(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCoplayTime(steamIDFriend);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00009D4F File Offset: 0x0000814F
		public static AppId_t GetFriendCoplayGame(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (AppId_t)NativeMethods.ISteamFriends_GetFriendCoplayGame(steamIDFriend);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00009D61 File Offset: 0x00008161
		public static SteamAPICall_t JoinClanChatRoom(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_JoinClanChatRoom(steamIDClan);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00009D73 File Offset: 0x00008173
		public static bool LeaveClanChatRoom(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_LeaveClanChatRoom(steamIDClan);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00009D80 File Offset: 0x00008180
		public static int GetClanChatMemberCount(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanChatMemberCount(steamIDClan);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00009D8D File Offset: 0x0000818D
		public static CSteamID GetChatMemberByIndex(CSteamID steamIDClan, int iUser)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetChatMemberByIndex(steamIDClan, iUser);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00009DA0 File Offset: 0x000081A0
		public static bool SendClanChatMessage(CSteamID steamIDClanChat, string pchText)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchText))
			{
				result = NativeMethods.ISteamFriends_SendClanChatMessage(steamIDClanChat, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00009DE4 File Offset: 0x000081E4
		public static int GetClanChatMessage(CSteamID steamIDClanChat, int iMessage, out string prgchText, int cchTextMax, out EChatEntryType peChatEntryType, out CSteamID psteamidChatter)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchTextMax);
			int num = NativeMethods.ISteamFriends_GetClanChatMessage(steamIDClanChat, iMessage, intPtr, cchTextMax, out peChatEntryType, out psteamidChatter);
			prgchText = ((num == 0) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00009E26 File Offset: 0x00008226
		public static bool IsClanChatAdmin(CSteamID steamIDClanChat, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanChatAdmin(steamIDClanChat, steamIDUser);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00009E34 File Offset: 0x00008234
		public static bool IsClanChatWindowOpenInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanChatWindowOpenInSteam(steamIDClanChat);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00009E41 File Offset: 0x00008241
		public static bool OpenClanChatWindowInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_OpenClanChatWindowInSteam(steamIDClanChat);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00009E4E File Offset: 0x0000824E
		public static bool CloseClanChatWindowInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_CloseClanChatWindowInSteam(steamIDClanChat);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00009E5B File Offset: 0x0000825B
		public static bool SetListenForFriendsMessages(bool bInterceptEnabled)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_SetListenForFriendsMessages(bInterceptEnabled);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00009E68 File Offset: 0x00008268
		public static bool ReplyToFriendMessage(CSteamID steamIDFriend, string pchMsgToSend)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchMsgToSend))
			{
				result = NativeMethods.ISteamFriends_ReplyToFriendMessage(steamIDFriend, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00009EAC File Offset: 0x000082AC
		public static int GetFriendMessage(CSteamID steamIDFriend, int iMessageID, out string pvData, int cubData, out EChatEntryType peChatEntryType)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cubData);
			int num = NativeMethods.ISteamFriends_GetFriendMessage(steamIDFriend, iMessageID, intPtr, cubData, out peChatEntryType);
			pvData = ((num == 0) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00009EEC File Offset: 0x000082EC
		public static SteamAPICall_t GetFollowerCount(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_GetFollowerCount(steamID);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00009EFE File Offset: 0x000082FE
		public static SteamAPICall_t IsFollowing(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_IsFollowing(steamID);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00009F10 File Offset: 0x00008310
		public static SteamAPICall_t EnumerateFollowingList(uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_EnumerateFollowingList(unStartIndex);
		}
	}
}
