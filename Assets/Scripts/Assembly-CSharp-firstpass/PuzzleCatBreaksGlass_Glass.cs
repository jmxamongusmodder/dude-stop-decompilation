using System;
using UnityEngine;

// Token: 0x020003D8 RID: 984
public class PuzzleCatBreaksGlass_Glass : MonoBehaviour
{
	// Token: 0x060018B8 RID: 6328 RVA: 0x000581B4 File Offset: 0x000565B4
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.transform.tag == "GlobalCollider")
		{
			base.GetComponent<Rigidbody2D>().simulated = false;
			base.GetComponent<Collider2D>().enabled = false;
			base.transform.GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(1).gameObject.SetActive(true);
			int num = 0;
			foreach (Rigidbody2D rigidbody2D in base.transform.GetChild(1).GetComponentsInChildren<Rigidbody2D>())
			{
				rigidbody2D.gameObject.layer = ((num++ % 2 != 0) ? LayerMask.NameToLayer("Back") : LayerMask.NameToLayer("Front"));
				rigidbody2D.AddForce(new Vector2(UnityEngine.Random.Range(-this.force, this.force), UnityEngine.Random.Range(0f, this.force)));
			}
			this.cat.GetComponent<PuzzleCatBreaksGlass_Cat>().glassShattered = true;
			this.particles.Emit();
			Audio.self.playOneShot("1892f903-b04f-4997-80c3-e27885e3a7ce", 1f);
		}
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x000582DC File Offset: 0x000566DC
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform == this.cat)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Back");
			base.GetComponent<Rigidbody2D>().AddTorque(this.torque);
			Audio.self.playOneShot("30fa8e36-a22e-4f7a-9126-37c83c1b5ce1", 1f);
			Global.self.currPuzzle.GetComponent<AudioVoice_CatBreakGlass>().catBreakGlass();
		}
	}

	// Token: 0x040016A6 RID: 5798
	public Transform cat;

	// Token: 0x040016A7 RID: 5799
	public float force;

	// Token: 0x040016A8 RID: 5800
	public float torque;

	// Token: 0x040016A9 RID: 5801
	public ParticleSystem particles;
}
