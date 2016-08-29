using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


public class JKController : MonoBehaviour, IPointerDownHandler
{

	//public GameObject mycanvas;
	public float mycanvas_y_offset = 5.0f;
    bool canvas_is_visible;

	static int count = 0;
	Brain brain;

	private GameObject canvasclone;
    //private bool canvas_is_visible = false;

    private JKEventManager myEventManager;

		// Use this for initialization
	void Start () {
		count = count + 1;
		setupCharacterCanvas ();
		setText (gameObject.name + "\n" + "just sitting with nothing to do");
		brain = new Brain (this);

        var player = GameObject.Find("Player");
        myEventManager = player.GetComponent<JKEventManager>();
        myEventManager.myDelegateCanvasList += canvas_visible_handler;

        canvas_is_visible = false;
        canvasclone.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		brain.update ();
		brain.showValues ();
	}

        
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("********************* ON POINTER DOWN  ***********************");
        myEventManager.myDelegateCanvasList(gameObject);

    }

    public void OnMouseDown()
    {
        Debug.Log("********************* ON MOUSE DOWN  ***********************");
        myEventManager.myDelegateCanvasList(gameObject);
    }
    
    public void canvas_visible_handler (GameObject go)
    {
        //Debug.Log("canvas_visible_hander: passed parameter " + go.ToString());
        //Debug.Log("canvas_visible_hander: this " + gameObject.ToString());

        if (gameObject == go)
        {
            Debug.Log("make_canvas_active: " + gameObject.name.ToString());
            canvas_is_visible = true;
            canvasclone.SetActive(true);
        }
        else
        {
//            Debug.Log("make_canvas_inactive: " + gameObject.name.ToString());
            canvas_is_visible = false;
            canvasclone.SetActive(false);
        }
        
    }

    

    // Perform actions when user clicks on the character object
    /*
    void OnMouseDown()
    {
        canvas_is_visible = true;
        Debug.Log("OnMouseDown!!! " + gameObject.name.ToString());
        BroadcastMessage("HideCanvas", true);
    }
    */
    
    // Target of SendMessage - used when user selects a game object to interact with it or see its info panel; that method 
    // broadcasts HideCanvas so that all other objects switch off their information panels
    void HideCanvas (bool ignore)
    {
        canvas_is_visible = false;
        Debug.Log("HideCanvas!!!: " + gameObject.name.ToString());
    }

	private void setupCharacterCanvas() {
		Vector3 upabit = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		upabit.y = mycanvas_y_offset;
		Quaternion q = Quaternion.LookRotation(-transform.forward, Vector3.up);
		
		try {
			canvasclone = (GameObject) Instantiate(Resources.Load ("npcCanvasPrefab"), upabit, q);
			canvasclone.transform.SetParent (gameObject.transform);
		}
		catch (Exception e) {
			Debug.Log ("jkController Exception setting up character canvas: " + e.ToString ());
		}
	}

	public void setText (String astring) {
		try {
			Transform mytext_transform = canvasclone.transform.Find("Text");
			Text mytext = (Text)mytext_transform.GetComponent<Text> ();
			mytext.text = astring;
		}
		catch (Exception e) {
			Debug.Log ("Exception accessing prefab text in setText JKController.cs: " + e.ToString ());
		}
	}

	public void appendText (String astring) {
		try {
			Transform mytext_transform = canvasclone.transform.Find("Text");
			Text mytext = (Text)mytext_transform.GetComponent<Text> ();
			mytext.text = mytext.text + astring;
		}
		catch (Exception e) {
			Debug.Log ("Exception accessing prefab text in appendText JKController.cs: " + e.ToString ());
		}
	}
}
