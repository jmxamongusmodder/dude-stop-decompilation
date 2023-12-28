using System;
using FMOD.Studio;
using FMODUnity;

// Token: 0x02000298 RID: 664
public class AudioSnapshot
{
	// Token: 0x06001041 RID: 4161 RVA: 0x00014FFA File Offset: 0x000133FA
	public AudioSnapshot(string guid, MusicTypes type)
	{
		this.type = type;
		this.snapshot = RuntimeManager.CreateInstance(new Guid(guid));
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x0001501C File Offset: 0x0001341C
	public void SetActive(bool on, bool fade = true)
	{
		if (this.active == on || this.alwaysOn)
		{
			return;
		}
		if (on)
		{
			this.snapshot.start();
		}
		else if (fade)
		{
			this.snapshot.stop(STOP_MODE.ALLOWFADEOUT);
		}
		else
		{
			this.snapshot.stop(STOP_MODE.IMMEDIATE);
		}
		this.active = on;
	}

	// Token: 0x04000D4D RID: 3405
	private EventInstance snapshot;

	// Token: 0x04000D4E RID: 3406
	private bool active;

	// Token: 0x04000D4F RID: 3407
	public MusicTypes type;

	// Token: 0x04000D50 RID: 3408
	public bool alwaysOn;
}
