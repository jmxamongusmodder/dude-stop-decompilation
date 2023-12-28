using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200010B RID: 267
	public static class SteamGameServerUtils
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x0000B85A File Offset: 0x00009C5A
		public static uint GetSecondsSinceAppActive()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetSecondsSinceAppActive();
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0000B866 File Offset: 0x00009C66
		public static uint GetSecondsSinceComputerActive()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetSecondsSinceComputerActive();
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0000B872 File Offset: 0x00009C72
		public static EUniverse GetConnectedUniverse()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetConnectedUniverse();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000B87E File Offset: 0x00009C7E
		public static uint GetServerRealTime()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetServerRealTime();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000B88A File Offset: 0x00009C8A
		public static string GetIPCountry()
		{
			InteropHelp.TestIfAvailableGameServer();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamGameServerUtils_GetIPCountry());
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000B89B File Offset: 0x00009C9B
		public static bool GetImageSize(int iImage, out uint pnWidth, out uint pnHeight)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetImageSize(iImage, out pnWidth, out pnHeight);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000B8AA File Offset: 0x00009CAA
		public static bool GetImageRGBA(int iImage, byte[] pubDest, int nDestBufferSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetImageRGBA(iImage, pubDest, nDestBufferSize);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0000B8B9 File Offset: 0x00009CB9
		public static bool GetCSERIPPort(out uint unIP, out ushort usPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetCSERIPPort(out unIP, out usPort);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0000B8C7 File Offset: 0x00009CC7
		public static byte GetCurrentBatteryPower()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetCurrentBatteryPower();
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000B8D3 File Offset: 0x00009CD3
		public static AppId_t GetAppID()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (AppId_t)NativeMethods.ISteamGameServerUtils_GetAppID();
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000B8E4 File Offset: 0x00009CE4
		public static void SetOverlayNotificationPosition(ENotificationPosition eNotificationPosition)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServerUtils_SetOverlayNotificationPosition(eNotificationPosition);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000B8F1 File Offset: 0x00009CF1
		public static bool IsAPICallCompleted(SteamAPICall_t hSteamAPICall, out bool pbFailed)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_IsAPICallCompleted(hSteamAPICall, out pbFailed);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0000B8FF File Offset: 0x00009CFF
		public static ESteamAPICallFailure GetAPICallFailureReason(SteamAPICall_t hSteamAPICall)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetAPICallFailureReason(hSteamAPICall);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0000B90C File Offset: 0x00009D0C
		public static bool GetAPICallResult(SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetAPICallResult(hSteamAPICall, pCallback, cubCallback, iCallbackExpected, out pbFailed);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0000B91E File Offset: 0x00009D1E
		public static uint GetIPCCallCount()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetIPCCallCount();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0000B92A File Offset: 0x00009D2A
		public static void SetWarningMessageHook(SteamAPIWarningMessageHook_t pFunction)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServerUtils_SetWarningMessageHook(pFunction);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0000B937 File Offset: 0x00009D37
		public static bool IsOverlayEnabled()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_IsOverlayEnabled();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0000B943 File Offset: 0x00009D43
		public static bool BOverlayNeedsPresent()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_BOverlayNeedsPresent();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0000B950 File Offset: 0x00009D50
		public static SteamAPICall_t CheckFileSignature(string szFileName)
		{
			InteropHelp.TestIfAvailableGameServer();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(szFileName))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamGameServerUtils_CheckFileSignature(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0000B998 File Offset: 0x00009D98
		public static bool ShowGamepadTextInput(EGamepadTextInputMode eInputMode, EGamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchExistingText))
				{
					result = NativeMethods.ISteamGameServerUtils_ShowGamepadTextInput(eInputMode, eLineInputMode, utf8StringHandle, unCharMax, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0000BA00 File Offset: 0x00009E00
		public static uint GetEnteredGamepadTextLength()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_GetEnteredGamepadTextLength();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0000BA0C File Offset: 0x00009E0C
		public static bool GetEnteredGamepadTextInput(out string pchText, uint cchText)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchText);
			bool flag = NativeMethods.ISteamGameServerUtils_GetEnteredGamepadTextInput(intPtr, cchText);
			pchText = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0000BA48 File Offset: 0x00009E48
		public static string GetSteamUILanguage()
		{
			InteropHelp.TestIfAvailableGameServer();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamGameServerUtils_GetSteamUILanguage());
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000BA59 File Offset: 0x00009E59
		public static bool IsSteamRunningInVR()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_IsSteamRunningInVR();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0000BA65 File Offset: 0x00009E65
		public static void SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServerUtils_SetOverlayNotificationInset(nHorizontalInset, nVerticalInset);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0000BA73 File Offset: 0x00009E73
		public static bool IsSteamInBigPictureMode()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_IsSteamInBigPictureMode();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0000BA7F File Offset: 0x00009E7F
		public static void StartVRDashboard()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServerUtils_StartVRDashboard();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0000BA8B File Offset: 0x00009E8B
		public static bool IsVRHeadsetStreamingEnabled()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUtils_IsVRHeadsetStreamingEnabled();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0000BA97 File Offset: 0x00009E97
		public static void SetVRHeadsetStreamingEnabled(bool bEnabled)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServerUtils_SetVRHeadsetStreamingEnabled(bEnabled);
		}
	}
}
