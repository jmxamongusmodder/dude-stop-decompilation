using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200051B RID: 1307
public class ButtonControl : MonoBehaviour
{
	// Token: 0x06001DF5 RID: 7669 RVA: 0x00086FC8 File Offset: 0x000853C8
	private void Start()
	{
		if (this.listOfButtons != null)
		{
			if (this.firstButton == null)
			{
				this.firstButton = this.listOfButtons.GetChild(0).gameObject;
			}
			if (this.lastButton == null)
			{
				this.lastButton = this.listOfButtons.GetChild(this.listOfButtons.childCount - 1).gameObject;
			}
		}
		if (this.firstButton == null && this.lastButton == null)
		{
			Debug.LogWarning("Button: " + base.name + " doesn't have a first and last button - it should have at least one of this");
			base.enabled = false;
		}
		if ((this.firstButton != null && this.firstButton.GetComponent<Button>() == null) || (this.lastButton != null && this.lastButton.GetComponent<Button>() == null))
		{
			Debug.LogWarning(base.name + ". First or last button doesn't have Button component. Add, or change gameObjects");
		}
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x000870E8 File Offset: 0x000854E8
	private void Update()
	{
		string text = "Vertical";
		if (!this.Vertical)
		{
			text = "Horizontal";
		}
		if (Input.GetButtonDown(text) && EventSystem.current.currentSelectedGameObject == null)
		{
			if (Input.GetAxisRaw(text) < 0f)
			{
				if (this.firstButton)
				{
					EventSystem.current.SetSelectedGameObject(this.firstButton);
				}
			}
			else if (this.lastButton)
			{
				EventSystem.current.SetSelectedGameObject(this.lastButton);
			}
		}
	}

	// Token: 0x0400213D RID: 8509
	[Tooltip("True - buttons go verticaly. False - horizontaly")]
	public bool Vertical = true;

	// Token: 0x0400213E RID: 8510
	[Tooltip("List of buttons where first and (or) last button can be found, if next 2 field are empty")]
	public Transform listOfButtons;

	// Token: 0x0400213F RID: 8511
	[Tooltip("First button to be selected on arrow down")]
	public GameObject firstButton;

	// Token: 0x04002140 RID: 8512
	[Tooltip("Last button to be selected on arrow up")]
	public GameObject lastButton;
}
