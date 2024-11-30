using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public float raycastRange = 3f; // Range for the raycast
    public LayerMask interactableLayer; // Layer for interactable objects
    public bool hasKey = false; // Track if the player has the key
    private bool playerEntered;
    private Camera fpsCam;
	private GameObject player;
    public float reachRange = 1.8f;		
    private GUIStyle guiStyle;
	private string msg;
    private bool showInteractMsg;
    private int rayLayerMask; 

    void Start()
    {
        Debug.Log("Key script started");
        //Initialize moveDrawController if script is enabled.
		player = GameObject.FindGameObjectWithTag("Player");

		fpsCam = Camera.main;
		if (fpsCam == null)	//a reference to Camera is required for rayasts
		{
			Debug.LogError("A camera tagged 'MainCamera' is missing.");
		}

        interactableLayer = LayerMask.NameToLayer("Interactable");
        rayLayerMask = 1 << interactableLayer.value;  

        //setup GUI style settings for user prompts
		setupGui();
    }
    void OnTriggerEnter(Collider other)
	{		
		if (other.gameObject == player)		//player has collided with trigger
		{			
			playerEntered = true;

		}
	}

	void OnTriggerExit(Collider other)
	{		
		if (other.gameObject == player)		//player has exited trigger
		{			
			playerEntered = false;
			//hide interact message as player may not have been looking at object when they left
			showInteractMsg = false;		
		}
	}
    void Update()
    {
        if (playerEntered)
        {
            //Debug.Log("Player entered key zone");
            showInteractMsg = true;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0f));
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit,reachRange, rayLayerMask))
            {
                // Check what the raycast hit
                if (hit.collider.CompareTag("Key"))
                {
                    Debug.Log("It's a key!");
                    PickUpKey(hit.collider.gameObject);
                }
            }
        }
        
        
    }

    void PickUpKey(GameObject key)
    {
        if (Input.GetKeyDown(KeyCode.E)) // Use E for interaction
        {
            hasKey = true; // Set key to true
            Destroy(key); // Remove the key object
            Debug.Log("Key acquired!");
        }
    }

    #region GUI Config

	//configure the style of the GUI
	private void setupGui()
	{
		guiStyle = new GUIStyle();
		guiStyle.fontSize = 16;
		guiStyle.fontStyle = FontStyle.Bold;
		guiStyle.normal.textColor = Color.white;
		msg = "Press E to Pickup";
	}

	private string getGuiMsg(bool isOpen)
	{
		string rtnVal;
		rtnVal = "Press E to Pickup";

		return rtnVal;
	}

	void OnGUI()
	{
		if (showInteractMsg)  //show on-screen prompts to user for guide.
		{
			GUI.Label(new Rect (50,Screen.height - 50,200,50), msg,guiStyle);
		}
	}		
	//End of GUI Config --------------
	#endregion
}
