using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200010A RID: 266
	public static class SteamGameServerUGC
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0000AE2A File Offset: 0x0000922A
		public static UGCQueryHandle_t CreateQueryUserUGCRequest(AccountID_t unAccountID, EUserUGCList eListType, EUGCMatchingUGCType eMatchingUGCType, EUserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (UGCQueryHandle_t)NativeMethods.ISteamGameServerUGC_CreateQueryUserUGCRequest(unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000AE45 File Offset: 0x00009245
		public static UGCQueryHandle_t CreateQueryAllUGCRequest(EUGCQuery eQueryType, EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (UGCQueryHandle_t)NativeMethods.ISteamGameServerUGC_CreateQueryAllUGCRequest(eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000AE5C File Offset: 0x0000925C
		public static UGCQueryHandle_t CreateQueryUGCDetailsRequest(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (UGCQueryHandle_t)NativeMethods.ISteamGameServerUGC_CreateQueryUGCDetailsRequest(pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000AE6F File Offset: 0x0000926F
		public static SteamAPICall_t SendQueryUGCRequest(UGCQueryHandle_t handle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_SendQueryUGCRequest(handle);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000AE81 File Offset: 0x00009281
		public static bool GetQueryUGCResult(UGCQueryHandle_t handle, uint index, out SteamUGCDetails_t pDetails)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetQueryUGCResult(handle, index, out pDetails);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000AE90 File Offset: 0x00009290
		public static bool GetQueryUGCPreviewURL(UGCQueryHandle_t handle, uint index, out string pchURL, uint cchURLSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchURLSize);
			bool flag = NativeMethods.ISteamGameServerUGC_GetQueryUGCPreviewURL(handle, index, intPtr, cchURLSize);
			pchURL = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000AED0 File Offset: 0x000092D0
		public static bool GetQueryUGCMetadata(UGCQueryHandle_t handle, uint index, out string pchMetadata, uint cchMetadatasize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchMetadatasize);
			bool flag = NativeMethods.ISteamGameServerUGC_GetQueryUGCMetadata(handle, index, intPtr, cchMetadatasize);
			pchMetadata = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000AF0E File Offset: 0x0000930E
		public static bool GetQueryUGCChildren(UGCQueryHandle_t handle, uint index, PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetQueryUGCChildren(handle, index, pvecPublishedFileID, cMaxEntries);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000AF1E File Offset: 0x0000931E
		public static bool GetQueryUGCStatistic(UGCQueryHandle_t handle, uint index, EItemStatistic eStatType, out ulong pStatValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetQueryUGCStatistic(handle, index, eStatType, out pStatValue);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000AF2E File Offset: 0x0000932E
		public static uint GetQueryUGCNumAdditionalPreviews(UGCQueryHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetQueryUGCNumAdditionalPreviews(handle, index);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000AF3C File Offset: 0x0000933C
		public static bool GetQueryUGCAdditionalPreview(UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, uint cchURLSize, out string pchOriginalFileName, uint cchOriginalFileNameSize, out EItemPreviewType pPreviewType)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchURLSize);
			IntPtr intPtr2 = Marshal.AllocHGlobal((int)cchOriginalFileNameSize);
			bool flag = NativeMethods.ISteamGameServerUGC_GetQueryUGCAdditionalPreview(handle, index, previewIndex, intPtr, cchURLSize, intPtr2, cchOriginalFileNameSize, out pPreviewType);
			pchURLOrVideoID = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			pchOriginalFileName = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr2));
			Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000AFA5 File Offset: 0x000093A5
		public static uint GetQueryUGCNumKeyValueTags(UGCQueryHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetQueryUGCNumKeyValueTags(handle, index);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000AFB4 File Offset: 0x000093B4
		public static bool GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, uint cchKeySize, out string pchValue, uint cchValueSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchKeySize);
			IntPtr intPtr2 = Marshal.AllocHGlobal((int)cchValueSize);
			bool flag = NativeMethods.ISteamGameServerUGC_GetQueryUGCKeyValueTag(handle, index, keyValueTagIndex, intPtr, cchKeySize, intPtr2, cchValueSize);
			pchKey = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			pchValue = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr2));
			Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000B01B File Offset: 0x0000941B
		public static bool ReleaseQueryUGCRequest(UGCQueryHandle_t handle)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_ReleaseQueryUGCRequest(handle);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000B028 File Offset: 0x00009428
		public static bool AddRequiredTag(UGCQueryHandle_t handle, string pTagName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pTagName))
			{
				result = NativeMethods.ISteamGameServerUGC_AddRequiredTag(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000B06C File Offset: 0x0000946C
		public static bool AddExcludedTag(UGCQueryHandle_t handle, string pTagName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pTagName))
			{
				result = NativeMethods.ISteamGameServerUGC_AddExcludedTag(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000B0B0 File Offset: 0x000094B0
		public static bool SetReturnOnlyIDs(UGCQueryHandle_t handle, bool bReturnOnlyIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetReturnOnlyIDs(handle, bReturnOnlyIDs);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000B0BE File Offset: 0x000094BE
		public static bool SetReturnKeyValueTags(UGCQueryHandle_t handle, bool bReturnKeyValueTags)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetReturnKeyValueTags(handle, bReturnKeyValueTags);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000B0CC File Offset: 0x000094CC
		public static bool SetReturnLongDescription(UGCQueryHandle_t handle, bool bReturnLongDescription)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetReturnLongDescription(handle, bReturnLongDescription);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000B0DA File Offset: 0x000094DA
		public static bool SetReturnMetadata(UGCQueryHandle_t handle, bool bReturnMetadata)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetReturnMetadata(handle, bReturnMetadata);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000B0E8 File Offset: 0x000094E8
		public static bool SetReturnChildren(UGCQueryHandle_t handle, bool bReturnChildren)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetReturnChildren(handle, bReturnChildren);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000B0F6 File Offset: 0x000094F6
		public static bool SetReturnAdditionalPreviews(UGCQueryHandle_t handle, bool bReturnAdditionalPreviews)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetReturnAdditionalPreviews(handle, bReturnAdditionalPreviews);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000B104 File Offset: 0x00009504
		public static bool SetReturnTotalOnly(UGCQueryHandle_t handle, bool bReturnTotalOnly)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetReturnTotalOnly(handle, bReturnTotalOnly);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000B112 File Offset: 0x00009512
		public static bool SetReturnPlaytimeStats(UGCQueryHandle_t handle, uint unDays)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetReturnPlaytimeStats(handle, unDays);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000B120 File Offset: 0x00009520
		public static bool SetLanguage(UGCQueryHandle_t handle, string pchLanguage)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLanguage))
			{
				result = NativeMethods.ISteamGameServerUGC_SetLanguage(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000B164 File Offset: 0x00009564
		public static bool SetAllowCachedResponse(UGCQueryHandle_t handle, uint unMaxAgeSeconds)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetAllowCachedResponse(handle, unMaxAgeSeconds);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000B174 File Offset: 0x00009574
		public static bool SetCloudFileNameFilter(UGCQueryHandle_t handle, string pMatchCloudFileName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pMatchCloudFileName))
			{
				result = NativeMethods.ISteamGameServerUGC_SetCloudFileNameFilter(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000B1B8 File Offset: 0x000095B8
		public static bool SetMatchAnyTag(UGCQueryHandle_t handle, bool bMatchAnyTag)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetMatchAnyTag(handle, bMatchAnyTag);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000B1C8 File Offset: 0x000095C8
		public static bool SetSearchText(UGCQueryHandle_t handle, string pSearchText)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pSearchText))
			{
				result = NativeMethods.ISteamGameServerUGC_SetSearchText(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000B20C File Offset: 0x0000960C
		public static bool SetRankedByTrendDays(UGCQueryHandle_t handle, uint unDays)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetRankedByTrendDays(handle, unDays);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000B21C File Offset: 0x0000961C
		public static bool AddRequiredKeyValueTag(UGCQueryHandle_t handle, string pKey, string pValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pValue))
				{
					result = NativeMethods.ISteamGameServerUGC_AddRequiredKeyValueTag(handle, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000B284 File Offset: 0x00009684
		public static SteamAPICall_t RequestUGCDetails(PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_RequestUGCDetails(nPublishedFileID, unMaxAgeSeconds);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000B297 File Offset: 0x00009697
		public static SteamAPICall_t CreateItem(AppId_t nConsumerAppId, EWorkshopFileType eFileType)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_CreateItem(nConsumerAppId, eFileType);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000B2AA File Offset: 0x000096AA
		public static UGCUpdateHandle_t StartItemUpdate(AppId_t nConsumerAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (UGCUpdateHandle_t)NativeMethods.ISteamGameServerUGC_StartItemUpdate(nConsumerAppId, nPublishedFileID);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000B2C0 File Offset: 0x000096C0
		public static bool SetItemTitle(UGCUpdateHandle_t handle, string pchTitle)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchTitle))
			{
				result = NativeMethods.ISteamGameServerUGC_SetItemTitle(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000B304 File Offset: 0x00009704
		public static bool SetItemDescription(UGCUpdateHandle_t handle, string pchDescription)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				result = NativeMethods.ISteamGameServerUGC_SetItemDescription(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000B348 File Offset: 0x00009748
		public static bool SetItemUpdateLanguage(UGCUpdateHandle_t handle, string pchLanguage)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLanguage))
			{
				result = NativeMethods.ISteamGameServerUGC_SetItemUpdateLanguage(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000B38C File Offset: 0x0000978C
		public static bool SetItemMetadata(UGCUpdateHandle_t handle, string pchMetaData)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchMetaData))
			{
				result = NativeMethods.ISteamGameServerUGC_SetItemMetadata(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000B3D0 File Offset: 0x000097D0
		public static bool SetItemVisibility(UGCUpdateHandle_t handle, ERemoteStoragePublishedFileVisibility eVisibility)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetItemVisibility(handle, eVisibility);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000B3DE File Offset: 0x000097DE
		public static bool SetItemTags(UGCUpdateHandle_t updateHandle, IList<string> pTags)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_SetItemTags(updateHandle, new InteropHelp.SteamParamStringArray(pTags));
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000B3F8 File Offset: 0x000097F8
		public static bool SetItemContent(UGCUpdateHandle_t handle, string pszContentFolder)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszContentFolder))
			{
				result = NativeMethods.ISteamGameServerUGC_SetItemContent(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000B43C File Offset: 0x0000983C
		public static bool SetItemPreview(UGCUpdateHandle_t handle, string pszPreviewFile)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamGameServerUGC_SetItemPreview(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000B480 File Offset: 0x00009880
		public static bool RemoveItemKeyValueTags(UGCUpdateHandle_t handle, string pchKey)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = NativeMethods.ISteamGameServerUGC_RemoveItemKeyValueTags(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000B4C4 File Offset: 0x000098C4
		public static bool AddItemKeyValueTag(UGCUpdateHandle_t handle, string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					result = NativeMethods.ISteamGameServerUGC_AddItemKeyValueTag(handle, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000B52C File Offset: 0x0000992C
		public static bool AddItemPreviewFile(UGCUpdateHandle_t handle, string pszPreviewFile, EItemPreviewType type)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamGameServerUGC_AddItemPreviewFile(handle, utf8StringHandle, type);
			}
			return result;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000B574 File Offset: 0x00009974
		public static bool AddItemPreviewVideo(UGCUpdateHandle_t handle, string pszVideoID)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszVideoID))
			{
				result = NativeMethods.ISteamGameServerUGC_AddItemPreviewVideo(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0000B5B8 File Offset: 0x000099B8
		public static bool UpdateItemPreviewFile(UGCUpdateHandle_t handle, uint index, string pszPreviewFile)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamGameServerUGC_UpdateItemPreviewFile(handle, index, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0000B600 File Offset: 0x00009A00
		public static bool UpdateItemPreviewVideo(UGCUpdateHandle_t handle, uint index, string pszVideoID)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszVideoID))
			{
				result = NativeMethods.ISteamGameServerUGC_UpdateItemPreviewVideo(handle, index, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0000B648 File Offset: 0x00009A48
		public static bool RemoveItemPreview(UGCUpdateHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_RemoveItemPreview(handle, index);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0000B658 File Offset: 0x00009A58
		public static SteamAPICall_t SubmitItemUpdate(UGCUpdateHandle_t handle, string pchChangeNote)
		{
			InteropHelp.TestIfAvailableGameServer();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchChangeNote))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_SubmitItemUpdate(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000B6A4 File Offset: 0x00009AA4
		public static EItemUpdateStatus GetItemUpdateProgress(UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetItemUpdateProgress(handle, out punBytesProcessed, out punBytesTotal);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000B6B3 File Offset: 0x00009AB3
		public static SteamAPICall_t SetUserItemVote(PublishedFileId_t nPublishedFileID, bool bVoteUp)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_SetUserItemVote(nPublishedFileID, bVoteUp);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000B6C6 File Offset: 0x00009AC6
		public static SteamAPICall_t GetUserItemVote(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_GetUserItemVote(nPublishedFileID);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0000B6D8 File Offset: 0x00009AD8
		public static SteamAPICall_t AddItemToFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_AddItemToFavorites(nAppId, nPublishedFileID);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0000B6EB File Offset: 0x00009AEB
		public static SteamAPICall_t RemoveItemFromFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_RemoveItemFromFavorites(nAppId, nPublishedFileID);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0000B6FE File Offset: 0x00009AFE
		public static SteamAPICall_t SubscribeItem(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_SubscribeItem(nPublishedFileID);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0000B710 File Offset: 0x00009B10
		public static SteamAPICall_t UnsubscribeItem(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_UnsubscribeItem(nPublishedFileID);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0000B722 File Offset: 0x00009B22
		public static uint GetNumSubscribedItems()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetNumSubscribedItems();
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000B72E File Offset: 0x00009B2E
		public static uint GetSubscribedItems(PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetSubscribedItems(pvecPublishedFileID, cMaxEntries);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0000B73C File Offset: 0x00009B3C
		public static uint GetItemState(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetItemState(nPublishedFileID);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0000B74C File Offset: 0x00009B4C
		public static bool GetItemInstallInfo(PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, out string pchFolder, uint cchFolderSize, out uint punTimeStamp)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchFolderSize);
			bool flag = NativeMethods.ISteamGameServerUGC_GetItemInstallInfo(nPublishedFileID, out punSizeOnDisk, intPtr, cchFolderSize, out punTimeStamp);
			pchFolder = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0000B78C File Offset: 0x00009B8C
		public static bool GetItemDownloadInfo(PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_GetItemDownloadInfo(nPublishedFileID, out punBytesDownloaded, out punBytesTotal);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0000B79B File Offset: 0x00009B9B
		public static bool DownloadItem(PublishedFileId_t nPublishedFileID, bool bHighPriority)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamGameServerUGC_DownloadItem(nPublishedFileID, bHighPriority);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0000B7AC File Offset: 0x00009BAC
		public static bool BInitWorkshopForGameServer(DepotId_t unWorkshopDepotID, string pszFolder)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszFolder))
			{
				result = NativeMethods.ISteamGameServerUGC_BInitWorkshopForGameServer(unWorkshopDepotID, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0000B7F0 File Offset: 0x00009BF0
		public static void SuspendDownloads(bool bSuspend)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamGameServerUGC_SuspendDownloads(bSuspend);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000B7FD File Offset: 0x00009BFD
		public static SteamAPICall_t StartPlaytimeTracking(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_StartPlaytimeTracking(pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0000B810 File Offset: 0x00009C10
		public static SteamAPICall_t StopPlaytimeTracking(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_StopPlaytimeTracking(pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0000B823 File Offset: 0x00009C23
		public static SteamAPICall_t StopPlaytimeTrackingForAllItems()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_StopPlaytimeTrackingForAllItems();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0000B834 File Offset: 0x00009C34
		public static SteamAPICall_t AddDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_AddDependency(nParentPublishedFileID, nChildPublishedFileID);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0000B847 File Offset: 0x00009C47
		public static SteamAPICall_t RemoveDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerUGC_RemoveDependency(nParentPublishedFileID, nChildPublishedFileID);
		}
	}
}
