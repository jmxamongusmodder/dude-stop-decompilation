using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000F9 RID: 249
	public struct EventDescription
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00007D01 File Offset: 0x00006101
		public RESULT getID(out Guid id)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetID(this.handle, out id);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00007D10 File Offset: 0x00006110
		public RESULT getPath(out string path)
		{
			path = null;
			RESULT result2;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				IntPtr intPtr = Marshal.AllocHGlobal(256);
				int num = 0;
				RESULT result = EventDescription.FMOD_Studio_EventDescription_GetPath(this.handle, intPtr, 256, out num);
				if (result == RESULT.ERR_TRUNCATED)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = Marshal.AllocHGlobal(num);
					result = EventDescription.FMOD_Studio_EventDescription_GetPath(this.handle, intPtr, num, out num);
				}
				if (result == RESULT.OK)
				{
					path = freeHelper.stringFromNative(intPtr);
				}
				Marshal.FreeHGlobal(intPtr);
				result2 = result;
			}
			return result2;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00007DA8 File Offset: 0x000061A8
		public RESULT getParameterCount(out int count)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetParameterCount(this.handle, out count);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00007DB6 File Offset: 0x000061B6
		public RESULT getParameterByIndex(int index, out PARAMETER_DESCRIPTION parameter)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetParameterByIndex(this.handle, index, out parameter);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00007DC8 File Offset: 0x000061C8
		public RESULT getParameter(string name, out PARAMETER_DESCRIPTION parameter)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = EventDescription.FMOD_Studio_EventDescription_GetParameter(this.handle, freeHelper.byteFromStringUTF8(name), out parameter);
			}
			return result;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00007E14 File Offset: 0x00006214
		public RESULT getUserPropertyCount(out int count)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetUserPropertyCount(this.handle, out count);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00007E22 File Offset: 0x00006222
		public RESULT getUserPropertyByIndex(int index, out USER_PROPERTY property)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetUserPropertyByIndex(this.handle, index, out property);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00007E34 File Offset: 0x00006234
		public RESULT getUserProperty(string name, out USER_PROPERTY property)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = EventDescription.FMOD_Studio_EventDescription_GetUserProperty(this.handle, freeHelper.byteFromStringUTF8(name), out property);
			}
			return result;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00007E80 File Offset: 0x00006280
		public RESULT getLength(out int length)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetLength(this.handle, out length);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00007E8E File Offset: 0x0000628E
		public RESULT getMinimumDistance(out float distance)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetMinimumDistance(this.handle, out distance);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00007E9C File Offset: 0x0000629C
		public RESULT getMaximumDistance(out float distance)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetMaximumDistance(this.handle, out distance);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00007EAA File Offset: 0x000062AA
		public RESULT getSoundSize(out float size)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetSoundSize(this.handle, out size);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00007EB8 File Offset: 0x000062B8
		public RESULT isSnapshot(out bool snapshot)
		{
			return EventDescription.FMOD_Studio_EventDescription_IsSnapshot(this.handle, out snapshot);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00007EC6 File Offset: 0x000062C6
		public RESULT isOneshot(out bool oneshot)
		{
			return EventDescription.FMOD_Studio_EventDescription_IsOneshot(this.handle, out oneshot);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00007ED4 File Offset: 0x000062D4
		public RESULT isStream(out bool isStream)
		{
			return EventDescription.FMOD_Studio_EventDescription_IsStream(this.handle, out isStream);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00007EE2 File Offset: 0x000062E2
		public RESULT is3D(out bool is3D)
		{
			return EventDescription.FMOD_Studio_EventDescription_Is3D(this.handle, out is3D);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00007EF0 File Offset: 0x000062F0
		public RESULT hasCue(out bool cue)
		{
			return EventDescription.FMOD_Studio_EventDescription_HasCue(this.handle, out cue);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00007EFE File Offset: 0x000062FE
		public RESULT createInstance(out EventInstance instance)
		{
			return EventDescription.FMOD_Studio_EventDescription_CreateInstance(this.handle, out instance.handle);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00007F11 File Offset: 0x00006311
		public RESULT getInstanceCount(out int count)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetInstanceCount(this.handle, out count);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00007F20 File Offset: 0x00006320
		public RESULT getInstanceList(out EventInstance[] array)
		{
			array = null;
			int num;
			RESULT result = EventDescription.FMOD_Studio_EventDescription_GetInstanceCount(this.handle, out num);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num == 0)
			{
				array = new EventInstance[0];
				return result;
			}
			IntPtr[] array2 = new IntPtr[num];
			int num2;
			result = EventDescription.FMOD_Studio_EventDescription_GetInstanceList(this.handle, array2, num, out num2);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num2 > num)
			{
				num2 = num;
			}
			array = new EventInstance[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i].handle = array2[i];
			}
			return RESULT.OK;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00007FAF File Offset: 0x000063AF
		public RESULT loadSampleData()
		{
			return EventDescription.FMOD_Studio_EventDescription_LoadSampleData(this.handle);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00007FBC File Offset: 0x000063BC
		public RESULT unloadSampleData()
		{
			return EventDescription.FMOD_Studio_EventDescription_UnloadSampleData(this.handle);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00007FC9 File Offset: 0x000063C9
		public RESULT getSampleLoadingState(out LOADING_STATE state)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetSampleLoadingState(this.handle, out state);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00007FD7 File Offset: 0x000063D7
		public RESULT releaseAllInstances()
		{
			return EventDescription.FMOD_Studio_EventDescription_ReleaseAllInstances(this.handle);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00007FE4 File Offset: 0x000063E4
		public RESULT setCallback(EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask = EVENT_CALLBACK_TYPE.ALL)
		{
			return EventDescription.FMOD_Studio_EventDescription_SetCallback(this.handle, callback, callbackmask);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00007FF3 File Offset: 0x000063F3
		public RESULT getUserData(out IntPtr userdata)
		{
			return EventDescription.FMOD_Studio_EventDescription_GetUserData(this.handle, out userdata);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00008001 File Offset: 0x00006401
		public RESULT setUserData(IntPtr userdata)
		{
			return EventDescription.FMOD_Studio_EventDescription_SetUserData(this.handle, userdata);
		}

		// Token: 0x06000579 RID: 1401
		[DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_EventDescription_IsValid(IntPtr eventdescription);

		// Token: 0x0600057A RID: 1402
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetID(IntPtr eventdescription, out Guid id);

		// Token: 0x0600057B RID: 1403
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetPath(IntPtr eventdescription, IntPtr path, int size, out int retrieved);

		// Token: 0x0600057C RID: 1404
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterCount(IntPtr eventdescription, out int count);

		// Token: 0x0600057D RID: 1405
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameterByIndex(IntPtr eventdescription, int index, out PARAMETER_DESCRIPTION parameter);

		// Token: 0x0600057E RID: 1406
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetParameter(IntPtr eventdescription, byte[] name, out PARAMETER_DESCRIPTION parameter);

		// Token: 0x0600057F RID: 1407
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyCount(IntPtr eventdescription, out int count);

		// Token: 0x06000580 RID: 1408
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyByIndex(IntPtr eventdescription, int index, out USER_PROPERTY property);

		// Token: 0x06000581 RID: 1409
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetUserProperty(IntPtr eventdescription, byte[] name, out USER_PROPERTY property);

		// Token: 0x06000582 RID: 1410
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetLength(IntPtr eventdescription, out int length);

		// Token: 0x06000583 RID: 1411
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetMinimumDistance(IntPtr eventdescription, out float distance);

		// Token: 0x06000584 RID: 1412
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetMaximumDistance(IntPtr eventdescription, out float distance);

		// Token: 0x06000585 RID: 1413
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetSoundSize(IntPtr eventdescription, out float size);

		// Token: 0x06000586 RID: 1414
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_IsSnapshot(IntPtr eventdescription, out bool snapshot);

		// Token: 0x06000587 RID: 1415
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_IsOneshot(IntPtr eventdescription, out bool oneshot);

		// Token: 0x06000588 RID: 1416
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_IsStream(IntPtr eventdescription, out bool isStream);

		// Token: 0x06000589 RID: 1417
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_Is3D(IntPtr eventdescription, out bool is3D);

		// Token: 0x0600058A RID: 1418
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_HasCue(IntPtr eventdescription, out bool cue);

		// Token: 0x0600058B RID: 1419
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_CreateInstance(IntPtr eventdescription, out IntPtr instance);

		// Token: 0x0600058C RID: 1420
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetInstanceCount(IntPtr eventdescription, out int count);

		// Token: 0x0600058D RID: 1421
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetInstanceList(IntPtr eventdescription, IntPtr[] array, int capacity, out int count);

		// Token: 0x0600058E RID: 1422
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_LoadSampleData(IntPtr eventdescription);

		// Token: 0x0600058F RID: 1423
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_UnloadSampleData(IntPtr eventdescription);

		// Token: 0x06000590 RID: 1424
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetSampleLoadingState(IntPtr eventdescription, out LOADING_STATE state);

		// Token: 0x06000591 RID: 1425
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_ReleaseAllInstances(IntPtr eventdescription);

		// Token: 0x06000592 RID: 1426
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_SetCallback(IntPtr eventdescription, EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask);

		// Token: 0x06000593 RID: 1427
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_GetUserData(IntPtr eventdescription, out IntPtr userdata);

		// Token: 0x06000594 RID: 1428
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_EventDescription_SetUserData(IntPtr eventdescription, IntPtr userdata);

		// Token: 0x06000595 RID: 1429 RVA: 0x0000800F File Offset: 0x0000640F
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00008021 File Offset: 0x00006421
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000802E File Offset: 0x0000642E
		public bool isValid()
		{
			return this.hasHandle() && EventDescription.FMOD_Studio_EventDescription_IsValid(this.handle);
		}

		// Token: 0x040004EA RID: 1258
		public IntPtr handle;
	}
}
