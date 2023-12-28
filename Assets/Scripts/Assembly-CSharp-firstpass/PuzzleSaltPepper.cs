using System;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class PuzzleSaltPepper : MonoBehaviour
{
	// Token: 0x06001BE7 RID: 7143 RVA: 0x00075008 File Offset: 0x00073408
	private void Update()
	{
		if (this.hover && Input.GetMouseButtonDown(0))
		{
			if (this.startingPosition == Vector3.zero)
			{
				this.startingPosition = base.transform.position;
			}
			this.dragged = true;
		}
		else if (this.dragged && Input.GetMouseButtonUp(0))
		{
			this.dragged = false;
		}
		if (this.dragged && (this.hover || this.distance > this.maxDistance))
		{
			if (this.distance > this.maxDistance)
			{
				float num = Input.GetAxis("Mouse Y") / this.throttle;
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + num, 0f);
				if (base.transform.position.y > 1.3f)
				{
					this.completelyUnscrewed = true;
				}
				if (base.transform.position.y < 0.89f)
				{
					base.transform.position = new Vector3(base.transform.position.x, 0.89f, 0f);
				}
				if (base.transform.position.y > 3.5f)
				{
					base.transform.position = new Vector3(base.transform.position.x, 3.5f, 0f);
				}
			}
			else
			{
				this.distance += Input.GetAxis("Mouse X");
				if (this.distance < 0f)
				{
					this.distance = 0f;
				}
				base.transform.position = new Vector3(this.startingPosition.x, this.startingPosition.y + this.distance / this.maxDistance * this.upMovement, 0f);
			}
		}
		else if (this.completelyUnscrewed && base.transform.position.y >= 0.89f && base.transform.position.y < 0.94f)
		{
			Global.LevelCompleted(0f, true);
		}
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x0007528B File Offset: 0x0007368B
	private void OnMouseOver()
	{
		this.hover = true;
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x00075294 File Offset: 0x00073694
	private void OnMouseExit()
	{
		this.hover = false;
	}

	// Token: 0x04001A4A RID: 6730
	public float maxDistance = 150f;

	// Token: 0x04001A4B RID: 6731
	public float upMovement = 1.5f;

	// Token: 0x04001A4C RID: 6732
	public float throttle = 50f;

	// Token: 0x04001A4D RID: 6733
	private Vector3 startingPosition = Vector3.zero;

	// Token: 0x04001A4E RID: 6734
	private bool hover;

	// Token: 0x04001A4F RID: 6735
	private bool dragged;

	// Token: 0x04001A50 RID: 6736
	private float distance;

	// Token: 0x04001A51 RID: 6737
	private bool completelyUnscrewed;
}
