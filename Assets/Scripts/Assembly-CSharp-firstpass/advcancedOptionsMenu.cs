using System;
using ExcelData;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000514 RID: 1300
public class advcancedOptionsMenu : AbstractUIScreen
{
	// Token: 0x06001DDE RID: 7646 RVA: 0x00086C40 File Offset: 0x00085040
	public override void Update()
	{
		base.Update();
		if (!this.active)
		{
			return;
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (Input.GetButtonDown("Horizontal") && currentSelectedGameObject != null)
		{
			optionsButton component = currentSelectedGameObject.GetComponent<optionsButton>();
			if (component != null)
			{
				component.bSubmit();
			}
		}
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x00086C9E File Offset: 0x0008509E
	public override void setScreen(Transform item)
	{
		this.setUnitsText();
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x00086CA6 File Offset: 0x000850A6
	public void bUnits()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.metricSystem = !Global.self.metricSystem;
		this.setUnitsText();
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x00086CE6 File Offset: 0x000850E6
	private void setUnitsText()
	{
		if (Global.self.metricSystem)
		{
			this.unitsStatus.setTextToTranslate("MEASURE_UNITS_METRIC", WordTranslationContainer.Theme.MENU);
		}
		else
		{
			this.unitsStatus.setTextToTranslate("MEASURE_UNITS_IMPERIAL", WordTranslationContainer.Theme.MENU);
		}
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x00086D1E File Offset: 0x0008511E
	protected override void cancelPressed()
	{
		this.bBack();
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x00086D26 File Offset: 0x00085126
	public void bBack()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.optionsMenu, Vector2.up, true);
	}

	// Token: 0x04002130 RID: 8496
	public LineTranslator unitsStatus;
}
