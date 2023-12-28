using System;
using System.Collections.Generic;
using System.Text;
using FMOD;
using FMOD.Studio;
using UnityEngine;

namespace FMODUnity
{
	// Token: 0x02000009 RID: 9
	[AddComponentMenu("")]
	public class RuntimeManager : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002328 File Offset: 0x00000728
		private static RuntimeManager Instance
		{
			get
			{
				if (RuntimeManager.initException != null)
				{
					throw RuntimeManager.initException;
				}
				if (RuntimeManager.isQuitting)
				{
					throw new Exception("FMOD Studio attempted access by script to RuntimeManager while application is quitting");
				}
				if (RuntimeManager.instance == null)
				{
					RESULT result = RESULT.OK;
					RuntimeManager runtimeManager = UnityEngine.Object.FindObjectOfType(typeof(RuntimeManager)) as RuntimeManager;
					if (runtimeManager != null && runtimeManager.cachedPointers[0] != 0L)
					{
						RuntimeManager.instance = runtimeManager;
						RuntimeManager.instance.studioSystem.handle = (IntPtr)RuntimeManager.instance.cachedPointers[0];
						RuntimeManager.instance.lowlevelSystem.handle = (IntPtr)RuntimeManager.instance.cachedPointers[1];
						return RuntimeManager.instance;
					}
					GameObject gameObject = new GameObject("FMOD.UnityIntegration.RuntimeManager");
					RuntimeManager.instance = gameObject.AddComponent<RuntimeManager>();
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					gameObject.hideFlags = HideFlags.HideInHierarchy;
					try
					{
						RuntimeUtils.EnforceLibraryOrder();
						result = RuntimeManager.instance.Initialize();
					}
					catch (Exception ex)
					{
						RuntimeManager.initException = (ex as SystemNotInitializedException);
						if (RuntimeManager.initException == null)
						{
							RuntimeManager.initException = new SystemNotInitializedException(ex);
						}
						throw RuntimeManager.initException;
					}
					if (result != RESULT.OK)
					{
						throw new SystemNotInitializedException(result, "Output forced to NO SOUND mode");
					}
				}
				return RuntimeManager.instance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002470 File Offset: 0x00000870
		public static FMOD.Studio.System StudioSystem
		{
			get
			{
				return RuntimeManager.Instance.studioSystem;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000247C File Offset: 0x0000087C
		public static FMOD.System LowlevelSystem
		{
			get
			{
				return RuntimeManager.Instance.lowlevelSystem;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002488 File Offset: 0x00000888
		private void CheckInitResult(RESULT result, string cause)
		{
			if (result != RESULT.OK)
			{
				if (this.studioSystem.isValid())
				{
					this.studioSystem.release();
					this.studioSystem.clearHandle();
				}
				throw new SystemNotInitializedException(result, cause);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024C0 File Offset: 0x000008C0
		private RESULT Initialize()
		{
			RESULT result = RESULT.OK;
			Settings settings = Settings.Instance;
			this.fmodPlatform = RuntimeUtils.GetCurrentPlatform();
			int sampleRate = settings.GetSampleRate(this.fmodPlatform);
			int num = Math.Min(settings.GetRealChannels(this.fmodPlatform), 256);
			int virtualChannels = settings.GetVirtualChannels(this.fmodPlatform);
			SPEAKERMODE speakerMode = (SPEAKERMODE)settings.GetSpeakerMode(this.fmodPlatform);
			OUTPUTTYPE output = OUTPUTTYPE.AUTODETECT;
			FMOD.ADVANCEDSETTINGS advancedsettings = default(FMOD.ADVANCEDSETTINGS);
			advancedsettings.randomSeed = (uint)DateTime.Now.Ticks;
			advancedsettings.maxVorbisCodecs = num;
			FMOD.Studio.INITFLAGS initflags = FMOD.Studio.INITFLAGS.DEFERRED_CALLBACKS;
			if (settings.IsLiveUpdateEnabled(this.fmodPlatform))
			{
				initflags |= FMOD.Studio.INITFLAGS.LIVEUPDATE;
			}
			for (;;)
			{
				RESULT result2 = FMOD.Studio.System.create(out this.studioSystem);
				this.CheckInitResult(result2, "FMOD.Studio.System.create");
				result2 = this.studioSystem.getLowLevelSystem(out this.lowlevelSystem);
				this.CheckInitResult(result2, "FMOD.Studio.System.getLowLevelSystem");
				result2 = this.lowlevelSystem.setOutput(output);
				this.CheckInitResult(result2, "FMOD.System.setOutput");
				result2 = this.lowlevelSystem.setSoftwareChannels(num);
				this.CheckInitResult(result2, "FMOD.System.setSoftwareChannels");
				result2 = this.lowlevelSystem.setSoftwareFormat(sampleRate, speakerMode, 0);
				this.CheckInitResult(result2, "FMOD.System.setSoftwareFormat");
				result2 = this.lowlevelSystem.setAdvancedSettings(ref advancedsettings);
				this.CheckInitResult(result2, "FMOD.System.setAdvancedSettings");
				result2 = this.studioSystem.initialize(virtualChannels, initflags, FMOD.INITFLAGS.NORMAL, IntPtr.Zero);
				if (result2 != RESULT.OK && result == RESULT.OK)
				{
					result = result2;
					output = OUTPUTTYPE.NOSOUND;
					UnityEngine.Debug.LogErrorFormat("FMOD Studio: Studio::System::initialize returned {0}, defaulting to no-sound mode.", new object[]
					{
						result2.ToString()
					});
				}
				else
				{
					this.CheckInitResult(result2, "Studio::System::initialize");
					if ((initflags & FMOD.Studio.INITFLAGS.LIVEUPDATE) == FMOD.Studio.INITFLAGS.NORMAL)
					{
						break;
					}
					this.studioSystem.flushCommands();
					result2 = this.studioSystem.update();
					if (result2 != RESULT.ERR_NET_SOCKET_ERROR)
					{
						break;
					}
					initflags &= ~FMOD.Studio.INITFLAGS.LIVEUPDATE;
					UnityEngine.Debug.LogWarning("FMOD Studio: Cannot open network port for Live Update (in-use), restarting with Live Update disabled.");
					result2 = this.studioSystem.release();
					this.CheckInitResult(result2, "FMOD.Studio.System.Release");
				}
			}
			this.LoadPlugins(settings);
			this.LoadBanks(settings);
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026C8 File Offset: 0x00000AC8
		private void Update()
		{
			if (this.studioSystem.isValid())
			{
				this.studioSystem.update();
				bool flag = false;
				bool flag2 = false;
				int numListeners = 0;
				for (int i = 7; i >= 0; i--)
				{
					if (!flag && RuntimeManager.HasListener[i])
					{
						numListeners = i + 1;
						flag = true;
						flag2 = true;
					}
					if (!RuntimeManager.HasListener[i] && flag)
					{
						flag2 = false;
					}
				}
				if (flag)
				{
					this.studioSystem.setNumListeners(numListeners);
				}
				if (!flag2 && !this.listenerWarningIssued)
				{
					this.listenerWarningIssued = true;
					UnityEngine.Debug.LogWarning("FMOD Studio Integration: Please add an 'FMOD Studio Listener' component to your a camera in the scene for correct 3D positioning of sounds");
				}
				for (int j = 0; j < this.attachedInstances.Count; j++)
				{
					PLAYBACK_STATE playback_STATE = PLAYBACK_STATE.STOPPED;
					this.attachedInstances[j].instance.getPlaybackState(out playback_STATE);
					if (!this.attachedInstances[j].instance.isValid() || playback_STATE == PLAYBACK_STATE.STOPPED || this.attachedInstances[j].transform == null)
					{
						this.attachedInstances.RemoveAt(j);
						j--;
					}
					else if (this.attachedInstances[j].rigidBody)
					{
						this.attachedInstances[j].instance.set3DAttributes(RuntimeUtils.To3DAttributes(this.attachedInstances[j].transform, this.attachedInstances[j].rigidBody));
					}
					else
					{
						this.attachedInstances[j].instance.set3DAttributes(RuntimeUtils.To3DAttributes(this.attachedInstances[j].transform, this.attachedInstances[j].rigidBody2D));
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028A8 File Offset: 0x00000CA8
		public static void AttachInstanceToGameObject(EventInstance instance, Transform transform, Rigidbody rigidBody)
		{
			RuntimeManager.AttachedInstance attachedInstance = new RuntimeManager.AttachedInstance();
			attachedInstance.transform = transform;
			attachedInstance.instance = instance;
			attachedInstance.rigidBody = rigidBody;
			RuntimeManager.Instance.attachedInstances.Add(attachedInstance);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028E0 File Offset: 0x00000CE0
		public static void AttachInstanceToGameObject(EventInstance instance, Transform transform, Rigidbody2D rigidBody2D)
		{
			RuntimeManager.AttachedInstance attachedInstance = new RuntimeManager.AttachedInstance();
			attachedInstance.transform = transform;
			attachedInstance.instance = instance;
			attachedInstance.rigidBody2D = rigidBody2D;
			attachedInstance.rigidBody = null;
			RuntimeManager.Instance.attachedInstances.Add(attachedInstance);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002920 File Offset: 0x00000D20
		public static void DetachInstanceFromGameObject(EventInstance instance)
		{
			RuntimeManager runtimeManager = RuntimeManager.Instance;
			for (int i = 0; i < runtimeManager.attachedInstances.Count; i++)
			{
				if (runtimeManager.attachedInstances[i].instance.handle == instance.handle)
				{
					runtimeManager.attachedInstances.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002984 File Offset: 0x00000D84
		private void OnGUI()
		{
			if (this.studioSystem.isValid() && Settings.Instance.IsOverlayEnabled(this.fmodPlatform))
			{
				this.windowRect = GUI.Window(0, this.windowRect, new GUI.WindowFunction(this.DrawDebugOverlay), "FMOD Studio Debug");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000029DC File Offset: 0x00000DDC
		private void DrawDebugOverlay(int windowID)
		{
			if (this.lastDebugUpdate + 0.25f < Time.unscaledTime)
			{
				if (RuntimeManager.initException != null)
				{
					this.lastDebugText = RuntimeManager.initException.Message;
				}
				else
				{
					if (!this.mixerHead.hasHandle())
					{
						ChannelGroup channelGroup;
						this.lowlevelSystem.getMasterChannelGroup(out channelGroup);
						channelGroup.getDSP(0, out this.mixerHead);
						this.mixerHead.setMeteringEnabled(false, true);
					}
					StringBuilder stringBuilder = new StringBuilder();
					CPU_USAGE cpu_USAGE;
					this.studioSystem.getCPUUsage(out cpu_USAGE);
					stringBuilder.AppendFormat("CPU: dsp = {0:F1}%, studio = {1:F1}%\n", cpu_USAGE.dspusage, cpu_USAGE.studiousage);
					int num;
					int num2;
					Memory.GetStats(out num, out num2);
					stringBuilder.AppendFormat("MEMORY: cur = {0}MB, max = {1}MB\n", num >> 20, num2 >> 20);
					int num3;
					int num4;
					this.lowlevelSystem.getChannelsPlaying(out num3, out num4);
					stringBuilder.AppendFormat("CHANNELS: real = {0}, total = {1}\n", num4, num3);
					DSP_METERING_INFO dsp_METERING_INFO;
					this.mixerHead.getMeteringInfo(IntPtr.Zero, out dsp_METERING_INFO);
					float num5 = 0f;
					for (int i = 0; i < (int)dsp_METERING_INFO.numchannels; i++)
					{
						num5 += dsp_METERING_INFO.rmslevel[i] * dsp_METERING_INFO.rmslevel[i];
					}
					num5 = Mathf.Sqrt(num5 / (float)dsp_METERING_INFO.numchannels);
					float num6 = (num5 <= 0f) ? -80f : (20f * Mathf.Log10(num5 * Mathf.Sqrt(2f)));
					if (num6 > 10f)
					{
						num6 = 10f;
					}
					stringBuilder.AppendFormat("VOLUME: RMS = {0:f2}db\n", num6);
					this.lastDebugText = stringBuilder.ToString();
					this.lastDebugUpdate = Time.unscaledTime;
				}
			}
			GUI.Label(new Rect(10f, 20f, 290f, 100f), this.lastDebugText);
			GUI.DragWindow();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002BDC File Offset: 0x00000FDC
		private void OnDisable()
		{
			this.cachedPointers[0] = (long)this.studioSystem.handle;
			this.cachedPointers[1] = (long)this.lowlevelSystem.handle;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002C0E File Offset: 0x0000100E
		private void OnDestroy()
		{
			if (this.studioSystem.isValid())
			{
				this.studioSystem.release();
				this.studioSystem.clearHandle();
			}
			RuntimeManager.initException = null;
			RuntimeManager.instance = null;
			RuntimeManager.isQuitting = true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002C4C File Offset: 0x0000104C
		private void OnApplicationPause(bool pauseStatus)
		{
			if (this.studioSystem.isValid())
			{
				if (this.loadedBanks.Count > 1)
				{
					RuntimeManager.PauseAllEvents(pauseStatus);
				}
				if (pauseStatus)
				{
					this.lowlevelSystem.mixerSuspend();
				}
				else
				{
					this.lowlevelSystem.mixerResume();
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002CA4 File Offset: 0x000010A4
		public static void LoadBank(string bankName, bool loadSamples = false)
		{
			if (RuntimeManager.Instance.loadedBanks.ContainsKey(bankName))
			{
				RuntimeManager.LoadedBank value = RuntimeManager.Instance.loadedBanks[bankName];
				value.RefCount++;
				if (loadSamples)
				{
					value.Bank.loadSampleData();
				}
				RuntimeManager.Instance.loadedBanks[bankName] = value;
			}
			else
			{
				RuntimeManager.LoadedBank value2 = default(RuntimeManager.LoadedBank);
				string bankPath = RuntimeUtils.GetBankPath(bankName);
				RESULT result = RuntimeManager.Instance.studioSystem.loadBankFile(bankPath, LOAD_BANK_FLAGS.NORMAL, out value2.Bank);
				if (result == RESULT.OK)
				{
					value2.RefCount = 1;
					RuntimeManager.Instance.loadedBanks.Add(bankName, value2);
					if (loadSamples)
					{
						value2.Bank.loadSampleData();
					}
				}
				else
				{
					if (result != RESULT.ERR_EVENT_ALREADY_LOADED)
					{
						throw new BankLoadException(bankPath, result);
					}
					value2.RefCount = 2;
					RuntimeManager.Instance.loadedBanks.Add(bankName, value2);
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002D9C File Offset: 0x0000119C
		public static void LoadBank(TextAsset asset, bool loadSamples = false)
		{
			string name = asset.name;
			if (RuntimeManager.Instance.loadedBanks.ContainsKey(name))
			{
				RuntimeManager.LoadedBank loadedBank = RuntimeManager.Instance.loadedBanks[name];
				loadedBank.RefCount++;
				if (loadSamples)
				{
					loadedBank.Bank.loadSampleData();
				}
			}
			else
			{
				RuntimeManager.LoadedBank value = default(RuntimeManager.LoadedBank);
				RESULT result = RuntimeManager.Instance.studioSystem.loadBankMemory(asset.bytes, LOAD_BANK_FLAGS.NORMAL, out value.Bank);
				if (result == RESULT.OK)
				{
					value.RefCount = 1;
					RuntimeManager.Instance.loadedBanks.Add(name, value);
					if (loadSamples)
					{
						value.Bank.loadSampleData();
					}
				}
				else
				{
					if (result != RESULT.ERR_EVENT_ALREADY_LOADED)
					{
						throw new BankLoadException(name, result);
					}
					value.RefCount = 2;
					RuntimeManager.Instance.loadedBanks.Add(name, value);
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002E88 File Offset: 0x00001288
		private void LoadBanks(Settings fmodSettings)
		{
			if (fmodSettings.ImportType == ImportType.StreamingAssets)
			{
				try
				{
					RuntimeManager.LoadBank(fmodSettings.MasterBank + ".strings", fmodSettings.AutomaticSampleLoading);
					if (fmodSettings.AutomaticEventLoading)
					{
						RuntimeManager.LoadBank(fmodSettings.MasterBank, fmodSettings.AutomaticSampleLoading);
						foreach (string bankName in fmodSettings.Banks)
						{
							RuntimeManager.LoadBank(bankName, fmodSettings.AutomaticSampleLoading);
						}
						RuntimeManager.WaitForAllLoads();
					}
				}
				catch (BankLoadException exception)
				{
					UnityEngine.Debug.LogException(exception);
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002F50 File Offset: 0x00001350
		public static void UnloadBank(string bankName)
		{
			RuntimeManager.LoadedBank value;
			if (RuntimeManager.Instance.loadedBanks.TryGetValue(bankName, out value))
			{
				value.RefCount--;
				if (value.RefCount == 0)
				{
					value.Bank.unload();
					RuntimeManager.Instance.loadedBanks.Remove(bankName);
					return;
				}
				RuntimeManager.Instance.loadedBanks[bankName] = value;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002FC0 File Offset: 0x000013C0
		public static bool AnyBankLoading()
		{
			bool flag = false;
			foreach (RuntimeManager.LoadedBank loadedBank in RuntimeManager.Instance.loadedBanks.Values)
			{
				LOADING_STATE loading_STATE;
				loadedBank.Bank.getSampleLoadingState(out loading_STATE);
				flag |= (loading_STATE == LOADING_STATE.LOADING);
			}
			return flag;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003038 File Offset: 0x00001438
		public static void WaitForAllLoads()
		{
			RuntimeManager.Instance.studioSystem.flushSampleLoading();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000304C File Offset: 0x0000144C
		public static Guid PathToGUID(string path)
		{
			Guid empty = Guid.Empty;
			if (path.StartsWith("{"))
			{
				Util.ParseID(path, out empty);
			}
			else
			{
				RESULT result = RuntimeManager.Instance.studioSystem.lookupID(path, out empty);
				if (result == RESULT.ERR_EVENT_NOTFOUND)
				{
					throw new EventNotFoundException(path);
				}
			}
			return empty;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000030A0 File Offset: 0x000014A0
		public static EventInstance CreateInstance(string path)
		{
			EventInstance result;
			try
			{
				result = RuntimeManager.CreateInstance(RuntimeManager.PathToGUID(path));
			}
			catch (EventNotFoundException)
			{
				throw new EventNotFoundException(path);
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000030D8 File Offset: 0x000014D8
		public static EventInstance CreateInstance(Guid guid)
		{
			EventInstance result;
			RuntimeManager.GetEventDescription(guid).createInstance(out result);
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000030F8 File Offset: 0x000014F8
		public static void PlayOneShot(string path, Vector3 position = default(Vector3))
		{
			try
			{
				RuntimeManager.PlayOneShot(RuntimeManager.PathToGUID(path), position);
			}
			catch (EventNotFoundException)
			{
				UnityEngine.Debug.LogWarning("FMOD Event not found: " + path);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000313C File Offset: 0x0000153C
		public static void PlayOneShot(Guid guid, Vector3 position = default(Vector3))
		{
			EventInstance eventInstance = RuntimeManager.CreateInstance(guid);
			eventInstance.set3DAttributes(position.To3DAttributes());
			eventInstance.start();
			eventInstance.release();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003170 File Offset: 0x00001570
		public static void PlayOneShotAttached(string path, GameObject gameObject)
		{
			try
			{
				RuntimeManager.PlayOneShotAttached(RuntimeManager.PathToGUID(path), gameObject);
			}
			catch (EventNotFoundException)
			{
				UnityEngine.Debug.LogWarning("FMOD Event not found: " + path);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000031B4 File Offset: 0x000015B4
		public static void PlayOneShotAttached(Guid guid, GameObject gameObject)
		{
			EventInstance eventInstance = RuntimeManager.CreateInstance(guid);
			RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
			eventInstance.start();
			eventInstance.release();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000031EC File Offset: 0x000015EC
		public static EventDescription GetEventDescription(string path)
		{
			EventDescription eventDescription;
			try
			{
				eventDescription = RuntimeManager.GetEventDescription(RuntimeManager.PathToGUID(path));
			}
			catch (EventNotFoundException)
			{
				throw new EventNotFoundException(path);
			}
			return eventDescription;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003224 File Offset: 0x00001624
		public static EventDescription GetEventDescription(Guid guid)
		{
			EventDescription eventDescription;
			if (RuntimeManager.Instance.cachedDescriptions.ContainsKey(guid) && RuntimeManager.Instance.cachedDescriptions[guid].isValid())
			{
				eventDescription = RuntimeManager.Instance.cachedDescriptions[guid];
			}
			else
			{
				RESULT eventByID = RuntimeManager.Instance.studioSystem.getEventByID(guid, out eventDescription);
				if (eventByID != RESULT.OK)
				{
					throw new EventNotFoundException(guid);
				}
				if (eventDescription.isValid())
				{
					RuntimeManager.Instance.cachedDescriptions[guid] = eventDescription;
				}
			}
			return eventDescription;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000032B7 File Offset: 0x000016B7
		public static void SetListenerLocation(GameObject gameObject, Rigidbody rigidBody = null)
		{
			RuntimeManager.Instance.studioSystem.setListenerAttributes(0, RuntimeUtils.To3DAttributes(gameObject, rigidBody));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000032D1 File Offset: 0x000016D1
		public static void SetListenerLocation(GameObject gameObject, Rigidbody2D rigidBody2D)
		{
			RuntimeManager.Instance.studioSystem.setListenerAttributes(0, RuntimeUtils.To3DAttributes(gameObject, rigidBody2D));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000032EB File Offset: 0x000016EB
		public static void SetListenerLocation(Transform transform)
		{
			RuntimeManager.Instance.studioSystem.setListenerAttributes(0, transform.To3DAttributes());
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003304 File Offset: 0x00001704
		public static void SetListenerLocation(int listenerIndex, GameObject gameObject, Rigidbody rigidBody = null)
		{
			RuntimeManager.Instance.studioSystem.setListenerAttributes(listenerIndex, RuntimeUtils.To3DAttributes(gameObject, rigidBody));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000331E File Offset: 0x0000171E
		public static void SetListenerLocation(int listenerIndex, GameObject gameObject, Rigidbody2D rigidBody2D)
		{
			RuntimeManager.Instance.studioSystem.setListenerAttributes(listenerIndex, RuntimeUtils.To3DAttributes(gameObject, rigidBody2D));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003338 File Offset: 0x00001738
		public static void SetListenerLocation(int listenerIndex, Transform transform)
		{
			RuntimeManager.Instance.studioSystem.setListenerAttributes(listenerIndex, transform.To3DAttributes());
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003354 File Offset: 0x00001754
		public static Bus GetBus(string path)
		{
			Bus result;
			if (RuntimeManager.StudioSystem.getBus(path, out result) != RESULT.OK)
			{
				throw new BusNotFoundException(path);
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003380 File Offset: 0x00001780
		public static VCA GetVCA(string path)
		{
			VCA result;
			if (RuntimeManager.StudioSystem.getVCA(path, out result) != RESULT.OK)
			{
				throw new VCANotFoundException(path);
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000033AC File Offset: 0x000017AC
		public static void PauseAllEvents(bool paused)
		{
			RuntimeManager.GetBus("bus:/").setPaused(paused);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000033D0 File Offset: 0x000017D0
		public static void MuteAllEvents(bool muted)
		{
			RuntimeManager.GetBus("bus:/").setMute(muted);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000033F1 File Offset: 0x000017F1
		public static bool IsInitialized
		{
			get
			{
				return RuntimeManager.instance != null && RuntimeManager.instance.studioSystem.isValid();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003415 File Offset: 0x00001815
		public static bool HasBanksLoaded
		{
			get
			{
				return RuntimeManager.instance.loadedBanks.Count > 1;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000342C File Offset: 0x0000182C
		private void LoadPlugins(Settings fmodSettings)
		{
			foreach (string text in fmodSettings.Plugins)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string pluginPath = RuntimeUtils.GetPluginPath(text);
					uint value;
					RESULT result = this.lowlevelSystem.loadPlugin(pluginPath, out value);
					this.CheckInitResult(result, string.Format("Loading plugin '{0}' from '{1}'", text, pluginPath));
					this.loadedPlugins.Add(text, value);
				}
			}
		}

		// Token: 0x0400000B RID: 11
		private static SystemNotInitializedException initException = null;

		// Token: 0x0400000C RID: 12
		private static RuntimeManager instance;

		// Token: 0x0400000D RID: 13
		private static bool isQuitting = false;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		private FMODPlatform fmodPlatform;

		// Token: 0x0400000F RID: 15
		private FMOD.Studio.System studioSystem;

		// Token: 0x04000010 RID: 16
		private FMOD.System lowlevelSystem;

		// Token: 0x04000011 RID: 17
		private DSP mixerHead;

		// Token: 0x04000012 RID: 18
		[SerializeField]
		private long[] cachedPointers = new long[2];

		// Token: 0x04000013 RID: 19
		private Dictionary<string, RuntimeManager.LoadedBank> loadedBanks = new Dictionary<string, RuntimeManager.LoadedBank>();

		// Token: 0x04000014 RID: 20
		private Dictionary<string, uint> loadedPlugins = new Dictionary<string, uint>();

		// Token: 0x04000015 RID: 21
		private Dictionary<Guid, EventDescription> cachedDescriptions = new Dictionary<Guid, EventDescription>(new RuntimeManager.GuidComparer());

		// Token: 0x04000016 RID: 22
		private List<RuntimeManager.AttachedInstance> attachedInstances = new List<RuntimeManager.AttachedInstance>(128);

		// Token: 0x04000017 RID: 23
		private bool listenerWarningIssued;

		// Token: 0x04000018 RID: 24
		private Rect windowRect = new Rect(10f, 10f, 300f, 100f);

		// Token: 0x04000019 RID: 25
		private string lastDebugText;

		// Token: 0x0400001A RID: 26
		private float lastDebugUpdate;

		// Token: 0x0400001B RID: 27
		public static bool[] HasListener = new bool[8];

		// Token: 0x0200000A RID: 10
		private struct LoadedBank
		{
			// Token: 0x0400001C RID: 28
			public Bank Bank;

			// Token: 0x0400001D RID: 29
			public int RefCount;
		}

		// Token: 0x0200000B RID: 11
		private class GuidComparer : IEqualityComparer<Guid>
		{
			// Token: 0x0600003A RID: 58 RVA: 0x000034E9 File Offset: 0x000018E9
			bool IEqualityComparer<Guid>.Equals(Guid x, Guid y)
			{
				return x.Equals(y);
			}

			// Token: 0x0600003B RID: 59 RVA: 0x000034F3 File Offset: 0x000018F3
			int IEqualityComparer<Guid>.GetHashCode(Guid obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x0200000C RID: 12
		private class AttachedInstance
		{
			// Token: 0x0400001E RID: 30
			public EventInstance instance;

			// Token: 0x0400001F RID: 31
			public Transform transform;

			// Token: 0x04000020 RID: 32
			public Rigidbody rigidBody;

			// Token: 0x04000021 RID: 33
			public Rigidbody2D rigidBody2D;
		}
	}
}
