using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000562 RID: 1378
public class NYCoinUI : MonoBehaviour
{
	// Token: 0x06001FA9 RID: 8105 RVA: 0x000984E0 File Offset: 0x000968E0
	private void Start()
	{
		this.rect = base.GetComponent<RectTransform>();
		this.text = base.transform.GetChild(0).GetComponent<Text>();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x00098514 File Offset: 0x00096914
	private void Update()
	{
		this.time += Time.deltaTime * this.speed;
		this.text.color = new Color(1f, 1f, 1f, this.curve.Evaluate(this.time));
		Vector2 anchoredPosition = this.rect.anchoredPosition;
		anchoredPosition.y -= Time.deltaTime * this.moveSpeed;
		this.rect.anchoredPosition = anchoredPosition;
		if (this.time >= 1f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x000985B8 File Offset: 0x000969B8
	public void Show(int value, Vector3 pos)
	{
		this.text.text = "+" + value.ToString();
		Vector3 vector = Camera.main.WorldToViewportPoint(pos);
		RectTransform component = UIControl.self.GetComponent<RectTransform>();
		this.rect.anchoredPosition = new Vector2(vector.x * component.sizeDelta.x, vector.y * component.sizeDelta.y);
		this.time = 0f;
		base.gameObject.SetActive(true);
	}

	// Token: 0x040022D1 RID: 8913
	private RectTransform rect;

	// Token: 0x040022D2 RID: 8914
	private Text text;

	// Token: 0x040022D3 RID: 8915
	public AnimationCurve curve;

	// Token: 0x040022D4 RID: 8916
	public float speed = 1f;

	// Token: 0x040022D5 RID: 8917
	public float moveSpeed = 1f;

	// Token: 0x040022D6 RID: 8918
	private float time;
}
