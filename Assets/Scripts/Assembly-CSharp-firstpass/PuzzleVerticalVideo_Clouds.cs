using System;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class PuzzleVerticalVideo_Clouds : MonoBehaviour, TransitionProcessor
{
	// Token: 0x06001CF2 RID: 7410 RVA: 0x0007DBA4 File Offset: 0x0007BFA4
	private void Awake()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>(true))
		{
			spriteRenderer.enabled = !this.hidden;
			spriteRenderer.material.SetFloat("_Top", 0f);
			spriteRenderer.material.SetFloat("_Angle", 1.5707964f);
			spriteRenderer.material.SetFloat("_Distance", 0f);
		}
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x0007DC30 File Offset: 0x0007C030
	private void Update()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, base.transform.position + base.transform.right, Time.deltaTime * this.speed);
		bool flag = false;
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			bool flag2 = GeometryUtility.TestPlanesAABB(this.planes, spriteRenderer.bounds);
			spriteRenderer.enabled = flag2;
			flag = (flag || flag2);
		}
		if (flag && !this.appeared)
		{
			this.appeared = true;
		}
		else if (!flag && this.appeared)
		{
			base.transform.position -= 2f * this.distance * base.transform.right;
			this.appeared = false;
		}
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x0007DD28 File Offset: 0x0007C128
	private void UpdateCloudLines()
	{
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			float x = Camera.main.WorldToViewportPoint(this.line.position).x;
			spriteRenderer.material.SetFloat("_Left", x);
		}
	}

	// Token: 0x06001CF5 RID: 7413 RVA: 0x0007DD84 File Offset: 0x0007C184
	public void TransitionUpdate()
	{
		this.UpdateCloudLines();
	}

	// Token: 0x04001B8A RID: 7050
	public float distance = 21.49f;

	// Token: 0x04001B8B RID: 7051
	public float speed = 1f;

	// Token: 0x04001B8C RID: 7052
	public bool hidden;

	// Token: 0x04001B8D RID: 7053
	public Transform line;

	// Token: 0x04001B8E RID: 7054
	private Plane[] planes;

	// Token: 0x04001B8F RID: 7055
	private bool appeared;

	// Token: 0x04001B90 RID: 7056
	private float alpha;
}
