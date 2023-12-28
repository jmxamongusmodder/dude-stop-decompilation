using System;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
public class PuzzleDogStick_Stick : MonoBehaviour
{
	// Token: 0x060019C7 RID: 6599 RVA: 0x00062260 File Offset: 0x00060660
	private void Start()
	{
		this.startPosition = base.transform.position;
		this.startingAngle = base.transform.eulerAngles.z;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		base.GetComponent<Rigidbody2D>().AddForce(this.force);
		base.GetComponent<Rigidbody2D>().AddTorque(this.torque);
		Audio.self.playOneShot("3d20b0f2-c780-4928-b6cb-4aadf28144a5", 1f);
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x000622E0 File Offset: 0x000606E0
	public void Return()
	{
		base.gameObject.SetActive(true);
		base.transform.position = this.startPosition;
		base.transform.rotation = Quaternion.Euler(0f, 0f, this.startingAngle);
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		this.GetPuzzleStats().goBadAfterTime = false;
	}

	// Token: 0x040017D5 RID: 6101
	public Vector2 force;

	// Token: 0x040017D6 RID: 6102
	public float torque;

	// Token: 0x040017D7 RID: 6103
	public float waitBeforeShout = 1.5f;

	// Token: 0x040017D8 RID: 6104
	private Vector2 startPosition;

	// Token: 0x040017D9 RID: 6105
	private float startingAngle;
}
