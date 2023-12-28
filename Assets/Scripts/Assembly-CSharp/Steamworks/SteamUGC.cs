using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000116 RID: 278
	public static class SteamUGC
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0000DAAC File Offset: 0x0000BEAC
		public static UGCQueryHandle_t CreateQueryUserUGCRequest(AccountID_t unAccountID, EUserUGCList eListType, EUGCMatchingUGCType eMatchingUGCType, EUserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
		{
			InteropHelp.TestIfAvailableClient();
			return (UGCQueryHandle_t)NativeMethods.ISteamUGC_CreateQueryUserUGCRequest(unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0000DAC7 File Offset: 0x0000BEC7
		public static UGCQueryHandle_t CreateQueryAllUGCRequest(EUGCQuery eQueryType, EUGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
		{
			InteropHelp.TestIfAvailableClient();
			return (UGCQueryHandle_t)NativeMethods.ISteamUGC_CreateQueryAllUGCRequest(eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000DADE File Offset: 0x0000BEDE
		public static UGCQueryHandle_t CreateQueryUGCDetailsRequest(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableClient();
			return (UGCQueryHandle_t)NativeMethods.ISteamUGC_CreateQueryUGCDetailsRequest(pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000DAF1 File Offset: 0x0000BEF1
		public static SteamAPICall_t SendQueryUGCRequest(UGCQueryHandle_t handle)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_SendQueryUGCRequest(handle);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0000DB03 File Offset: 0x0000BF03
		public static bool GetQueryUGCResult(UGCQueryHandle_t handle, uint index, out SteamUGCDetails_t pDetails)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetQueryUGCResult(handle, index, out pDetails);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000DB14 File Offset: 0x0000BF14
		public static bool GetQueryUGCPreviewURL(UGCQueryHandle_t handle, uint index, out string pchURL, uint cchURLSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchURLSize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCPreviewURL(handle, index, intPtr, cchURLSize);
			pchURL = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0000DB54 File Offset: 0x0000BF54
		public static bool GetQueryUGCMetadata(UGCQueryHandle_t handle, uint index, out string pchMetadata, uint cchMetadatasize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchMetadatasize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCMetadata(handle, index, intPtr, cchMetadatasize);
			pchMetadata = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0000DB92 File Offset: 0x0000BF92
		public static bool GetQueryUGCChildren(UGCQueryHandle_t handle, uint index, PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetQueryUGCChildren(handle, index, pvecPublishedFileID, cMaxEntries);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0000DBA2 File Offset: 0x0000BFA2
		public static bool GetQueryUGCStatistic(UGCQueryHandle_t handle, uint index, EItemStatistic eStatType, out ulong pStatValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetQueryUGCStatistic(handle, index, eStatType, out pStatValue);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000DBB2 File Offset: 0x0000BFB2
		public static uint GetQueryUGCNumAdditionalPreviews(UGCQueryHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetQueryUGCNumAdditionalPreviews(handle, index);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000DBC0 File Offset: 0x0000BFC0
		public static bool GetQueryUGCAdditionalPreview(UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, uint cchURLSize, out string pchOriginalFileName, uint cchOriginalFileNameSize, out EItemPreviewType pPreviewType)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchURLSize);
			IntPtr intPtr2 = Marshal.AllocHGlobal((int)cchOriginalFileNameSize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCAdditionalPreview(handle, index, previewIndex, intPtr, cchURLSize, intPtr2, cchOriginalFileNameSize, out pPreviewType);
			pchURLOrVideoID = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			pchOriginalFileName = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr2));
			Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000DC29 File Offset: 0x0000C029
		public static uint GetQueryUGCNumKeyValueTags(UGCQueryHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetQueryUGCNumKeyValueTags(handle, index);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0000DC38 File Offset: 0x0000C038
		public static bool GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, uint cchKeySize, out string pchValue, uint cchValueSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchKeySize);
			IntPtr intPtr2 = Marshal.AllocHGlobal((int)cchValueSize);
			bool flag = NativeMethods.ISteamUGC_GetQueryUGCKeyValueTag(handle, index, keyValueTagIndex, intPtr, cchKeySize, intPtr2, cchValueSize);
			pchKey = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			pchValue = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr2));
			Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0000DC9F File Offset: 0x0000C09F
		public static bool ReleaseQueryUGCRequest(UGCQueryHandle_t handle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_ReleaseQueryUGCRequest(handle);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000DCAC File Offset: 0x0000C0AC
		public static bool AddRequiredTag(UGCQueryHandle_t handle, string pTagName)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pTagName))
			{
				result = NativeMethods.ISteamUGC_AddRequiredTag(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0000DCF0 File Offset: 0x0000C0F0
		public static bool AddExcludedTag(UGCQueryHandle_t handle, string pTagName)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pTagName))
			{
				result = NativeMethods.ISteamUGC_AddExcludedTag(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000DD34 File Offset: 0x0000C134
		public static bool SetReturnOnlyIDs(UGCQueryHandle_t handle, bool bReturnOnlyIDs)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetReturnOnlyIDs(handle, bReturnOnlyIDs);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000DD42 File Offset: 0x0000C142
		public static bool SetReturnKeyValueTags(UGCQueryHandle_t handle, bool bReturnKeyValueTags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetReturnKeyValueTags(handle, bReturnKeyValueTags);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000DD50 File Offset: 0x0000C150
		public static bool SetReturnLongDescription(UGCQueryHandle_t handle, bool bReturnLongDescription)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetReturnLongDescription(handle, bReturnLongDescription);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0000DD5E File Offset: 0x0000C15E
		public static bool SetReturnMetadata(UGCQueryHandle_t handle, bool bReturnMetadata)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetReturnMetadata(handle, bReturnMetadata);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0000DD6C File Offset: 0x0000C16C
		public static bool SetReturnChildren(UGCQueryHandle_t handle, bool bReturnChildren)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetReturnChildren(handle, bReturnChildren);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0000DD7A File Offset: 0x0000C17A
		public static bool SetReturnAdditionalPreviews(UGCQueryHandle_t handle, bool bReturnAdditionalPreviews)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetReturnAdditionalPreviews(handle, bReturnAdditionalPreviews);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0000DD88 File Offset: 0x0000C188
		public static bool SetReturnTotalOnly(UGCQueryHandle_t handle, bool bReturnTotalOnly)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetReturnTotalOnly(handle, bReturnTotalOnly);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0000DD96 File Offset: 0x0000C196
		public static bool SetReturnPlaytimeStats(UGCQueryHandle_t handle, uint unDays)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetReturnPlaytimeStats(handle, unDays);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0000DDA4 File Offset: 0x0000C1A4
		public static bool SetLanguage(UGCQueryHandle_t handle, string pchLanguage)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLanguage))
			{
				result = NativeMethods.ISteamUGC_SetLanguage(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0000DDE8 File Offset: 0x0000C1E8
		public static bool SetAllowCachedResponse(UGCQueryHandle_t handle, uint unMaxAgeSeconds)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetAllowCachedResponse(handle, unMaxAgeSeconds);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0000DDF8 File Offset: 0x0000C1F8
		public static bool SetCloudFileNameFilter(UGCQueryHandle_t handle, string pMatchCloudFileName)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pMatchCloudFileName))
			{
				result = NativeMethods.ISteamUGC_SetCloudFileNameFilter(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000DE3C File Offset: 0x0000C23C
		public static bool SetMatchAnyTag(UGCQueryHandle_t handle, bool bMatchAnyTag)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetMatchAnyTag(handle, bMatchAnyTag);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000DE4C File Offset: 0x0000C24C
		public static bool SetSearchText(UGCQueryHandle_t handle, string pSearchText)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pSearchText))
			{
				result = NativeMethods.ISteamUGC_SetSearchText(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0000DE90 File Offset: 0x0000C290
		public static bool SetRankedByTrendDays(UGCQueryHandle_t handle, uint unDays)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetRankedByTrendDays(handle, unDays);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0000DEA0 File Offset: 0x0000C2A0
		public static bool AddRequiredKeyValueTag(UGCQueryHandle_t handle, string pKey, string pValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pValue))
				{
					result = NativeMethods.ISteamUGC_AddRequiredKeyValueTag(handle, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0000DF08 File Offset: 0x0000C308
		public static SteamAPICall_t RequestUGCDetails(PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_RequestUGCDetails(nPublishedFileID, unMaxAgeSeconds);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0000DF1B File Offset: 0x0000C31B
		public static SteamAPICall_t CreateItem(AppId_t nConsumerAppId, EWorkshopFileType eFileType)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_CreateItem(nConsumerAppId, eFileType);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000DF2E File Offset: 0x0000C32E
		public static UGCUpdateHandle_t StartItemUpdate(AppId_t nConsumerAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return (UGCUpdateHandle_t)NativeMethods.ISteamUGC_StartItemUpdate(nConsumerAppId, nPublishedFileID);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000DF44 File Offset: 0x0000C344
		public static bool SetItemTitle(UGCUpdateHandle_t handle, string pchTitle)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchTitle))
			{
				result = NativeMethods.ISteamUGC_SetItemTitle(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000DF88 File Offset: 0x0000C388
		public static bool SetItemDescription(UGCUpdateHandle_t handle, string pchDescription)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				result = NativeMethods.ISteamUGC_SetItemDescription(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0000DFCC File Offset: 0x0000C3CC
		public static bool SetItemUpdateLanguage(UGCUpdateHandle_t handle, string pchLanguage)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchLanguage))
			{
				result = NativeMethods.ISteamUGC_SetItemUpdateLanguage(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0000E010 File Offset: 0x0000C410
		public static bool SetItemMetadata(UGCUpdateHandle_t handle, string pchMetaData)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchMetaData))
			{
				result = NativeMethods.ISteamUGC_SetItemMetadata(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0000E054 File Offset: 0x0000C454
		public static bool SetItemVisibility(UGCUpdateHandle_t handle, ERemoteStoragePublishedFileVisibility eVisibility)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetItemVisibility(handle, eVisibility);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000E062 File Offset: 0x0000C462
		public static bool SetItemTags(UGCUpdateHandle_t updateHandle, IList<string> pTags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_SetItemTags(updateHandle, new InteropHelp.SteamParamStringArray(pTags));
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0000E07C File Offset: 0x0000C47C
		public static bool SetItemContent(UGCUpdateHandle_t handle, string pszContentFolder)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszContentFolder))
			{
				result = NativeMethods.ISteamUGC_SetItemContent(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000E0C0 File Offset: 0x0000C4C0
		public static bool SetItemPreview(UGCUpdateHandle_t handle, string pszPreviewFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamUGC_SetItemPreview(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000E104 File Offset: 0x0000C504
		public static bool RemoveItemKeyValueTags(UGCUpdateHandle_t handle, string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = NativeMethods.ISteamUGC_RemoveItemKeyValueTags(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0000E148 File Offset: 0x0000C548
		public static bool AddItemKeyValueTag(UGCUpdateHandle_t handle, string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					result = NativeMethods.ISteamUGC_AddItemKeyValueTag(handle, utf8StringHandle, utf8StringHandle2);
				}
			}
			return result;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0000E1B0 File Offset: 0x0000C5B0
		public static bool AddItemPreviewFile(UGCUpdateHandle_t handle, string pszPreviewFile, EItemPreviewType type)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamUGC_AddItemPreviewFile(handle, utf8StringHandle, type);
			}
			return result;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0000E1F8 File Offset: 0x0000C5F8
		public static bool AddItemPreviewVideo(UGCUpdateHandle_t handle, string pszVideoID)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszVideoID))
			{
				result = NativeMethods.ISteamUGC_AddItemPreviewVideo(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000E23C File Offset: 0x0000C63C
		public static bool UpdateItemPreviewFile(UGCUpdateHandle_t handle, uint index, string pszPreviewFile)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszPreviewFile))
			{
				result = NativeMethods.ISteamUGC_UpdateItemPreviewFile(handle, index, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000E284 File Offset: 0x0000C684
		public static bool UpdateItemPreviewVideo(UGCUpdateHandle_t handle, uint index, string pszVideoID)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszVideoID))
			{
				result = NativeMethods.ISteamUGC_UpdateItemPreviewVideo(handle, index, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0000E2CC File Offset: 0x0000C6CC
		public static bool RemoveItemPreview(UGCUpdateHandle_t handle, uint index)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_RemoveItemPreview(handle, index);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000E2DC File Offset: 0x0000C6DC
		public static SteamAPICall_t SubmitItemUpdate(UGCUpdateHandle_t handle, string pchChangeNote)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchChangeNote))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamUGC_SubmitItemUpdate(handle, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0000E328 File Offset: 0x0000C728
		public static EItemUpdateStatus GetItemUpdateProgress(UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetItemUpdateProgress(handle, out punBytesProcessed, out punBytesTotal);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0000E337 File Offset: 0x0000C737
		public static SteamAPICall_t SetUserItemVote(PublishedFileId_t nPublishedFileID, bool bVoteUp)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_SetUserItemVote(nPublishedFileID, bVoteUp);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0000E34A File Offset: 0x0000C74A
		public static SteamAPICall_t GetUserItemVote(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_GetUserItemVote(nPublishedFileID);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0000E35C File Offset: 0x0000C75C
		public static SteamAPICall_t AddItemToFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_AddItemToFavorites(nAppId, nPublishedFileID);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0000E36F File Offset: 0x0000C76F
		public static SteamAPICall_t RemoveItemFromFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_RemoveItemFromFavorites(nAppId, nPublishedFileID);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0000E382 File Offset: 0x0000C782
		public static SteamAPICall_t SubscribeItem(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_SubscribeItem(nPublishedFileID);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0000E394 File Offset: 0x0000C794
		public static SteamAPICall_t UnsubscribeItem(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_UnsubscribeItem(nPublishedFileID);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0000E3A6 File Offset: 0x0000C7A6
		public static uint GetNumSubscribedItems()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetNumSubscribedItems();
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0000E3B2 File Offset: 0x0000C7B2
		public static uint GetSubscribedItems(PublishedFileId_t[] pvecPublishedFileID, uint cMaxEntries)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetSubscribedItems(pvecPublishedFileID, cMaxEntries);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0000E3C0 File Offset: 0x0000C7C0
		public static uint GetItemState(PublishedFileId_t nPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetItemState(nPublishedFileID);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0000E3D0 File Offset: 0x0000C7D0
		public static bool GetItemInstallInfo(PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, out string pchFolder, uint cchFolderSize, out uint punTimeStamp)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchFolderSize);
			bool flag = NativeMethods.ISteamUGC_GetItemInstallInfo(nPublishedFileID, out punSizeOnDisk, intPtr, cchFolderSize, out punTimeStamp);
			pchFolder = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000E410 File Offset: 0x0000C810
		public static bool GetItemDownloadInfo(PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_GetItemDownloadInfo(nPublishedFileID, out punBytesDownloaded, out punBytesTotal);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0000E41F File Offset: 0x0000C81F
		public static bool DownloadItem(PublishedFileId_t nPublishedFileID, bool bHighPriority)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUGC_DownloadItem(nPublishedFileID, bHighPriority);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0000E430 File Offset: 0x0000C830
		public static bool BInitWorkshopForGameServer(DepotId_t unWorkshopDepotID, string pszFolder)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszFolder))
			{
				result = NativeMethods.ISteamUGC_BInitWorkshopForGameServer(unWorkshopDepotID, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0000E474 File Offset: 0x0000C874
		public static void SuspendDownloads(bool bSuspend)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamUGC_SuspendDownloads(bSuspend);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0000E481 File Offset: 0x0000C881
		public static SteamAPICall_t StartPlaytimeTracking(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_StartPlaytimeTracking(pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0000E494 File Offset: 0x0000C894
		public static SteamAPICall_t StopPlaytimeTracking(PublishedFileId_t[] pvecPublishedFileID, uint unNumPublishedFileIDs)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_StopPlaytimeTracking(pvecPublishedFileID, unNumPublishedFileIDs);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0000E4A7 File Offset: 0x0000C8A7
		public static SteamAPICall_t StopPlaytimeTrackingForAllItems()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_StopPlaytimeTrackingForAllItems();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0000E4B8 File Offset: 0x0000C8B8
		public static SteamAPICall_t AddDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_AddDependency(nParentPublishedFileID, nChildPublishedFileID);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0000E4CB File Offset: 0x0000C8CB
		public static SteamAPICall_t RemoveDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamUGC_RemoveDependency(nParentPublishedFileID, nChildPublishedFileID);
		}
	}
}
