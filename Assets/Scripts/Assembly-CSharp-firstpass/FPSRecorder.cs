using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200054A RID: 1354
public class FPSRecorder : MonoBehaviour
{
	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00093664 File Offset: 0x00091A64
	public static FPSRecorder self
	{
		get
		{
			if (FPSRecorder._self == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("Global");
				if (gameObject == null)
				{
					FPSRecorder._self = null;
				}
				else
				{
					FPSRecorder._self = gameObject.GetComponent<FPSRecorder>();
				}
			}
			return FPSRecorder._self;
		}
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x000936B3 File Offset: 0x00091AB3
	private void Start()
	{
		this.startRecording();
	}

	// Token: 0x06001F0C RID: 7948 RVA: 0x000936BB File Offset: 0x00091ABB
	public static void turnFpsRecorder()
	{
		if (FPSRecorder.self.recordFPS)
		{
			FPSRecorder.self.stopRecording();
		}
		else
		{
			FPSRecorder.self.startRecording();
		}
	}

	// Token: 0x06001F0D RID: 7949 RVA: 0x000936E8 File Offset: 0x00091AE8
	[ContextMenu("Start Recording")]
	public void startRecording()
	{
		string str = DateTime.Now.ToString("ddMMyy_hhmmss");
		this.fileName = "FPS_log_" + str + ".txt";
		this.file = File.CreateText(this.fileName);
		Debug.LogWarning("FPS Recording is ON at " + str);
		this.lines = new List<string>();
		this.lastEvent = "START RECORDING";
		this.recordFPS = true;
		base.StartCoroutine(this.saveFPSReport());
		base.StartCoroutine(this.collectFPS());
	}

	// Token: 0x06001F0E RID: 7950 RVA: 0x00093778 File Offset: 0x00091B78
	[ContextMenu("Stop Recording")]
	public void stopRecording()
	{
		if (!this.recordFPS)
		{
			Debug.LogWarning("FPS Recorder wasn't ON");
			return;
		}
		this.OnDisable();
		this.recordFPS = false;
		this.file.Close();
		Debug.LogWarning("FPS Recording is OFF, saved in: " + this.fileName);
	}

	// Token: 0x06001F0F RID: 7951 RVA: 0x000937C8 File Offset: 0x00091BC8
	public void addEvent(string name)
	{
		if (!this.recordFPS)
		{
			return;
		}
		if (this.lastEvent != string.Empty)
		{
			Debug.LogWarning("FPS Recorder: Too much events per second?");
			this.lastEvent += "; ";
		}
		this.lastEvent += name;
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x00093828 File Offset: 0x00091C28
	private IEnumerator collectFPS()
	{
		while (this.recordFPS)
		{
			this.addLineToList();
			yield return new WaitForSeconds(this.recordEachSec);
		}
		yield break;
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x00093844 File Offset: 0x00091C44
	private void addLineToList()
	{
		this.lines.Add(string.Concat(new object[]
		{
			this.lastEvent,
			",",
			this.shortTime(),
			",",
			UIControl.FPS
		}));
		this.lastEvent = string.Empty;
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x000938A4 File Offset: 0x00091CA4
	private IEnumerator saveFPSReport()
	{
		while (this.recordFPS)
		{
			this.writeToFile();
			yield return new WaitForSeconds(5f);
		}
		yield break;
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x000938C0 File Offset: 0x00091CC0
	private void writeToFile()
	{
		if (this.lines.Count == 0)
		{
			return;
		}
		foreach (string value in this.lines)
		{
			this.file.WriteLine(value);
		}
		this.file.Flush();
		this.lines.Clear();
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x00093948 File Offset: 0x00091D48
	private void OnDisable()
	{
		if (!this.recordFPS)
		{
			return;
		}
		this.addEvent("STOP RECORDING");
		this.addLineToList();
		this.writeToFile();
		Debug.LogWarning("FPS Recording is OFF, saved in: " + this.fileName);
	}

	// Token: 0x06001F15 RID: 7957 RVA: 0x00093984 File Offset: 0x00091D84
	private string shortTime()
	{
		return DateTime.Now.ToString("hh:mm:ss");
	}

	// Token: 0x0400224F RID: 8783
	private static FPSRecorder _self;

	// Token: 0x04002250 RID: 8784
	private bool recordFPS;

	// Token: 0x04002251 RID: 8785
	private StreamWriter file;

	// Token: 0x04002252 RID: 8786
	private string fileName = string.Empty;

	// Token: 0x04002253 RID: 8787
	private List<string> lines;

	// Token: 0x04002254 RID: 8788
	public float recordEachSec = 0.5f;

	// Token: 0x04002255 RID: 8789
	private string lastEvent = string.Empty;
}
