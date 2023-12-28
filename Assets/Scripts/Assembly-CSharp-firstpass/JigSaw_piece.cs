using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000552 RID: 1362
public class JigSaw_piece : MonoBehaviour
{
	// Token: 0x06001F34 RID: 7988 RVA: 0x0009484C File Offset: 0x00092C4C
	public void OnMouseDown()
	{
		if (!this.active || !base.enabled || !Global.self.NoCurrentTransition)
		{
			return;
		}
		Global.self.unlockedJigsawPieces = Mathf.Min(++Global.self.unlockedJigsawPieces, 20);
		if (AwardController.self != null)
		{
			AwardController.self.jigsawCurrentCount++;
		}
		SerializablePuzzleStats.Get(Global.self.currPuzzle.name).jigSawPiecesFound++;
		SerializablePackSavedStats.Get(Global.self.currentLevelPack).jigSawPiecesFound++;
		Global.self.Save();
		Audio.self.playOneShot("73944741-dcc8-4887-927b-f7b29e839529", 1f);
		base.transform.SetParent(null);
		base.GetComponent<Collider2D>().enabled = false;
		this.selectedIcon.sortingLayerName = "Front";
		this.selectedIcon.sortingOrder = 100;
		this.trailParticles.gameObject.SetActive(true);
		this.explodeParticles.Play();
		base.StartCoroutine(this.flyAnimation());
		this.active = false;
		UIControl.self.showJigSawCounter();
		AnalyticsComponent.JigSawCollected(Global.self.currPuzzle.name);
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x000949A8 File Offset: 0x00092DA8
	private IEnumerator flyAnimation()
	{
		this.flying = true;
		Vector3 target = UIControl.self.jigsawFlyPoint.position;
		target.z = base.transform.position.z;
		Vector3 followPoint = target + UnityEngine.Random.insideUnitCircle.normalized * Mathf.Min(4f, Vector2.Distance(target, base.transform.position));
		float timer = 0f;
		while (Vector2.SqrMagnitude(base.transform.position - target) > 0.1f)
		{
			timer = Mathf.MoveTowards(timer, this.flySpeedCurve.GetAnimationLength(), Time.deltaTime);
			float speed = this.flySpeedCurve.Evaluate(timer);
			followPoint = Vector3.MoveTowards(followPoint, target, Time.deltaTime * (speed + this.moveSpeedMin) * this.moveSpeed * 1.1f);
			base.transform.position = Vector3.MoveTowards(base.transform.position, followPoint, Time.deltaTime * (speed + this.moveSpeedMin) * this.moveSpeed);
			base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, Vector3.one, Time.deltaTime * 1.5f);
			yield return null;
		}
		Audio.self.playOneShot("ff36adda-21c3-45ab-b358-4a5ce251bc43", 1f);
		this.explodeParticles.Play();
		this.selectedIcon.enabled = false;
		this.trailParticles.GetComponent<ParticleSystem>().EnableEmmision(false);
		UIControl.self.jigSawCollision();
		UnityEngine.Object.Destroy(base.gameObject, 2f);
		yield break;
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x000949C4 File Offset: 0x00092DC4
	public void spawnPiece(Transform origin)
	{
		SpriteRenderer component = origin.GetComponent<SpriteRenderer>();
		IEnumerator enumerator = this.spriteContainer.GetEnumerator();
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
		Transform child = this.spriteContainer.GetChild(UnityEngine.Random.Range(0, this.spriteContainer.childCount));
		child.gameObject.SetActive(true);
		child.localScale = new Vector2((float)Extensions.GetRandomSign(), (float)Extensions.GetRandomSign());
		this.selectedIcon = child.GetComponent<SpriteRenderer>();
		this.selectedIcon.sortingLayerName = component.sortingLayerName;
		this.selectedIcon.sortingOrder = component.sortingOrder;
		this.trailParticles.gameObject.SetActive(false);
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x00094AC0 File Offset: 0x00092EC0
	public void hide()
	{
		if (this.flying)
		{
			return;
		}
		this.active = false;
		if (base.enabled && base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.hideAnimation());
		}
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x00094B00 File Offset: 0x00092F00
	private IEnumerator hideAnimation()
	{
		float scale = base.transform.localScale.x;
		while (scale > 0f)
		{
			scale = Mathf.MoveTowards(scale, 0f, Time.deltaTime * 2f);
			base.transform.localScale = Vector2.one * scale;
			base.transform.Rotate(0f, 0f, Time.deltaTime * 300f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400226E RID: 8814
	public Transform spriteContainer;

	// Token: 0x0400226F RID: 8815
	public Transform trailParticles;

	// Token: 0x04002270 RID: 8816
	public ParticleSystem explodeParticles;

	// Token: 0x04002271 RID: 8817
	public AnimationCurve flySpeedCurve;

	// Token: 0x04002272 RID: 8818
	public float moveSpeed = 2f;

	// Token: 0x04002273 RID: 8819
	public float moveSpeedMin = 0.5f;

	// Token: 0x04002274 RID: 8820
	private SpriteRenderer selectedIcon;

	// Token: 0x04002275 RID: 8821
	private bool active = true;

	// Token: 0x04002276 RID: 8822
	private bool flying;
}
