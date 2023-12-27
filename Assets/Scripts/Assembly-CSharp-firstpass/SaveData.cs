using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
	public bool corrupted;
	public List<SerializablePackSavedStats> packSavedStats;
	public List<LegoCupPiece> legoCupPieces;
	public List<SerializableJigsawPiece> jigsawPieces;
	public List<SerializablePuzzleStats> puzzleSavedStats;
	public int unlockedJigsawPieces;
	public SerializableGameStats gameStats;
	public string saveFileGUID;
	public uint totalPlayedTime;
}
