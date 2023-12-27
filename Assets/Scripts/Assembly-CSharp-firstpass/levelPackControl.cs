using UnityEngine;
using ExcelData;

public class levelPackControl : MonoBehaviour
{
	public Transform awardController;
	public AwardController awardControllerScript;
	public Transform packList;
	public int packIndex;
	public string headerGUID;
	public string headerText;
	public WordTranslationContainer.Theme headerType;
	public Color headerColor;
	public bool scrollablePack;
	public bool completionLine;
}
