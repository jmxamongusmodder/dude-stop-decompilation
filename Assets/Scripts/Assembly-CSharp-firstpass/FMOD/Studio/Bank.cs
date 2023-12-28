using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000FE RID: 254
	public struct Bank
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x0000861B File Offset: 0x00006A1B
		public RESULT getID(out Guid id)
		{
			return Bank.FMOD_Studio_Bank_GetID(this.handle, out id);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0000862C File Offset: 0x00006A2C
		public RESULT getPath(out string path)
		{
			path = null;
			RESULT result2;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				IntPtr intPtr = Marshal.AllocHGlobal(256);
				int num = 0;
				RESULT result = Bank.FMOD_Studio_Bank_GetPath(this.handle, intPtr, 256, out num);
				if (result == RESULT.ERR_TRUNCATED)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = Marshal.AllocHGlobal(num);
					result = Bank.FMOD_Studio_Bank_GetPath(this.handle, intPtr, num, out num);
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

		// Token: 0x06000616 RID: 1558 RVA: 0x000086C4 File Offset: 0x00006AC4
		public RESULT unload()
		{
			return Bank.FMOD_Studio_Bank_Unload(this.handle);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000086D1 File Offset: 0x00006AD1
		public RESULT loadSampleData()
		{
			return Bank.FMOD_Studio_Bank_LoadSampleData(this.handle);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000086DE File Offset: 0x00006ADE
		public RESULT unloadSampleData()
		{
			return Bank.FMOD_Studio_Bank_UnloadSampleData(this.handle);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000086EB File Offset: 0x00006AEB
		public RESULT getLoadingState(out LOADING_STATE state)
		{
			return Bank.FMOD_Studio_Bank_GetLoadingState(this.handle, out state);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000086F9 File Offset: 0x00006AF9
		public RESULT getSampleLoadingState(out LOADING_STATE state)
		{
			return Bank.FMOD_Studio_Bank_GetSampleLoadingState(this.handle, out state);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00008707 File Offset: 0x00006B07
		public RESULT getStringCount(out int count)
		{
			return Bank.FMOD_Studio_Bank_GetStringCount(this.handle, out count);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00008718 File Offset: 0x00006B18
		public RESULT getStringInfo(int index, out Guid id, out string path)
		{
			path = null;
			id = Guid.Empty;
			RESULT result2;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				IntPtr intPtr = Marshal.AllocHGlobal(256);
				int num = 0;
				RESULT result = Bank.FMOD_Studio_Bank_GetStringInfo(this.handle, index, out id, intPtr, 256, out num);
				if (result == RESULT.ERR_TRUNCATED)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = Marshal.AllocHGlobal(num);
					result = Bank.FMOD_Studio_Bank_GetStringInfo(this.handle, index, out id, intPtr, num, out num);
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

		// Token: 0x0600061D RID: 1565 RVA: 0x000087C0 File Offset: 0x00006BC0
		public RESULT getEventCount(out int count)
		{
			return Bank.FMOD_Studio_Bank_GetEventCount(this.handle, out count);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000087D0 File Offset: 0x00006BD0
		public RESULT getEventList(out EventDescription[] array)
		{
			array = null;
			int num;
			RESULT result = Bank.FMOD_Studio_Bank_GetEventCount(this.handle, out num);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num == 0)
			{
				array = new EventDescription[0];
				return result;
			}
			IntPtr[] array2 = new IntPtr[num];
			int num2;
			result = Bank.FMOD_Studio_Bank_GetEventList(this.handle, array2, num, out num2);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num2 > num)
			{
				num2 = num;
			}
			array = new EventDescription[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i].handle = array2[i];
			}
			return RESULT.OK;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000885F File Offset: 0x00006C5F
		public RESULT getBusCount(out int count)
		{
			return Bank.FMOD_Studio_Bank_GetBusCount(this.handle, out count);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00008870 File Offset: 0x00006C70
		public RESULT getBusList(out Bus[] array)
		{
			array = null;
			int num;
			RESULT result = Bank.FMOD_Studio_Bank_GetBusCount(this.handle, out num);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num == 0)
			{
				array = new Bus[0];
				return result;
			}
			IntPtr[] array2 = new IntPtr[num];
			int num2;
			result = Bank.FMOD_Studio_Bank_GetBusList(this.handle, array2, num, out num2);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num2 > num)
			{
				num2 = num;
			}
			array = new Bus[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i].handle = array2[i];
			}
			return RESULT.OK;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000088FF File Offset: 0x00006CFF
		public RESULT getVCACount(out int count)
		{
			return Bank.FMOD_Studio_Bank_GetVCACount(this.handle, out count);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00008910 File Offset: 0x00006D10
		public RESULT getVCAList(out VCA[] array)
		{
			array = null;
			int num;
			RESULT result = Bank.FMOD_Studio_Bank_GetVCACount(this.handle, out num);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num == 0)
			{
				array = new VCA[0];
				return result;
			}
			IntPtr[] array2 = new IntPtr[num];
			int num2;
			result = Bank.FMOD_Studio_Bank_GetVCAList(this.handle, array2, num, out num2);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num2 > num)
			{
				num2 = num;
			}
			array = new VCA[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i].handle = array2[i];
			}
			return RESULT.OK;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0000899F File Offset: 0x00006D9F
		public RESULT getUserData(out IntPtr userdata)
		{
			return Bank.FMOD_Studio_Bank_GetUserData(this.handle, out userdata);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000089AD File Offset: 0x00006DAD
		public RESULT setUserData(IntPtr userdata)
		{
			return Bank.FMOD_Studio_Bank_SetUserData(this.handle, userdata);
		}

		// Token: 0x06000625 RID: 1573
		[DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_Bank_IsValid(IntPtr bank);

		// Token: 0x06000626 RID: 1574
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetID(IntPtr bank, out Guid id);

		// Token: 0x06000627 RID: 1575
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetPath(IntPtr bank, IntPtr path, int size, out int retrieved);

		// Token: 0x06000628 RID: 1576
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_Unload(IntPtr bank);

		// Token: 0x06000629 RID: 1577
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_LoadSampleData(IntPtr bank);

		// Token: 0x0600062A RID: 1578
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_UnloadSampleData(IntPtr bank);

		// Token: 0x0600062B RID: 1579
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetLoadingState(IntPtr bank, out LOADING_STATE state);

		// Token: 0x0600062C RID: 1580
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetSampleLoadingState(IntPtr bank, out LOADING_STATE state);

		// Token: 0x0600062D RID: 1581
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetStringCount(IntPtr bank, out int count);

		// Token: 0x0600062E RID: 1582
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetStringInfo(IntPtr bank, int index, out Guid id, IntPtr path, int size, out int retrieved);

		// Token: 0x0600062F RID: 1583
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetEventCount(IntPtr bank, out int count);

		// Token: 0x06000630 RID: 1584
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetEventList(IntPtr bank, IntPtr[] array, int capacity, out int count);

		// Token: 0x06000631 RID: 1585
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetBusCount(IntPtr bank, out int count);

		// Token: 0x06000632 RID: 1586
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetBusList(IntPtr bank, IntPtr[] array, int capacity, out int count);

		// Token: 0x06000633 RID: 1587
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetVCACount(IntPtr bank, out int count);

		// Token: 0x06000634 RID: 1588
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetVCAList(IntPtr bank, IntPtr[] array, int capacity, out int count);

		// Token: 0x06000635 RID: 1589
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_GetUserData(IntPtr bank, out IntPtr userdata);

		// Token: 0x06000636 RID: 1590
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bank_SetUserData(IntPtr bank, IntPtr userdata);

		// Token: 0x06000637 RID: 1591 RVA: 0x000089BB File Offset: 0x00006DBB
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000089CD File Offset: 0x00006DCD
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000089DA File Offset: 0x00006DDA
		public bool isValid()
		{
			return this.hasHandle() && Bank.FMOD_Studio_Bank_IsValid(this.handle);
		}

		// Token: 0x040004EF RID: 1263
		public IntPtr handle;
	}
}
