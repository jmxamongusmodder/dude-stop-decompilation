using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class AudioVoiceScrollable : AudioVoice
{
	// Token: 0x06001362 RID: 4962 RVA: 0x0001EA9B File Offset: 0x0001CE9B
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.onTransitionIn();
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x0001EAB6 File Offset: 0x0001CEB6
	public virtual bool onTransitionOut()
	{
		return true;
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x0001EAB9 File Offset: 0x0001CEB9
	public virtual void onTransitionIn()
	{
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x0001EABC File Offset: 0x0001CEBC
	protected bool playVoice(StandaloneLevelVoice[] list, bool overrideOld, bool oneTime, bool checkChance, bool pauseArrows = true)
	{
		if (!this.active)
		{
			return false;
		}
		if (Global.self.pack10CutsceneActive)
		{
			return false;
		}
		if (list.Length == 0)
		{
			Debug.LogError("ERROR: Asking to play a voice from en empty list?\nPuzzle: " + base.transform.name);
			return false;
		}
		if (checkChance)
		{
			int completedTimes = SerializablePackSavedStats.Get(Global.self.currentLevelPack).completedTimes;
			float num = Mathf.Max(-0.1f, 1f - (float)completedTimes * 0.15f);
			if (UnityEngine.Random.value > num)
			{
				return false;
			}
		}
		bool flag = false;
		if (this.voice != null && this.voice.isPlaying())
		{
			if (!overrideOld)
			{
				return false;
			}
			flag = true;
		}
		int num2;
		if (oneTime)
		{
			num2 = this.getNonRepeatingVoice(list);
		}
		else
		{
			num2 = UnityEngine.Random.Range(0, list.Length);
		}
		if (num2 == -1)
		{
			return false;
		}
		if (flag)
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(list[num2]);
		if (pauseArrows)
		{
			Global.PauseArrows(-1f);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				Global.UnpauseArrows();
			});
		}
		this.voice.start(true);
		return true;
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x0001EC0C File Offset: 0x0001D00C
	private int getNonRepeatingVoice(StandaloneLevelVoice[] list)
	{
		int num = -1;
		List<int> list2 = Enumerable.Range(0, list.Length).ToList<int>();
		for (;;)
		{
			if (num != -1)
			{
				list2.Remove(num);
			}
			if (list2.Count == 0)
			{
				break;
			}
			num = list2[UnityEngine.Random.Range(0, list2.Count)];
			if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(list[num].levelVoiceId))
			{
				return num;
			}
		}
		return -1;
	}
}
