using System;

// Token: 0x02000028 RID: 40
public class PuzzleCodePad_Number : PuzzleCodePad_Button
{
	// Token: 0x060000E8 RID: 232 RVA: 0x00009D52 File Offset: 0x00007F52
	protected override void ButtonDown()
	{
		this.display.Enter(this.number);
	}

	// Token: 0x04000165 RID: 357
	public int number;
}
