using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000440 RID: 1088
public class PuzzleQueue_Queuer : MonoBehaviour
{
	// Token: 0x06001BC2 RID: 7106 RVA: 0x00073F5D File Offset: 0x0007235D
	private void Awake()
	{
		if (this.speechBubble != null)
		{
			this.speechBubble.SetActive(false);
		}
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x00073F7C File Offset: 0x0007237C
	private void Start()
	{
		this.face = (this.originalFace = base.transform.GetChild(0).Find("face"));
		this.coll = base.GetComponent<CircleCollider2D>();
		this.originalRadius = this.coll.radius;
		this.container = base.transform.GetChild(0);
		this.container.SetParent(base.transform.parent);
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x00073FF4 File Offset: 0x000723F4
	private void Update()
	{
		this.container.position = Vector3.MoveTowards(this.container.position, base.transform.position, Time.deltaTime * this.movementSpeed);
		if (this.beingPushed)
		{
			this.coll.radius = Mathf.MoveTowards(this.coll.radius, this.originalRadius * this.scaleColliderTo, this.scaleSpeed * Time.deltaTime);
			if (this.originalFace == this.face)
			{
				this.originalFace.gameObject.SetActive(false);
				int index = UnityEngine.Random.Range(0, this.angryFaces.transform.childCount - 1);
				this.face = UnityEngine.Object.Instantiate<Transform>(this.angryFaces.GetChild(index));
				this.face.SetParent(this.container, false);
				this.face.localPosition = Vector3.zero;
			}
			this.faceCooldown = 0.5f;
		}
		else
		{
			this.faceCooldown = Mathf.MoveTowards(this.faceCooldown, 0f, Time.deltaTime);
			if (this.faceCooldown < Mathf.Epsilon)
			{
				this.coll.radius = Mathf.MoveTowards(this.coll.radius, this.originalRadius, Time.deltaTime);
			}
			if (this.coll.radius == this.originalRadius && this.face != this.originalFace && this.faceCooldown == 0f)
			{
				UnityEngine.Object.Destroy(this.face.gameObject);
				this.face = this.originalFace;
				this.originalFace.gameObject.SetActive(true);
			}
		}
		this.beingPushed = false;
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x000741BC File Offset: 0x000725BC
	private void OnEnable()
	{
		base.StartCoroutine(this.DistanceJointEnablingCoroutine());
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x000741CB File Offset: 0x000725CB
	private void OnDisable()
	{
		base.GetComponent<DistanceJoint2D>().enabled = false;
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x000741D9 File Offset: 0x000725D9
	private void OnCollisionStay2D(Collision2D cn)
	{
		if (cn.gameObject.tag == "Player")
		{
			this.beingPushed = true;
			base.StartCoroutine(this.SpeechBubble());
		}
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x0007420C File Offset: 0x0007260C
	private void OnCollisionEnter2D(Collision2D cn)
	{
		if (cn.gameObject.tag != "Player")
		{
			return;
		}
		if (this.startingPosition == -Vector3.one)
		{
			this.startingPosition = base.transform.position;
		}
		if (cn.relativeVelocity.x > this.velocityThreshold)
		{
			this.coll.radius *= this.velocityScale;
		}
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x00074290 File Offset: 0x00072690
	private void OnCollisionExit2D(Collision2D cn)
	{
		if (cn.gameObject.tag == "Player")
		{
			this.beingPushed = false;
		}
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x000742B4 File Offset: 0x000726B4
	private IEnumerator DistanceJointEnablingCoroutine()
	{
		yield return new WaitForSeconds(0.1f);
		if (!base.enabled)
		{
			yield break;
		}
		base.GetComponent<DistanceJoint2D>().enabled = true;
		yield break;
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x000742D0 File Offset: 0x000726D0
	private IEnumerator SpeechBubble()
	{
		if (this.speechBubble == null || PuzzleQueue_Queuer.speechShowen)
		{
			yield break;
		}
		this.speechBubble.SetActive(true);
		PuzzleQueue_Queuer.speechShowen = true;
		yield return new WaitForSeconds(this.speechShowTime);
		this.speechBubble.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		PuzzleQueue_Queuer.speechShowen = false;
		yield break;
	}

	// Token: 0x04001A28 RID: 6696
	public Transform angryFaces;

	// Token: 0x04001A29 RID: 6697
	public float velocityThreshold = 10f;

	// Token: 0x04001A2A RID: 6698
	public float velocityScale = 0.5f;

	// Token: 0x04001A2B RID: 6699
	public float scaleColliderTo = 0.8f;

	// Token: 0x04001A2C RID: 6700
	public float scaleSpeed = 1f;

	// Token: 0x04001A2D RID: 6701
	public float movementSpeed = 1f;

	// Token: 0x04001A2E RID: 6702
	private Transform container;

	// Token: 0x04001A2F RID: 6703
	private Transform originalFace;

	// Token: 0x04001A30 RID: 6704
	private Transform face;

	// Token: 0x04001A31 RID: 6705
	private CircleCollider2D coll;

	// Token: 0x04001A32 RID: 6706
	private float originalRadius;

	// Token: 0x04001A33 RID: 6707
	private bool beingPushed;

	// Token: 0x04001A34 RID: 6708
	private Vector3 startingPosition = -Vector3.one;

	// Token: 0x04001A35 RID: 6709
	private float faceCooldown;

	// Token: 0x04001A36 RID: 6710
	[Header("Speech Bubbles")]
	public GameObject speechBubble;

	// Token: 0x04001A37 RID: 6711
	public float speechShowTime = 3f;

	// Token: 0x04001A38 RID: 6712
	private static bool speechShowen;
}
