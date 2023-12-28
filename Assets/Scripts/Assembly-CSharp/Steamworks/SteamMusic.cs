using System;

namespace Steamworks
{
	// Token: 0x02000111 RID: 273
	public static class SteamMusic
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x0000CB0A File Offset: 0x0000AF0A
		public static bool BIsEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_BIsEnabled();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0000CB16 File Offset: 0x0000AF16
		public static bool BIsPlaying()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_BIsPlaying();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0000CB22 File Offset: 0x0000AF22
		public static AudioPlayback_Status GetPlaybackStatus()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_GetPlaybackStatus();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0000CB2E File Offset: 0x0000AF2E
		public static void Play()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_Play();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0000CB3A File Offset: 0x0000AF3A
		public static void Pause()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_Pause();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0000CB46 File Offset: 0x0000AF46
		public static void PlayPrevious()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_PlayPrevious();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0000CB52 File Offset: 0x0000AF52
		public static void PlayNext()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_PlayNext();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000CB5E File Offset: 0x0000AF5E
		public static void SetVolume(float flVolume)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_SetVolume(flVolume);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000CB6B File Offset: 0x0000AF6B
		public static float GetVolume()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_GetVolume();
		}
	}
}
