using System;
using UnityEngine;

// Token: 0x0200046D RID: 1133
public class PuzzleWomensRestroomTop : MonoBehaviour
{
	// Token: 0x06001D1B RID: 7451 RVA: 0x0007F5B0 File Offset: 0x0007D9B0
	private void Update()
	{
		if (this.dragged)
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.localPosition);
			Vector2 vector2 = new Vector2(mousePosition.x - vector.x, mousePosition.y - vector.y);
			float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
			num += 180f;
			if (num < 270f && num >= 260f)
			{
				num = 269f;
			}
			else if (num > 0f && num <= 10f)
			{
				num = 0f;
			}
			float num2 = (this.otherLid.eulerAngles.z >= 10f) ? this.otherLid.eulerAngles.z : (this.otherLid.eulerAngles.z + 360f);
			if ((num == 0f || num >= 269f) && num2 >= num)
			{
				base.transform.rotation = Quaternion.Euler(0f, 0f, num);
			}
			else
			{
				this.dragged = false;
			}
		}
		else if ((base.transform.eulerAngles.z > 270f && base.transform.eulerAngles.z < 360f) || (base.transform.eulerAngles.z > 260f && base.transform.eulerAngles.z <= 270f && !this.firstFall))
		{
			float z = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, 0f, this.fallSpeed * Time.deltaTime);
			if (base.transform.eulerAngles.z <= 270f && base.transform.eulerAngles.z > 260f)
			{
				this.firstFall = true;
				z = 271f;
			}
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
		}
	}

	// Token: 0x06001D1C RID: 7452 RVA: 0x0007F81A File Offset: 0x0007DC1A
	private void OnMouseDown()
	{
		this.dragged = true;
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x0007F823 File Offset: 0x0007DC23
	private void OnMouseUp()
	{
		this.dragged = false;
	}

	// Token: 0x04001BC4 RID: 7108
	public float fallSpeed = 20f;

	// Token: 0x04001BC5 RID: 7109
	public Transform otherLid;

	// Token: 0x04001BC6 RID: 7110
	private bool dragged;

	// Token: 0x04001BC7 RID: 7111
	private bool firstFall;
}
