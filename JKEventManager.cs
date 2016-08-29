using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JKEventManager : MonoBehaviour
{
    public delegate void JKObjectDelegateCanvasList(GameObject go);
    public JKObjectDelegateCanvasList myDelegateCanvasList;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
}
