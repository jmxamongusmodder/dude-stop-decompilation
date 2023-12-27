using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
	public bool active;
	public ConsoleSubMenu currentMenu;
	public RectTransform container;
	public AnimationCurve showContainerCurve;
	public AnimationCurve hideContainerCurve;
	public GameObject colliderToBlockMouse;
	public RectTransform mouseOverObj;
	public AnimationCurve mouseOverOnClickCurve;
	public float mouveOverOnClickAlpha;
	public float mouseOverOnClickTime;
	public Text inputField;
	public GameObject defaultInputField;
	public RectTransform pauseMenu;
	public RectTransform audioMenu;
	public RectTransform gameLoading;
	public RectTransform confirmExitMenu;
	public RectTransform contactingDeveloper;
	public RectTransform hundredPhotosDuck;
	public RectTransform queueDuckCkeck;
	public RectTransform cupDuck;
	public RectTransform cupDuck_DuckOptions;
	public RectTransform cupDuck_DestroyDuck;
	public RectTransform cupDuck_BlueButton;
	public RectTransform cupDuck_LastScreen;
	public RectTransform pack11_Port;
	public RectTransform pack11_Info;
	public RectTransform pack11_File;
	public RectTransform pack12_MailList;
}
