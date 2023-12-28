using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000223 RID: 547
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCDetails_t
	{
		// Token: 0x04000C0A RID: 3082
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000C0B RID: 3083
		public EResult m_eResult;

		// Token: 0x04000C0C RID: 3084
		public EWorkshopFileType m_eFileType;

		// Token: 0x04000C0D RID: 3085
		public AppId_t m_nCreatorAppID;

		// Token: 0x04000C0E RID: 3086
		public AppId_t m_nConsumerAppID;

		// Token: 0x04000C0F RID: 3087
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		public string m_rgchTitle;

		// Token: 0x04000C10 RID: 3088
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		public string m_rgchDescription;

		// Token: 0x04000C11 RID: 3089
		public ulong m_ulSteamIDOwner;

		// Token: 0x04000C12 RID: 3090
		public uint m_rtimeCreated;

		// Token: 0x04000C13 RID: 3091
		public uint m_rtimeUpdated;

		// Token: 0x04000C14 RID: 3092
		public uint m_rtimeAddedToUserList;

		// Token: 0x04000C15 RID: 3093
		public ERemoteStoragePublishedFileVisibility m_eVisibility;

		// Token: 0x04000C16 RID: 3094
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x04000C17 RID: 3095
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse;

		// Token: 0x04000C18 RID: 3096
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated;

		// Token: 0x04000C19 RID: 3097
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		public string m_rgchTags;

		// Token: 0x04000C1A RID: 3098
		public UGCHandle_t m_hFile;

		// Token: 0x04000C1B RID: 3099
		public UGCHandle_t m_hPreviewFile;

		// Token: 0x04000C1C RID: 3100
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_pchFileName;

		// Token: 0x04000C1D RID: 3101
		public int m_nFileSize;

		// Token: 0x04000C1E RID: 3102
		public int m_nPreviewFileSize;

		// Token: 0x04000C1F RID: 3103
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchURL;

		// Token: 0x04000C20 RID: 3104
		public uint m_unVotesUp;

		// Token: 0x04000C21 RID: 3105
		public uint m_unVotesDown;

		// Token: 0x04000C22 RID: 3106
		public float m_flScore;

		// Token: 0x04000C23 RID: 3107
		public uint m_unNumChildren;
	}
}
