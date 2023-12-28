using System;

namespace Steamworks
{
	// Token: 0x02000103 RID: 259
	public static class SteamController
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00009638 File Offset: 0x00007A38
		public static bool Init()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_Init();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00009644 File Offset: 0x00007A44
		public static bool Shutdown()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_Shutdown();
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00009650 File Offset: 0x00007A50
		public static void RunFrame()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_RunFrame();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000965C File Offset: 0x00007A5C
		public static int GetConnectedControllers(ControllerHandle_t[] handlesOut)
		{
			InteropHelp.TestIfAvailableClient();
			if (handlesOut.Length != 16)
			{
				throw new ArgumentException("handlesOut must be the same size as Constants.STEAM_CONTROLLER_MAX_COUNT!");
			}
			return NativeMethods.ISteamController_GetConnectedControllers(handlesOut);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000967E File Offset: 0x00007A7E
		public static bool ShowBindingPanel(ControllerHandle_t controllerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_ShowBindingPanel(controllerHandle);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000968C File Offset: 0x00007A8C
		public static ControllerActionSetHandle_t GetActionSetHandle(string pszActionSetName)
		{
			InteropHelp.TestIfAvailableClient();
			ControllerActionSetHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionSetName))
			{
				result = (ControllerActionSetHandle_t)NativeMethods.ISteamController_GetActionSetHandle(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000096D4 File Offset: 0x00007AD4
		public static void ActivateActionSet(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_ActivateActionSet(controllerHandle, actionSetHandle);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000096E2 File Offset: 0x00007AE2
		public static ControllerActionSetHandle_t GetCurrentActionSet(ControllerHandle_t controllerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return (ControllerActionSetHandle_t)NativeMethods.ISteamController_GetCurrentActionSet(controllerHandle);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000096F4 File Offset: 0x00007AF4
		public static ControllerDigitalActionHandle_t GetDigitalActionHandle(string pszActionName)
		{
			InteropHelp.TestIfAvailableClient();
			ControllerDigitalActionHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionName))
			{
				result = (ControllerDigitalActionHandle_t)NativeMethods.ISteamController_GetDigitalActionHandle(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000973C File Offset: 0x00007B3C
		public static ControllerDigitalActionData_t GetDigitalActionData(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetDigitalActionData(controllerHandle, digitalActionHandle);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000974A File Offset: 0x00007B4A
		public static int GetDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerDigitalActionHandle_t digitalActionHandle, EControllerActionOrigin[] originsOut)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetDigitalActionOrigins(controllerHandle, actionSetHandle, digitalActionHandle, originsOut);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000975C File Offset: 0x00007B5C
		public static ControllerAnalogActionHandle_t GetAnalogActionHandle(string pszActionName)
		{
			InteropHelp.TestIfAvailableClient();
			ControllerAnalogActionHandle_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszActionName))
			{
				result = (ControllerAnalogActionHandle_t)NativeMethods.ISteamController_GetAnalogActionHandle(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000097A4 File Offset: 0x00007BA4
		public static ControllerAnalogActionData_t GetAnalogActionData(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetAnalogActionData(controllerHandle, analogActionHandle);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000097B2 File Offset: 0x00007BB2
		public static int GetAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerAnalogActionHandle_t analogActionHandle, EControllerActionOrigin[] originsOut)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetAnalogActionOrigins(controllerHandle, actionSetHandle, analogActionHandle, originsOut);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000097C2 File Offset: 0x00007BC2
		public static void StopAnalogActionMomentum(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t eAction)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_StopAnalogActionMomentum(controllerHandle, eAction);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000097D0 File Offset: 0x00007BD0
		public static void TriggerHapticPulse(ControllerHandle_t controllerHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_TriggerHapticPulse(controllerHandle, eTargetPad, usDurationMicroSec);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000097DF File Offset: 0x00007BDF
		public static void TriggerRepeatedHapticPulse(ControllerHandle_t controllerHandle, ESteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_TriggerRepeatedHapticPulse(controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000097F3 File Offset: 0x00007BF3
		public static void TriggerVibration(ControllerHandle_t controllerHandle, ushort usLeftSpeed, ushort usRightSpeed)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_TriggerVibration(controllerHandle, usLeftSpeed, usRightSpeed);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00009802 File Offset: 0x00007C02
		public static void SetLEDColor(ControllerHandle_t controllerHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamController_SetLEDColor(controllerHandle, nColorR, nColorG, nColorB, nFlags);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00009814 File Offset: 0x00007C14
		public static int GetGamepadIndexForController(ControllerHandle_t ulControllerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetGamepadIndexForController(ulControllerHandle);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00009821 File Offset: 0x00007C21
		public static ControllerHandle_t GetControllerForGamepadIndex(int nIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (ControllerHandle_t)NativeMethods.ISteamController_GetControllerForGamepadIndex(nIndex);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00009833 File Offset: 0x00007C33
		public static ControllerMotionData_t GetMotionData(ControllerHandle_t controllerHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_GetMotionData(controllerHandle);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00009840 File Offset: 0x00007C40
		public static bool ShowDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle, float flScale, float flXPosition, float flYPosition)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_ShowDigitalActionOrigins(controllerHandle, digitalActionHandle, flScale, flXPosition, flYPosition);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00009852 File Offset: 0x00007C52
		public static bool ShowAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle, float flScale, float flXPosition, float flYPosition)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamController_ShowAnalogActionOrigins(controllerHandle, analogActionHandle, flScale, flXPosition, flYPosition);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00009864 File Offset: 0x00007C64
		public static string GetStringForActionOrigin(EControllerActionOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamController_GetStringForActionOrigin(eOrigin));
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00009876 File Offset: 0x00007C76
		public static string GetGlyphForActionOrigin(EControllerActionOrigin eOrigin)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamController_GetGlyphForActionOrigin(eOrigin));
		}
	}
}
