using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x02000066 RID: 102
	public struct DSPConnection
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00006BFE File Offset: 0x00004FFE
		public RESULT getInput(out DSP input)
		{
			return DSPConnection.FMOD5_DSPConnection_GetInput(this.handle, out input.handle);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00006C11 File Offset: 0x00005011
		public RESULT getOutput(out DSP output)
		{
			return DSPConnection.FMOD5_DSPConnection_GetOutput(this.handle, out output.handle);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00006C24 File Offset: 0x00005024
		public RESULT setMix(float volume)
		{
			return DSPConnection.FMOD5_DSPConnection_SetMix(this.handle, volume);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00006C32 File Offset: 0x00005032
		public RESULT getMix(out float volume)
		{
			return DSPConnection.FMOD5_DSPConnection_GetMix(this.handle, out volume);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00006C40 File Offset: 0x00005040
		public RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop)
		{
			return DSPConnection.FMOD5_DSPConnection_SetMixMatrix(this.handle, matrix, outchannels, inchannels, inchannel_hop);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00006C52 File Offset: 0x00005052
		public RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop)
		{
			return DSPConnection.FMOD5_DSPConnection_GetMixMatrix(this.handle, matrix, out outchannels, out inchannels, inchannel_hop);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00006C64 File Offset: 0x00005064
		public RESULT getType(out DSPCONNECTION_TYPE type)
		{
			return DSPConnection.FMOD5_DSPConnection_GetType(this.handle, out type);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00006C72 File Offset: 0x00005072
		public RESULT setUserData(IntPtr userdata)
		{
			return DSPConnection.FMOD5_DSPConnection_SetUserData(this.handle, userdata);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00006C80 File Offset: 0x00005080
		public RESULT getUserData(out IntPtr userdata)
		{
			return DSPConnection.FMOD5_DSPConnection_GetUserData(this.handle, out userdata);
		}

		// Token: 0x06000406 RID: 1030
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_GetInput(IntPtr dspconnection, out IntPtr input);

		// Token: 0x06000407 RID: 1031
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_GetOutput(IntPtr dspconnection, out IntPtr output);

		// Token: 0x06000408 RID: 1032
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_SetMix(IntPtr dspconnection, float volume);

		// Token: 0x06000409 RID: 1033
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_GetMix(IntPtr dspconnection, out float volume);

		// Token: 0x0600040A RID: 1034
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_SetMixMatrix(IntPtr dspconnection, float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		// Token: 0x0600040B RID: 1035
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_GetMixMatrix(IntPtr dspconnection, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);

		// Token: 0x0600040C RID: 1036
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_GetType(IntPtr dspconnection, out DSPCONNECTION_TYPE type);

		// Token: 0x0600040D RID: 1037
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_SetUserData(IntPtr dspconnection, IntPtr userdata);

		// Token: 0x0600040E RID: 1038
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSPConnection_GetUserData(IntPtr dspconnection, out IntPtr userdata);

		// Token: 0x0600040F RID: 1039 RVA: 0x00006C8E File Offset: 0x0000508E
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00006CA0 File Offset: 0x000050A0
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x0400028A RID: 650
		public IntPtr handle;
	}
}
