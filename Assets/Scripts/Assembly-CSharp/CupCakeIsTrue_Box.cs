using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class CupCakeIsTrue_Box : Draggable
{
	// Token: 0x06000045 RID: 69 RVA: 0x00004BE5 File Offset: 0x00002DE5
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00004BF7 File Offset: 0x00002DF7
	private void Update()
	{
		this.CheckLockAndPosition();
		this.CheckVisibility();
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00004C08 File Offset: 0x00002E08
	private void CheckLockAndPosition()
	{
		if (!this.lockX)
		{
			return;
		}
		if (base.transform.position.y < this.thresholdY)
		{
			return;
		}
		this.lockX = false;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		base.gameObject.layer = LayerMask.NameToLayer("Individual");
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00004C74 File Offset: 0x00002E74
	private void CheckVisibility()
	{
		if (!this.lockX && !GeometryUtility.TestPlanesAABB(this.planes, base.GetComponent<SpriteRenderer>().bounds))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040000B7 RID: 183
	public float thresholdY;

	// Token: 0x040000B8 RID: 184
	private Plane[] planes;
}
