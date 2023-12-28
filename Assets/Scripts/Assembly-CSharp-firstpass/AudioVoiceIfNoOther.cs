using System;
using UnityEngine;

// Token: 0x02000305 RID: 773
public class AudioVoiceIfNoOther : AudioVoice
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600134C RID: 4940 RVA: 0x0002F301 File Offset: 0x0002D701
	public static AudioVoiceIfNoOther self
	{
		get
		{
			if (AudioVoiceIfNoOther._self == null)
			{
				AudioVoiceIfNoOther._self = Audio.self.transform.GetComponent<AudioVoiceIfNoOther>();
			}
			return AudioVoiceIfNoOther._self;
		}
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x0002F32C File Offset: 0x0002D72C
	public void setPuzzleStats(PuzzleStats ps)
	{
		base.ps = ps;
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x0002F338 File Offset: 0x0002D738
	public override void subsctibeToEnding(endTextControl item)
	{
		string endText = LevelVoice.getEndText(base.ps.solvedAsBad == true, Global.self.currLanguage);
		item.SetEnding(endText, false);
	}

	// Token: 0x0400103C RID: 4156
	private static AudioVoiceIfNoOther _self;

	// Token: 0x0400103D RID: 4157
	[Header("Default entries")]
	public StandaloneLevelVoice DefaultLoad;
}
