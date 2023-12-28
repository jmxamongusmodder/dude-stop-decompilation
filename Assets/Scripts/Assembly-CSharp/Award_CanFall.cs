using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000310 RID: 784
public class Award_CanFall : Award
{
	// Token: 0x060013A1 RID: 5025 RVA: 0x00030DD4 File Offset: 0x0002F1D4
	private void OnMouseDown()
	{
		if (!this.canFall || !base.enabled)
		{
			return;
		}
		if (Global.self.currentAwardAnimCount > 0 || Global.self.currentAwardAnimation != AwardName.None)
		{
			return;
		}
		this.canFall = false;
		base.StartCoroutine(this.FallDown());
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x00030E2C File Offset: 0x0002F22C
	private IEnumerator FallDown()
	{
		float speed = 0f;
		while (base.transform.position.y > -100f)
		{
			Vector2 pos = base.transform.position;
			pos.y -= speed * Time.deltaTime;
			speed += this.acceleration * Time.deltaTime;
			base.transform.position = pos;
			base.transform.Rotate(0f, 0f, speed * this.rotationSpeed * Time.deltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001072 RID: 4210
	[Space(10f)]
	public float acceleration = 1f;

	// Token: 0x04001073 RID: 4211
	public float rotationSpeed = 1f;

	// Token: 0x04001074 RID: 4212
	private bool canFall = true;
}
