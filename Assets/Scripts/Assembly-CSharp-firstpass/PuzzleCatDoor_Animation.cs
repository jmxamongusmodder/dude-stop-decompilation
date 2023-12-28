using System;
using UnityEngine;

// Token: 0x020003DA RID: 986
public class PuzzleCatDoor_Animation : MonoBehaviour
{
	// Token: 0x060018CA RID: 6346 RVA: 0x00058B0C File Offset: 0x00056F0C
	private void Start()
	{
		base.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_Angle", 0f);
		base.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_Distance", 0f);
		base.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_Left", 0f);
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x00058B68 File Offset: 0x00056F68
	public void BreakGlass()
	{
		int num = 0;
		foreach (Rigidbody2D rigidbody2D in this.glassPieces.GetComponentsInChildren<Rigidbody2D>())
		{
			rigidbody2D.isKinematic = false;
			rigidbody2D.gameObject.layer = ((num++ % 2 != 0) ? LayerMask.NameToLayer("Back") : LayerMask.NameToLayer("Front"));
			Vector2 force = new Vector2(UnityEngine.Random.Range(-this.partsForce, this.partsForce), UnityEngine.Random.Range(0f, this.partsForce));
			rigidbody2D.AddForce(force);
		}
		this.particles.gameObject.SetActive(true);
		this.particles.Play();
	}

	// Token: 0x040016B5 RID: 5813
	public Transform glassPieces;

	// Token: 0x040016B6 RID: 5814
	public float partsForce;

	// Token: 0x040016B7 RID: 5815
	public ParticleSystem particles;
}
