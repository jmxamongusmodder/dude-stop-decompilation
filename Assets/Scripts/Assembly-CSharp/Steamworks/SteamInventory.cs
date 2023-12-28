using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200010E RID: 270
	public static class SteamInventory
	{
		// Token: 0x06000824 RID: 2084 RVA: 0x0000C272 File Offset: 0x0000A672
		public static EResult GetResultStatus(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetResultStatus(resultHandle);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0000C27F File Offset: 0x0000A67F
		public static bool GetResultItems(SteamInventoryResult_t resultHandle, SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetResultItems(resultHandle, pOutItemsArray, ref punOutItemsArraySize);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0000C290 File Offset: 0x0000A690
		public static bool GetResultItemProperty(SteamInventoryResult_t resultHandle, uint unItemIndex, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				bool flag = NativeMethods.ISteamInventory_GetResultItemProperty(resultHandle, unItemIndex, utf8StringHandle, intPtr, ref punValueBufferSizeOut);
				pchValueBuffer = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
				Marshal.FreeHGlobal(intPtr);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0000C300 File Offset: 0x0000A700
		public static uint GetResultTimestamp(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetResultTimestamp(resultHandle);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0000C30D File Offset: 0x0000A70D
		public static bool CheckResultSteamID(SteamInventoryResult_t resultHandle, CSteamID steamIDExpected)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_CheckResultSteamID(resultHandle, steamIDExpected);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0000C31B File Offset: 0x0000A71B
		public static void DestroyResult(SteamInventoryResult_t resultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInventory_DestroyResult(resultHandle);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0000C328 File Offset: 0x0000A728
		public static bool GetAllItems(out SteamInventoryResult_t pResultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetAllItems(out pResultHandle);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0000C335 File Offset: 0x0000A735
		public static bool GetItemsByID(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetItemsByID(out pResultHandle, pInstanceIDs, unCountInstanceIDs);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0000C344 File Offset: 0x0000A744
		public static bool SerializeResult(SteamInventoryResult_t resultHandle, byte[] pOutBuffer, out uint punOutBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_SerializeResult(resultHandle, pOutBuffer, out punOutBufferSize);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0000C353 File Offset: 0x0000A753
		public static bool DeserializeResult(out SteamInventoryResult_t pOutResultHandle, byte[] pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE = false)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_DeserializeResult(out pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0000C363 File Offset: 0x0000A763
		public static bool GenerateItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GenerateItems(out pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0000C373 File Offset: 0x0000A773
		public static bool GrantPromoItems(out SteamInventoryResult_t pResultHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GrantPromoItems(out pResultHandle);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0000C380 File Offset: 0x0000A780
		public static bool AddPromoItem(out SteamInventoryResult_t pResultHandle, SteamItemDef_t itemDef)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_AddPromoItem(out pResultHandle, itemDef);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0000C38E File Offset: 0x0000A78E
		public static bool AddPromoItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayItemDefs, uint unArrayLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_AddPromoItems(out pResultHandle, pArrayItemDefs, unArrayLength);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0000C39D File Offset: 0x0000A79D
		public static bool ConsumeItem(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemConsume, uint unQuantity)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_ConsumeItem(out pResultHandle, itemConsume, unQuantity);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0000C3AC File Offset: 0x0000A7AC
		public static bool ExchangeItems(out SteamInventoryResult_t pResultHandle, SteamItemDef_t[] pArrayGenerate, uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, SteamItemInstanceID_t[] pArrayDestroy, uint[] punArrayDestroyQuantity, uint unArrayDestroyLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_ExchangeItems(out pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0000C3C2 File Offset: 0x0000A7C2
		public static bool TransferItemQuantity(out SteamInventoryResult_t pResultHandle, SteamItemInstanceID_t itemIdSource, uint unQuantity, SteamItemInstanceID_t itemIdDest)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_TransferItemQuantity(out pResultHandle, itemIdSource, unQuantity, itemIdDest);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0000C3D2 File Offset: 0x0000A7D2
		public static void SendItemDropHeartbeat()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamInventory_SendItemDropHeartbeat();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0000C3DE File Offset: 0x0000A7DE
		public static bool TriggerItemDrop(out SteamInventoryResult_t pResultHandle, SteamItemDef_t dropListDefinition)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_TriggerItemDrop(out pResultHandle, dropListDefinition);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0000C3EC File Offset: 0x0000A7EC
		public static bool TradeItems(out SteamInventoryResult_t pResultHandle, CSteamID steamIDTradePartner, SteamItemInstanceID_t[] pArrayGive, uint[] pArrayGiveQuantity, uint nArrayGiveLength, SteamItemInstanceID_t[] pArrayGet, uint[] pArrayGetQuantity, uint nArrayGetLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_TradeItems(out pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0000C404 File Offset: 0x0000A804
		public static bool LoadItemDefinitions()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_LoadItemDefinitions();
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0000C410 File Offset: 0x0000A810
		public static bool GetItemDefinitionIDs(SteamItemDef_t[] pItemDefIDs, out uint punItemDefIDsArraySize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetItemDefinitionIDs(pItemDefIDs, out punItemDefIDsArraySize);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0000C420 File Offset: 0x0000A820
		public static bool GetItemDefinitionProperty(SteamItemDef_t iDefinition, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPropertyName))
			{
				bool flag = NativeMethods.ISteamInventory_GetItemDefinitionProperty(iDefinition, utf8StringHandle, intPtr, ref punValueBufferSizeOut);
				pchValueBuffer = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
				Marshal.FreeHGlobal(intPtr);
				result = flag;
			}
			return result;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0000C48C File Offset: 0x0000A88C
		public static SteamAPICall_t RequestEligiblePromoItemDefinitionsIDs(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(steamID);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0000C49E File Offset: 0x0000A89E
		public static bool GetEligiblePromoItemDefinitionIDs(CSteamID steamID, SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamInventory_GetEligiblePromoItemDefinitionIDs(steamID, pItemDefIDs, ref punItemDefIDsArraySize);
		}
	}
}
