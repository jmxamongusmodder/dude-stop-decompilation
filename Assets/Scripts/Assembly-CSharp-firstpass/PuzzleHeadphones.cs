using System;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class PuzzleHeadphones : MonoBehaviour
{
	// Token: 0x06001A93 RID: 6803 RVA: 0x00068C70 File Offset: 0x00067070
	private void Update()
	{
		if (this.dragged)
		{
			this.distance += Input.GetAxis("Mouse X");
			if (this.distance < 0f)
			{
				this.distance = 0f;
			}
			if (this.distance / this.distancePerCircle > this.circles)
			{
				this.distance = this.circles * this.distancePerCircle;
			}
			base.transform.localEulerAngles = new Vector3(0f, 0f, 360f * (this.distance / this.distancePerCircle));
		}
		else
		{
			if (this.distance == this.circles * this.distancePerCircle)
			{
				this.timer += Time.deltaTime;
			}
			else
			{
				this.timer = 0f;
			}
			if (this.timer >= this.waitBeforeCompletion)
			{
				Global.LevelCompleted(0f, true);
			}
		}
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x00068D6D File Offset: 0x0006716D
	private void OnMouseDown()
	{
		this.dragged = true;
	}

	// Token: 0x06001A95 RID: 6805 RVA: 0x00068D76 File Offset: 0x00067176
	private void OnMouseUp()
	{
		this.dragged = false;
	}

	// Token: 0x040018AF RID: 6319
	public float distancePerCircle = 100f;

	// Token: 0x040018B0 RID: 6320
	public float circles = 2f;

	// Token: 0x040018B1 RID: 6321
	public float waitBeforeCompletion = 2f;

	// Token: 0x040018B2 RID: 6322
	private bool dragged;

	// Token: 0x040018B3 RID: 6323
	private float distance;

	// Token: 0x040018B4 RID: 6324
	private float timer;
}
