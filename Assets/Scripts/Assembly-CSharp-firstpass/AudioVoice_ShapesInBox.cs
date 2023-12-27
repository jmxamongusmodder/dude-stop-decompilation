using System;

public class AudioVoice_ShapesInBox : AudioVoiceReceive
{
	[Serializable]
	public class Lines
	{
		public bool firstIn;
		public StandaloneLevelVoice firstLine;
		public StandaloneLevelVoice secondInLine;
		public StandaloneLevelVoice secondMissLine;
	}

	public StandaloneLevelVoice openLine;
	public StandaloneLevelVoice[] wrongHoleLine;
	public Lines[] endings;
}
