using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003C1 RID: 961
public class PuzzleBarkAtNight_Dog_animation : MonoBehaviour
{
	// Token: 0x060017F9 RID: 6137 RVA: 0x00052974 File Offset: 0x00050D74
	private void Awake()
	{
		bool? flag = PuzzleBarkAtNight_Dog_animation.showFlyingDog;
		if (flag == null)
		{
			PuzzleBarkAtNight_Dog_animation.showFlyingDog = new bool?(true);
		}
		base.GetComponent<Animator>().enabled = false;
		if (PuzzleBarkAtNight_Dog_animation.showFlyingDog == true)
		{
			this.dog.gameObject.SetActive(false);
			base.gameObject.SetActive(true);
		}
		else
		{
			this.dog.gameObject.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x00052A0C File Offset: 0x00050E0C
	private void Start()
	{
		if (PuzzleBarkAtNight_Dog_animation.showFlyingDog == true)
		{
			base.StartCoroutine(this.playAnimation());
		}
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x00052A48 File Offset: 0x00050E48
	private IEnumerator playAnimation()
	{
		yield return new WaitForSeconds(1f);
		base.GetComponent<Animator>().enabled = true;
		Audio.self.playOneShot("6b17985b-0c08-4bb7-905e-7f5b146a212d", 1f);
		yield break;
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x00052A63 File Offset: 0x00050E63
	public void endAnimation()
	{
		this.dog.gameObject.SetActive(true);
		base.gameObject.SetActive(false);
		this.dog.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		PuzzleBarkAtNight_Dog_animation.showFlyingDog = new bool?(false);
	}

	// Token: 0x040015E9 RID: 5609
	public static bool? showFlyingDog;

	// Token: 0x040015EA RID: 5610
	public Transform dog;
}
