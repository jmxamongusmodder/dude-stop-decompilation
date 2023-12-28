using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x02000068 RID: 104
	public struct Reverb3D
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x00006DFC File Offset: 0x000051FC
		public RESULT release()
		{
			return Reverb3D.FMOD5_Reverb3D_Release(this.handle);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00006E09 File Offset: 0x00005209
		public RESULT set3DAttributes(ref VECTOR position, float mindistance, float maxdistance)
		{
			return Reverb3D.FMOD5_Reverb3D_Set3DAttributes(this.handle, ref position, mindistance, maxdistance);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00006E19 File Offset: 0x00005219
		public RESULT get3DAttributes(ref VECTOR position, ref float mindistance, ref float maxdistance)
		{
			return Reverb3D.FMOD5_Reverb3D_Get3DAttributes(this.handle, ref position, ref mindistance, ref maxdistance);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00006E29 File Offset: 0x00005229
		public RESULT setProperties(ref REVERB_PROPERTIES properties)
		{
			return Reverb3D.FMOD5_Reverb3D_SetProperties(this.handle, ref properties);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00006E37 File Offset: 0x00005237
		public RESULT getProperties(ref REVERB_PROPERTIES properties)
		{
			return Reverb3D.FMOD5_Reverb3D_GetProperties(this.handle, ref properties);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00006E45 File Offset: 0x00005245
		public RESULT setActive(bool active)
		{
			return Reverb3D.FMOD5_Reverb3D_SetActive(this.handle, active);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00006E53 File Offset: 0x00005253
		public RESULT getActive(out bool active)
		{
			return Reverb3D.FMOD5_Reverb3D_GetActive(this.handle, out active);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00006E61 File Offset: 0x00005261
		public RESULT setUserData(IntPtr userdata)
		{
			return Reverb3D.FMOD5_Reverb3D_SetUserData(this.handle, userdata);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00006E6F File Offset: 0x0000526F
		public RESULT getUserData(out IntPtr userdata)
		{
			return Reverb3D.FMOD5_Reverb3D_GetUserData(this.handle, out userdata);
		}

		// Token: 0x06000444 RID: 1092
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_Release(IntPtr reverb);

		// Token: 0x06000445 RID: 1093
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_Set3DAttributes(IntPtr reverb, ref VECTOR position, float mindistance, float maxdistance);

		// Token: 0x06000446 RID: 1094
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_Get3DAttributes(IntPtr reverb, ref VECTOR position, ref float mindistance, ref float maxdistance);

		// Token: 0x06000447 RID: 1095
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_SetProperties(IntPtr reverb, ref REVERB_PROPERTIES properties);

		// Token: 0x06000448 RID: 1096
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_GetProperties(IntPtr reverb, ref REVERB_PROPERTIES properties);

		// Token: 0x06000449 RID: 1097
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_SetActive(IntPtr reverb, bool active);

		// Token: 0x0600044A RID: 1098
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_GetActive(IntPtr reverb, out bool active);

		// Token: 0x0600044B RID: 1099
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_SetUserData(IntPtr reverb, IntPtr userdata);

		// Token: 0x0600044C RID: 1100
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Reverb3D_GetUserData(IntPtr reverb, out IntPtr userdata);

		// Token: 0x0600044D RID: 1101 RVA: 0x00006E7D File Offset: 0x0000527D
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00006E8F File Offset: 0x0000528F
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x0400028C RID: 652
		public IntPtr handle;
	}
}
