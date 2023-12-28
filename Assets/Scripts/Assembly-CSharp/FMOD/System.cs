using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x0200005F RID: 95
	public struct System
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00004FB0 File Offset: 0x000033B0
		public RESULT release()
		{
			return System.FMOD5_System_Release(this.handle);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004FBD File Offset: 0x000033BD
		public RESULT setOutput(OUTPUTTYPE output)
		{
			return System.FMOD5_System_SetOutput(this.handle, output);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004FCB File Offset: 0x000033CB
		public RESULT getOutput(out OUTPUTTYPE output)
		{
			return System.FMOD5_System_GetOutput(this.handle, out output);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004FD9 File Offset: 0x000033D9
		public RESULT getNumDrivers(out int numdrivers)
		{
			return System.FMOD5_System_GetNumDrivers(this.handle, out numdrivers);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004FE8 File Offset: 0x000033E8
		public RESULT getDriverInfo(int id, out string name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(namelen);
			RESULT result = System.FMOD5_System_GetDriverInfo(this.handle, id, intPtr, namelen, out guid, out systemrate, out speakermode, out speakermodechannels);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				name = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000504C File Offset: 0x0000344C
		public RESULT getDriverInfo(int id, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels)
		{
			return System.FMOD5_System_GetDriverInfo(this.handle, id, IntPtr.Zero, 0, out guid, out systemrate, out speakermode, out speakermodechannels);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005066 File Offset: 0x00003466
		public RESULT setDriver(int driver)
		{
			return System.FMOD5_System_SetDriver(this.handle, driver);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005074 File Offset: 0x00003474
		public RESULT getDriver(out int driver)
		{
			return System.FMOD5_System_GetDriver(this.handle, out driver);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005082 File Offset: 0x00003482
		public RESULT setSoftwareChannels(int numsoftwarechannels)
		{
			return System.FMOD5_System_SetSoftwareChannels(this.handle, numsoftwarechannels);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005090 File Offset: 0x00003490
		public RESULT getSoftwareChannels(out int numsoftwarechannels)
		{
			return System.FMOD5_System_GetSoftwareChannels(this.handle, out numsoftwarechannels);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000509E File Offset: 0x0000349E
		public RESULT setSoftwareFormat(int samplerate, SPEAKERMODE speakermode, int numrawspeakers)
		{
			return System.FMOD5_System_SetSoftwareFormat(this.handle, samplerate, speakermode, numrawspeakers);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000050AE File Offset: 0x000034AE
		public RESULT getSoftwareFormat(out int samplerate, out SPEAKERMODE speakermode, out int numrawspeakers)
		{
			return System.FMOD5_System_GetSoftwareFormat(this.handle, out samplerate, out speakermode, out numrawspeakers);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000050BE File Offset: 0x000034BE
		public RESULT setDSPBufferSize(uint bufferlength, int numbuffers)
		{
			return System.FMOD5_System_SetDSPBufferSize(this.handle, bufferlength, numbuffers);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000050CD File Offset: 0x000034CD
		public RESULT getDSPBufferSize(out uint bufferlength, out int numbuffers)
		{
			return System.FMOD5_System_GetDSPBufferSize(this.handle, out bufferlength, out numbuffers);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000050DC File Offset: 0x000034DC
		public RESULT setFileSystem(FILE_OPENCALLBACK useropen, FILE_CLOSECALLBACK userclose, FILE_READCALLBACK userread, FILE_SEEKCALLBACK userseek, FILE_ASYNCREADCALLBACK userasyncread, FILE_ASYNCCANCELCALLBACK userasynccancel, int blockalign)
		{
			return System.FMOD5_System_SetFileSystem(this.handle, useropen, userclose, userread, userseek, userasyncread, userasynccancel, blockalign);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000050F4 File Offset: 0x000034F4
		public RESULT attachFileSystem(FILE_OPENCALLBACK useropen, FILE_CLOSECALLBACK userclose, FILE_READCALLBACK userread, FILE_SEEKCALLBACK userseek)
		{
			return System.FMOD5_System_AttachFileSystem(this.handle, useropen, userclose, userread, userseek);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005106 File Offset: 0x00003506
		public RESULT setAdvancedSettings(ref ADVANCEDSETTINGS settings)
		{
			settings.cbSize = Marshal.SizeOf(settings);
			return System.FMOD5_System_SetAdvancedSettings(this.handle, ref settings);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000512A File Offset: 0x0000352A
		public RESULT getAdvancedSettings(ref ADVANCEDSETTINGS settings)
		{
			settings.cbSize = Marshal.SizeOf(settings);
			return System.FMOD5_System_GetAdvancedSettings(this.handle, ref settings);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000514E File Offset: 0x0000354E
		public RESULT setCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask)
		{
			return System.FMOD5_System_SetCallback(this.handle, callback, callbackmask);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005160 File Offset: 0x00003560
		public RESULT setPluginPath(string path)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD5_System_SetPluginPath(this.handle, freeHelper.byteFromStringUTF8(path));
			}
			return result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000051AC File Offset: 0x000035AC
		public RESULT loadPlugin(string filename, out uint handle, uint priority)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD5_System_LoadPlugin(this.handle, freeHelper.byteFromStringUTF8(filename), out handle, priority);
			}
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000051F8 File Offset: 0x000035F8
		public RESULT loadPlugin(string filename, out uint handle)
		{
			return this.loadPlugin(filename, out handle, 0U);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005203 File Offset: 0x00003603
		public RESULT unloadPlugin(uint handle)
		{
			return System.FMOD5_System_UnloadPlugin(this.handle, handle);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005211 File Offset: 0x00003611
		public RESULT getNumNestedPlugins(uint handle, out int count)
		{
			return System.FMOD5_System_GetNumNestedPlugins(this.handle, handle, out count);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005220 File Offset: 0x00003620
		public RESULT getNestedPlugin(uint handle, int index, out uint nestedhandle)
		{
			return System.FMOD5_System_GetNestedPlugin(this.handle, handle, index, out nestedhandle);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005230 File Offset: 0x00003630
		public RESULT getNumPlugins(PLUGINTYPE plugintype, out int numplugins)
		{
			return System.FMOD5_System_GetNumPlugins(this.handle, plugintype, out numplugins);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000523F File Offset: 0x0000363F
		public RESULT getPluginHandle(PLUGINTYPE plugintype, int index, out uint handle)
		{
			return System.FMOD5_System_GetPluginHandle(this.handle, plugintype, index, out handle);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005250 File Offset: 0x00003650
		public RESULT getPluginInfo(uint handle, out PLUGINTYPE plugintype, out string name, int namelen, out uint version)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(namelen);
			RESULT result = System.FMOD5_System_GetPluginInfo(this.handle, handle, out plugintype, intPtr, namelen, out version);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				name = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000052B0 File Offset: 0x000036B0
		public RESULT getPluginInfo(uint handle, out PLUGINTYPE plugintype, out uint version)
		{
			return System.FMOD5_System_GetPluginInfo(this.handle, handle, out plugintype, IntPtr.Zero, 0, out version);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000052C6 File Offset: 0x000036C6
		public RESULT setOutputByPlugin(uint handle)
		{
			return System.FMOD5_System_SetOutputByPlugin(this.handle, handle);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000052D4 File Offset: 0x000036D4
		public RESULT getOutputByPlugin(out uint handle)
		{
			return System.FMOD5_System_GetOutputByPlugin(this.handle, out handle);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000052E2 File Offset: 0x000036E2
		public RESULT createDSPByPlugin(uint handle, out DSP dsp)
		{
			return System.FMOD5_System_CreateDSPByPlugin(this.handle, handle, out dsp.handle);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000052F6 File Offset: 0x000036F6
		public RESULT getDSPInfoByPlugin(uint handle, out IntPtr description)
		{
			return System.FMOD5_System_GetDSPInfoByPlugin(this.handle, handle, out description);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005305 File Offset: 0x00003705
		public RESULT registerDSP(ref DSP_DESCRIPTION description, out uint handle)
		{
			return System.FMOD5_System_RegisterDSP(this.handle, ref description, out handle);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005314 File Offset: 0x00003714
		public RESULT init(int maxchannels, INITFLAGS flags, IntPtr extradriverdata)
		{
			return System.FMOD5_System_Init(this.handle, maxchannels, flags, extradriverdata);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005324 File Offset: 0x00003724
		public RESULT close()
		{
			return System.FMOD5_System_Close(this.handle);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005331 File Offset: 0x00003731
		public RESULT update()
		{
			return System.FMOD5_System_Update(this.handle);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000533E File Offset: 0x0000373E
		public RESULT setSpeakerPosition(SPEAKER speaker, float x, float y, bool active)
		{
			return System.FMOD5_System_SetSpeakerPosition(this.handle, speaker, x, y, active);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005350 File Offset: 0x00003750
		public RESULT getSpeakerPosition(SPEAKER speaker, out float x, out float y, out bool active)
		{
			return System.FMOD5_System_GetSpeakerPosition(this.handle, speaker, out x, out y, out active);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005362 File Offset: 0x00003762
		public RESULT setStreamBufferSize(uint filebuffersize, TIMEUNIT filebuffersizetype)
		{
			return System.FMOD5_System_SetStreamBufferSize(this.handle, filebuffersize, filebuffersizetype);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005371 File Offset: 0x00003771
		public RESULT getStreamBufferSize(out uint filebuffersize, out TIMEUNIT filebuffersizetype)
		{
			return System.FMOD5_System_GetStreamBufferSize(this.handle, out filebuffersize, out filebuffersizetype);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005380 File Offset: 0x00003780
		public RESULT set3DSettings(float dopplerscale, float distancefactor, float rolloffscale)
		{
			return System.FMOD5_System_Set3DSettings(this.handle, dopplerscale, distancefactor, rolloffscale);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005390 File Offset: 0x00003790
		public RESULT get3DSettings(out float dopplerscale, out float distancefactor, out float rolloffscale)
		{
			return System.FMOD5_System_Get3DSettings(this.handle, out dopplerscale, out distancefactor, out rolloffscale);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000053A0 File Offset: 0x000037A0
		public RESULT set3DNumListeners(int numlisteners)
		{
			return System.FMOD5_System_Set3DNumListeners(this.handle, numlisteners);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000053AE File Offset: 0x000037AE
		public RESULT get3DNumListeners(out int numlisteners)
		{
			return System.FMOD5_System_Get3DNumListeners(this.handle, out numlisteners);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000053BC File Offset: 0x000037BC
		public RESULT set3DListenerAttributes(int listener, ref VECTOR pos, ref VECTOR vel, ref VECTOR forward, ref VECTOR up)
		{
			return System.FMOD5_System_Set3DListenerAttributes(this.handle, listener, ref pos, ref vel, ref forward, ref up);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000053D0 File Offset: 0x000037D0
		public RESULT get3DListenerAttributes(int listener, out VECTOR pos, out VECTOR vel, out VECTOR forward, out VECTOR up)
		{
			return System.FMOD5_System_Get3DListenerAttributes(this.handle, listener, out pos, out vel, out forward, out up);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000053E4 File Offset: 0x000037E4
		public RESULT set3DRolloffCallback(CB_3D_ROLLOFFCALLBACK callback)
		{
			return System.FMOD5_System_Set3DRolloffCallback(this.handle, callback);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000053F2 File Offset: 0x000037F2
		public RESULT mixerSuspend()
		{
			return System.FMOD5_System_MixerSuspend(this.handle);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000053FF File Offset: 0x000037FF
		public RESULT mixerResume()
		{
			return System.FMOD5_System_MixerResume(this.handle);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000540C File Offset: 0x0000380C
		public RESULT getDefaultMixMatrix(SPEAKERMODE sourcespeakermode, SPEAKERMODE targetspeakermode, float[] matrix, int matrixhop)
		{
			return System.FMOD5_System_GetDefaultMixMatrix(this.handle, sourcespeakermode, targetspeakermode, matrix, matrixhop);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000541E File Offset: 0x0000381E
		public RESULT getSpeakerModeChannels(SPEAKERMODE mode, out int channels)
		{
			return System.FMOD5_System_GetSpeakerModeChannels(this.handle, mode, out channels);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000542D File Offset: 0x0000382D
		public RESULT getVersion(out uint version)
		{
			return System.FMOD5_System_GetVersion(this.handle, out version);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000543B File Offset: 0x0000383B
		public RESULT getOutputHandle(out IntPtr handle)
		{
			return System.FMOD5_System_GetOutputHandle(this.handle, out handle);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005449 File Offset: 0x00003849
		public RESULT getChannelsPlaying(out int channels, out int realchannels)
		{
			return System.FMOD5_System_GetChannelsPlaying(this.handle, out channels, out realchannels);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005458 File Offset: 0x00003858
		public RESULT getCPUUsage(out float dsp, out float stream, out float geometry, out float update, out float total)
		{
			return System.FMOD5_System_GetCPUUsage(this.handle, out dsp, out stream, out geometry, out update, out total);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000546C File Offset: 0x0000386C
		public RESULT getFileUsage(out long sampleBytesRead, out long streamBytesRead, out long otherBytesRead)
		{
			return System.FMOD5_System_GetFileUsage(this.handle, out sampleBytesRead, out streamBytesRead, out otherBytesRead);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000547C File Offset: 0x0000387C
		public RESULT getSoundRAM(out int currentalloced, out int maxalloced, out int total)
		{
			return System.FMOD5_System_GetSoundRAM(this.handle, out currentalloced, out maxalloced, out total);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000548C File Offset: 0x0000388C
		public RESULT createSound(string name, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD5_System_CreateSound(this.handle, freeHelper.byteFromStringUTF8(name), mode, ref exinfo, out sound.handle);
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000054E0 File Offset: 0x000038E0
		public RESULT createSound(byte[] data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			return System.FMOD5_System_CreateSound(this.handle, data, mode, ref exinfo, out sound.handle);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000054F7 File Offset: 0x000038F7
		public RESULT createSound(IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			return System.FMOD5_System_CreateSound(this.handle, name_or_data, mode, ref exinfo, out sound.handle);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005510 File Offset: 0x00003910
		public RESULT createSound(string name, MODE mode, out Sound sound)
		{
			CREATESOUNDEXINFO createsoundexinfo = default(CREATESOUNDEXINFO);
			createsoundexinfo.cbsize = Marshal.SizeOf(createsoundexinfo);
			return this.createSound(name, mode, ref createsoundexinfo, out sound);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005544 File Offset: 0x00003944
		public RESULT createStream(string name, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD5_System_CreateStream(this.handle, freeHelper.byteFromStringUTF8(name), mode, ref exinfo, out sound.handle);
			}
			return result;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005598 File Offset: 0x00003998
		public RESULT createStream(byte[] data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			return System.FMOD5_System_CreateStream(this.handle, data, mode, ref exinfo, out sound.handle);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000055AF File Offset: 0x000039AF
		public RESULT createStream(IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			return System.FMOD5_System_CreateStream(this.handle, name_or_data, mode, ref exinfo, out sound.handle);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000055C8 File Offset: 0x000039C8
		public RESULT createStream(string name, MODE mode, out Sound sound)
		{
			CREATESOUNDEXINFO createsoundexinfo = default(CREATESOUNDEXINFO);
			createsoundexinfo.cbsize = Marshal.SizeOf(createsoundexinfo);
			return this.createStream(name, mode, ref createsoundexinfo, out sound);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000055FA File Offset: 0x000039FA
		public RESULT createDSP(ref DSP_DESCRIPTION description, out DSP dsp)
		{
			return System.FMOD5_System_CreateDSP(this.handle, ref description, out dsp.handle);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000560E File Offset: 0x00003A0E
		public RESULT createDSPByType(DSP_TYPE type, out DSP dsp)
		{
			return System.FMOD5_System_CreateDSPByType(this.handle, type, out dsp.handle);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005624 File Offset: 0x00003A24
		public RESULT createChannelGroup(string name, out ChannelGroup channelgroup)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD5_System_CreateChannelGroup(this.handle, freeHelper.byteFromStringUTF8(name), out channelgroup.handle);
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005674 File Offset: 0x00003A74
		public RESULT createSoundGroup(string name, out SoundGroup soundgroup)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD5_System_CreateSoundGroup(this.handle, freeHelper.byteFromStringUTF8(name), out soundgroup.handle);
			}
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000056C4 File Offset: 0x00003AC4
		public RESULT createReverb3D(out Reverb3D reverb)
		{
			return System.FMOD5_System_CreateReverb3D(this.handle, out reverb.handle);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000056D7 File Offset: 0x00003AD7
		public RESULT playSound(Sound sound, ChannelGroup channelGroup, bool paused, out Channel channel)
		{
			return System.FMOD5_System_PlaySound(this.handle, sound.handle, channelGroup.handle, paused, out channel.handle);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000056FA File Offset: 0x00003AFA
		public RESULT playDSP(DSP dsp, ChannelGroup channelGroup, bool paused, out Channel channel)
		{
			return System.FMOD5_System_PlayDSP(this.handle, dsp.handle, channelGroup.handle, paused, out channel.handle);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000571D File Offset: 0x00003B1D
		public RESULT getChannel(int channelid, out Channel channel)
		{
			return System.FMOD5_System_GetChannel(this.handle, channelid, out channel.handle);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005731 File Offset: 0x00003B31
		public RESULT getMasterChannelGroup(out ChannelGroup channelgroup)
		{
			return System.FMOD5_System_GetMasterChannelGroup(this.handle, out channelgroup.handle);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005744 File Offset: 0x00003B44
		public RESULT getMasterSoundGroup(out SoundGroup soundgroup)
		{
			return System.FMOD5_System_GetMasterSoundGroup(this.handle, out soundgroup.handle);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005757 File Offset: 0x00003B57
		public RESULT attachChannelGroupToPort(uint portType, ulong portIndex, ChannelGroup channelgroup, bool passThru = false)
		{
			return System.FMOD5_System_AttachChannelGroupToPort(this.handle, portType, portIndex, channelgroup.handle, passThru);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000576F File Offset: 0x00003B6F
		public RESULT detachChannelGroupFromPort(ChannelGroup channelgroup)
		{
			return System.FMOD5_System_DetachChannelGroupFromPort(this.handle, channelgroup.handle);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005783 File Offset: 0x00003B83
		public RESULT setReverbProperties(int instance, ref REVERB_PROPERTIES prop)
		{
			return System.FMOD5_System_SetReverbProperties(this.handle, instance, ref prop);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005792 File Offset: 0x00003B92
		public RESULT getReverbProperties(int instance, out REVERB_PROPERTIES prop)
		{
			return System.FMOD5_System_GetReverbProperties(this.handle, instance, out prop);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000057A1 File Offset: 0x00003BA1
		public RESULT lockDSP()
		{
			return System.FMOD5_System_LockDSP(this.handle);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000057AE File Offset: 0x00003BAE
		public RESULT unlockDSP()
		{
			return System.FMOD5_System_UnlockDSP(this.handle);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000057BB File Offset: 0x00003BBB
		public RESULT getRecordNumDrivers(out int numdrivers, out int numconnected)
		{
			return System.FMOD5_System_GetRecordNumDrivers(this.handle, out numdrivers, out numconnected);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000057CC File Offset: 0x00003BCC
		public RESULT getRecordDriverInfo(int id, out string name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels, out DRIVER_STATE state)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(namelen);
			RESULT result = System.FMOD5_System_GetRecordDriverInfo(this.handle, id, intPtr, namelen, out guid, out systemrate, out speakermode, out speakermodechannels, out state);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				name = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005834 File Offset: 0x00003C34
		public RESULT getRecordDriverInfo(int id, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels, out DRIVER_STATE state)
		{
			return System.FMOD5_System_GetRecordDriverInfo(this.handle, id, IntPtr.Zero, 0, out guid, out systemrate, out speakermode, out speakermodechannels, out state);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000585B File Offset: 0x00003C5B
		public RESULT getRecordPosition(int id, out uint position)
		{
			return System.FMOD5_System_GetRecordPosition(this.handle, id, out position);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000586A File Offset: 0x00003C6A
		public RESULT recordStart(int id, Sound sound, bool loop)
		{
			return System.FMOD5_System_RecordStart(this.handle, id, sound.handle, loop);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005880 File Offset: 0x00003C80
		public RESULT recordStop(int id)
		{
			return System.FMOD5_System_RecordStop(this.handle, id);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000588E File Offset: 0x00003C8E
		public RESULT isRecording(int id, out bool recording)
		{
			return System.FMOD5_System_IsRecording(this.handle, id, out recording);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000589D File Offset: 0x00003C9D
		public RESULT createGeometry(int maxpolygons, int maxvertices, out Geometry geometry)
		{
			return System.FMOD5_System_CreateGeometry(this.handle, maxpolygons, maxvertices, out geometry.handle);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000058B2 File Offset: 0x00003CB2
		public RESULT setGeometrySettings(float maxworldsize)
		{
			return System.FMOD5_System_SetGeometrySettings(this.handle, maxworldsize);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000058C0 File Offset: 0x00003CC0
		public RESULT getGeometrySettings(out float maxworldsize)
		{
			return System.FMOD5_System_GetGeometrySettings(this.handle, out maxworldsize);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000058CE File Offset: 0x00003CCE
		public RESULT loadGeometry(IntPtr data, int datasize, out Geometry geometry)
		{
			return System.FMOD5_System_LoadGeometry(this.handle, data, datasize, out geometry.handle);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000058E3 File Offset: 0x00003CE3
		public RESULT getGeometryOcclusion(ref VECTOR listener, ref VECTOR source, out float direct, out float reverb)
		{
			return System.FMOD5_System_GetGeometryOcclusion(this.handle, ref listener, ref source, out direct, out reverb);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000058F8 File Offset: 0x00003CF8
		public RESULT setNetworkProxy(string proxy)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD5_System_SetNetworkProxy(this.handle, freeHelper.byteFromStringUTF8(proxy));
			}
			return result;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005944 File Offset: 0x00003D44
		public RESULT getNetworkProxy(out string proxy, int proxylen)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(proxylen);
			RESULT result = System.FMOD5_System_GetNetworkProxy(this.handle, intPtr, proxylen);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				proxy = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000059A0 File Offset: 0x00003DA0
		public RESULT setNetworkTimeout(int timeout)
		{
			return System.FMOD5_System_SetNetworkTimeout(this.handle, timeout);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000059AE File Offset: 0x00003DAE
		public RESULT getNetworkTimeout(out int timeout)
		{
			return System.FMOD5_System_GetNetworkTimeout(this.handle, out timeout);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000059BC File Offset: 0x00003DBC
		public RESULT setUserData(IntPtr userdata)
		{
			return System.FMOD5_System_SetUserData(this.handle, userdata);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000059CA File Offset: 0x00003DCA
		public RESULT getUserData(out IntPtr userdata)
		{
			return System.FMOD5_System_GetUserData(this.handle, out userdata);
		}

		// Token: 0x0600016C RID: 364
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Release(IntPtr system);

		// Token: 0x0600016D RID: 365
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetOutput(IntPtr system, OUTPUTTYPE output);

		// Token: 0x0600016E RID: 366
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetOutput(IntPtr system, out OUTPUTTYPE output);

		// Token: 0x0600016F RID: 367
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetNumDrivers(IntPtr system, out int numdrivers);

		// Token: 0x06000170 RID: 368
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetDriverInfo(IntPtr system, int id, IntPtr name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels);

		// Token: 0x06000171 RID: 369
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetDriver(IntPtr system, int driver);

		// Token: 0x06000172 RID: 370
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetDriver(IntPtr system, out int driver);

		// Token: 0x06000173 RID: 371
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetSoftwareChannels(IntPtr system, int numsoftwarechannels);

		// Token: 0x06000174 RID: 372
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetSoftwareChannels(IntPtr system, out int numsoftwarechannels);

		// Token: 0x06000175 RID: 373
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetSoftwareFormat(IntPtr system, int samplerate, SPEAKERMODE speakermode, int numrawspeakers);

		// Token: 0x06000176 RID: 374
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetSoftwareFormat(IntPtr system, out int samplerate, out SPEAKERMODE speakermode, out int numrawspeakers);

		// Token: 0x06000177 RID: 375
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetDSPBufferSize(IntPtr system, uint bufferlength, int numbuffers);

		// Token: 0x06000178 RID: 376
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetDSPBufferSize(IntPtr system, out uint bufferlength, out int numbuffers);

		// Token: 0x06000179 RID: 377
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetFileSystem(IntPtr system, FILE_OPENCALLBACK useropen, FILE_CLOSECALLBACK userclose, FILE_READCALLBACK userread, FILE_SEEKCALLBACK userseek, FILE_ASYNCREADCALLBACK userasyncread, FILE_ASYNCCANCELCALLBACK userasynccancel, int blockalign);

		// Token: 0x0600017A RID: 378
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_AttachFileSystem(IntPtr system, FILE_OPENCALLBACK useropen, FILE_CLOSECALLBACK userclose, FILE_READCALLBACK userread, FILE_SEEKCALLBACK userseek);

		// Token: 0x0600017B RID: 379
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetAdvancedSettings(IntPtr system, ref ADVANCEDSETTINGS settings);

		// Token: 0x0600017C RID: 380
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetAdvancedSettings(IntPtr system, ref ADVANCEDSETTINGS settings);

		// Token: 0x0600017D RID: 381
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetCallback(IntPtr system, SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask);

		// Token: 0x0600017E RID: 382
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetPluginPath(IntPtr system, byte[] path);

		// Token: 0x0600017F RID: 383
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_LoadPlugin(IntPtr system, byte[] filename, out uint handle, uint priority);

		// Token: 0x06000180 RID: 384
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_UnloadPlugin(IntPtr system, uint handle);

		// Token: 0x06000181 RID: 385
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetNumNestedPlugins(IntPtr system, uint handle, out int count);

		// Token: 0x06000182 RID: 386
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetNestedPlugin(IntPtr system, uint handle, int index, out uint nestedhandle);

		// Token: 0x06000183 RID: 387
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetNumPlugins(IntPtr system, PLUGINTYPE plugintype, out int numplugins);

		// Token: 0x06000184 RID: 388
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetPluginHandle(IntPtr system, PLUGINTYPE plugintype, int index, out uint handle);

		// Token: 0x06000185 RID: 389
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetPluginInfo(IntPtr system, uint handle, out PLUGINTYPE plugintype, IntPtr name, int namelen, out uint version);

		// Token: 0x06000186 RID: 390
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetOutputByPlugin(IntPtr system, uint handle);

		// Token: 0x06000187 RID: 391
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetOutputByPlugin(IntPtr system, out uint handle);

		// Token: 0x06000188 RID: 392
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateDSPByPlugin(IntPtr system, uint handle, out IntPtr dsp);

		// Token: 0x06000189 RID: 393
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetDSPInfoByPlugin(IntPtr system, uint handle, out IntPtr description);

		// Token: 0x0600018A RID: 394
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_RegisterDSP(IntPtr system, ref DSP_DESCRIPTION description, out uint handle);

		// Token: 0x0600018B RID: 395
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Init(IntPtr system, int maxchannels, INITFLAGS flags, IntPtr extradriverdata);

		// Token: 0x0600018C RID: 396
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Close(IntPtr system);

		// Token: 0x0600018D RID: 397
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Update(IntPtr system);

		// Token: 0x0600018E RID: 398
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetSpeakerPosition(IntPtr system, SPEAKER speaker, float x, float y, bool active);

		// Token: 0x0600018F RID: 399
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetSpeakerPosition(IntPtr system, SPEAKER speaker, out float x, out float y, out bool active);

		// Token: 0x06000190 RID: 400
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetStreamBufferSize(IntPtr system, uint filebuffersize, TIMEUNIT filebuffersizetype);

		// Token: 0x06000191 RID: 401
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetStreamBufferSize(IntPtr system, out uint filebuffersize, out TIMEUNIT filebuffersizetype);

		// Token: 0x06000192 RID: 402
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Set3DSettings(IntPtr system, float dopplerscale, float distancefactor, float rolloffscale);

		// Token: 0x06000193 RID: 403
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Get3DSettings(IntPtr system, out float dopplerscale, out float distancefactor, out float rolloffscale);

		// Token: 0x06000194 RID: 404
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Set3DNumListeners(IntPtr system, int numlisteners);

		// Token: 0x06000195 RID: 405
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Get3DNumListeners(IntPtr system, out int numlisteners);

		// Token: 0x06000196 RID: 406
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Set3DListenerAttributes(IntPtr system, int listener, ref VECTOR pos, ref VECTOR vel, ref VECTOR forward, ref VECTOR up);

		// Token: 0x06000197 RID: 407
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Get3DListenerAttributes(IntPtr system, int listener, out VECTOR pos, out VECTOR vel, out VECTOR forward, out VECTOR up);

		// Token: 0x06000198 RID: 408
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Set3DRolloffCallback(IntPtr system, CB_3D_ROLLOFFCALLBACK callback);

		// Token: 0x06000199 RID: 409
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_MixerSuspend(IntPtr system);

		// Token: 0x0600019A RID: 410
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_MixerResume(IntPtr system);

		// Token: 0x0600019B RID: 411
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetDefaultMixMatrix(IntPtr system, SPEAKERMODE sourcespeakermode, SPEAKERMODE targetspeakermode, float[] matrix, int matrixhop);

		// Token: 0x0600019C RID: 412
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetSpeakerModeChannels(IntPtr system, SPEAKERMODE mode, out int channels);

		// Token: 0x0600019D RID: 413
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetVersion(IntPtr system, out uint version);

		// Token: 0x0600019E RID: 414
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetOutputHandle(IntPtr system, out IntPtr handle);

		// Token: 0x0600019F RID: 415
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetChannelsPlaying(IntPtr system, out int channels, out int realchannels);

		// Token: 0x060001A0 RID: 416
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetCPUUsage(IntPtr system, out float dsp, out float stream, out float geometry, out float update, out float total);

		// Token: 0x060001A1 RID: 417
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetFileUsage(IntPtr system, out long sampleBytesRead, out long streamBytesRead, out long otherBytesRead);

		// Token: 0x060001A2 RID: 418
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetSoundRAM(IntPtr system, out int currentalloced, out int maxalloced, out int total);

		// Token: 0x060001A3 RID: 419
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateSound(IntPtr system, byte[] name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);

		// Token: 0x060001A4 RID: 420
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateSound(IntPtr system, IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);

		// Token: 0x060001A5 RID: 421
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateStream(IntPtr system, byte[] name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);

		// Token: 0x060001A6 RID: 422
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateStream(IntPtr system, IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);

		// Token: 0x060001A7 RID: 423
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateDSP(IntPtr system, ref DSP_DESCRIPTION description, out IntPtr dsp);

		// Token: 0x060001A8 RID: 424
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateDSPByType(IntPtr system, DSP_TYPE type, out IntPtr dsp);

		// Token: 0x060001A9 RID: 425
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateChannelGroup(IntPtr system, byte[] name, out IntPtr channelgroup);

		// Token: 0x060001AA RID: 426
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateSoundGroup(IntPtr system, byte[] name, out IntPtr soundgroup);

		// Token: 0x060001AB RID: 427
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateReverb3D(IntPtr system, out IntPtr reverb);

		// Token: 0x060001AC RID: 428
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_PlaySound(IntPtr system, IntPtr sound, IntPtr channelGroup, bool paused, out IntPtr channel);

		// Token: 0x060001AD RID: 429
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_PlayDSP(IntPtr system, IntPtr dsp, IntPtr channelGroup, bool paused, out IntPtr channel);

		// Token: 0x060001AE RID: 430
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetChannel(IntPtr system, int channelid, out IntPtr channel);

		// Token: 0x060001AF RID: 431
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetMasterChannelGroup(IntPtr system, out IntPtr channelgroup);

		// Token: 0x060001B0 RID: 432
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetMasterSoundGroup(IntPtr system, out IntPtr soundgroup);

		// Token: 0x060001B1 RID: 433
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_AttachChannelGroupToPort(IntPtr system, uint portType, ulong portIndex, IntPtr channelgroup, bool passThru);

		// Token: 0x060001B2 RID: 434
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_DetachChannelGroupFromPort(IntPtr system, IntPtr channelgroup);

		// Token: 0x060001B3 RID: 435
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetReverbProperties(IntPtr system, int instance, ref REVERB_PROPERTIES prop);

		// Token: 0x060001B4 RID: 436
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetReverbProperties(IntPtr system, int instance, out REVERB_PROPERTIES prop);

		// Token: 0x060001B5 RID: 437
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_LockDSP(IntPtr system);

		// Token: 0x060001B6 RID: 438
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_UnlockDSP(IntPtr system);

		// Token: 0x060001B7 RID: 439
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetRecordNumDrivers(IntPtr system, out int numdrivers, out int numconnected);

		// Token: 0x060001B8 RID: 440
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetRecordDriverInfo(IntPtr system, int id, IntPtr name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels, out DRIVER_STATE state);

		// Token: 0x060001B9 RID: 441
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetRecordPosition(IntPtr system, int id, out uint position);

		// Token: 0x060001BA RID: 442
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_RecordStart(IntPtr system, int id, IntPtr sound, bool loop);

		// Token: 0x060001BB RID: 443
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_RecordStop(IntPtr system, int id);

		// Token: 0x060001BC RID: 444
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_IsRecording(IntPtr system, int id, out bool recording);

		// Token: 0x060001BD RID: 445
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_CreateGeometry(IntPtr system, int maxpolygons, int maxvertices, out IntPtr geometry);

		// Token: 0x060001BE RID: 446
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetGeometrySettings(IntPtr system, float maxworldsize);

		// Token: 0x060001BF RID: 447
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetGeometrySettings(IntPtr system, out float maxworldsize);

		// Token: 0x060001C0 RID: 448
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_LoadGeometry(IntPtr system, IntPtr data, int datasize, out IntPtr geometry);

		// Token: 0x060001C1 RID: 449
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetGeometryOcclusion(IntPtr system, ref VECTOR listener, ref VECTOR source, out float direct, out float reverb);

		// Token: 0x060001C2 RID: 450
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetNetworkProxy(IntPtr system, byte[] proxy);

		// Token: 0x060001C3 RID: 451
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetNetworkProxy(IntPtr system, IntPtr proxy, int proxylen);

		// Token: 0x060001C4 RID: 452
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetNetworkTimeout(IntPtr system, int timeout);

		// Token: 0x060001C5 RID: 453
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetNetworkTimeout(IntPtr system, out int timeout);

		// Token: 0x060001C6 RID: 454
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_SetUserData(IntPtr system, IntPtr userdata);

		// Token: 0x060001C7 RID: 455
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_GetUserData(IntPtr system, out IntPtr userdata);

		// Token: 0x060001C8 RID: 456 RVA: 0x000059D8 File Offset: 0x00003DD8
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000059EA File Offset: 0x00003DEA
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x04000284 RID: 644
		public IntPtr handle;
	}
}
