using System;
using UnityEngine;

// Token: 0x020003EA RID: 1002
public class PuzzleCinemaPhone_Volume : MonoBehaviour
{
	// Token: 0x0600194D RID: 6477 RVA: 0x0005DBC0 File Offset: 0x0005BFC0
	private void Start()
	{
		this.volume = 2;
		this.zeroVolume = base.transform.GetChild(1);
		this.speaker = base.transform.GetChild(2);
		this.firstBar = this.speaker.GetChild(0);
		this.secondBar = this.speaker.GetChild(1);
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x0005DC1C File Offset: 0x0005C01C
	private void Update()
	{
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x0005DC20 File Offset: 0x0005C020
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.volumeChanged = true;
		this.volume = ++this.volume % 4;
		this.SetVolume(this.volume);
		Audio.self.playOneShot("3295a9b2-026e-4df1-9613-4589e76a097f", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_CinemaPhone>().onOptionsChange();
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x0005DC8E File Offset: 0x0005C08E
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		Audio.self.playOneShot("675b79d8-000e-4984-9dd8-e24df8439b4e", 1f);
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x0005DCB1 File Offset: 0x0005C0B1
	public bool IsOn()
	{
		return this.volume > 0;
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x0005DCBC File Offset: 0x0005C0BC
	public bool IsOff()
	{
		return this.volumeChanged && this.volume == 0;
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x0005DCD8 File Offset: 0x0005C0D8
	private void SetVolume(int volume)
	{
		switch (volume)
		{
		case 0:
			this.speaker.gameObject.SetActive(false);
			this.zeroVolume.gameObject.SetActive(true);
			break;
		case 1:
			this.zeroVolume.gameObject.SetActive(false);
			this.speaker.gameObject.SetActive(true);
			this.firstBar.GetComponent<SpriteRenderer>().color = this.offColor;
			this.secondBar.GetComponent<SpriteRenderer>().color = this.offColor;
			break;
		case 2:
			this.firstBar.GetComponent<SpriteRenderer>().color = this.onColor;
			break;
		case 3:
			this.secondBar.GetComponent<SpriteRenderer>().color = this.onColor;
			break;
		}
	}

	// Token: 0x0400174C RID: 5964
	public Color onColor;

	// Token: 0x0400174D RID: 5965
	public Color offColor;

	// Token: 0x0400174E RID: 5966
	private bool volumeChanged;

	// Token: 0x0400174F RID: 5967
	private int volume;

	// Token: 0x04001750 RID: 5968
	private Transform zeroVolume;

	// Token: 0x04001751 RID: 5969
	private Transform speaker;

	// Token: 0x04001752 RID: 5970
	private Transform firstBar;

	// Token: 0x04001753 RID: 5971
	private Transform secondBar;
}
