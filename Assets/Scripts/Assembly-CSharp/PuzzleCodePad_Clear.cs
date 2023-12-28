using System;

// Token: 0x02000026 RID: 38
public class PuzzleCodePad_Clear : PuzzleCodePad_Button
{
	// Token: 0x060000D9 RID: 217 RVA: 0x00009798 File Offset: 0x00007998
	protected override void ButtonDown()
	{
		this.display.Clear();
	}
}
