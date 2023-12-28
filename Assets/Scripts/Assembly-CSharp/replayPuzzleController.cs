using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000579 RID: 1401
public class replayPuzzleController : MonoBehaviour
{
	// Token: 0x0600203C RID: 8252 RVA: 0x0009DE7B File Offset: 0x0009C27B
	private void Awake()
	{
		if (Global.self.cupList[base.GetComponent<Award>().awardName] == CupStatus.Empty)
		{
			return;
		}
		base.StartCoroutine(this.showReplayButton());
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x0009DEAC File Offset: 0x0009C2AC
	private void OnMouseUpAsButton()
	{
		if (!base.enabled)
		{
			return;
		}
		if (Global.self.cupList[base.GetComponent<Award>().awardName] == CupStatus.Empty)
		{
			return;
		}
		this.GetPuzzleStats().UIScreenCurr.GetComponent<levelPackMenu>().bReplay();
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x0009DEFC File Offset: 0x0009C2FC
	private IEnumerator showReplayButton()
	{
		while (!(this.GetPuzzleStats().UIScreenCurr != null))
		{
			yield return null;
		}
		this.lp = this.GetPuzzleStats().UIScreenCurr.GetComponent<levelPackMenu>();
		RectTransform button = null;
		if (this.legoButton)
		{
			button = this.lp.legoReplayButton;
		}
		if (this.diplomaButton)
		{
			button = this.lp.diplomReplayButton;
		}
		RectTransform sb = this.lp.startButton.GetComponent<RectTransform>();
		button.gameObject.SetActive(true);
		Vector2 pos = button.anchoredPosition;
		pos.x = sb.sizeDelta.x * 0.5f + button.sizeDelta.x * 0.5f + 20f;
		button.anchoredPosition = pos;
		button.GetComponent<ButtonTemplate>().addTriggerEvent(new Action(this.mouseOver), EventTriggerType.PointerEnter);
		this.lp.replayCallback = new Action(this.startLevel);
		yield break;
	}

	// Token: 0x0600203F RID: 8255 RVA: 0x0009DF17 File Offset: 0x0009C317
	private void mouseOver()
	{
		base.GetComponent<Award>().highlightAward();
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x0009DF24 File Offset: 0x0009C324
	private void startLevel()
	{
		Global.self.loopFirstPuzzle = false;
		Global.self.lastPackCompletionState = CompletionState.None;
		Global.self.replayingCupPuzzle = true;
		Global.self.makeNewLevel(this.replayPuzzle, Vector2.right, true);
	}

	// Token: 0x0400237B RID: 9083
	public bool legoButton;

	// Token: 0x0400237C RID: 9084
	public bool diplomaButton;

	// Token: 0x0400237D RID: 9085
	[Space(10f)]
	public Transform replayPuzzle;

	// Token: 0x0400237E RID: 9086
	private levelPackMenu lp;
}
