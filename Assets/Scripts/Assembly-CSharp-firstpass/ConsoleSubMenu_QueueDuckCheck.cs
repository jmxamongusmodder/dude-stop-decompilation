using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000535 RID: 1333
public class ConsoleSubMenu_QueueDuckCheck : ConsoleSubMenuMultiple
{
	// Token: 0x06001E8D RID: 7821 RVA: 0x0008DA7C File Offset: 0x0008BE7C
	protected override IEnumerator showText()
	{
		AudioVoice_Queue audio = Global.self.currPuzzle.GetComponent<AudioVoice_Queue>();
		Vector2 typeSpeed = new Vector2(0.015f, 0.3f);
		Vector2 slowTypeSpeed = new Vector2(0.3f, 0.4f);
		yield return base.StartCoroutine(base.typeLine(this.list[0], typeSpeed));
		yield return base.StartCoroutine(base.showResponse(this.list[0]));
		yield return new WaitForSeconds(1f);
		audio.StartCoroutine(audio.playVoice(Voices.VoicePack07_Duck.Queue3_Wrong1));
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(base.typeLine(this.list[1], typeSpeed));
		yield return base.StartCoroutine(base.showResponse(this.list[1]));
		yield return new WaitForSeconds(0.5f);
		audio.StartCoroutine(audio.playVoice(Voices.VoicePack07_Duck.Queue3_Wrong2));
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(base.typeLine(this.list[2], slowTypeSpeed));
		yield return base.StartCoroutine(base.showResponse(this.list[2]));
		audio.closeConsole();
		yield return base.StartCoroutine(base.showResponse(this.list[3]));
		while (!audio.showLastLine)
		{
			yield return null;
		}
		yield return base.StartCoroutine(base.showResponse(this.list[4]));
		yield break;
	}
}
