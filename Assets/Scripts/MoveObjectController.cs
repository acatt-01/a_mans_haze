using UnityEngine;
using System.Collections;

public class MoveObjectController : MonoBehaviour
{
	public float reachRange = 1.8f;

	private Animator anim;
	private Camera fpsCam;
	private GameObject player;

	public UnityEngine.AI.NavMeshObstacle doorObstacle; // Reference to the NavMeshObstacle on the door

	public GameObject fleeingNPC; // Reference to the AI character

	private const string animBoolName = "isOpen_Obj_";

	private bool playerEntered;
	private bool showInteractMsg;
	private GUIStyle guiStyle;
	private string msg;
	public InteractObject key;

	private int rayLayerMask;
	//public float raycastRange = 3f; // Range for the raycast
	//public LayerMask interactableLayer; // Layer for interactable objects
	private bool keyAcquired; // Track if the player has the key


	void Start()
	{
		//Initialize moveDrawController if script is enabled.
		player = GameObject.FindGameObjectWithTag("Player");

		fpsCam = Camera.main;
		if (fpsCam == null) //a reference to Camera is required for rayasts
		{
			Debug.LogError("A camera tagged 'MainCamera' is missing.");
		}

		//create AnimatorOverrideController to re-use animationController for sliding draws.
		anim = GetComponent<Animator>();
		anim.enabled = false;  //disable animation states by default.  

		keyAcquired = false; // player has to acquire key

		//the layer used to mask raycast for interactable objects only
		LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
		rayLayerMask = 1 << iRayLM.value;

		//setup GUI style settings for user prompts
		setupGui();

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)     //player has collided with trigger
		{
			playerEntered = true;

		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)     //player has exited trigger
		{
			playerEntered = false;
			//hide interact message as player may not have been looking at object when they left
			showInteractMsg = false;
		}
	}



	void Update()
	{
		// update key state
		keyAcquired = key.hasKey;

		if (playerEntered)
		{

			//center point of viewport in World space.
			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit hit;

			//if raycast hits a collider on the rayLayerMask
			if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, reachRange, rayLayerMask))
			{
				MoveableObject moveableObject = null;
				//is the object of the collider player is looking at the same as me?
				if (!isEqualToParent(hit.collider, out moveableObject))
				{   //it's not so return;
					return;
				}

				if (moveableObject != null)     //hit object must have MoveableDraw script attached
				{
					// Check what the raycast hit
					if (hit.collider.CompareTag("NormalDoor"))
					{
						//Debug.Log("It's a normal door!");
						OpenNormalDoor(hit.collider.gameObject, moveableObject);
					}
					else if (hit.collider.CompareTag("ClosedDoor") && !keyAcquired)
					{
						Debug.Log("It's a closed door! get a key");
					}
					else if (hit.collider.CompareTag("ClosedDoor") && keyAcquired)
					{
						Debug.Log("It's a closed door! open it");
						OpenDoor(hit.collider.gameObject, moveableObject);
					}

				}
			}
			else
			{
				showInteractMsg = false;
			}
		}

	}

	void OpenNormalDoor(GameObject door, MoveableObject moveableObject)
	{
		showInteractMsg = true;
		string animBoolNameNum = animBoolName + moveableObject.objectNumber.ToString();

		bool isOpen = anim.GetBool(animBoolNameNum);    //need current state for message.
		msg = getGuiMsg(isOpen);

		if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("Fire1"))
		{
			anim.enabled = true;
			anim.SetBool(animBoolNameNum, !isOpen);
			msg = getGuiMsg(isOpen);
		}

	}

	void OpenDoor(GameObject door, MoveableObject moveableObject)
	{
		showInteractMsg = true;
		string animBoolNameNum = animBoolName + moveableObject.objectNumber.ToString();

		bool isOpen = anim.GetBool(animBoolNameNum);    //need current state for message.
		msg = getGuiMsg(isOpen);

		if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("Fire1"))
		{
			anim.enabled = true;

			anim.SetBool(animBoolNameNum, !isOpen);
			//Debug.Log($"Animator parameter {animBoolNameNum} toggled to {!isOpen}");

			msg = getGuiMsg(isOpen);
			//Debug.Log("Door opened!");
			//Debug.Log("Has kew been acquired?" + keyAcquired);

			isOpen = !isOpen;
			//Debug.Log($"isOpen: {isOpen}, Animator Parameter: {animBoolNameNum} = {anim.GetBool(animBoolNameNum)}");

			UnityEngine.AI.NavMeshObstacle doorObstacle = door.GetComponentInParent<UnityEngine.AI.NavMeshObstacle>();
			if (isOpen)
			{
				// Door is closing, re-enable the obstacle
				if (doorObstacle != null)
				{
					doorObstacle.enabled = true;
					Debug.Log("Door closed, obstacle re-enabled.");
				}
			}
			else
			{
				// Door is opening, disable the obstacle
				if (doorObstacle != null)
				{
					doorObstacle.enabled = false;
					Debug.Log("Door opened, obstacle disabled.");

					/*// Disable the door's collider when the door is opening
					Collider doorCollider = door.GetComponent<Collider>();  // Get the collider attached to the door
					if (doorCollider != null)
					{
						doorCollider.enabled = false;  // Disable the collider
						Debug.Log("Door opened, collider disabled.");
					}*/


				}

				// Force the NPC to find a valid path after the obstacle is disabled
				if (fleeingNPC != null)
				{
					// Start a coroutine that waits for the animation to finish
					StartCoroutine(WaitForDoorAnimationAndMoveNPC());
				}
			}
		}



	}

	private IEnumerator WaitForDoorAnimationAndMoveNPC()
	{
		// Wait until the door animation completes (normalizedTime >= 1)
		while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || anim.IsInTransition(0))
		{
			var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			//Debug.Log($"Animation state: {stateInfo.normalizedTime}, IsInTransition: {anim.IsInTransition(0)}");
			yield return null;  // Wait one frame
		}

		Debug.Log("Door animation finished. NPC can move now.");

		if (fleeingNPC != null)
		{

			Debug.Log("Door toggled.");

			if (fleeingNPC != null)
			{
				Debug.Log("Fleeing NPC is not null, triggering flee.");
				fleeingNPC.GetComponent<NPCFleeAI>().RunAway();
			}

			//Debug.Log("Fleeing NPC is not null, triggering flee.");
			//fleeingNPC.GetComponent<NPCFleeAI>().RunAway();

			UnityEngine.AI.NavMeshAgent agent = fleeingNPC.GetComponent<UnityEngine.AI.NavMeshAgent>();
			if (agent != null)
			{
				// Save the original speed
				//float originalSpeed = agent.speed;

				// Set the speed to a slower value for slow-motion effect
				//agent.speed = originalSpeed * 0.5f; // 0.2f for 20% of the original speed, adjust as needed
				agent.SetDestination(agent.destination);
				agent.avoidancePriority = 1;

				/*// Smoothly update the position until the NPC reaches the destination
				while (agent.remainingDistance > agent.stoppingDistance)
				{
					// Allow the agent to continue moving smoothly
					yield return null;  // Wait one frame for smooth movement
				}*/

				// NPC has reached the destination, stop the movement
				//agent.isStopped = true;

				// Restore the original speed after the slow-motion duration
				//agent.speed = originalSpeed;
				//Debug.Log("NPC reached the destination.");
			}
		}
	}

	//is current gameObject equal to the gameObject of other.  check its parents
	private bool isEqualToParent(Collider other, out MoveableObject draw)
	{
		draw = null;
		bool rtnVal = false;
		try
		{
			int maxWalk = 6;
			draw = other.GetComponent<MoveableObject>();

			GameObject currentGO = other.gameObject;
			for (int i = 0; i < maxWalk; i++)
			{
				if (currentGO.Equals(this.gameObject))
				{
					rtnVal = true;
					if (draw == null) draw = currentGO.GetComponentInParent<MoveableObject>();
					break;          //exit loop early.
				}

				//not equal to if reached this far in loop. move to parent if exists.
				if (currentGO.transform.parent != null)     //is there a parent
				{
					currentGO = currentGO.transform.parent.gameObject;
				}
			}
		}
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
		}

		return rtnVal;

	}


	#region GUI Config

	//configure the style of the GUI
	private void setupGui()
	{
		guiStyle = new GUIStyle();
		guiStyle.fontSize = 16;
		guiStyle.fontStyle = FontStyle.Bold;
		guiStyle.normal.textColor = Color.white;
		msg = "Press E/Fire1 to Open";
	}

	private string getGuiMsg(bool isOpen)
	{
		string rtnVal;
		if (isOpen)
		{
			rtnVal = "Press E/Fire1 to Close";
		}
		else
		{
			rtnVal = "Press E/Fire1 to Open";
		}

		return rtnVal;
	}

	void OnGUI()
	{
		if (showInteractMsg)  //show on-screen prompts to user for guide.
		{
			GUI.Label(new Rect(50, Screen.height - 50, 200, 50), msg, guiStyle);
		}
	}
	//End of GUI Config --------------
	#endregion
}
