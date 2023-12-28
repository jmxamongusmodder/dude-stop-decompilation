using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000450 RID: 1104
public class PuzzleSlowLeftLine_Player : MonoBehaviour
{
	// Token: 0x06001C47 RID: 7239 RVA: 0x00077B4A File Offset: 0x00075F4A
	private void Start()
	{
		this.InitSpring();
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x00077B54 File Offset: 0x00075F54
	private void Update()
	{
		if (!this.onScreen)
		{
			return;
		}
		float paramValue = Extensions.Between(0f, this.thresholdSpeed, base.GetComponent<Rigidbody2D>().velocity.magnitude, true);
		Audio.self.playLoopSound("ec53d62c-54b6-49f9-b993-a0050e287310", "Velocity", paramValue);
	}

	// Token: 0x06001C49 RID: 7241 RVA: 0x00077BA7 File Offset: 0x00075FA7
	private void FixedUpdate()
	{
		this.UpdateAnchorPosition();
	}

	// Token: 0x06001C4A RID: 7242 RVA: 0x00077BAF File Offset: 0x00075FAF
	private void OnEnable()
	{
		this.InitSpring();
		this.spring.enabled = true;
	}

	// Token: 0x06001C4B RID: 7243 RVA: 0x00077BC3 File Offset: 0x00075FC3
	private void OnDisable()
	{
		this.InitSpring();
		this.spring.enabled = false;
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x00077BD7 File Offset: 0x00075FD7
	private void OnTriggerEnter2D(Collider2D other)
	{
		this.ChangeColliderStatusTo(true, other);
	}

	// Token: 0x06001C4D RID: 7245 RVA: 0x00077BE1 File Offset: 0x00075FE1
	private void OnTriggerExit2D(Collider2D other)
	{
		this.ChangeColliderStatusTo(false, other);
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x00077BEC File Offset: 0x00075FEC
	private void OnMouseDown()
	{
		if (!this.dragEnabled)
		{
			return;
		}
		this.dragged = true;
		Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.delta = a - base.transform.position;
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x00077C33 File Offset: 0x00076033
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = false;
		if (this.movedOut)
		{
			base.StartCoroutine(this.LevelCompletionCoroutine());
		}
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x00077C60 File Offset: 0x00076060
	private void UpdateAnchorPosition()
	{
		if (!this.dragged)
		{
			return;
		}
		Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 connectedAnchor = new Vector2(vector.x - this.delta.x, vector.y - this.delta.y);
		if (this.limitLeft && connectedAnchor.x < this.leftX)
		{
			connectedAnchor.x = this.leftX;
		}
		this.spring.connectedAnchor = connectedAnchor;
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x00077CEC File Offset: 0x000760EC
	private void ChangeColliderStatusTo(bool status, Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onLeftLane = status;
		}
		else if (other.tag == "FailCollider")
		{
			this.movedOut = status;
		}
	}

	// Token: 0x06001C52 RID: 7250 RVA: 0x00077D2B File Offset: 0x0007612B
	private void InitSpring()
	{
		if (this.spring == null)
		{
			this.spring = base.GetComponent<SpringJoint2D>();
		}
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x00077D4C File Offset: 0x0007614C
	private IEnumerator LevelCompletionCoroutine()
	{
		this.GetComponentInPuzzleStats<PuzzleSlowLeftLine_MovingCar>().enabled = false;
		this.spring.enabled = false;
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.dragEnabled = false;
		Renderer rend = base.GetComponent<Renderer>();
		this.onScreen = false;
		Audio.self.stopLoopSound("ec53d62c-54b6-49f9-b993-a0050e287310", true);
		Audio.self.playOneShot("ee42fe8e-46b7-4e3d-88e6-379f68bb9c38", 1f);
		while (GeometryUtility.TestPlanesAABB(planes, rend.bounds))
		{
			base.transform.position += Vector3.up * this.movementSpeed * Time.deltaTime;
			yield return null;
		}
		Global.LevelFailed(0f, true);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04001AA4 RID: 6820
	public PuzzleSlowLeftLine_MovingCar movingCar;

	// Token: 0x04001AA5 RID: 6821
	public float movementSpeed;

	// Token: 0x04001AA6 RID: 6822
	public bool limitLeft;

	// Token: 0x04001AA7 RID: 6823
	public float leftX;

	// Token: 0x04001AA8 RID: 6824
	private bool dragged;

	// Token: 0x04001AA9 RID: 6825
	private bool dragEnabled = true;

	// Token: 0x04001AAA RID: 6826
	private Vector3 delta;

	// Token: 0x04001AAB RID: 6827
	private SpringJoint2D spring;

	// Token: 0x04001AAC RID: 6828
	[HideInInspector]
	public bool movedOut;

	// Token: 0x04001AAD RID: 6829
	[HideInInspector]
	public bool onLeftLane;

	// Token: 0x04001AAE RID: 6830
	[HideInInspector]
	public bool onScreen = true;

	// Token: 0x04001AAF RID: 6831
	public float thresholdSpeed = 10f;
}
