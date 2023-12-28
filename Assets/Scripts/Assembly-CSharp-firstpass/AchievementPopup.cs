using System;
using UnityEngine;

// Token: 0x02000512 RID: 1298
public class AchievementPopup : MonoBehaviour
{
	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x00086685 File Offset: 0x00084A85
	public static AchievementPopup self
	{
		get
		{
			if (AchievementPopup._self == null)
			{
				AchievementPopup._self = UIControl.self.achievementPopup;
			}
			return AchievementPopup._self;
		}
	}

	// Token: 0x06001DD8 RID: 7640 RVA: 0x000866AC File Offset: 0x00084AAC
	public void AddAchievement(AwardName name)
	{
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.prefab);
		transform.SetParent(base.transform, false);
		transform.gameObject.SetActive(true);
		AchievementPopupItem component = transform.GetComponent<AchievementPopupItem>();
		component.SetAchievement(name);
	}

	// Token: 0x04002126 RID: 8486
	private static AchievementPopup _self;

	// Token: 0x04002127 RID: 8487
	public Transform prefab;
}
