using System;

namespace FMOD
{
	// Token: 0x02000061 RID: 97
	internal interface IChannelControl
	{
		// Token: 0x06000223 RID: 547
		RESULT getSystemObject(out System system);

		// Token: 0x06000224 RID: 548
		RESULT stop();

		// Token: 0x06000225 RID: 549
		RESULT setPaused(bool paused);

		// Token: 0x06000226 RID: 550
		RESULT getPaused(out bool paused);

		// Token: 0x06000227 RID: 551
		RESULT setVolume(float volume);

		// Token: 0x06000228 RID: 552
		RESULT getVolume(out float volume);

		// Token: 0x06000229 RID: 553
		RESULT setVolumeRamp(bool ramp);

		// Token: 0x0600022A RID: 554
		RESULT getVolumeRamp(out bool ramp);

		// Token: 0x0600022B RID: 555
		RESULT getAudibility(out float audibility);

		// Token: 0x0600022C RID: 556
		RESULT setPitch(float pitch);

		// Token: 0x0600022D RID: 557
		RESULT getPitch(out float pitch);

		// Token: 0x0600022E RID: 558
		RESULT setMute(bool mute);

		// Token: 0x0600022F RID: 559
		RESULT getMute(out bool mute);

		// Token: 0x06000230 RID: 560
		RESULT setReverbProperties(int instance, float wet);

		// Token: 0x06000231 RID: 561
		RESULT getReverbProperties(int instance, out float wet);

		// Token: 0x06000232 RID: 562
		RESULT setLowPassGain(float gain);

		// Token: 0x06000233 RID: 563
		RESULT getLowPassGain(out float gain);

		// Token: 0x06000234 RID: 564
		RESULT setMode(MODE mode);

		// Token: 0x06000235 RID: 565
		RESULT getMode(out MODE mode);

		// Token: 0x06000236 RID: 566
		RESULT setCallback(CHANNEL_CALLBACK callback);

		// Token: 0x06000237 RID: 567
		RESULT isPlaying(out bool isplaying);

		// Token: 0x06000238 RID: 568
		RESULT setPan(float pan);

		// Token: 0x06000239 RID: 569
		RESULT setMixLevelsOutput(float frontleft, float frontright, float center, float lfe, float surroundleft, float surroundright, float backleft, float backright);

		// Token: 0x0600023A RID: 570
		RESULT setMixLevelsInput(float[] levels, int numlevels);

		// Token: 0x0600023B RID: 571
		RESULT setMixMatrix(float[] matrix, int outchannels, int inchannels, int inchannel_hop);

		// Token: 0x0600023C RID: 572
		RESULT getMixMatrix(float[] matrix, out int outchannels, out int inchannels, int inchannel_hop);

		// Token: 0x0600023D RID: 573
		RESULT getDSPClock(out ulong dspclock, out ulong parentclock);

		// Token: 0x0600023E RID: 574
		RESULT setDelay(ulong dspclock_start, ulong dspclock_end, bool stopchannels);

		// Token: 0x0600023F RID: 575
		RESULT getDelay(out ulong dspclock_start, out ulong dspclock_end, out bool stopchannels);

		// Token: 0x06000240 RID: 576
		RESULT addFadePoint(ulong dspclock, float volume);

		// Token: 0x06000241 RID: 577
		RESULT setFadePointRamp(ulong dspclock, float volume);

		// Token: 0x06000242 RID: 578
		RESULT removeFadePoints(ulong dspclock_start, ulong dspclock_end);

		// Token: 0x06000243 RID: 579
		RESULT getFadePoints(ref uint numpoints, ulong[] point_dspclock, float[] point_volume);

		// Token: 0x06000244 RID: 580
		RESULT getDSP(int index, out DSP dsp);

		// Token: 0x06000245 RID: 581
		RESULT addDSP(int index, DSP dsp);

		// Token: 0x06000246 RID: 582
		RESULT removeDSP(DSP dsp);

		// Token: 0x06000247 RID: 583
		RESULT getNumDSPs(out int numdsps);

		// Token: 0x06000248 RID: 584
		RESULT setDSPIndex(DSP dsp, int index);

		// Token: 0x06000249 RID: 585
		RESULT getDSPIndex(DSP dsp, out int index);

		// Token: 0x0600024A RID: 586
		RESULT set3DAttributes(ref VECTOR pos, ref VECTOR vel, ref VECTOR alt_pan_pos);

		// Token: 0x0600024B RID: 587
		RESULT get3DAttributes(out VECTOR pos, out VECTOR vel, out VECTOR alt_pan_pos);

		// Token: 0x0600024C RID: 588
		RESULT set3DMinMaxDistance(float mindistance, float maxdistance);

		// Token: 0x0600024D RID: 589
		RESULT get3DMinMaxDistance(out float mindistance, out float maxdistance);

		// Token: 0x0600024E RID: 590
		RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume);

		// Token: 0x0600024F RID: 591
		RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		// Token: 0x06000250 RID: 592
		RESULT set3DConeOrientation(ref VECTOR orientation);

		// Token: 0x06000251 RID: 593
		RESULT get3DConeOrientation(out VECTOR orientation);

		// Token: 0x06000252 RID: 594
		RESULT set3DCustomRolloff(ref VECTOR points, int numpoints);

		// Token: 0x06000253 RID: 595
		RESULT get3DCustomRolloff(out IntPtr points, out int numpoints);

		// Token: 0x06000254 RID: 596
		RESULT set3DOcclusion(float directocclusion, float reverbocclusion);

		// Token: 0x06000255 RID: 597
		RESULT get3DOcclusion(out float directocclusion, out float reverbocclusion);

		// Token: 0x06000256 RID: 598
		RESULT set3DSpread(float angle);

		// Token: 0x06000257 RID: 599
		RESULT get3DSpread(out float angle);

		// Token: 0x06000258 RID: 600
		RESULT set3DLevel(float level);

		// Token: 0x06000259 RID: 601
		RESULT get3DLevel(out float level);

		// Token: 0x0600025A RID: 602
		RESULT set3DDopplerLevel(float level);

		// Token: 0x0600025B RID: 603
		RESULT get3DDopplerLevel(out float level);

		// Token: 0x0600025C RID: 604
		RESULT set3DDistanceFilter(bool custom, float customLevel, float centerFreq);

		// Token: 0x0600025D RID: 605
		RESULT get3DDistanceFilter(out bool custom, out float customLevel, out float centerFreq);

		// Token: 0x0600025E RID: 606
		RESULT setUserData(IntPtr userdata);

		// Token: 0x0600025F RID: 607
		RESULT getUserData(out IntPtr userdata);
	}
}
