using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A9 RID: 681
public class AudioVoice_CodePad : AudioVoiceDefault
{
	// Token: 0x060010AD RID: 4269 RVA: 0x0001A68C File Offset: 0x00018A8C
	protected override void setActiveDefault()
	{
		if (Global.self.endCutscenePack12)
		{
			this.voice = Audio.self.playVoice(LevelVoice.getVoice(this.ENDLines, LevelVoice.Type.Start, null));
			this.voice.start(true);
		}
		else
		{
			base.setActiveDefault();
		}
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0001A6E4 File Offset: 0x00018AE4
	public void easyPassword()
	{
		if (Global.self.endCutscenePack12)
		{
			return;
		}
		if (!base.playVoice(this.easyLine, true, true))
		{
			base.playVoice(this.noLine, false, false);
		}
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0001A718 File Offset: 0x00018B18
	public void setPassword(string pswrd)
	{
		this.password = pswrd;
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0001A724 File Offset: 0x00018B24
	public override void subsctibeToEnding(endTextControl item)
	{
		if (Global.self.endCutscenePack12)
		{
			base.StartCoroutine(this.DelayedEnd(item));
			return;
		}
		string text = this.password;
		if (text != null)
		{
			if (text == "8008" || text == "80085")
			{
				base.playSpecificEnd(this.end80085, item);
				return;
			}
			if (text == "1337" || text == "31337")
			{
				base.playSpecificEnd(this.end1337, item);
				return;
			}
		}
		AudioVoice_CodePad.playersAge = DateTime.Today.Year - int.Parse(this.password);
		if (100 > AudioVoice_CodePad.playersAge && AudioVoice_CodePad.playersAge > 5)
		{
			base.playSpecificEnd(this.endAge, item);
		}
		else
		{
			base.subsctibeToEnding(item);
		}
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0001A818 File Offset: 0x00018C18
	private IEnumerator DelayedEnd(endTextControl item)
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		base.playSpecificEnd(this.ENDLines, item, new bool?(true));
		yield break;
	}

	// Token: 0x04000DB8 RID: 3512
	[Space(10f)]
	public StandaloneLevelVoice ENDLines;

	// Token: 0x04000DB9 RID: 3513
	[Space(10f)]
	public StandaloneLevelVoice easyLine;

	// Token: 0x04000DBA RID: 3514
	public StandaloneLevelVoice noLine;

	// Token: 0x04000DBB RID: 3515
	public StandaloneLevelVoice end80085;

	// Token: 0x04000DBC RID: 3516
	public StandaloneLevelVoice end1337;

	// Token: 0x04000DBD RID: 3517
	public StandaloneLevelVoice endAge;

	// Token: 0x04000DBE RID: 3518
	private string password = string.Empty;

	// Token: 0x04000DBF RID: 3519
	public static int playersAge = 13;
}
