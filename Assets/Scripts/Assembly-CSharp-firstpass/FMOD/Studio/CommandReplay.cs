using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000FF RID: 255
	public struct CommandReplay
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x000089F5 File Offset: 0x00006DF5
		public RESULT getSystem(out System system)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetSystem(this.handle, out system.handle);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00008A08 File Offset: 0x00006E08
		public RESULT getLength(out float totalTime)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetLength(this.handle, out totalTime);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00008A16 File Offset: 0x00006E16
		public RESULT getCommandCount(out int count)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetCommandCount(this.handle, out count);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00008A24 File Offset: 0x00006E24
		public RESULT getCommandInfo(int commandIndex, out COMMAND_INFO info)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetCommandInfo(this.handle, commandIndex, out info);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00008A34 File Offset: 0x00006E34
		public RESULT getCommandString(int commandIndex, out string description)
		{
			description = null;
			RESULT result2;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				int num = 256;
				IntPtr intPtr = Marshal.AllocHGlobal(256);
				RESULT result;
				for (result = CommandReplay.FMOD_Studio_CommandReplay_GetCommandString(this.handle, commandIndex, intPtr, num); result == RESULT.ERR_TRUNCATED; result = CommandReplay.FMOD_Studio_CommandReplay_GetCommandString(this.handle, commandIndex, intPtr, num))
				{
					Marshal.FreeHGlobal(intPtr);
					num *= 2;
					intPtr = Marshal.AllocHGlobal(num);
				}
				if (result == RESULT.OK)
				{
					description = freeHelper.stringFromNative(intPtr);
				}
				Marshal.FreeHGlobal(intPtr);
				result2 = result;
			}
			return result2;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00008AD4 File Offset: 0x00006ED4
		public RESULT getCommandAtTime(float time, out int commandIndex)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetCommandAtTime(this.handle, time, out commandIndex);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00008AE4 File Offset: 0x00006EE4
		public RESULT setBankPath(string bankPath)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = CommandReplay.FMOD_Studio_CommandReplay_SetBankPath(this.handle, freeHelper.byteFromStringUTF8(bankPath));
			}
			return result;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00008B30 File Offset: 0x00006F30
		public RESULT start()
		{
			return CommandReplay.FMOD_Studio_CommandReplay_Start(this.handle);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00008B3D File Offset: 0x00006F3D
		public RESULT stop()
		{
			return CommandReplay.FMOD_Studio_CommandReplay_Stop(this.handle);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00008B4A File Offset: 0x00006F4A
		public RESULT seekToTime(float time)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_SeekToTime(this.handle, time);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00008B58 File Offset: 0x00006F58
		public RESULT seekToCommand(int commandIndex)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_SeekToCommand(this.handle, commandIndex);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00008B66 File Offset: 0x00006F66
		public RESULT getPaused(out bool paused)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetPaused(this.handle, out paused);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00008B74 File Offset: 0x00006F74
		public RESULT setPaused(bool paused)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_SetPaused(this.handle, paused);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00008B82 File Offset: 0x00006F82
		public RESULT getPlaybackState(out PLAYBACK_STATE state)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetPlaybackState(this.handle, out state);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00008B90 File Offset: 0x00006F90
		public RESULT getCurrentCommand(out int commandIndex, out float currentTime)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetCurrentCommand(this.handle, out commandIndex, out currentTime);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00008B9F File Offset: 0x00006F9F
		public RESULT release()
		{
			return CommandReplay.FMOD_Studio_CommandReplay_Release(this.handle);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00008BAC File Offset: 0x00006FAC
		public RESULT setFrameCallback(COMMANDREPLAY_FRAME_CALLBACK callback)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_SetFrameCallback(this.handle, callback);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00008BBA File Offset: 0x00006FBA
		public RESULT setLoadBankCallback(COMMANDREPLAY_LOAD_BANK_CALLBACK callback)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_SetLoadBankCallback(this.handle, callback);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00008BC8 File Offset: 0x00006FC8
		public RESULT setCreateInstanceCallback(COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_SetCreateInstanceCallback(this.handle, callback);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00008BD6 File Offset: 0x00006FD6
		public RESULT getUserData(out IntPtr userdata)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_GetUserData(this.handle, out userdata);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00008BE4 File Offset: 0x00006FE4
		public RESULT setUserData(IntPtr userdata)
		{
			return CommandReplay.FMOD_Studio_CommandReplay_SetUserData(this.handle, userdata);
		}

		// Token: 0x0600064F RID: 1615
		[DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_CommandReplay_IsValid(IntPtr replay);

		// Token: 0x06000650 RID: 1616
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetSystem(IntPtr replay, out IntPtr system);

		// Token: 0x06000651 RID: 1617
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetLength(IntPtr replay, out float totalTime);

		// Token: 0x06000652 RID: 1618
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCommandCount(IntPtr replay, out int count);

		// Token: 0x06000653 RID: 1619
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCommandInfo(IntPtr replay, int commandIndex, out COMMAND_INFO info);

		// Token: 0x06000654 RID: 1620
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCommandString(IntPtr replay, int commandIndex, IntPtr description, int capacity);

		// Token: 0x06000655 RID: 1621
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCommandAtTime(IntPtr replay, float time, out int commandIndex);

		// Token: 0x06000656 RID: 1622
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_SetBankPath(IntPtr replay, byte[] bankPath);

		// Token: 0x06000657 RID: 1623
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_Start(IntPtr replay);

		// Token: 0x06000658 RID: 1624
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_Stop(IntPtr replay);

		// Token: 0x06000659 RID: 1625
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_SeekToTime(IntPtr replay, float time);

		// Token: 0x0600065A RID: 1626
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_SeekToCommand(IntPtr replay, int commandIndex);

		// Token: 0x0600065B RID: 1627
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetPaused(IntPtr replay, out bool paused);

		// Token: 0x0600065C RID: 1628
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_SetPaused(IntPtr replay, bool paused);

		// Token: 0x0600065D RID: 1629
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetPlaybackState(IntPtr replay, out PLAYBACK_STATE state);

		// Token: 0x0600065E RID: 1630
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetCurrentCommand(IntPtr replay, out int commandIndex, out float currentTime);

		// Token: 0x0600065F RID: 1631
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_Release(IntPtr replay);

		// Token: 0x06000660 RID: 1632
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_SetFrameCallback(IntPtr replay, COMMANDREPLAY_FRAME_CALLBACK callback);

		// Token: 0x06000661 RID: 1633
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_SetLoadBankCallback(IntPtr replay, COMMANDREPLAY_LOAD_BANK_CALLBACK callback);

		// Token: 0x06000662 RID: 1634
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_SetCreateInstanceCallback(IntPtr replay, COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback);

		// Token: 0x06000663 RID: 1635
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_GetUserData(IntPtr replay, out IntPtr userdata);

		// Token: 0x06000664 RID: 1636
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_CommandReplay_SetUserData(IntPtr replay, IntPtr userdata);

		// Token: 0x06000665 RID: 1637 RVA: 0x00008BF2 File Offset: 0x00006FF2
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00008C04 File Offset: 0x00007004
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00008C11 File Offset: 0x00007011
		public bool isValid()
		{
			return this.hasHandle() && CommandReplay.FMOD_Studio_CommandReplay_IsValid(this.handle);
		}

		// Token: 0x040004F0 RID: 1264
		public IntPtr handle;
	}
}
