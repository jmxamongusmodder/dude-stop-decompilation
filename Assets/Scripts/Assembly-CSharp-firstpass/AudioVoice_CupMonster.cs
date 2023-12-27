using System;

public class AudioVoice_CupMonster : AudioVoice
{
	[Serializable]
	public class Lines
	{
		public CupMonsterItems type;
		public StandaloneLevelVoice line;
	}

	public StandaloneLevelVoice onLoad;
	public StandaloneLevelVoice onFirstLine;
	public StandaloneLevelVoice onThirdLine;
	public Lines[] itemLines;
	public StandaloneLevelVoice endLine;
	public StandaloneLevelVoice endText;
}
