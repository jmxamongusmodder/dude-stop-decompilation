using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public class PuzzleDogEatsShoes_Dog : Draggable, TransitionProcessor
{
	// Token: 0x060019A1 RID: 6561 RVA: 0x00060804 File Offset: 0x0005EC04
	private void Awake()
	{
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>(true))
		{
			spriteRenderer.material.SetFloat("_Angle", 1.5707964f);
			spriteRenderer.material.SetFloat("_Distance", 0f);
			spriteRenderer.material.SetFloat("_Left", 1f);
		}
	}

	// Token: 0x060019A2 RID: 6562 RVA: 0x00060870 File Offset: 0x0005EC70
	private void Start()
	{
		this.collisionsLeft = this.collisions;
		this.lastPosition = base.transform.position;
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		base.StartCoroutine(this.FlippingCoroutine());
	}

	// Token: 0x060019A3 RID: 6563 RVA: 0x000608AC File Offset: 0x0005ECAC
	private void Update()
	{
		this.RotateDog();
		this.CheckFacingDirection();
		this.CheckEatingDistance();
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x000608C0 File Offset: 0x0005ECC0
	public void showArrow()
	{
		if (this.allowArrow)
		{
			this.allowArrow = false;
			this.arrow.gameObject.SetActive(true);
		}
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x000608E8 File Offset: 0x0005ECE8
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			base.StartCoroutine(this.SceneChangingCoroutine(other.transform));
			Global.self.currPuzzle.GetComponent<AudioVoice_DogEatsShoe>().hitArrow();
			if (this.arrow.gameObject.activeInHierarchy)
			{
				this.arrow.GetComponent<Animator>().SetTrigger("hide");
			}
			this.allowArrow = false;
		}
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x00060964 File Offset: 0x0005ED64
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!this.dragged)
		{
			return;
		}
		if (other.transform == this.fridge)
		{
			this.CollideWithFridge(other);
		}
		else if (other.transform.tag == "SuccessCollider")
		{
			this.CollideWithSlipper();
		}
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x000609C0 File Offset: 0x0005EDC0
	private void CollideWithFridge(Collision2D other)
	{
		if (this.collisionsLeft == 0)
		{
			return;
		}
		if (Mathf.Abs(other.relativeVelocity.x) < this.requiredVelocity)
		{
			return;
		}
		this.collisionsLeft--;
		this.OnMouseUp();
		this.slipper.GetComponent<Rigidbody2D>().AddForce(this.slipperJumpForce * Vector2.up);
		this.inactiveSlipper.GetComponent<Rigidbody2D>().AddForce(this.slipperJumpForce * Vector2.up);
		base.body.AddForce(this.collisionForce * Vector2.right);
		this.fridge.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.fridge.GetComponent<Rigidbody2D>().AddForce(this.fridgeJump);
		if (this.collisionsLeft == 0)
		{
			this.fridge.gameObject.layer = LayerMask.NameToLayer("Back");
		}
		Audio.self.playOneShot("c7c99809-3485-4d82-8163-a13f30fa955b", 1f);
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x00060ACC File Offset: 0x0005EECC
	private void CollideWithSlipper()
	{
		this.slipper.GetChild(1).GetComponent<ParticleSystem>().Emit();
		this.slipper.GetComponent<SpriteRenderer>().enabled = false;
		this.slipper.GetComponent<Rigidbody2D>().simulated = false;
		this.slipper.GetComponent<Collider2D>().enabled = false;
		this.slipper.GetChild(0).gameObject.SetActive(true);
		foreach (Rigidbody2D rigidbody2D in this.slipper.GetChild(0).GetComponentsInChildren<Rigidbody2D>())
		{
			rigidbody2D.AddForce(new Vector2(UnityEngine.Random.Range(this.minExplosion, this.maxExplosion), UnityEngine.Random.Range(Mathf.Max(this.minExplosion, 0f), this.maxExplosion)));
		}
		base.transform.GetChild(0).gameObject.SetActive(false);
		base.transform.GetChild(1).gameObject.SetActive(true);
		this.OnMouseUp();
		this.slipperEaten = true;
		Audio.self.playOneShot("c2d14fe3-f65d-4b10-a06a-5961d9a41514", 1f);
		base.StartCoroutine(this.EndLevel());
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x00060BF7 File Offset: 0x0005EFF7
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.StartSound();
		base.OnMouseDown();
		this.FixRotationPosition();
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x00060C18 File Offset: 0x0005F018
	public override void OnMouseUp()
	{
		if (!base.enabled || !this.dragged)
		{
			return;
		}
		this.StopSound();
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
		if (base.transform.position.x > this.doorLine.position.x || (base.transform.position.x > this.doorThreshold.position.x && !this.facingLeft))
		{
			base.StartCoroutine(this.FinishingCoroutine());
		}
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x00060CC6 File Offset: 0x0005F0C6
	private void OnDisable()
	{
		this.StopSound();
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x00060CCE File Offset: 0x0005F0CE
	private void StartSound()
	{
		if (this.tongueOut)
		{
			Audio.self.playLoopSound("a797b8e0-de78-4a22-a4b2-f19a572c7fa7");
		}
		else
		{
			Audio.self.playLoopSound("83c1ca16-9cde-4629-ad3c-c06d2b86350d");
		}
	}

	// Token: 0x060019AD RID: 6573 RVA: 0x00060CFE File Offset: 0x0005F0FE
	private void StopSound()
	{
		if (!this.dragged)
		{
			return;
		}
		if (this.tongueOut)
		{
			Audio.self.stopLoopSound("a797b8e0-de78-4a22-a4b2-f19a572c7fa7", true);
		}
		else
		{
			Audio.self.stopLoopSound("83c1ca16-9cde-4629-ad3c-c06d2b86350d", true);
		}
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x00060D3C File Offset: 0x0005F13C
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

	// Token: 0x060019AF RID: 6575 RVA: 0x00060D90 File Offset: 0x0005F190
	private void CheckEatingDistance()
	{
		if (this.slipperEaten)
		{
			return;
		}
		float num = Vector2.Distance(this.slipper.position, base.transform.position);
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		if (num < this.mouthDistance)
		{
			if (!gameObject.activeInHierarchy)
			{
				this.tongueOut = true;
				gameObject.SetActive(true);
				Audio.self.stopLoopSound("83c1ca16-9cde-4629-ad3c-c06d2b86350d", true);
				Audio.self.playLoopSound("a797b8e0-de78-4a22-a4b2-f19a572c7fa7");
			}
		}
		else if (gameObject.activeInHierarchy)
		{
			this.tongueOut = false;
			gameObject.SetActive(false);
			Audio.self.playLoopSound("83c1ca16-9cde-4629-ad3c-c06d2b86350d");
			Audio.self.stopLoopSound("a797b8e0-de78-4a22-a4b2-f19a572c7fa7", true);
		}
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x00060E64 File Offset: 0x0005F264
	private IEnumerator SceneChangingCoroutine(Transform arrow)
	{
		float maxTime = this.sceneChange.keys[this.sceneChange.length - 1].time;
		float timer = 0f;
		Vector2 start = this.scene.position;
		Vector2 end = this.scene.position + Vector3.right * this.sceneOffset;
		this.door.gameObject.SetActive(true);
		arrow.gameObject.SetActive(false);
		this.OnMouseUp();
		this.dragEnabled = false;
		Global.self.canBePaused = false;
		yield return new WaitForSeconds(1f);
		Dictionary<Transform, Vector2> slipperPartVelocities = new Dictionary<Transform, Vector2>();
		this.fridge.GetComponent<Rigidbody2D>().isKinematic = true;
		foreach (Rigidbody2D rigidbody2D in this.slipper.GetComponentsInChildren<Rigidbody2D>())
		{
			slipperPartVelocities.Add(rigidbody2D.transform, rigidbody2D.velocity);
			rigidbody2D.isKinematic = true;
		}
		this.inactiveSlipper.GetComponent<Rigidbody2D>().isKinematic = true;
		while (timer != maxTime)
		{
			timer = Mathf.MoveTowards(timer, maxTime, Time.deltaTime);
			this.scene.position = Vector3.Lerp(start, end, this.sceneChange.Evaluate(timer));
			yield return null;
		}
		Global.self.canBePaused = true;
		this.dragEnabled = true;
		this.fridge.gameObject.SetActive(false);
		this.inactiveSlipper.gameObject.SetActive(false);
		foreach (SpriteRenderer spriteRenderer in this.slipper.GetComponentsInChildren<SpriteRenderer>())
		{
			if (!GeometryUtility.TestPlanesAABB(this.planes, spriteRenderer.bounds))
			{
				spriteRenderer.enabled = false;
			}
			else
			{
				spriteRenderer.GetComponent<Rigidbody2D>().isKinematic = false;
				spriteRenderer.GetComponent<Rigidbody2D>().velocity = slipperPartVelocities[spriteRenderer.transform];
			}
		}
		Vector2 pos = Camera.main.WorldToViewportPoint(this.doorLine.position);
		foreach (SpriteRenderer spriteRenderer2 in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer2.material.SetFloat("_Angle", 1.5707964f);
			spriteRenderer2.material.SetFloat("_Distance", 0f);
			spriteRenderer2.material.SetFloat("_Left", pos.x);
		}
		this.sceneChanged = true;
		yield break;
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x00060E88 File Offset: 0x0005F288
	private IEnumerator FinishingCoroutine()
	{
		this.dragEnabled = false;
		Global.self.canBePaused = false;
		base.GetComponent<Collider2D>().enabled = false;
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		while (base.transform.position.x < this.doorLine.position.x)
		{
			base.transform.position += Vector3.right * this.endMovementSpeed * Time.deltaTime;
			yield return null;
		}
		Global.self.canBePaused = true;
		PuzzleBarkAtNight_Dog_animation.showFlyingDog = new bool?(false);
		Global.LevelFailed(0f, true);
		AudioVoice_DogEatsShoe voice = Global.self.currPuzzle.GetComponent<AudioVoice_DogEatsShoe>();
		while (!voice.canExitPuzzle)
		{
			yield return null;
		}
		Global.self.gotoNextLevel(false, null);
		yield break;
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x00060EA4 File Offset: 0x0005F2A4
	private IEnumerator EndLevel()
	{
		if (this.arrow.gameObject.activeInHierarchy)
		{
			this.arrow.GetComponent<Animator>().SetTrigger("hide");
		}
		this.allowArrow = false;
		this.dragEnabled = false;
		PuzzleBarkAtNight_Dog_animation.showFlyingDog = new bool?(true);
		Global.LevelCompleted(0f, true);
		AudioVoice_DogEatsShoe voice = Global.self.currPuzzle.GetComponent<AudioVoice_DogEatsShoe>();
		voice.eatShoe();
		while (!voice.canExitPuzzle)
		{
			yield return null;
		}
		Global.self.gotoNextLevel(false, null);
		yield break;
	}

	// Token: 0x060019B3 RID: 6579 RVA: 0x00060EC0 File Offset: 0x0005F2C0
	public void TransitionUpdate()
	{
		if (!this.sceneChanged)
		{
			return;
		}
		Vector2 vector = Camera.main.WorldToViewportPoint(this.doorLine.position);
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.material.SetFloat("_Left", vector.x);
		}
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x00060F2C File Offset: 0x0005F32C
	private void FlipDog()
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

	// Token: 0x060019B5 RID: 6581 RVA: 0x0006105C File Offset: 0x0005F45C
	private IEnumerator FlippingCoroutine()
	{
		for (;;)
		{
			this.FlipDog();
			yield return null;
		}
		yield break;
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x00061078 File Offset: 0x0005F478
	private void FixRotationPosition()
	{
		if (base.transform.position == this.lastPosition)
		{
			return;
		}
		float num = Mathf.Abs(base.transform.position.x - this.lastPosition.x);
		float num2 = Mathf.Sign(base.transform.position.x - this.lastPosition.x);
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
		this.lastPosition = new Vector3(base.transform.position.x - num4, base.transform.position.y);
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x00061274 File Offset: 0x0005F674
	private void RotateDog()
	{
		if (this.dragged)
		{
			float num = (base.transform.position.x - this.lastPosition.x) / this.length;
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
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.startingZ, 0f, num2));
		}
		else
		{
			this.lastPosition = base.transform.position;
		}
	}

	// Token: 0x040017A5 RID: 6053
	[Header("Fridge collisions")]
	public Transform fridge;

	// Token: 0x040017A6 RID: 6054
	public int collisions;

	// Token: 0x040017A7 RID: 6055
	public float collisionForce;

	// Token: 0x040017A8 RID: 6056
	public Vector2 fridgeJump;

	// Token: 0x040017A9 RID: 6057
	public float requiredVelocity;

	// Token: 0x040017AA RID: 6058
	[Header("Slipper stuff")]
	public Transform slipper;

	// Token: 0x040017AB RID: 6059
	public Transform inactiveSlipper;

	// Token: 0x040017AC RID: 6060
	public float slipperJumpForce;

	// Token: 0x040017AD RID: 6061
	public float minExplosion = 1f;

	// Token: 0x040017AE RID: 6062
	public float maxExplosion = 10f;

	// Token: 0x040017AF RID: 6063
	private bool slipperEaten;

	// Token: 0x040017B0 RID: 6064
	private Plane[] planes;

	// Token: 0x040017B1 RID: 6065
	[Header("Scene change stuff")]
	public Transform scene;

	// Token: 0x040017B2 RID: 6066
	public float sceneOffset;

	// Token: 0x040017B3 RID: 6067
	public AnimationCurve sceneChange;

	// Token: 0x040017B4 RID: 6068
	public Transform door;

	// Token: 0x040017B5 RID: 6069
	public Transform doorLine;

	// Token: 0x040017B6 RID: 6070
	public Transform doorThreshold;

	// Token: 0x040017B7 RID: 6071
	public float endMovementSpeed;

	// Token: 0x040017B8 RID: 6072
	private bool sceneChanged;

	// Token: 0x040017B9 RID: 6073
	[Header("Dog stuff")]
	public float mouthDistance;

	// Token: 0x040017BA RID: 6074
	public float eatingDistance;

	// Token: 0x040017BB RID: 6075
	private int collisionsLeft;

	// Token: 0x040017BC RID: 6076
	private bool tongueOut;

	// Token: 0x040017BD RID: 6077
	[Header("Rotation stuff")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x040017BE RID: 6078
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x040017BF RID: 6079
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x040017C0 RID: 6080
	public float scaleTime = 0.3f;

	// Token: 0x040017C1 RID: 6081
	private Vector3 lastPosition;

	// Token: 0x040017C2 RID: 6082
	private float rotationTimer;

	// Token: 0x040017C3 RID: 6083
	private float startingZ;

	// Token: 0x040017C4 RID: 6084
	private bool facingLeft;

	// Token: 0x040017C5 RID: 6085
	private float scaleTimer;

	// Token: 0x040017C6 RID: 6086
	private float startingScale;

	// Token: 0x040017C7 RID: 6087
	[Space(20f)]
	public Transform arrow;

	// Token: 0x040017C8 RID: 6088
	private bool allowArrow = true;
}
