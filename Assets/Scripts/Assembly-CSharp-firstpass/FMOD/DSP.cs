using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x02000065 RID: 101
	public struct DSP
	{
		// Token: 0x060003AA RID: 938 RVA: 0x000068CF File Offset: 0x00004CCF
		public RESULT release()
		{
			return DSP.FMOD5_DSP_Release(this.handle);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000068DC File Offset: 0x00004CDC
		public RESULT getSystemObject(out System system)
		{
			return DSP.FMOD5_DSP_GetSystemObject(this.handle, out system.handle);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000068EF File Offset: 0x00004CEF
		public RESULT addInput(DSP target, out DSPConnection connection, DSPCONNECTION_TYPE type)
		{
			return DSP.FMOD5_DSP_AddInput(this.handle, target.handle, out connection.handle, type);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000690A File Offset: 0x00004D0A
		public RESULT disconnectFrom(DSP target, DSPConnection connection)
		{
			return DSP.FMOD5_DSP_DisconnectFrom(this.handle, target.handle, connection.handle);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00006925 File Offset: 0x00004D25
		public RESULT disconnectAll(bool inputs, bool outputs)
		{
			return DSP.FMOD5_DSP_DisconnectAll(this.handle, inputs, outputs);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00006934 File Offset: 0x00004D34
		public RESULT getNumInputs(out int numinputs)
		{
			return DSP.FMOD5_DSP_GetNumInputs(this.handle, out numinputs);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00006942 File Offset: 0x00004D42
		public RESULT getNumOutputs(out int numoutputs)
		{
			return DSP.FMOD5_DSP_GetNumOutputs(this.handle, out numoutputs);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00006950 File Offset: 0x00004D50
		public RESULT getInput(int index, out DSP input, out DSPConnection inputconnection)
		{
			return DSP.FMOD5_DSP_GetInput(this.handle, index, out input.handle, out inputconnection.handle);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000696A File Offset: 0x00004D6A
		public RESULT getOutput(int index, out DSP output, out DSPConnection outputconnection)
		{
			return DSP.FMOD5_DSP_GetOutput(this.handle, index, out output.handle, out outputconnection.handle);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00006984 File Offset: 0x00004D84
		public RESULT setActive(bool active)
		{
			return DSP.FMOD5_DSP_SetActive(this.handle, active);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00006992 File Offset: 0x00004D92
		public RESULT getActive(out bool active)
		{
			return DSP.FMOD5_DSP_GetActive(this.handle, out active);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000069A0 File Offset: 0x00004DA0
		public RESULT setBypass(bool bypass)
		{
			return DSP.FMOD5_DSP_SetBypass(this.handle, bypass);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000069AE File Offset: 0x00004DAE
		public RESULT getBypass(out bool bypass)
		{
			return DSP.FMOD5_DSP_GetBypass(this.handle, out bypass);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000069BC File Offset: 0x00004DBC
		public RESULT setWetDryMix(float prewet, float postwet, float dry)
		{
			return DSP.FMOD5_DSP_SetWetDryMix(this.handle, prewet, postwet, dry);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000069CC File Offset: 0x00004DCC
		public RESULT getWetDryMix(out float prewet, out float postwet, out float dry)
		{
			return DSP.FMOD5_DSP_GetWetDryMix(this.handle, out prewet, out postwet, out dry);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000069DC File Offset: 0x00004DDC
		public RESULT setChannelFormat(CHANNELMASK channelmask, int numchannels, SPEAKERMODE source_speakermode)
		{
			return DSP.FMOD5_DSP_SetChannelFormat(this.handle, channelmask, numchannels, source_speakermode);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000069EC File Offset: 0x00004DEC
		public RESULT getChannelFormat(out CHANNELMASK channelmask, out int numchannels, out SPEAKERMODE source_speakermode)
		{
			return DSP.FMOD5_DSP_GetChannelFormat(this.handle, out channelmask, out numchannels, out source_speakermode);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000069FC File Offset: 0x00004DFC
		public RESULT getOutputChannelFormat(CHANNELMASK inmask, int inchannels, SPEAKERMODE inspeakermode, out CHANNELMASK outmask, out int outchannels, out SPEAKERMODE outspeakermode)
		{
			return DSP.FMOD5_DSP_GetOutputChannelFormat(this.handle, inmask, inchannels, inspeakermode, out outmask, out outchannels, out outspeakermode);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00006A12 File Offset: 0x00004E12
		public RESULT reset()
		{
			return DSP.FMOD5_DSP_Reset(this.handle);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00006A1F File Offset: 0x00004E1F
		public RESULT setParameterFloat(int index, float value)
		{
			return DSP.FMOD5_DSP_SetParameterFloat(this.handle, index, value);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00006A2E File Offset: 0x00004E2E
		public RESULT setParameterInt(int index, int value)
		{
			return DSP.FMOD5_DSP_SetParameterInt(this.handle, index, value);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00006A3D File Offset: 0x00004E3D
		public RESULT setParameterBool(int index, bool value)
		{
			return DSP.FMOD5_DSP_SetParameterBool(this.handle, index, value);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00006A4C File Offset: 0x00004E4C
		public RESULT setParameterData(int index, byte[] data)
		{
			return DSP.FMOD5_DSP_SetParameterData(this.handle, index, Marshal.UnsafeAddrOfPinnedArrayElement(data, 0), (uint)data.Length);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00006A64 File Offset: 0x00004E64
		public RESULT getParameterFloat(int index, out float value)
		{
			return DSP.FMOD5_DSP_GetParameterFloat(this.handle, index, out value, IntPtr.Zero, 0);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00006A79 File Offset: 0x00004E79
		public RESULT getParameterInt(int index, out int value)
		{
			return DSP.FMOD5_DSP_GetParameterInt(this.handle, index, out value, IntPtr.Zero, 0);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00006A8E File Offset: 0x00004E8E
		public RESULT getParameterBool(int index, out bool value)
		{
			return DSP.FMOD5_DSP_GetParameterBool(this.handle, index, out value, IntPtr.Zero, 0);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00006AA3 File Offset: 0x00004EA3
		public RESULT getParameterData(int index, out IntPtr data, out uint length)
		{
			return DSP.FMOD5_DSP_GetParameterData(this.handle, index, out data, out length, IntPtr.Zero, 0);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00006AB9 File Offset: 0x00004EB9
		public RESULT getNumParameters(out int numparams)
		{
			return DSP.FMOD5_DSP_GetNumParameters(this.handle, out numparams);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00006AC7 File Offset: 0x00004EC7
		public RESULT getParameterInfo(int index, out DSP_PARAMETER_DESC desc)
		{
			return DSP.FMOD5_DSP_GetParameterInfo(this.handle, index, out desc);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00006AD6 File Offset: 0x00004ED6
		public RESULT getDataParameterIndex(int datatype, out int index)
		{
			return DSP.FMOD5_DSP_GetDataParameterIndex(this.handle, datatype, out index);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00006AE5 File Offset: 0x00004EE5
		public RESULT showConfigDialog(IntPtr hwnd, bool show)
		{
			return DSP.FMOD5_DSP_ShowConfigDialog(this.handle, hwnd, show);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00006AF4 File Offset: 0x00004EF4
		public RESULT getInfo(out string name, out uint version, out int channels, out int configwidth, out int configheight)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(32);
			RESULT result = DSP.FMOD5_DSP_GetInfo(this.handle, intPtr, out version, out channels, out configwidth, out configheight);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				name = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00006B54 File Offset: 0x00004F54
		public RESULT getInfo(out uint version, out int channels, out int configwidth, out int configheight)
		{
			return DSP.FMOD5_DSP_GetInfo(this.handle, IntPtr.Zero, out version, out channels, out configwidth, out configheight);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00006B6B File Offset: 0x00004F6B
		public RESULT getType(out DSP_TYPE type)
		{
			return DSP.FMOD5_DSP_GetType(this.handle, out type);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00006B79 File Offset: 0x00004F79
		public RESULT getIdle(out bool idle)
		{
			return DSP.FMOD5_DSP_GetIdle(this.handle, out idle);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00006B87 File Offset: 0x00004F87
		public RESULT setUserData(IntPtr userdata)
		{
			return DSP.FMOD5_DSP_SetUserData(this.handle, userdata);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00006B95 File Offset: 0x00004F95
		public RESULT getUserData(out IntPtr userdata)
		{
			return DSP.FMOD5_DSP_GetUserData(this.handle, out userdata);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00006BA3 File Offset: 0x00004FA3
		public RESULT setMeteringEnabled(bool inputEnabled, bool outputEnabled)
		{
			return DSP.FMOD5_DSP_SetMeteringEnabled(this.handle, inputEnabled, outputEnabled);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00006BB2 File Offset: 0x00004FB2
		public RESULT getMeteringEnabled(out bool inputEnabled, out bool outputEnabled)
		{
			return DSP.FMOD5_DSP_GetMeteringEnabled(this.handle, out inputEnabled, out outputEnabled);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00006BC1 File Offset: 0x00004FC1
		public RESULT getMeteringInfo(IntPtr zero, out DSP_METERING_INFO outputInfo)
		{
			return DSP.FMOD5_DSP_GetMeteringInfo(this.handle, zero, out outputInfo);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00006BD0 File Offset: 0x00004FD0
		public RESULT getMeteringInfo(out DSP_METERING_INFO inputInfo, IntPtr zero)
		{
			return DSP.FMOD5_DSP_GetMeteringInfo(this.handle, out inputInfo, zero);
		}

		// Token: 0x060003D3 RID: 979
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_Release(IntPtr dsp);

		// Token: 0x060003D4 RID: 980
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetSystemObject(IntPtr dsp, out IntPtr system);

		// Token: 0x060003D5 RID: 981
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_AddInput(IntPtr dsp, IntPtr target, out IntPtr connection, DSPCONNECTION_TYPE type);

		// Token: 0x060003D6 RID: 982
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_DisconnectFrom(IntPtr dsp, IntPtr target, IntPtr connection);

		// Token: 0x060003D7 RID: 983
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_DisconnectAll(IntPtr dsp, bool inputs, bool outputs);

		// Token: 0x060003D8 RID: 984
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetNumInputs(IntPtr dsp, out int numinputs);

		// Token: 0x060003D9 RID: 985
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetNumOutputs(IntPtr dsp, out int numoutputs);

		// Token: 0x060003DA RID: 986
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetInput(IntPtr dsp, int index, out IntPtr input, out IntPtr inputconnection);

		// Token: 0x060003DB RID: 987
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetOutput(IntPtr dsp, int index, out IntPtr output, out IntPtr outputconnection);

		// Token: 0x060003DC RID: 988
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetActive(IntPtr dsp, bool active);

		// Token: 0x060003DD RID: 989
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetActive(IntPtr dsp, out bool active);

		// Token: 0x060003DE RID: 990
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetBypass(IntPtr dsp, bool bypass);

		// Token: 0x060003DF RID: 991
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetBypass(IntPtr dsp, out bool bypass);

		// Token: 0x060003E0 RID: 992
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetWetDryMix(IntPtr dsp, float prewet, float postwet, float dry);

		// Token: 0x060003E1 RID: 993
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetWetDryMix(IntPtr dsp, out float prewet, out float postwet, out float dry);

		// Token: 0x060003E2 RID: 994
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetChannelFormat(IntPtr dsp, CHANNELMASK channelmask, int numchannels, SPEAKERMODE source_speakermode);

		// Token: 0x060003E3 RID: 995
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetChannelFormat(IntPtr dsp, out CHANNELMASK channelmask, out int numchannels, out SPEAKERMODE source_speakermode);

		// Token: 0x060003E4 RID: 996
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetOutputChannelFormat(IntPtr dsp, CHANNELMASK inmask, int inchannels, SPEAKERMODE inspeakermode, out CHANNELMASK outmask, out int outchannels, out SPEAKERMODE outspeakermode);

		// Token: 0x060003E5 RID: 997
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_Reset(IntPtr dsp);

		// Token: 0x060003E6 RID: 998
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetParameterFloat(IntPtr dsp, int index, float value);

		// Token: 0x060003E7 RID: 999
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetParameterInt(IntPtr dsp, int index, int value);

		// Token: 0x060003E8 RID: 1000
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetParameterBool(IntPtr dsp, int index, bool value);

		// Token: 0x060003E9 RID: 1001
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetParameterData(IntPtr dsp, int index, IntPtr data, uint length);

		// Token: 0x060003EA RID: 1002
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetParameterFloat(IntPtr dsp, int index, out float value, IntPtr valuestr, int valuestrlen);

		// Token: 0x060003EB RID: 1003
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetParameterInt(IntPtr dsp, int index, out int value, IntPtr valuestr, int valuestrlen);

		// Token: 0x060003EC RID: 1004
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetParameterBool(IntPtr dsp, int index, out bool value, IntPtr valuestr, int valuestrlen);

		// Token: 0x060003ED RID: 1005
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetParameterData(IntPtr dsp, int index, out IntPtr data, out uint length, IntPtr valuestr, int valuestrlen);

		// Token: 0x060003EE RID: 1006
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetNumParameters(IntPtr dsp, out int numparams);

		// Token: 0x060003EF RID: 1007
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetParameterInfo(IntPtr dsp, int index, out DSP_PARAMETER_DESC desc);

		// Token: 0x060003F0 RID: 1008
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetDataParameterIndex(IntPtr dsp, int datatype, out int index);

		// Token: 0x060003F1 RID: 1009
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_ShowConfigDialog(IntPtr dsp, IntPtr hwnd, bool show);

		// Token: 0x060003F2 RID: 1010
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetInfo(IntPtr dsp, IntPtr name, out uint version, out int channels, out int configwidth, out int configheight);

		// Token: 0x060003F3 RID: 1011
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetType(IntPtr dsp, out DSP_TYPE type);

		// Token: 0x060003F4 RID: 1012
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetIdle(IntPtr dsp, out bool idle);

		// Token: 0x060003F5 RID: 1013
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_SetUserData(IntPtr dsp, IntPtr userdata);

		// Token: 0x060003F6 RID: 1014
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_DSP_GetUserData(IntPtr dsp, out IntPtr userdata);

		// Token: 0x060003F7 RID: 1015
		[DllImport("fmodstudio")]
		public static extern RESULT FMOD5_DSP_SetMeteringEnabled(IntPtr dsp, bool inputEnabled, bool outputEnabled);

		// Token: 0x060003F8 RID: 1016
		[DllImport("fmodstudio")]
		public static extern RESULT FMOD5_DSP_GetMeteringEnabled(IntPtr dsp, out bool inputEnabled, out bool outputEnabled);

		// Token: 0x060003F9 RID: 1017
		[DllImport("fmodstudio")]
		public static extern RESULT FMOD5_DSP_GetMeteringInfo(IntPtr dsp, IntPtr zero, out DSP_METERING_INFO outputInfo);

		// Token: 0x060003FA RID: 1018
		[DllImport("fmodstudio")]
		public static extern RESULT FMOD5_DSP_GetMeteringInfo(IntPtr dsp, out DSP_METERING_INFO inputInfo, IntPtr zero);

		// Token: 0x060003FB RID: 1019 RVA: 0x00006BDF File Offset: 0x00004FDF
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00006BF1 File Offset: 0x00004FF1
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x04000289 RID: 649
		public IntPtr handle;
	}
}
