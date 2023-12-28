using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003D7 RID: 983
public class PuzzleCatBreaksGlass_Cat : Draggable
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06001898 RID: 6296 RVA: 0x00056DD9 File Offset: 0x000551D9
	private float LeftTableLimit
	{
		get
		{
			return this.leftDropoffPoint - 0.1f;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06001899 RID: 6297 RVA: 0x00056DE7 File Offset: 0x000551E7
	private float RightTableLimit
	{
		get
		{
			return this.rightDropoffPoint + 0.1f;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x0600189A RID: 6298 RVA: 0x00056DF8 File Offset: 0x000551F8
	private float LeftWaterLimit
	{
		get
		{
			return this.water.position.x - this.waterLimitOffset;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600189B RID: 6299 RVA: 0x00056E20 File Offset: 0x00055220
	private float RightWaterLimit
	{
		get
		{
			return this.water.position.x + this.waterLimitOffset;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x0600189C RID: 6300 RVA: 0x00056E47 File Offset: 0x00055247
	private float LeftScreenLimit
	{
		get
		{
			return -this.screen.x + 1f;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x0600189D RID: 6301 RVA: 0x00056E5B File Offset: 0x0005525B
	private float RightScreenLimit
	{
		get
		{
			return this.screen.x - 0.6f;
		}
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x00056E70 File Offset: 0x00055270
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		GizmosExtension.DrawVerticalLine(this.water.position.x - this.waterLimitOffset, Color.blue);
		GizmosExtension.DrawVerticalLine(this.water.position.x + this.waterLimitOffset, Color.blue);
		GizmosExtension.DrawVerticalLine(this.leftDropoffPoint, Color.red);
		GizmosExtension.DrawVerticalLine(this.rightDropoffPoint, Color.red);
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x00056EEC File Offset: 0x000552EC
	private void Start()
	{
		this.lastPosition = base.transform.localPosition;
		this.screen = Camera.main.ViewportToWorldPoint(Vector2.one);
		this.limit.leftVal = this.LeftTableLimit;
		this.limit.rightVal = this.LeftWaterLimit;
		base.StartCoroutine(this.FlippingCoroutine());
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x00056F58 File Offset: 0x00055358
	private void Update()
	{
		this.CheckDropoffPosition();
		this.CheckFacingDirection();
		this.RotateAnimal();
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x00056F6C File Offset: 0x0005536C
	public void ShowArrow()
	{
		if (this.arrowHidden)
		{
			return;
		}
		this.arrow.gameObject.SetActive(true);
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x00056F8B File Offset: 0x0005538B
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		Audio.self.playLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5");
		base.OnMouseDown();
		this.FixRotationPosition();
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x00056FB4 File Offset: 0x000553B4
	public override void OnMouseUp()
	{
		if (base.enabled && this.dragged)
		{
			Audio.self.stopLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5", true);
		}
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x00056FF2 File Offset: 0x000553F2
	private void OnDisable()
	{
		if (this.dragged)
		{
			Audio.self.stopLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5", true);
		}
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x0005700F File Offset: 0x0005540F
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.Finish();
		}
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x0005702C File Offset: 0x0005542C
	protected override Vector3 ProcessMousePosition(Vector3 mouse, Vector3 delta)
	{
		if (this.water.localScale.x == 0f || this.droppedOff)
		{
			return mouse - delta;
		}
		bool flag = base.transform.position.x < this.water.position.x;
		float x = base.GetComponent<SpriteRenderer>().bounds.size.x;
		mouse -= delta;
		if (flag && mouse.x + x / 2f - 0.4f > this.water.position.x)
		{
			mouse.x = this.water.position.x - x / 2f + 0.16f;
		}
		else if (!flag && mouse.x - x / 2f + 0.4f < this.water.position.x)
		{
			mouse.x = this.water.position.x + x / 2f - 0.16f;
		}
		return mouse;
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x0005717E File Offset: 0x0005557E
	public void PushBack()
	{
		if (!this.activeCoroutine)
		{
			base.StartCoroutine(this.PushBackCoroutine());
			Audio.self.playLoopSound("0491a95d-71ae-496d-819e-a7e10c862169");
			Global.self.currPuzzle.GetComponent<AudioVoice_CatBreakGlass>().catHitWater();
		}
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x000571BC File Offset: 0x000555BC
	public void WaterIsOn()
	{
		if (this.droppedOff)
		{
			return;
		}
		if (base.transform.position.x > this.water.position.x && !this.jumping)
		{
			this.limit.leftVal = this.RightWaterLimit;
			this.limit.rightVal = this.RightTableLimit;
		}
		else
		{
			this.jumping = false;
			this.limit.leftVal = this.LeftTableLimit;
			this.limit.rightVal = this.LeftWaterLimit;
		}
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x0005725B File Offset: 0x0005565B
	public void WaterIsOff()
	{
		if (this.droppedOff)
		{
			return;
		}
		this.limit.leftVal = this.leftDropoffPoint - 0.1f;
		this.limit.rightVal = this.rightDropoffPoint + 0.1f;
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x00057298 File Offset: 0x00055698
	public void WaterJump()
	{
		Global.self.GetCup(AwardName.WET_CAT);
		this.jumping = true;
		if (!this.activeCoroutine)
		{
			base.StartCoroutine(this.WaterJumpCoroutine());
			Audio.self.playOneShot("0491a95d-71ae-496d-819e-a7e10c862169", 1f);
		}
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x000572E8 File Offset: 0x000556E8
	private IEnumerator PushBackCoroutine()
	{
		this.OnMouseUp();
		this.dragEnabled = false;
		float distance = (base.transform.position.x >= this.water.position.x) ? (-this.pushBackDistance) : this.pushBackDistance;
		Vector3 start = base.transform.position;
		Vector3 end = new Vector3(base.transform.position.x - distance, base.transform.position.y);
		this.activeCoroutine = true;
		this.coroutineTimer = 0f;
		yield return null;
		while (this.coroutineTimer != this.pushBackTime)
		{
			this.coroutineTimer = Mathf.MoveTowards(this.coroutineTimer, this.pushBackTime, Time.deltaTime);
			float t = Mathf.Pow(this.coroutineTimer / this.pushBackTime, 2f);
			base.transform.position = Vector3.Lerp(start, end, t);
			yield return null;
		}
		this.activeCoroutine = false;
		this.dragEnabled = true;
		yield break;
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x00057304 File Offset: 0x00055704
	private void CheckDropoffPosition()
	{
		if (!this.dragged || this.droppedOff)
		{
			return;
		}
		if (base.transform.position.x < this.leftDropoffPoint || base.transform.position.x > this.rightDropoffPoint)
		{
			base.StartCoroutine(this.DropoffCoroutine());
		}
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x00057374 File Offset: 0x00055774
	private IEnumerator DropoffCoroutine()
	{
		this.OnMouseUp();
		this.dragEnabled = false;
		this.activeCoroutine = true;
		this.facingLeft = false;
		while (!Mathf.Approximately(base.transform.localEulerAngles.z, 0f))
		{
			yield return null;
		}
		Audio.self.playOneShot("c338877d-13d5-4630-827b-57dc7d584a24", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_CatBreakGlass>().catJump();
		this.dropoffAnimation.position = base.transform.position;
		this.dropoffAnimation.gameObject.SetActive(true);
		base.gameObject.SetActive(false);
		this.droppedOff = true;
		yield break;
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x00057390 File Offset: 0x00055790
	public void DropoffFinished()
	{
		base.gameObject.SetActive(true);
		this.dragEnabled = true;
		this.activeCoroutine = false;
		this.facingLeft = false;
		this.scaleTimer = 0f;
		this.dropoffAnimation.gameObject.SetActive(false);
		base.transform.position = this.dropoffAnimation.GetChild(0).position;
		base.transform.localScale = Vector2.one * 0.8f;
		this.lastPosition = base.transform.localPosition;
		this.limit.leftVal = this.LeftScreenLimit;
		this.limit.rightVal = this.RightScreenLimit;
		base.StartCoroutine(this.FlippingCoroutine());
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x00057458 File Offset: 0x00055858
	private IEnumerator WaterJumpCoroutine()
	{
		this.OnMouseUp();
		this.dragEnabled = false;
		Vector3 start = base.transform.position;
		Vector3 end = new Vector3(base.transform.position.x - 3f * this.pushBackDistance, base.transform.position.y);
		this.activeCoroutine = true;
		this.coroutineTimer = 0f;
		yield return null;
		while (this.coroutineTimer != this.jumpTime)
		{
			this.coroutineTimer = Mathf.MoveTowards(this.coroutineTimer, this.jumpTime, Time.deltaTime);
			float t = this.coroutineTimer / this.jumpTime;
			Vector3 lerp = Vector3.Lerp(start, end, t);
			float height = Mathf.Cos((2f * this.coroutineTimer / this.jumpTime - 1f) * 3.1415927f * 0.5f) * this.jumpHeight;
			lerp.y += height;
			base.transform.position = lerp;
			yield return null;
		}
		this.activeCoroutine = false;
		this.dragEnabled = true;
		yield break;
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x00057473 File Offset: 0x00055873
	private void Finish()
	{
		if (this.glassShattered)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
		base.StartCoroutine(this.EndLevel());
	}

	// Token: 0x060018B1 RID: 6321 RVA: 0x000574A8 File Offset: 0x000558A8
	private IEnumerator EndLevel()
	{
		if (this.arrow.gameObject.activeInHierarchy)
		{
			this.arrow.GetComponent<Animator>().SetTrigger("hide");
		}
		this.arrowHidden = true;
		AudioVoice_CatBreakGlass voice = Global.self.currPuzzle.GetComponent<AudioVoice_CatBreakGlass>();
		voice.catExit();
		this.dragged = true;
		while (!voice.canExitPuzzle)
		{
			this.RotateAnimal();
			yield return null;
		}
		this.dragged = false;
		this.transitionCat.SetRotationPosition(this.lastPosition);
		this.transitionCat.showCat(base.transform);
		base.gameObject.SetActive(false);
		Global.self.transitionUseStoryModeCurve = true;
		Global.self.gotoNextLevel(false, null);
		yield break;
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x000574C4 File Offset: 0x000558C4
	private IEnumerator FlippingCoroutine()
	{
		for (;;)
		{
			this.FlipAnimal();
			yield return null;
		}
		yield break;
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x000574E0 File Offset: 0x000558E0
	private void CheckFacingDirection()
	{
		if (!this.dragged)
		{
			return;
		}
		if (Input.GetAxis("Mouse X") > 0f)
		{
			this.facingLeft = false;
		}
		else if (Input.GetAxis("Mouse X") < 0f)
		{
			this.facingLeft = true;
		}
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x00057534 File Offset: 0x00055934
	private void FlipAnimal()
	{
		if (((this.facingLeft && base.transform.localScale.x > 0f) || (!this.facingLeft && base.transform.localScale.x < 0f)) && this.scaleTimer == 0f)
		{
			this.scaleTimer = this.scaleTime;
			this.startingScale = base.transform.localScale.x;
		}
		else if (this.scaleTimer > 0f)
		{
			this.scaleTimer = Mathf.MoveTowards(this.scaleTimer, 0f, Time.deltaTime);
			float num = this.scaleTimer / this.scaleTime;
			num = Mathf.Cos(num * 3.1415927f * 0.5f);
			num = Mathf.Lerp(this.startingScale, -this.startingScale, num);
			base.transform.localScale = new Vector3(num, base.transform.localScale.y, base.transform.localScale.z);
		}
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x00057664 File Offset: 0x00055A64
	private void FixRotationPosition()
	{
		if (base.transform.localPosition == this.lastPosition)
		{
			return;
		}
		float num = Mathf.Abs(base.transform.localPosition.x - this.lastPosition.x);
		float num2 = Mathf.Sign(base.transform.localPosition.x - this.lastPosition.x);
		num %= 4f * this.length;
		if (num2 < 0f)
		{
			num = 4f - num;
		}
		float num3 = base.transform.localEulerAngles.z % this.amplitude / this.amplitude;
		if (num > 2f * this.length)
		{
			num3 = Mathf.Sign(num3) - num3;
		}
		float num4 = 2f * Mathf.Asin(num3) / 3.1415927f;
		if (num > 3f * this.length)
		{
			num4 = 4f * this.length - num4;
		}
		else if (num > 2f * this.length)
		{
			num4 += 2f * this.length;
		}
		else if (num > this.length)
		{
			num4 = 2f * this.length - num4;
		}
		if (float.IsNaN(num4))
		{
			Debug.Log(string.Format("({0}) % {1} ==> {2} .. {3} = {4}", new object[]
			{
				base.transform.localEulerAngles.z,
				this.amplitude,
				num3,
				num,
				num4
			}));
		}
		this.lastPosition = new Vector3(base.transform.localPosition.x - num4, base.transform.localPosition.y);
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x00057860 File Offset: 0x00055C60
	private void RotateAnimal()
	{
		if (this.dragged)
		{
			float num = (base.transform.localPosition.x - this.lastPosition.x) / this.length;
			float z = Mathf.Sin(num * 3.1415927f * 0.5f) * this.amplitude;
			base.transform.localRotation = Quaternion.Euler(0f, 0f, z);
			this.rotationTimer = 0f;
			this.startingZ = base.transform.localEulerAngles.z;
		}
		else if (this.rotationTimer != this.rotationTime)
		{
			this.rotationTimer = Mathf.MoveTowards(this.rotationTimer, this.rotationTime, Time.deltaTime);
			float num2 = this.rotationTimer / this.rotationTime;
			num2 = Mathf.Sin(num2 * 3.1415927f * 0.5f);
			if (Mathf.Approximately(num2, 1f))
			{
				base.transform.rotation = Quaternion.Euler(Vector3.zero);
			}
			else
			{
				base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.startingZ, 0f, num2));
			}
		}
		else
		{
			this.lastPosition = base.transform.localPosition;
		}
	}

	// Token: 0x0400168A RID: 5770
	[HideInInspector]
	public bool glassShattered;

	// Token: 0x0400168B RID: 5771
	[Header("Falling stuff")]
	public Transform dropoffAnimation;

	// Token: 0x0400168C RID: 5772
	public float leftDropoffPoint;

	// Token: 0x0400168D RID: 5773
	public float rightDropoffPoint;

	// Token: 0x0400168E RID: 5774
	public Transform arrow;

	// Token: 0x0400168F RID: 5775
	private bool arrowHidden;

	// Token: 0x04001690 RID: 5776
	private bool droppedOff;

	// Token: 0x04001691 RID: 5777
	private Vector2 screen;

	// Token: 0x04001692 RID: 5778
	private bool jumping;

	// Token: 0x04001693 RID: 5779
	[Header("Water stuff")]
	public Transform water;

	// Token: 0x04001694 RID: 5780
	public float waterLimitOffset;

	// Token: 0x04001695 RID: 5781
	public float pushBackDistance;

	// Token: 0x04001696 RID: 5782
	public float pushBackTime;

	// Token: 0x04001697 RID: 5783
	public float jumpTime;

	// Token: 0x04001698 RID: 5784
	public float jumpHeight;

	// Token: 0x04001699 RID: 5785
	private bool activeCoroutine;

	// Token: 0x0400169A RID: 5786
	private float coroutineTimer;

	// Token: 0x0400169B RID: 5787
	[Header("Rotation stuff")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x0400169C RID: 5788
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x0400169D RID: 5789
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x0400169E RID: 5790
	public float scaleTime = 0.3f;

	// Token: 0x0400169F RID: 5791
	private Vector3 lastPosition;

	// Token: 0x040016A0 RID: 5792
	private float rotationTimer;

	// Token: 0x040016A1 RID: 5793
	private float startingZ;

	// Token: 0x040016A2 RID: 5794
	private bool facingLeft;

	// Token: 0x040016A3 RID: 5795
	private float scaleTimer;

	// Token: 0x040016A4 RID: 5796
	private float startingScale;

	// Token: 0x040016A5 RID: 5797
	[Header("Transition cat")]
	public StoryMode_TransitionCat transitionCat;
}
