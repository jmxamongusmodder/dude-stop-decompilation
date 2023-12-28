using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000FD RID: 253
	public struct VCA
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0000851E File Offset: 0x0000691E
		public RESULT getID(out Guid id)
		{
			return VCA.FMOD_Studio_VCA_GetID(this.handle, out id);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000852C File Offset: 0x0000692C
		public RESULT getPath(out string path)
		{
			path = null;
			RESULT result2;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				IntPtr intPtr = Marshal.AllocHGlobal(256);
				int num = 0;
				RESULT result = VCA.FMOD_Studio_VCA_GetPath(this.handle, intPtr, 256, out num);
				if (result == RESULT.ERR_TRUNCATED)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = Marshal.AllocHGlobal(num);
					result = VCA.FMOD_Studio_VCA_GetPath(this.handle, intPtr, num, out num);
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

		// Token: 0x0600060A RID: 1546 RVA: 0x000085C4 File Offset: 0x000069C4
		public RESULT getVolume(out float volume, out float finalvolume)
		{
			return VCA.FMOD_Studio_VCA_GetVolume(this.handle, out volume, out finalvolume);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000085D3 File Offset: 0x000069D3
		public RESULT setVolume(float volume)
		{
			return VCA.FMOD_Studio_VCA_SetVolume(this.handle, volume);
		}

		// Token: 0x0600060C RID: 1548
		[DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_VCA_IsValid(IntPtr vca);

		// Token: 0x0600060D RID: 1549
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_VCA_GetID(IntPtr vca, out Guid id);

		// Token: 0x0600060E RID: 1550
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_VCA_GetPath(IntPtr vca, IntPtr path, int size, out int retrieved);

		// Token: 0x0600060F RID: 1551
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_VCA_GetVolume(IntPtr vca, out float volume, out float finalvolume);

		// Token: 0x06000610 RID: 1552
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_VCA_SetVolume(IntPtr vca, float value);

		// Token: 0x06000611 RID: 1553 RVA: 0x000085E1 File Offset: 0x000069E1
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000085F3 File Offset: 0x000069F3
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00008600 File Offset: 0x00006A00
		public bool isValid()
		{
			return this.hasHandle() && VCA.FMOD_Studio_VCA_IsValid(this.handle);
		}

		// Token: 0x040004EE RID: 1262
		public IntPtr handle;
	}
}
