using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x020003DD RID: 989
public class PuzzleCatOnBlackDress : Draggable
{
	// Token: 0x060018E3 RID: 6371 RVA: 0x00059E9C File Offset: 0x0005829C
	private void Awake()
	{
		if (StoryMode_TransitionCat.self != null)
		{
			base.gameObject.SetActive(false);
		}
		this.particleArr = new ParticleSystem.Particle[this.particles.Length][];
		for (int i = 0; i < this.particles.Length; i++)
		{
			this.particleArr[i] = new ParticleSystem.Particle[this.expectedParticleCount];
		}
		this.UpdateParticlePosition();
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x00059F0A File Offset: 0x0005830A
	private void Start()
	{
		Audio.self.playLoopSound("a452f025-32e3-4272-858b-3c0f3c1896bf", base.transform);
		base.StartCoroutine(this.FlippingCoroutine());
		this.sleepingCoroutine = base.StartCoroutine(this.ShowZCoroutine());
		this.started = true;
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x00059F47 File Offset: 0x00058347
	private void Update()
	{
		this.RotateAnimal();
		this.UpdateParticlePosition();
		this.CheckOnClothesTimer();
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x00059F5B File Offset: 0x0005835B
	public void showArrow()
	{
		if (this.allowArrow)
		{
			this.arrow.SetActive(true);
		}
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x00059F74 File Offset: 0x00058374
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onClothes = true;
		}
		else if (other.tag == "FailCollider")
		{
			this.allowArrow = false;
			if (!(from x in base.GetComponents<Collider2D>()
			where x.enabled
			select x).FirstOrDefault<Collider2D>().isTrigger)
			{
				Global.LevelFailed(0f, true);
				base.StartCoroutine(this.EndLevel(false));
			}
		}
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x0005A010 File Offset: 0x00058410
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onClothes = false;
		}
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x0005A02E File Offset: 0x0005842E
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		Audio.self.playLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5", base.transform);
		base.OnMouseDown();
		this.FixRotationPosition();
		this.StandUp();
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x0005A064 File Offset: 0x00058464
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.dragged)
		{
			Audio.self.stopLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5", base.transform, true);
		}
		base.OnMouseUp();
		this.LayDown();
		if (this.onClothes)
		{
			this.allowArrow = false;
			Global.LevelCompleted(0f, true);
			base.StartCoroutine(this.EndLevel(true));
		}
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x0005A0D5 File Offset: 0x000584D5
	private void OnDisable()
	{
		if (!this.started)
		{
			return;
		}
		if (this.dragged)
		{
			Audio.self.stopLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5", base.transform, true);
		}
		if (this.sleeping)
		{
		}
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x0005A110 File Offset: 0x00058510
	private IEnumerator EndLevel(bool good)
	{
		if (this.arrow.gameObject.activeInHierarchy)
		{
			this.arrow.GetComponent<Animator>().SetTrigger("hide");
		}
		AudioVoice_CatOnBlackDress voice = Global.self.currPuzzle.GetComponent<AudioVoice_CatOnBlackDress>();
		if (!good)
		{
			this.facingLeft = false;
			this.dragged = false;
			float timer = 0f;
			this.climbCat.GetComponent<AttachToTheScreenSide>().CalculatePosition();
			Vector2 offset = base.transform.GetChild(0).localPosition;
			Vector2 startPos = base.transform.localPosition;
			Vector2 endPos = this.climbCat.transform.GetChild(0).position - offset;
			endPos = base.transform.parent.InverseTransformPoint(endPos);
			float startAngle = base.transform.eulerAngles.z;
			while (timer != this.rotationTime)
			{
				timer = Mathf.MoveTowards(timer, this.rotationTime, Time.deltaTime);
				float angle = Mathf.LerpAngle(startAngle, 0f, timer / this.rotationTime);
				base.transform.localPosition = Vector2.Lerp(startPos, endPos, timer / this.rotationTime);
				base.transform.rotation = Quaternion.Euler(0f, 0f, angle);
				this.UpdateParticlePosition();
				yield return null;
			}
			voice.wallClimb();
			while (!voice.canClimb)
			{
				yield return null;
			}
			this.climbCat.SetActive(true);
			base.gameObject.SetActive(false);
			this.particles.ToList<ParticleSystem>().ForEach(delegate(ParticleSystem x)
			{
				x.EnableEmmision(false);
			});
			if (SerializablePuzzleStats.Get(this.GetPuzzleStats().transform.name).playedTimes > 1)
			{
				this.climbCat.GetComponent<Animator>().SetTrigger("Short");
			}
			else
			{
				this.climbCat.GetComponent<Animator>().SetTrigger("Long");
			}
		}
		else
		{
			float timer2 = 0f;
			float startAngle2 = base.transform.eulerAngles.z;
			while (timer2 != this.rotationTime)
			{
				timer2 = Mathf.MoveTowards(timer2, this.rotationTime, Time.deltaTime);
				float angle2 = Mathf.LerpAngle(startAngle2, 0f, timer2 / this.rotationTime);
				base.transform.rotation = Quaternion.Euler(0f, 0f, angle2);
				yield return null;
			}
			voice.sleepOnDress();
			while (!voice.canExitPuzzle)
			{
				yield return null;
			}
			Global.self.gotoNextLevel(false, null);
		}
		yield break;
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x0005A132 File Offset: 0x00058532
	private void CheckOnClothesTimer()
	{
		if (this.onClothes)
		{
			this.onClothesTimer = Mathf.MoveTowards(this.onClothesTimer, this.secondsOnClothesToEnd, Time.deltaTime);
			if (this.onClothesTimer == this.secondsOnClothesToEnd)
			{
				this.OnMouseUp();
			}
		}
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x0005A174 File Offset: 0x00058574
	private void UpdateParticlePosition()
	{
		if (this.lastParticlePosition == base.transform.position)
		{
			return;
		}
		for (int i = 0; i < this.particles.Length; i++)
		{
			int num = this.particles[i].GetParticles(this.particleArr[i]);
			for (int j = 0; j < this.particleArr[i].Length; j++)
			{
				ParticleSystem.Particle[] array = this.particleArr[i];
				int num2 = j;
				array[num2].position = array[num2].position - (base.transform.position - this.lastParticlePosition);
			}
			for (int k = num; k < this.particleArr[i].Length; k++)
			{
				this.particleArr[i][k] = default(ParticleSystem.Particle);
			}
			this.particles[i].SetParticles(this.particleArr[i], this.particleArr[i].Length);
			this.particles[i].transform.position = base.transform.position;
			if (num > 4)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_CatOnBlackDress>().dropHair();
			}
		}
		this.lastParticlePosition = base.transform.position;
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x0005A2BC File Offset: 0x000586BC
	private IEnumerator ShowZCoroutine()
	{
		int zCount = 0;
		base.transform.GetChild(2).gameObject.SetActive(false);
		base.transform.GetChild(3).gameObject.SetActive(false);
		base.transform.GetChild(4).gameObject.SetActive(false);
		for (;;)
		{
			if (zCount == 3)
			{
				zCount = 0;
				base.transform.GetChild(2).gameObject.SetActive(false);
				base.transform.GetChild(3).gameObject.SetActive(false);
				base.transform.GetChild(4).gameObject.SetActive(false);
			}
			else
			{
				Transform child = base.transform.GetChild(2 + zCount);
				child.localScale = new Vector3(Mathf.Abs(child.localScale.x) * Mathf.Sign(base.transform.localScale.x), Mathf.Abs(child.localScale.y));
				child.gameObject.SetActive(true);
				zCount++;
			}
			yield return new WaitForSeconds(this.timeBetweenZ);
		}
		yield break;
	}

	// Token: 0x060018F0 RID: 6384 RVA: 0x0005A2D8 File Offset: 0x000586D8
	private void StandUp()
	{
		this.sleeping = false;
		base.StopCoroutine(this.sleepingCoroutine);
		this.sleepingCoroutine = null;
		foreach (Collider2D collider2D in base.GetComponents<Collider2D>())
		{
			if (collider2D.isTrigger)
			{
				collider2D.enabled = false;
			}
			else
			{
				collider2D.enabled = true;
			}
		}
		base.transform.GetChild(0).gameObject.SetActive(true);
		base.transform.GetChild(1).gameObject.SetActive(false);
		base.transform.GetChild(2).gameObject.SetActive(false);
		base.transform.GetChild(3).gameObject.SetActive(false);
		base.transform.GetChild(4).gameObject.SetActive(false);
		this.particles.ToList<ParticleSystem>().ForEach(delegate(ParticleSystem x)
		{
			x.EnableEmmision(true);
		});
		Audio.self.stopLoopSound("a452f025-32e3-4272-858b-3c0f3c1896bf", base.transform, true);
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x0005A3F4 File Offset: 0x000587F4
	private void LayDown()
	{
		this.sleeping = true;
		if (this.sleepingCoroutine == null)
		{
			this.sleepingCoroutine = base.StartCoroutine(this.ShowZCoroutine());
		}
		foreach (Collider2D collider2D in base.GetComponents<Collider2D>())
		{
			if (collider2D.isTrigger)
			{
				collider2D.enabled = true;
			}
			else
			{
				collider2D.enabled = false;
			}
		}
		base.transform.GetChild(0).gameObject.SetActive(false);
		base.transform.GetChild(1).gameObject.SetActive(true);
		Audio.self.playLoopSound("a452f025-32e3-4272-858b-3c0f3c1896bf", base.transform);
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x0005A4A8 File Offset: 0x000588A8
	private void FlipAnimal()
	{
		if (this.dragged)
		{
			if (Input.GetAxis("Mouse X") > 0f)
			{
				this.facingLeft = false;
			}
			else if (Input.GetAxis("Mouse X") < 0f)
			{
				this.facingLeft = true;
			}
		}
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

	// Token: 0x060018F3 RID: 6387 RVA: 0x0005A620 File Offset: 0x00058A20
	private IEnumerator FlippingCoroutine()
	{
		for (;;)
		{
			this.FlipAnimal();
			yield return null;
		}
		yield break;
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x0005A63C File Offset: 0x00058A3C
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

	// Token: 0x060018F5 RID: 6389 RVA: 0x0005A838 File Offset: 0x00058C38
	private void RotateAnimal()
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

	// Token: 0x040016E1 RID: 5857
	[Header("Cat stuff")]
	public ParticleSystem[] particles;

	// Token: 0x040016E2 RID: 5858
	public int expectedParticleCount = 50;

	// Token: 0x040016E3 RID: 5859
	public float timeBetweenZ;

	// Token: 0x040016E4 RID: 5860
	public float secondsOnClothesToEnd = 5f;

	// Token: 0x040016E5 RID: 5861
	private ParticleSystem.Particle[][] particleArr;

	// Token: 0x040016E6 RID: 5862
	private Vector3 lastParticlePosition;

	// Token: 0x040016E7 RID: 5863
	private bool onClothes;

	// Token: 0x040016E8 RID: 5864
	private bool sleeping = true;

	// Token: 0x040016E9 RID: 5865
	private Coroutine sleepingCoroutine;

	// Token: 0x040016EA RID: 5866
	private float onClothesTimer;

	// Token: 0x040016EB RID: 5867
	[Header("Rotation stuff")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x040016EC RID: 5868
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x040016ED RID: 5869
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x040016EE RID: 5870
	public float scaleTime = 0.3f;

	// Token: 0x040016EF RID: 5871
	private Vector3 lastPosition;

	// Token: 0x040016F0 RID: 5872
	private float rotationTimer;

	// Token: 0x040016F1 RID: 5873
	private float startingZ;

	// Token: 0x040016F2 RID: 5874
	private bool facingLeft;

	// Token: 0x040016F3 RID: 5875
	private float scaleTimer;

	// Token: 0x040016F4 RID: 5876
	private float startingScale;

	// Token: 0x040016F5 RID: 5877
	[Space(20f)]
	public GameObject arrow;

	// Token: 0x040016F6 RID: 5878
	private bool allowArrow = true;

	// Token: 0x040016F7 RID: 5879
	public GameObject climbCat;

	// Token: 0x040016F8 RID: 5880
	private bool started;
}
