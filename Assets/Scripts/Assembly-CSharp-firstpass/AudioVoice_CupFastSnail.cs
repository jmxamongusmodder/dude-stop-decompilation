using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020002B1 RID: 689
public class AudioVoice_CupFastSnail : AudioVoice
{
	// Token: 0x060010E7 RID: 4327 RVA: 0x0001C5D0 File Offset: 0x0001A9D0
	public IEnumerator playVoice(StandaloneLevelVoice line)
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(line);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0001C5F2 File Offset: 0x0001A9F2
	public void subscribeToMarker(string markerName, Action callback)
	{
		this.callbackList.Add(markerName, callback);
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x0001C601 File Offset: 0x0001AA01
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (this.callbackList.ContainsKey(markerName))
		{
			this.callbackList[markerName]();
			this.callbackList.Remove(markerName);
		}
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x0001C63A File Offset: 0x0001AA3A
	public override void subsctibeToEnding(endTextControl item)
	{
		base.subscribeToMarkers(item, false);
	}

	// Token: 0x04000DE8 RID: 3560
	private Dictionary<string, Action> callbackList = new Dictionary<string, Action>();
}
