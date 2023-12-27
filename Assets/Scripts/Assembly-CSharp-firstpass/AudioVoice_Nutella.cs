using UnityEngine;

public class AudioVoice_Nutella : AudioVoiceReceive
{
	public StandaloneLevelVoice booLine;
	public StandaloneLevelVoice[] spreadedLines;
	public StandaloneLevelVoice[] snapLines;
	public StandaloneLevelVoice knifeEnd;
	public StandaloneLevelVoice wait1;
	public StandaloneLevelVoice wait2;
	public StandaloneLevelVoice wait3;
	public Transform breadParent;
	public Transform breadSprite;
	public float breadLeftX;
	public float breadEndY;
	public float breadFlyTime;
	public GameObject breadLog;
}
