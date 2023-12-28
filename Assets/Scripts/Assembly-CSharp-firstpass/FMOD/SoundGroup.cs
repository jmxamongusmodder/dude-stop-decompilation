using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x02000064 RID: 100
	public struct SoundGroup
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0000676A File Offset: 0x00004B6A
		public RESULT release()
		{
			return SoundGroup.FMOD5_SoundGroup_Release(this.handle);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00006777 File Offset: 0x00004B77
		public RESULT getSystemObject(out System system)
		{
			return SoundGroup.FMOD5_SoundGroup_GetSystemObject(this.handle, out system.handle);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000678A File Offset: 0x00004B8A
		public RESULT setMaxAudible(int maxaudible)
		{
			return SoundGroup.FMOD5_SoundGroup_SetMaxAudible(this.handle, maxaudible);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00006798 File Offset: 0x00004B98
		public RESULT getMaxAudible(out int maxaudible)
		{
			return SoundGroup.FMOD5_SoundGroup_GetMaxAudible(this.handle, out maxaudible);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000067A6 File Offset: 0x00004BA6
		public RESULT setMaxAudibleBehavior(SOUNDGROUP_BEHAVIOR behavior)
		{
			return SoundGroup.FMOD5_SoundGroup_SetMaxAudibleBehavior(this.handle, behavior);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000067B4 File Offset: 0x00004BB4
		public RESULT getMaxAudibleBehavior(out SOUNDGROUP_BEHAVIOR behavior)
		{
			return SoundGroup.FMOD5_SoundGroup_GetMaxAudibleBehavior(this.handle, out behavior);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000067C2 File Offset: 0x00004BC2
		public RESULT setMuteFadeSpeed(float speed)
		{
			return SoundGroup.FMOD5_SoundGroup_SetMuteFadeSpeed(this.handle, speed);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000067D0 File Offset: 0x00004BD0
		public RESULT getMuteFadeSpeed(out float speed)
		{
			return SoundGroup.FMOD5_SoundGroup_GetMuteFadeSpeed(this.handle, out speed);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000067DE File Offset: 0x00004BDE
		public RESULT setVolume(float volume)
		{
			return SoundGroup.FMOD5_SoundGroup_SetVolume(this.handle, volume);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000067EC File Offset: 0x00004BEC
		public RESULT getVolume(out float volume)
		{
			return SoundGroup.FMOD5_SoundGroup_GetVolume(this.handle, out volume);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000067FA File Offset: 0x00004BFA
		public RESULT stop()
		{
			return SoundGroup.FMOD5_SoundGroup_Stop(this.handle);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00006808 File Offset: 0x00004C08
		public RESULT getName(out string name, int namelen)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(namelen);
			RESULT result = SoundGroup.FMOD5_SoundGroup_GetName(this.handle, intPtr, namelen);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				name = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00006864 File Offset: 0x00004C64
		public RESULT getNumSounds(out int numsounds)
		{
			return SoundGroup.FMOD5_SoundGroup_GetNumSounds(this.handle, out numsounds);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00006872 File Offset: 0x00004C72
		public RESULT getSound(int index, out Sound sound)
		{
			return SoundGroup.FMOD5_SoundGroup_GetSound(this.handle, index, out sound.handle);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00006886 File Offset: 0x00004C86
		public RESULT getNumPlaying(out int numplaying)
		{
			return SoundGroup.FMOD5_SoundGroup_GetNumPlaying(this.handle, out numplaying);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00006894 File Offset: 0x00004C94
		public RESULT setUserData(IntPtr userdata)
		{
			return SoundGroup.FMOD5_SoundGroup_SetUserData(this.handle, userdata);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000068A2 File Offset: 0x00004CA2
		public RESULT getUserData(out IntPtr userdata)
		{
			return SoundGroup.FMOD5_SoundGroup_GetUserData(this.handle, out userdata);
		}

		// Token: 0x06000397 RID: 919
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_Release(IntPtr soundgroup);

		// Token: 0x06000398 RID: 920
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetSystemObject(IntPtr soundgroup, out IntPtr system);

		// Token: 0x06000399 RID: 921
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_SetMaxAudible(IntPtr soundgroup, int maxaudible);

		// Token: 0x0600039A RID: 922
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetMaxAudible(IntPtr soundgroup, out int maxaudible);

		// Token: 0x0600039B RID: 923
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_SetMaxAudibleBehavior(IntPtr soundgroup, SOUNDGROUP_BEHAVIOR behavior);

		// Token: 0x0600039C RID: 924
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetMaxAudibleBehavior(IntPtr soundgroup, out SOUNDGROUP_BEHAVIOR behavior);

		// Token: 0x0600039D RID: 925
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_SetMuteFadeSpeed(IntPtr soundgroup, float speed);

		// Token: 0x0600039E RID: 926
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetMuteFadeSpeed(IntPtr soundgroup, out float speed);

		// Token: 0x0600039F RID: 927
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_SetVolume(IntPtr soundgroup, float volume);

		// Token: 0x060003A0 RID: 928
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetVolume(IntPtr soundgroup, out float volume);

		// Token: 0x060003A1 RID: 929
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_Stop(IntPtr soundgroup);

		// Token: 0x060003A2 RID: 930
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetName(IntPtr soundgroup, IntPtr name, int namelen);

		// Token: 0x060003A3 RID: 931
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetNumSounds(IntPtr soundgroup, out int numsounds);

		// Token: 0x060003A4 RID: 932
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetSound(IntPtr soundgroup, int index, out IntPtr sound);

		// Token: 0x060003A5 RID: 933
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetNumPlaying(IntPtr soundgroup, out int numplaying);

		// Token: 0x060003A6 RID: 934
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_SetUserData(IntPtr soundgroup, IntPtr userdata);

		// Token: 0x060003A7 RID: 935
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_SoundGroup_GetUserData(IntPtr soundgroup, out IntPtr userdata);

		// Token: 0x060003A8 RID: 936 RVA: 0x000068B0 File Offset: 0x00004CB0
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000068C2 File Offset: 0x00004CC2
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x04000288 RID: 648
		public IntPtr handle;
	}
}
