using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200051C RID: 1308
public class ButtonTemplate : MonoBehaviour
{
	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x000871A5 File Offset: 0x000855A5
	private EventTrigger trigger
	{
		get
		{
			return base.GetComponent<EventTrigger>();
		}
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x000871B0 File Offset: 0x000855B0
	private void Start()
	{
		if (!this.addSoundTriggers)
		{
			return;
		}
		if (this.trigger == null)
		{
			Debug.LogError("Button " + base.name + " doesn't have EventTrigger. Set addSoundTrigger to false, if it was intentional.");
			return;
		}
		this.addTriggerEvent(new Action(this.soundSelect), EventTriggerType.Select);
		this.addTriggerEvent(new Action(this.soundEnter), EventTriggerType.PointerEnter);
		this.addTriggerEvent(new Action(this.soundExit), EventTriggerType.PointerExit);
		if (this.playClickSound)
		{
			this.addTriggerEvent(new Action(this.soundSubmit), EventTriggerType.Submit);
			this.addTriggerEvent(new Action(this.soundSubmit), EventTriggerType.PointerClick);
		}
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x00087263 File Offset: 0x00085663
	public void setActive(bool on)
	{
		base.GetComponent<Button>().interactable = on;
		this.enableSounds = on;
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x00087278 File Offset: 0x00085678
	public void addTriggerEvent(Action call, EventTriggerType type)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = type;
		entry.callback.AddListener(delegate(BaseEventData data)
		{
			call();
		});
		this.trigger.triggers.Add(entry);
	}

	// Token: 0x06001DFC RID: 7676 RVA: 0x000872C8 File Offset: 0x000856C8
	public void soundSelect()
	{
		if ((!this.ignoreTransitions && !Global.self.NoCurrentTransition) || !this.enableSounds)
		{
			return;
		}
		if (!this.mouseOn && this.playMouseOver)
		{
			Audio.self.playOneShot("1c7070c4-6fc7-4419-8ba5-0fdd299f2180", 1f);
		}
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x00087328 File Offset: 0x00085728
	public virtual void soundEnter()
	{
		this.mouseOn = true;
		if (this.callbackMouseOn != null)
		{
			this.callbackMouseOn(base.GetComponent<RectTransform>());
		}
		if ((!this.ignoreTransitions && !Global.self.NoCurrentTransition) || !this.enableSounds)
		{
			return;
		}
		if (this.playMouseOver)
		{
			Audio.self.playOneShot("1c7070c4-6fc7-4419-8ba5-0fdd299f2180", 1f);
		}
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x0008739E File Offset: 0x0008579E
	public virtual void soundExit()
	{
		this.mouseOn = false;
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x000873A8 File Offset: 0x000857A8
	public void soundSubmit()
	{
		if (!this.ignoreTransitions && !Global.self.NoCurrentTransition && Global.self.transitionFramesCurr > 2)
		{
			return;
		}
		if (!this.enableSounds)
		{
			return;
		}
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x00087401 File Offset: 0x00085801
	public void disableSounds(int frames = 1)
	{
		base.StartCoroutine(this.disableSoundsDelay(frames));
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x00087414 File Offset: 0x00085814
	private IEnumerator disableSoundsDelay(int frames = 1)
	{
		for (;;)
		{
			int num;
			frames = (num = frames) - 1;
			if (num <= 0)
			{
				break;
			}
			yield return new WaitForEndOfFrame();
		}
		this.enableSounds = false;
		yield break;
	}

	// Token: 0x04002141 RID: 8513
	[Tooltip("Add triggers to a button: mouse over sound, click, and so on")]
	public bool addSoundTriggers = true;

	// Token: 0x04002142 RID: 8514
	[Tooltip("Play click sound on Submit or MouseClick")]
	public bool playClickSound = true;

	// Token: 0x04002143 RID: 8515
	[Tooltip("Play click sound on MouseOver")]
	public bool playMouseOver = true;

	// Token: 0x04002144 RID: 8516
	[Tooltip("TRUE - play sound even on transitions. FALSE - don't work on transitions")]
	public bool ignoreTransitions;

	// Token: 0x04002145 RID: 8517
	private bool mouseOn;

	// Token: 0x04002146 RID: 8518
	public Action<RectTransform> callbackMouseOn;

	// Token: 0x04002147 RID: 8519
	private bool enableSounds = true;
}
