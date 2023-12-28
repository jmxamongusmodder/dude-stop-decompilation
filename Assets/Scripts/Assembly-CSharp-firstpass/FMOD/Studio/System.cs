using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000F8 RID: 248
	public struct System
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0000768C File Offset: 0x00005A8C
		public static RESULT create(out System studiosystem)
		{
			return System.FMOD_Studio_System_Create(out studiosystem.handle, 69635U);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000769E File Offset: 0x00005A9E
		public RESULT setAdvancedSettings(ADVANCEDSETTINGS settings)
		{
			settings.cbsize = Marshal.SizeOf(typeof(ADVANCEDSETTINGS));
			return System.FMOD_Studio_System_SetAdvancedSettings(this.handle, ref settings);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000076C3 File Offset: 0x00005AC3
		public RESULT getAdvancedSettings(out ADVANCEDSETTINGS settings)
		{
			settings.cbsize = Marshal.SizeOf(typeof(ADVANCEDSETTINGS));
			return System.FMOD_Studio_System_GetAdvancedSettings(this.handle, out settings);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000076E6 File Offset: 0x00005AE6
		public RESULT initialize(int maxchannels, INITFLAGS studioFlags, INITFLAGS flags, IntPtr extradriverdata)
		{
			return System.FMOD_Studio_System_Initialize(this.handle, maxchannels, studioFlags, flags, extradriverdata);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000076F8 File Offset: 0x00005AF8
		public RESULT release()
		{
			return System.FMOD_Studio_System_Release(this.handle);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00007705 File Offset: 0x00005B05
		public RESULT update()
		{
			return System.FMOD_Studio_System_Update(this.handle);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00007712 File Offset: 0x00005B12
		public RESULT getLowLevelSystem(out System system)
		{
			return System.FMOD_Studio_System_GetLowLevelSystem(this.handle, out system.handle);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00007728 File Offset: 0x00005B28
		public RESULT getEvent(string path, out EventDescription _event)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_GetEvent(this.handle, freeHelper.byteFromStringUTF8(path), out _event.handle);
			}
			return result;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00007778 File Offset: 0x00005B78
		public RESULT getBus(string path, out Bus bus)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_GetBus(this.handle, freeHelper.byteFromStringUTF8(path), out bus.handle);
			}
			return result;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000077C8 File Offset: 0x00005BC8
		public RESULT getVCA(string path, out VCA vca)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_GetVCA(this.handle, freeHelper.byteFromStringUTF8(path), out vca.handle);
			}
			return result;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00007818 File Offset: 0x00005C18
		public RESULT getBank(string path, out Bank bank)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_GetBank(this.handle, freeHelper.byteFromStringUTF8(path), out bank.handle);
			}
			return result;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00007868 File Offset: 0x00005C68
		public RESULT getEventByID(Guid guid, out EventDescription _event)
		{
			return System.FMOD_Studio_System_GetEventByID(this.handle, ref guid, out _event.handle);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000787D File Offset: 0x00005C7D
		public RESULT getBusByID(Guid guid, out Bus bus)
		{
			return System.FMOD_Studio_System_GetBusByID(this.handle, ref guid, out bus.handle);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00007892 File Offset: 0x00005C92
		public RESULT getVCAByID(Guid guid, out VCA vca)
		{
			return System.FMOD_Studio_System_GetVCAByID(this.handle, ref guid, out vca.handle);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000078A7 File Offset: 0x00005CA7
		public RESULT getBankByID(Guid guid, out Bank bank)
		{
			return System.FMOD_Studio_System_GetBankByID(this.handle, ref guid, out bank.handle);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000078BC File Offset: 0x00005CBC
		public RESULT getSoundInfo(string key, out SOUND_INFO info)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_GetSoundInfo(this.handle, freeHelper.byteFromStringUTF8(key), out info);
			}
			return result;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00007908 File Offset: 0x00005D08
		public RESULT lookupID(string path, out Guid guid)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_LookupID(this.handle, freeHelper.byteFromStringUTF8(path), out guid);
			}
			return result;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00007954 File Offset: 0x00005D54
		public RESULT lookupPath(Guid guid, out string path)
		{
			path = null;
			RESULT result2;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				IntPtr intPtr = Marshal.AllocHGlobal(256);
				int num = 0;
				RESULT result = System.FMOD_Studio_System_LookupPath(this.handle, ref guid, intPtr, 256, out num);
				if (result == RESULT.ERR_TRUNCATED)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = Marshal.AllocHGlobal(num);
					result = System.FMOD_Studio_System_LookupPath(this.handle, ref guid, intPtr, num, out num);
				}
				if (result == RESULT.OK)
				{
					path = freeHelper.stringFromNative(intPtr);
				}
				Marshal.FreeHGlobal(intPtr);
				result2 = result;
			}
			return result2;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000079F0 File Offset: 0x00005DF0
		public RESULT getNumListeners(out int numlisteners)
		{
			return System.FMOD_Studio_System_GetNumListeners(this.handle, out numlisteners);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000079FE File Offset: 0x00005DFE
		public RESULT setNumListeners(int numlisteners)
		{
			return System.FMOD_Studio_System_SetNumListeners(this.handle, numlisteners);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00007A0C File Offset: 0x00005E0C
		public RESULT getListenerAttributes(int listener, out ATTRIBUTES_3D attributes)
		{
			return System.FMOD_Studio_System_GetListenerAttributes(this.handle, listener, out attributes);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00007A1B File Offset: 0x00005E1B
		public RESULT setListenerAttributes(int listener, ATTRIBUTES_3D attributes)
		{
			return System.FMOD_Studio_System_SetListenerAttributes(this.handle, listener, ref attributes);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00007A2B File Offset: 0x00005E2B
		public RESULT getListenerWeight(int listener, out float weight)
		{
			return System.FMOD_Studio_System_GetListenerWeight(this.handle, listener, out weight);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00007A3A File Offset: 0x00005E3A
		public RESULT setListenerWeight(int listener, float weight)
		{
			return System.FMOD_Studio_System_SetListenerWeight(this.handle, listener, weight);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00007A4C File Offset: 0x00005E4C
		public RESULT loadBankFile(string name, LOAD_BANK_FLAGS flags, out Bank bank)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_LoadBankFile(this.handle, freeHelper.byteFromStringUTF8(name), flags, out bank.handle);
			}
			return result;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00007A9C File Offset: 0x00005E9C
		public RESULT loadBankMemory(byte[] buffer, LOAD_BANK_FLAGS flags, out Bank bank)
		{
			GCHandle gchandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			IntPtr buffer2 = gchandle.AddrOfPinnedObject();
			RESULT result = System.FMOD_Studio_System_LoadBankMemory(this.handle, buffer2, buffer.Length, LOAD_MEMORY_MODE.LOAD_MEMORY, flags, out bank.handle);
			gchandle.Free();
			return result;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00007AD9 File Offset: 0x00005ED9
		public RESULT loadBankCustom(BANK_INFO info, LOAD_BANK_FLAGS flags, out Bank bank)
		{
			info.size = Marshal.SizeOf(info);
			return System.FMOD_Studio_System_LoadBankCustom(this.handle, ref info, flags, out bank.handle);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00007B01 File Offset: 0x00005F01
		public RESULT unloadAll()
		{
			return System.FMOD_Studio_System_UnloadAll(this.handle);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00007B0E File Offset: 0x00005F0E
		public RESULT flushCommands()
		{
			return System.FMOD_Studio_System_FlushCommands(this.handle);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00007B1B File Offset: 0x00005F1B
		public RESULT flushSampleLoading()
		{
			return System.FMOD_Studio_System_FlushSampleLoading(this.handle);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00007B28 File Offset: 0x00005F28
		public RESULT startCommandCapture(string path, COMMANDCAPTURE_FLAGS flags)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_StartCommandCapture(this.handle, freeHelper.byteFromStringUTF8(path), flags);
			}
			return result;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00007B74 File Offset: 0x00005F74
		public RESULT stopCommandCapture()
		{
			return System.FMOD_Studio_System_StopCommandCapture(this.handle);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00007B84 File Offset: 0x00005F84
		public RESULT loadCommandReplay(string path, COMMANDREPLAY_FLAGS flags, out CommandReplay replay)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = System.FMOD_Studio_System_LoadCommandReplay(this.handle, freeHelper.byteFromStringUTF8(path), flags, out replay.handle);
			}
			return result;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00007BD4 File Offset: 0x00005FD4
		public RESULT getBankCount(out int count)
		{
			return System.FMOD_Studio_System_GetBankCount(this.handle, out count);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00007BE4 File Offset: 0x00005FE4
		public RESULT getBankList(out Bank[] array)
		{
			array = null;
			int num;
			RESULT result = System.FMOD_Studio_System_GetBankCount(this.handle, out num);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num == 0)
			{
				array = new Bank[0];
				return result;
			}
			IntPtr[] array2 = new IntPtr[num];
			int num2;
			result = System.FMOD_Studio_System_GetBankList(this.handle, array2, num, out num2);
			if (result != RESULT.OK)
			{
				return result;
			}
			if (num2 > num)
			{
				num2 = num;
			}
			array = new Bank[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i].handle = array2[i];
			}
			return RESULT.OK;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00007C73 File Offset: 0x00006073
		public RESULT getCPUUsage(out CPU_USAGE usage)
		{
			return System.FMOD_Studio_System_GetCPUUsage(this.handle, out usage);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00007C81 File Offset: 0x00006081
		public RESULT getBufferUsage(out BUFFER_USAGE usage)
		{
			return System.FMOD_Studio_System_GetBufferUsage(this.handle, out usage);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00007C8F File Offset: 0x0000608F
		public RESULT resetBufferUsage()
		{
			return System.FMOD_Studio_System_ResetBufferUsage(this.handle);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00007C9C File Offset: 0x0000609C
		public RESULT setCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask = SYSTEM_CALLBACK_TYPE.ALL)
		{
			return System.FMOD_Studio_System_SetCallback(this.handle, callback, callbackmask);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00007CAB File Offset: 0x000060AB
		public RESULT getUserData(out IntPtr userdata)
		{
			return System.FMOD_Studio_System_GetUserData(this.handle, out userdata);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00007CB9 File Offset: 0x000060B9
		public RESULT setUserData(IntPtr userdata)
		{
			return System.FMOD_Studio_System_SetUserData(this.handle, userdata);
		}

		// Token: 0x06000531 RID: 1329
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_Create(out IntPtr studiosystem, uint headerversion);

		// Token: 0x06000532 RID: 1330
		[DllImport("fmodstudio")]
		private static extern bool FMOD_Studio_System_IsValid(IntPtr studiosystem);

		// Token: 0x06000533 RID: 1331
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_SetAdvancedSettings(IntPtr studiosystem, ref ADVANCEDSETTINGS settings);

		// Token: 0x06000534 RID: 1332
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetAdvancedSettings(IntPtr studiosystem, out ADVANCEDSETTINGS settings);

		// Token: 0x06000535 RID: 1333
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_Initialize(IntPtr studiosystem, int maxchannels, INITFLAGS studioFlags, INITFLAGS flags, IntPtr extradriverdata);

		// Token: 0x06000536 RID: 1334
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_Release(IntPtr studiosystem);

		// Token: 0x06000537 RID: 1335
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_Update(IntPtr studiosystem);

		// Token: 0x06000538 RID: 1336
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetLowLevelSystem(IntPtr studiosystem, out IntPtr system);

		// Token: 0x06000539 RID: 1337
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetEvent(IntPtr studiosystem, byte[] path, out IntPtr description);

		// Token: 0x0600053A RID: 1338
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetBus(IntPtr studiosystem, byte[] path, out IntPtr bus);

		// Token: 0x0600053B RID: 1339
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetVCA(IntPtr studiosystem, byte[] path, out IntPtr vca);

		// Token: 0x0600053C RID: 1340
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetBank(IntPtr studiosystem, byte[] path, out IntPtr bank);

		// Token: 0x0600053D RID: 1341
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetEventByID(IntPtr studiosystem, ref Guid guid, out IntPtr description);

		// Token: 0x0600053E RID: 1342
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetBusByID(IntPtr studiosystem, ref Guid guid, out IntPtr bus);

		// Token: 0x0600053F RID: 1343
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetVCAByID(IntPtr studiosystem, ref Guid guid, out IntPtr vca);

		// Token: 0x06000540 RID: 1344
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetBankByID(IntPtr studiosystem, ref Guid guid, out IntPtr bank);

		// Token: 0x06000541 RID: 1345
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetSoundInfo(IntPtr studiosystem, byte[] key, out SOUND_INFO info);

		// Token: 0x06000542 RID: 1346
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_LookupID(IntPtr studiosystem, byte[] path, out Guid guid);

		// Token: 0x06000543 RID: 1347
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_LookupPath(IntPtr studiosystem, ref Guid guid, IntPtr path, int size, out int retrieved);

		// Token: 0x06000544 RID: 1348
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetNumListeners(IntPtr studiosystem, out int numlisteners);

		// Token: 0x06000545 RID: 1349
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_SetNumListeners(IntPtr studiosystem, int numlisteners);

		// Token: 0x06000546 RID: 1350
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetListenerAttributes(IntPtr studiosystem, int listener, out ATTRIBUTES_3D attributes);

		// Token: 0x06000547 RID: 1351
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_SetListenerAttributes(IntPtr studiosystem, int listener, ref ATTRIBUTES_3D attributes);

		// Token: 0x06000548 RID: 1352
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetListenerWeight(IntPtr studiosystem, int listener, out float weight);

		// Token: 0x06000549 RID: 1353
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_SetListenerWeight(IntPtr studiosystem, int listener, float weight);

		// Token: 0x0600054A RID: 1354
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_LoadBankFile(IntPtr studiosystem, byte[] filename, LOAD_BANK_FLAGS flags, out IntPtr bank);

		// Token: 0x0600054B RID: 1355
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_LoadBankMemory(IntPtr studiosystem, IntPtr buffer, int length, LOAD_MEMORY_MODE mode, LOAD_BANK_FLAGS flags, out IntPtr bank);

		// Token: 0x0600054C RID: 1356
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_LoadBankCustom(IntPtr studiosystem, ref BANK_INFO info, LOAD_BANK_FLAGS flags, out IntPtr bank);

		// Token: 0x0600054D RID: 1357
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_UnloadAll(IntPtr studiosystem);

		// Token: 0x0600054E RID: 1358
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_FlushCommands(IntPtr studiosystem);

		// Token: 0x0600054F RID: 1359
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_FlushSampleLoading(IntPtr studiosystem);

		// Token: 0x06000550 RID: 1360
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_StartCommandCapture(IntPtr studiosystem, byte[] path, COMMANDCAPTURE_FLAGS flags);

		// Token: 0x06000551 RID: 1361
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_StopCommandCapture(IntPtr studiosystem);

		// Token: 0x06000552 RID: 1362
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_LoadCommandReplay(IntPtr studiosystem, byte[] path, COMMANDREPLAY_FLAGS flags, out IntPtr commandReplay);

		// Token: 0x06000553 RID: 1363
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetBankCount(IntPtr studiosystem, out int count);

		// Token: 0x06000554 RID: 1364
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetBankList(IntPtr studiosystem, IntPtr[] array, int capacity, out int count);

		// Token: 0x06000555 RID: 1365
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetCPUUsage(IntPtr studiosystem, out CPU_USAGE usage);

		// Token: 0x06000556 RID: 1366
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetBufferUsage(IntPtr studiosystem, out BUFFER_USAGE usage);

		// Token: 0x06000557 RID: 1367
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_ResetBufferUsage(IntPtr studiosystem);

		// Token: 0x06000558 RID: 1368
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_SetCallback(IntPtr studiosystem, SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask);

		// Token: 0x06000559 RID: 1369
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_GetUserData(IntPtr studiosystem, out IntPtr userdata);

		// Token: 0x0600055A RID: 1370
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_System_SetUserData(IntPtr studiosystem, IntPtr userdata);

		// Token: 0x0600055B RID: 1371 RVA: 0x00007CC7 File Offset: 0x000060C7
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00007CD9 File Offset: 0x000060D9
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00007CE6 File Offset: 0x000060E6
		public bool isValid()
		{
			return this.hasHandle() && System.FMOD_Studio_System_IsValid(this.handle);
		}

		// Token: 0x040004E9 RID: 1257
		public IntPtr handle;
	}
}
