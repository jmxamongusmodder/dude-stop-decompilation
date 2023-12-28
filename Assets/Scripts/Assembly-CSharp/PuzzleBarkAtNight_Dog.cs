using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003C0 RID: 960
public class PuzzleBarkAtNight_Dog : EnhancedDraggable, TransitionProcessor
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060017E1 RID: 6113 RVA: 0x00051B68 File Offset: 0x0004FF68
	// (remove) Token: 0x060017E2 RID: 6114 RVA: 0x00051B9C File Offset: 0x0004FF9C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action OnBark;

	// Token: 0x060017E3 RID: 6115 RVA: 0x00051BD0 File Offset: 0x0004FFD0
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		GizmosExtension.DrawVerticalLine(this.houseLine.position.x);
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x00051BFC File Offset: 0x0004FFFC
	private void Awake()
	{
		this.phantomBody = this.phantomDoge.GetComponent<Rigidbody2D>();
		if (PuzzleBarkAtNight_Dog_animation.showFlyingDog == false)
		{
			base.transform.position = this.house.position;
			this.phantomBody.simulated = false;
			base.body.simulated = false;
			base.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
			base.GetComponent<SpriteRenderer>().sortingOrder--;
			this.hasToMoveOut = true;
		}
		this.SetLineShader();
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x00051C98 File Offset: 0x00050098
	private void Start()
	{
		this.lastPosition = base.transform.localPosition;
		if (this.hasToMoveOut)
		{
			base.StartCoroutine(this.MovingOutCoroutine());
		}
		base.StartCoroutine(this.RotationCoroutine());
		base.StartCoroutine(this.FlippingCoroutine());
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x00051CE8 File Offset: 0x000500E8
	private void Update()
	{
		this.phantomBody.MovePosition(base.transform.position);
		if (Mathf.Sign(base.transform.localScale.x) != Mathf.Sign(this.phantomDoge.localScale.x))
		{
			this.phantomDoge.localScale = new Vector3(this.phantomDoge.localScale.x * -1f, this.phantomDoge.localScale.y);
		}
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x00051D81 File Offset: 0x00050181
	private void OnDisable()
	{
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
	}

	// Token: 0x060017E8 RID: 6120 RVA: 0x00051D99 File Offset: 0x00050199
	protected override void MouseDowned()
	{
		this.Bark();
		this.FixRotationPosition();
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x00051DA7 File Offset: 0x000501A7
	protected override void MouseUpped()
	{
		base.body.velocity = Vector2.zero;
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x00051DB9 File Offset: 0x000501B9
	public void MoveLine()
	{
		base.GetComponent<SpriteRenderer>().material.SetFloat("_Left", 0f);
	}

	// Token: 0x060017EB RID: 6123 RVA: 0x00051DD8 File Offset: 0x000501D8
	private void FlipAnimal()
	{
		if (this.dragged && Vector3.Distance(Input.mousePosition, this.mouseClickPosition) >= this.mouseClickDistance)
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

	// Token: 0x060017EC RID: 6124 RVA: 0x00051F70 File Offset: 0x00050370
	private void FixRotationPosition()
	{
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
			UnityEngine.Debug.Log(string.Format("({0}) % {1} ==> {2} .. {3} = {4}", new object[]
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

	// Token: 0x060017ED RID: 6125 RVA: 0x0005217C File Offset: 0x0005057C
	private void RotateAnimal()
	{
		if (this.dragged || this.activeCoroutine)
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
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.startingZ, 0f, num2));
		}
		else
		{
			this.lastPosition = base.transform.localPosition;
		}
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x000522B8 File Offset: 0x000506B8
	private void Bark()
	{
		if (!this.canBark)
		{
			return;
		}
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.bark);
		transform.gameObject.SetActive(true);
		float num = UnityEngine.Random.Range(this.barkMinScale, this.barkMaxScale);
		transform.localScale = new Vector3(num, num, num);
		transform.GetComponentInChildren<Text>().color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		UnityEngine.Object.Destroy(transform.gameObject, this.barkDuration);
		transform.position = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(this.minBarkX, 0.8f), UnityEngine.Random.Range(0.2f, 0.8f)));
		transform.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-this.barkRotation, this.barkRotation));
		transform.SetParent(base.transform.parent);
		Audio.self.playOneShot("88dc5792-5949-460d-b7bc-444b969d663c", 1f);
		if (this.barks.Count == 3)
		{
			Transform transform2 = this.barks.Dequeue();
			if (transform2 != null)
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		this.barks.Enqueue(transform);
		PuzzleBarkAtNight_Dog.OnBark();
		base.StartCoroutine(this.BarkCoroutine());
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x0005243C File Offset: 0x0005083C
	private IEnumerator BarkCoroutine()
	{
		this.mouth.gameObject.SetActive(true);
		yield return new WaitForSeconds(this.showMouthTime);
		this.mouth.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x00052458 File Offset: 0x00050858
	private IEnumerator FlippingCoroutine()
	{
		for (;;)
		{
			this.FlipAnimal();
			yield return null;
		}
		yield break;
	}

	// Token: 0x060017F1 RID: 6129 RVA: 0x00052474 File Offset: 0x00050874
	private IEnumerator MovingOutCoroutine()
	{
		this.activeCoroutine = true;
		base.enabled = false;
		this.phantomBody.simulated = false;
		this.facingLeft = false;
		float timer = 0f;
		Vector2 start = new Vector2(this.house.localPosition.x, this.endPosition.y);
		this.lastPosition = start;
		while (timer != this.movingOutTime)
		{
			timer = Mathf.MoveTowards(timer, this.movingOutTime, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(start, this.endPosition, timer / this.movingOutTime);
			yield return null;
		}
		base.enabled = true;
		base.body.simulated = true;
		base.body.velocity = Vector2.zero;
		base.GetComponent<SpriteRenderer>().sortingLayerName = "Top";
		base.GetComponent<SpriteRenderer>().sortingOrder++;
		this.phantomBody.simulated = true;
		this.activeCoroutine = false;
		this.facingLeft = true;
		yield break;
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x00052490 File Offset: 0x00050890
	private IEnumerator RotationCoroutine()
	{
		this.FixRotationPosition();
		for (;;)
		{
			this.RotateAnimal();
			yield return null;
		}
		yield break;
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x000524AC File Offset: 0x000508AC
	private void SetLineShader()
	{
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		component.material.SetFloat("_Top", 0f);
		component.material.SetFloat("_Angle", -1.5707964f);
		component.material.SetFloat("_Distance", 0f);
		this.UpdateLineShader();
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x00052508 File Offset: 0x00050908
	private void UpdateLineShader()
	{
		Vector2 vector = Camera.main.WorldToViewportPoint(this.houseLine.position);
		base.GetComponent<SpriteRenderer>().material.SetFloat("_Left", vector.x);
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x0005254C File Offset: 0x0005094C
	public void TransitionUpdate()
	{
		this.UpdateLineShader();
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x00052554 File Offset: 0x00050954
	// Note: this type is marked as 'beforefieldinit'.
	static PuzzleBarkAtNight_Dog()
	{
		PuzzleBarkAtNight_Dog.OnBark = delegate()
		{
		};
	}

	// Token: 0x040015C9 RID: 5577
	public Transform phantomDoge;

	// Token: 0x040015CA RID: 5578
	private Rigidbody2D phantomBody;

	// Token: 0x040015CB RID: 5579
	[Header("House stuff")]
	public Transform house;

	// Token: 0x040015CC RID: 5580
	public Transform houseLine;

	// Token: 0x040015CD RID: 5581
	public Vector2 endPosition;

	// Token: 0x040015CE RID: 5582
	public float movingOutTime = 2f;

	// Token: 0x040015CF RID: 5583
	private bool hasToMoveOut;

	// Token: 0x040015D0 RID: 5584
	private bool activeCoroutine;

	// Token: 0x040015D1 RID: 5585
	[Header("Bark stuff")]
	public Transform bark;

	// Token: 0x040015D2 RID: 5586
	public bool canBark = true;

	// Token: 0x040015D3 RID: 5587
	public Transform mouth;

	// Token: 0x040015D4 RID: 5588
	public float showMouthTime = 0.2f;

	// Token: 0x040015D5 RID: 5589
	public float minBarkX = 0.5f;

	// Token: 0x040015D6 RID: 5590
	public float barkDuration = 3f;

	// Token: 0x040015D7 RID: 5591
	public float barkRotation = 60f;

	// Token: 0x040015D8 RID: 5592
	public float barkMinScale = 0.5f;

	// Token: 0x040015D9 RID: 5593
	public float barkMaxScale = 1.3f;

	// Token: 0x040015DA RID: 5594
	public float mouseClickDistance = 0.1f;

	// Token: 0x040015DB RID: 5595
	private Vector2 mouseClickPosition;

	// Token: 0x040015DC RID: 5596
	private Queue<Transform> barks = new Queue<Transform>(3);

	// Token: 0x040015DD RID: 5597
	private const int TOTAL_BARK_COUNT = 3;

	// Token: 0x040015DE RID: 5598
	[Header("Rotation stuff")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x040015DF RID: 5599
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x040015E0 RID: 5600
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x040015E1 RID: 5601
	public float scaleTime = 0.3f;

	// Token: 0x040015E2 RID: 5602
	private Vector3 lastPosition;

	// Token: 0x040015E3 RID: 5603
	private float rotationTimer;

	// Token: 0x040015E4 RID: 5604
	private float startingZ;

	// Token: 0x040015E5 RID: 5605
	private bool facingLeft = true;

	// Token: 0x040015E6 RID: 5606
	private float scaleTimer;

	// Token: 0x040015E7 RID: 5607
	private float startingScale;
}
