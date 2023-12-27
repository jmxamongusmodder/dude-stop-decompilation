using UnityEngine;

public class AnalyticsComponent : MonoBehaviour
{
	public bool internetAccessAllowed;
	[SerializeField]
	private bool showDebugInfo;
	[SerializeField]
	private int flushEntryCount;
	[SerializeField]
	private int maxFailCount;
	[SerializeField]
	private string address;
}
