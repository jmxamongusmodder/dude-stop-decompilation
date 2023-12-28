using System;
using UnityEngine;

// Token: 0x020003F5 RID: 1013
public class PuzzleDogEatsShoes_Shoe : MonoBehaviour
{
	// Token: 0x060019B9 RID: 6585 RVA: 0x00061B5E File Offset: 0x0005FF5E
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "GlobalCollider")
		{
			Audio.self.playOneShot("3f0cdc3b-2696-45b0-b9ea-6001e73816a8", 1f);
		}
	}
}
