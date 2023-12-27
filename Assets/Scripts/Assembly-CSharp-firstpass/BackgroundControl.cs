using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
	public SpriteRenderer backgroundColor;
	public Transform curBG;
	public int layerOrderMin;
	public int shiftLayerOrder;
	public float backgroundPosZ;
	public float timeBetweenLayers;
	public float colorLerpSpeed;
	public float scaleSpeed;
	public float bgColorDelayMax;
	public float delayShowMax;
	public float hideColorSpeed;
}
