using System;
using System.Collections;

// Token: 0x020002E9 RID: 745
public class AudioVoice_Pack13 : AudioVoice
{
	// Token: 0x0600127B RID: 4731 RVA: 0x00028670 File Offset: 0x00026A70
	public override void setActive(bool on)
	{
		base.setActive(on);
		base.StartCoroutine(this.hideStart());
		if (!this.active)
		{
			return;
		}
		if (!this.getCup)
		{
			return;
		}
		this.canExit = false;
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.canExit = true;
		});
		this.voice.start(true);
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x000286EA File Offset: 0x00026AEA
	public void getAllCups()
	{
		this.getCup = true;
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x000286F4 File Offset: 0x00026AF4
	private IEnumerator hideStart()
	{
		while (base.GetComponent<PuzzleStats>().UIScreenCurr == null)
		{
			yield return null;
		}
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startButton.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x0002870F File Offset: 0x00026B0F
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		return this.canExit;
	}

	// Token: 0x04000F89 RID: 3977
	private bool getCup;

	// Token: 0x04000F8A RID: 3978
	private bool canExit = true;
}
