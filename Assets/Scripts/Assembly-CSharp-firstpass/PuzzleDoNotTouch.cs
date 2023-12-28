using System;
using UnityEngine;

// Token: 0x020003FD RID: 1021
public class PuzzleDoNotTouch : MonoBehaviour
{
	// Token: 0x060019E9 RID: 6633 RVA: 0x00062FF0 File Offset: 0x000613F0
	private void OnMouseDown()
	{
		Audio.self.playOneShot("86c02198-04aa-4ddf-96fb-4bba618dab67", 1f);
		base.GetComponent<Rigidbody2D>().AddTorque(this.clickForce * (float)(++this.clicks) * (float)((this.clicks % 2 != 1) ? -1 : 1));
		this.GetPuzzleStats().GetComponent<AudioVoice_DoNotTouch>().touched();
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x00063060 File Offset: 0x00061460
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag != "SuccessCollider")
		{
			return;
		}
		this.table.GetComponent<Collider2D>().enabled = false;
		base.GetComponent<Collider2D>().enabled = false;
		base.transform.gameObject.SetActive(false);
		this.parts.transform.position = base.transform.GetChild(0).position;
		this.parts.rotation = base.transform.GetChild(0).rotation;
		this.parts.gameObject.SetActive(true);
		int num = 0;
		foreach (Rigidbody2D rigidbody2D in this.parts.GetComponentsInChildren<Rigidbody2D>())
		{
			rigidbody2D.gameObject.layer = ((num++ % 2 != 0) ? LayerMask.NameToLayer("Back") : LayerMask.NameToLayer("Front"));
			rigidbody2D.AddForce(new Vector2(UnityEngine.Random.Range(-this.partsForce, this.partsForce), UnityEngine.Random.Range(0f, this.partsForce)));
		}
		foreach (PhysicsSound physicsSound in this.parts.GetComponentsInChildren<PhysicsSound>())
		{
			physicsSound.enabled = true;
		}
		this.particles.transform.position = base.transform.position;
		this.particles.Emit();
		Audio.self.playOneShot("1d1897aa-8a16-4cf9-8772-0777c5599fab", other.relativeVelocity.magnitude * 0.1f);
		this.GetPuzzleStats().GetComponent<AudioVoice_DoNotTouch>().finishLevel();
		Global.LevelCompleted(this.waitTime, true);
	}

	// Token: 0x040017F3 RID: 6131
	public Transform table;

	// Token: 0x040017F4 RID: 6132
	public float clickForce;

	// Token: 0x040017F5 RID: 6133
	public Transform parts;

	// Token: 0x040017F6 RID: 6134
	public float partsForce;

	// Token: 0x040017F7 RID: 6135
	public float waitTime = 0.5f;

	// Token: 0x040017F8 RID: 6136
	public ParticleSystem particles;

	// Token: 0x040017F9 RID: 6137
	private int clicks;
}
