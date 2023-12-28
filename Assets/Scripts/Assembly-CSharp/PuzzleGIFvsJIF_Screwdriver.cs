using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000413 RID: 1043
public class PuzzleGIFvsJIF_Screwdriver : PivotDraggable
{
	// Token: 0x06001A74 RID: 6772 RVA: 0x00067E0C File Offset: 0x0006620C
	private void Start()
	{
		this.startingAngle = base.transform.eulerAngles.z;
		base.StartCoroutine(this.BlinkBomb());
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x00067E40 File Offset: 0x00066240
	private IEnumerator BlinkBomb()
	{
		float time = 0f;
		float speed = 1f;
		bool faster = false;
		Color c = this.blink.color;
		while (!this.defused.activeInHierarchy)
		{
			if (this.blinkFaster)
			{
				this.blinkFaster = false;
				faster = true;
				time = 0f;
			}
			time += Time.deltaTime * speed;
			if (faster)
			{
				speed = Mathf.MoveTowards(speed, this.blickFasterSpeed, Time.deltaTime * 1.5f);
			}
			if (time > 1f)
			{
				time -= 1f;
				if (!faster)
				{
					Audio.self.playOneShot("54beadf1-8999-4e02-8690-fa04447aca05", 1f);
				}
			}
			c.a = this.blinkCurve.Evaluate(time);
			this.blink.color = c;
			yield return null;
		}
		while (c.a > 0f)
		{
			c.a -= Time.deltaTime;
			this.blink.color = c;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x00067E5C File Offset: 0x0006625C
	private void Update()
	{
		if (base.transform.eulerAngles.z != this.angle || base.transform.eulerAngles.z != this.startingAngle)
		{
			float target = (!this.dragged) ? this.startingAngle : this.angle;
			float z = Mathf.MoveTowards(base.transform.eulerAngles.z, target, this.changeSpeed * Time.deltaTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
		}
		if (!this.dragged && base.WasMoved())
		{
			this.returnTimer = Mathf.MoveTowards(this.returnTimer, this.returnTime, Time.deltaTime);
			float t = Mathf.Sin(this.returnTimer / this.returnTime * 3.1415927f * 0.5f);
			base.transform.position = Vector2.Lerp(this.returnPosition, this.startingPosition, t);
		}
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x00067F7E File Offset: 0x0006637E
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		base.GetComponent<BoxCollider2D>().isTrigger = true;
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x00067F94 File Offset: 0x00066394
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		this.returnTimer = 0f;
		this.returnPosition = base.transform.position;
		base.GetComponent<BoxCollider2D>().isTrigger = false;
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	// Token: 0x04001893 RID: 6291
	[Header("Screwdriver stuff")]
	public Transform bomb;

	// Token: 0x04001894 RID: 6292
	public float angle;

	// Token: 0x04001895 RID: 6293
	public float changeSpeed;

	// Token: 0x04001896 RID: 6294
	public float returnTime;

	// Token: 0x04001897 RID: 6295
	private Vector2 returnPosition;

	// Token: 0x04001898 RID: 6296
	private float startingAngle;

	// Token: 0x04001899 RID: 6297
	private float returnTimer;

	// Token: 0x0400189A RID: 6298
	[Space(20f)]
	public GameObject defused;

	// Token: 0x0400189B RID: 6299
	public SpriteRenderer blink;

	// Token: 0x0400189C RID: 6300
	public AnimationCurve blinkCurve;

	// Token: 0x0400189D RID: 6301
	[HideInInspector]
	public bool blinkFaster;

	// Token: 0x0400189E RID: 6302
	public float blickFasterSpeed = 3f;
}
