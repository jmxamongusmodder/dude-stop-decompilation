using System;

[Serializable]
public class SerializablePackSavedStats
{
	public SerializablePackSavedStats(string name)
	{
	}

	public string packName;
	public int completedTimes;
	public int solvedAsBad;
	public int solvedAsGood;
	public int jigSawPiecesFound;
	public bool packClickedOn;
	public bool packShowedOnce;
	public int badEndCount;
	public int goodEndCount;
}
