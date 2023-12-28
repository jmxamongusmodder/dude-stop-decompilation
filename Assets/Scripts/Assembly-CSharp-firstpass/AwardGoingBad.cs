using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class AwardGoingBad : Award
{
	// Token: 0x060013F7 RID: 5111 RVA: 0x000322B4 File Offset: 0x000306B4
	protected override void setText()
	{
		base.setText();
		if (this.chosenCup == null)
		{
			return;
		}
		this.textHas = LineTranslator.translateText(this.awardName.ToString() + "_" + this.chosenCup.name.ToUpper(), WordTranslationContainer.Theme.AWARD, false, string.Empty);
		string[] array = this.textHas.Split(new char[]
		{
			'\n'
		});
		this.textHas = TextFormating.formatAcquiredAward(array[0], string.Join("\n", array, 1, array.Length - 1), false, false, true, false);
		this.textOld = LineTranslator.translateText(this.awardName.ToString() + "_" + this.chosenCup.name.ToUpper() + "_BAD", WordTranslationContainer.Theme.AWARD, false, string.Empty);
		array = this.textOld.Split(new char[]
		{
			'\n'
		});
		this.textOld = TextFormating.formatAcquiredAward(array[0], string.Join("\n", array, 1, array.Length - 1), false, false, true, false);
		this.textHas = TextFormating.format(this.textHas);
		this.textOld = TextFormating.format(this.textOld);
	}

	// Token: 0x060013F8 RID: 5112 RVA: 0x000323F0 File Offset: 0x000307F0
	public override void setCupState()
	{
		foreach (Transform transform in this.list)
		{
			transform.gameObject.SetActive(false);
		}
		CupStatus cupStatus = Global.self.cupList[this.awardName];
		if (cupStatus != CupStatus.Empty)
		{
			if (cupStatus != CupStatus.ShowAnimation)
			{
				if (cupStatus == CupStatus.Exist)
				{
					this.cup.GetComponent<SpriteRenderer>().enabled = false;
					this.chosenCup = this.list[UnityEngine.Random.Range(0, this.list.Length)];
					base.StartCoroutine(this.GoBad());
				}
			}
			else
			{
				this.cup.GetComponent<SpriteRenderer>().enabled = true;
				this.chosenCup = this.avocadoCup;
				base.StartCoroutine(this.GoBad());
				this.chosenCup.GetChild(0).gameObject.SetActive(false);
			}
		}
		else
		{
			this.cup.GetComponent<SpriteRenderer>().enabled = false;
		}
		base.setCupState();
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x000324F8 File Offset: 0x000308F8
	private IEnumerator GoBad()
	{
		this.mouseOverList = new Transform[]
		{
			this.cup,
			this.chosenCup.GetChild(0),
			this.chosenCup.GetChild(1)
		};
		this.chosenCup.gameObject.SetActive(true);
		this.chosenCup.GetChild(0).gameObject.SetActive(true);
		this.chosenCup.GetChild(1).gameObject.SetActive(false);
		while (!Global.self.NoCurrentTransition || Global.self.currentAwardAnimCount > 0)
		{
			yield return null;
		}
		yield return new WaitForSeconds(this.waitToGoBad);
		if (base.enabled)
		{
			this.cup.GetComponent<SpriteRenderer>().enabled = false;
			this.chosenCup.GetChild(0).gameObject.SetActive(false);
			this.chosenCup.GetChild(1).gameObject.SetActive(true);
			this.chosenCup.GetChild(2).gameObject.SetActive(true);
			this.textHas = this.textOld;
			if (this.lp.awardSelected == base.transform.GetInstanceID())
			{
				this.lp.awardText.text = this.textHas;
			}
			Audio.self.playOneShot("f6622e13-83a2-4ddf-9240-5ab2534d3b22", 1f);
		}
		yield break;
	}

	// Token: 0x040010AC RID: 4268
	[Header("Going Bad Cup List")]
	public Transform[] list;

	// Token: 0x040010AD RID: 4269
	private Transform chosenCup;

	// Token: 0x040010AE RID: 4270
	public Transform avocadoCup;

	// Token: 0x040010AF RID: 4271
	public float waitToGoBad = 3f;

	// Token: 0x040010B0 RID: 4272
	private string textOld;
}
