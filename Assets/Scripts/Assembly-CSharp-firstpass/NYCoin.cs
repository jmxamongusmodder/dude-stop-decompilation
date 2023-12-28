using System;
using UnityEngine;

// Token: 0x02000560 RID: 1376
public class NYCoin : MonoBehaviour
{
	// Token: 0x06001FA5 RID: 8101 RVA: 0x000983BC File Offset: 0x000967BC
	private void OnMouseDown()
	{
		if (Global.self.NoCurrentTransition)
		{
			Audio.self.playOneShot("c9a5dcb8-b799-4699-9660-c004a4b8a016", 1f);
			if (Global.self.nyCoinCurrent == Global.self.nyCoinsMaxFake)
			{
				int num = Global.self.nyCoinMax - Global.self.nyCoinCurrent;
				Global.self.nyCoinCurrent += num;
				UIControl.self.nyCoin.Show(num, base.transform.position);
			}
			else
			{
				Global.self.nyCoinCurrent++;
				UIControl.self.nyCoin.Show(1, base.transform.position);
			}
			if (Global.self.nyCoinCurrent >= Global.self.nyCoinMax)
			{
				Global.self.GetCup(AwardName.ChristmasEvent);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
