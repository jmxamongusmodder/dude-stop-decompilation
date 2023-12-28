using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B9 RID: 953
public class PuzzleWrongDress : Draggable
{
	// Token: 0x060017BC RID: 6076 RVA: 0x00050144 File Offset: 0x0004E544
	private void OnDrawGizmos()
	{
		Color color;
		if (this.type == PuzzleWrongDress.Type.Shoes)
		{
			color = Color.blue;
		}
		else
		{
			color = Color.black;
		}
		foreach (Vector2 point in this.points)
		{
			GizmosExtension.DrawPoint(point, color, 0.5f);
		}
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x000501C4 File Offset: 0x0004E5C4
	private void Start()
	{
		foreach (Vector2 point in this.points)
		{
			base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, point, this.snapDist), false);
		}
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x00050230 File Offset: 0x0004E630
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (base.Snapped())
		{
			Audio.self.playOneShot("0d988fa3-db33-45f9-a236-007a64ca1c0c", 1f);
		}
		foreach (PuzzleWrongDress puzzleWrongDress in base.transform.parent.GetComponentsInChildren<PuzzleWrongDress>())
		{
			if (!(puzzleWrongDress == this))
			{
				if (puzzleWrongDress.Snapped() && base.Snapped())
				{
					if (puzzleWrongDress.set == this.set)
					{
						Global.LevelFailed(0f, true);
					}
					else
					{
						Global.LevelCompleted(0f, true);
					}
				}
				if (puzzleWrongDress.type == this.type)
				{
					puzzleWrongDress.snapEnabled = !base.Snapped();
				}
			}
		}
	}

	// Token: 0x04001580 RID: 5504
	public List<Vector2> points;

	// Token: 0x04001581 RID: 5505
	public float snapDist;

	// Token: 0x04001582 RID: 5506
	public PuzzleWrongDress.Type type;

	// Token: 0x04001583 RID: 5507
	public PuzzleWrongDress.Set set;

	// Token: 0x020003BA RID: 954
	public enum Type
	{
		// Token: 0x04001585 RID: 5509
		Upperwear,
		// Token: 0x04001586 RID: 5510
		Shoes
	}

	// Token: 0x020003BB RID: 955
	public enum Set
	{
		// Token: 0x04001588 RID: 5512
		Dress,
		// Token: 0x04001589 RID: 5513
		Coat
	}
}
