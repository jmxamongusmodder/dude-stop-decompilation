using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000544 RID: 1348
public class endTextControl : AbstractUIScreen
{
	// Token: 0x06001EDD RID: 7901 RVA: 0x00091D38 File Offset: 0x00090138
	public override void Update()
	{
		if (!this.isEndingVoiced && this.waitTimer > 0f)
		{
			this.waitTimer -= Time.deltaTime;
			if (this.waitTimer <= 0f)
			{
				this.ShowNextLine();
			}
		}
		this.AnimateCup();
		if (!Global.self.canExitEndScreen || !this.noMoreLine || this.rotateActive || this.waitForTheVoice)
		{
			return;
		}
		if (this.canExit && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel")))
		{
			Global.self.gotoNextLevel(false, null);
			this.canExit = false;
		}
		if (Global.self.cantExitEndScreenTimer >= 0f)
		{
			Global.self.cantExitEndScreenTimer = Mathf.MoveTowards(Global.self.cantExitEndScreenTimer, -1f, Time.deltaTime);
			if (Global.self.cantExitEndScreenTimer <= 0f)
			{
				if (Global.self.packHasTimeLine)
				{
					this.canExit = true;
					if (this.exitOnEndTimer)
					{
						Global.self.gotoNextLevel(false, null);
						this.canExit = false;
					}
				}
				else
				{
					base.StartCoroutine(base.showHintIcon(this.hintText, delegate
					{
						this.canExit = true;
					}));
				}
			}
		}
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x00091EBC File Offset: 0x000902BC
	private void AnimateCup()
	{
		if (this.cup == null)
		{
			return;
		}
		if (this.cupMoveTimeCurr < this.cupMoveTime * 0.95f)
		{
			this.cupMoveTimeCurr = Mathf.MoveTowards(this.cupMoveTimeCurr, this.cupMoveTime, Time.deltaTime);
			this.cup.localPosition = Vector2.Lerp(this.cup.localPosition, this.cupMoveTarget, this.cupMoveTimeCurr / this.cupMoveTime);
		}
		else
		{
			this.cupMoveTarget = Vector2.MoveTowards(this.cupMoveTarget, this.cupTargetsTarget, Time.deltaTime * this.cupWobbleSpeed * 2f);
			this.cup.localPosition = Vector2.MoveTowards(this.cup.localPosition, this.cupMoveTarget, Time.deltaTime * this.cupWobbleSpeed);
			this.cupWobbleEachCurr -= Time.deltaTime;
			if (this.cupWobbleEachCurr <= 0f)
			{
				this.cupWobbleEachCurr = Extensions.Random(this.cupWobbleEach);
				this.cupTargetsTarget = UnityEngine.Random.insideUnitCircle * this.cupWobbleDistance;
				this.cupTargetsTarget += Vector2.up * this.cupMoveTargetShiftY;
			}
		}
		if (this.cupScaleTimeCurr <= this.cupScaleTime)
		{
			this.cupScaleTimeCurr = Mathf.MoveTowards(this.cupScaleTimeCurr, this.cupScaleTime, Time.deltaTime);
			this.cup.localScale = Vector2.Lerp(this.cup.localScale, Vector2.one, this.cupScaleTimeCurr / this.cupScaleTime);
		}
		if (this.angleWobble)
		{
			this.cupAngle = Mathf.MoveTowards(this.cupAngle, this.cupAngleTarget, Time.deltaTime * this.cupAngleWobbleSpeed);
			this.cup.rotation = Quaternion.Euler(Vector3.forward * this.cupAngle);
			this.cupAngleWobbleEachCurr -= Time.deltaTime;
			if (this.cupAngleWobbleEachCurr <= 0f)
			{
				this.cupAngleWobbleEachCurr = Extensions.Random(this.cupAngleWobbleEach);
				this.cupAngleTarget = UnityEngine.Random.Range(-this.cupAngleWobbleDistance, this.cupAngleWobbleDistance);
			}
		}
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x00092114 File Offset: 0x00090514
	private IEnumerator RotateCup()
	{
		this.cupAngle = this.cup.eulerAngles.z;
		float angleSpeed = 0f;
		int minLaps = 1;
		float time = 0f;
		float timeMax = this.cupFirstLapSpeed.keys[this.cupFirstLapSpeed.length - 1].time;
		float prog = 0f;
		for (;;)
		{
			if (this.cupAngle > 360f)
			{
				minLaps--;
				this.cupAngle = 360f - this.cupAngle;
				if (Vector2.Distance(this.cup.position, this.cupMoveTarget) < 0.2f && minLaps <= 0)
				{
					break;
				}
			}
			time = Mathf.MoveTowards(time, 1f, Time.deltaTime);
			prog = this.cupFirstLapSpeed.Evaluate(time / timeMax);
			angleSpeed = prog * this.angleSpeedMax;
			this.cupAngle += angleSpeed * Time.deltaTime;
			this.cup.rotation = Quaternion.Euler(Vector3.forward * this.cupAngle);
			yield return null;
		}
		for (;;)
		{
			prog = Mathf.Clamp(this.cupAngle / 360f, 0f, 1f);
			angleSpeed = Mathf.Min(angleSpeed, this.cupLastLapSpeed.Evaluate(prog) * this.angleSpeedMax);
			this.cupAngle += angleSpeed * Time.deltaTime;
			this.cup.rotation = Quaternion.Euler(Vector3.forward * this.cupAngle);
			if (this.cupAngle > 350f)
			{
				this.cupScript.ShowStars();
				this.rotateActive = false;
			}
			if (this.cupAngle > 359f)
			{
				break;
			}
			yield return null;
		}
		this.cupAngle -= 360f;
		this.angleWobble = true;
		yield break;
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x00092130 File Offset: 0x00090530
	public override void setScreen(Transform item)
	{
		this.puzzle = item;
		this.hintText.gameObject.SetActive(false);
		this.textLine.gameObject.SetActive(false);
		if (!Global.self.packHasTimeLine)
		{
			Global.self.cantExitEndScreenTimer = Mathf.Max(Global.self.cantExitEndScreenTimer, this.hintTimeWait);
		}
		this.PrepareTrophy();
		PuzzleStats component = this.puzzle.GetComponent<PuzzleStats>();
		if (component.activeAudioVoice != null)
		{
			component.activeAudioVoice.subsctibeToEnding(this);
		}
		else
		{
			this.SetAllTextLines("No ending found");
			this.ShowNextLine();
		}
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x000921DC File Offset: 0x000905DC
	private void PrepareTrophy()
	{
		if (Global.self.awardOnLevel == null)
		{
			return;
		}
		this.rotateActive = true;
		this.cup = Global.self.awardOnLevel;
		Global.self.awardOnLevel = null;
		this.cupScript = this.cup.GetComponent<PuzzleCup>();
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.bannerPref);
		transform.SetParent(this.puzzle, false);
		Banner component = transform.GetComponent<Banner>();
		component.SetActive(this.cupScript.monsterCup, this.cupScript.bannerColor);
		this.cupScript.SetCup(component);
		this.cupMoveTarget = new Vector2(0f, this.cupMoveTargetShiftY);
		base.StartCoroutine(this.RotateCup());
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x0009229D File Offset: 0x0009069D
	public void LockUntillVoiceHasEnded(bool on = true)
	{
		this.waitForTheVoice = on;
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x000922A6 File Offset: 0x000906A6
	public void SetEnding(string newLine, bool voiced)
	{
		if (string.IsNullOrEmpty(newLine))
		{
			newLine = "ERROR: can't be empty";
		}
		this.isEndingVoiced = voiced;
		this.SetAllTextLines(newLine);
		this.ShowNextLine();
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x000922D0 File Offset: 0x000906D0
	private void SetAllTextLines(string str)
	{
		string[] array = str.Split(new string[]
		{
			"(n)",
			"\n"
		}, StringSplitOptions.None);
		int num = array.Length;
		this.textLine.gameObject.SetActive(true);
		bool flag = false;
		bool flag2 = false;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			Transform line;
			if (i < num - 1)
			{
				line = UnityEngine.Object.Instantiate<Transform>(this.textLine);
				line.SetParent(this.textLine.parent);
				line.localScale = Vector3.one;
			}
			else
			{
				line = this.textLine;
			}
			line.SetAsLastSibling();
			line.gameObject.SetActive(false);
			endTextQueue endTextQueue = new endTextQueue
			{
				obj = line.gameObject
			};
			float d = UnityEngine.Random.Range(-this.rndRotation, this.rndRotation);
			line.Rotate(Vector3.forward * d);
			if (array[i].StartsWith("(u)"))
			{
				line.Rotate(Vector3.forward * 180f);
				array[i] = array[i].Replace("(u)", string.Empty);
			}
			if (array[i].Contains("(age)"))
			{
				array[i] = array[i].Replace("(age)", AudioVoice_CodePad.playersAge.ToString());
			}
			if (array[i].StartsWith("(r)"))
			{
				if (i > 0)
				{
					num2++;
					endTextQueue.onAppearCallback = delegate()
					{
						line.parent.GetChild(line.GetSiblingIndex() - 1).gameObject.SetActive(false);
					};
				}
				array[i] = array[i].Replace("(r)", string.Empty);
			}
			if (array[i].Contains("(playtime)"))
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds(Global.self.totalPlayedTime);
				string newValue;
				if (timeSpan.TotalHours < 25.0)
				{
					newValue = string.Format("{0:D2} : {1:D2} : {2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				}
				else
				{
					newValue = string.Format("{0} : {1:D2} : {2:D2}", timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				}
				array[i] = array[i].Replace("(playtime)", newValue);
			}
			RectTransform component = line.GetComponent<RectTransform>();
			component.anchoredPosition = Vector2.down * ((float)(i - num2) * this.moveEachLineY + this.initialY);
			if (flag2 || array[i].StartsWith("(place)"))
			{
				array[i] = array[i].Replace("(place)", string.Empty);
				flag2 = true;
				if (i == 0)
				{
					component.anchoredPosition = new Vector2(-25f, -363f);
				}
				else if (i == 1)
				{
					component.anchoredPosition = new Vector2(-125f, -307f);
				}
				else
				{
					component.anchoredPosition = new Vector2(44f, -286f);
				}
			}
			Regex regex = new Regex("\\(w(\\d+(?:\\.\\d+)?)\\)");
			Match match = regex.Match(array[i]);
			if (match.Success)
			{
				array[i] = array[i].Replace(match.Value, string.Empty);
				endTextQueue.waitBefore = float.Parse(match.Groups[1].Value);
			}
			endTextLine component2 = line.GetComponent<endTextLine>();
			component2.setParent(this);
			component2.textBox.fontSize = Mathf.RoundToInt((float)component2.textBox.fontSize * UIControl.self.BigFontScale);
			component2.textBox.GetComponent<Outline>().effectDistance *= UIControl.self.OutlineScale;
			if (UIControl.self.useBackupFont)
			{
				component2.textBox.font = UIControl.self.backupFont;
			}
			endTextQueue.anim = line.GetComponent<Animator>();
			if (this.cup == null)
			{
				if (this.puzzle.GetComponent<PuzzleStats>().solvedAsBad == true)
				{
					endTextQueue.animEffect = "Slam";
				}
				else
				{
					endTextQueue.animEffect = "SlowGrow";
				}
			}
			else if (array[i].StartsWith("(h)") || (!flag && i == num - 1))
			{
				array[i] = array[i].Replace("(h)", string.Empty);
				component2.textBox.fontSize = Mathf.RoundToInt(84f * UIControl.self.BigFontScale);
				component.anchoredPosition += Vector2.down * 10f;
				this.initialY += 10f;
				endTextQueue.animEffect = "cupSlam";
				endTextQueue.waitBefore += 0.2f;
				flag = true;
			}
			else
			{
				component2.textBox.fontSize = Mathf.RoundToInt(48f * UIControl.self.BigFontScale);
				this.initialY -= 5f;
				endTextQueue.animEffect = "tinyTextTop";
			}
			component2.setText(array[i]);
			this.list.Enqueue(endTextQueue);
		}
		this.textLine.gameObject.SetActive(false);
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x0009289F File Offset: 0x00090C9F
	public void ShowNextLineAfterAnimation()
	{
		this.animationPlaying = false;
		if (!this.isEndingVoiced)
		{
			this.ShowNextLine();
		}
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x000928B9 File Offset: 0x00090CB9
	public void ShowNextLineOnMarker()
	{
		if (this.isEndingVoiced && !this.noMoreLine)
		{
			this.ShowNextLine();
		}
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x000928D7 File Offset: 0x00090CD7
	public void StopVoiceEnding()
	{
		this.isEndingVoiced = false;
		if (!this.animationPlaying)
		{
			this.ShowNextLine();
		}
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x000928F4 File Offset: 0x00090CF4
	private void ShowNextLine()
	{
		endTextQueue endTextQueue;
		if (this.waitingItem == null)
		{
			if (this.list.Count == 0)
			{
				if (Global.self.packHasTimeLine && this.canExit)
				{
					this.canExit = false;
					Global.self.gotoNextLevel(false, null);
				}
				if (Global.self.packHasTimeLine && !this.canExit)
				{
					this.exitOnEndTimer = true;
				}
				this.noMoreLine = true;
				return;
			}
			endTextQueue = (this.list.Dequeue() as endTextQueue);
			if (this.list.Count == 0)
			{
			}
		}
		else
		{
			endTextQueue = this.waitingItem;
		}
		if (!this.isEndingVoiced && endTextQueue.waitBefore > 0f)
		{
			this.waitTimer = endTextQueue.waitBefore;
			this.waitingItem = endTextQueue;
			endTextQueue.waitBefore = 0f;
			return;
		}
		endTextQueue.obj.SetActive(true);
		if (endTextQueue.onAppearCallback != null)
		{
			endTextQueue.onAppearCallback();
		}
		endTextQueue.anim.SetTrigger(endTextQueue.animEffect);
		this.PlaySound(endTextQueue.animEffect);
		this.animationPlaying = true;
		this.waitingItem = null;
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x00092A30 File Offset: 0x00090E30
	private void PlaySound(string animation)
	{
		if (animation != null)
		{
			if (animation == "cupSlam")
			{
				Audio.self.playOneShot("22033ad9-4778-4b03-8aa7-f457c60b5771", 1f);
				return;
			}
			if (animation == "tinyTextTop")
			{
				Audio.self.playOneShot("916db86a-fc1f-492e-abeb-9311f9fbefb4", 1f);
				return;
			}
			if (animation == "Slam")
			{
				if (!this.endTextPlayedOnce)
				{
					Audio.self.playOneShot("1245874d-b6ad-4ff0-a306-6b9eac0dadf2", 1f);
				}
				else
				{
					Audio.self.playOneShot("d055f839-a6f7-439c-95b3-a552fac16000", 1f);
				}
				this.endTextPlayedOnce = true;
				return;
			}
			if (animation == "SlowGrow")
			{
				if (!this.endTextPlayedOnce)
				{
					Audio.self.playOneShot("2f7457f4-bcf0-4402-8b0b-648a36e2098e", 1f);
				}
				else
				{
					Audio.self.playOneShot("530d069d-2914-43c6-991f-22a236e3c51d", 1f);
				}
				this.endTextPlayedOnce = true;
				return;
			}
		}
		Debug.LogError("Can't find sound to this End Text Animation: " + animation);
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x00092B5D File Offset: 0x00090F5D
	protected override void cancelPressed()
	{
	}

	// Token: 0x0400220B RID: 8715
	private Transform puzzle;

	// Token: 0x0400220C RID: 8716
	public Transform textLine;

	// Token: 0x0400220D RID: 8717
	public float rndRotation = 7f;

	// Token: 0x0400220E RID: 8718
	public float initialY = 20f;

	// Token: 0x0400220F RID: 8719
	public float moveEachLineY = 50f;

	// Token: 0x04002210 RID: 8720
	private Queue list = new Queue();

	// Token: 0x04002211 RID: 8721
	private float waitTimer;

	// Token: 0x04002212 RID: 8722
	private endTextQueue waitingItem;

	// Token: 0x04002213 RID: 8723
	private bool canExit;

	// Token: 0x04002214 RID: 8724
	private bool exitOnEndTimer;

	// Token: 0x04002215 RID: 8725
	private bool isEndingVoiced = true;

	// Token: 0x04002216 RID: 8726
	private bool waitForTheVoice;

	// Token: 0x04002217 RID: 8727
	private bool noMoreLine;

	// Token: 0x04002218 RID: 8728
	private bool animationPlaying;

	// Token: 0x04002219 RID: 8729
	private const string ANIMATION_SLAM = "Slam";

	// Token: 0x0400221A RID: 8730
	private const string ANIMATION_SLOW_GROW = "SlowGrow";

	// Token: 0x0400221B RID: 8731
	private const string ANIMATION_CUP_SLAM = "cupSlam";

	// Token: 0x0400221C RID: 8732
	private const string ANIMATION_CUP_SMALL_TEXT = "tinyTextTop";

	// Token: 0x0400221D RID: 8733
	private bool endTextPlayedOnce;

	// Token: 0x0400221E RID: 8734
	[Header("Hint")]
	public RectTransform hintText;

	// Token: 0x0400221F RID: 8735
	[Tooltip("Default wait time between level finish and click to exit")]
	public float hintTimeWait = 0.5f;

	// Token: 0x04002220 RID: 8736
	private Transform cup;

	// Token: 0x04002221 RID: 8737
	private PuzzleCup cupScript;

	// Token: 0x04002222 RID: 8738
	[Header("Cup Move")]
	public float cupMoveTime = 0.5f;

	// Token: 0x04002223 RID: 8739
	private float cupMoveTimeCurr;

	// Token: 0x04002224 RID: 8740
	public float cupScaleTime = 0.6f;

	// Token: 0x04002225 RID: 8741
	private float cupScaleTimeCurr;

	// Token: 0x04002226 RID: 8742
	[Header("Cup Move Wobble")]
	public Vector2 cupWobbleEach;

	// Token: 0x04002227 RID: 8743
	public float cupWobbleEachCurr;

	// Token: 0x04002228 RID: 8744
	public float cupWobbleDistance = 0.2f;

	// Token: 0x04002229 RID: 8745
	public float cupWobbleSpeed = 5f;

	// Token: 0x0400222A RID: 8746
	public float cupMoveTargetShiftY = -1f;

	// Token: 0x0400222B RID: 8747
	private Vector2 cupMoveTarget = Vector2.zero;

	// Token: 0x0400222C RID: 8748
	private Vector2 cupTargetsTarget = Vector2.zero;

	// Token: 0x0400222D RID: 8749
	[Header("Cup Rotate")]
	public float angleSpeedMax;

	// Token: 0x0400222E RID: 8750
	private float cupAngle;

	// Token: 0x0400222F RID: 8751
	private bool rotateActive;

	// Token: 0x04002230 RID: 8752
	private bool angleWobble;

	// Token: 0x04002231 RID: 8753
	public AnimationCurve cupFirstLapSpeed;

	// Token: 0x04002232 RID: 8754
	public AnimationCurve cupLastLapSpeed;

	// Token: 0x04002233 RID: 8755
	[Header("Cup Rotate Wobble")]
	public float cupAngleWobbleSpeed = 0.5f;

	// Token: 0x04002234 RID: 8756
	private float cupAngleTarget;

	// Token: 0x04002235 RID: 8757
	public Vector2 cupAngleWobbleEach;

	// Token: 0x04002236 RID: 8758
	public float cupAngleWobbleEachCurr = 1f;

	// Token: 0x04002237 RID: 8759
	public float cupAngleWobbleDistance = 25f;

	// Token: 0x04002238 RID: 8760
	[Header("Banner")]
	public Transform bannerPref;
}
