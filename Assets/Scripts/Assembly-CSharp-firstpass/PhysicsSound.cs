using UnityEngine;

public class PhysicsSound : MonoBehaviour
{
	public bool enable;
	public bool disableAutomaticEnable;
	public bool printDebug;
	public bool drawDebug;
	public string hitSound;
	public string slidingSound;
	public string rollingSound;
	public float noSoundAfterClick;
	public float waitMax;
	public float minCollVelocity;
	public float liveTimeOfThePoint;
	public float maxDistForPrevCollPoint;
	public float maxAngleForPrevCollPoint;
	public float hitVolume;
	public float hitVolumeMax;
	public float hitVolumeOnDragg;
	public bool muteHit;
	public bool canSlide;
	public bool canSlideFast;
	public float minCollVelocitySlide;
	public float distBeforeSlide;
	public float distBeforeSlideOnObj;
	public float angleToNotSlide;
	public int pointAmountSlide;
	public float slideVolume;
	public float slideVolumeMax;
	public float slideVolumeOnDragg;
	public bool canRoll;
	public float minCollVelocityRoll;
	public float distBeforeRoll;
	public float distBeforeRollOnObj;
	public float angleToRoll;
	public float rollVolume;
	public float rollVolumeMax;
	public float rollVolumeOnDragg;
	public float speedToRoll;
	public float minRollPitch;
	public float maxRollPitch;
}
