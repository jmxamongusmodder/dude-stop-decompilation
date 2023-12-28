using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x02000063 RID: 99
	public struct ChannelGroup : IChannelControl
	{
		// Token: 0x060002FA RID: 762 RVA: 0x000062B6 File Offset: 0x000046B6
		public RESULT release()
		{
			return ChannelGroup.FMOD5_ChannelGroup_Release(this.handle);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000062C3 File Offset: 0x000046C3
		public RESULT addGroup(ChannelGroup group, bool propagatedspclock, out DSPConnection connection)
		{
			return ChannelGroup.FMOD5_ChannelGroup_AddGroup(this.handle, group.handle, propagatedspclock, out connection.handle);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000062DE File Offset: 0x000046DE
		public RESULT getNumGroups(out int numgroups)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetNumGroups(this.handle, out numgroups);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000062EC File Offset: 0x000046EC
		public RESULT getGroup(int index, out ChannelGroup group)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetGroup(this.handle, index, out group.handle);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00006300 File Offset: 0x00004700
		public RESULT getParentGroup(out ChannelGroup group)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetParentGroup(this.handle, out group.handle);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00006314 File Offset: 0x00004714
		public RESULT getName(out string name, int namelen)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(namelen);
			RESULT result = ChannelGroup.FMOD5_ChannelGroup_GetName(this.handle, intPtr, namelen);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				name = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00006370 File Offset: 0x00004770
		public RESULT getNumChannels(out int numchannels)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetNumChannels(this.handle, out numchannels);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000637E File Offset: 0x0000477E
		public RESULT getChannel(int index, out Channel channel)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetChannel(this.handle, index, out channel.handle);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00006392 File Offset: 0x00004792
		public RESULT getSystemObject(out System system)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetSystemObject(this.handle, out system.handle);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000063A5 File Offset: 0x000047A5
		public RESULT stop()
		{
			return ChannelGroup.FMOD5_ChannelGroup_Stop(this.handle);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000063B2 File Offset: 0x000047B2
		public RESULT setPaused(bool paused)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetPaused(this.handle, paused);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000063C0 File Offset: 0x000047C0
		public RESULT getPaused(out bool paused)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetPaused(this.handle, out paused);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000063CE File Offset: 0x000047CE
		public RESULT setVolume(float volume)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetVolume(this.handle, volume);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000063DC File Offset: 0x000047DC
		public RESULT getVolume(out float volume)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetVolume(this.handle, out volume);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000063EA File Offset: 0x000047EA
		public RESULT setVolumeRamp(bool ramp)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetVolumeRamp(this.handle, ramp);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000063F8 File Offset: 0x000047F8
		public RESULT getVolumeRamp(out bool ramp)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetVolumeRamp(this.handle, out ramp);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00006406 File Offset: 0x00004806
		public RESULT getAudibility(out float audibility)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetAudibility(this.handle, out audibility);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00006414 File Offset: 0x00004814
		public RESULT setPitch(float pitch)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetPitch(this.handle, pitch);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00006422 File Offset: 0x00004822
		public RESULT getPitch(out float pitch)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetPitch(this.handle, out pitch);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00006430 File Offset: 0x00004830
		public RESULT setMute(bool mute)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetMute(this.handle, mute);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000643E File Offset: 0x0000483E
		public RESULT getMute(out bool mute)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetMute(this.handle, out mute);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000644C File Offset: 0x0000484C
		public RESULT setReverbProperties(int instance, float wet)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetReverbProperties(this.handle, instance, wet);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000645B File Offset: 0x0000485B
		public RESULT getReverbProperties(int instance, out float wet)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetReverbProperties(this.handle, instance, out wet);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000646A File Offset: 0x0000486A
		public RESULT setLowPassGain(float gain)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetLowPassGain(this.handle, gain);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00006478 File Offset: 0x00004878
		public RESULT getLowPassGain(out float gain)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetLowPassGain(this.handle, out gain);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00006486 File Offset: 0x00004886
		public RESULT setMode(MODE mode)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetMode(this.handle, mode);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00006494 File Offset: 0x00004894
		public RESULT getMode(out MODE mode)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetMode(this.handle, out mode);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000064A2 File Offset: 0x000048A2
		public RESULT setCallback(CHANNEL_CALLBACK callback)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetCallback(this.handle, callback);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000064B0 File Offset: 0x000048B0
		public RESULT isPlaying(out bool isplaying)
		{
			return ChannelGroup.FMOD5_ChannelGroup_IsPlaying(this.handle, out isplaying);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000064BE File Offset: 0x000048BE
		public RESULT setPan(float pan)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetPan(this.handle, pan);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000064CC File Offset: 0x000048CC
		public RESULT setMixLevelsOutput(float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetMixLevelsOutput(this.handle, frontleft, frontright, center, lfe, surroundleft, surroundright, backleft, backright);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000064F1 File Offset: 0x000048F1
		public RESULT setMixLevelsInput(float[] levels, int numlevels)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetMixLevelsInput(this.handle, levels, numlevels);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00006500 File Offset: 0x00004900
		public RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetMixMatrix(this.handle, matrix, outchannels, inchannels, inchannel_hop);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00006512 File Offset: 0x00004912
		public RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetMixMatrix(this.handle, matrix, out outchannels, out inchannels, inchannel_hop);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00006524 File Offset: 0x00004924
		public RESULT getDSPClock(out ulong dspclock, out ulong parentclock)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetDSPClock(this.handle, out dspclock, out parentclock);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00006533 File Offset: 0x00004933
		public RESULT setDelay(ulong dspclock_start, ulong dspclock_end, bool stopchannels)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetDelay(this.handle, dspclock_start, dspclock_end, stopchannels);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00006543 File Offset: 0x00004943
		public RESULT getDelay(out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetDelay(this.handle, out dspclock_start, out dspclock_end, out stopchannels);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00006553 File Offset: 0x00004953
		public RESULT addFadePoint(ulong dspclock, float volume)
		{
			return ChannelGroup.FMOD5_ChannelGroup_AddFadePoint(this.handle, dspclock, volume);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00006562 File Offset: 0x00004962
		public RESULT setFadePointRamp(ulong dspclock, float volume)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetFadePointRamp(this.handle, dspclock, volume);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00006571 File Offset: 0x00004971
		public RESULT removeFadePoints(ulong dspclock_start, ulong dspclock_end)
		{
			return ChannelGroup.FMOD5_ChannelGroup_RemoveFadePoints(this.handle, dspclock_start, dspclock_end);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00006580 File Offset: 0x00004980
		public RESULT getFadePoints(ref uint numpoints, ulong[] point_dspclock, float[] point_volume)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetFadePoints(this.handle, ref numpoints, point_dspclock, point_volume);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00006590 File Offset: 0x00004990
		public RESULT getDSP(int index, out DSP dsp)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetDSP(this.handle, index, out dsp.handle);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000065A4 File Offset: 0x000049A4
		public RESULT addDSP(int index, DSP dsp)
		{
			return ChannelGroup.FMOD5_ChannelGroup_AddDSP(this.handle, index, dsp.handle);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000065B9 File Offset: 0x000049B9
		public RESULT removeDSP(DSP dsp)
		{
			return ChannelGroup.FMOD5_ChannelGroup_RemoveDSP(this.handle, dsp.handle);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000065CD File Offset: 0x000049CD
		public RESULT getNumDSPs(out int numdsps)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetNumDSPs(this.handle, out numdsps);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000065DB File Offset: 0x000049DB
		public RESULT setDSPIndex(DSP dsp, int index)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetDSPIndex(this.handle, dsp.handle, index);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000065F0 File Offset: 0x000049F0
		public RESULT getDSPIndex(DSP dsp, out int index)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetDSPIndex(this.handle, dsp.handle, out index);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00006605 File Offset: 0x00004A05
		public RESULT set3DAttributes(ref VECTOR pos, ref VECTOR vel, ref VECTOR alt_pan_pos)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DAttributes(this.handle, ref pos, ref vel, ref alt_pan_pos);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00006615 File Offset: 0x00004A15
		public RESULT get3DAttributes(out VECTOR pos, out VECTOR vel, out VECTOR alt_pan_pos)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DAttributes(this.handle, out pos, out vel, out alt_pan_pos);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00006625 File Offset: 0x00004A25
		public RESULT set3DMinMaxDistance(float mindistance, float maxdistance)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DMinMaxDistance(this.handle, mindistance, maxdistance);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00006634 File Offset: 0x00004A34
		public RESULT get3DMinMaxDistance(out float mindistance, out float maxdistance)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DMinMaxDistance(this.handle, out mindistance, out maxdistance);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00006643 File Offset: 0x00004A43
		public RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DConeSettings(this.handle, insideconeangle, outsideconeangle, outsidevolume);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00006653 File Offset: 0x00004A53
		public RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DConeSettings(this.handle, out insideconeangle, out outsideconeangle, out outsidevolume);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00006663 File Offset: 0x00004A63
		public RESULT set3DConeOrientation(ref VECTOR orientation)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DConeOrientation(this.handle, ref orientation);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00006671 File Offset: 0x00004A71
		public RESULT get3DConeOrientation(out VECTOR orientation)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DConeOrientation(this.handle, out orientation);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000667F File Offset: 0x00004A7F
		public RESULT set3DCustomRolloff(ref VECTOR points, int numpoints)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DCustomRolloff(this.handle, ref points, numpoints);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000668E File Offset: 0x00004A8E
		public RESULT get3DCustomRolloff(out IntPtr points, out int numpoints)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DCustomRolloff(this.handle, out points, out numpoints);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000669D File Offset: 0x00004A9D
		public RESULT set3DOcclusion(float directocclusion, float reverbocclusion)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DOcclusion(this.handle, directocclusion, reverbocclusion);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000066AC File Offset: 0x00004AAC
		public RESULT get3DOcclusion(out float directocclusion, out float reverbocclusion)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DOcclusion(this.handle, out directocclusion, out reverbocclusion);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000066BB File Offset: 0x00004ABB
		public RESULT set3DSpread(float angle)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DSpread(this.handle, angle);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000066C9 File Offset: 0x00004AC9
		public RESULT get3DSpread(out float angle)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DSpread(this.handle, out angle);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000066D7 File Offset: 0x00004AD7
		public RESULT set3DLevel(float level)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DLevel(this.handle, level);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000066E5 File Offset: 0x00004AE5
		public RESULT get3DLevel(out float level)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DLevel(this.handle, out level);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000066F3 File Offset: 0x00004AF3
		public RESULT set3DDopplerLevel(float level)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DDopplerLevel(this.handle, level);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00006701 File Offset: 0x00004B01
		public RESULT get3DDopplerLevel(out float level)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DDopplerLevel(this.handle, out level);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000670F File Offset: 0x00004B0F
		public RESULT set3DDistanceFilter(bool custom, float customLevel, float centerFreq)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Set3DDistanceFilter(this.handle, custom, customLevel, centerFreq);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000671F File Offset: 0x00004B1F
		public RESULT get3DDistanceFilter(out bool custom, out float customLevel, out float centerFreq)
		{
			return ChannelGroup.FMOD5_ChannelGroup_Get3DDistanceFilter(this.handle, out custom, out customLevel, out centerFreq);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000672F File Offset: 0x00004B2F
		public RESULT setUserData(IntPtr userdata)
		{
			return ChannelGroup.FMOD5_ChannelGroup_SetUserData(this.handle, userdata);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000673D File Offset: 0x00004B3D
		public RESULT getUserData(out IntPtr userdata)
		{
			return ChannelGroup.FMOD5_ChannelGroup_GetUserData(this.handle, out userdata);
		}

		// Token: 0x0600033F RID: 831
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Release(IntPtr channelgroup);

		// Token: 0x06000340 RID: 832
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_AddGroup(IntPtr channelgroup, IntPtr group, bool propogatedspclocks, out IntPtr connection);

		// Token: 0x06000341 RID: 833
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetNumGroups(IntPtr channelgroup, out int numgroups);

		// Token: 0x06000342 RID: 834
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetGroup(IntPtr channelgroup, int index, out IntPtr group);

		// Token: 0x06000343 RID: 835
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetParentGroup(IntPtr channelgroup, out IntPtr group);

		// Token: 0x06000344 RID: 836
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetName(IntPtr channelgroup, IntPtr name, int namelen);

		// Token: 0x06000345 RID: 837
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetNumChannels(IntPtr channelgroup, out int numchannels);

		// Token: 0x06000346 RID: 838
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetChannel(IntPtr channelgroup, int index, out IntPtr channel);

		// Token: 0x06000347 RID: 839
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetSystemObject(IntPtr channelgroup, out IntPtr system);

		// Token: 0x06000348 RID: 840
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Stop(IntPtr channelgroup);

		// Token: 0x06000349 RID: 841
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetPaused(IntPtr channelgroup, bool paused);

		// Token: 0x0600034A RID: 842
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetPaused(IntPtr channelgroup, out bool paused);

		// Token: 0x0600034B RID: 843
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetVolume(IntPtr channelgroup, float volume);

		// Token: 0x0600034C RID: 844
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetVolume(IntPtr channelgroup, out float volume);

		// Token: 0x0600034D RID: 845
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetVolumeRamp(IntPtr channelgroup, bool ramp);

		// Token: 0x0600034E RID: 846
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetVolumeRamp(IntPtr channelgroup, out bool ramp);

		// Token: 0x0600034F RID: 847
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetAudibility(IntPtr channelgroup, out float audibility);

		// Token: 0x06000350 RID: 848
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetPitch(IntPtr channelgroup, float pitch);

		// Token: 0x06000351 RID: 849
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetPitch(IntPtr channelgroup, out float pitch);

		// Token: 0x06000352 RID: 850
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetMute(IntPtr channelgroup, bool mute);

		// Token: 0x06000353 RID: 851
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetMute(IntPtr channelgroup, out bool mute);

		// Token: 0x06000354 RID: 852
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetReverbProperties(IntPtr channelgroup, int instance, float wet);

		// Token: 0x06000355 RID: 853
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetReverbProperties(IntPtr channelgroup, int instance, out float wet);

		// Token: 0x06000356 RID: 854
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetLowPassGain(IntPtr channelgroup, float gain);

		// Token: 0x06000357 RID: 855
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetLowPassGain(IntPtr channelgroup, out float gain);

		// Token: 0x06000358 RID: 856
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetMode(IntPtr channelgroup, MODE mode);

		// Token: 0x06000359 RID: 857
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetMode(IntPtr channelgroup, out MODE mode);

		// Token: 0x0600035A RID: 858
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetCallback(IntPtr channelgroup, CHANNEL_CALLBACK callback);

		// Token: 0x0600035B RID: 859
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_IsPlaying(IntPtr channelgroup, out bool isplaying);

		// Token: 0x0600035C RID: 860
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetPan(IntPtr channelgroup, float pan);

		// Token: 0x0600035D RID: 861
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetMixLevelsOutput(IntPtr channelgroup, float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);

		// Token: 0x0600035E RID: 862
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetMixLevelsInput(IntPtr channelgroup, float[] levels, int numlevels);

		// Token: 0x0600035F RID: 863
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetMixMatrix(IntPtr channelgroup, float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		// Token: 0x06000360 RID: 864
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetMixMatrix(IntPtr channelgroup, float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);

		// Token: 0x06000361 RID: 865
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetDSPClock(IntPtr channelgroup, out ulong dspclock, out ulong parentclock);

		// Token: 0x06000362 RID: 866
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetDelay(IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end, bool stopchannels);

		// Token: 0x06000363 RID: 867
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetDelay(IntPtr channelgroup, out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);

		// Token: 0x06000364 RID: 868
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_AddFadePoint(IntPtr channelgroup, ulong dspclock, float volume);

		// Token: 0x06000365 RID: 869
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetFadePointRamp(IntPtr channelgroup, ulong dspclock, float volume);

		// Token: 0x06000366 RID: 870
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_RemoveFadePoints(IntPtr channelgroup, ulong dspclock_start, ulong dspclock_end);

		// Token: 0x06000367 RID: 871
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetFadePoints(IntPtr channelgroup, ref uint numpoints, ulong[] point_dspclock, float[] point_volume);

		// Token: 0x06000368 RID: 872
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetDSP(IntPtr channelgroup, int index, out IntPtr dsp);

		// Token: 0x06000369 RID: 873
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_AddDSP(IntPtr channelgroup, int index, IntPtr dsp);

		// Token: 0x0600036A RID: 874
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_RemoveDSP(IntPtr channelgroup, IntPtr dsp);

		// Token: 0x0600036B RID: 875
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetNumDSPs(IntPtr channelgroup, out int numdsps);

		// Token: 0x0600036C RID: 876
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetDSPIndex(IntPtr channelgroup, IntPtr dsp, int index);

		// Token: 0x0600036D RID: 877
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetDSPIndex(IntPtr channelgroup, IntPtr dsp, out int index);

		// Token: 0x0600036E RID: 878
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DAttributes(IntPtr channelgroup, ref VECTOR pos, ref VECTOR vel, ref VECTOR alt_pan_pos);

		// Token: 0x0600036F RID: 879
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DAttributes(IntPtr channelgroup, out VECTOR pos, out VECTOR vel, out VECTOR alt_pan_pos);

		// Token: 0x06000370 RID: 880
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DMinMaxDistance(IntPtr channelgroup, float mindistance, float maxdistance);

		// Token: 0x06000371 RID: 881
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DMinMaxDistance(IntPtr channelgroup, out float mindistance, out float maxdistance);

		// Token: 0x06000372 RID: 882
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DConeSettings(IntPtr channelgroup, float insideconeangle, float outsideconeangle, float outsidevolume);

		// Token: 0x06000373 RID: 883
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DConeSettings(IntPtr channelgroup, out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		// Token: 0x06000374 RID: 884
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DConeOrientation(IntPtr channelgroup, ref VECTOR orientation);

		// Token: 0x06000375 RID: 885
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DConeOrientation(IntPtr channelgroup, out VECTOR orientation);

		// Token: 0x06000376 RID: 886
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DCustomRolloff(IntPtr channelgroup, ref VECTOR points, int numpoints);

		// Token: 0x06000377 RID: 887
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DCustomRolloff(IntPtr channelgroup, out IntPtr points, out int numpoints);

		// Token: 0x06000378 RID: 888
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DOcclusion(IntPtr channelgroup, float directocclusion, float reverbocclusion);

		// Token: 0x06000379 RID: 889
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DOcclusion(IntPtr channelgroup, out float directocclusion, out float reverbocclusion);

		// Token: 0x0600037A RID: 890
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DSpread(IntPtr channelgroup, float angle);

		// Token: 0x0600037B RID: 891
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DSpread(IntPtr channelgroup, out float angle);

		// Token: 0x0600037C RID: 892
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DLevel(IntPtr channelgroup, float level);

		// Token: 0x0600037D RID: 893
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DLevel(IntPtr channelgroup, out float level);

		// Token: 0x0600037E RID: 894
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DDopplerLevel(IntPtr channelgroup, float level);

		// Token: 0x0600037F RID: 895
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DDopplerLevel(IntPtr channelgroup, out float level);

		// Token: 0x06000380 RID: 896
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Set3DDistanceFilter(IntPtr channelgroup, bool custom, float customLevel, float centerFreq);

		// Token: 0x06000381 RID: 897
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_Get3DDistanceFilter(IntPtr channelgroup, out bool custom, out float customLevel, out float centerFreq);

		// Token: 0x06000382 RID: 898
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_SetUserData(IntPtr channelgroup, IntPtr userdata);

		// Token: 0x06000383 RID: 899
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_ChannelGroup_GetUserData(IntPtr channelgroup, out IntPtr userdata);

		// Token: 0x06000384 RID: 900 RVA: 0x0000674B File Offset: 0x00004B4B
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000675D File Offset: 0x00004B5D
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x04000287 RID: 647
		public IntPtr handle;
	}
}
