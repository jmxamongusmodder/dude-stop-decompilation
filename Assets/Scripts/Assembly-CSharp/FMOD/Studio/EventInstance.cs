using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000FA RID: 250
	public struct EventInstance
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x00008049 File Offset: 0x00006449
		public RESULT getDescription(out EventDescription description)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetDescription(this.handle, out description.handle);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000805C File Offset: 0x0000645C
		public RESULT getVolume(out float volume, out float finalvolume)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetVolume(this.handle, out volume, out finalvolume);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000806B File Offset: 0x0000646B
		public RESULT setVolume(float volume)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetVolume(this.handle, volume);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00008079 File Offset: 0x00006479
		public RESULT getPitch(out float pitch, out float finalpitch)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetPitch(this.handle, out pitch, out finalpitch);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00008088 File Offset: 0x00006488
		public RESULT setPitch(float pitch)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetPitch(this.handle, pitch);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00008096 File Offset: 0x00006496
		public RESULT get3DAttributes(out ATTRIBUTES_3D attributes)
		{
			return EventInstance.FMOD_Studio_EventInstance_Get3DAttributes(this.handle, out attributes);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000080A4 File Offset: 0x000064A4
		public RESULT set3DAttributes(ATTRIBUTES_3D attributes)
		{
			return EventInstance.FMOD_Studio_EventInstance_Set3DAttributes(this.handle, ref attributes);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000080B3 File Offset: 0x000064B3
		public RESULT getListenerMask(out uint mask)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetListenerMask(this.handle, out mask);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000080C1 File Offset: 0x000064C1
		public RESULT setListenerMask(uint mask)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetListenerMask(this.handle, mask);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000080CF File Offset: 0x000064CF
		public RESULT getProperty(EVENT_PROPERTY index, out float value)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetProperty(this.handle, index, out value);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000080DE File Offset: 0x000064DE
		public RESULT setProperty(EVENT_PROPERTY index, float value)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetProperty(this.handle, index, value);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000080ED File Offset: 0x000064ED
		public RESULT getReverbLevel(int index, out float level)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetReverbLevel(this.handle, index, out level);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000080FC File Offset: 0x000064FC
		public RESULT setReverbLevel(int index, float level)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetReverbLevel(this.handle, index, level);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000810B File Offset: 0x0000650B
		public RESULT getPaused(out bool paused)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetPaused(this.handle, out paused);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00008119 File Offset: 0x00006519
		public RESULT setPaused(bool paused)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetPaused(this.handle, paused);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00008127 File Offset: 0x00006527
		public RESULT start()
		{
			return EventInstance.FMOD_Studio_EventInstance_Start(this.handle);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00008134 File Offset: 0x00006534
		public RESULT stop(STOP_MODE mode)
		{
			return EventInstance.FMOD_Studio_EventInstance_Stop(this.handle, mode);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00008142 File Offset: 0x00006542
		public RESULT getTimelinePosition(out int position)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetTimelinePosition(this.handle, out position);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00008150 File Offset: 0x00006550
		public RESULT setTimelinePosition(int position)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetTimelinePosition(this.handle, position);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000815E File Offset: 0x0000655E
		public RESULT getPlaybackState(out PLAYBACK_STATE state)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetPlaybackState(this.handle, out state);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000816C File Offset: 0x0000656C
		public RESULT getChannelGroup(out ChannelGroup group)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetChannelGroup(this.handle, out group.handle);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000817F File Offset: 0x0000657F
		public RESULT release()
		{
			return EventInstance.FMOD_Studio_EventInstance_Release(this.handle);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000818C File Offset: 0x0000658C
		public RESULT isVirtual(out bool virtualState)
		{
			return EventInstance.FMOD_Studio_EventInstance_IsVirtual(this.handle, out virtualState);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000819C File Offset: 0x0000659C
		public RESULT getParameter(string name, out ParameterInstance instance)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = EventInstance.FMOD_Studio_EventInstance_GetParameter(this.handle, freeHelper.byteFromStringUTF8(name), out instance.handle);
			}
			return result;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000081EC File Offset: 0x000065EC
		public RESULT getParameterCount(out int count)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetParameterCount(this.handle, out count);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000081FA File Offset: 0x000065FA
		public RESULT getParameterByIndex(int index, out ParameterInstance instance)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetParameterByIndex(this.handle, index, out instance.handle);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00008210 File Offset: 0x00006610
		public RESULT getParameterValue(string name, out float value, out float finalvalue)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = EventInstance.FMOD_Studio_EventInstance_GetParameterValue(this.handle, freeHelper.byteFromStringUTF8(name), out value, out finalvalue);
			}
			return result;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000825C File Offset: 0x0000665C
		public RESULT setParameterValue(string name, float value)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = EventInstance.FMOD_Studio_EventInstance_SetParameterValue(this.handle, freeHelper.byteFromStringUTF8(name), value);
			}
			return result;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000082A8 File Offset: 0x000066A8
		public RESULT getParameterValueByIndex(int index, out float value, out float finalvalue)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetParameterValueByIndex(this.handle, index, out value, out finalvalue);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000082B8 File Offset: 0x000066B8
		public RESULT setParameterValueByIndex(int index, float value)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetParameterValueByIndex(this.handle, index, value);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000082C7 File Offset: 0x000066C7
		public RESULT setParameterValuesByIndices(int[] indices, float[] values, int count)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetParameterValuesByIndices(this.handle, indices, values, count);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000082D7 File Offset: 0x000066D7
		public RESULT triggerCue()
		{
			return EventInstance.FMOD_Studio_EventInstance_TriggerCue(this.handle);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000082E4 File Offset: 0x000066E4
		public RESULT setCallback(EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask = EVENT_CALLBACK_TYPE.ALL)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetCallback(this.handle, callback, callbackmask);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000082F3 File Offset: 0x000066F3
		public RESULT getUserData(out IntPtr userdata)
		{
			return EventInstance.FMOD_Studio_EventInstance_GetUserData(this.handle, out userdata);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00008301 File Offset: 0x00006701
		public RESULT setUserData(IntPtr userdata)
		{
			return EventInstance.FMOD_Studio_EventInstance_SetUserData(this.handle, userdata);
		}

		// Token: 0x060005BB RID: 1467
		[DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_EventInstance_IsValid(IntPtr _event);

		// Token: 0x060005BC RID: 1468
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetDescription(IntPtr _event, out IntPtr description);

		// Token: 0x060005BD RID: 1469
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetVolume(IntPtr _event, out float volume, out float finalvolume);

		// Token: 0x060005BE RID: 1470
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetVolume(IntPtr _event, float volume);

		// Token: 0x060005BF RID: 1471
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetPitch(IntPtr _event, out float pitch, out float finalpitch);

		// Token: 0x060005C0 RID: 1472
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetPitch(IntPtr _event, float pitch);

		// Token: 0x060005C1 RID: 1473
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_Get3DAttributes(IntPtr _event, out ATTRIBUTES_3D attributes);

		// Token: 0x060005C2 RID: 1474
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_Set3DAttributes(IntPtr _event, ref ATTRIBUTES_3D attributes);

		// Token: 0x060005C3 RID: 1475
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetListenerMask(IntPtr _event, out uint mask);

		// Token: 0x060005C4 RID: 1476
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetListenerMask(IntPtr _event, uint mask);

		// Token: 0x060005C5 RID: 1477
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetProperty(IntPtr _event, EVENT_PROPERTY index, out float value);

		// Token: 0x060005C6 RID: 1478
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetProperty(IntPtr _event, EVENT_PROPERTY index, float value);

		// Token: 0x060005C7 RID: 1479
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetReverbLevel(IntPtr _event, int index, out float level);

		// Token: 0x060005C8 RID: 1480
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetReverbLevel(IntPtr _event, int index, float level);

		// Token: 0x060005C9 RID: 1481
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetPaused(IntPtr _event, out bool paused);

		// Token: 0x060005CA RID: 1482
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetPaused(IntPtr _event, bool paused);

		// Token: 0x060005CB RID: 1483
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_Start(IntPtr _event);

		// Token: 0x060005CC RID: 1484
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_Stop(IntPtr _event, STOP_MODE mode);

		// Token: 0x060005CD RID: 1485
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetTimelinePosition(IntPtr _event, out int position);

		// Token: 0x060005CE RID: 1486
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetTimelinePosition(IntPtr _event, int position);

		// Token: 0x060005CF RID: 1487
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetPlaybackState(IntPtr _event, out PLAYBACK_STATE state);

		// Token: 0x060005D0 RID: 1488
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetChannelGroup(IntPtr _event, out IntPtr group);

		// Token: 0x060005D1 RID: 1489
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_Release(IntPtr _event);

		// Token: 0x060005D2 RID: 1490
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_IsVirtual(IntPtr _event, out bool virtualState);

		// Token: 0x060005D3 RID: 1491
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetParameter(IntPtr _event, byte[] name, out IntPtr parameter);

		// Token: 0x060005D4 RID: 1492
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetParameterByIndex(IntPtr _event, int index, out IntPtr parameter);

		// Token: 0x060005D5 RID: 1493
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetParameterCount(IntPtr _event, out int count);

		// Token: 0x060005D6 RID: 1494
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetParameterValue(IntPtr _event, byte[] name, out float value, out float finalvalue);

		// Token: 0x060005D7 RID: 1495
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetParameterValue(IntPtr _event, byte[] name, float value);

		// Token: 0x060005D8 RID: 1496
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetParameterValueByIndex(IntPtr _event, int index, out float value, out float finalvalue);

		// Token: 0x060005D9 RID: 1497
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetParameterValueByIndex(IntPtr _event, int index, float value);

		// Token: 0x060005DA RID: 1498
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetParameterValuesByIndices(IntPtr _event, int[] indices, float[] values, int count);

		// Token: 0x060005DB RID: 1499
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_TriggerCue(IntPtr _event);

		// Token: 0x060005DC RID: 1500
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetCallback(IntPtr _event, EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask);

		// Token: 0x060005DD RID: 1501
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_GetUserData(IntPtr _event, out IntPtr userdata);

		// Token: 0x060005DE RID: 1502
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventInstance_SetUserData(IntPtr _event, IntPtr userdata);

		// Token: 0x060005DF RID: 1503 RVA: 0x0000830F File Offset: 0x0000670F
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00008321 File Offset: 0x00006721
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000832E File Offset: 0x0000672E
		public bool isValid()
		{
			return this.hasHandle() && EventInstance.FMOD_Studio_EventInstance_IsValid(this.handle);
		}

		// Token: 0x040004EB RID: 1259
		public IntPtr handle;
	}
}
