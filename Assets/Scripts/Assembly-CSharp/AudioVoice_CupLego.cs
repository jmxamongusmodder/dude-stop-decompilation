using System;

// Token: 0x020002B6 RID: 694
public class AudioVoice_CupLego : AudioVoice_LegoCupSecond
{
	// Token: 0x06001108 RID: 4360 RVA: 0x0001D145 File Offset: 0x0001B545
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (this.voice != null)
		{
			this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.ended));
		}
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0001D171 File Offset: 0x0001B571
	private void ended(VoiceLine line)
	{
		base.GetComponent<PuzzleStats>().UIScreenSecondary.GetComponent<legoCupUI>().showSaveButton();
	}
}
