using UnityEngine;
using ExcelData;
using UnityEngine.UI;

public class LineTranslator : MonoBehaviour
{
	public bool scriptControlled;
	public WordTranslationContainer.Theme type;
	public string guid;
	[MultilineAttribute]
	public string enText;
	public string comment;
	public Text[] components;
	public string currentText;
	public bool canHaveErrors;
	[MultilineAttribute]
	public string appendToTheEnd;
	public string appendToStart;
}
