using System;

namespace Steamworks
{
	// Token: 0x0200025A RID: 602
	public static class GameServer
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00010554 File Offset: 0x0000E954
		public static bool Init(uint unIP, ushort usSteamPort, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, string pchVersionString)
		{
			InteropHelp.TestIfPlatformSupported();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchVersionString))
			{
				result = NativeMethods.SteamGameServer_Init(unIP, usSteamPort, usGamePort, usQueryPort, eServerMode, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000105A0 File Offset: 0x0000E9A0
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_Shutdown();
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000105AC File Offset: 0x0000E9AC
		public static void RunCallbacks()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_RunCallbacks();
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000105B8 File Offset: 0x0000E9B8
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_ReleaseCurrentThreadMemory();
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000105C4 File Offset: 0x0000E9C4
		public static bool BSecure()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamGameServer_BSecure();
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000105D0 File Offset: 0x0000E9D0
		public static CSteamID GetSteamID()
		{
			InteropHelp.TestIfPlatformSupported();
			return (CSteamID)NativeMethods.SteamGameServer_GetSteamID();
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000105E1 File Offset: 0x0000E9E1
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamGameServer_GetHSteamPipe();
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000105F2 File Offset: 0x0000E9F2
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamGameServer_GetHSteamUser();
		}
	}
}
