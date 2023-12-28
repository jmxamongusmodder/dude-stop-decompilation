using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200054C RID: 1356
[RequireComponent(typeof(SpriteRenderer))]
public class Ghost : MonoBehaviour
{
	// Token: 0x06001F1E RID: 7966 RVA: 0x00093D2C File Offset: 0x0009212C
	private void Awake()
	{
		base.transform.position = new Vector2(UnityEngine.Random.value * 20f - 10f, ((double)UnityEngine.Random.value <= 0.5) ? 7f : -7f);
		base.StartCoroutine(this.Move());
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x00093D90 File Offset: 0x00092190
	private IEnumerator Move()
	{
		SpriteRenderer spr = base.GetComponent<SpriteRenderer>();
		spr.sprite = this.list.GetRandom<Sprite>();
		spr.enabled = false;
		base.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(base.transform.position.y, base.transform.position.x) * 57.29578f + 90f, Vector3.forward);
		yield return null;
		yield return null;
		while (!Global.self.NoCurrentTransition)
		{
			yield return null;
		}
		spr.enabled = true;
		Vector2 target = UnityEngine.Random.insideUnitCircle.normalized * this.firstTargetDist;
		for (;;)
		{
			if (!this.collected)
			{
				if (Vector2.Distance(base.transform.position, target) < this.maxAllowedDist)
				{
					target = UnityEngine.Random.insideUnitCircle.normalized * this.secondTargetDist;
				}
				Vector2 lhs = base.transform.position - target;
				if (lhs != Vector2.zero)
				{
					float angle = Mathf.Atan2(lhs.y, lhs.x) * 57.29578f + 90f;
					base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * this.rotationSpeed);
				}
				Vector2 a = new Vector2(Mathf.Cos((base.transform.rotation.eulerAngles.z - 90f) * 0.017453292f), Mathf.Sin((base.transform.rotation.eulerAngles.z - 90f) * 0.017453292f));
				Vector2 vector = base.transform.position;
				vector -= a * Time.deltaTime * this.speed;
				base.transform.position = vector;
			}
			this.maxAliveTime -= Time.deltaTime;
			if (this.maxAliveTime <= 0f)
			{
				this.isActive = false;
			}
			if (!this.isActive)
			{
				Color color = spr.color;
				color.a -= Time.deltaTime * this.hideSpeed * ((!this.collected) ? 1f : 5f);
				spr.color = color;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x00093DAC File Offset: 0x000921AC
	private void OnMouseDown()
	{
		if (Global.self.NoCurrentTransition && this.maxAliveTime > 0f && !this.collected && this.isActive)
		{
			this.particles.Play();
			this.isActive = false;
			this.collected = true;
			Audio.self.playOneShot("6ae05f90-7842-4e0f-a832-257de8312d25", 1f);
			if (++Global.self.ghostCountCurrent >= Global.self.ghostCountMax)
			{
				Global.self.GetCup(AwardName.Halloween);
			}
		}
	}

	// Token: 0x0400225A RID: 8794
	public ParticleSystem particles;

	// Token: 0x0400225B RID: 8795
	public float maxAliveTime = 3f;

	// Token: 0x0400225C RID: 8796
	private bool collected;

	// Token: 0x0400225D RID: 8797
	private bool isActive = true;

	// Token: 0x0400225E RID: 8798
	[Header("Movement")]
	public float speed = 1f;

	// Token: 0x0400225F RID: 8799
	public float hideSpeed = 1f;

	// Token: 0x04002260 RID: 8800
	public float rotationSpeed = 0.5f;

	// Token: 0x04002261 RID: 8801
	public float maxAllowedDist = 0.1f;

	// Token: 0x04002262 RID: 8802
	public float firstTargetDist = 2f;

	// Token: 0x04002263 RID: 8803
	public float secondTargetDist = 2f;

	// Token: 0x04002264 RID: 8804
	[Header("Sprite List")]
	public Sprite[] list;
}
