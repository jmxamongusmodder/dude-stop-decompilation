using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x0200025C RID: 604
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 372)]
	public class gameserveritem_t
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x000106A7 File Offset: 0x0000EAA7
		public string GetGameDir()
		{
			return Encoding.UTF8.GetString(this.m_szGameDir, 0, Array.IndexOf<byte>(this.m_szGameDir, 0));
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x000106C6 File Offset: 0x0000EAC6
		public void SetGameDir(string dir)
		{
			this.m_szGameDir = Encoding.UTF8.GetBytes(dir + '\0');
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x000106E4 File Offset: 0x0000EAE4
		public string GetMap()
		{
			return Encoding.UTF8.GetString(this.m_szMap, 0, Array.IndexOf<byte>(this.m_szMap, 0));
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00010703 File Offset: 0x0000EB03
		public void SetMap(string map)
		{
			this.m_szMap = Encoding.UTF8.GetBytes(map + '\0');
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00010721 File Offset: 0x0000EB21
		public string GetGameDescription()
		{
			return Encoding.UTF8.GetString(this.m_szGameDescription, 0, Array.IndexOf<byte>(this.m_szGameDescription, 0));
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00010740 File Offset: 0x0000EB40
		public void SetGameDescription(string desc)
		{
			this.m_szGameDescription = Encoding.UTF8.GetBytes(desc + '\0');
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0001075E File Offset: 0x0000EB5E
		public string GetServerName()
		{
			if (this.m_szServerName[0] == 0)
			{
				return this.m_NetAdr.GetConnectionAddressString();
			}
			return Encoding.UTF8.GetString(this.m_szServerName, 0, Array.IndexOf<byte>(this.m_szServerName, 0));
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00010796 File Offset: 0x0000EB96
		public void SetServerName(string name)
		{
			this.m_szServerName = Encoding.UTF8.GetBytes(name + '\0');
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000107B4 File Offset: 0x0000EBB4
		public string GetGameTags()
		{
			return Encoding.UTF8.GetString(this.m_szGameTags, 0, Array.IndexOf<byte>(this.m_szGameTags, 0));
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000107D3 File Offset: 0x0000EBD3
		public void SetGameTags(string tags)
		{
			this.m_szGameTags = Encoding.UTF8.GetBytes(tags + '\0');
		}

		// Token: 0x04000C7A RID: 3194
		public servernetadr_t m_NetAdr;

		// Token: 0x04000C7B RID: 3195
		public int m_nPing;

		// Token: 0x04000C7C RID: 3196
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHadSuccessfulResponse;

		// Token: 0x04000C7D RID: 3197
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDoNotRefresh;

		// Token: 0x04000C7E RID: 3198
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szGameDir;

		// Token: 0x04000C7F RID: 3199
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szMap;

		// Token: 0x04000C80 RID: 3200
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szGameDescription;

		// Token: 0x04000C81 RID: 3201
		public uint m_nAppID;

		// Token: 0x04000C82 RID: 3202
		public int m_nPlayers;

		// Token: 0x04000C83 RID: 3203
		public int m_nMaxPlayers;

		// Token: 0x04000C84 RID: 3204
		public int m_nBotPlayers;

		// Token: 0x04000C85 RID: 3205
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPassword;

		// Token: 0x04000C86 RID: 3206
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSecure;

		// Token: 0x04000C87 RID: 3207
		public uint m_ulTimeLastPlayed;

		// Token: 0x04000C88 RID: 3208
		public int m_nServerVersion;

		// Token: 0x04000C89 RID: 3209
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szServerName;

		// Token: 0x04000C8A RID: 3210
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szGameTags;

		// Token: 0x04000C8B RID: 3211
		public CSteamID m_steamID;
	}
}
