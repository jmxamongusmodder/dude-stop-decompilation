using System;
using UnityEngine;

// Token: 0x0200045E RID: 1118
[EnabledManually]
public class PuzzleSwipePhoto_Thumbnail : MonoBehaviour
{
	// Token: 0x1700006F RID: 111
	// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0007BFC6 File Offset: 0x0007A3C6
	public bool clickable
	{
		set
		{
			base.GetComponent<Collider2D>().enabled = value;
			if (value)
			{
				this.EnableBlending();
			}
			else
			{
				this.EnableBoxMask();
			}
		}
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x0007BFEB File Offset: 0x0007A3EB
	private void Start()
	{
		base.GetComponent<SpriteRenderer>().material.shader = this.box;
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x0007C003 File Offset: 0x0007A403
	private void Awake()
	{
		this.blend = Shader.Find("Custom/Blend");
		this.box = Shader.Find("Custom/Mask/Box");
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x0007C025 File Offset: 0x0007A425
	private void OnMouseEnter()
	{
		Audio.self.playOneShot("408e46a0-e30e-40b7-b4d6-8d24de011c01", 1f);
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x0007C03C File Offset: 0x0007A43C
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		Audio.self.playOneShot("8a742b64-581a-4208-a0b7-132e108fbb78", 1f);
		foreach (SpriteRenderer spriteRenderer in base.transform.parent.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.material.shader = this.box;
		}
		this.album.GetComponent<PuzzleSwipePhoto_Photo>().MoveIn();
		if (this.badAlbum)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_SwipePhoto>().OpenSecondAlbum();
		}
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x0007C0D4 File Offset: 0x0007A4D4
	private void EnableBlending()
	{
		foreach (SpriteRenderer spriteRenderer in base.transform.parent.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.material.shader = this.blend;
			base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
		}
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x0007C138 File Offset: 0x0007A538
	private void EnableBoxMask()
	{
		foreach (SpriteRenderer spriteRenderer in base.transform.parent.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.material.shader = this.box;
			base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
		}
	}

	// Token: 0x04001B42 RID: 6978
	public Transform album;

	// Token: 0x04001B43 RID: 6979
	public bool onScreen;

	// Token: 0x04001B44 RID: 6980
	private Shader blend;

	// Token: 0x04001B45 RID: 6981
	private Shader box;

	// Token: 0x04001B46 RID: 6982
	public bool badAlbum;
}
