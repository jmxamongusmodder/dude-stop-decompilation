using System;
using UnityEngine;

// Token: 0x0200046C RID: 1132
public class PuzzleWomensRestroomBottom : MonoBehaviour
{
	// Token: 0x06001D17 RID: 7447 RVA: 0x0007F378 File Offset: 0x0007D778
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
				num = 270f;
			}
			else if (num > 0f && num <= 10f)
			{
				num = 0f;
			}
			float num2 = (this.otherLid.eulerAngles.z >= 10f) ? this.otherLid.eulerAngles.z : (this.otherLid.eulerAngles.z + 360f);
			if ((num == 0f || num >= 270f) && num2 < num)
			{
				base.transform.rotation = Quaternion.Euler(0f, 0f, num);
			}
			else
			{
				this.dragged = false;
			}
		}
		else if (base.transform.eulerAngles.z > 270f)
		{
			float z = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, 0f, this.fallSpeed * Time.deltaTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
		}
		else if (base.transform.eulerAngles.z == 270f)
		{
			if (this.timer >= this.waitBeforeCompletion)
			{
				Global.LevelCompleted(0f, true);
			}
			else
			{
				this.timer += Time.deltaTime;
			}
		}
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x0007F589 File Offset: 0x0007D989
	private void OnMouseDown()
	{
		this.dragged = true;
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x0007F592 File Offset: 0x0007D992
	private void OnMouseUp()
	{
		this.dragged = false;
	}

	// Token: 0x04001BBF RID: 7103
	public float fallSpeed = 20f;

	// Token: 0x04001BC0 RID: 7104
	public Transform otherLid;

	// Token: 0x04001BC1 RID: 7105
	public float waitBeforeCompletion = 2f;

	// Token: 0x04001BC2 RID: 7106
	private bool dragged;

	// Token: 0x04001BC3 RID: 7107
	private float timer;
}
