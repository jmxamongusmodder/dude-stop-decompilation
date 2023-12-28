using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class PhysicsSound : MonoBehaviour
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06001728 RID: 5928 RVA: 0x0004A6BB File Offset: 0x00048ABB
	private Rigidbody2D body
	{
		get
		{
			if (this._body == null)
			{
				this._body = base.GetComponent<Rigidbody2D>();
			}
			return this._body;
		}
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x0004A6E0 File Offset: 0x00048AE0
	private void Start()
	{
		this.dragScript = base.transform.GetComponent<Draggable>();
		this.lastPos = base.transform.position;
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x0004A70C File Offset: 0x00048B0C
	private void OnDisable()
	{
		if (Audio.self)
		{
			if (this.canRoll)
			{
				Audio.self.stopLoopSound(this.rollingSound, base.transform, false);
			}
			if (this.canSlide)
			{
				Audio.self.stopLoopSound(this.slidingSound, base.transform, false);
			}
		}
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x0004A76C File Offset: 0x00048B6C
	private void Update()
	{
		this.noSoundAfterClickCurr = Mathf.MoveTowards(this.noSoundAfterClickCurr, 0f, Time.deltaTime);
		if (Input.GetMouseButtonDown(0) && this.noSoundAfterClick > 0f)
		{
			this.noSoundAfterClickCurr = this.noSoundAfterClick;
		}
		this.wait = Mathf.MoveTowards(this.wait, 0f, Time.deltaTime);
		this.playSliding();
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x0004A7DC File Offset: 0x00048BDC
	public virtual void OnCollisionEnter2D(Collision2D other)
	{
		this.OnCollisionStay2D(other);
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x0004A7E5 File Offset: 0x00048BE5
	private void OnCollisionExit2D(Collision2D other)
	{
		this.collisionCount = 0;
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x0004A7F0 File Offset: 0x00048BF0
	public virtual void OnCollisionStay2D(Collision2D other)
	{
		if (!this.enable)
		{
			return;
		}
		if (this.printDebug)
		{
			MonoBehaviour.print("----");
		}
		this.collisionCount = other.contacts.Length;
		this.velocity = other.relativeVelocity;
		if (this.velocity.magnitude < this.minCollVelocity)
		{
			return;
		}
		float num = 0f;
		foreach (ContactPoint2D contactPoint2D in other.contacts)
		{
			this.pointOnObj = this.body.GetPoint(contactPoint2D.point);
			bool flag = false;
			if (this.pointList.Count > 0)
			{
				float num2 = 0f;
				this.normalVertical = this.upVector * contactPoint2D.normal;
				this.normalVertical.x = Mathf.Abs(this.normalVertical.x);
				this.normalVertical.y = Mathf.Abs(this.normalVertical.y);
				int num3 = -1;
				float num4 = this.maxDistForPrevCollPoint;
				for (int j = 0; j < this.pointList.Count; j++)
				{
					this.objDelta = Vector2.Scale(this.lastPos - base.transform.position, this.normalVertical);
					this.pointDelta = this.body.GetRelativePoint(this.pointList[j].pointOnObj) - this.body.GetRelativePoint(this.pointOnObj);
					this.comparePoint = this.pointList[j].point - this.objDelta;
					if (this.canSlideFast)
					{
						this.comparePoint -= this.pointDelta;
					}
					num2 = Vector2.Distance(this.comparePoint, contactPoint2D.point);
					if (this.printDebug)
					{
						MonoBehaviour.print(string.Concat(new object[]
						{
							"cp: ",
							contactPoint2D.point,
							" lp: ",
							this.pointList[j].point,
							" com: ",
							this.comparePoint,
							" pos: ",
							base.transform.position,
							" lp: ",
							this.lastPos,
							" d: ",
							this.objDelta,
							" pd: ",
							this.pointDelta
						}));
					}
					if (this.printDebug)
					{
						MonoBehaviour.print(string.Concat(new object[]
						{
							num2 < num4,
							" ",
							this.pointList[j].time > Time.time - this.liveTimeOfThePoint,
							" ",
							!this.compareAngles(this.pointList[j].normal, contactPoint2D.normal, this.maxAngleForPrevCollPoint)
						}));
					}
					if (num2 < num4 && this.pointList[j].time > Time.time - this.liveTimeOfThePoint && !this.compareAngles(this.pointList[j].normal, contactPoint2D.normal, this.maxAngleForPrevCollPoint))
					{
						num3 = j;
						num4 = num2;
					}
				}
				if (num3 < 0)
				{
					flag = true;
				}
				else
				{
					float num5 = Vector2.Distance(this.pointList[num3].pointOnObj, this.pointOnObj);
					float num6 = Mathf.DeltaAngle(this.pointList[num3].angle, this.body.rotation);
					this.collisionPoint = this.pointList[num3];
					this.collisionPoint.point = contactPoint2D.point;
					this.collisionPoint.normal = contactPoint2D.normal;
					this.collisionPoint.time = Time.time;
					this.collisionPoint.angle = this.body.rotation;
					this.collisionPoint.distance = this.collisionPoint.distance + num2;
					this.collisionPoint.pointOnObj = this.pointOnObj;
					this.collisionPoint.distanceOnObj = this.collisionPoint.distanceOnObj + num5;
					this.collisionPoint.distanceAngle = this.collisionPoint.distanceAngle + num6;
					this.collisionPoint.pointCount = this.collisionPoint.pointCount + 1;
					this.pointList[num3] = this.collisionPoint;
					if (this.printDebug)
					{
						MonoBehaviour.print(string.Concat(new object[]
						{
							"p: ",
							contactPoint2D.point,
							" d: ",
							this.pointList[num3].distance,
							" do: ",
							this.pointList[num3].distanceOnObj,
							" pc: ",
							this.pointList[num3].pointCount,
							" an: ",
							this.pointList[num3].distanceAngle
						}));
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				if (this.drawDebug)
				{
					Debug.DrawRay(contactPoint2D.point, contactPoint2D.normal * 10f, Color.red, 1f);
				}
				if (this.printDebug)
				{
					MonoBehaviour.print("new pt: " + contactPoint2D.point);
				}
				this.pointList.Add(new CollisionPoint(contactPoint2D.point, contactPoint2D.normal, Time.time, this.pointOnObj, this.body.rotation, base.transform.position));
				num = Mathf.Max(num, this.velocity.magnitude);
			}
		}
		this.lastPos = base.transform.position;
		if (num != 0f && this.hitVolume != 0f && this.noSoundAfterClickCurr <= 0f)
		{
			num *= this.hitVolume;
			if (this.dragScript && this.dragScript.IsDragged())
			{
				if (this.wait > 0f)
				{
					return;
				}
				num *= this.hitVolumeOnDragg;
				this.wait = this.waitMax;
			}
			this.PlayHit(other, Mathf.Min(num, this.hitVolumeMax));
		}
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x0004AF2F File Offset: 0x0004932F
	protected virtual void PlayHit(Collision2D obj, float volume)
	{
		if (this.muteHit)
		{
			return;
		}
		Audio.self.playOneShot(this.hitSound, volume);
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x0004AF50 File Offset: 0x00049350
	private void playSliding()
	{
		bool flag = false;
		bool flag2 = false;
		Rigidbody2D component = base.GetComponent<Rigidbody2D>();
		float num = component.velocity.magnitude * 0.1f;
		float num2 = Mathf.Abs(component.angularVelocity * this.speedToRoll);
		for (int i = 0; i < this.pointList.Count; i++)
		{
			if (i > this.pointList.Count - 1)
			{
				break;
			}
			CollisionPoint collisionPoint = this.pointList[i];
			if (this.collisionCount > 0)
			{
				if (this.canSlide && num > this.minCollVelocitySlide && collisionPoint.distance > this.distBeforeSlide && collisionPoint.distanceOnObj > this.distBeforeSlideOnObj && collisionPoint.pointCount > this.pointAmountSlide && Mathf.Abs(collisionPoint.distanceAngle) < this.angleToNotSlide)
				{
					flag = true;
					this.slidingPlayedOnce = true;
					if (this.dragScript && this.dragScript.IsDragged())
					{
						if (this.slideVolumeOnDragg == 0f)
						{
							flag = false;
						}
						else
						{
							Audio.self.playLoopSound(this.slidingSound, base.transform, "Velocity", num);
						}
					}
					else
					{
						Audio.self.playLoopSound(this.slidingSound, base.transform, "Velocity", num);
					}
					if (this.drawDebug)
					{
						Debug.DrawRay(collisionPoint.point, collisionPoint.normal * 10f, Color.blue, 1f);
					}
				}
				if (this.canRoll && !flag && num > this.minCollVelocityRoll && collisionPoint.distance > this.distBeforeRoll && collisionPoint.distanceOnObj > this.distBeforeRollOnObj && Mathf.Abs(collisionPoint.distanceAngle) > this.angleToRoll)
				{
					flag2 = true;
					this.rollingPlayedOnce = true;
					num2 = Mathf.Max(num2, this.minRollPitch);
					num2 = Mathf.Min(num2, this.maxRollPitch);
					if (this.dragScript && this.dragScript.IsDragged())
					{
						if (this.rollVolumeOnDragg == 0f)
						{
							flag2 = false;
						}
						else
						{
							Audio.self.playLoopSound(this.slidingSound, base.transform, "Roll", num2);
						}
					}
					else
					{
						Audio.self.playLoopSound(this.slidingSound, base.transform, "Roll", num2);
					}
					if (this.drawDebug)
					{
						Debug.DrawRay(collisionPoint.point, collisionPoint.normal * 10f, Color.yellow, 1f);
					}
				}
			}
			if (collisionPoint.time < Time.time - this.liveTimeOfThePoint)
			{
				this.pointList.RemoveAt(i);
				i--;
			}
		}
		if (!flag && this.slidingPlayedOnce)
		{
			Audio.self.stopLoopSound(this.slidingSound, base.transform, false);
			this.slidingPlayedOnce = false;
		}
		if (!flag2 && this.rollingPlayedOnce)
		{
			Audio.self.stopLoopSound(this.rollingSound, base.transform, false);
			this.rollingPlayedOnce = false;
		}
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x0004B2C2 File Offset: 0x000496C2
	private bool compareAngles(Vector2 normalA, Vector2 normalB, float angle)
	{
		return Vector3.Angle(normalA, normalB) > angle;
	}

	// Token: 0x040014ED RID: 5357
	public bool enable = true;

	// Token: 0x040014EE RID: 5358
	public bool disableAutomaticEnable;

	// Token: 0x040014EF RID: 5359
	public bool printDebug;

	// Token: 0x040014F0 RID: 5360
	public bool drawDebug;

	// Token: 0x040014F1 RID: 5361
	[Header("Sound events from FMOD")]
	[EventRef]
	public string hitSound;

	// Token: 0x040014F2 RID: 5362
	[EventRef]
	public string slidingSound;

	// Token: 0x040014F3 RID: 5363
	[EventRef]
	public string rollingSound;

	// Token: 0x040014F4 RID: 5364
	[Header("General Parameters")]
	[Tooltip("DO not play hit sounds this long after click")]
	[Range(0f, 1f)]
	public float noSoundAfterClick = 0.05f;

	// Token: 0x040014F5 RID: 5365
	private float noSoundAfterClickCurr;

	// Token: 0x040014F6 RID: 5366
	[Tooltip("How often Hit sound can play")]
	[Range(0f, 0.5f)]
	public float waitMax = 0.2f;

	// Token: 0x040014F7 RID: 5367
	private float wait;

	// Token: 0x040014F8 RID: 5368
	[Tooltip("Trigger only when collison force is more then this")]
	[Range(0f, 0.5f)]
	public float minCollVelocity = 0.05f;

	// Token: 0x040014F9 RID: 5369
	[Tooltip("How long point can live, before it's removed from the memory")]
	[Range(0f, 1f)]
	public float liveTimeOfThePoint = 0.2f;

	// Token: 0x040014FA RID: 5370
	[Tooltip("How far from current collison point search for previously remembered one")]
	[Range(0f, 2f)]
	public float maxDistForPrevCollPoint = 0.8f;

	// Token: 0x040014FB RID: 5371
	[Tooltip("If collison normal is moved more then this - it's not same normal as previous one")]
	[Range(0f, 20f)]
	public float maxAngleForPrevCollPoint = 12f;

	// Token: 0x040014FC RID: 5372
	[Header("Hit Parameters")]
	[Tooltip("Volume of the hit sound")]
	[Range(0f, 50f)]
	public float hitVolume = 0.01f;

	// Token: 0x040014FD RID: 5373
	[Tooltip("Maximum volume of the hit sound in %")]
	[Range(0f, 1f)]
	public float hitVolumeMax = 0.6f;

	// Token: 0x040014FE RID: 5374
	[Tooltip("Lower volume by this much when object is dragged")]
	[Range(0f, 1f)]
	public float hitVolumeOnDragg = 0.3f;

	// Token: 0x040014FF RID: 5375
	[HideInInspector]
	public bool muteHit;

	// Token: 0x04001500 RID: 5376
	[Header("Slide Parameters")]
	[Tooltip("This object can slide")]
	public bool canSlide = true;

	// Token: 0x04001501 RID: 5377
	[Tooltip("Set true if object will be sliding really fast. Otherwise to false, to catch some hit sounds.")]
	public bool canSlideFast = true;

	// Token: 0x04001502 RID: 5378
	[Tooltip("Play slide if velocity is big enough")]
	[Range(0f, 0.5f)]
	public float minCollVelocitySlide = 0.05f;

	// Token: 0x04001503 RID: 5379
	[Tooltip("After object is moved this much - play sliding sound")]
	[Range(0f, 2f)]
	public float distBeforeSlide = 0.2f;

	// Token: 0x04001504 RID: 5380
	[Tooltip("After point ON the object is moved this much - play sliding sound (to know it's not a roll)")]
	[Range(0f, 2f)]
	public float distBeforeSlideOnObj = 0.2f;

	// Token: 0x04001505 RID: 5381
	[Tooltip("If object is rotated more than this number - it's not slide anymore.")]
	[Range(0f, 20f)]
	public float angleToNotSlide = 10f;

	// Token: 0x04001506 RID: 5382
	[Tooltip("How much collision points requeired to trigger slide")]
	[Range(0f, 10f)]
	public int pointAmountSlide = 4;

	// Token: 0x04001507 RID: 5383
	[Tooltip("Volume of the Slide sound")]
	[Range(0f, 0.5f)]
	public float slideVolume = 0.1f;

	// Token: 0x04001508 RID: 5384
	[Tooltip("Maximum volume of the Slide sound in %")]
	[Range(0f, 1f)]
	public float slideVolumeMax = 0.8f;

	// Token: 0x04001509 RID: 5385
	[Tooltip("Lower volume by this much when object is dragged")]
	[Range(0f, 1f)]
	public float slideVolumeOnDragg;

	// Token: 0x0400150A RID: 5386
	[Header("Roll Parameters")]
	[Tooltip("This object can roll")]
	public bool canRoll = true;

	// Token: 0x0400150B RID: 5387
	[Tooltip("Play roll if velocity is big enough")]
	[Range(0f, 0.5f)]
	public float minCollVelocityRoll = 0.05f;

	// Token: 0x0400150C RID: 5388
	[Tooltip("Object must be moved less than thsi to play rolling sound")]
	[Range(0f, 2f)]
	public float distBeforeRoll = 0.3f;

	// Token: 0x0400150D RID: 5389
	[Tooltip("Point ON the object should be moved less then this, to play rolling sound")]
	[Range(0f, 2f)]
	public float distBeforeRollOnObj = 0.2f;

	// Token: 0x0400150E RID: 5390
	[Tooltip("Rotate object this much to play rolling sound")]
	[Range(0f, 10f)]
	public float angleToRoll = 1f;

	// Token: 0x0400150F RID: 5391
	[Tooltip("Volume of the Roll sound")]
	[Range(0f, 10f)]
	public float rollVolume = 1f;

	// Token: 0x04001510 RID: 5392
	[Tooltip("Maximum volume of the Roll sound in %")]
	[Range(0f, 1f)]
	public float rollVolumeMax = 1f;

	// Token: 0x04001511 RID: 5393
	[Tooltip("Lower volume by this much when object is dragged")]
	[Range(0f, 1f)]
	public float rollVolumeOnDragg;

	// Token: 0x04001512 RID: 5394
	[Tooltip("How to convert rotation speed to pitch")]
	[Range(0f, 1f)]
	public float speedToRoll = 0.005f;

	// Token: 0x04001513 RID: 5395
	[Tooltip("Minimal rolling pitch")]
	[Range(0f, 1f)]
	public float minRollPitch = 0.1f;

	// Token: 0x04001514 RID: 5396
	[Tooltip("Maximal rolling pitch")]
	[Range(0f, 2f)]
	public float maxRollPitch = 1f;

	// Token: 0x04001515 RID: 5397
	private List<CollisionPoint> pointList = new List<CollisionPoint>();

	// Token: 0x04001516 RID: 5398
	private Vector2 lastPos;

	// Token: 0x04001517 RID: 5399
	private Draggable dragScript;

	// Token: 0x04001518 RID: 5400
	private int collisionCount;

	// Token: 0x04001519 RID: 5401
	private bool slidingPlayedOnce;

	// Token: 0x0400151A RID: 5402
	private bool rollingPlayedOnce;

	// Token: 0x0400151B RID: 5403
	private Vector2 velocity;

	// Token: 0x0400151C RID: 5404
	private Vector2 pointOnObj;

	// Token: 0x0400151D RID: 5405
	private Vector2 normalVertical;

	// Token: 0x0400151E RID: 5406
	private Vector2 objDelta;

	// Token: 0x0400151F RID: 5407
	private Vector2 pointDelta;

	// Token: 0x04001520 RID: 5408
	private Vector2 comparePoint;

	// Token: 0x04001521 RID: 5409
	private CollisionPoint collisionPoint;

	// Token: 0x04001522 RID: 5410
	private Quaternion upVector = Quaternion.Euler(0f, 0f, 90f);

	// Token: 0x04001523 RID: 5411
	private Rigidbody2D _body;
}
