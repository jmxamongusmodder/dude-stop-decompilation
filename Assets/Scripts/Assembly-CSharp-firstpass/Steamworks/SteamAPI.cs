using System;

namespace Steamworks
{
	// Token: 0x02000259 RID: 601
	public static class SteamAPI
	{
		// Token: 0x06000DCC RID: 3532 RVA: 0x000104CF File Offset: 0x0000E8CF
		public static bool InitSafe()
		{
			return SteamAPI.Init();
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000104D6 File Offset: 0x0000E8D6
		public static bool Init()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_Init();
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000104E2 File Offset: 0x0000E8E2
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_Shutdown();
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000104EE File Offset: 0x0000E8EE
		public static bool RestartAppIfNecessary(AppId_t unOwnAppID)
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_RestartAppIfNecessary(unOwnAppID);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000104FB File Offset: 0x0000E8FB
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_ReleaseCurrentThreadMemory();
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00010507 File Offset: 0x0000E907
		public static void RunCallbacks()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_RunCallbacks();
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00010513 File Offset: 0x0000E913
		public static bool IsSteamRunning()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_IsSteamRunning();
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0001051F File Offset: 0x0000E91F
		public static HSteamUser GetHSteamUserCurrent()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.Steam_GetHSteamUserCurrent();
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00010530 File Offset: 0x0000E930
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamAPI_GetHSteamPipe();
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00010541 File Offset: 0x0000E941
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamAPI_GetHSteamUser();
		}
	}
}
