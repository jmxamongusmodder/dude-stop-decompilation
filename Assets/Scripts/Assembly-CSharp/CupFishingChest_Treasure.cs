using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class CupFishingChest_Treasure : MonoBehaviour
{
	// Token: 0x060014B4 RID: 5300 RVA: 0x00039CC0 File Offset: 0x000380C0
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		this.sprite = base.GetComponent<SpriteRenderer>();
		this.sprite.enabled = true;
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.particles.transform);
		transform.SetParent(base.transform.parent);
		transform.GetComponent<ParticleSystem>().main.simulationSpace = ParticleSystemSimulationSpace.World;
		transform.position = new Vector3(base.transform.position.x, transform.position.y);
		this.localParticles = transform.GetComponent<ParticleSystem>();
		base.StartCoroutine(this.waitToDrop());
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x00039D70 File Offset: 0x00038170
	private void Update()
	{
		if (base.transform.position.y < 0f && !GeometryUtility.TestPlanesAABB(this.planes, this.sprite.bounds))
		{
			CupFishingChest_Treasure[] componentsInPuzzleStats = this.GetComponentsInPuzzleStats(false);
			if (componentsInPuzzleStats.Length == 1)
			{
				if (!Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>().canShowRod)
				{
					return;
				}
				this.GetComponentInPuzzleStats(true).Activate();
			}
			UnityEngine.Object.Destroy(this.localParticles.gameObject, 3f);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x00039E0C File Offset: 0x0003820C
	private IEnumerator waitToDrop()
	{
		while (Global.self.currPuzzle == null)
		{
			yield return null;
		}
		AudioVoice_CupFishingChest voice = Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>();
		while (!voice.canDropCoins)
		{
			yield return null;
		}
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		base.GetComponent<Rigidbody2D>().AddTorque(this.torque);
		yield break;
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x00039E27 File Offset: 0x00038227
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "GlobalCollider")
		{
			Audio.self.playOneShot("a6e1a698-b590-45f5-9944-22cff03ddb5d", 1f);
			this.localParticles.Emit();
		}
	}

	// Token: 0x0400123F RID: 4671
	public ParticleSystem particles;

	// Token: 0x04001240 RID: 4672
	public float torque;

	// Token: 0x04001241 RID: 4673
	private ParticleSystem localParticles;

	// Token: 0x04001242 RID: 4674
	private Plane[] planes;

	// Token: 0x04001243 RID: 4675
	private SpriteRenderer sprite;
}
