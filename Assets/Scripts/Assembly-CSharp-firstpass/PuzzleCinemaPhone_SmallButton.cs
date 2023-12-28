using System;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
public class PuzzleCinemaPhone_SmallButton : MonoBehaviour
{
	// Token: 0x06001949 RID: 6473 RVA: 0x0005DB1B File Offset: 0x0005BF1B
	private void Update()
	{
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x0005DB1D File Offset: 0x0005BF1D
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		Audio.self.playOneShot("675b79d8-000e-4984-9dd8-e24df8439b4e", 1f);
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x0005DB40 File Offset: 0x0005BF40
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.on = !this.on;
		base.GetComponent<SpriteRenderer>().color = ((!this.on) ? this.offColor : this.onColor);
		Audio.self.playOneShot("3295a9b2-026e-4df1-9613-4589e76a097f", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_CinemaPhone>().onOptionsChange();
	}

	// Token: 0x04001749 RID: 5961
	public Color onColor;

	// Token: 0x0400174A RID: 5962
	public Color offColor;

	// Token: 0x0400174B RID: 5963
	public bool on;
}
