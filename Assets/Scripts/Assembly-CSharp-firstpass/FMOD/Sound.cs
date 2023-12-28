using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x02000060 RID: 96
	public struct Sound
	{
		// Token: 0x060001CA RID: 458 RVA: 0x000059F7 File Offset: 0x00003DF7
		public RESULT release()
		{
			return Sound.FMOD5_Sound_Release(this.handle);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005A04 File Offset: 0x00003E04
		public RESULT getSystemObject(out System system)
		{
			return Sound.FMOD5_Sound_GetSystemObject(this.handle, out system.handle);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005A17 File Offset: 0x00003E17
		public RESULT @lock(uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2)
		{
			return Sound.FMOD5_Sound_Lock(this.handle, offset, length, out ptr1, out ptr2, out len1, out len2);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005A2D File Offset: 0x00003E2D
		public RESULT unlock(IntPtr ptr1, IntPtr ptr2, uint len1, uint len2)
		{
			return Sound.FMOD5_Sound_Unlock(this.handle, ptr1, ptr2, len1, len2);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005A3F File Offset: 0x00003E3F
		public RESULT setDefaults(float frequency, int priority)
		{
			return Sound.FMOD5_Sound_SetDefaults(this.handle, frequency, priority);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005A4E File Offset: 0x00003E4E
		public RESULT getDefaults(out float frequency, out int priority)
		{
			return Sound.FMOD5_Sound_GetDefaults(this.handle, out frequency, out priority);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005A5D File Offset: 0x00003E5D
		public RESULT set3DMinMaxDistance(float min, float max)
		{
			return Sound.FMOD5_Sound_Set3DMinMaxDistance(this.handle, min, max);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005A6C File Offset: 0x00003E6C
		public RESULT get3DMinMaxDistance(out float min, out float max)
		{
			return Sound.FMOD5_Sound_Get3DMinMaxDistance(this.handle, out min, out max);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005A7B File Offset: 0x00003E7B
		public RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
		{
			return Sound.FMOD5_Sound_Set3DConeSettings(this.handle, insideconeangle, outsideconeangle, outsidevolume);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005A8B File Offset: 0x00003E8B
		public RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
		{
			return Sound.FMOD5_Sound_Get3DConeSettings(this.handle, out insideconeangle, out outsideconeangle, out outsidevolume);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005A9B File Offset: 0x00003E9B
		public RESULT set3DCustomRolloff(ref VECTOR points, int numpoints)
		{
			return Sound.FMOD5_Sound_Set3DCustomRolloff(this.handle, ref points, numpoints);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005AAA File Offset: 0x00003EAA
		public RESULT get3DCustomRolloff(out IntPtr points, out int numpoints)
		{
			return Sound.FMOD5_Sound_Get3DCustomRolloff(this.handle, out points, out numpoints);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005AB9 File Offset: 0x00003EB9
		public RESULT getSubSound(int index, out Sound subsound)
		{
			return Sound.FMOD5_Sound_GetSubSound(this.handle, index, out subsound.handle);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005ACD File Offset: 0x00003ECD
		public RESULT getSubSoundParent(out Sound parentsound)
		{
			return Sound.FMOD5_Sound_GetSubSoundParent(this.handle, out parentsound.handle);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005AE0 File Offset: 0x00003EE0
		public RESULT getName(out string name, int namelen)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(namelen);
			RESULT result = Sound.FMOD5_Sound_GetName(this.handle, intPtr, namelen);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				name = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005B3C File Offset: 0x00003F3C
		public RESULT getLength(out uint length, TIMEUNIT lengthtype)
		{
			return Sound.FMOD5_Sound_GetLength(this.handle, out length, lengthtype);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005B4B File Offset: 0x00003F4B
		public RESULT getFormat(out SOUND_TYPE type, out SOUND_FORMAT format, out int channels, out int bits)
		{
			return Sound.FMOD5_Sound_GetFormat(this.handle, out type, out format, out channels, out bits);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005B5D File Offset: 0x00003F5D
		public RESULT getNumSubSounds(out int numsubsounds)
		{
			return Sound.FMOD5_Sound_GetNumSubSounds(this.handle, out numsubsounds);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005B6B File Offset: 0x00003F6B
		public RESULT getNumTags(out int numtags, out int numtagsupdated)
		{
			return Sound.FMOD5_Sound_GetNumTags(this.handle, out numtags, out numtagsupdated);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005B7C File Offset: 0x00003F7C
		public RESULT getTag(string name, int index, out TAG tag)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = Sound.FMOD5_Sound_GetTag(this.handle, freeHelper.byteFromStringUTF8(name), index, out tag);
			}
			return result;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005BC8 File Offset: 0x00003FC8
		public RESULT getOpenState(out OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy)
		{
			return Sound.FMOD5_Sound_GetOpenState(this.handle, out openstate, out percentbuffered, out starving, out diskbusy);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005BDA File Offset: 0x00003FDA
		public RESULT readData(IntPtr buffer, uint lenbytes, out uint read)
		{
			return Sound.FMOD5_Sound_ReadData(this.handle, buffer, lenbytes, out read);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005BEA File Offset: 0x00003FEA
		public RESULT seekData(uint pcm)
		{
			return Sound.FMOD5_Sound_SeekData(this.handle, pcm);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005BF8 File Offset: 0x00003FF8
		public RESULT setSoundGroup(SoundGroup soundgroup)
		{
			return Sound.FMOD5_Sound_SetSoundGroup(this.handle, soundgroup.handle);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005C0C File Offset: 0x0000400C
		public RESULT getSoundGroup(out SoundGroup soundgroup)
		{
			return Sound.FMOD5_Sound_GetSoundGroup(this.handle, out soundgroup.handle);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005C1F File Offset: 0x0000401F
		public RESULT getNumSyncPoints(out int numsyncpoints)
		{
			return Sound.FMOD5_Sound_GetNumSyncPoints(this.handle, out numsyncpoints);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005C2D File Offset: 0x0000402D
		public RESULT getSyncPoint(int index, out IntPtr point)
		{
			return Sound.FMOD5_Sound_GetSyncPoint(this.handle, index, out point);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005C3C File Offset: 0x0000403C
		public RESULT getSyncPointInfo(IntPtr point, out string name, int namelen, out uint offset, TIMEUNIT offsettype)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(namelen);
			RESULT result = Sound.FMOD5_Sound_GetSyncPointInfo(this.handle, point, intPtr, namelen, out offset, offsettype);
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				name = freeHelper.stringFromNative(intPtr);
			}
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005C9C File Offset: 0x0000409C
		public RESULT getSyncPointInfo(IntPtr point, out uint offset, TIMEUNIT offsettype)
		{
			return Sound.FMOD5_Sound_GetSyncPointInfo(this.handle, point, IntPtr.Zero, 0, out offset, offsettype);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005CB4 File Offset: 0x000040B4
		public RESULT addSyncPoint(uint offset, TIMEUNIT offsettype, string name, out IntPtr point)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = Sound.FMOD5_Sound_AddSyncPoint(this.handle, offset, offsettype, freeHelper.byteFromStringUTF8(name), out point);
			}
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005D04 File Offset: 0x00004104
		public RESULT deleteSyncPoint(IntPtr point)
		{
			return Sound.FMOD5_Sound_DeleteSyncPoint(this.handle, point);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005D12 File Offset: 0x00004112
		public RESULT setMode(MODE mode)
		{
			return Sound.FMOD5_Sound_SetMode(this.handle, mode);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005D20 File Offset: 0x00004120
		public RESULT getMode(out MODE mode)
		{
			return Sound.FMOD5_Sound_GetMode(this.handle, out mode);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005D2E File Offset: 0x0000412E
		public RESULT setLoopCount(int loopcount)
		{
			return Sound.FMOD5_Sound_SetLoopCount(this.handle, loopcount);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005D3C File Offset: 0x0000413C
		public RESULT getLoopCount(out int loopcount)
		{
			return Sound.FMOD5_Sound_GetLoopCount(this.handle, out loopcount);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005D4A File Offset: 0x0000414A
		public RESULT setLoopPoints(uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype)
		{
			return Sound.FMOD5_Sound_SetLoopPoints(this.handle, loopstart, loopstarttype, loopend, loopendtype);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005D5C File Offset: 0x0000415C
		public RESULT getLoopPoints(out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype)
		{
			return Sound.FMOD5_Sound_GetLoopPoints(this.handle, out loopstart, loopstarttype, out loopend, loopendtype);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005D6E File Offset: 0x0000416E
		public RESULT getMusicNumChannels(out int numchannels)
		{
			return Sound.FMOD5_Sound_GetMusicNumChannels(this.handle, out numchannels);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005D7C File Offset: 0x0000417C
		public RESULT setMusicChannelVolume(int channel, float volume)
		{
			return Sound.FMOD5_Sound_SetMusicChannelVolume(this.handle, channel, volume);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00005D8B File Offset: 0x0000418B
		public RESULT getMusicChannelVolume(int channel, out float volume)
		{
			return Sound.FMOD5_Sound_GetMusicChannelVolume(this.handle, channel, out volume);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005D9A File Offset: 0x0000419A
		public RESULT setMusicSpeed(float speed)
		{
			return Sound.FMOD5_Sound_SetMusicSpeed(this.handle, speed);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005DA8 File Offset: 0x000041A8
		public RESULT getMusicSpeed(out float speed)
		{
			return Sound.FMOD5_Sound_GetMusicSpeed(this.handle, out speed);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005DB6 File Offset: 0x000041B6
		public RESULT setUserData(IntPtr userdata)
		{
			return Sound.FMOD5_Sound_SetUserData(this.handle, userdata);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005DC4 File Offset: 0x000041C4
		public RESULT getUserData(out IntPtr userdata)
		{
			return Sound.FMOD5_Sound_GetUserData(this.handle, out userdata);
		}

		// Token: 0x060001F6 RID: 502
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Release(IntPtr sound);

		// Token: 0x060001F7 RID: 503
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetSystemObject(IntPtr sound, out IntPtr system);

		// Token: 0x060001F8 RID: 504
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Lock(IntPtr sound, uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2);

		// Token: 0x060001F9 RID: 505
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Unlock(IntPtr sound, IntPtr ptr1, IntPtr ptr2, uint len1, uint len2);

		// Token: 0x060001FA RID: 506
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SetDefaults(IntPtr sound, float frequency, int priority);

		// Token: 0x060001FB RID: 507
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetDefaults(IntPtr sound, out float frequency, out int priority);

		// Token: 0x060001FC RID: 508
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Set3DMinMaxDistance(IntPtr sound, float min, float max);

		// Token: 0x060001FD RID: 509
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Get3DMinMaxDistance(IntPtr sound, out float min, out float max);

		// Token: 0x060001FE RID: 510
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Set3DConeSettings(IntPtr sound, float insideconeangle, float outsideconeangle, float outsidevolume);

		// Token: 0x060001FF RID: 511
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Get3DConeSettings(IntPtr sound, out float insideconeangle, out float outsideconeangle, out float outsidevolume);

		// Token: 0x06000200 RID: 512
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Set3DCustomRolloff(IntPtr sound, ref VECTOR points, int numpoints);

		// Token: 0x06000201 RID: 513
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_Get3DCustomRolloff(IntPtr sound, out IntPtr points, out int numpoints);

		// Token: 0x06000202 RID: 514
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetSubSound(IntPtr sound, int index, out IntPtr subsound);

		// Token: 0x06000203 RID: 515
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetSubSoundParent(IntPtr sound, out IntPtr parentsound);

		// Token: 0x06000204 RID: 516
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetName(IntPtr sound, IntPtr name, int namelen);

		// Token: 0x06000205 RID: 517
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetLength(IntPtr sound, out uint length, TIMEUNIT lengthtype);

		// Token: 0x06000206 RID: 518
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetFormat(IntPtr sound, out SOUND_TYPE type, out SOUND_FORMAT format, out int channels, out int bits);

		// Token: 0x06000207 RID: 519
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetNumSubSounds(IntPtr sound, out int numsubsounds);

		// Token: 0x06000208 RID: 520
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetNumTags(IntPtr sound, out int numtags, out int numtagsupdated);

		// Token: 0x06000209 RID: 521
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetTag(IntPtr sound, byte[] name, int index, out TAG tag);

		// Token: 0x0600020A RID: 522
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetOpenState(IntPtr sound, out OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy);

		// Token: 0x0600020B RID: 523
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_ReadData(IntPtr sound, IntPtr buffer, uint lenbytes, out uint read);

		// Token: 0x0600020C RID: 524
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SeekData(IntPtr sound, uint pcm);

		// Token: 0x0600020D RID: 525
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SetSoundGroup(IntPtr sound, IntPtr soundgroup);

		// Token: 0x0600020E RID: 526
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetSoundGroup(IntPtr sound, out IntPtr soundgroup);

		// Token: 0x0600020F RID: 527
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetNumSyncPoints(IntPtr sound, out int numsyncpoints);

		// Token: 0x06000210 RID: 528
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetSyncPoint(IntPtr sound, int index, out IntPtr point);

		// Token: 0x06000211 RID: 529
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetSyncPointInfo(IntPtr sound, IntPtr point, IntPtr name, int namelen, out uint offset, TIMEUNIT offsettype);

		// Token: 0x06000212 RID: 530
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_AddSyncPoint(IntPtr sound, uint offset, TIMEUNIT offsettype, byte[] name, out IntPtr point);

		// Token: 0x06000213 RID: 531
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_DeleteSyncPoint(IntPtr sound, IntPtr point);

		// Token: 0x06000214 RID: 532
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SetMode(IntPtr sound, MODE mode);

		// Token: 0x06000215 RID: 533
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetMode(IntPtr sound, out MODE mode);

		// Token: 0x06000216 RID: 534
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SetLoopCount(IntPtr sound, int loopcount);

		// Token: 0x06000217 RID: 535
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetLoopCount(IntPtr sound, out int loopcount);

		// Token: 0x06000218 RID: 536
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SetLoopPoints(IntPtr sound, uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype);

		// Token: 0x06000219 RID: 537
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetLoopPoints(IntPtr sound, out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype);

		// Token: 0x0600021A RID: 538
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetMusicNumChannels(IntPtr sound, out int numchannels);

		// Token: 0x0600021B RID: 539
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SetMusicChannelVolume(IntPtr sound, int channel, float volume);

		// Token: 0x0600021C RID: 540
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetMusicChannelVolume(IntPtr sound, int channel, out float volume);

		// Token: 0x0600021D RID: 541
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SetMusicSpeed(IntPtr sound, float speed);

		// Token: 0x0600021E RID: 542
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetMusicSpeed(IntPtr sound, out float speed);

		// Token: 0x0600021F RID: 543
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_SetUserData(IntPtr sound, IntPtr userdata);

		// Token: 0x06000220 RID: 544
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Sound_GetUserData(IntPtr sound, out IntPtr userdata);

		// Token: 0x06000221 RID: 545 RVA: 0x00005DD2 File Offset: 0x000041D2
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00005DE4 File Offset: 0x000041E4
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x04000285 RID: 645
		public IntPtr handle;
	}
}
