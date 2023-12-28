using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class Audio : MonoBehaviour
{
	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00013780 File Offset: 0x00011B80
	// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x000137CF File Offset: 0x00011BCF
	public static Audio self
	{
		get
		{
			if (Audio._self == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("Audio");
				if (gameObject == null)
				{
					Audio._self = null;
				}
				else
				{
					Audio._self = gameObject.GetComponent<Audio>();
				}
			}
			return Audio._self;
		}
		set
		{
			Audio._self = value;
		}
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x000137D8 File Offset: 0x00011BD8
	private void Awake()
	{
		this.loadBank(this.BankAlwaysLoaded, false);
		if (this.loadAllBanksInEditor)
		{
			this.loadBank(this.AllBankList, false);
		}
		this.voiceLineList = new List<VoiceLine>();
		this.audioDebug = base.GetComponent<AudioLiveDebug>();
		this.LoadVolume();
		this.InitializeMusic();
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x0001382D File Offset: 0x00011C2D
	private void Update()
	{
		this.updateVoice();
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x00013838 File Offset: 0x00011C38
	private void InitializeMusic()
	{
		this.StartMusic("4b4f2e0b-ba15-4c73-a792-849131845350");
		this.StartMusic("ab175daa-8759-4af9-b3b0-74df51ee0d24");
		this.snapshotList = new AudioSnapshot[5];
		this.snapshotList[0] = new AudioSnapshot("9779b1e0-8a72-4915-9f26-6e5090c5f2d7", MusicTypes.NoMusic);
		this.SetSnapshot(MusicTypes.NoMusic, true);
		this.snapshotList[0].alwaysOn = true;
		this.snapshotList[1] = new AudioSnapshot("ed4e7001-b093-44aa-ba10-470c02553089", MusicTypes.MenuMusic);
		this.snapshotList[2] = new AudioSnapshot("c5419b9c-58dc-4d57-8890-98780b7a3622", MusicTypes.InGameMusic);
		this.snapshotList[3] = new AudioSnapshot("90aa636c-8947-44e7-ac34-cb097af4396e", MusicTypes.Console);
		this.snapshotList[4] = new AudioSnapshot("81914ca0-fd5a-46ee-b984-765d96a2b18b", MusicTypes.Glitch);
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x000138E0 File Offset: 0x00011CE0
	public void StartMusic(string guid)
	{
		EventInstance value = RuntimeManager.CreateInstance(new Guid(guid));
		value.start();
		this.musicList.Add(guid, value);
		string str;
		if (this.audioDebug.getEventDescription(new Guid(guid), out str))
		{
			UIControl.addNewChatText("<color=white>Start music.</color> <color=yellow>" + str + "</color>");
		}
		else
		{
			UIControl.addNewChatText("<color=red>Can't start music: GUID doesn't exist: " + guid + " </color>");
		}
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x00013958 File Offset: 0x00011D58
	public void StopMusic(string guid)
	{
		if (!this.musicList.ContainsKey(guid))
		{
			return;
		}
		this.musicList[guid].stop(STOP_MODE.ALLOWFADEOUT);
		this.musicList.Remove(guid);
		string str;
		if (this.audioDebug.getEventDescription(new Guid(guid), out str))
		{
			UIControl.addNewChatText("<color=white>Stop music.</color> <color=yellow>" + str + "</color>");
		}
		else
		{
			UIControl.addNewChatText("<color=red>Can't stop music: GUID doesn't exist: " + guid + " </color>");
		}
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x000139E4 File Offset: 0x00011DE4
	public void PauseMusic(string guid, bool pause)
	{
		if (!this.musicList.ContainsKey(guid))
		{
			return;
		}
		this.musicList[guid].setPaused(pause);
		string str;
		if (this.audioDebug.getEventDescription(new Guid(guid), out str))
		{
			if (pause)
			{
				UIControl.addNewChatText("<color=white>Pause music.</color> <color=yellow>" + str + "</color>");
			}
			else
			{
				UIControl.addNewChatText("<color=white>Unpause music.</color> <color=yellow>" + str + "</color>");
			}
		}
		else
		{
			UIControl.addNewChatText("<color=red>Can't pause/unpause music: GUID doesn't exist: " + guid + " </color>");
		}
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x00013A80 File Offset: 0x00011E80
	public void RestartMusic(string guid)
	{
		if (!this.musicList.ContainsKey(guid))
		{
			return;
		}
		this.musicList[guid].setTimelinePosition(0);
		string str;
		if (this.audioDebug.getEventDescription(new Guid(guid), out str))
		{
			UIControl.addNewChatText("<color=white>Restart music.</color> <color=yellow>" + str + "</color>");
		}
		else
		{
			UIControl.addNewChatText("<color=red>Can't restart music: GUID doesn't exist: " + guid + " </color>");
		}
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x00013AFC File Offset: 0x00011EFC
	public void ChangeMusicParameter(string guid, string parameter, float value)
	{
		if (!this.musicList.ContainsKey(guid))
		{
			return;
		}
		this.musicList[guid].setParameterValue(parameter, value);
		string guid2;
		if (this.audioDebug.getEventDescription(new Guid(guid), out guid2))
		{
			UIControl.addNewChatText(string.Format("Set <color=#00ffffff>{0}</color>: <color=magenta>{1}</color> in ", parameter, value.ToString("F2")), guid2);
		}
		else
		{
			UIControl.addNewChatText("<color=red>Can't change parameter for Music: GUID doesn't exist: " + guid + " </color>");
		}
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x00013B84 File Offset: 0x00011F84
	public void ChangeMusicParameterOverTime(string guid, string parameter, float time)
	{
		if (this.musicParameterCoroutine != null)
		{
			UnityEngine.Debug.LogError("ERROR: Can't start new music paramter change while old is active!");
			return;
		}
		string guid2;
		if (this.audioDebug.getEventDescription(new Guid(guid), out guid2))
		{
			UIControl.addNewChatText(string.Format("Change <color=#00ffffff>{0}</color> from 0 to 1 over <color=magenta>{1}</color> sec in ", parameter, time.ToString("F0")), guid2);
		}
		this.musicParameterCoroutine = base.StartCoroutine(this.ChangeMusicParam(guid, parameter, time, 0f, 1f));
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x00013BFC File Offset: 0x00011FFC
	public void RevertMusicParameterOverTime(string guid, string parameter, float time)
	{
		if (this.musicParameterCoroutine != null)
		{
			base.StopCoroutine(this.musicParameterCoroutine);
		}
		if (this.musicParameterCurrent == 0f)
		{
			return;
		}
		string guid2;
		if (this.audioDebug.getEventDescription(new Guid(guid), out guid2))
		{
			UIControl.addNewChatText(string.Format("Change <color=#00ffffff>{0}</color> from {2} to 0 over <color=magenta>{1}</color> sec in ", parameter, time.ToString("F0"), this.musicParameterCurrent.ToString("F2")), guid2);
		}
		base.StartCoroutine(this.ChangeMusicParam(guid, parameter, time, this.musicParameterCurrent, 0f));
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x00013C91 File Offset: 0x00012091
	public void StopMusicParameterOverTime()
	{
		this.musicParameterChangeActive = false;
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x00013C9C File Offset: 0x0001209C
	private IEnumerator ChangeMusicParam(string guid, string parameter, float timeMax, float from, float to)
	{
		this.musicParameterChangeActive = true;
		float time = 0f;
		while (this.musicParameterChangeActive)
		{
			if (time != timeMax)
			{
				time = Mathf.MoveTowards(time, timeMax, Time.deltaTime);
				this.musicParameterCurrent = Mathf.Lerp(from, to, time / timeMax);
				this.ChangeMusicParameter(guid, parameter, this.musicParameterCurrent);
			}
			if (to == 0f && this.musicParameterCurrent == 0f)
			{
				break;
			}
			yield return null;
		}
		this.ChangeMusicParameter(guid, parameter, 0f);
		this.musicParameterCurrent = 0f;
		this.musicParameterCoroutine = null;
		yield break;
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x00013CDC File Offset: 0x000120DC
	public void StartSoloSnapshot(MusicTypes type, bool fade = true)
	{
		this.StopMusicParameterOverTime();
		foreach (AudioSnapshot audioSnapshot in this.snapshotList)
		{
			if (audioSnapshot.type == type)
			{
				audioSnapshot.SetActive(true, true);
				UIControl.addNewChatText("<color=purple>Start Snapshot (Turn OFF others):</color> <color=yellow>" + type.ToString() + "</color>");
			}
			else
			{
				audioSnapshot.SetActive(false, fade);
			}
		}
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x00013D50 File Offset: 0x00012150
	public void SetSnapshot(MusicTypes type, bool on)
	{
		this.snapshotList.First((AudioSnapshot x) => x.type == type).SetActive(on, true);
		if (on)
		{
			UIControl.addNewChatText("<color=purple>Start Snapshot:</color> <color=yellow>" + type.ToString() + "</color>");
		}
		else
		{
			UIControl.addNewChatText("<color=purple>Stop snapshot:</color> <color=yellow>" + type.ToString() + "</color>");
		}
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x00013DE0 File Offset: 0x000121E0
	public void loadBank(string[] list, bool unloadRest = true)
	{
		if (unloadRest)
		{
			this.UnloadBanks(this.BankLoaded.Except(list).ToArray<string>());
		}
		if (list.Count<string>() == 0)
		{
			return;
		}
		(from x in list
		where !this.BankLoaded.Contains(x)
		select x).ToList<string>().ForEach(delegate(string x)
		{
			RuntimeManager.LoadBank(x, false);
		});
		this.BankLoaded = this.BankLoaded.Union(list).ToList<string>();
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x00013E68 File Offset: 0x00012268
	public void UnloadBanks(string[] list)
	{
		if (list.Count<string>() == 0 || this.loadAllBanksInEditor)
		{
			return;
		}
		if (this.unloadingBanks != null)
		{
			UnityEngine.Debug.LogError("Trying to unload banks twice in a row - this could end badly!!");
			base.StopCoroutine(this.unloadingBanks);
		}
		this.unloadingBanks = base.StartCoroutine(this.UnloadBankDelayed(list));
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x00013EC0 File Offset: 0x000122C0
	private IEnumerator UnloadBankDelayed(string[] list)
	{
		while (this.voiceLineList.Count > 0)
		{
			yield return null;
		}
		list = (from x in list
		where !this.BankAlwaysLoaded.Contains(x) && this.BankLoaded.Contains(x)
		select x).ToArray<string>();
		list.ToList<string>().ForEach(delegate(string x)
		{
			RuntimeManager.UnloadBank(x);
		});
		this.BankLoaded = this.BankLoaded.Except(list).ToList<string>();
		this.unloadingBanks = null;
		yield break;
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x00013EE4 File Offset: 0x000122E4
	public void UnloadBank()
	{
		if (this.loadAllBanksInEditor)
		{
			return;
		}
		if (this.unloadingBank != null)
		{
			UnityEngine.Debug.LogError("Trying to unload banks twice in a row - this could end badly!!");
			base.StopCoroutine(this.unloadingBank);
		}
		this.unloadingBank = base.StartCoroutine(this.UnloadBankDelayed());
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x00013F30 File Offset: 0x00012330
	private IEnumerator UnloadBankDelayed()
	{
		while (this.voiceLineList.Count > 0)
		{
			yield return null;
		}
		(from x in this.BankLoaded
		where !this.BankAlwaysLoaded.Contains(x)
		select x).ToList<string>().ForEach(delegate(string x)
		{
			RuntimeManager.UnloadBank(x);
		});
		this.BankLoaded.RemoveAll((string x) => !this.BankAlwaysLoaded.Contains(x));
		this.unloadingBank = null;
		yield break;
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00013F4C File Offset: 0x0001234C
	public VoiceLine playVoice(StandaloneLevelVoice entry)
	{
		StandaloneLevelVoiceGuid voice = LevelVoice.getVoice(entry);
		return this.playVoice(voice);
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x00013F68 File Offset: 0x00012368
	public VoiceLine playVoice(StandaloneLevelVoiceGuid voice)
	{
		if (this.voiceLineList == null)
		{
			this.voiceLineList = new List<VoiceLine>();
		}
		if (voice == null || string.IsNullOrEmpty(voice.fmodName) || voice.guid == Guid.Empty)
		{
			UnityEngine.Debug.LogError("Audio.playVoice can't play empty voice");
			return null;
		}
		VoiceLine voiceLine = new VoiceLine(voice);
		this.voiceLineList.Add(voiceLine);
		return voiceLine;
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00013FD6 File Offset: 0x000123D6
	public void removeVoice(VoiceLine line)
	{
		this.voiceLineList.Remove(line);
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x00013FE8 File Offset: 0x000123E8
	private void updateVoice()
	{
		if (this.voiceLineList == null)
		{
			return;
		}
		for (int i = 0; i < this.voiceLineList.Count; i++)
		{
			if (i > this.voiceLineList.Count)
			{
				break;
			}
			if (this.voiceLineList[i] != null && !this.voiceLineList[i].UpdateIfAlive())
			{
				i--;
			}
		}
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0001405E File Offset: 0x0001245E
	private void OnApplicationQuit()
	{
		this.stopAllVoices();
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x00014068 File Offset: 0x00012468
	public void stopAllVoices()
	{
		if (this.voiceLineList == null)
		{
			return;
		}
		int count = this.voiceLineList.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			if (i < this.voiceLineList.Count)
			{
				if (this.voiceLineList[i] != null)
				{
					if (this.voiceLineList[i].isPlaying())
					{
						this.voiceLineList[i].stop();
					}
				}
			}
		}
		UIControl.self.hideSubtitles();
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x000140FE File Offset: 0x000124FE
	public void playLoopSound(string guidStr)
	{
		this.makeLoop(guidStr, null, string.Empty, 0f);
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x00014112 File Offset: 0x00012512
	public void playLoopSound(string guidStr, Transform obj)
	{
		this.makeLoop(guidStr, obj, string.Empty, 0f);
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x00014126 File Offset: 0x00012526
	public void playLoopSound(string guidStr, string paramName, float paramValue)
	{
		this.makeLoop(guidStr, null, paramName, paramValue);
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00014132 File Offset: 0x00012532
	public void playLoopSound(string guidStr, Transform obj, string paramName, float paramValue)
	{
		this.makeLoop(guidStr, obj, paramName, paramValue);
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x00014140 File Offset: 0x00012540
	private void makeLoop(string guidStr, Transform obj, string paramName, float paramValue)
	{
		if (this.soundVolume == 0f)
		{
			return;
		}
		if (string.IsNullOrEmpty(guidStr))
		{
			UIControl.addNewChatText("<color=red>GUID/PATH is empty. Can't start Loop sound</color>");
			return;
		}
		string text;
		Guid guid;
		if (!this.convertToGUID(guidStr, out text, out guid))
		{
			return;
		}
		EventInstance loop;
		if (!this.getExistingLoop(guid, obj, out loop))
		{
			loop = this.createLoop(guid, obj, text);
		}
		else
		{
			PLAYBACK_STATE playback_STATE;
			loop.getPlaybackState(out playback_STATE);
			if (playback_STATE != PLAYBACK_STATE.PLAYING && playback_STATE != PLAYBACK_STATE.STARTING)
			{
				loop.start();
				UIControl.addNewChatText("Start loop sound: ", text);
			}
		}
		this.changeLoopParameter(loop, text, paramName, paramValue);
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x000141D8 File Offset: 0x000125D8
	private void changeLoopParameter(EventInstance loop, string path, string paramName, float paramValue)
	{
		if (string.IsNullOrEmpty(paramName))
		{
			return;
		}
		if (loop.setParameterValue(paramName, paramValue) == RESULT.OK)
		{
			UIControl.addNewChatText(string.Format("Set <color=#00ffffff>{0}</color>: <color=magenta>{1}</color> in ", paramName, paramValue.ToString("F2")), path + " ");
		}
		else
		{
			UIControl.addNewChatText("<color=red>Param not found: " + paramName + " in </color>", path + "  ");
		}
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x0001424C File Offset: 0x0001264C
	private EventInstance createLoop(Guid guid, Transform obj, string path)
	{
		EventInstance eventInstance = RuntimeManager.CreateInstance(guid);
		eventInstance.set3DAttributes((Vector3.forward * 0.001f).To3DAttributes());
		eventInstance.start();
		this.loopingSounds.Add(this.combineToAKey(guid, obj), eventInstance);
		UIControl.addNewChatText("Start loop sound: ", path);
		return eventInstance;
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x000142A4 File Offset: 0x000126A4
	private bool getExistingLoop(Guid guid, Transform obj, out EventInstance loop)
	{
		string key = this.combineToAKey(guid, obj);
		if (this.loopingSounds.ContainsKey(key))
		{
			loop = this.loopingSounds[key];
			return true;
		}
		loop = default(EventInstance);
		return false;
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x000142E8 File Offset: 0x000126E8
	private string combineToAKey(Guid guid, Transform obj)
	{
		if (obj != null)
		{
			return obj.GetInstanceID().ToString() + guid.ToString();
		}
		return guid.ToString();
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x00014338 File Offset: 0x00012738
	private bool convertToGUID(string str, out string path, out Guid guid)
	{
		bool flag;
		if (str.Contains("/"))
		{
			flag = this.convertPathToGuid(str, out guid);
			path = str;
		}
		else
		{
			guid = new Guid(str);
			flag = this.audioDebug.getEventDescription(guid, out path);
		}
		if (!flag)
		{
			UIControl.addNewChatText(AudioLiveDebug.noAudioError(str));
			return false;
		}
		return true;
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x00014398 File Offset: 0x00012798
	private bool convertPathToGuid(string path, out Guid guid)
	{
		guid = Guid.Empty;
		EventDescription eventDescription;
		if (RuntimeManager.StudioSystem.getEvent(path, out eventDescription) == RESULT.OK)
		{
			eventDescription.getID(out guid);
			return true;
		}
		return false;
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x000143D1 File Offset: 0x000127D1
	public void stopLoopSound(string guidStr, bool fade = true)
	{
		this.stopLoopSound(guidStr, null, fade);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x000143DC File Offset: 0x000127DC
	public void stopLoopSound(string guidStr, Transform obj, bool fade = true)
	{
		if (string.IsNullOrEmpty(guidStr))
		{
			UIControl.addNewChatText("<color=red>GUID/PATH is empty. Can't stop Loop sound</color>");
			return;
		}
		string text;
		Guid guid;
		if (!this.convertToGUID(guidStr, out text, out guid))
		{
			return;
		}
		EventInstance eventInstance;
		if (!this.getExistingLoop(guid, obj, out eventInstance))
		{
			UIControl.addNewChatText("<color=red>Stop loop failed:</color> loop doesn't exist. ", text);
			UnityEngine.Debug.LogError("Loop stop failed: no such loop is playing: " + text);
		}
		else
		{
			PLAYBACK_STATE playback_STATE;
			eventInstance.getPlaybackState(out playback_STATE);
			if (playback_STATE != PLAYBACK_STATE.STOPPED && playback_STATE != PLAYBACK_STATE.STOPPING)
			{
				if (fade)
				{
					eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
				}
				else
				{
					eventInstance.stop(STOP_MODE.IMMEDIATE);
				}
				UIControl.addNewChatText("Stop loop " + ((!fade) ? "(Immidiate)" : "(Fade)") + " sound: ", text);
			}
			else
			{
				UIControl.addNewChatText("<color=red>Stop loop failed:</color> loop is already stopped/ended: ", text);
			}
		}
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x000144B0 File Offset: 0x000128B0
	public void resetLoopSounds()
	{
		foreach (KeyValuePair<string, EventInstance> keyValuePair in this.loopingSounds)
		{
			keyValuePair.Value.stop(STOP_MODE.ALLOWFADEOUT);
			keyValuePair.Value.release();
		}
		this.loopingSounds.Clear();
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x00014534 File Offset: 0x00012934
	public EventInstance playOneShot(string guidStr, float volume = 1f)
	{
		if (this.soundVolume == 0f)
		{
			return default(EventInstance);
		}
		if (string.IsNullOrEmpty(guidStr))
		{
			return default(EventInstance);
		}
		string guid;
		Guid guid2;
		if (!this.convertToGUID(guidStr, out guid, out guid2))
		{
			return default(EventInstance);
		}
		EventInstance result = RuntimeManager.CreateInstance(guid2);
		result.set3DAttributes((Vector3.forward * 0.001f).To3DAttributes());
		result.setVolume(volume);
		result.start();
		result.release();
		UIControl.addNewChatText("Play sound: ", guid);
		return result;
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x000145D8 File Offset: 0x000129D8
	private void LoadVolume()
	{
		float sound = this.LoadVolumeSlider(SaveLoad.getInt("SoundVolume", -1), this.soundVolume);
		float music = this.LoadVolumeSlider(SaveLoad.getInt("MusicVolume", -1), this.musicVolume);
		float voice = this.LoadVolumeSlider(SaveLoad.getInt("VoiceVolume", -1), this.voiceVolume);
		float master = this.LoadVolumeSlider(SaveLoad.getInt("MasterVolume", -1), this.masterVolume);
		this.SetVolume(master, voice, music, sound);
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x00014650 File Offset: 0x00012A50
	private float LoadVolumeSlider(int loaded, float def)
	{
		float value;
		if ((float)loaded == -1f)
		{
			value = def;
		}
		else
		{
			value = (float)loaded * 0.01f;
		}
		return Mathf.Clamp(value, 0f, 1f);
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0001468F File Offset: 0x00012A8F
	public void SetMasterVolume(float volume)
	{
		this.SetVolume(volume, this.voiceVolume, this.musicVolume, this.soundVolume);
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x000146AC File Offset: 0x00012AAC
	public void SetVolume(float master, float voice, float music, float sound)
	{
		this.musicVolume = music;
		this.soundVolume = sound;
		this.voiceVolume = voice;
		this.masterVolume = master;
		this.SetFMODbus("bus:/Master", this.masterVolume);
		if (this.masterVolume <= 0f)
		{
			sound = 0f;
			voice = 0f;
			music = 0f;
		}
		this.SetFMODbus("bus:/Master/MASTER VO", voice);
		this.SetFMODbus("bus:/Master/MASTER SFX", sound);
		this.SetFMODbus("bus:/Master/MASTER Music", music);
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x00014734 File Offset: 0x00012B34
	private void SetFMODbus(string busName, float volume)
	{
		RuntimeManager.GetBus(busName).setMute(volume <= 0f);
		RuntimeManager.GetBus(busName).setVolume(volume);
	}

	// Token: 0x04000D27 RID: 3367
	private static Audio _self;

	// Token: 0x04000D28 RID: 3368
	[Space(10f)]
	public bool muteVoiceInEditor;

	// Token: 0x04000D29 RID: 3369
	public bool loadAllBanksInEditor;

	// Token: 0x04000D2A RID: 3370
	[Range(0f, 1f)]
	public float soundVolume = 1f;

	// Token: 0x04000D2B RID: 3371
	[Range(0f, 1f)]
	public float musicVolume = 1f;

	// Token: 0x04000D2C RID: 3372
	[Range(0f, 1f)]
	public float voiceVolume;

	// Token: 0x04000D2D RID: 3373
	[Range(0f, 1f)]
	public float masterVolume;

	// Token: 0x04000D2E RID: 3374
	private const string BUS_MASTER = "bus:/Master";

	// Token: 0x04000D2F RID: 3375
	private const string BUS_MUSIC = "bus:/Master/MASTER Music";

	// Token: 0x04000D30 RID: 3376
	private const string BUS_SOUNDS = "bus:/Master/MASTER SFX";

	// Token: 0x04000D31 RID: 3377
	private const string BUS_VOICE = "bus:/Master/MASTER VO";

	// Token: 0x04000D32 RID: 3378
	private Dictionary<string, EventInstance> musicList = new Dictionary<string, EventInstance>();

	// Token: 0x04000D33 RID: 3379
	private AudioSnapshot[] snapshotList;

	// Token: 0x04000D34 RID: 3380
	[Header("Bank list")]
	[BankRef]
	public string[] AllBankList;

	// Token: 0x04000D35 RID: 3381
	[Tooltip("Always load this banks when the game starts and never remove them")]
	[BankRef]
	public string[] BankAlwaysLoaded;

	// Token: 0x04000D36 RID: 3382
	public List<string> BankLoaded = new List<string>();

	// Token: 0x04000D37 RID: 3383
	private LevelVoice voiceEntry;

	// Token: 0x04000D38 RID: 3384
	private EventInstance voiceClip;

	// Token: 0x04000D39 RID: 3385
	private List<VoiceLine> voiceLineList;

	// Token: 0x04000D3A RID: 3386
	[HideInInspector]
	public AudioLiveDebug audioDebug;

	// Token: 0x04000D3B RID: 3387
	[HideInInspector]
	public Dictionary<string, EventInstance> loopingSounds = new Dictionary<string, EventInstance>();

	// Token: 0x04000D3C RID: 3388
	private bool musicParameterChangeActive;

	// Token: 0x04000D3D RID: 3389
	private Coroutine musicParameterCoroutine;

	// Token: 0x04000D3E RID: 3390
	private float musicParameterCurrent;

	// Token: 0x04000D3F RID: 3391
	private Coroutine unloadingBanks;

	// Token: 0x04000D40 RID: 3392
	private Coroutine unloadingBank;
}
