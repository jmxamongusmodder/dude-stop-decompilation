using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x02000062 RID: 98
	public struct Channel : IChannelControl
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00005DF1 File Offset: 0x000041F1
		public RESULT setFrequency(float frequency)
		{
			return Channel.FMOD5_Channel_SetFrequency(this.handle, frequency);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00005DFF File Offset: 0x000041FF
		public RESULT getFrequency(out float frequency)
		{
			return Channel.FMOD5_Channel_GetFrequency(this.handle, out frequency);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00005E0D File Offset: 0x0000420D
		public RESULT setPriority(int priority)
		{
			return Channel.FMOD5_Channel_SetPriority(this.handle, priority);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00005E1B File Offset: 0x0000421B
		public RESULT getPriority(out int priority)
		{
			return Channel.FMOD5_Channel_GetPriority(this.handle, out priority);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00005E29 File Offset: 0x00004229
		public RESULT setPosition(uint position, TIMEUNIT postype)
		{
			return Channel.FMOD5_Channel_SetPosition(this.handle, position, postype);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00005E38 File Offset: 0x00004238
		public RESULT getPosition(out uint position, TIMEUNIT postype)
		{
			return Channel.FMOD5_Channel_GetPosition(this.handle, out position, postype);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00005E47 File Offset: 0x00004247
		public RESULT setChannelGroup(ChannelGroup channelgroup)
		{
			return Channel.FMOD5_Channel_SetChannelGroup(this.handle, channelgroup.handle);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00005E5B File Offset: 0x0000425B
		public RESULT getChannelGroup(out ChannelGroup channelgroup)
		{
			return Channel.FMOD5_Channel_GetChannelGroup(this.handle, out channelgroup.handle);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00005E6E File Offset: 0x0000426E
		public RESULT setLoopCount(int loopcount)
		{
			return Channel.FMOD5_Channel_SetLoopCount(this.handle, loopcount);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00005E7C File Offset: 0x0000427C
		public RESULT getLoopCount(out int loopcount)
		{
			return Channel.FMOD5_Channel_GetLoopCount(this.handle, out loopcount);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00005E8A File Offset: 0x0000428A
		public RESULT setLoopPoints(uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype)
		{
			return Channel.FMOD5_Channel_SetLoopPoints(this.handle, loopstart, loopstarttype, loopend, loopendtype);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00005E9C File Offset: 0x0000429C
		public RESULT getLoopPoints(out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype)
		{
			return Channel.FMOD5_Channel_GetLoopPoints(this.handle, out loopstart, loopstarttype, out loopend, loopendtype);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00005EAE File Offset: 0x000042AE
		public RESULT isVirtual(out bool isvirtual)
		{
			return Channel.FMOD5_Channel_IsVirtual(this.handle, out isvirtual);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00005EBC File Offset: 0x000042BC
		public RESULT getCurrentSound(out Sound sound)
		{
			return Channel.FMOD5_Channel_GetCurrentSound(this.handle, out sound.handle);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00005ECF File Offset: 0x000042CF
		public RESULT getIndex(out int index)
		{
			return Channel.FMOD5_Channel_GetIndex(this.handle, out index);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00005EDD File Offset: 0x000042DD
		public RESULT getSystemObject(out System system)
		{
			return Channel.FMOD5_Channel_GetSystemObject(this.handle, out system.handle);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00005EF0 File Offset: 0x000042F0
		public RESULT stop()
		{
			return Channel.FMOD5_Channel_Stop(this.handle);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00005EFD File Offset: 0x000042FD
		public RESULT setPaused(bool paused)
		{
			return Channel.FMOD5_Channel_SetPaused(this.handle, paused);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00005F0B File Offset: 0x0000430B
		public RESULT getPaused(out bool paused)
		{
			return Channel.FMOD5_Channel_GetPaused(this.handle, out paused);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00005F19 File Offset: 0x00004319
		public RESULT setVolume(float volume)
		{
			return Channel.FMOD5_Channel_SetVolume(this.handle, volume);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00005F27 File Offset: 0x00004327
		public RESULT getVolume(out float volume)
		{
			return Channel.FMOD5_Channel_GetVolume(this.handle, out volume);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00005F35 File Offset: 0x00004335
		public RESULT setVolumeRamp(bool ramp)
		{
			return Channel.FMOD5_Channel_SetVolumeRamp(this.handle, ramp);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00005F43 File Offset: 0x00004343
		public RESULT getVolumeRamp(out bool ramp)
		{
			return Channel.FMOD5_Channel_GetVolumeRamp(this.handle, out ramp);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00005F51 File Offset: 0x00004351
		public RESULT getAudibility(out float audibility)
		{
			return Channel.FMOD5_Channel_GetAudibility(this.handle, out audibility);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00005F5F File Offset: 0x0000435F
		public RESULT setPitch(float pitch)
		{
			return Channel.FMOD5_Channel_SetPitch(this.handle, pitch);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00005F6D File Offset: 0x0000436D
		public RESULT getPitch(out float pitch)
		{
			return Channel.FMOD5_Channel_GetPitch(this.handle, out pitch);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00005F7B File Offset: 0x0000437B
		public RESULT setMute(bool mute)
		{
			return Channel.FMOD5_Channel_SetMute(this.handle, mute);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00005F89 File Offset: 0x00004389
		public RESULT getMute(out bool mute)
		{
			return Channel.FMOD5_Channel_GetMute(this.handle, out mute);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00005F97 File Offset: 0x00004397
		public RESULT setReverbProperties(int instance, float wet)
		{
			return Channel.FMOD5_Channel_SetReverbProperties(this.handle, instance, wet);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00005FA6 File Offset: 0x000043A6
		public RESULT getReverbProperties(int instance, out float wet)
		{
			return Channel.FMOD5_Channel_GetReverbProperties(this.handle, instance, out wet);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00005FB5 File Offset: 0x000043B5
		public RESULT setLowPassGain(float gain)
		{
			return Channel.FMOD5_Channel_SetLowPassGain(this.handle, gain);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00005FC3 File Offset: 0x000043C3
		public RESULT getLowPassGain(out float gain)
		{
			return Channel.FMOD5_Channel_GetLowPassGain(this.handle, out gain);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00005FD1 File Offset: 0x000043D1
		public RESULT setMode(MODE mode)
		{
			return Channel.FMOD5_Channel_SetMode(this.handle, mode);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00005FDF File Offset: 0x000043DF
		public RESULT getMode(out MODE mode)
		{
			return Channel.FMOD5_Channel_GetMode(this.handle, out mode);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00005FED File Offset: 0x000043ED
		public RESULT setCallback(CHANNEL_CALLBACK callback)
		{
			return Channel.FMOD5_Channel_SetCallback(this.handle, callback);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00005FFB File Offset: 0x000043FB
		public RESULT isPlaying(out bool isplaying)
		{
			return Channel.FMOD5_Channel_IsPlaying(this.handle, out isplaying);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00006009 File Offset: 0x00004409
		public RESULT setPan(float pan)
		{
			return Channel.FMOD5_Channel_SetPan(this.handle, pan);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00006018 File Offset: 0x00004418
		public RESULT setMixLevelsOutput(float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright)
		{
			return Channel.FMOD5_Channel_SetMixLevelsOutput(this.handle, frontleft, frontright, center, lfe, surroundleft, surroundright, backleft, backright);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000603D File Offset: 0x0000443D
		public RESULT setMixLevelsInput(float[] levels, int numlevels)
		{
			return Channel.FMOD5_Channel_SetMixLevelsInput(this.handle, levels, numlevels);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000604C File Offset: 0x0000444C
		public RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop)
		{
			return Channel.FMOD5_Channel_SetMixMatrix(this.handle, matrix, outchannels, inchannels, inchannel_hop);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000605E File Offset: 0x0000445E
		public RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop)
		{
			return Channel.FMOD5_Channel_GetMixMatrix(this.handle, matrix, out outchannels, out inchannels, inchannel_hop);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00006070 File Offset: 0x00004470
		public RESULT getDSPClock(out ulong dspclock, out ulong parentclock)
		{
			return Channel.FMOD5_Channel_GetDSPClock(this.handle, out dspclock, out parentclock);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000607F File Offset: 0x0000447F
		public RESULT setDelay(ulong dspclock_start, ulong dspclock_end, bool stopchannels)
		{
			return Channel.FMOD5_Channel_SetDelay(this.handle, dspclock_start, dspclock_end, stopchannels);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000608F File Offset: 0x0000448F
		public RESULT getDelay(out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels)
		{
			return Channel.FMOD5_Channel_GetDelay(this.handle, out dspclock_start, out dspclock_end, out stopchannels);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000609F File Offset: 0x0000449F
		public RESULT addFadePoint(ulong dspclock, float volume)
		{
			return Channel.FMOD5_Channel_AddFadePoint(this.handle, dspclock, volume);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000060AE File Offset: 0x000044AE
		public RESULT setFadePointRamp(ulong dspclock, float volume)
		{
			return Channel.FMOD5_Channel_SetFadePointRamp(this.handle, dspclock, volume);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000060BD File Offset: 0x000044BD
		public RESULT removeFadePoints(ulong dspclock_start, ulong dspclock_end)
		{
			return Channel.FMOD5_Channel_RemoveFadePoints(this.handle, dspclock_start, dspclock_end);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000060CC File Offset: 0x000044CC
		public RESULT getFadePoints(ref uint numpoints, ulong[] point_dspclock, float[] point_volume)
		{
			return Channel.FMOD5_Channel_GetFadePoints(this.handle, ref numpoints, point_dspclock, point_volume);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000060DC File Offset: 0x000044DC
		public RESULT getDSP(int index, out DSP dsp)
		{
			return Channel.FMOD5_Channel_GetDSP(this.handle, index, out dsp.handle);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000060F0 File Offset: 0x000044F0
		public RESULT addDSP(int index, DSP dsp)
		{
			return Channel.FMOD5_Channel_AddDSP(this.handle, index, dsp.handle);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006105 File Offset: 0x00004505
		public RESULT removeDSP(DSP dsp)
		{
			return Channel.FMOD5_Channel_RemoveDSP(this.handle, dsp.handle);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006119 File Offset: 0x00004519
		public RESULT getNumDSPs(out int numdsps)
		{
			return Channel.FMOD5_Channel_GetNumDSPs(this.handle, out numdsps);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00006127 File Offset: 0x00004527
		public RESULT setDSPIndex(DSP dsp, int index)
		{
			return Channel.FMOD5_Channel_SetDSPIndex(this.handle, dsp.handle, index);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000613C File Offset: 0x0000453C
		public RESULT getDSPIndex(DSP dsp, out int index)
		{
			return Channel.FMOD5_Channel_GetDSPIndex(this.handle, dsp.handle, out index);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00006151 File Offset: 0x00004551
		public RESULT set3DAttributes(ref VECTOR pos, ref VECTOR vel, ref VECTOR alt_pan_pos)
		{
			return Channel.FMOD5_Channel_Set3DAttributes(this.handle, ref pos, ref vel, ref alt_pan_pos);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00006161 File Offset: 0x00004561
		public RESULT get3DAttributes(out VECTOR pos, out VECTOR vel, out VECTOR alt_pan_pos)
		{
			return Channel.FMOD5_Channel_Get3DAttributes(this.handle, out pos, out vel, out alt_pan_pos);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00006171 File Offset: 0x00004571
		public RESULT set3DMinMaxDistance(float mindistance, float maxdistance)
		{
			return Channel.FMOD5_Channel_Set3DMinMaxDistance(this.handle, mindistance, maxdistance);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00006180 File Offset: 0x00004580
		public RESULT get3DMinMaxDistance(out float mindistance, out float maxdistance)
		{
			return Channel.FMOD5_Channel_Get3DMinMaxDistance(this.handle, out mindistance, out maxdistance);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000618F File Offset: 0x0000458F
		public RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
		{
			return Channel.FMOD5_Channel_Set3DConeSettings(this.handle, insideconeangle, outsideconeangle, outsidevolume);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000619F File Offset: 0x0000459F
		public RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
		{
			return Channel.FMOD5_Channel_Get3DConeSettings(this.handle, out insideconeangle, out outsideconeangle, out outsidevolume);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000061AF File Offset: 0x000045AF
		public RESULT set3DConeOrientation(ref VECTOR orientation)
		{
			return Channel.FMOD5_Channel_Set3DConeOrientation(this.handle, ref orientation);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000061BD File Offset: 0x000045BD
		public RESULT get3DConeOrientation(out VECTOR orientation)
		{
			return Channel.FMOD5_Channel_Get3DConeOrientation(this.handle, out orientation);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000061CB File Offset: 0x000045CB
		public RESULT set3DCustomRolloff(ref VECTOR points, int numpoints)
		{
			return Channel.FMOD5_Channel_Set3DCustomRolloff(this.handle, ref points, numpoints);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000061DA File Offset: 0x000045DA
		public RESULT get3DCustomRolloff(out IntPtr points, out int numpoints)
		{
			return Channel.FMOD5_Channel_Get3DCustomRolloff(this.handle, out points, out numpoints);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000061E9 File Offset: 0x000045E9
		public RESULT set3DOcclusion(float directocclusion, float reverbocclusion)
		{
			return Channel.FMOD5_Channel_Set3DOcclusion(this.handle, directocclusion, reverbocclusion);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000061F8 File Offset: 0x000045F8
		public RESULT get3DOcclusion(out float directocclusion, out float reverbocclusion)
		{
			return Channel.FMOD5_Channel_Get3DOcclusion(this.handle, out directocclusion, out reverbocclusion);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00006207 File Offset: 0x00004607
		public RESULT set3DSpread(float angle)
		{
			return Channel.FMOD5_Channel_Set3DSpread(this.handle, angle);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00006215 File Offset: 0x00004615
		public RESULT get3DSpread(out float angle)
		{
			return Channel.FMOD5_Channel_Get3DSpread(this.handle, out angle);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00006223 File Offset: 0x00004623
		public RESULT set3DLevel(float level)
		{
			return Channel.FMOD5_Channel_Set3DLevel(this.handle, level);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00006231 File Offset: 0x00004631
		public RESULT get3DLevel(out float level)
		{
			return Channel.FMOD5_Channel_Get3DLevel(this.handle, out level);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000623F File Offset: 0x0000463F
		public RESULT set3DDopplerLevel(float level)
		{
			return Channel.FMOD5_Channel_Set3DDopplerLevel(this.handle, level);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000624D File Offset: 0x0000464D
		public RESULT get3DDopplerLevel(out float level)
		{
			return Channel.FMOD5_Channel_Get3DDopplerLevel(this.handle, out level);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000625B File Offset: 0x0000465B
		public RESULT set3DDistanceFilter(bool custom, float customLevel, float centerFreq)
		{
			return Channel.FMOD5_Channel_Set3DDistanceFilter(this.handle, custom, customLevel, centerFreq);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000626B File Offset: 0x0000466B
		public RESULT get3DDistanceFilter(out bool custom, out float customLevel, out float centerFreq)
		{
			return Channel.FMOD5_Channel_Get3DDistanceFilter(this.handle, out custom, out customLevel, out centerFreq);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000627B File Offset: 0x0000467B
		public RESULT setUserData(IntPtr userdata)
		{
			return Channel.FMOD5_Channel_SetUserData(this.handle, userdata);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00006289 File Offset: 0x00004689
		public RESULT getUserData(out IntPtr userdata)
		{
			return Channel.FMOD5_Channel_GetUserData(this.handle, out userdata);
		}

		// Token: 0x060002AC RID: 684
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetFrequency(IntPtr channel, float frequency);

		// Token: 0x060002AD RID: 685
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetFrequency(IntPtr channel, out float frequency);

		// Token: 0x060002AE RID: 686
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetPriority(IntPtr channel, int priority);

		// Token: 0x060002AF RID: 687
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetPriority(IntPtr channel, out int priority);

		// Token: 0x060002B0 RID: 688
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetPosition(IntPtr channel, uint position, TIMEUNIT postype);

		// Token: 0x060002B1 RID: 689
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetPosition(IntPtr channel, out uint position, TIMEUNIT postype);

		// Token: 0x060002B2 RID: 690
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetChannelGroup(IntPtr channel, IntPtr channelgroup);

		// Token: 0x060002B3 RID: 691
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetChannelGroup(IntPtr channel, out IntPtr channelgroup);

		// Token: 0x060002B4 RID: 692
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetLoopCount(IntPtr channel, int loopcount);

		// Token: 0x060002B5 RID: 693
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetLoopCount(IntPtr channel, out int loopcount);

		// Token: 0x060002B6 RID: 694
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetLoopPoints(IntPtr channel, uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype);

		// Token: 0x060002B7 RID: 695
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetLoopPoints(IntPtr channel, out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype);

		// Token: 0x060002B8 RID: 696
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_IsVirtual(IntPtr channel, out bool isvirtual);

		// Token: 0x060002B9 RID: 697
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetCurrentSound(IntPtr channel, out IntPtr sound);

		// Token: 0x060002BA RID: 698
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetIndex(IntPtr channel, out int index);

		// Token: 0x060002BB RID: 699
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetSystemObject(IntPtr channel, out IntPtr system);

		// Token: 0x060002BC RID: 700
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Stop(IntPtr channel);

		// Token: 0x060002BD RID: 701
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetPaused(IntPtr channel, bool paused);

		// Token: 0x060002BE RID: 702
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetPaused(IntPtr channel, out bool paused);

		// Token: 0x060002BF RID: 703
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetVolume(IntPtr channel, float volume);

		// Token: 0x060002C0 RID: 704
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetVolume(IntPtr channel, out float volume);

		// Token: 0x060002C1 RID: 705
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetVolumeRamp(IntPtr channel, bool ramp);

		// Token: 0x060002C2 RID: 706
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetVolumeRamp(IntPtr channel, out bool ramp);

		// Token: 0x060002C3 RID: 707
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetAudibility(IntPtr channel, out float audibility);

		// Token: 0x060002C4 RID: 708
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetPitch(IntPtr channel, float pitch);

		// Token: 0x060002C5 RID: 709
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetPitch(IntPtr channel, out float pitch);

		// Token: 0x060002C6 RID: 710
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetMute(IntPtr channel, bool mute);

		// Token: 0x060002C7 RID: 711
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetMute(IntPtr channel, out bool mute);

		// Token: 0x060002C8 RID: 712
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetReverbProperties(IntPtr channel, int instance, float wet);

		// Token: 0x060002C9 RID: 713
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetReverbProperties(IntPtr channel, int instance, out float wet);

		// Token: 0x060002CA RID: 714
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetLowPassGain(IntPtr channel, float gain);

		// Token: 0x060002CB RID: 715
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetLowPassGain(IntPtr channel, out float gain);

		// Token: 0x060002CC RID: 716
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetMode(IntPtr channel, MODE mode);

		// Token: 0x060002CD RID: 717
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetMode(IntPtr channel, out MODE mode);

		// Token: 0x060002CE RID: 718
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetCallback(IntPtr channel, CHANNEL_CALLBACK callback);

		// Token: 0x060002CF RID: 719
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_IsPlaying(IntPtr channel, out bool isplaying);

		// Token: 0x060002D0 RID: 720
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetPan(IntPtr channel, float pan);

		// Token: 0x060002D1 RID: 721
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetMixLevelsOutput(IntPtr channel, float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);

		// Token: 0x060002D2 RID: 722
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetMixLevelsInput(IntPtr channel, float[] levels, int numlevels);

		// Token: 0x060002D3 RID: 723
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetMixMatrix(IntPtr channel, float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		// Token: 0x060002D4 RID: 724
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetMixMatrix(IntPtr channel, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);

		// Token: 0x060002D5 RID: 725
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetDSPClock(IntPtr channel, out ulong dspclock, out ulong parentclock);

		// Token: 0x060002D6 RID: 726
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetDelay(IntPtr channel, ulong dspclock_start, ulong dspclock_end, bool stopchannels);

		// Token: 0x060002D7 RID: 727
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetDelay(IntPtr channel, out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);

		// Token: 0x060002D8 RID: 728
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_AddFadePoint(IntPtr channel, ulong dspclock, float volume);

		// Token: 0x060002D9 RID: 729
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetFadePointRamp(IntPtr channel, ulong dspclock, float volume);

		// Token: 0x060002DA RID: 730
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_RemoveFadePoints(IntPtr channel, ulong dspclock_start, ulong dspclock_end);

		// Token: 0x060002DB RID: 731
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetFadePoints(IntPtr channel, ref uint numpoints, ulong[] point_dspclock, float[] point_volume);

		// Token: 0x060002DC RID: 732
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetDSP(IntPtr channel, int index, out IntPtr dsp);

		// Token: 0x060002DD RID: 733
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_AddDSP(IntPtr channel, int index, IntPtr dsp);

		// Token: 0x060002DE RID: 734
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_RemoveDSP(IntPtr channel, IntPtr dsp);

		// Token: 0x060002DF RID: 735
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetNumDSPs(IntPtr channel, out int numdsps);

		// Token: 0x060002E0 RID: 736
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetDSPIndex(IntPtr channel, IntPtr dsp, int index);

		// Token: 0x060002E1 RID: 737
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetDSPIndex(IntPtr channel, IntPtr dsp, out int index);

		// Token: 0x060002E2 RID: 738
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DAttributes(IntPtr channel, ref VECTOR pos, ref VECTOR vel, ref VECTOR alt_pan_pos);

		// Token: 0x060002E3 RID: 739
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DAttributes(IntPtr channel, out VECTOR pos, out VECTOR vel, out VECTOR alt_pan_pos);

		// Token: 0x060002E4 RID: 740
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DMinMaxDistance(IntPtr channel, float mindistance, float maxdistance);

		// Token: 0x060002E5 RID: 741
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DMinMaxDistance(IntPtr channel, out float mindistance, out float maxdistance);

		// Token: 0x060002E6 RID: 742
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DConeSettings(IntPtr channel, float insideconeangle, float outsideconeangle, float outsidevolume);

		// Token: 0x060002E7 RID: 743
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DConeSettings(IntPtr channel, out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		// Token: 0x060002E8 RID: 744
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DConeOrientation(IntPtr channel, ref VECTOR orientation);

		// Token: 0x060002E9 RID: 745
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DConeOrientation(IntPtr channel, out VECTOR orientation);

		// Token: 0x060002EA RID: 746
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DCustomRolloff(IntPtr channel, ref VECTOR points, int numpoints);

		// Token: 0x060002EB RID: 747
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DCustomRolloff(IntPtr channel, out IntPtr points, out int numpoints);

		// Token: 0x060002EC RID: 748
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DOcclusion(IntPtr channel, float directocclusion, float reverbocclusion);

		// Token: 0x060002ED RID: 749
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DOcclusion(IntPtr channel, out float directocclusion, out float reverbocclusion);

		// Token: 0x060002EE RID: 750
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DSpread(IntPtr channel, float angle);

		// Token: 0x060002EF RID: 751
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DSpread(IntPtr channel, out float angle);

		// Token: 0x060002F0 RID: 752
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DLevel(IntPtr channel, float level);

		// Token: 0x060002F1 RID: 753
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DLevel(IntPtr channel, out float level);

		// Token: 0x060002F2 RID: 754
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DDopplerLevel(IntPtr channel, float level);

		// Token: 0x060002F3 RID: 755
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DDopplerLevel(IntPtr channel, out float level);

		// Token: 0x060002F4 RID: 756
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Set3DDistanceFilter(IntPtr channel, bool custom, float customLevel, float centerFreq);

		// Token: 0x060002F5 RID: 757
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_Get3DDistanceFilter(IntPtr channel, out bool custom, out float customLevel, out float centerFreq);

		// Token: 0x060002F6 RID: 758
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_SetUserData(IntPtr channel, IntPtr userdata);

		// Token: 0x060002F7 RID: 759
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Channel_GetUserData(IntPtr channel, out IntPtr userdata);

		// Token: 0x060002F8 RID: 760 RVA: 0x00006297 File Offset: 0x00004697
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000062A9 File Offset: 0x000046A9
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x04000286 RID: 646
		public IntPtr handle;
	}
}
