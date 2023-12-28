using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003F2 RID: 1010
public class PuzzleDeodorant_Human : Draggable, TransitionProcessor
{
	// Token: 0x06001982 RID: 6530 RVA: 0x0005F759 File Offset: 0x0005DB59
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x0005F764 File Offset: 0x0005DB64
	private void Awake()
	{
		this.rends = base.GetComponentsInChildren<SpriteRenderer>(true);
		this.particles = base.transform.parent.GetComponentInChildren<ParticleSystemRenderer>();
		this.particleSystem = base.transform.parent.GetComponentInChildren<ParticleSystem>();
		this.doorColl = this.door.GetComponent<Collider2D>();
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x0005F7BC File Offset: 0x0005DBBC
	private void Start()
	{
		this.allParticles = new ParticleSystem.Particle[this.particleSystem.main.maxParticles];
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x0005F7E7 File Offset: 0x0005DBE7
	private void Update()
	{
		this.CheckRotation();
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x0005F7F0 File Offset: 0x0005DBF0
	private void OnEnable()
	{
		Vector2 vector = Camera.main.WorldToViewportPoint(this.doorColl.bounds.max);
		foreach (SpriteRenderer spriteRenderer in this.rends)
		{
			spriteRenderer.material.SetFloat("_Top", 0f);
			spriteRenderer.material.SetFloat("_Left", vector.x);
			spriteRenderer.material.SetFloat("_Angle", 1.5707964f);
			spriteRenderer.material.SetFloat("_Distance", 0f);
		}
		this.particles.material.SetFloat("_Top", 0f);
		this.particles.material.SetFloat("_Left", vector.x);
		this.particles.material.SetFloat("_Angle", 1.5707964f);
		this.particles.material.SetFloat("_Distance", 0f);
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x0005F904 File Offset: 0x0005DD04
	private void OnDisable()
	{
		foreach (SpriteRenderer spriteRenderer in this.rends)
		{
			spriteRenderer.material.SetFloat("_Left", 1f);
		}
		this.particles.material.SetFloat("_Left", 1f);
		this.lastX = base.transform.position.x;
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x0005F978 File Offset: 0x0005DD78
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (base.transform.position.x > this.doorColl.bounds.max.x + 3f)
		{
			this.Finish();
		}
		else if (base.transform.position.x > this.doorColl.bounds.min.x)
		{
			base.StartCoroutine(this.SnapCoroutine());
		}
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x0005FA11 File Offset: 0x0005DE11
	public void BadHuman()
	{
		base.GetComponent<Animator>().SetBool("showBadHat", true);
		this.bad = true;
		Audio.self.playOneShot("49ab76e4-92d8-4777-bdf8-2879a3053d52", 1f);
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x0005FA40 File Offset: 0x0005DE40
	public void GoodHuman()
	{
		base.GetComponent<Animator>().applyRootMotion = true;
		base.GetComponent<Animator>().SetBool("showHat", true);
		base.GetComponent<Draggable>().dragEnabled = true;
		Audio.self.playOneShot("49ab76e4-92d8-4777-bdf8-2879a3053d52", 1f);
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x0005FA80 File Offset: 0x0005DE80
	private void CheckRotation()
	{
		float num = this.lastRotationPosition - base.transform.position.x;
		if (this.dragged && num != 0f)
		{
			this.lastRotationPosition = base.transform.position.x;
			float num2 = num * this.rotationPerUnit + base.transform.eulerAngles.z;
			num2 = this.ClampAngle(num2, -this.maxAngle, this.maxAngle);
			base.transform.rotation = Quaternion.Euler(0f, 0f, num2);
		}
		else if (Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 0f)) > 0.01f)
		{
			float z = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, 0f, this.rotationReturnSpeed * Time.deltaTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
		}
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x0005FBA0 File Offset: 0x0005DFA0
	private float ClampAngle(float angle, float min, float max)
	{
		if (angle < 90f || angle > 270f)
		{
			if (angle > 180f)
			{
				angle -= 360f;
			}
			if (max > 180f)
			{
				max -= 360f;
			}
			if (min > 180f)
			{
				min -= 360f;
			}
		}
		angle = Mathf.Clamp(angle, min, max);
		if (angle < 0f)
		{
			angle += 360f;
		}
		return angle;
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x0005FC20 File Offset: 0x0005E020
	private void Finish()
	{
		this.dragEnabled = false;
		this.GetComponentInPuzzleStats<PuzzleDeodorant_Can>().GetComponent<Collider2D>().enabled = false;
		Audio.self.playOneShot("af585774-2d7a-46a2-ab38-010b196ea92c", 1f);
		if (this.bad)
		{
			Global.setCompletionState(CompletionState.Monster, null);
		}
		else
		{
			Global.setCompletionState(CompletionState.Good, null);
		}
		this.GetComponentInPuzzleStats<PuzzleDeodorant_Door>().Close();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x0005FC94 File Offset: 0x0005E094
	private IEnumerator SnapCoroutine()
	{
		Global.self.canBePaused = false;
		this.dragEnabled = false;
		Global.PauseArrows(1.5f);
		while (base.transform.position.x < this.doorColl.bounds.max.x + 3f)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, new Vector2(this.doorColl.bounds.max.x + 3f, base.transform.position.y), this.snapDoorSpeed * Time.deltaTime);
			yield return null;
		}
		this.Finish();
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x0005FCB0 File Offset: 0x0005E0B0
	public void TransitionUpdate()
	{
		if (this.doorColl != null)
		{
			Vector2 vector = Camera.main.WorldToViewportPoint(this.doorColl.bounds.max);
			foreach (SpriteRenderer spriteRenderer in this.rends)
			{
				spriteRenderer.material.SetFloat("_Top", 0f);
				spriteRenderer.material.SetFloat("_Left", vector.x);
				spriteRenderer.material.SetFloat("_Angle", 1.5707964f);
				spriteRenderer.material.SetFloat("_Distance", 0f);
			}
		}
		if (this.allParticles != null)
		{
			float x = base.transform.position.x - this.lastX;
			this.lastX = base.transform.position.x;
			Array.Clear(this.allParticles, 0, this.allParticles.Length);
			this.particleSystem.GetParticles(this.allParticles);
			for (int j = 0; j < this.allParticles.Length; j++)
			{
				ParticleSystem.Particle[] array2 = this.allParticles;
				int num = j;
				array2[num].position = array2[num].position + new Vector3(x, 0f);
			}
			this.particleSystem.SetParticles(this.allParticles, this.allParticles.Length);
		}
	}

	// Token: 0x0400178A RID: 6026
	[Header("Rotation stuff")]
	public float maxAngle;

	// Token: 0x0400178B RID: 6027
	public float rotationPerUnit;

	// Token: 0x0400178C RID: 6028
	public float rotationReturnSpeed;

	// Token: 0x0400178D RID: 6029
	private float lastRotationPosition;

	// Token: 0x0400178E RID: 6030
	[Header("Door stuff")]
	public Transform door;

	// Token: 0x0400178F RID: 6031
	public float snapDoorSpeed;

	// Token: 0x04001790 RID: 6032
	private Collider2D doorColl;

	// Token: 0x04001791 RID: 6033
	private bool bad;

	// Token: 0x04001792 RID: 6034
	private SpriteRenderer[] rends;

	// Token: 0x04001793 RID: 6035
	private ParticleSystemRenderer particles;

	// Token: 0x04001794 RID: 6036
	private ParticleSystem particleSystem;

	// Token: 0x04001795 RID: 6037
	private ParticleSystem.Particle[] allParticles;

	// Token: 0x04001796 RID: 6038
	private float lastX;
}
