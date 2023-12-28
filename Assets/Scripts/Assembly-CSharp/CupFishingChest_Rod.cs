using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class CupFishingChest_Rod : MonoBehaviour
{
	// Token: 0x060014A4 RID: 5284 RVA: 0x00038828 File Offset: 0x00036C28
	private void Start()
	{
		Vector2 vector = Camera.main.ViewportToWorldPoint(new Vector3(this.minX, 0f));
		Vector2 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(this.maxX, 0f));
		this.minX = vector.x;
		this.maxX = vector2.x;
		this.minJunkX = Camera.main.ViewportToWorldPoint(new Vector3(this.minJunkX, 0f)).x;
		this.maxJunkX = Camera.main.ViewportToWorldPoint(new Vector3(this.maxJunkX, 0f)).x;
		this.descendCurveTime = this.descendCurve.keys[this.descendCurve.length - 1].time;
		this.hookStartY = this.hook.transform.localPosition.y;
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x00038928 File Offset: 0x00036D28
	private void Update()
	{
		if (this.canBeClicked && Input.GetMouseButtonDown(0))
		{
			this.canBeClicked = false;
			this.movingDown = true;
			Audio.self.playOneShot("8b083deb-f424-40c8-bb09-0072b5eabb87", 1f);
			Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>().mouseDown();
		}
		if (!this.middleReached && (this.hook.transform.localPosition.y - this.hookStartY) / (this.chestY - this.hookStartY) > 0.6f)
		{
			this.middleReached = Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>().middleReached();
		}
		this.controller.UpdateHookPosition(Extensions.Between(this.hookStartY, this.chestY, this.hook.transform.localPosition.y, true));
		this.CheckChest();
		this.ChangeRodPosition();
		this.CheckBubbleEmission();
		this.CheckGarbage();
		this.MoveDown();
		this.UpdateLineScale();
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x00038A37 File Offset: 0x00036E37
	public void Activate()
	{
		base.gameObject.SetActive(true);
		base.enabled = false;
		base.StartCoroutine(this.AppearingCoroutine());
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x00038A59 File Offset: 0x00036E59
	public void StuffCaught(Transform obj)
	{
		base.StartCoroutine(this.AscendingCoroutine(obj));
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x00038A6C File Offset: 0x00036E6C
	private void MoveDown()
	{
		if (!this.movingDown)
		{
			return;
		}
		this.descendTimer = Mathf.MoveTowards(this.descendTimer, this.descendCurveTime, Time.deltaTime);
		Vector2 v = this.hook.transform.position;
		base.transform.parent.position += Vector3.up * this.descendCurve.Evaluate(this.descendTimer) * Time.deltaTime;
		this.hook.transform.position = v;
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x00038B10 File Offset: 0x00036F10
	private void ChangeRodPosition()
	{
		Vector2 v = base.transform.position;
		if (!this.chaseChest)
		{
			Vector2 vector = Camera.main.GetMousePosition();
			vector.x = Mathf.Clamp(vector.x, this.minX, this.maxX);
			vector.x += this.mouseOffset;
			v.x = Mathf.Lerp(v.x, vector.x, this.mouseLerpSpeed * Time.deltaTime);
			v.x = Mathf.MoveTowards(v.x, vector.x, this.mouseChaseSpeed * Time.deltaTime);
			v.x = Mathf.Clamp(v.x, this.minX + this.mouseOffset, this.maxX + this.mouseOffset);
		}
		else if (this.movingDown || !this.hook.gotStuff)
		{
			v.x = Mathf.Lerp(v.x, this.chest.position.x + this.mouseOffset, this.mouseLerpSpeed * Time.deltaTime);
			v.x = Mathf.MoveTowards(v.x, this.chest.position.x + this.mouseOffset, this.mouseChaseSpeed * Time.deltaTime);
		}
		base.transform.position = v;
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x00038C98 File Offset: 0x00037098
	private void CheckChest()
	{
		if (!this.chestCreated)
		{
			float x = UnityEngine.Random.Range(this.minX, this.maxX) * 0.8f;
			this.chest.position = new Vector2(x, this.chestY);
			this.chest.SetParent(base.transform.parent);
			this.chest.gameObject.SetActive(true);
			this.chestCreated = true;
			this.chestSprite = this.chest.GetComponentInChildren<SpriteRenderer>();
			this.sponge.position = this.chest.position;
			this.sponge.gameObject.SetActive(true);
		}
		if (!this.chestVisible)
		{
			Vector2 vector = Camera.main.WorldToViewportPoint(this.chest.position);
			if (vector.x > 0.05f && vector.x < 0.95f && vector.y > 0.05f && vector.y < 0.95f)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>().cupReached();
				this.chestVisible = true;
			}
		}
		float num = 0.1f;
		if (Mathf.Abs(this.hook.transform.position.y - this.chest.position.y) < num)
		{
			this.movingDown = false;
			this.chaseChest = true;
			this.hook.goingForChest = true;
		}
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x00038E28 File Offset: 0x00037228
	private void CheckGarbage()
	{
		if (!this.movingDown || this.chaseChest)
		{
			return;
		}
		this.garbageTimer = Mathf.MoveTowards(this.garbageTimer, this.randJunkTime, Time.deltaTime);
		if (this.garbageTimer == this.randJunkTime)
		{
			this.CreateGarbage();
			this.garbageTimer = 0f;
			this.randJunkTime = UnityEngine.Random.Range(this.minJunkTime, this.maxJunkTime);
		}
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x00038EA4 File Offset: 0x000372A4
	private void CreateGarbage()
	{
		int num = UnityEngine.Random.Range(0, this.junkPool.Length);
		float x = UnityEngine.Random.Range(this.minJunkX, this.maxJunkX);
		Vector2 vector = Camera.main.ViewportToWorldPoint(new Vector3(0f, -1.1f));
		if (vector.y < this.chest.position.y + this.chestGarbageOffset)
		{
			return;
		}
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.junkPool[num]);
		transform.position = new Vector2(x, vector.y);
		transform.SetParent(base.transform.parent);
		transform.gameObject.SetActive(true);
		if (UnityEngine.Random.value > 0.5f)
		{
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
		}
		this.garbage.Add(transform);
		base.StartCoroutine(this.WobblingCoroutine(transform));
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x00038FB8 File Offset: 0x000373B8
	private void DestroyGarbage()
	{
		foreach (Transform transform in this.garbage)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
		this.garbage.Clear();
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x00039024 File Offset: 0x00037424
	private void CheckBubbleEmission()
	{
		if (!this.bubblesCreated)
		{
			Vector2 vector = Camera.main.ViewportToWorldPoint(Vector2.one);
			int num = Mathf.Abs(Mathf.CeilToInt(this.chestY / vector.y));
			for (int i = 3; i <= num; i++)
			{
				Transform transform = UnityEngine.Object.Instantiate<Transform>(this.bubbles.transform);
				transform.position = new Vector3(this.bubbles.transform.position.x, vector.y * ((float)(-(float)i) + 0.3f));
				transform.SetParent(base.transform.parent);
				transform.GetComponent<ParticleSystem>().emission.rateOverTime.constant = this.bubbles.emission.rateOverTime.constant;
				transform.gameObject.SetActive(true);
			}
			this.bubblesCreated = true;
		}
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x00039128 File Offset: 0x00037528
	private void UpdateLineScale()
	{
		float num = this.hook.transform.position.y + this.hookLineOffsets.y - this.lineStart.position.y;
		this.line.position = new Vector3(this.lineStart.position.x, this.lineStart.position.y + num / 2f);
		this.line.localScale = new Vector3(this.line.localScale.x, num * this.lineScaleQuotient);
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x000391DC File Offset: 0x000375DC
	private IEnumerator AppearingCoroutine()
	{
		float timer = 0f;
		float maxTime = 1f;
		Vector2 start = base.transform.localPosition;
		Vector2 end = new Vector2(this.mouseOffset, start.y);
		while (timer != maxTime)
		{
			Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouse.x = Mathf.Clamp(mouse.x, this.minX, this.maxX);
			end.x = Mathf.Lerp(end.x, mouse.x + this.mouseOffset, this.mouseLerpSpeed * Time.deltaTime);
			end.x = Mathf.MoveTowards(end.x, mouse.x + this.mouseOffset, this.mouseChaseSpeed * Time.deltaTime);
			timer = Mathf.MoveTowards(timer, maxTime, Time.deltaTime);
			float t = Mathf.Sin(timer / maxTime * 3.1415927f * 0.5f);
			base.transform.localPosition = Vector2.Lerp(start, end, t);
			yield return null;
		}
		base.enabled = true;
		this.controller.RodActivated();
		yield break;
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x000391F8 File Offset: 0x000375F8
	private IEnumerator AscendingCoroutine(Transform obj)
	{
		obj.GetComponent<Collider2D>().enabled = false;
		this.hook.gotStuff = true;
		this.movingDown = false;
		bool isChest = obj.tag == "SuccessCollider";
		AnimationCurve curve = (!isChest) ? this.junkAscendCurve : this.chestAscendCurve;
		Audio.self.playOneShot("02258fb5-2894-4d6d-90e0-3553a706b4cc", 1f);
		float maxTime = curve.keys[curve.length - 1].time;
		float timer = 0f;
		Vector2 start = base.transform.parent.position;
		Vector2 end = Vector2.zero;
		Vector3 diff = this.hook.transform.position - obj.position;
		foreach (SpriteRenderer spriteRenderer in obj.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.transform.localPosition -= new Vector3(diff.x * obj.localScale.x, diff.y);
		}
		obj.localPosition += diff;
		foreach (ParticleSystem particleSystem in obj.GetComponentsInChildren<ParticleSystem>())
		{
			particleSystem.transform.SetParent(obj.parent);
		}
		obj.SetParent(this.hook.transform);
		float pointAngle = Mathf.Atan2(diff.x, diff.y) * 57.29578f;
		while (timer != maxTime)
		{
			timer = Mathf.MoveTowards(timer, maxTime, Time.deltaTime);
			Vector2 hookPosition = this.hook.transform.position;
			base.transform.parent.position = Vector2.Lerp(start, end, curve.Evaluate(timer));
			obj.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(0f, pointAngle, this.stuffRotationCurve.Evaluate(timer)));
			this.hook.transform.position = hookPosition;
			this.UpdateLineScale();
			yield return null;
		}
		obj.SetParent(base.transform.parent.parent);
		foreach (SpriteRenderer spriteRenderer2 in obj.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer2.transform.localPosition = Vector3.zero;
		}
		Vector3 newDiff = Quaternion.Euler(0f, 0f, obj.eulerAngles.z) * diff;
		obj.position -= newDiff;
		if (isChest)
		{
			Audio.self.playOneShot("4dfc67c2-7702-4ced-b235-38443bd7e3c4", 1f);
			Global.CupAcquired(this.chestSprite.transform.parent);
		}
		else
		{
			this.descendTimer = 0f;
			this.hook.GetComponent<Collider2D>().enabled = true;
			this.hook.gotStuff = false;
			this.canBeClicked = true;
			Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>().readyToClick();
			Rigidbody2D component = obj.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				Audio.self.playOneShot("a606d80e-4f5d-4123-a5b4-e622fb536fec", 1f);
				obj.SetParent(this.GetPuzzleStats().transform);
				int num = UnityEngine.Random.Range(-1, 2);
				component.isKinematic = false;
				component.AddForce(new Vector2(this.junkThrowForce.x * (float)num, this.junkThrowForce.y));
				this.garbage.Remove(obj);
				UnityEngine.Object.Destroy(obj.gameObject, this.junkDestructionTime);
			}
		}
		this.DestroyGarbage();
		yield break;
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x0003921C File Offset: 0x0003761C
	private IEnumerator WobblingCoroutine(Transform junk)
	{
		int sign = UnityEngine.Random.Range(-1, 2);
		Collider2D coll = junk.GetComponent<Collider2D>();
		float lastSine = 0f;
		while (junk != null && coll.enabled)
		{
			junk.position += Vector3.right * (float)sign * this.junkFloatSpeed * Time.deltaTime;
			float sine = Mathf.Sin(junk.position.x) * this.junkWobbleAmplitude;
			float newY = junk.position.y - lastSine + sine;
			lastSine = sine;
			junk.position = new Vector3(junk.position.x, newY);
			if (!this.trashInsideCamera)
			{
				Vector2 vector = Camera.main.WorldToViewportPoint(junk.position);
				if (vector.x > 0.1f && vector.x < 0.9f && vector.y > 0.1f && vector.y < 0.9f)
				{
					Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>().trashVisible();
					this.trashInsideCamera = true;
				}
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001211 RID: 4625
	[Header("Some random stuff")]
	public ParticleSystem bubbles;

	// Token: 0x04001212 RID: 4626
	public SpriteRenderer[] rodSprites;

	// Token: 0x04001213 RID: 4627
	public SpriteRenderer waterlineSprite;

	// Token: 0x04001214 RID: 4628
	public CupFishingChest_Controller controller;

	// Token: 0x04001215 RID: 4629
	[Header("Line stuff")]
	public Transform line;

	// Token: 0x04001216 RID: 4630
	public Transform lineStart;

	// Token: 0x04001217 RID: 4631
	public float lineScaleQuotient = 1.6f;

	// Token: 0x04001218 RID: 4632
	public Vector2 hookLineOffsets = new Vector3(0.0831f, 0.3f);

	// Token: 0x04001219 RID: 4633
	[Header("Hook")]
	public CupFishingChest_Hook hook;

	// Token: 0x0400121A RID: 4634
	public AnimationCurve descendCurve;

	// Token: 0x0400121B RID: 4635
	public AnimationCurve chestAscendCurve;

	// Token: 0x0400121C RID: 4636
	public AnimationCurve stuffRotationCurve;

	// Token: 0x0400121D RID: 4637
	public AnimationCurve junkAscendCurve;

	// Token: 0x0400121E RID: 4638
	public float minX;

	// Token: 0x0400121F RID: 4639
	public float maxX;

	// Token: 0x04001220 RID: 4640
	public float mouseLerpSpeed;

	// Token: 0x04001221 RID: 4641
	public float mouseChaseSpeed;

	// Token: 0x04001222 RID: 4642
	public float mouseOffset;

	// Token: 0x04001223 RID: 4643
	private float descendCurveTime;

	// Token: 0x04001224 RID: 4644
	private float descendTimer;

	// Token: 0x04001225 RID: 4645
	[Header("Water stuff")]
	public Transform[] junkPool;

	// Token: 0x04001226 RID: 4646
	public float junkFloatSpeed;

	// Token: 0x04001227 RID: 4647
	public Vector2 junkThrowForce;

	// Token: 0x04001228 RID: 4648
	public float minJunkTime;

	// Token: 0x04001229 RID: 4649
	public float maxJunkTime;

	// Token: 0x0400122A RID: 4650
	public float minJunkX;

	// Token: 0x0400122B RID: 4651
	public float maxJunkX;

	// Token: 0x0400122C RID: 4652
	public float junkWobbleAmplitude = 0.5f;

	// Token: 0x0400122D RID: 4653
	public float junkDestructionTime = 0.5f;

	// Token: 0x0400122E RID: 4654
	public Transform chest;

	// Token: 0x0400122F RID: 4655
	public Transform sponge;

	// Token: 0x04001230 RID: 4656
	public float chestY;

	// Token: 0x04001231 RID: 4657
	public float chestGarbageOffset = 10f;

	// Token: 0x04001232 RID: 4658
	private float randJunkTime;

	// Token: 0x04001233 RID: 4659
	private List<Transform> garbage = new List<Transform>();

	// Token: 0x04001234 RID: 4660
	private float garbageTimer;

	// Token: 0x04001235 RID: 4661
	private float hookStartY;

	// Token: 0x04001236 RID: 4662
	private SpriteRenderer chestSprite;

	// Token: 0x04001237 RID: 4663
	private bool bubblesCreated;

	// Token: 0x04001238 RID: 4664
	private bool chestCreated;

	// Token: 0x04001239 RID: 4665
	private bool chaseChest;

	// Token: 0x0400123A RID: 4666
	private bool movingDown;

	// Token: 0x0400123B RID: 4667
	private bool canBeClicked = true;

	// Token: 0x0400123C RID: 4668
	private bool trashInsideCamera;

	// Token: 0x0400123D RID: 4669
	private bool middleReached;

	// Token: 0x0400123E RID: 4670
	private bool chestVisible;
}
