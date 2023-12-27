using UnityEngine;
using System.Collections.Generic;

public class Audio : MonoBehaviour
{
	public bool muteVoiceInEditor;
	public bool loadAllBanksInEditor;
	public float soundVolume;
	public float musicVolume;
	public float voiceVolume;
	public float masterVolume;
	public string[] AllBankList;
	public string[] BankAlwaysLoaded;
	public List<string> BankLoaded;
	public AudioLiveDebug audioDebug;
}
