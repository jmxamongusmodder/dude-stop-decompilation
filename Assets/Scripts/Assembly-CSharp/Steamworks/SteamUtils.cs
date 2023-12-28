using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200011A RID: 282
	public static class SteamUtils
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0000F024 File Offset: 0x0000D424
		public static uint GetSecondsSinceAppActive()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetSecondsSinceAppActive();
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0000F030 File Offset: 0x0000D430
		public static uint GetSecondsSinceComputerActive()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetSecondsSinceComputerActive();
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0000F03C File Offset: 0x0000D43C
		public static EUniverse GetConnectedUniverse()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetConnectedUniverse();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000F048 File Offset: 0x0000D448
		public static uint GetServerRealTime()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetServerRealTime();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0000F054 File Offset: 0x0000D454
		public static string GetIPCountry()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamUtils_GetIPCountry());
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0000F065 File Offset: 0x0000D465
		public static bool GetImageSize(int iImage, out uint pnWidth, out uint pnHeight)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetImageSize(iImage, out pnWidth, out pnHeight);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000F074 File Offset: 0x0000D474
		public static bool GetImageRGBA(int iImage, byte[] pubDest, int nDestBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetImageRGBA(iImage, pubDest, nDestBufferSize);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0000F083 File Offset: 0x0000D483
		public static bool GetCSERIPPort(out uint unIP, out ushort usPort)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetCSERIPPort(out unIP, out usPort);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000F091 File Offset: 0x0000D491
		public static byte GetCurrentBatteryPower()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetCurrentBatteryPower();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000F09D File Offset: 0x0000D49D
		public static AppId_t GetAppID()
		{
			InteropHelp.TestIfAvailableClient();
			return (AppId_t)NativeMethods.ISteamUtils_GetAppID();
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0000F0AE File Offset: 0x0000D4AE
		public static void SetOverlayNotificationPosition(ENotificationPosition eNotificationPosition)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetOverlayNotificationPosition(eNotificationPosition);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0000F0BB File Offset: 0x0000D4BB
		public static bool IsAPICallCompleted(SteamAPICall_t hSteamAPICall, out bool pbFailed)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsAPICallCompleted(hSteamAPICall, out pbFailed);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000F0C9 File Offset: 0x0000D4C9
		public static ESteamAPICallFailure GetAPICallFailureReason(SteamAPICall_t hSteamAPICall)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetAPICallFailureReason(hSteamAPICall);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0000F0D6 File Offset: 0x0000D4D6
		public static bool GetAPICallResult(SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, out bool pbFailed)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetAPICallResult(hSteamAPICall, pCallback, cubCallback, iCallbackExpected, out pbFailed);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0000F0E8 File Offset: 0x0000D4E8
		public static uint GetIPCCallCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetIPCCallCount();
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0000F0F4 File Offset: 0x0000D4F4
		public static void SetWarningMessageHook(SteamAPIWarningMessageHook_t pFunction)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetWarningMessageHook(pFunction);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000F101 File Offset: 0x0000D501
		public static bool IsOverlayEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsOverlayEnabled();
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0000F10D File Offset: 0x0000D50D
		public static bool BOverlayNeedsPresent()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_BOverlayNeedsPresent();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000F11C File Offset: 0x0000D51C
		public static SteamAPICall_t CheckFileSignature(string szFileName)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(szFileName))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamUtils_CheckFileSignature(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0000F164 File Offset: 0x0000D564
		public static bool ShowGamepadTextInput(EGamepadTextInputMode eInputMode, EGamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchExistingText))
				{
					result = NativeMethods.ISteamUtils_ShowGamepadTextInput(eInputMode, eLineInputMode, utf8StringHandle, unCharMax, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0000F1CC File Offset: 0x0000D5CC
		public static uint GetEnteredGamepadTextLength()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_GetEnteredGamepadTextLength();
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0000F1D8 File Offset: 0x0000D5D8
		public static bool GetEnteredGamepadTextInput(out string pchText, uint cchText)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchText);
			bool flag = NativeMethods.ISteamUtils_GetEnteredGamepadTextInput(intPtr, cchText);
			pchText = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0000F214 File Offset: 0x0000D614
		public static string GetSteamUILanguage()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamUtils_GetSteamUILanguage());
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0000F225 File Offset: 0x0000D625
		public static bool IsSteamRunningInVR()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsSteamRunningInVR();
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0000F231 File Offset: 0x0000D631
		public static void SetOverlayNotificationInset(int nHorizontalInset, int nVerticalInset)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetOverlayNotificationInset(nHorizontalInset, nVerticalInset);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0000F23F File Offset: 0x0000D63F
		public static bool IsSteamInBigPictureMode()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsSteamInBigPictureMode();
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0000F24B File Offset: 0x0000D64B
		public static void StartVRDashboard()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_StartVRDashboard();
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0000F257 File Offset: 0x0000D657
		public static bool IsVRHeadsetStreamingEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUtils_IsVRHeadsetStreamingEnabled();
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000F263 File Offset: 0x0000D663
		public static void SetVRHeadsetStreamingEnabled(bool bEnabled)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUtils_SetVRHeadsetStreamingEnabled(bEnabled);
		}
	}
}
