using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x020003CC RID: 972
public class PuzzleBoxOrHouse_BoxLid : MonoBehaviour
{
	// Token: 0x06001854 RID: 6228 RVA: 0x0005506C File Offset: 0x0005346C
	private void Awake()
	{
		base.transform.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_Alpha", 0f);
		base.transform.GetComponentInChildren<SpriteRenderer>().material.SetColor("_SpriteColor", this.color);
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x000550B8 File Offset: 0x000534B8
	private void Start()
	{
		this.otherLid = (from x in this.GetComponentsInPuzzleStats(false)
		where x != this
		select x).FirstOrDefault<PuzzleBoxOrHouse_BoxLid>();
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x000550DD File Offset: 0x000534DD
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!this.opened)
		{
			this.dragged = true;
		}
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x00055100 File Offset: 0x00053500
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = false;
		this.opened = ((!this.negate) ? (base.transform.eulerAngles.z > this.openAngle) : (base.transform.eulerAngles.z < this.openAngle));
		if (this.opened)
		{
			base.StartCoroutine(this.FallingCoroutine());
		}
		if (this.opened && this.otherLid.opened)
		{
			this.house.GetComponent<Draggable>().enabled = true;
			this.otherLid.enabled = false;
		}
		if (!this.hover)
		{
			this.OnMouseExit();
		}
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x000551CD File Offset: 0x000535CD
	private void OnMouseEnter()
	{
		if (base.enabled)
		{
			this.hover = true;
			base.transform.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_Alpha", this.whiten);
		}
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x00055201 File Offset: 0x00053601
	private void OnMouseExit()
	{
		this.hover = false;
		if (!this.dragged)
		{
			base.transform.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_Alpha", 0f);
		}
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x00055234 File Offset: 0x00053634
	private void Update()
	{
		if (!this.dragged)
		{
			return;
		}
		Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - base.transform.position;
		float num = 90f - Mathf.Atan2(vector.x, vector.y) * 57.29578f;
		if (this.negate)
		{
			num += 180f;
		}
		if (num < this.minAngle)
		{
			num = this.minAngle;
		}
		else if (num > this.maxAngle)
		{
			num = this.maxAngle;
		}
		float z = Mathf.MoveTowards(base.transform.eulerAngles.z, num, this.rotationSpeed * Time.deltaTime);
		base.transform.rotation = Quaternion.Euler(0f, 0f, z);
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x00055314 File Offset: 0x00053714
	private IEnumerator FallingCoroutine()
	{
		float fallingSpeed = 0f;
		float targetAngle = (!this.negate) ? this.maxAngle : this.minAngle;
		while (Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, targetAngle)) > 0.1f)
		{
			float angle = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, targetAngle, fallingSpeed);
			fallingSpeed += this.fallingAcceleration * Time.deltaTime;
			base.transform.rotation = Quaternion.Euler(0f, 0f, angle);
			yield return null;
		}
		Audio.self.playOneShot("2a0a724d-089c-4472-b9c3-1dffc1f4169e", 1f);
		this.animationLid.rotation = base.transform.rotation;
		base.enabled = false;
		yield break;
	}

	// Token: 0x0400163A RID: 5690
	public float minAngle;

	// Token: 0x0400163B RID: 5691
	public float maxAngle;

	// Token: 0x0400163C RID: 5692
	public float openAngle;

	// Token: 0x0400163D RID: 5693
	public float rotationSpeed;

	// Token: 0x0400163E RID: 5694
	public float fallingAcceleration;

	// Token: 0x0400163F RID: 5695
	public bool negate;

	// Token: 0x04001640 RID: 5696
	public Color color;

	// Token: 0x04001641 RID: 5697
	public float whiten = 0.7f;

	// Token: 0x04001642 RID: 5698
	public Transform animationLid;

	// Token: 0x04001643 RID: 5699
	public Transform house;

	// Token: 0x04001644 RID: 5700
	private float targetAngle;

	// Token: 0x04001645 RID: 5701
	private bool opened;

	// Token: 0x04001646 RID: 5702
	private bool dragged;

	// Token: 0x04001647 RID: 5703
	private PuzzleBoxOrHouse_BoxLid otherLid;

	// Token: 0x04001648 RID: 5704
	private bool hover;
}
