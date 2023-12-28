using System;
using System.Collections;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000027 RID: 39
public class PuzzleCodePad_Display : MonoBehaviour
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060000DB RID: 219 RVA: 0x000097C3 File Offset: 0x000079C3
	// (set) Token: 0x060000DC RID: 220 RVA: 0x000097CB File Offset: 0x000079CB
	private string numbers
	{
		get
		{
			return this._numbers;
		}
		set
		{
			this._numbers = value;
			base.GetComponent<Text>().text = this._numbers.PadRight(4, '-');
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x000097ED File Offset: 0x000079ED
	public void Clear()
	{
		this.numbers = string.Empty;
		this.hideError();
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00009804 File Offset: 0x00007A04
	public void Accept()
	{
		if (this.numbers.Length < 4)
		{
			if (this.errorCoroutine == null)
			{
				this.PlayErrorSound();
				this.errorCoroutine = base.StartCoroutine(this.showError("NUMPAD_ERROR_TOO_SHORT"));
			}
			return;
		}
		if (this.numbers == "1234" || this.numbers == "0000")
		{
			if (this.errorCoroutine == null)
			{
				this.PlayErrorSound();
				this.errorCoroutine = base.StartCoroutine(this.showError("NUMPAD_PASSWORD_TOO_EASY"));
			}
			Global.self.currPuzzle.GetComponent<AudioVoice_CodePad>().easyPassword();
			return;
		}
		if (this.numbers == "80085")
		{
			Global.self.GetCup(AwardName.Pack13_CodePad);
		}
		base.StartCoroutine(this.showError("NUMPAD_PASSWORD_ACCEPTED"));
		Audio.self.playOneShot("008e5583-b373-4c00-aace-24eef7d7b0a6", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_CodePad>().setPassword(this.numbers);
		if (this.BadPassword())
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000993C File Offset: 0x00007B3C
	private IEnumerator showError(string guid)
	{
		this.errorText.GetComponent<LineTranslator>().setTextToTranslate(guid, WordTranslationContainer.Theme.PUZZLE);
		this.errorText.text = LineTranslator.addErrorsToText(this.errorText.text);
		this.canvas.alpha = 0f;
		this.errorText.enabled = true;
		yield return new WaitForSeconds(this.errorTime);
		this.hideError();
		yield break;
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000995E File Offset: 0x00007B5E
	private bool hideError()
	{
		if (this.errorCoroutine == null)
		{
			return false;
		}
		base.StopCoroutine(this.errorCoroutine);
		this.errorCoroutine = null;
		this.canvas.alpha = 1f;
		this.errorText.enabled = false;
		return true;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x000099A0 File Offset: 0x00007BA0
	public void Enter(int number)
	{
		if (this.hideError())
		{
			return;
		}
		if (this.numbers.Length == 5)
		{
			this.PlayErrorSound();
		}
		else if (this.numbers.Length == 4)
		{
			if ((this.numbers == "8008" && number == 5) || (this.numbers == "3133" && number == 7))
			{
				this.numbers += number;
			}
			else
			{
				this.PlayErrorSound();
			}
		}
		else
		{
			this.numbers += number;
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00009A5C File Offset: 0x00007C5C
	private bool BadPassword()
	{
		return this.AllSameNumbers() || this.SequenceOfNumbers();
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00009A74 File Offset: 0x00007C74
	private bool AllSameNumbers()
	{
		char c = this.numbers[0];
		foreach (char c2 in this.numbers)
		{
			if (c2 != c)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00009ABE File Offset: 0x00007CBE
	private void PlayErrorSound()
	{
		if (this.soundCooldownCoroutine == null)
		{
			this.soundCooldownCoroutine = base.StartCoroutine(this.ErrorSoundCoroutine());
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00009AE0 File Offset: 0x00007CE0
	private IEnumerator ErrorSoundCoroutine()
	{
		Audio.self.playOneShot("06d71daa-73d3-44ae-840f-0be7476480df", 1f);
		yield return new WaitForSeconds(this.errorTime);
		this.soundCooldownCoroutine = null;
		yield break;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00009AFC File Offset: 0x00007CFC
	private bool SequenceOfNumbers()
	{
		int num = -10;
		int num2 = int.Parse(this.numbers[0].ToString());
		for (int i = 1; i < this.numbers.Length; i++)
		{
			int num3 = int.Parse(this.numbers[i].ToString());
			if (num == -10)
			{
				num = num3 - num2;
			}
			if (Mathf.Abs(num) > 1 || num3 - num2 != num)
			{
				return false;
			}
			num2 = num3;
		}
		return true;
	}

	// Token: 0x0400015D RID: 349
	private string _numbers = string.Empty;

	// Token: 0x0400015E RID: 350
	private const string OBSCENE = "8008";

	// Token: 0x0400015F RID: 351
	private const string ELITE = "3133";

	// Token: 0x04000160 RID: 352
	public CanvasGroup canvas;

	// Token: 0x04000161 RID: 353
	public Text errorText;

	// Token: 0x04000162 RID: 354
	public float errorTime = 2f;

	// Token: 0x04000163 RID: 355
	private Coroutine errorCoroutine;

	// Token: 0x04000164 RID: 356
	private Coroutine soundCooldownCoroutine;
}
