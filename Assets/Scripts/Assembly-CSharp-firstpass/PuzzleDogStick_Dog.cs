using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public class PuzzleDogStick_Dog : Draggable
{
	// Token: 0x060019BB RID: 6587 RVA: 0x00061BC3 File Offset: 0x0005FFC3
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		GizmosExtension.DrawVerticalLine(this.stickReturnPosition);
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x00061BD6 File Offset: 0x0005FFD6
	private void Start()
	{
		this.lastPosition = base.transform.position;
		this.facingLeft = true;
		base.StartCoroutine(this.DogRotationCoroutine());
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x00061C00 File Offset: 0x00060000
	private void Update()
	{
		if (this.stick != null && base.transform.position.x < this.stickReturnPosition)
		{
			this.ReturnStick();
		}
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x00061C42 File Offset: 0x00060042
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.FixRotationPosition();
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x00061C5C File Offset: 0x0006005C
	private void OnDisable()
	{
		this.OnMouseUp();
	}

	// Token: 0x060019C0 RID: 6592 RVA: 0x00061C64 File Offset: 0x00060064
	private void OnTriggerEnter2D(Collider2D other)
	{
		Audio.self.playOneShot("3df163c1-679b-4ee2-838f-954091f1c853", 1f);
		base.transform.GetChild(0).gameObject.SetActive(true);
		this.stick = other.transform;
		this.stick.gameObject.SetActive(false);
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x00061CBA File Offset: 0x000600BA
	private void ReturnStick()
	{
		this.stick.GetComponent<PuzzleDogStick_Stick>().Return();
		base.transform.GetChild(0).gameObject.SetActive(false);
		Global.LevelFailed(0f, true);
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x00061CF0 File Offset: 0x000600F0
	private void FlipDog()
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

	// Token: 0x060019C3 RID: 6595 RVA: 0x00061E68 File Offset: 0x00060268
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

	// Token: 0x060019C4 RID: 6596 RVA: 0x00062064 File Offset: 0x00060464
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

	// Token: 0x060019C5 RID: 6597 RVA: 0x00062194 File Offset: 0x00060594
	private IEnumerator DogRotationCoroutine()
	{
		for (;;)
		{
			this.FlipDog();
			this.RotateDog();
			yield return null;
		}
		yield break;
	}

	// Token: 0x040017C9 RID: 6089
	public float stickReturnPosition;

	// Token: 0x040017CA RID: 6090
	private Transform stick;

	// Token: 0x040017CB RID: 6091
	[Header("Rotation stuff")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x040017CC RID: 6092
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x040017CD RID: 6093
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x040017CE RID: 6094
	public float scaleTime = 0.3f;

	// Token: 0x040017CF RID: 6095
	private Vector3 lastPosition;

	// Token: 0x040017D0 RID: 6096
	private float rotationTimer;

	// Token: 0x040017D1 RID: 6097
	private float startingZ;

	// Token: 0x040017D2 RID: 6098
	private bool facingLeft;

	// Token: 0x040017D3 RID: 6099
	private float scaleTimer;

	// Token: 0x040017D4 RID: 6100
	private float startingScale;
}
