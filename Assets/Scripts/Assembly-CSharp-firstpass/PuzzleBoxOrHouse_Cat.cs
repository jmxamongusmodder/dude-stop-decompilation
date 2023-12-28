using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003CD RID: 973
public class PuzzleBoxOrHouse_Cat : Draggable
{
	// Token: 0x0600185E RID: 6238 RVA: 0x00055514 File Offset: 0x00053914
	private void Start()
	{
		this.limit.leftScreen = false;
		this.limit.leftVal = this.houseAnimationPosition;
		this.limit.rightScreen = false;
		this.limit.rightVal = this.boxAnimationPosition;
		this.facingLeft = true;
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x00055562 File Offset: 0x00053962
	private void Update()
	{
		this.FlipAnimal();
		this.RotateAnimal();
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x00055570 File Offset: 0x00053970
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawVerticalLine(this.houseAnimationPosition, Color.red);
		GizmosExtension.DrawVerticalLine(this.boxAnimationPosition, Color.red);
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x00055592 File Offset: 0x00053992
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.FixRotationPosition();
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x000555AC File Offset: 0x000539AC
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.transform.position.x == this.houseAnimationPosition)
		{
			base.StartCoroutine(this.WaitingForRotationCoroutine(delegate
			{
				this.houseAnimation.gameObject.SetActive(true);
				this.houseAnimation.enabled = true;
				UnityEngine.Object.Destroy(base.gameObject);
				Global.self.currPuzzle.GetComponent<AudioVoice_CatBoxOrHouse>().end(false);
				Global.LevelFailed(0f, true);
			}));
		}
		else if (base.transform.position.x == this.boxAnimationPosition)
		{
			base.StartCoroutine(this.WaitingForRotationCoroutine(delegate
			{
				this.boxAnimation.gameObject.SetActive(true);
				this.boxAnimation.enabled = true;
				UnityEngine.Object.Destroy(base.gameObject);
				Global.self.currPuzzle.GetComponent<AudioVoice_CatBoxOrHouse>().end(true);
				Global.LevelCompleted(0f, true);
			}));
		}
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x0005563E File Offset: 0x00053A3E
	private void OnCollisionEnter2D()
	{
		base.GetComponent<Collider2D>().isTrigger = true;
		base.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x00055658 File Offset: 0x00053A58
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

	// Token: 0x06001865 RID: 6245 RVA: 0x000557D0 File Offset: 0x00053BD0
	private void RotateAnimal()
	{
		if (this.dragged || this.simplyRotating)
		{
			float num = (base.transform.position - this.lastPosition).magnitude / this.length;
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

	// Token: 0x06001866 RID: 6246 RVA: 0x0005590C File Offset: 0x00053D0C
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

	// Token: 0x06001867 RID: 6247 RVA: 0x00055B08 File Offset: 0x00053F08
	private IEnumerator WaitingForRotationCoroutine(Action callback)
	{
		this.dragEnabled = false;
		while (base.transform.position != this.lastPosition)
		{
			yield return null;
		}
		callback();
		yield break;
	}

	// Token: 0x04001649 RID: 5705
	[Header("Animations")]
	public Animator houseAnimation;

	// Token: 0x0400164A RID: 5706
	public Animator boxAnimation;

	// Token: 0x0400164B RID: 5707
	public GameObject box;

	// Token: 0x0400164C RID: 5708
	public float houseAnimationPosition;

	// Token: 0x0400164D RID: 5709
	public float boxAnimationPosition;

	// Token: 0x0400164E RID: 5710
	[Header("Rotation stuff")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x0400164F RID: 5711
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x04001650 RID: 5712
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x04001651 RID: 5713
	public float scaleTime = 0.3f;

	// Token: 0x04001652 RID: 5714
	private Vector3 lastPosition;

	// Token: 0x04001653 RID: 5715
	private float rotationTimer;

	// Token: 0x04001654 RID: 5716
	private float startingZ;

	// Token: 0x04001655 RID: 5717
	private bool facingLeft;

	// Token: 0x04001656 RID: 5718
	private float scaleTimer;

	// Token: 0x04001657 RID: 5719
	private float startingScale;

	// Token: 0x04001658 RID: 5720
	private bool simplyRotating;
}
