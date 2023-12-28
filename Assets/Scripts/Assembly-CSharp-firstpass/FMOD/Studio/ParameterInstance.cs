using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000FB RID: 251
	public struct ParameterInstance
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x00008349 File Offset: 0x00006749
		public RESULT getDescription(out PARAMETER_DESCRIPTION description)
		{
			return ParameterInstance.FMOD_Studio_ParameterInstance_GetDescription(this.handle, out description);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00008357 File Offset: 0x00006757
		public RESULT getValue(out float value)
		{
			return ParameterInstance.FMOD_Studio_ParameterInstance_GetValue(this.handle, out value);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00008365 File Offset: 0x00006765
		public RESULT setValue(float value)
		{
			return ParameterInstance.FMOD_Studio_ParameterInstance_SetValue(this.handle, value);
		}

		// Token: 0x060005E5 RID: 1509
		[DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_ParameterInstance_IsValid(IntPtr parameter);

		// Token: 0x060005E6 RID: 1510
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_ParameterInstance_GetDescription(IntPtr parameter, out PARAMETER_DESCRIPTION description);

		// Token: 0x060005E7 RID: 1511
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_ParameterInstance_GetValue(IntPtr parameter, out float value);

		// Token: 0x060005E8 RID: 1512
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_ParameterInstance_SetValue(IntPtr parameter, float value);

		// Token: 0x060005E9 RID: 1513 RVA: 0x00008373 File Offset: 0x00006773
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00008385 File Offset: 0x00006785
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00008392 File Offset: 0x00006792
		public bool isValid()
		{
			return this.hasHandle() && ParameterInstance.FMOD_Studio_ParameterInstance_IsValid(this.handle);
		}

		// Token: 0x040004EC RID: 1260
		public IntPtr handle;
	}
}
