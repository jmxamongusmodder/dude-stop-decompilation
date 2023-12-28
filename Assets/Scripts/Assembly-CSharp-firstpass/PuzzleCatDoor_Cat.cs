using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DB RID: 987
public class PuzzleCatDoor_Cat : Draggable
{
	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060018CD RID: 6349 RVA: 0x00058CD4 File Offset: 0x000570D4
	private bool byTheDoor
	{
		get
		{
			return base.transform.position.x > this.meowStartPosition;
		}
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x00058CFC File Offset: 0x000570FC
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		GizmosExtension.DrawVerticalLine(this.meowStartPosition, Color.cyan);
		GizmosExtension.DrawVerticalLine(this.maxPositionBeforeDoor, Color.blue);
		GizmosExtension.DrawVerticalLine(this.automaticExitPosition, Color.red);
		GizmosExtension.DrawVerticalLine(this.positionBeforeFlip, Color.green);
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x00058D4F File Offset: 0x0005714F
	private void Start()
	{
		this.onMeow = base.transform.Find("onMeow");
		this.limit.rightVal = this.maxPositionBeforeDoor;
		this.arrow.gameObject.SetActive(true);
	}

	// Token: 0x060018D0 RID: 6352 RVA: 0x00058D8C File Offset: 0x0005718C
	private void Update()
	{
		this.UpdateOnMeow();
		this.FlipCat();
		if (this.dragged)
		{
			if (Vector3.Distance(Input.mousePosition, this.mouseClickPosition) >= this.mouseClickDistance)
			{
				this.lockX = false;
				float num = (base.transform.localPosition.x - this.lastPosition.x) / this.length;
				float z = Mathf.Sin(num * 3.1415927f * 0.5f) * this.amplitude;
				base.transform.localRotation = Quaternion.Euler(0f, 0f, z);
				this.rotationTimer = 0f;
				this.startingZ = base.transform.localEulerAngles.z;
				if (Input.GetAxis("Mouse X") > 0f)
				{
					this.facingLeft = false;
				}
				else if (Input.GetAxis("Mouse X") < 0f)
				{
					this.facingLeft = true;
				}
			}
			else
			{
				this.lockX = true;
			}
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
			this.lastPosition = base.transform.localPosition;
		}
		this.CheckVictoryPosition();
	}

	// Token: 0x060018D1 RID: 6353 RVA: 0x00058F38 File Offset: 0x00057338
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.Meow();
		Audio.self.playLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5", base.transform);
		if (this.sleepingCat.gameObject.activeInHierarchy)
		{
			this.sleepingCat.gameObject.SetActive(false);
			base.GetComponent<SpriteRenderer>().enabled = true;
		}
		this.mouseClickPosition = Input.mousePosition;
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

	// Token: 0x060018D2 RID: 6354 RVA: 0x0005919E File Offset: 0x0005759E
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
		this.CheckVictoryConditions();
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x000591D9 File Offset: 0x000575D9
	private void OnDisable()
	{
		if (this.dragged)
		{
			Audio.self.stopLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5", base.transform, true);
		}
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x000591FC File Offset: 0x000575FC
	private void CheckVictoryPosition()
	{
		if (!this.dragged || !this.doorIsOpen)
		{
			return;
		}
		if (base.transform.position.x < this.jumpingCat.position.x)
		{
			base.transform.SetX(this.jumpingCat.position.x);
			base.StartCoroutine(this.EndJumpCoroutine());
		}
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x00059278 File Offset: 0x00057678
	private void CheckVictoryConditions()
	{
		if (!this.doorIsOpen)
		{
			return;
		}
		if (base.transform.position.x >= this.automaticExitPosition)
		{
			Global.LevelFailed(0f, true);
			Global.self.currPuzzle.GetComponent<AudioVoice_CatDoor>().end(false);
			base.StartCoroutine(this.MovingOutCoroutine(false));
			base.StartCoroutine(this.EndLevel(this.waitAfterEnd));
		}
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x000592F0 File Offset: 0x000576F0
	private void FlipCat()
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

	// Token: 0x060018D7 RID: 6359 RVA: 0x00059420 File Offset: 0x00057820
	private void Meow()
	{
		if (this.byTheDoor && (float)(++this.meowCounter) == this.meowCount)
		{
			this.door.Find("Closed").gameObject.SetActive(false);
			this.door.Find("Open").gameObject.SetActive(true);
			this.doorIsOpen = true;
			this.limit.rightVal = this.maxPositionAfterDoor;
			base.StartCoroutine(this.WaitForExitCoroutine());
			if (!this.arrowIsHidden)
			{
				this.arrow.GetComponent<Animator>().SetTrigger("hide");
				this.arrowIsHidden = true;
			}
			Audio.self.playOneShot("7c483392-9653-42e4-99f5-454cce46f6dd", 1f);
		}
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.meow);
		transform.gameObject.SetActive(true);
		transform.localScale = Vector3.one * UnityEngine.Random.Range(this.meowMinScale, this.meowMaxScale);
		transform.GetComponentInChildren<Text>().color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		UnityEngine.Object.Destroy(transform.gameObject, this.meowDuration);
		transform.position = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.2f, 0.8f), UnityEngine.Random.Range(0.2f, 0.8f)));
		transform.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-this.meowRotation, this.meowRotation));
		transform.SetParent(base.transform.parent);
		this.onMeowTimer = this.meowMouth;
		this.onMeow.gameObject.SetActive(true);
		Audio.self.playOneShot("0dc7a773-d6d1-4f75-a2fd-2f72a181e77e", 1f);
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x0005961A File Offset: 0x00057A1A
	private void UpdateOnMeow()
	{
		this.onMeowTimer -= Time.deltaTime;
		if (this.onMeowTimer < 0f)
		{
			this.onMeow.gameObject.SetActive(false);
		}
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x00059650 File Offset: 0x00057A50
	private IEnumerator MovingOutCoroutine(bool callEnd = false)
	{
		this.movingOut = true;
		Vector2 end = new Vector2(this.maxPositionAfterDoor, base.transform.position.y);
		while (base.transform.localPosition.x != this.maxPositionAfterDoor)
		{
			base.transform.localPosition = Vector2.MoveTowards(base.transform.localPosition, end, this.speed * Time.deltaTime);
			this.RotateAnimal();
			this.FlipCat();
			yield return null;
		}
		if (callEnd)
		{
			base.StartCoroutine(this.EndLevel(this.waitAfterEnd));
		}
		yield break;
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x00059674 File Offset: 0x00057A74
	private IEnumerator WaitForExitCoroutine()
	{
		yield return new WaitForSeconds(this.waitAfterOpenDoor);
		if (!this.movingOut && base.enabled)
		{
			this.door.Find("Closed").gameObject.SetActive(true);
			this.door.Find("Open").gameObject.SetActive(false);
			this.doorIsOpen = false;
			this.limit.rightVal = this.maxPositionBeforeDoor;
			this.meowCounter = 0;
			Audio.self.playOneShot("aa14c42f-b12e-4e2c-a73b-44da014c42bd", 1f);
		}
		yield break;
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x00059690 File Offset: 0x00057A90
	private IEnumerator EndJumpCoroutine()
	{
		this.dragEnabled = false;
		this.dragged = false;
		this.facingLeft = true;
		base.enabled = false;
		Audio.self.stopLoopSound("219c89fe-5ec7-4d84-aba9-9e360e9753b5", base.transform, true);
		while (base.transform.position.x > this.jumpingCat.position.x)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.jumpingCat.position, this.speed * Time.deltaTime);
			this.RotateAnimal();
			this.FlipCat();
			yield return null;
		}
		this.rotationTimer = 0f;
		while (Mathf.Abs(base.transform.eulerAngles.z) > 0.5f || Mathf.Abs(base.transform.localScale.x) != Mathf.Abs(this.startingScale))
		{
			this.RotateBack();
			this.FlipCat();
			yield return null;
		}
		Global.LevelCompleted(0f, true);
		Global.self.currPuzzle.GetComponent<AudioVoice_CatDoor>().end(true);
		Audio.self.playOneShot("db44b4ef-6dc9-49c0-bba3-21bb32bde0a6", 1f);
		this.jumpingAnimation.gameObject.SetActive(true);
		foreach (SpriteRenderer obj in base.GetComponentsInChildren<SpriteRenderer>())
		{
			UnityEngine.Object.Destroy(obj);
		}
		base.StartCoroutine(this.EndLevel(this.waitAfterBrokenWindow));
		yield break;
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x000596AC File Offset: 0x00057AAC
	private void RotateBack()
	{
		if (this.rotationTimer == this.rotationTime)
		{
			return;
		}
		this.rotationTimer = Mathf.MoveTowards(this.rotationTimer, this.rotationTime, Time.deltaTime);
		float num = this.rotationTimer / this.rotationTime;
		num = Mathf.Sin(num * 3.1415927f * 0.5f);
		base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.startingZ, 0f, num));
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x00059734 File Offset: 0x00057B34
	private void RotateAnimal()
	{
		float num = (base.transform.localPosition.x - this.lastPosition.x) / this.length;
		float z = Mathf.Sin(num * 3.1415927f * 0.5f) * this.amplitude;
		base.transform.localRotation = Quaternion.Euler(0f, 0f, z);
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x000597A0 File Offset: 0x00057BA0
	private IEnumerator EndLevel(float waitTime)
	{
		AudioVoice_CatDoor voice = Global.self.currPuzzle.GetComponent<AudioVoice_CatDoor>();
		while (!voice.canExitPuzzle)
		{
			yield return null;
		}
		Global.self.gotoNextLevel(false, null);
		yield break;
	}

	// Token: 0x040016B8 RID: 5816
	[Header("Level stuff")]
	public Transform door;

	// Token: 0x040016B9 RID: 5817
	public float mouseClickDistance;

	// Token: 0x040016BA RID: 5818
	[Header("Meow")]
	public Transform meow;

	// Token: 0x040016BB RID: 5819
	[Tooltip("For how many seconds a MEOW is shown")]
	public float meowDuration = 3f;

	// Token: 0x040016BC RID: 5820
	public float meowRotation = 60f;

	// Token: 0x040016BD RID: 5821
	public float meowMinScale = 0.5f;

	// Token: 0x040016BE RID: 5822
	public float meowMaxScale = 1.3f;

	// Token: 0x040016BF RID: 5823
	public float meowMouth = 0.3f;

	// Token: 0x040016C0 RID: 5824
	[Tooltip("How many meows are required to open door")]
	public float meowCount = 3f;

	// Token: 0x040016C1 RID: 5825
	[Header("Rotation")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x040016C2 RID: 5826
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x040016C3 RID: 5827
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x040016C4 RID: 5828
	[Tooltip("The time it takes the cat to scale to the left")]
	public float scaleTime = 0.3f;

	// Token: 0x040016C5 RID: 5829
	[Header("Exit")]
	public float meowStartPosition;

	// Token: 0x040016C6 RID: 5830
	public float maxPositionBeforeDoor;

	// Token: 0x040016C7 RID: 5831
	public float automaticExitPosition;

	// Token: 0x040016C8 RID: 5832
	public float maxPositionAfterDoor;

	// Token: 0x040016C9 RID: 5833
	public float waitAfterOpenDoor = 4f;

	// Token: 0x040016CA RID: 5834
	public float waitAfterEnd = 1f;

	// Token: 0x040016CB RID: 5835
	public float waitAfterBrokenWindow = 3f;

	// Token: 0x040016CC RID: 5836
	public Animator jumpingAnimation;

	// Token: 0x040016CD RID: 5837
	public Transform jumpingCat;

	// Token: 0x040016CE RID: 5838
	public float positionBeforeFlip;

	// Token: 0x040016CF RID: 5839
	public float speed;

	// Token: 0x040016D0 RID: 5840
	private bool movingOut;

	// Token: 0x040016D1 RID: 5841
	private Transform onMeow;

	// Token: 0x040016D2 RID: 5842
	private float onMeowTimer;

	// Token: 0x040016D3 RID: 5843
	private int meowCounter;

	// Token: 0x040016D4 RID: 5844
	private Vector3 lastPosition = -Vector3.zero;

	// Token: 0x040016D5 RID: 5845
	private Vector3 lastMousePosition;

	// Token: 0x040016D6 RID: 5846
	private float startingZ;

	// Token: 0x040016D7 RID: 5847
	private float rotationTimer;

	// Token: 0x040016D8 RID: 5848
	private float scaleTimer;

	// Token: 0x040016D9 RID: 5849
	private bool doorIsOpen;

	// Token: 0x040016DA RID: 5850
	private bool arrowIsHidden;

	// Token: 0x040016DB RID: 5851
	private float startingScale;

	// Token: 0x040016DC RID: 5852
	private bool facingLeft;

	// Token: 0x040016DD RID: 5853
	private Vector3 mouseClickPosition;

	// Token: 0x040016DE RID: 5854
	[Space(20f)]
	public Transform sleepingCat;

	// Token: 0x040016DF RID: 5855
	public Transform arrow;
}
