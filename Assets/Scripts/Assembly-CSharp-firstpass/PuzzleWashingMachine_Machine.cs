using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200046A RID: 1130
public class PuzzleWashingMachine_Machine : MonoBehaviour
{
	// Token: 0x06001D0B RID: 7435 RVA: 0x0007E65C File Offset: 0x0007CA5C
	private void Start()
	{
		this.closedDoorCollider = base.GetComponent<CircleCollider2D>();
		this.openedDoorCollider = (from x in base.GetComponents<BoxCollider2D>()
		where x.isTrigger
		select x).First<BoxCollider2D>();
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x0007E6A8 File Offset: 0x0007CAA8
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.openedDoor.gameObject.SetActive(!this.openedDoor.gameObject.activeSelf);
		this.openedDoorCollider.enabled = this.openedDoor.gameObject.activeSelf;
		this.closedDoor.gameObject.SetActive(!this.closedDoor.gameObject.activeSelf);
		this.closedDoorCollider.enabled = this.closedDoor.gameObject.activeSelf;
		if (this.openedDoorCollider.enabled)
		{
			Audio.self.playOneShot("f80e6907-123f-49e8-b737-fa202d7e4894", 1f);
		}
		foreach (Draggable draggable in this.clothes.GetComponentsInChildren<Draggable>())
		{
			draggable.snapEnabled = this.openedDoor.gameObject.activeSelf;
		}
		foreach (Draggable draggable2 in this.spinner.GetComponentsInChildren<Draggable>())
		{
			draggable2.dragEnabled = this.openedDoor.gameObject.activeSelf;
			draggable2.transform.position = ((!this.openedDoor.gameObject.activeSelf) ? new Vector3(draggable2.transform.position.x, draggable2.transform.position.y, 0.1f) : new Vector3(draggable2.transform.position.x, draggable2.transform.position.y, -0.1f));
		}
		if (this.closedDoor.gameObject.activeSelf)
		{
			if (this.spinner.childCount == 2)
			{
				if ((from x in this.spinner.GetComponentsInChildren<PuzzleWashingMachine_Cloth>()
				where x.isRed
				select x).Count<PuzzleWashingMachine_Cloth>() == 0)
				{
					Global.self.GetCup(AwardName.WASHING_MACHINE);
					base.StartCoroutine(this.Spin(true));
				}
				else
				{
					base.StartCoroutine(this.Spin(false));
				}
			}
			else if (this.spinner.childCount >= 3)
			{
				bool monster = false;
				foreach (PuzzleWashingMachine_Cloth puzzleWashingMachine_Cloth in this.spinner.GetComponentsInChildren<PuzzleWashingMachine_Cloth>())
				{
					if (puzzleWashingMachine_Cloth.isRed)
					{
						monster = true;
						break;
					}
				}
				base.StartCoroutine(this.Spin(monster));
			}
			else
			{
				Audio.self.playOneShot("79576189-1a61-4f97-8ef5-8b457b144ca2", 1f);
			}
		}
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x0007E978 File Offset: 0x0007CD78
	private IEnumerator Spin(bool monster)
	{
		Audio.self.playLoopSound("a646c420-e896-4154-b789-b672f984dfcf");
		if (this.spinner.childCount == 2)
		{
			if (monster)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_WashClothes>().onlyWhite = true;
			}
			else
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_WashClothes>().onlyRed = true;
			}
		}
		if (monster)
		{
			if (this.spinner.childCount == 2)
			{
				Global.LevelCompleted(this.startTime + this.failWaitTime, true);
			}
			else
			{
				Global.LevelCompleted(this.startTime + this.spinTime + this.waitBeforeOpening + this.waitBeforeThrowing, true);
			}
		}
		else
		{
			Global.LevelFailed(this.startTime + this.failWaitTime, true);
		}
		if (monster && this.spinner.childCount != 2)
		{
			Color origRed = (from x in this.spinner.GetComponentsInChildren<PuzzleWashingMachine_Cloth>()
			where x.isRed
			select x).First<PuzzleWashingMachine_Cloth>().GetComponent<SpriteRenderer>().color;
			Color newRed = origRed * 2f;
			IEnumerable<PuzzleWashingMachine_Cloth> clothes = from x in this.spinner.GetComponentsInChildren<PuzzleWashingMachine_Cloth>()
			where !x.isRed
			select x;
			List<SpriteRenderer> sprites = new List<SpriteRenderer>();
			foreach (PuzzleWashingMachine_Cloth puzzleWashingMachine_Cloth in clothes)
			{
				sprites.Add(puzzleWashingMachine_Cloth.GetComponent<SpriteRenderer>());
			}
			float waitTimer = 0f;
			float timer = 0f;
			while (waitTimer != this.spinTime)
			{
				timer = Mathf.MoveTowards(timer, this.startTime, Time.deltaTime);
				float t = Mathf.Sin(timer / this.startTime * 3.1415927f * 0.5f);
				waitTimer = Mathf.MoveTowards(waitTimer, this.spinTime, Time.deltaTime);
				this.spinner.rotation = Quaternion.Euler(0f, 0f, this.spinner.eulerAngles.z + t * this.rotationSpeed * Time.deltaTime);
				foreach (SpriteRenderer spriteRenderer in sprites)
				{
					spriteRenderer.color = Color.Lerp(Color.white, newRed, waitTimer / this.spinTime);
				}
				yield return null;
			}
			timer = 0f;
			while (timer != this.startTime)
			{
				timer = Mathf.MoveTowards(timer, this.startTime, Time.deltaTime);
				float t2 = Mathf.Sin(timer / this.startTime * 3.1415927f * 0.5f);
				t2 = 1f - t2;
				this.spinner.rotation = Quaternion.Euler(0f, 0f, this.spinner.eulerAngles.z + t2 * this.rotationSpeed * Time.deltaTime);
				yield return null;
			}
			yield return new WaitForSeconds(this.waitBeforeOpening);
			Audio.self.playOneShot("f80e6907-123f-49e8-b737-fa202d7e4894", 1f);
			this.openedDoor.gameObject.SetActive(!this.openedDoor.gameObject.activeSelf);
			this.closedDoor.gameObject.SetActive(!this.closedDoor.gameObject.activeSelf);
			yield return new WaitForSeconds(this.waitBeforeThrowing);
			IEnumerator enumerator3 = this.spinner.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					object obj = enumerator3.Current;
					Transform transform = (Transform)obj;
					SpriteRenderer component = transform.GetComponent<SpriteRenderer>();
					if (component != null)
					{
						component.sortingOrder = 1;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator3 as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			this.spinner.GetComponent<Collider2D>().enabled = false;
			Audio.self.stopLoopSound("a646c420-e896-4154-b789-b672f984dfcf", true);
			yield break;
		}
		float timer2 = 0f;
		for (;;)
		{
			timer2 = Mathf.MoveTowards(timer2, this.startTime, Time.deltaTime);
			float t3 = Mathf.Sin(timer2 / this.startTime * 3.1415927f * 0.5f);
			this.spinner.rotation = Quaternion.Euler(0f, 0f, this.spinner.eulerAngles.z + t3 * this.rotationSpeed * Time.deltaTime);
			yield return null;
		}
	}

	// Token: 0x04001BAA RID: 7082
	public Transform clothes;

	// Token: 0x04001BAB RID: 7083
	public Transform openedDoor;

	// Token: 0x04001BAC RID: 7084
	public Transform closedDoor;

	// Token: 0x04001BAD RID: 7085
	public Transform spinner;

	// Token: 0x04001BAE RID: 7086
	[Header("End animation stuff")]
	public float failWaitTime = 1f;

	// Token: 0x04001BAF RID: 7087
	public float rotationSpeed = 350f;

	// Token: 0x04001BB0 RID: 7088
	public float startTime = 1f;

	// Token: 0x04001BB1 RID: 7089
	public float spinTime = 3f;

	// Token: 0x04001BB2 RID: 7090
	public float waitBeforeOpening = 0.3f;

	// Token: 0x04001BB3 RID: 7091
	public float waitBeforeThrowing = 0.5f;

	// Token: 0x04001BB4 RID: 7092
	private BoxCollider2D openedDoorCollider;

	// Token: 0x04001BB5 RID: 7093
	private CircleCollider2D closedDoorCollider;
}
