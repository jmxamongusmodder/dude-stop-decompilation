using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000541 RID: 1345
public class DuckPopup : MonoBehaviour
{
	// Token: 0x06001EB7 RID: 7863 RVA: 0x0008EC00 File Offset: 0x0008D000
	public void setDuck(bool right)
	{
		if (UIControl.self.useBackupFont)
		{
			this.textField.font = UIControl.self.backupFont;
			this.textField.fontSize = Mathf.RoundToInt((float)this.textField.fontSize * UIControl.self.SmallFontScale);
		}
		this.textBoxOrigPos = this.textBox.anchoredPosition;
		this.setSide(right);
		this.speechBubble.gameObject.SetActive(false);
		this.duckAnimator.SetTrigger("show");
		Audio.self.playOneShot("333cac0c-256b-4c86-99d0-ced46440acf7", 1f);
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x0008ECA8 File Offset: 0x0008D0A8
	public IEnumerator setTextSize(string wholeText)
	{
		while (!this.canBeUsed)
		{
			yield return null;
		}
		this.canBeUsed = false;
		this.yesButton.gameObject.SetActive(false);
		this.ratingContainer.gameObject.SetActive(false);
		this.yesNoButton.gameObject.SetActive(false);
		this.viewLastMsgButton.gameObject.SetActive(false);
		this.borderAroundText = new Vector4(this.textBoxOrigPos.x, this.textBoxOrigPos.y, this.textBoxOrigPos.x, this.textBoxOrigPos.y);
		this.speechBubble.gameObject.SetActive(true);
		this.speechBubble.GetComponent<CanvasGroup>().alpha = 0f;
		this.textField.text = wholeText;
		yield return null;
		this.speechBubble.sizeDelta = this.addBorders(this.textBox.sizeDelta, this.borderAroundText);
		this.speechBubbleMaxX = Mathf.Max(130f, this.speechBubble.sizeDelta.x);
		this.canBeUsed = true;
		yield break;
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x0008ECCC File Offset: 0x0008D0CC
	public IEnumerator setOneLine(string firstLine, params DuckPopupSettings[] settings)
	{
		while (!this.canBeUsed)
		{
			yield return null;
		}
		this.canBeUsed = false;
		this.textField.text = firstLine;
		if (settings.Contains(DuckPopupSettings.Yes))
		{
			yield return null;
			this.borderAroundText.w = this.yesButton.anchoredPosition.y + this.yesButton.sizeDelta.y;
			this.yesButton.gameObject.SetActive(false);
		}
		else if (settings.Contains(DuckPopupSettings.StarRating))
		{
			this.borderAroundText.w = this.ratingContainer.anchoredPosition.y + this.ratingContainer.sizeDelta.y;
			yield return null;
		}
		else if (settings.Contains(DuckPopupSettings.YesNo))
		{
			yield return null;
			this.borderAroundText.w = this.yesNoButton.anchoredPosition.y + this.yesNoButton.sizeDelta.y;
			this.yesNoButton.gameObject.SetActive(false);
		}
		else if (settings.Contains(DuckPopupSettings.ViewLastMsg))
		{
			yield return null;
			this.borderAroundText.w = this.viewLastMsgButton.anchoredPosition.y + this.viewLastMsgButton.sizeDelta.y;
			this.viewLastMsgButton.gameObject.SetActive(false);
		}
		else
		{
			yield return null;
		}
		this.speechBubble.sizeDelta = new Vector2(this.speechBubbleMaxX, this.textBox.sizeDelta.y + this.borderAroundText.y + this.borderAroundText.w);
		this.textBox.anchoredPosition = new Vector2(this.borderAroundText.x, this.borderAroundText.w);
		this.textField.text = string.Empty;
		this.speechBubble.GetComponent<CanvasGroup>().alpha = 1f;
		bool right = this.speechBubble.pivot == Vector2.right;
		float arrowSize = Vector3.Magnitude(this.arrow.sizeDelta);
		if (right)
		{
			this.background.anchorMin = new Vector2(1f - Mathf.Abs(this.arrow.anchoredPosition.x - arrowSize * 0.5f) / this.speechBubble.sizeDelta.x, 0.5f);
		}
		else
		{
			this.background.anchorMin = new Vector2(Mathf.Abs(this.arrow.anchoredPosition.x - arrowSize * 0.5f) / this.speechBubble.sizeDelta.x, 0.5f);
		}
		this.background.anchorMax = this.background.anchorMin;
		this.background.pivot = this.background.anchorMin;
		this.background.anchoredPosition = Vector2.zero;
		this.background.sizeDelta = new Vector2(arrowSize, this.speechBubble.sizeDelta.y);
		Audio.self.playOneShot("eed61425-840b-4d58-8665-e0f67ed6426c", 1f);
		Vector2 size = this.background.sizeDelta;
		float time = 0f;
		float timeMax = this.bubbleAppearCurve.GetAnimationLength();
		bool txtShowed = false;
		while (time < timeMax)
		{
			time += Time.deltaTime;
			this.background.sizeDelta = size + Vector2.right * (this.speechBubble.sizeDelta.x - size.x) * this.bubbleAppearCurve.Evaluate(time);
			if (time / timeMax > 0.7f && !txtShowed)
			{
				Audio.self.playOneShot("88592867-f128-4170-849c-c95c5bf1b184", 1f);
				txtShowed = true;
				this.textField.text = firstLine;
				if (settings.Contains(DuckPopupSettings.Yes))
				{
					this.yesButton.gameObject.SetActive(true);
				}
				else if (settings.Contains(DuckPopupSettings.StarRating))
				{
					this.ratingContainer.gameObject.SetActive(true);
				}
				else if (settings.Contains(DuckPopupSettings.YesNo))
				{
					this.yesNoButton.gameObject.SetActive(true);
				}
				else if (settings.Contains(DuckPopupSettings.ViewLastMsg))
				{
					this.viewLastMsgButton.gameObject.SetActive(true);
				}
				if (settings.Contains(DuckPopupSettings.ShowDoneLoading))
				{
					this.loadingDoneCoroutine = base.StartCoroutine(this.loadDotDotDotDone(string.Empty, firstLine));
				}
			}
			yield return null;
		}
		this.background.anchorMin = Vector2.zero;
		this.background.anchorMax = Vector2.one;
		this.background.pivot = Vector2.one * 0.5f;
		this.background.anchoredPosition = Vector2.zero;
		this.background.offsetMin = Vector2.zero;
		this.background.offsetMax = Vector2.zero;
		this.canBeUsed = true;
		yield break;
	}

	// Token: 0x06001EBA RID: 7866 RVA: 0x0008ECF8 File Offset: 0x0008D0F8
	public IEnumerator addNewLine(string line, params DuckPopupSettings[] settings)
	{
		while (!this.canBeUsed)
		{
			yield return null;
		}
		this.canBeUsed = false;
		string oldText = this.textField.text;
		Text text = this.textField;
		text.text = text.text + "\n" + line;
		yield return null;
		if (settings.Contains(DuckPopupSettings.Yes))
		{
			this.borderAroundText.w = this.yesButton.anchoredPosition.y + this.yesButton.sizeDelta.y;
			this.yesButton.gameObject.SetActive(true);
			this.textBox.anchoredPosition = new Vector2(this.borderAroundText.x, this.borderAroundText.w);
		}
		this.speechBubble.sizeDelta = new Vector2(this.speechBubbleMaxX, this.textBox.sizeDelta.y + this.borderAroundText.y + this.borderAroundText.w);
		Audio.self.playOneShot("88592867-f128-4170-849c-c95c5bf1b184", 1f);
		if (settings.Contains(DuckPopupSettings.ShowProcLoading))
		{
			yield return base.StartCoroutine(this.countProcents(oldText, line));
		}
		this.canBeUsed = true;
		yield break;
	}

	// Token: 0x06001EBB RID: 7867 RVA: 0x0008ED21 File Offset: 0x0008D121
	public void setLine(string line)
	{
		this.textField.text = line;
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x0008ED30 File Offset: 0x0008D130
	private IEnumerator loadDotDotDotDone(string oldText, string line)
	{
		if (!line.Contains("..."))
		{
			yield break;
		}
		if (!string.IsNullOrEmpty(oldText))
		{
			oldText += "\n";
		}
		float waitTime = 0.5f;
		int ind = line.LastIndexOf("...");
		int dot = 0;
		while (base.enabled)
		{
			if (++dot > 3)
			{
				dot = 0;
			}
			this.textField.text = oldText + line.Substring(0, ind + dot);
			yield return new WaitForSeconds(waitTime);
		}
		this.textField.text = oldText + line;
		yield break;
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x0008ED59 File Offset: 0x0008D159
	public void stopLoadingDots()
	{
		base.StopCoroutine(this.loadingDoneCoroutine);
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x0008ED68 File Offset: 0x0008D168
	private IEnumerator countProcents(string oldText, string line)
	{
		if (!line.Contains("0%"))
		{
			yield break;
		}
		if (!string.IsNullOrEmpty(oldText))
		{
			oldText += "\n";
		}
		float waitTime = 0.01f;
		int curr = 0;
		int to = 100;
		while (curr < to)
		{
			curr = (int)Mathf.MoveTowards((float)curr, (float)to, 3f);
			this.textField.text = oldText + line.Replace("0%", curr + "%");
			yield return new WaitForSeconds(waitTime);
			if ((double)UnityEngine.Random.value > 0.7)
			{
				yield return new WaitForSeconds(waitTime * 10f);
			}
			if ((double)UnityEngine.Random.value > 0.9)
			{
				yield return new WaitForSeconds(waitTime * 10f);
			}
		}
		yield break;
	}

	// Token: 0x06001EBF RID: 7871 RVA: 0x0008ED91 File Offset: 0x0008D191
	public void bYes()
	{
		this.yesCallback();
	}

	// Token: 0x06001EC0 RID: 7872 RVA: 0x0008ED9E File Offset: 0x0008D19E
	public void bNo()
	{
		this.noCallback();
	}

	// Token: 0x06001EC1 RID: 7873 RVA: 0x0008EDAB File Offset: 0x0008D1AB
	public void subscribeToYes(Action callback)
	{
		this.yesCallback = callback;
	}

	// Token: 0x06001EC2 RID: 7874 RVA: 0x0008EDB4 File Offset: 0x0008D1B4
	public void subscriveToNo(Action callback)
	{
		this.noCallback = callback;
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x0008EDBD File Offset: 0x0008D1BD
	public void bRating(int index)
	{
		this.ratingCallback(index);
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x0008EDCB File Offset: 0x0008D1CB
	public void subscribeToRating(Action<int> callback)
	{
		this.ratingCallback = callback;
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x0008EDD4 File Offset: 0x0008D1D4
	public void bViewLastMsg()
	{
		this.viewLastCallback();
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x0008EDE1 File Offset: 0x0008D1E1
	public void subscribeToViewLastMsg(Action callback)
	{
		this.viewLastCallback = callback;
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x0008EDEA File Offset: 0x0008D1EA
	public void hideDuck()
	{
		if (!this.active)
		{
			return;
		}
		this.active = false;
		base.StartCoroutine(this.hideDuckAnimation());
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x0008EE0C File Offset: 0x0008D20C
	private IEnumerator hideDuckAnimation()
	{
		this.duckAnimator.SetTrigger("hide");
		this.arrow.gameObject.SetActive(false);
		CanvasGroup gr = this.speechBubble.GetComponent<CanvasGroup>();
		this.yesButton.gameObject.SetActive(false);
		this.yesNoButton.gameObject.SetActive(false);
		this.viewLastMsgButton.gameObject.SetActive(false);
		while (gr.alpha > 0f)
		{
			gr.alpha -= Time.deltaTime * 10f;
			yield return null;
		}
		this.speechBubble.gameObject.SetActive(false);
		UnityEngine.Object.Destroy(base.gameObject, 1f);
		base.enabled = false;
		yield break;
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x0008EE28 File Offset: 0x0008D228
	public void setSide(bool right)
	{
		foreach (RectTransform rectTransform in this.leftRightList)
		{
			Vector2 vector = new Vector2((float)((!right) ? 0 : 1), 0f);
			rectTransform.anchorMin = vector;
			rectTransform.anchorMax = vector;
			rectTransform.pivot = vector;
			rectTransform.anchoredPosition = Vector2.zero;
		}
		if (right)
		{
			this.arrow.anchorMax = Vector2.right;
			this.arrow.anchorMin = Vector2.right;
			this.arrow.anchoredPosition = new Vector2(-110f, this.arrow.anchoredPosition.y);
		}
		else
		{
			this.arrow.anchorMax = Vector2.zero;
			this.arrow.anchorMin = Vector2.zero;
			this.arrow.anchoredPosition = new Vector2(80f, this.arrow.anchoredPosition.y);
		}
	}

	// Token: 0x06001ECA RID: 7882 RVA: 0x0008EF2B File Offset: 0x0008D32B
	private Vector2 addBorders(Vector2 size, Vector4 border)
	{
		return new Vector2(size.x + border.x + border.z, size.y + border.w + border.y);
	}

	// Token: 0x040021DD RID: 8669
	private bool active = true;

	// Token: 0x040021DE RID: 8670
	public Animator duckAnimator;

	// Token: 0x040021DF RID: 8671
	public RectTransform[] leftRightList;

	// Token: 0x040021E0 RID: 8672
	public RectTransform textBox;

	// Token: 0x040021E1 RID: 8673
	public RectTransform arrow;

	// Token: 0x040021E2 RID: 8674
	public Text textField;

	// Token: 0x040021E3 RID: 8675
	public RectTransform speechBubble;

	// Token: 0x040021E4 RID: 8676
	public RectTransform background;

	// Token: 0x040021E5 RID: 8677
	public AnimationCurve bubbleAppearCurve;

	// Token: 0x040021E6 RID: 8678
	private float speechBubbleMaxX;

	// Token: 0x040021E7 RID: 8679
	private Vector4 borderAroundText;

	// Token: 0x040021E8 RID: 8680
	private Vector2 textBoxOrigPos;

	// Token: 0x040021E9 RID: 8681
	private bool canBeUsed = true;

	// Token: 0x040021EA RID: 8682
	private Coroutine loadingDoneCoroutine;

	// Token: 0x040021EB RID: 8683
	public RectTransform yesButton;

	// Token: 0x040021EC RID: 8684
	private Action yesCallback;

	// Token: 0x040021ED RID: 8685
	public RectTransform yesNoButton;

	// Token: 0x040021EE RID: 8686
	private Action noCallback;

	// Token: 0x040021EF RID: 8687
	public RectTransform ratingContainer;

	// Token: 0x040021F0 RID: 8688
	private Action<int> ratingCallback;

	// Token: 0x040021F1 RID: 8689
	public RectTransform viewLastMsgButton;

	// Token: 0x040021F2 RID: 8690
	private Action viewLastCallback;
}
