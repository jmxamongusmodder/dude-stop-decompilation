using System;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public class PuzzleHotBeer : Draggable
{
	// Token: 0x06001ABE RID: 6846 RVA: 0x0006A36C File Offset: 0x0006876C
	private void Update()
	{
		if (this.cooling)
		{
			this.temperature -= Time.deltaTime;
		}
		else
		{
			this.temperature += Time.deltaTime;
		}
		if (!this.warmed && !this.cooled)
		{
			if (this.temperature >= this.temperatureToWarm)
			{
				this.warmed = true;
			}
			else if (this.temperature <= this.temperatureToCool)
			{
				this.cooled = true;
			}
		}
		if (this.cooled || this.warmed)
		{
			this.timer += Time.deltaTime;
			if (this.timer > this.waitBeforeTransition)
			{
				if (this.cooled)
				{
					Global.LevelFailed(0f, true);
				}
				else if (this.warmed)
				{
					Global.LevelCompleted(0f, true);
				}
			}
		}
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x0006A462 File Offset: 0x00068862
	private void OnTriggerEnter2D()
	{
		this.cooling = true;
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x0006A46B File Offset: 0x0006886B
	private void OnTriggerExit2D()
	{
		this.cooling = false;
	}

	// Token: 0x040018DA RID: 6362
	public float temperatureToWarm = 15f;

	// Token: 0x040018DB RID: 6363
	public float temperatureToCool = -5f;

	// Token: 0x040018DC RID: 6364
	public float waitBeforeTransition = 2f;

	// Token: 0x040018DD RID: 6365
	private float timer;

	// Token: 0x040018DE RID: 6366
	public float temperature;

	// Token: 0x040018DF RID: 6367
	private bool cooling;

	// Token: 0x040018E0 RID: 6368
	private bool cooled;

	// Token: 0x040018E1 RID: 6369
	private bool warmed;
}
