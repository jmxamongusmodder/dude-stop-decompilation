using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x0200030D RID: 781
public class VoiceLine
{
	// Token: 0x06001379 RID: 4985 RVA: 0x0002F92C File Offset: 0x0002DD2C
	public VoiceLine(StandaloneLevelVoiceGuid entry)
	{
		this.info = entry;
		this.voice = RuntimeManager.CreateInstance(this.info.guid);
		GCHandle value = GCHandle.Alloc(this, GCHandleType.Pinned);
		this.voice.setUserData(GCHandle.ToIntPtr(value));
		if (this.info.fmodName.Contains("event:/"))
		{
			Audio.self.audioDebug.getEventDescription(this.info.guid, out this.info.fmodName);
			if (VoiceLine.<> f__mg$cache0 == null)
			{
				VoiceLine.<> f__mg$cache0 = new EVENT_CALLBACK(VoiceLine.markerFound);
			}
			this.voice.setCallback(VoiceLine.<> f__mg$cache0, EVENT_CALLBACK_TYPE.STOPPED | EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
		}
		else
		{
			if (VoiceLine.<> f__mg$cache1 == null)
			{
				VoiceLine.<> f__mg$cache1 = new EVENT_CALLBACK(VoiceLine.markerFound);
			}
			this.voice.setCallback(VoiceLine.<> f__mg$cache1, EVENT_CALLBACK_TYPE.STOPPED | EVENT_CALLBACK_TYPE.CREATE_PROGRAMMER_SOUND | EVENT_CALLBACK_TYPE.DESTROY_PROGRAMMER_SOUND | EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600137A RID: 4986 RVA: 0x0002FA3F File Offset: 0x0002DE3F
	public bool isPaused
	{
		get
		{
			return this._isPaused;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x0600137B RID: 4987 RVA: 0x0002FA47 File Offset: 0x0002DE47
	public bool removed
	{
		get
		{
			return this._removed;
		}
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x0002FA50 File Offset: 0x0002DE50
	public bool UpdateIfAlive()
	{
		if (this._removed)
		{
			return false;
		}
		while (this.markFound.Count > 0)
		{
			this.callMarker(this.markFound[0]);
			this.markFound.RemoveAt(0);
		}
		if (this.stopFound)
		{
			this.callStopped();
			this.destroy();
			return false;
		}
		return true;
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x0002FAB8 File Offset: 0x0002DEB8
	public void start(bool showSubs = true)
	{
		if (this._removed)
		{
			return;
		}
		if (this.isStarted)
		{
			UnityEngine.Debug.LogError("Trying ot start voice for the second time!\nVoice: " + this.info.fmodName);
			return;
		}
		this.voice.start();
		this.isStarted = true;
		UIControl.addNewChatText("<color=green>Start voice:</color> <color=yellow>" + this.info.fmodName + "</color>");
		if (showSubs)
		{
			this.showSubtitles();
		}
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x0002FB35 File Offset: 0x0002DF35
	public void stop()
	{
		if (this._removed)
		{
			return;
		}
		this.voice.stop(STOP_MODE.IMMEDIATE);
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x0002FB50 File Offset: 0x0002DF50
	public void setParameter(float value)
	{
		if (this._removed)
		{
			return;
		}
		this.voice.setParameterValueByIndex(0, value);
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x0002FB6C File Offset: 0x0002DF6C
	public void setParameter(string name, float value)
	{
		if (this._removed)
		{
			return;
		}
		this.voice.setParameterValue(name, value);
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x0002FB88 File Offset: 0x0002DF88
	private void showSubtitles()
	{
		string subtitles = LevelVoice.getSubtitles(this.info.fmodName, Global.self.currLanguage, this.sentenceIndex);
		if (string.IsNullOrEmpty(subtitles))
		{
			UnityEngine.Debug.LogError("Subtitle wasn't found, or doesn't have this index.");
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"Subtitles for: ",
				this.info.fmodName,
				", guid: ",
				this.info.guid
			}));
			UnityEngine.Debug.LogError("Sentence Index: " + this.sentenceIndex);
		}
		UIControl.setSubtitles(subtitles);
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x0002FC2C File Offset: 0x0002E02C
	public void updateSubtitles(string markerName)
	{
		int num = -1;
		if (!VoiceLine.isMarkerForSubtitles(markerName, out num))
		{
			return;
		}
		if (num != -1)
		{
			this.sentenceIndex = num - 1;
		}
		if (markerName != "ps")
		{
			this.sentenceIndex++;
		}
		this.sentenceStart = this.getPosition() + 1;
		if (this.isPlaying() && !this._isPaused)
		{
			this.showSubtitles();
		}
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x0002FCA4 File Offset: 0x0002E0A4
	public static bool isMarkerForSubtitles(string name, out int index)
	{
		index = -1;
		if (name.Length > 1 && name[0] == 's')
		{
			string s = name.Substring(1);
			if (int.TryParse(s, out index))
			{
				return true;
			}
			index = -1;
		}
		return name == "SentenceStart" || name == "ss" || name == "ps";
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x0002FD18 File Offset: 0x0002E118
	private void destroy()
	{
		if (this._removed)
		{
			return;
		}
		this._removed = true;
		IntPtr value;
		this.voice.getUserData(out value);
		GCHandle gchandle = GCHandle.FromIntPtr(value);
		this.voice.release();
		gchandle.Free();
		Audio.self.removeVoice(this);
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x0002FD6B File Offset: 0x0002E16B
	public bool isPlaying()
	{
		return !this._removed && this.isStarted;
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x0002FD80 File Offset: 0x0002E180
	public int getPosition()
	{
		if (this._removed)
		{
			return -1;
		}
		int result;
		this.voice.getTimelinePosition(out result);
		return result;
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x0002FDA9 File Offset: 0x0002E1A9
	public void gotoPosition(int newPos)
	{
		if (this._removed)
		{
			return;
		}
		this.voice.setTimelinePosition(newPos);
		if (newPos <= 0)
		{
			this.sentenceIndex = 0;
			this.showSubtitles();
		}
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x0002FDD8 File Offset: 0x0002E1D8
	public void gotoSentenceStart()
	{
		if (this._removed)
		{
			return;
		}
		this.voice.setTimelinePosition(this.sentenceStart);
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x0002FDF8 File Offset: 0x0002E1F8
	public void pause()
	{
		if (this._removed || this._isPaused)
		{
			return;
		}
		this.voice.setPaused(true);
		this._isPaused = true;
		UIControl.addNewChatText("<color=orange>Pause voice:</color> <color=yellow>" + this.info.fmodName + "</color>");
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x0002FE50 File Offset: 0x0002E250
	public void unPause(bool showSubs = true)
	{
		if (!this._removed && !this.isStarted)
		{
			this.start(showSubs);
			return;
		}
		if (this._removed || !this._isPaused)
		{
			return;
		}
		this.voice.setPaused(false);
		this._isPaused = false;
		UIControl.addNewChatText("<color=orange>Unpause voice:</color> <color=yellow>" + this.info.fmodName + "</color>");
		if (showSubs)
		{
			this.showSubtitles();
		}
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x0002FED4 File Offset: 0x0002E2D4
	public void subscribeToMarker(UnityEngine.Object source, Action<VoiceLine, string> callback)
	{
		if (this._removed)
		{
			return;
		}
		foreach (ActionWithSource actionWithSource in this.callbackForMarker)
		{
			if (source.GetInstanceID() == actionWithSource.source.GetInstanceID() && callback.Method.Name == actionWithSource.actionString.Method.Name)
			{
				UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					"Object ",
					source.name,
					" already subsctibed to this voice on a ",
					callback.Method.Name,
					"\nIt's not fatal error. Just inform Patomkin about it.\nBank: ",
					this.info.bankName,
					" id: ",
					this.info.levelVoiceId
				}));
				return;
			}
		}
		this.callbackForMarker.Add(new ActionWithSource(source, callback));
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x0002FFEC File Offset: 0x0002E3EC
	public void subscribeToStopped(UnityEngine.Object source, Action<VoiceLine> callback)
	{
		if (this._removed)
		{
			return;
		}
		foreach (ActionWithSource actionWithSource in this.callbackForStopped)
		{
			if (source.GetInstanceID() == actionWithSource.source.GetInstanceID() && callback.Method.Name == actionWithSource.action.Method.Name)
			{
				UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					"Object ",
					source.name,
					" already subsctibed to this voice on a ",
					callback.Method.Name,
					"\nIt's not fatal error. Just inform Patomkin about it.\nBank: ",
					this.info.bankName,
					" id: ",
					this.info.levelVoiceId
				}));
				return;
			}
		}
		this.callbackForStopped.Add(new ActionWithSource(source, callback));
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x00030104 File Offset: 0x0002E504
	public void unsubscribeFromAll(UnityEngine.Object source)
	{
		if (this._removed)
		{
			return;
		}
		foreach (ActionWithSource actionWithSource in this.callbackForMarker)
		{
			if (actionWithSource.source == source)
			{
				this.callbackForMarker.Remove(actionWithSource);
				break;
			}
		}
		foreach (ActionWithSource actionWithSource2 in this.callbackForStopped)
		{
			if (actionWithSource2.source == source)
			{
				this.callbackForStopped.Remove(actionWithSource2);
				break;
			}
		}
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x000301F0 File Offset: 0x0002E5F0
	private void callMarker(string markerName)
	{
		foreach (ActionWithSource actionWithSource in this.callbackForMarker)
		{
			actionWithSource.runAction(this, markerName);
		}
		this.updateSubtitles(markerName);
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x00030254 File Offset: 0x0002E654
	private void callStopped()
	{
		foreach (ActionWithSource actionWithSource in this.callbackForStopped)
		{
			actionWithSource.runAction(this, null);
		}
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x000302B4 File Offset: 0x0002E6B4
	private static RESULT markerFound(EVENT_CALLBACK_TYPE type, EventInstance eventInstance, IntPtr parameters)
	{
		try
		{
			EventInstance eventInstance2 = default(EventInstance);
			eventInstance2.handle = eventInstance.handle;
			IntPtr intPtr;
			eventInstance2.getUserData(out intPtr);
			if (intPtr == IntPtr.Zero)
			{
				UnityEngine.Debug.LogError("WHAT?");
				return RESULT.ERR_EVENT_NOTFOUND;
			}
			VoiceLine voiceLine = (VoiceLine)GCHandle.FromIntPtr(intPtr).Target;
			if (voiceLine.removed)
			{
				UnityEngine.Debug.LogError("Some Voice is playing, but isn't in the list (3)");
				return RESULT.OK;
			}
			if (type != EVENT_CALLBACK_TYPE.STOPPED)
			{
				if (type != EVENT_CALLBACK_TYPE.CREATE_PROGRAMMER_SOUND)
				{
					if (type != EVENT_CALLBACK_TYPE.DESTROY_PROGRAMMER_SOUND)
					{
						if (type == EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
						{
							TIMELINE_MARKER_PROPERTIES timeline_MARKER_PROPERTIES = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(TIMELINE_MARKER_PROPERTIES));
							int num;
							if (!VoiceLine.isMarkerForSubtitles(timeline_MARKER_PROPERTIES.name, out num))
							{
								voiceLine.markFound.Insert(0, timeline_MARKER_PROPERTIES.name);
							}
							else
							{
								voiceLine.markFound.Add(timeline_MARKER_PROPERTIES.name);
							}
						}
					}
					else
					{
						PROGRAMMER_SOUND_PROPERTIES programmer_SOUND_PROPERTIES = (PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(PROGRAMMER_SOUND_PROPERTIES));
						new Sound
						{
							handle = programmer_SOUND_PROPERTIES.sound
						}.release();
					}
				}
				else
				{
					PROGRAMMER_SOUND_PROPERTIES programmer_SOUND_PROPERTIES2 = (PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(PROGRAMMER_SOUND_PROPERTIES));
					SOUND_INFO sound_INFO;
					RESULT soundInfo = RuntimeManager.StudioSystem.getSoundInfo(voiceLine.info.fmodName, out sound_INFO);
					Sound sound;
					if (soundInfo != RESULT.OK)
					{
						UnityEngine.Debug.LogError(string.Concat(new object[]
						{
							"Can't find this sound in AudioTable: ",
							voiceLine.info.fmodName,
							"\nIn Event: ",
							voiceLine.info.guid,
							";"
						}));
					}
					else if (RuntimeManager.LowlevelSystem.createSound(sound_INFO.name_or_data, sound_INFO.mode, ref sound_INFO.exinfo, out sound) == RESULT.OK)
					{
						programmer_SOUND_PROPERTIES2.sound = sound.handle;
						programmer_SOUND_PROPERTIES2.subsoundIndex = sound_INFO.subsoundindex;
						Marshal.StructureToPtr(programmer_SOUND_PROPERTIES2, parameters, false);
					}
				}
			}
			else
			{
				voiceLine.stopFound = true;
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("THIS IS SCARY: " + ex.ToString());
		}
		return RESULT.OK;
	}

	// Token: 0x04001051 RID: 4177
	public readonly StandaloneLevelVoiceGuid info;

	// Token: 0x04001052 RID: 4178
	private EventInstance voice;

	// Token: 0x04001053 RID: 4179
	private List<ActionWithSource> callbackForMarker = new List<ActionWithSource>();

	// Token: 0x04001054 RID: 4180
	private List<ActionWithSource> callbackForStopped = new List<ActionWithSource>();

	// Token: 0x04001055 RID: 4181
	private bool stopFound;

	// Token: 0x04001056 RID: 4182
	private List<string> markFound = new List<string>();

	// Token: 0x04001057 RID: 4183
	private int sentenceStart = -1;

	// Token: 0x04001058 RID: 4184
	private int sentenceIndex;

	// Token: 0x04001059 RID: 4185
	private bool _isPaused;

	// Token: 0x0400105A RID: 4186
	private bool isStarted;

	// Token: 0x0400105B RID: 4187
	private bool _removed;

	// Token: 0x0400105C RID: 4188
	[CompilerGenerated]
	private static EVENT_CALLBACK<> f__mg$cache0;

	// Token: 0x0400105D RID: 4189
	[CompilerGenerated]
	private static EVENT_CALLBACK<> f__mg$cache1;
}
