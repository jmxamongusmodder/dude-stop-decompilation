using System;
using System.Collections;
using ExcelData;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002F2 RID: 754
public class AudioVoice_ScrollableController : AudioVoice
{
	// Token: 0x060012B9 RID: 4793 RVA: 0x00029FA8 File Offset: 0x000283A8
	private void Awake()
	{
		this.loadingScreen.SetActive(false);
		this.voiceMessagePref.gameObject.SetActive(false);
		this.friendMessagePref.gameObject.SetActive(false);
		this.errorMsg.SetActive(false);
		this.popupMsg.SetActive(false);
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x00029FFB File Offset: 0x000283FB
	public override void setActive(bool on)
	{
		this.active = true;
		AudioVoice_ScrollableController.self = this;
		global::Console.self.canOpen = false;
		base.StartCoroutine(this.mainCoroutine());
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x0002A022 File Offset: 0x00028422
	public void setActive(bool on, VoiceLine line)
	{
		this.voice = line;
		this.setActive(on);
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x0002A034 File Offset: 0x00028434
	private IEnumerator mainCoroutine()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		if (!this.DebbugSkipVoicesOnLevel)
		{
			this.playTyping();
			this.playNotification();
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.oh_ignore_this_sounds_im_, false));
			yield return new WaitForSeconds(3f);
			this.playNotification();
			yield return new WaitForSeconds(2f);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.you_dont_see_but_Im_doing, false));
			this.playNotification();
			yield return new WaitForSeconds(3f);
			this.playTyping();
			yield return new WaitForSeconds(1f);
			this.playNotification();
			yield return new WaitForSeconds(2f);
			this.playTyping();
			yield return new WaitForSeconds(3f);
			this.playNotification();
		}
		if (!this.DebbugSkipFirstScreen)
		{
			yield return base.StartCoroutine(this.waitForTransitionStart());
			yield return base.StartCoroutine(this.stopTransition());
			yield return base.StartCoroutine(this.connectToPCMsg(false));
			this.PCscreen.SetActive(true);
			this.resetTaskBar();
			this.loadingScreen.SetActive(false);
			Cursor.SetCursor(this.mouseCursor, Vector2.zero, CursorMode.Auto);
			yield return base.StartCoroutine(this.createMessages(0, 10));
			this.showMessages(2);
			yield return new WaitForSeconds(10f * this.DebugShortWaitTimers);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.how_its_going_there_teste, false));
			yield return new WaitForSeconds(3f);
			yield return base.StartCoroutine(this.addMessage(1, 5f));
			yield return base.StartCoroutine(this.addMessage(0, 5f));
			yield return new WaitForSeconds(1f);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.but_its_not_an_excuse, false));
			yield return new WaitForSeconds(8f * this.DebugShortWaitTimers);
			this.showError("SCROLLABLE_ERROR_MESSAGE_1", false, true);
			yield return new WaitForSeconds(1f);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.tester_wait_a_second, true));
			yield return new WaitForSeconds(8f * this.DebugShortWaitTimers);
			this.addTextToError(true, "ERROR");
			yield return base.StartCoroutine(this.showErrorOKAndWait(false));
			this.continueTransition();
			yield return new WaitForSeconds(1f);
		}
		if (!this.DebbugSkipVoicesOnLevel)
		{
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.op_it_just_disappeared_I_, false));
			yield return new WaitForSeconds(2f);
			this.playNotification();
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.oh_my_friend_is_mess, false));
			yield return new WaitForSeconds(2f);
			this.playNotification();
			yield return new WaitForSeconds(4f);
			this.playNotification();
			yield return new WaitForSeconds(1f);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.oh_I_so_dont_want_to, false));
			yield return new WaitForSeconds(1f);
			this.playNotification();
		}
		if (!this.DebbugSkipSecondScreen)
		{
			yield return base.StartCoroutine(this.waitForTransitionStart());
			yield return base.StartCoroutine(this.stopTransition());
			yield return base.StartCoroutine(this.connectToPCMsg(true));
			this.PCscreen.SetActive(true);
			this.errorMsg.SetActive(false);
			this.resetTaskBar();
			this.loadingScreen.SetActive(false);
			Cursor.SetCursor(this.mouseCursor, Vector2.zero, CursorMode.Auto);
			yield return base.StartCoroutine(this.createMessages(0, 19));
			this.showMessages(3);
			yield return new WaitForSeconds(13f * this.DebugShortWaitTimers);
			this.showError("SCROLLABLE_ERROR_MESSAGE_1", false, true);
			this.resetAnimation();
			yield return new WaitForSeconds(0.5f);
			this.typingNotification.SetActive(true);
			this.cursorParent.SetTrigger("Cursor2");
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.this_weird_error_again, false));
			yield return new WaitForSeconds(4f);
			this.playNotification();
			this.showMessages(2);
			yield return new WaitForSeconds(1f);
			this.addTextToError(true, "ERROR");
			yield return base.StartCoroutine(this.showErrorOKAndWait(true));
			this.errorMsg.SetActive(false);
			this.resetTaskBar();
			Cursor.SetCursor(this.mouseCursor, Vector2.zero, CursorMode.Auto);
			yield return new WaitForSeconds(1f);
			base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.oh_it_disappeared_again_c, true));
			this.voicedClickedOk = false;
			this.buttonPressed = false;
			while (!this.voicedClickedOk && !this.buttonPressed)
			{
				yield return null;
			}
			if (!this.buttonPressed)
			{
				yield return new WaitForSeconds(0.3f);
			}
			this.typingNotification.SetActive(false);
			this.continueTransition();
			while (this.voice.isPlaying())
			{
				yield return null;
			}
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.good_now_its_good, false));
		}
		if (!this.DebbugSkipVoicesOnLevel)
		{
			this.voiceOnLevelCoroutine = base.StartCoroutine(this.voicesOnLevel());
			while (this.voiceOnLevelCoroutine != null)
			{
				yield return null;
			}
			while (this.voice != null && this.voice.isPlaying())
			{
				yield return null;
			}
			yield return new WaitForSeconds(1f);
			yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.aand_he_went_offline, false));
			yield return new WaitForSeconds(0.5f);
		}
		if (!this.DebbugSkipThirdScreen)
		{
			yield return base.StartCoroutine(this.waitForTransitionStart());
			yield return base.StartCoroutine(this.stopTransition());
			yield return base.StartCoroutine(this.connectToPCMsg(true));
			this.PCscreen.SetActive(true);
			this.errorMsg.SetActive(false);
			this.resetTaskBar();
			this.loadingScreen.SetActive(false);
			Cursor.SetCursor(this.mouseCursor, Vector2.zero, CursorMode.Auto);
			yield return base.StartCoroutine(this.createMessages(15, 33));
			this.showMessages(0);
			this.showError("SCROLLABLE_ERROR_MESSAGE_1", false, true);
			this.resetAnimation();
			yield return new WaitForSeconds(13f * this.DebugShortWaitTimers);
			this.addTextToError(true, "SCROLLABLE_ERROR_MESSAGE_SUCCESS");
			yield return base.StartCoroutine(this.showErrorOKAndWait(true));
			this.errorMsg.SetActive(false);
			this.resetTaskBar();
			Cursor.SetCursor(this.mouseCursor, Vector2.zero, CursorMode.Auto);
			yield return new WaitForSeconds(0.5f);
			base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.was_there_another_error_k, true));
			this.voicedClickedOk = false;
			this.popupClicked = false;
			while (!this.voicedClickedOk && !this.popupClicked)
			{
				yield return null;
			}
			if (!this.popupClicked)
			{
				yield return new WaitForSeconds(0.3f);
			}
			this.popupMsg.SetActive(false);
			this.resetTaskBar();
			this.continueTransition();
		}
		this.setButtonOnEndCard();
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		this.endIntro();
		yield break;
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x0002A050 File Offset: 0x00028450
	private IEnumerator voicesOnLevel()
	{
		yield return base.StartCoroutine(this.waitAndCheckEnd(1.5f));
		this.playNotification();
		yield return base.StartCoroutine(this.waitAndCheckEnd(0.5f));
		yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.wow_dont_message_me_, false));
		yield return base.StartCoroutine(this.waitAndCheckEnd(1f));
		this.playNotification();
		yield return base.StartCoroutine(this.waitAndCheckEnd(3f));
		this.playNotification();
		yield return base.StartCoroutine(this.waitAndCheckEnd(2f));
		yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.hm_ok_my_plan_isnt_w, false));
		yield return base.StartCoroutine(this.waitAndCheckEnd(0.5f));
		this.playTyping();
		yield return base.StartCoroutine(this.waitAndCheckEnd(1f));
		yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.why_am_I_friends_wit, false));
		yield return base.StartCoroutine(this.waitAndCheckEnd(0.5f));
		this.playTyping();
		yield return base.StartCoroutine(this.waitAndCheckEnd(1f));
		this.playNotification();
		yield return base.StartCoroutine(this.waitAndCheckEnd(3f));
		yield return base.StartCoroutine(this.playVoice(Voices.VoicePack10_Duck.ahh_another_option_to_bre, false));
		yield return base.StartCoroutine(this.waitAndCheckEnd(2f));
		this.playNotification();
		yield return base.StartCoroutine(this.waitAndCheckEnd(0.5f));
		this.playTyping();
		yield return base.StartCoroutine(this.waitAndCheckEnd(1f));
		this.playTyping();
		this.voiceOnLevelCoroutine = null;
		yield break;
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x0002A06C File Offset: 0x0002846C
	private IEnumerator waitAndCheckEnd(float time)
	{
		float waitTime = 0.1f;
		while (time > 0f)
		{
			time -= waitTime;
			if (UIControl.self.getCompletionProgress() >= 80)
			{
				if (this.voiceOnLevelCoroutine != null)
				{
					base.StopCoroutine(this.voiceOnLevelCoroutine);
				}
				this.voiceOnLevelCoroutine = null;
				yield break;
			}
			yield return new WaitForSeconds(waitTime);
		}
		yield break;
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x0002A090 File Offset: 0x00028490
	public void OnAnimationEvent(MouseAnimationEvent type)
	{
		switch (type)
		{
		case MouseAnimationEvent.click:
			this.playClick();
			return;
		case MouseAnimationEvent.unpsauseVoice:
			base.StartCoroutine(this.unpauseVoice());
			return;
		case MouseAnimationEvent.showOk:
			this.errorButtonDisctonnect.SetActive(true);
			return;
		case MouseAnimationEvent.clickCancel:
			if (!this.popupClicked)
			{
				this.playClick();
				this.simulateMouseDown(this.popupCancelButton);
				this.voicedClickedOk = true;
			}
			return;
		case MouseAnimationEvent.clickOk:
			if (!this.buttonPressed)
			{
				if (this.errorButtonClose.activeInHierarchy)
				{
					this.simulateMouseDown(this.errorButtonClose);
				}
				else
				{
					this.simulateMouseDown(this.errorButtonDisctonnect);
				}
				this.voicedClickedOk = true;
				this.playClick();
			}
			return;
		case MouseAnimationEvent.mouseEnterOk:
			if (this.errorButtonClose.activeInHierarchy)
			{
				this.simulateMouseEnter(this.errorButtonClose);
			}
			else
			{
				this.simulateMouseEnter(this.errorButtonDisctonnect);
			}
			return;
		case MouseAnimationEvent.mouseEnterYes:
			this.simulateMouseEnter(this.popupYesButton);
			return;
		case MouseAnimationEvent.mouseExitYes:
			this.simulateMouseExit(this.popupYesButton);
			return;
		case MouseAnimationEvent.mouseEnterCancel:
			this.simulateMouseEnter(this.popupCancelButton);
			return;
		case MouseAnimationEvent.mouseExitCancel:
			this.simulateMouseExit(this.popupCancelButton);
			return;
		case MouseAnimationEvent.showPopup:
			this.showPopup();
			return;
		case MouseAnimationEvent.enableButtons:
			return;
		case MouseAnimationEvent.startErrorDrag:
			this.errorMsg.transform.SetParent(this.cursorParent.transform.GetChild(0), true);
			this.errorMsg.transform.SetAsFirstSibling();
			return;
		case MouseAnimationEvent.endErrorDrag:
			this.errorMsg.transform.SetParent(this.PCscreen.transform, true);
			this.errorMsg.transform.SetSiblingIndex(this.popupMsg.transform.GetSiblingIndex());
			return;
		}
		Debug.LogError("Enum doesn't have action: " + type);
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x0002A2A0 File Offset: 0x000286A0
	private void simulateMouseEnter(GameObject obj)
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute<IPointerEnterHandler>(obj, eventData, ExecuteEvents.pointerEnterHandler);
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x0002A2C8 File Offset: 0x000286C8
	private void simulateMouseExit(GameObject obj)
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute<IPointerExitHandler>(obj, eventData, ExecuteEvents.pointerExitHandler);
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x0002A2F0 File Offset: 0x000286F0
	private void simulateMouseDown(GameObject obj)
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute<IPointerDownHandler>(obj, eventData, ExecuteEvents.pointerDownHandler);
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x0002A318 File Offset: 0x00028718
	private IEnumerator unpauseVoice()
	{
		while (!this.voice.isPaused)
		{
			yield return null;
		}
		this.voice.unPause(false);
		yield break;
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x0002A333 File Offset: 0x00028733
	public void resetAnimation()
	{
		this.cursorParent.SetTrigger("Reset");
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x0002A348 File Offset: 0x00028748
	private IEnumerator createMessages(int from, int to)
	{
		for (int i = this.chatParent.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(this.chatParent.GetChild(i).gameObject);
		}
		int ind = from;
		string str;
		do
		{
			str = WordTranslationContainer.Get(WordTranslationContainer.Theme.PUZZLE, "SCROLLABLE_CHATMESSAGE_" + ind, Global.self.currLanguage);
			if (string.IsNullOrEmpty(str))
			{
				break;
			}
			string a = str.Substring(0, 2);
			str = str.Substring(2).Trim();
			Transform transform;
			if (a == "1:")
			{
				transform = UnityEngine.Object.Instantiate<Transform>(this.voiceMessagePref);
			}
			else
			{
				transform = UnityEngine.Object.Instantiate<Transform>(this.friendMessagePref);
			}
			transform.gameObject.SetActive(true);
			transform.SetParent(this.chatParent, false);
			transform.SetAsFirstSibling();
			transform.GetChild(1).GetComponent<Text>().text = str;
			if (a == "3:")
			{
				transform.GetChild(0).gameObject.SetActive(false);
				transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.chatParent.GetComponent<RectTransform>().sizeDelta.x + this.chatParent.parent.GetComponent<RectTransform>().sizeDelta.x, 25f);
				transform.GetChild(1).GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
			}
			ind++;
		}
		while (!string.IsNullOrEmpty(str) && ind <= to);
		yield return null;
		yield break;
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x0002A374 File Offset: 0x00028774
	private void showMessages(int start)
	{
		float num = 0f;
		float num2 = 0f;
		bool flag = false;
		for (int i = 0; i < this.chatParent.childCount; i++)
		{
			if (i < start)
			{
				this.chatParent.GetChild(i).gameObject.SetActive(false);
			}
			else
			{
				RectTransform component = this.chatParent.GetChild(i).GetComponent<RectTransform>();
				component.gameObject.SetActive(true);
				bool flag2 = component.anchorMin.x == 1f;
				float num3 = ((flag2 != flag) ? this.distBetweenMsg : this.distBetweenSameMsg) * (float)Mathf.Min(i - start, 1);
				component.anchoredPosition = Vector2.up * (num2 + num3 + num);
				component.sizeDelta = new Vector2(component.sizeDelta.x, component.GetChild(1).GetComponent<RectTransform>().sizeDelta.y + this.textBorder * 2f);
				num = component.sizeDelta.y;
				num2 = component.anchoredPosition.y;
				flag = flag2;
			}
		}
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x0002A4B0 File Offset: 0x000288B0
	private IEnumerator addMessage(int fromStart, float waitTime)
	{
		this.typingNotification.SetActive(true);
		yield return new WaitForSeconds(waitTime);
		this.typingNotification.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		this.playNotification();
		this.showMessages(fromStart);
		yield break;
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x0002A4DC File Offset: 0x000288DC
	private void resetTaskBar()
	{
		this.errorBar.SetActive(false);
		this.popupBar.SetActive(false);
		this.gameBar.transform.GetChild(0).GetComponent<Image>().color = this.barColorDefault;
		this.chatBar.transform.GetChild(0).GetComponent<Image>().color = this.barColorSelect;
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x0002A544 File Offset: 0x00028944
	private IEnumerator setBarWarning(GameObject bar)
	{
		this.chatBar.transform.GetChild(0).GetComponent<Image>().color = this.barColorDefault;
		Image img = bar.transform.GetChild(0).GetComponent<Image>();
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime * this.colorLerpSpeed;
			float prog = this.colorLerpCurve.Evaluate(time);
			img.color = Color.Lerp(this.barColorDefault, this.barColorWarning, prog);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x0002A568 File Offset: 0x00028968
	private void showError(string msg, bool showButton, bool buttonOK = true)
	{
		base.StartCoroutine(this.setBarWarning(this.errorBar));
		this.errorMsg.SetActive(true);
		this.errorBar.SetActive(true);
		this.errorMsgText.text = LineTranslator.translateText(msg, WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		if (!showButton)
		{
			this.errorButtonClose.SetActive(false);
			this.errorButtonDisctonnect.SetActive(false);
		}
		else if (buttonOK)
		{
			this.errorButtonClose.SetActive(true);
		}
		else
		{
			this.errorButtonDisctonnect.SetActive(true);
		}
		this.playError();
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x0002A604 File Offset: 0x00028A04
	private void addTextToError(bool blinkInTaskbar, string text = "ERROR")
	{
		Text text2 = this.errorMsgText;
		text2.text = text2.text + " " + LineTranslator.translateText(text, WordTranslationContainer.Theme.MENU, false, string.Empty);
		if (blinkInTaskbar)
		{
			base.StartCoroutine(this.setBarWarning(this.errorBar));
		}
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x0002A654 File Offset: 0x00028A54
	private IEnumerator showErrorOKAndWait(bool bOk = true)
	{
		this.buttonPressed = false;
		if (bOk)
		{
			this.errorButtonClose.SetActive(true);
		}
		else
		{
			this.errorButtonDisctonnect.SetActive(true);
		}
		while (!this.buttonPressed)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x0002A676 File Offset: 0x00028A76
	public void bError()
	{
		this.buttonPressed = true;
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x0002A680 File Offset: 0x00028A80
	private void showPopup()
	{
		if (this.currentBlinking != null)
		{
			base.StopCoroutine(this.currentBlinking);
		}
		this.currentBlinking = base.StartCoroutine(this.setBarWarning(this.popupBar));
		this.popupClicked = false;
		this.popupMsg.SetActive(true);
		this.popupBar.SetActive(true);
		this.playError();
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x0002A6E1 File Offset: 0x00028AE1
	public void bPopup()
	{
		if (this.popupClicked)
		{
			return;
		}
		this.voice.setParameter(1f);
		this.popupClicked = true;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x0002A708 File Offset: 0x00028B08
	private IEnumerator playVoice(StandaloneLevelVoice line, bool subToMark = false)
	{
		bool done = false;
		this.voice = Audio.self.playVoice(line);
		this.voice.subscribeToStopped(this, delegate(VoiceLine l)
		{
			done = true;
		});
		if (subToMark)
		{
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		}
		this.voice.start(true);
		while (!done)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x0002A734 File Offset: 0x00028B34
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		switch (markerName)
		{
		case "ShowCursor":
			this.cursorParent.SetTrigger("Cursor1");
			break;
		case "Pause":
			this.voice.pause();
			break;
		case "getMsg":
			this.playNotification();
			this.showMessages(1);
			break;
		case "getMsg2":
			this.playNotification();
			this.showMessages(0);
			break;
		case "showError":
			this.showError("SCROLLABLE_ERROR_MESSAGE_1", false, true);
			this.cursorParent.SetTrigger("Cursor3");
			break;
		case "showNetwork":
			this.showError("SCROLLABLE_ERROR_MESSAGE_NETWORK", false, true);
			this.cursorParent.SetTrigger("Network");
			break;
		case "showPopup":
			this.showPopup();
			this.cursorParent.SetTrigger("Popup");
			break;
		}
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x0002A89B File Offset: 0x00028C9B
	private void playTyping()
	{
		Audio.self.playOneShot("c56a6853-6eae-466e-8eb0-5a1f0ceddffa", 1f);
		UIControl.setSubtitles(WordTranslationContainer.Get(WordTranslationContainer.Theme.PUZZLE, "CHAT_TYPING", Global.self.currLanguage));
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x0002A8CC File Offset: 0x00028CCC
	private void playNotification()
	{
		Audio.self.playOneShot("d4b381a1-f575-4031-80c7-2eaf40be77c9", 1f);
		UIControl.setSubtitles(WordTranslationContainer.Get(WordTranslationContainer.Theme.PUZZLE, "CHAT_NOTIFICATION", Global.self.currLanguage));
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x0002A8FD File Offset: 0x00028CFD
	private void playError()
	{
		Audio.self.playOneShot("d8f62038-2d33-436c-b430-5a67a380a053", 1f);
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x0002A914 File Offset: 0x00028D14
	private void playClick()
	{
		Audio.self.playOneShot("2d925cb1-2ee4-4d3e-9531-95d4bb4e4981", 1f);
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x0002A92C File Offset: 0x00028D2C
	private IEnumerator stopTransition()
	{
		Audio.self.playLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb");
		GlitchEffectController.self.startGlitch(5f, 1f);
		while (Global.self.transitionManualSpeed > 0f)
		{
			Global.self.transitionManualSpeed = Mathf.MoveTowards(Global.self.transitionManualSpeed, 0f, Time.deltaTime * this.transitionSlowDownSpeed);
			if (this.transitionSound.isValid())
			{
				this.transitionSound.setPitch(Global.self.transitionManualSpeed);
			}
			yield return null;
		}
		GlitchEffectController.self.stopGlitch();
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, false);
		yield break;
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x0002A948 File Offset: 0x00028D48
	private void continueTransition()
	{
		this.PCscreen.SetActive(false);
		UIControl.self.subtitilesIgnoreInventory = false;
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		GlitchEffectController.self.startGlitch(0.5f, 0.5f);
		Audio.self.playOneShot("f0e4627f-43f5-48a2-b313-d32528676246", 1f);
		Global.self.transitionManualSpeed = 1f;
		if (this.transitionSound.isValid())
		{
			this.transitionSound.setPitch(1f);
		}
		Audio.self.stopLoopSound("5aa7a15f-95f7-4bba-b511-b4ac1cb723f1", true);
		Audio.self.StartSoloSnapshot(MusicTypes.InGameMusic, true);
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x0002A9EE File Offset: 0x00028DEE
	public void onTransitionStart(EventInstance sound)
	{
		this.transitionSound = sound;
		this.startTransition = true;
		base.StartCoroutine(this.setStartToFalse());
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x0002AA0B File Offset: 0x00028E0B
	public void onTransitionEnd()
	{
		this.endTransition = true;
		base.StartCoroutine(this.setEndToFalse());
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x0002AA24 File Offset: 0x00028E24
	private IEnumerator setStartToFalse()
	{
		yield return null;
		this.startTransition = false;
		yield break;
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x0002AA40 File Offset: 0x00028E40
	private IEnumerator setEndToFalse()
	{
		yield return null;
		this.endTransition = false;
		yield break;
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x0002AA5C File Offset: 0x00028E5C
	private IEnumerator waitForTransitionStart()
	{
		while (!this.startTransition)
		{
			yield return null;
		}
		this.startTransition = false;
		yield break;
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x0002AA78 File Offset: 0x00028E78
	private IEnumerator waitForTransitionEnd()
	{
		while (!this.endTransition)
		{
			yield return null;
		}
		this.endTransition = false;
		yield break;
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x0002AA94 File Offset: 0x00028E94
	private IEnumerator connectToPCMsg(bool resume = false)
	{
		UIControl.self.subtitilesIgnoreInventory = true;
		UIControl.positionSubtitles(35f);
		UIControl.setSubtitlesAlpha(0.5f);
		this.loadingScreen.SetActive(true);
		this.loadingDuckVisible = true;
		this.continueButton.SetActive(false);
		if (resume)
		{
			this.loadingText.setTextToTranslate("SCROLLABLE_DUCK_SCREEN_SHARE_RESUME", WordTranslationContainer.Theme.PUZZLE);
		}
		else
		{
			this.loadingText.setTextToTranslate("SCROLLABLE_DUCK_SCREEN_SHARE_START", WordTranslationContainer.Theme.PUZZLE);
		}
		string txt = this.loadingText.currentText;
		int dotCount = 0;
		while (dotCount < 3)
		{
			LineTranslator lineTranslator = this.loadingText;
			string str = txt;
			char c = '.';
			int count;
			dotCount = (count = dotCount) + 1;
			lineTranslator.setTextNoTranslation(str + new string(c, count));
			if (this.shortDuck)
			{
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				yield return new WaitForSeconds(3f);
			}
		}
		Audio.self.stopLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb", true);
		this.loadingText.setTextToTranslate("SCROLLABLE_DUCK_SCREEN_SHARE_READY", WordTranslationContainer.Theme.PUZZLE);
		this.continueButton.SetActive(true);
		while (this.loadingDuckVisible)
		{
			yield return null;
		}
		Audio.self.playLoopSound("5aa7a15f-95f7-4bba-b511-b4ac1cb723f1");
		yield break;
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x0002AAB6 File Offset: 0x00028EB6
	public void bContinue()
	{
		this.loadingDuckVisible = false;
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x0002AAC0 File Offset: 0x00028EC0
	private void endIntro()
	{
		SerializableGameStats.self.pack10CutscenePlayed = true;
		Global.self.Save();
		Global.self.pack10CutsceneActive = false;
		this.setButtonOnEndCard();
		global::Console.self.canOpen = true;
		this.active = false;
		AudioVoice_ScrollableController.self = null;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x0002AB18 File Offset: 0x00028F18
	private void setButtonOnEndCard()
	{
		this.enableNextButton = true;
		PuzzleStats component;
		if (Global.self.NoCurrentTransition)
		{
			component = Global.self.currPuzzle.GetComponent<PuzzleStats>();
		}
		else
		{
			component = Global.self.getNextPuzzleToChangeVoiceParent().GetComponent<PuzzleStats>();
		}
		if (component.UIScreenCurr != null && component.UIScreenCurr.GetComponent<scrollableEndCard>() != null)
		{
			component.UIScreenCurr.GetComponent<scrollableEndCard>().setNextButton();
		}
	}

	// Token: 0x04000FB2 RID: 4018
	public static AudioVoice_ScrollableController self;

	// Token: 0x04000FB3 RID: 4019
	public float transitionSlowDownSpeed = 1f;

	// Token: 0x04000FB4 RID: 4020
	private bool startTransition;

	// Token: 0x04000FB5 RID: 4021
	private EventInstance transitionSound;

	// Token: 0x04000FB6 RID: 4022
	private bool endTransition;

	// Token: 0x04000FB7 RID: 4023
	[HideInInspector]
	public bool enableNextButton;

	// Token: 0x04000FB8 RID: 4024
	[Header("Debbug")]
	public bool DebbugSkipVoicesOnLevel;

	// Token: 0x04000FB9 RID: 4025
	public bool shortDuck;

	// Token: 0x04000FBA RID: 4026
	[Range(0.1f, 1f)]
	public float DebugShortWaitTimers = 1f;

	// Token: 0x04000FBB RID: 4027
	public bool DebbugSkipFirstScreen;

	// Token: 0x04000FBC RID: 4028
	public bool DebbugSkipSecondScreen;

	// Token: 0x04000FBD RID: 4029
	public bool DebbugSkipThirdScreen;

	// Token: 0x04000FBE RID: 4030
	[Header("Duck loading")]
	public GameObject loadingScreen;

	// Token: 0x04000FBF RID: 4031
	public LineTranslator loadingText;

	// Token: 0x04000FC0 RID: 4032
	public GameObject continueButton;

	// Token: 0x04000FC1 RID: 4033
	private bool loadingDuckVisible = true;

	// Token: 0x04000FC2 RID: 4034
	[Header("Voices PC")]
	public GameObject PCscreen;

	// Token: 0x04000FC3 RID: 4035
	public Transform chatParent;

	// Token: 0x04000FC4 RID: 4036
	public Transform voiceMessagePref;

	// Token: 0x04000FC5 RID: 4037
	public Transform friendMessagePref;

	// Token: 0x04000FC6 RID: 4038
	public GameObject typingNotification;

	// Token: 0x04000FC7 RID: 4039
	public float distBetweenMsg = 10f;

	// Token: 0x04000FC8 RID: 4040
	public float distBetweenSameMsg = 10f;

	// Token: 0x04000FC9 RID: 4041
	public float textBorder = 7f;

	// Token: 0x04000FCA RID: 4042
	[Header("Cursor")]
	public Texture2D mouseCursor;

	// Token: 0x04000FCB RID: 4043
	[Header("Error Message")]
	public GameObject errorMsg;

	// Token: 0x04000FCC RID: 4044
	public Text errorMsgText;

	// Token: 0x04000FCD RID: 4045
	public GameObject errorButtonClose;

	// Token: 0x04000FCE RID: 4046
	public GameObject errorButtonDisctonnect;

	// Token: 0x04000FCF RID: 4047
	private bool buttonPressed;

	// Token: 0x04000FD0 RID: 4048
	private bool voicedClickedOk;

	// Token: 0x04000FD1 RID: 4049
	[Header("Popup Message")]
	public GameObject popupMsg;

	// Token: 0x04000FD2 RID: 4050
	public GameObject popupYesButton;

	// Token: 0x04000FD3 RID: 4051
	public GameObject popupCancelButton;

	// Token: 0x04000FD4 RID: 4052
	private bool popupClicked;

	// Token: 0x04000FD5 RID: 4053
	[Header("Taskbar")]
	public Color barColorDefault;

	// Token: 0x04000FD6 RID: 4054
	public Color barColorSelect;

	// Token: 0x04000FD7 RID: 4055
	public Color barColorWarning;

	// Token: 0x04000FD8 RID: 4056
	public float colorLerpSpeed = 1f;

	// Token: 0x04000FD9 RID: 4057
	public AnimationCurve colorLerpCurve;

	// Token: 0x04000FDA RID: 4058
	private Coroutine currentBlinking;

	// Token: 0x04000FDB RID: 4059
	public GameObject errorBar;

	// Token: 0x04000FDC RID: 4060
	public GameObject gameBar;

	// Token: 0x04000FDD RID: 4061
	public GameObject chatBar;

	// Token: 0x04000FDE RID: 4062
	public GameObject popupBar;

	// Token: 0x04000FDF RID: 4063
	[Header("Cursror Animations")]
	public Animator cursorParent;

	// Token: 0x04000FE0 RID: 4064
	private Coroutine voiceOnLevelCoroutine;
}
