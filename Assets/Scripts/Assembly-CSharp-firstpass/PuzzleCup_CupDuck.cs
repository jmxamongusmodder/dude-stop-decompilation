using System;

// Token: 0x02000338 RID: 824
public class PuzzleCup_CupDuck : PuzzleCup
{
	// Token: 0x06001438 RID: 5176 RVA: 0x000342D5 File Offset: 0x000326D5
	protected override void PlayStarSound()
	{
		Audio.self.playOneShot("2a1f7d94-9fb3-496f-9b89-4c344f93afbf", 1f);
	}
}
