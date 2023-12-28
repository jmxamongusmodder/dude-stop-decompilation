using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200042E RID: 1070
public class PuzzleMonaLisa_Pen : DrawingPen
{
	// Token: 0x06001B41 RID: 6977 RVA: 0x0006F790 File Offset: 0x0006DB90
	private void Start()
	{
		base.StartCoroutine(this.PixelCountingCoroutine());
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x0006F79F File Offset: 0x0006DB9F
	public override void OnDisable()
	{
		base.OnDisable();
		if (this.pixelCount > 0)
		{
			Audio.self.stopLoopSound("8ec7cf8b-7931-4891-afa1-2b909b43011e", true);
		}
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x0006F7C4 File Offset: 0x0006DBC4
	public override void PixelDrawn(DrawingCanvas paper, int pixelsDrawn)
	{
		if (pixelsDrawn > 0)
		{
			this.GetPuzzleStats().goBadAfterTime = true;
			for (int i = 0; i < pixelsDrawn; i++)
			{
				this.pixels.Add(0f);
			}
		}
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x0006F808 File Offset: 0x0006DC08
	private IEnumerator PixelCountingCoroutine()
	{
		for (;;)
		{
			for (int i = 0; i < this.pixels.Count; i++)
			{
				List<float> list;
				int index;
				(list = this.pixels)[index = i] = list[index] + Time.deltaTime;
			}
			this.pixels.RemoveAll((float x) => x > this.cooldown);
			if (this.pixels.Count == 0 && this.pixelCount > 0)
			{
				Audio.self.stopLoopSound("8ec7cf8b-7931-4891-afa1-2b909b43011e", true);
			}
			else if (this.pixels.Count > 0 && this.pixelCount == 0)
			{
				Audio.self.playLoopSound("8ec7cf8b-7931-4891-afa1-2b909b43011e");
			}
			this.pixelCount = this.pixels.Count;
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001976 RID: 6518
	public float cooldown = 0.5f;

	// Token: 0x04001977 RID: 6519
	private int pixelCount;

	// Token: 0x04001978 RID: 6520
	private List<float> pixels = new List<float>();
}
