using System;
using UnityEngine;

// Token: 0x0200045D RID: 1117
public class PuzzleSwipePhoto_Photo : MonoBehaviour
{
	// Token: 0x1700006D RID: 109
	// (set) Token: 0x06001CB4 RID: 7348 RVA: 0x0007B8B5 File Offset: 0x00079CB5
	public bool clickable
	{
		set
		{
			base.GetComponent<Collider2D>().enabled = value;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x0007B8C3 File Offset: 0x00079CC3
	// (set) Token: 0x06001CB6 RID: 7350 RVA: 0x0007B8CB File Offset: 0x00079CCB
	public int shownPhoto { get; private set; }

	// Token: 0x06001CB7 RID: 7351 RVA: 0x0007B8D4 File Offset: 0x00079CD4
	private void Update()
	{
		float num = 3.5f;
		if (this.dragged)
		{
			Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				float num2 = (Mathf.Abs(this.startingMousePosition.x - vector.x) <= this.maxX) ? (this.startingMousePosition.x - vector.x) : (this.maxX * Mathf.Sign(this.startingMousePosition.x - vector.x));
				Transform child = base.transform.GetChild(i);
				child.localPosition = new Vector3((float)(i - this.shownPhoto) * num - num2, child.localPosition.y);
			}
		}
		else if (this.swipeTimer != -1f)
		{
			if (this.preparedAlbum)
			{
				this.swipeTimer = Mathf.MoveTowards(this.swipeTimer, this.swipeTime, Time.deltaTime);
				Transform child2 = base.transform.GetChild(this.shownPhoto);
				child2.localPosition = Vector3.Lerp(new Vector3(0f, child2.localPosition.y), new Vector3(num, child2.localPosition.y), this.swipeTimer / this.swipeTime);
				this.albumScreen.localPosition = Vector3.Lerp(new Vector3(-num, this.albumScreen.localPosition.y), new Vector3(0f, this.albumScreen.localPosition.y), this.swipeTimer / this.swipeTime);
				if (this.swipeTimer == this.swipeTime)
				{
					foreach (PuzzleSwipePhoto_Thumbnail puzzleSwipePhoto_Thumbnail in this.albumScreen.GetComponentsInChildren<PuzzleSwipePhoto_Thumbnail>())
					{
						puzzleSwipePhoto_Thumbnail.clickable = true;
						puzzleSwipePhoto_Thumbnail.onScreen = true;
					}
					for (int k = 0; k < base.transform.childCount; k++)
					{
						Transform child3 = base.transform.GetChild(k);
						child3.localPosition = new Vector3(num * (float)(k + 1), child2.localPosition.y);
					}
					this.swipeTimer = -1f;
					this.shownPhoto = 0;
					this.onScreen = false;
					this.clickable = false;
					this.preparedAlbum = false;
					this.exitAlbum = false;
				}
			}
			else
			{
				this.swipeTimer = Mathf.MoveTowards(this.swipeTimer, this.swipeTime, Time.deltaTime);
				for (int l = 0; l < base.transform.childCount; l++)
				{
					Transform child4 = base.transform.GetChild(l);
					child4.localPosition = Vector3.Lerp(child4.localPosition, new Vector3((float)(l - this.shownPhoto) * num, child4.localPosition.y), this.swipeTimer / this.swipeTime);
				}
				if (this.albumScreen.gameObject.activeSelf)
				{
					this.albumScreen.localPosition = Vector3.Lerp(this.albumScreen.localPosition, new Vector3(-1f * num, this.albumScreen.localPosition.y), this.swipeTimer / this.swipeTime);
					if (this.swipeTimer == this.swipeTime)
					{
						foreach (PuzzleSwipePhoto_Thumbnail puzzleSwipePhoto_Thumbnail2 in this.albumScreen.GetComponentsInChildren<PuzzleSwipePhoto_Thumbnail>())
						{
							puzzleSwipePhoto_Thumbnail2.onScreen = false;
						}
						this.albumScreen.gameObject.SetActive(false);
					}
				}
				if (this.swipeTimer == this.swipeTime)
				{
					if (this.exitAlbum)
					{
						this.swipeTimer = 0f;
						this.albumScreen.gameObject.SetActive(true);
						this.preparedAlbum = true;
					}
					else if (this.shownPhoto == base.transform.childCount - 1)
					{
						if (this.fail)
						{
							Global.LevelCompleted(0f, true);
						}
						else
						{
							base.transform.parent.GetComponent<PuzzleSwipePhoto_Phone>().albumSwiped = true;
						}
					}
					if (!this.exitAlbum)
					{
						this.swipeTimer = -1f;
					}
				}
			}
		}
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x0007BD59 File Offset: 0x0007A159
	private void OnMouseDown()
	{
		if (!base.enabled || this.swipeTimer != -1f)
		{
			return;
		}
		this.startingMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.dragged = true;
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x0007BD98 File Offset: 0x0007A198
	private void OnMouseUp()
	{
		if (!this.dragged)
		{
			return;
		}
		this.dragged = false;
		Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (Mathf.Abs(this.startingMousePosition.x - vector.x) > this.minX)
		{
			AudioVoice_SwipePhoto component = Global.self.currPuzzle.GetComponent<AudioVoice_SwipePhoto>();
			Audio.self.playOneShot("98b12082-bf70-46dc-9857-6c22b26a9d8c", 1f);
			if (Mathf.Sign(this.startingMousePosition.x - vector.x) == 1f && this.shownPhoto != base.transform.childCount - 1)
			{
				this.shownPhoto++;
				if (!this.fail && this.shownPhoto == base.transform.childCount - 1)
				{
					this.lastGoodPhotoSeen = true;
					component.SwipeLastGood();
				}
			}
			else if (Mathf.Sign(this.startingMousePosition.x - vector.x) == -1f)
			{
				if (!this.fail && this.lastGoodPhotoSeen && !this.swipedToPreviousPhoto)
				{
					this.swipedToPreviousPhoto = true;
					component.SwipeBackFromLastGood();
				}
				if (this.shownPhoto != 0)
				{
					this.shownPhoto--;
				}
				else
				{
					this.exitAlbum = true;
					if (!this.fail && this.firstSwipe)
					{
						component.SwipeToAlbum();
						this.firstAlbumExitCommented = true;
					}
					else if (!this.fail && this.firstAlbumExitCommented && !this.secondAlbumExitCommented)
					{
						component.SwipeToAlbumAgain();
						this.secondAlbumExitCommented = true;
					}
					if (!this.fail)
					{
						component.SwipeToAlbumAfterAllGood();
					}
				}
			}
			if (this.fail)
			{
				this.hand.GetComponent<PuzzleSwipePhoto_Hand>().Shake(this.shownPhoto);
			}
			this.firstSwipe = false;
		}
		this.swipeTimer = 0f;
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x0007BFA1 File Offset: 0x0007A3A1
	public void MoveIn()
	{
		this.onScreen = true;
		this.clickable = true;
		this.swipeTimer = 0f;
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x0007BFBC File Offset: 0x0007A3BC
	private void MoveOut()
	{
	}

	// Token: 0x04001B2E RID: 6958
	[Tooltip("Whether or not the level fails by reaching the end of the album")]
	public bool fail;

	// Token: 0x04001B2F RID: 6959
	public Transform albumScreen;

	// Token: 0x04001B30 RID: 6960
	public Transform hand;

	// Token: 0x04001B31 RID: 6961
	public Vector2 phoneSize;

	// Token: 0x04001B32 RID: 6962
	public Transform phone;

	// Token: 0x04001B33 RID: 6963
	[Tooltip("Maximum X shift allowed for each swipe")]
	public float maxX;

	// Token: 0x04001B34 RID: 6964
	[Tooltip("Minimum X shift required to switch to another image")]
	public float minX;

	// Token: 0x04001B35 RID: 6965
	[Tooltip("Seconds required for images to take their place")]
	public float swipeTime = 0.4f;

	// Token: 0x04001B37 RID: 6967
	public bool onScreen;

	// Token: 0x04001B38 RID: 6968
	private Vector2 startingMousePosition;

	// Token: 0x04001B39 RID: 6969
	private bool dragged;

	// Token: 0x04001B3A RID: 6970
	private bool exitAlbum;

	// Token: 0x04001B3B RID: 6971
	private bool preparedAlbum;

	// Token: 0x04001B3C RID: 6972
	private float swipeTimer = -1f;

	// Token: 0x04001B3D RID: 6973
	private bool firstSwipe = true;

	// Token: 0x04001B3E RID: 6974
	private bool firstAlbumExitCommented;

	// Token: 0x04001B3F RID: 6975
	private bool secondAlbumExitCommented;

	// Token: 0x04001B40 RID: 6976
	private bool lastGoodPhotoSeen;

	// Token: 0x04001B41 RID: 6977
	private bool swipedToPreviousPhoto;
}
