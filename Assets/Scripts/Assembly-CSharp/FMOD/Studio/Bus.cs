using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000FC RID: 252
	public struct Bus
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x000083AD File Offset: 0x000067AD
		public RESULT getID(out Guid id)
		{
			return Bus.FMOD_Studio_Bus_GetID(this.handle, out id);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000083BC File Offset: 0x000067BC
		public RESULT getPath(out string path)
		{
			path = null;
			RESULT result2;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				IntPtr intPtr = Marshal.AllocHGlobal(256);
				int num = 0;
				RESULT result = Bus.FMOD_Studio_Bus_GetPath(this.handle, intPtr, 256, out num);
				if (result == RESULT.ERR_TRUNCATED)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = Marshal.AllocHGlobal(num);
					result = Bus.FMOD_Studio_Bus_GetPath(this.handle, intPtr, num, out num);
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

		// Token: 0x060005EE RID: 1518 RVA: 0x00008454 File Offset: 0x00006854
		public RESULT getVolume(out float volume, out float finalvolume)
		{
			return Bus.FMOD_Studio_Bus_GetVolume(this.handle, out volume, out finalvolume);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00008463 File Offset: 0x00006863
		public RESULT setVolume(float volume)
		{
			return Bus.FMOD_Studio_Bus_SetVolume(this.handle, volume);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00008471 File Offset: 0x00006871
		public RESULT getPaused(out bool paused)
		{
			return Bus.FMOD_Studio_Bus_GetPaused(this.handle, out paused);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000847F File Offset: 0x0000687F
		public RESULT setPaused(bool paused)
		{
			return Bus.FMOD_Studio_Bus_SetPaused(this.handle, paused);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000848D File Offset: 0x0000688D
		public RESULT getMute(out bool mute)
		{
			return Bus.FMOD_Studio_Bus_GetMute(this.handle, out mute);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000849B File Offset: 0x0000689B
		public RESULT setMute(bool mute)
		{
			return Bus.FMOD_Studio_Bus_SetMute(this.handle, mute);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000084A9 File Offset: 0x000068A9
		public RESULT stopAllEvents(STOP_MODE mode)
		{
			return Bus.FMOD_Studio_Bus_StopAllEvents(this.handle, mode);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000084B7 File Offset: 0x000068B7
		public RESULT lockChannelGroup()
		{
			return Bus.FMOD_Studio_Bus_LockChannelGroup(this.handle);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000084C4 File Offset: 0x000068C4
		public RESULT unlockChannelGroup()
		{
			return Bus.FMOD_Studio_Bus_UnlockChannelGroup(this.handle);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000084D1 File Offset: 0x000068D1
		public RESULT getChannelGroup(out ChannelGroup group)
		{
			return Bus.FMOD_Studio_Bus_GetChannelGroup(this.handle, out group.handle);
		}

		// Token: 0x060005F8 RID: 1528
		[DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_Bus_IsValid(IntPtr bus);

		// Token: 0x060005F9 RID: 1529
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_GetID(IntPtr bus, out Guid id);

		// Token: 0x060005FA RID: 1530
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_GetPath(IntPtr bus, IntPtr path, int size, out int retrieved);

		// Token: 0x060005FB RID: 1531
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_GetVolume(IntPtr bus, out float volume, out float finalvolume);

		// Token: 0x060005FC RID: 1532
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_SetVolume(IntPtr bus, float value);

		// Token: 0x060005FD RID: 1533
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_GetPaused(IntPtr bus, out bool paused);

		// Token: 0x060005FE RID: 1534
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_SetPaused(IntPtr bus, bool paused);

		// Token: 0x060005FF RID: 1535
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_GetMute(IntPtr bus, out bool mute);

		// Token: 0x06000600 RID: 1536
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_SetMute(IntPtr bus, bool mute);

		// Token: 0x06000601 RID: 1537
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_StopAllEvents(IntPtr bus, STOP_MODE mode);

		// Token: 0x06000602 RID: 1538
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_LockChannelGroup(IntPtr bus);

		// Token: 0x06000603 RID: 1539
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_UnlockChannelGroup(IntPtr bus);

		// Token: 0x06000604 RID: 1540
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_Bus_GetChannelGroup(IntPtr bus, out IntPtr group);

		// Token: 0x06000605 RID: 1541 RVA: 0x000084E4 File Offset: 0x000068E4
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000084F6 File Offset: 0x000068F6
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00008503 File Offset: 0x00006903
		public bool isValid()
		{
			return this.hasHandle() && Bus.FMOD_Studio_Bus_IsValid(this.handle);
		}

		// Token: 0x040004ED RID: 1261
		public IntPtr handle;
	}
}
