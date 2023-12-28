using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class PuzzleParkOnLine_Car : MonoBehaviour
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600014D RID: 333 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
	// (set) Token: 0x0600014E RID: 334 RVA: 0x0000C5FC File Offset: 0x0000A7FC
	private Vector3 delta
	{
		get
		{
			return Quaternion.Euler(0f, 0f, base.transform.eulerAngles.z) * this._delta;
		}
		set
		{
			this._delta = Quaternion.Euler(0f, 0f, -base.transform.eulerAngles.z) * value;
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000C638 File Offset: 0x0000A838
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(base.transform.position - base.transform.right, base.transform.position - base.transform.right + this.clickAngleOffset * -base.transform.right);
		Gizmos.color = Color.magenta;
		Vector2 a = Quaternion.Euler(0f, 0f, this.clickAngle) * -base.transform.right;
		Vector2 a2 = Quaternion.Euler(0f, 0f, -this.clickAngle) * -base.transform.right;
		Vector2 vector = base.transform.position + this.clickAngleOffset * -base.transform.right;
		Gizmos.DrawLine(vector, vector + a * 3f);
		Gizmos.DrawLine(vector, vector + a2 * 3f);
		Gizmos.DrawLine(vector, vector - a * 3f);
		Gizmos.DrawLine(vector, vector - a2 * 3f);
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000C7CC File Offset: 0x0000A9CC
	private void Update()
	{
		if (this.MouseMoved() && this.WithinCone())
		{
			bool flag = this.CalculateMovement();
			if (flag && !this.NearWall())
			{
				this.UpdatePosition(true);
				this.UpdateRotation();
				Global.self.currPuzzle.GetComponent<AudioVoice_ParkOnLine>().startMoving();
			}
		}
		else
		{
			this.UpdatePosition(false);
		}
		this.UpdateSound();
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000C83C File Offset: 0x0000AA3C
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.linesCrossed++;
			Global.self.currPuzzle.GetComponent<AudioVoice_ParkOnLine>().collideWithLine();
		}
		else if (other.tag == "FailCollider")
		{
			this.spacesOccupied++;
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.linesCrossed--;
		}
		else if (other.tag == "FailCollider")
		{
			this.spacesOccupied--;
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0000C900 File Offset: 0x0000AB00
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = true;
		Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.delta = a - base.transform.position;
		if (this.waitingCoroutine != null)
		{
			base.StopCoroutine(this.waitingCoroutine);
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000C960 File Offset: 0x0000AB60
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = false;
		this.leftRotationSpeed = 0f;
		this.rightRotationSpeed = 0f;
		this.currentSpeed = 0f;
		if (this.spacesOccupied > 0 || this.linesCrossed > 0)
		{
			this.waitingCoroutine = base.StartCoroutine(this.WaitingCoroutine());
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000C9CC File Offset: 0x0000ABCC
	private bool MouseMoved()
	{
		if (!this.dragged)
		{
			return false;
		}
		Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return Vector2.Distance(base.transform.position + this.clickAngleOffset * -base.transform.right, v) >= this.minMouseDistance;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x0000CA40 File Offset: 0x0000AC40
	private bool WithinCone()
	{
		Vector2 vector = base.transform.position + this.clickAngleOffset * -base.transform.right;
		Vector2 vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 from = vector2 - vector;
		float num = Vector2.Angle(from, -base.transform.right + this.clickAngleOffset * -base.transform.right);
		Debug.DrawLine(vector, vector2);
		if (num > 90f)
		{
			num = 180f - num;
		}
		if (num > this.clickAngle && from.magnitude > this.clickDisableDistance)
		{
			this.OnMouseUp();
		}
		return num < this.clickAngle;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0000CB28 File Offset: 0x0000AD28
	private bool NearWall()
	{
		float d = 0.4f;
		int num = (!this.moveBackwards) ? -1 : 1;
		Vector3 a = base.transform.position + (this.clickAngleOffset - 0.1f) * -base.transform.right;
		base.gameObject.layer = LayerMask.NameToLayer("Individual");
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		RaycastHit2D raycastHit2D = Physics2D.Raycast(a + base.transform.up * d, base.transform.right * (float)num, this.raycastDistance, layerMask);
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(a - base.transform.up * d, base.transform.right * (float)num, this.raycastDistance, layerMask);
		base.gameObject.layer = LayerMask.NameToLayer("Default");
		Debug.DrawRay(a + base.transform.up * d, base.transform.right * (float)num * this.raycastDistance);
		Debug.DrawRay(a - base.transform.up * d, base.transform.right * (float)num * this.raycastDistance);
		return raycastHit2D.collider != null || raycastHit2D2.collider != null;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000CCD0 File Offset: 0x0000AED0
	private bool CalculateMovement()
	{
		Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 v = a - (base.transform.position + this.clickAngleOffset * -base.transform.right);
		v = Quaternion.Euler(0f, 0f, -base.transform.eulerAngles.z) * v;
		this.moveBackwards = (v.x > 0f);
		this.rotate = (Mathf.Abs(v.y) > this.minRotationDistance);
		return v.x != 0f;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000CD98 File Offset: 0x0000AF98
	private void UpdatePosition(bool accelerate)
	{
		int num = (!this.moveBackwards) ? 1 : -1;
		float target = (!accelerate) ? 0f : (this.maxSpeed * (float)num);
		this.currentSpeed = Mathf.MoveTowards(this.currentSpeed, target, this.acceleration * Time.deltaTime);
		base.transform.position -= base.transform.right * this.currentSpeed * Time.deltaTime;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000CE28 File Offset: 0x0000B028
	private void UpdateSound()
	{
		if (!base.enabled)
		{
			return;
		}
		float target = Extensions.Between(0f, this.maxSpeed, Mathf.Abs(this.currentSpeed), true);
		this.soundSpeed = Mathf.MoveTowards(this.soundSpeed, target, Time.deltaTime * 4f);
		Audio.self.playLoopSound("8820f571-a367-4051-8bbc-9795731efead", base.transform, "Velocity", this.soundSpeed);
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000CE9C File Offset: 0x0000B09C
	private void UpdateRotation()
	{
		Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 vector = a - this.delta;
		Vector2 vector2 = base.transform.position + this.clickAngleOffset * -base.transform.right;
		if (this.moveBackwards)
		{
			vector -= vector2;
		}
		else
		{
			vector = vector2 - vector;
		}
		float num = PuzzleParkOnLine_Car.AngleDir(-base.transform.right, vector);
		int num2 = 0;
		if (num < 0f)
		{
			num2 = -1;
		}
		else if (num > 0f)
		{
			num2 = 1;
		}
		if (this.moveBackwards)
		{
			num2 *= -1;
		}
		float num3 = 0f;
		if (num2 == 0 || !this.rotate)
		{
			this.leftRotationSpeed = Mathf.MoveTowards(this.leftRotationSpeed, 0f, this.rotationAcceleration * Time.deltaTime);
			this.rightRotationSpeed = Mathf.MoveTowards(this.rightRotationSpeed, 0f, this.rotationAcceleration * Time.deltaTime);
		}
		else if (num2 == -1)
		{
			if (this.rightRotationSpeed > 0f)
			{
				this.rightRotationSpeed = Mathf.MoveTowards(this.rightRotationSpeed, 0f, this.rotationAcceleration * Time.deltaTime);
			}
			else
			{
				this.leftRotationSpeed = Mathf.MoveTowards(this.leftRotationSpeed, this.maxRotationSpeed, this.rotationAcceleration * Time.deltaTime);
			}
			num3 = -this.rightRotationSpeed + this.leftRotationSpeed;
		}
		else if (num2 == 1)
		{
			if (this.leftRotationSpeed > 0f)
			{
				this.leftRotationSpeed = Mathf.MoveTowards(this.leftRotationSpeed, 0f, this.rotationAcceleration * Time.deltaTime);
			}
			else
			{
				this.rightRotationSpeed = Mathf.MoveTowards(this.rightRotationSpeed, this.maxRotationSpeed, this.rotationAcceleration * Time.deltaTime);
			}
			num3 = -this.leftRotationSpeed + this.rightRotationSpeed;
		}
		float num4 = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		num4 %= 360f;
		if (num4 < 0f)
		{
			num4 = 360f + num4;
		}
		if (num3 > 0f)
		{
			num4 = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, num4, num3 * Time.deltaTime);
		}
		else
		{
			num4 = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, num4 - 180f, Mathf.Abs(num3) * Time.deltaTime);
		}
		base.transform.rotation = Quaternion.Euler(0f, 0f, num4);
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000D175 File Offset: 0x0000B375
	private static float AngleDir(Vector2 A, Vector2 B)
	{
		return -A.x * B.y + A.y * B.x;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000D198 File Offset: 0x0000B398
	private IEnumerator WaitingCoroutine()
	{
		yield return new WaitForSeconds(this.waitBeforeEnd);
		if (this.linesCrossed > 0)
		{
			Global.LevelCompleted(0f, true);
		}
		else if (this.spacesOccupied > 0)
		{
			Global.LevelFailed(0f, true);
		}
		yield break;
	}

	// Token: 0x040001EF RID: 495
	public float waitBeforeEnd = 2f;

	// Token: 0x040001F0 RID: 496
	[Header("Movement")]
	public float minMouseDistance;

	// Token: 0x040001F1 RID: 497
	public float raycastDistance = 1f;

	// Token: 0x040001F2 RID: 498
	public float maxSpeed;

	// Token: 0x040001F3 RID: 499
	public float acceleration;

	// Token: 0x040001F4 RID: 500
	private float currentSpeed;

	// Token: 0x040001F5 RID: 501
	private bool moveBackwards;

	// Token: 0x040001F6 RID: 502
	[Header("Rotation")]
	public float minRotationDistance;

	// Token: 0x040001F7 RID: 503
	public float maxRotationSpeed = 45f;

	// Token: 0x040001F8 RID: 504
	public float rotationAcceleration = 45f;

	// Token: 0x040001F9 RID: 505
	public float clickAngle = 30f;

	// Token: 0x040001FA RID: 506
	public float clickAngleOffset;

	// Token: 0x040001FB RID: 507
	public float clickDisableDistance;

	// Token: 0x040001FC RID: 508
	private bool rotate;

	// Token: 0x040001FD RID: 509
	private float leftRotationSpeed;

	// Token: 0x040001FE RID: 510
	private float rightRotationSpeed;

	// Token: 0x040001FF RID: 511
	private float rotationSpeed;

	// Token: 0x04000200 RID: 512
	private bool dragged;

	// Token: 0x04000201 RID: 513
	private Vector3 _delta;

	// Token: 0x04000202 RID: 514
	private int linesCrossed;

	// Token: 0x04000203 RID: 515
	private int spacesOccupied;

	// Token: 0x04000204 RID: 516
	private Coroutine waitingCoroutine;

	// Token: 0x04000205 RID: 517
	private float soundSpeed;
}
