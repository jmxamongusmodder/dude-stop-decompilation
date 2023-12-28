using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200053E RID: 1342
public class CurrentTime : MonoBehaviour
{
	// Token: 0x06001EAB RID: 7851 RVA: 0x0008E8D6 File Offset: 0x0008CCD6
	private void Start()
	{
		this.textBox = base.GetComponent<Text>();
	}

	// Token: 0x06001EAC RID: 7852 RVA: 0x0008E8E4 File Offset: 0x0008CCE4
	private void Update()
	{
		this.updateTime -= Time.deltaTime;
		if (this.updateTime <= 0f)
		{
			this.textBox.text = Puzzle6amNoise_Switch.ConvertMinutesToTime(DateTime.Now.Hour * 60 + DateTime.Now.Minute);
			this.updateTime = 5f;
		}
	}

	// Token: 0x040021D6 RID: 8662
	private Text textBox;

	// Token: 0x040021D7 RID: 8663
	private float updateTime;
}
