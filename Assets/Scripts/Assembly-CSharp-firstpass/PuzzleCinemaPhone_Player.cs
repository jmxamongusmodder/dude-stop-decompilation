using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003E6 RID: 998
public class PuzzleCinemaPhone_Player : MonoBehaviour
{
	// Token: 0x06001930 RID: 6448 RVA: 0x0005C99D File Offset: 0x0005AD9D
	private void Awake()
	{
		this.spring = base.GetComponent<SpringJoint2D>();
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x0005C9AC File Offset: 0x0005ADAC
	private void Update()
	{
		if (this.dragged)
		{
			float x = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, base.transform.position.x - this.maxSpringJointDistance, base.transform.position.x + this.maxSpringJointDistance);
			this.spring.connectedAnchor = new Vector2(x, base.transform.position.y);
		}
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x0005CA3A File Offset: 0x0005AE3A
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.spring.enabled = true;
		this.dragged = true;
		this.StartSound();
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x0005CA61 File Offset: 0x0005AE61
	private void OnMouseUp()
	{
		if (!this.dragged || !base.enabled)
		{
			return;
		}
		this.spring.enabled = false;
		this.dragged = false;
		this.StopSound();
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x0005CA93 File Offset: 0x0005AE93
	private void OnDisable()
	{
		if (this.dragged)
		{
			this.StopSound();
		}
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x0005CAA8 File Offset: 0x0005AEA8
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (base.GetComponent<Rigidbody2D>().velocity.x <= 0f || this.activeCoroutine)
		{
			return;
		}
		if (other.tag == "SuccessCollider")
		{
			base.StartCoroutine(this.JupmingIntoSeatCoroutine(other.transform));
		}
		else
		{
			base.StartCoroutine(this.JumpingCoroutine());
		}
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x0005CB18 File Offset: 0x0005AF18
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (this.activeCoroutine)
		{
			return;
		}
		if (collision.transform.tag != "GlobalCollider")
		{
			return;
		}
		if (collision.collider.isTrigger)
		{
			return;
		}
		GlobalCollider component = collision.transform.GetComponent<GlobalCollider>();
		if (component != null && component.bottom.collider != collision.collider)
		{
			return;
		}
		Audio.self.playOneShot("ada9cc91-6da6-44b7-a098-3633e94c1806", 1f);
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x0005CBA6 File Offset: 0x0005AFA6
	private void StartSound()
	{
		if (this.movingPlaying)
		{
			return;
		}
		Audio.self.playLoopSound("2f2b8cbe-ab1b-4aba-b5d7-09034869ebee");
		this.movingPlaying = true;
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x0005CBCA File Offset: 0x0005AFCA
	private void StopSound()
	{
		if (!this.movingPlaying)
		{
			return;
		}
		Audio.self.stopLoopSound("2f2b8cbe-ab1b-4aba-b5d7-09034869ebee", true);
		this.movingPlaying = false;
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x0005CBF0 File Offset: 0x0005AFF0
	private IEnumerator SlidingPhoneIntoScreenCoroutine()
	{
		yield return null;
		Vector2 start = this.phone.position;
		Vector2 end = new Vector2(start.x, this.finalY);
		float timer = 0f;
		while (timer != this.slideTime)
		{
			timer = Mathf.MoveTowards(timer, this.slideTime, Time.deltaTime);
			float t = Mathf.Sin(timer / this.slideTime * 3.1415927f * 0.5f);
			this.phone.position = Vector2.Lerp(start, end, t);
			yield return null;
		}
		this.phone.GetComponent<Collider2D>().enabled = true;
		this.phone.GetComponent<Draggable>().limit.bottom = true;
		yield break;
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x0005CC0C File Offset: 0x0005B00C
	private IEnumerator JupmingIntoSeatCoroutine(Transform chair)
	{
		this.dragged = false;
		UnityEngine.Object.Destroy(base.GetComponent<Collider2D>());
		UnityEngine.Object.Destroy(base.GetComponent<SpringJoint2D>());
		UnityEngine.Object.Destroy(base.GetComponent<Rigidbody2D>());
		yield return null;
		this.StopSound();
		Vector2 start = base.transform.localPosition;
		float seatHeight = this.seatPosition.y - start.y;
		float seatDistance = this.seatPosition.x - start.x;
		float totalHeight = 2f * this.jumpHeight - seatHeight;
		float time = this.jumpHeight / totalHeight * this.time;
		float time2 = (this.jumpHeight - seatHeight) / totalHeight * this.time;
		float timer = 0f;
		while (timer != time)
		{
			timer = Mathf.MoveTowards(timer, time, Time.deltaTime);
			float y = Mathf.Sin(timer / time * 3.1415927f * 0.5f) * this.jumpHeight;
			float x = timer / this.time * seatDistance;
			base.transform.localPosition = new Vector2(start.x + x, start.y + y);
			yield return null;
		}
		this.handle.GetComponent<SpriteRenderer>().sortingOrder = 4;
		timer = time2;
		while (timer != 0f)
		{
			timer = Mathf.MoveTowards(timer, 0f, Time.deltaTime);
			float y2 = Mathf.Sin(timer / time2 * 3.1415927f * 0.5f) * (this.jumpHeight - seatHeight) + seatHeight;
			float x2 = (this.time - timer) / this.time * seatDistance;
			base.transform.localPosition = new Vector2(start.x + x2, start.y + y2);
			yield return null;
		}
		Audio.self.playLoopSound("b9edb8a8-88b6-466e-a486-a2ec0b526360", base.transform);
		Audio.self.playOneShot("ada9cc91-6da6-44b7-a098-3633e94c1806", 1f);
		this.screen.GetComponent<PuzzleCinemaPhone_Screen>().StartGlowing();
		base.StartCoroutine(this.SlidingPhoneIntoScreenCoroutine());
		yield break;
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x0005CC28 File Offset: 0x0005B028
	private IEnumerator JumpingCoroutine()
	{
		this.activeCoroutine = true;
		this.spring.enabled = false;
		yield return null;
		Vector2 start = base.transform.localPosition;
		float totalHeight = 2f * this.height - this.stepHeight;
		float time = this.height / totalHeight * this.time;
		float time2 = (this.height - this.stepHeight) / totalHeight * this.time;
		float timer = 0f;
		while (timer != time)
		{
			timer = Mathf.MoveTowards(timer, time, Time.deltaTime);
			float y = Mathf.Sin(timer / time * 3.1415927f * 0.5f) * this.height;
			float x = timer / this.time * this.distance;
			base.transform.localPosition = new Vector2(start.x + x, start.y + y);
			yield return null;
		}
		timer = time2;
		while (timer != 0f)
		{
			timer = Mathf.MoveTowards(timer, 0f, Time.deltaTime);
			float y2 = Mathf.Sin(timer / time2 * 3.1415927f * 0.5f) * (this.height - this.stepHeight) + this.stepHeight;
			float x2 = (this.time - timer) / this.time * this.distance;
			base.transform.localPosition = new Vector2(start.x + x2, start.y + y2);
			yield return null;
		}
		if (this.dragged)
		{
			this.spring.enabled = true;
		}
		this.activeCoroutine = false;
		Audio.self.playOneShot("ada9cc91-6da6-44b7-a098-3633e94c1806", 1f);
		yield break;
	}

	// Token: 0x0400172C RID: 5932
	public Transform screen;

	// Token: 0x0400172D RID: 5933
	public float maxSpringJointDistance = 2f;

	// Token: 0x0400172E RID: 5934
	[Header("Phone")]
	public Transform phone;

	// Token: 0x0400172F RID: 5935
	public float finalY;

	// Token: 0x04001730 RID: 5936
	public float slideTime;

	// Token: 0x04001731 RID: 5937
	[Header("Jumping")]
	public float distance = 0.6f;

	// Token: 0x04001732 RID: 5938
	public float height = 0.8f;

	// Token: 0x04001733 RID: 5939
	public float stepHeight = 0.43f;

	// Token: 0x04001734 RID: 5940
	public float time = 0.6f;

	// Token: 0x04001735 RID: 5941
	[Header("Jumping into seat")]
	public Transform handle;

	// Token: 0x04001736 RID: 5942
	public Vector2 seatPosition;

	// Token: 0x04001737 RID: 5943
	public float jumpHeight;

	// Token: 0x04001738 RID: 5944
	public float jumpTime = 0.7f;

	// Token: 0x04001739 RID: 5945
	private bool activeCoroutine;

	// Token: 0x0400173A RID: 5946
	private bool dragged;

	// Token: 0x0400173B RID: 5947
	private SpringJoint2D spring;

	// Token: 0x0400173C RID: 5948
	private int topSortingLayerId;

	// Token: 0x0400173D RID: 5949
	private bool movingPlaying;
}
