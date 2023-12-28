using System;

namespace Steamworks
{
	// Token: 0x02000115 RID: 277
	public static class SteamScreenshots
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x0000D934 File Offset: 0x0000BD34
		public static ScreenshotHandle WriteScreenshot(byte[] pubRGB, uint cubRGB, int nWidth, int nHeight)
		{
			InteropHelp.TestIfAvailableClient();
			return (ScreenshotHandle)NativeMethods.ISteamScreenshots_WriteScreenshot(pubRGB, cubRGB, nWidth, nHeight);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0000D94C File Offset: 0x0000BD4C
		public static ScreenshotHandle AddScreenshotToLibrary(string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight)
		{
			InteropHelp.TestIfAvailableClient();
			ScreenshotHandle result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFilename))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchThumbnailFilename))
				{
					result = (ScreenshotHandle)NativeMethods.ISteamScreenshots_AddScreenshotToLibrary(utf8StringHandle, utf8StringHandle2, nWidth, nHeight);
				}
			}
			return result;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0000D9B8 File Offset: 0x0000BDB8
		public static void TriggerScreenshot()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamScreenshots_TriggerScreenshot();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0000D9C4 File Offset: 0x0000BDC4
		public static void HookScreenshots(bool bHook)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamScreenshots_HookScreenshots(bHook);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0000D9D4 File Offset: 0x0000BDD4
		public static bool SetLocation(ScreenshotHandle hScreenshot, string pchLocation)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLocation))
			{
				result = NativeMethods.ISteamScreenshots_SetLocation(hScreenshot, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0000DA18 File Offset: 0x0000BE18
		public static bool TagUser(ScreenshotHandle hScreenshot, CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamScreenshots_TagUser(hScreenshot, steamID);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0000DA26 File Offset: 0x0000BE26
		public static bool TagPublishedFile(ScreenshotHandle hScreenshot, PublishedFileId_t unPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamScreenshots_TagPublishedFile(hScreenshot, unPublishedFileID);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0000DA34 File Offset: 0x0000BE34
		public static bool IsScreenshotsHooked()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamScreenshots_IsScreenshotsHooked();
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0000DA40 File Offset: 0x0000BE40
		public static ScreenshotHandle AddVRScreenshotToLibrary(EVRScreenshotType eType, string pchFilename, string pchVRFilename)
		{
			InteropHelp.TestIfAvailableClient();
			ScreenshotHandle result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchFilename))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchVRFilename))
				{
					result = (ScreenshotHandle)NativeMethods.ISteamScreenshots_AddVRScreenshotToLibrary(eType, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}
	}
}
