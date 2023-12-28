using System;
using UnityEngine;

// Token: 0x02000442 RID: 1090
public class PuzzleRoadCover_Cover : MonoBehaviour
{
	// Token: 0x06001BE1 RID: 7137 RVA: 0x00074E28 File Offset: 0x00073228
	private void Start()
	{
		this.cover = base.transform.GetChild(1);
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x00074E3C File Offset: 0x0007323C
	private void Update()
	{
		this.CheckRotation();
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x00074E44 File Offset: 0x00073244
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = false;
		this.startDeltaSet = false;
		if (Mathf.Abs(Mathf.DeltaAngle(this.cover.eulerAngles.z, 0f)) > this.requiredRotation)
		{
			Global.LevelCompleted(0f, true);
		}
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x00074EA4 File Offset: 0x000732A4
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = true;
		this.startAngle = this.cover.eulerAngles.z;
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x00074EE0 File Offset: 0x000732E0
	private void CheckRotation()
	{
		if (!this.dragged)
		{
			return;
		}
		Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - base.transform.position;
		float num = 360f - Mathf.Atan2(vector.x, vector.y) * 57.29578f;
		if (!this.startDeltaSet)
		{
			this.startDelta = num;
			this.startDeltaSet = true;
		}
		else
		{
			float f = Mathf.DeltaAngle(this.cover.eulerAngles.z, this.startAngle + num - this.startDelta);
			if (Mathf.Abs(f) > 1f)
			{
				Audio.self.playOneShot("d7d61c94-744e-4d49-8743-84cd402e0e37", 1f);
			}
			this.cover.rotation = Quaternion.Euler(0f, 0f, this.startAngle + num - this.startDelta);
		}
	}

	// Token: 0x04001A44 RID: 6724
	public float requiredRotation = 60f;

	// Token: 0x04001A45 RID: 6725
	private bool dragged;

	// Token: 0x04001A46 RID: 6726
	private Transform cover;

	// Token: 0x04001A47 RID: 6727
	private float startDelta;

	// Token: 0x04001A48 RID: 6728
	private float startAngle;

	// Token: 0x04001A49 RID: 6729
	private bool startDeltaSet;
}
