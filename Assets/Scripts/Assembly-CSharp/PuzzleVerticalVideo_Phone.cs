using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000467 RID: 1127
public class PuzzleVerticalVideo_Phone : MonoBehaviour
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0007DDC0 File Offset: 0x0007C1C0
	// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x0007DDC8 File Offset: 0x0007C1C8
	public bool horizontal { get; private set; }

	// Token: 0x06001CF9 RID: 7417 RVA: 0x0007DDD1 File Offset: 0x0007C1D1
	private void Start()
	{
		this.horizontal = true;
		this.count = this.timer.Find("Count");
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x0007DDF0 File Offset: 0x0007C1F0
	private void Update()
	{
		if (!this.recording && this.dragged)
		{
			Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			a.z = 0f;
			a -= base.transform.position;
			float num = Mathf.Atan2(a.y, a.x) * 57.29578f;
			float? num2 = this.delta;
			if (num2 == null)
			{
				this.delta = new float?(num - base.transform.localEulerAngles.z);
			}
			else
			{
				float num3 = num;
				float? num4 = this.delta;
				num = num3 - num4.Value;
				int[] array = new int[]
				{
					0,
					90,
					180,
					270
				};
				foreach (int num5 in array)
				{
					float f = Mathf.DeltaAngle(num, (float)num5);
					if (Mathf.Abs(f) <= 45f)
					{
						this.horizontal = (num5 == 90 || num5 == 270);
						if (Mathf.Abs(f) > this.degrees)
						{
							num = (float)num5 + this.degrees * -1f * Mathf.Sign(f);
						}
					}
				}
				base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(0f, 0f, num), Time.deltaTime * 10f * this.rotationSpeed);
			}
			this.currentLerpTime = 0f;
			this.startAngle = base.transform.localEulerAngles.z;
		}
		else if (Mathf.Abs(base.transform.localEulerAngles.z % 90f) > 0.1f)
		{
			float num6 = base.transform.localEulerAngles.z % 90f;
			float z = (num6 <= 45f) ? (base.transform.localEulerAngles.z - num6) : (base.transform.localEulerAngles.z + 90f - num6);
			this.currentLerpTime = Mathf.MoveTowards(this.currentLerpTime, this.lerpTime, Time.deltaTime);
			float num7 = this.currentLerpTime / this.lerpTime;
			num7 = Mathf.Sin(num7 * 3.1415927f * 0.5f);
			base.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0f, 0f, this.startAngle), Quaternion.Euler(0f, 0f, z), num7);
		}
		if (this.recording)
		{
			if (this.time == -1f)
			{
				this.rec.gameObject.SetActive(true);
				this.timer.gameObject.SetActive(true);
				this.time = 0f;
			}
			else
			{
				this.time += Time.deltaTime;
				if (this.counter == 2)
				{
					if (this.horizontal)
					{
						Global.LevelFailed(0f, true);
					}
					else
					{
						Global.LevelCompleted(0f, true);
					}
				}
				else if (this.time > 1f)
				{
					IEnumerator enumerator = this.count.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Transform transform = (Transform)obj;
							transform.gameObject.SetActive(false);
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
					this.count.GetChild(++this.counter).gameObject.SetActive(true);
					this.rec.gameObject.SetActive(true);
					this.time = 0f;
				}
				else if (this.time > 0.5f)
				{
					this.rec.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x0007E228 File Offset: 0x0007C628
	private void OnMouseDown()
	{
		this.dragged = true;
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x0007E234 File Offset: 0x0007C634
	private void OnMouseUp()
	{
		this.delta = null;
		this.dragged = false;
	}

	// Token: 0x04001B91 RID: 7057
	public Transform rec;

	// Token: 0x04001B92 RID: 7058
	public Transform timer;

	// Token: 0x04001B93 RID: 7059
	private Transform count;

	// Token: 0x04001B94 RID: 7060
	[HideInInspector]
	public bool recording;

	// Token: 0x04001B95 RID: 7061
	[Tooltip("How many degrees to move before shifting to other side")]
	public float degrees = 25f;

	// Token: 0x04001B96 RID: 7062
	public float rotationSpeed = 2f;

	// Token: 0x04001B98 RID: 7064
	private float? delta;

	// Token: 0x04001B99 RID: 7065
	private bool dragged;

	// Token: 0x04001B9A RID: 7066
	private float time = -1f;

	// Token: 0x04001B9B RID: 7067
	private int counter;

	// Token: 0x04001B9C RID: 7068
	[Tooltip("Bigger time --> slower animation")]
	public float lerpTime = 1f;

	// Token: 0x04001B9D RID: 7069
	private float startAngle;

	// Token: 0x04001B9E RID: 7070
	private float currentLerpTime;
}
