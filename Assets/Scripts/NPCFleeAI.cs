using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFleeAI : MonoBehaviour
{

    public Transform safeLocation; // Where the NPC will flee to
    public float fleeSpeed = 1f;
    public float doorCheckDistance = 2f;
    public float checkNavMeshRadius = 2f;

    private Coroutine doorCheckRoutine;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = fleeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Agent Position: " + agent.transform.position + " Destination: " + agent.destination);

    }

    public void RunAway()
    {
        if (safeLocation != null)
        {
            if (doorCheckRoutine != null)
            {
                StopCoroutine(doorCheckRoutine); // Stop any existing door check coroutine
            }
            doorCheckRoutine = StartCoroutine(CheckDoorsWhileMoving());
        }
    }

    private IEnumerator CheckDoorsWhileMoving()
    {
        while (true)
        {
            // Check for nearby door with the tag "NormalDoor"
            GameObject door = CheckForNearbyDoor();
            if (door != null)
            {
                Animator doorAnim = door.GetComponent<Animator>();
                if (doorAnim != null)
                {
                    MoveableObject moveableObject = door.GetComponent<MoveableObject>();

                    string animBoolNameNum = "isOpen_Obj_" + moveableObject.objectNumber.ToString();
                    bool isOpen = doorAnim.GetBool(animBoolNameNum);
                    isOpen = !isOpen;

                    if (!isOpen)
                    {
                        doorAnim.enabled = true;
                        doorAnim.SetBool(animBoolNameNum, isOpen);

                        agent.isStopped = true; // Stop the NPC while the door is being opened
                        yield return StartCoroutine(HandleDoorAndMove(doorAnim));
                        agent.isStopped = false;
                    }
                }
                else
                {
                    Debug.Log("No animations found on the door...");
                }
            }
            else
            {
                Debug.Log("GameObject door NULL!!");
            }

            // Resume movement to the safe location
            if (safeLocation != null && !agent.isStopped && IsOnValidNavMesh(safeLocation.position))
            {
                //agent.isStopped = false;
                agent.SetDestination(safeLocation.position);
            }
            else
            {
                Debug.Log("Safe location is not on a valid NavMesh.");
            }

            yield return new WaitForSeconds(1f); // Check for doors every 0.5 seconds
        }
    }

    // Method to check if a position is on a valid NavMesh surface
    private bool IsOnValidNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        // Use NavMesh.SamplePosition to check if the position is on a valid NavMesh
        if (NavMesh.SamplePosition(position, out hit, checkNavMeshRadius, NavMesh.AllAreas))
        {
            Debug.Log($"Safe Location: {position} is on a valid NavMesh at {hit.position}");
            return true;
        }
        else
        {
            Debug.Log($"Safe Location: {position} is NOT on a valid NavMesh.");

        }
        return false;
    }

    private IEnumerator HandleDoorAndMove(Animator doorAnim)
    {

        //doorAnim.SetBool(animBoolNameNum, true); // Open the door
        Debug.Log("Opening door...");

        yield return new WaitUntil(() => doorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !doorAnim.IsInTransition(0));
        Debug.Log("Door opened.");
    }

    /*public void RunAway()
    {
        if (safeLocation != null)
        {
            // Check for nearby door with the tag "NormalDoor"
            GameObject door = CheckForNearbyDoor();
            if (door != null)
            {
                Debug.Log("Door found: " + door.name);
                // Get the Animator component of the door
                Animator doorAnim = door.GetComponent<Animator>();
                if (doorAnim != null)
                {
                    // Check if the door is open (using the Animator parameter)
                    string animBoolNameNum = "isOpen_Obj_" + door.name; // Example parameter name, you can customize this
                    bool isOpen = doorAnim.GetBool(animBoolNameNum);

                    // If the door is closed, open it and wait for the animation to finish
                    if (!isOpen)
                    {
                        StartCoroutine(HandleDoorAndMove(doorAnim, animBoolNameNum));
                    }
                    else
                    {
                        // If the door is already open, move to the safe location
                        MoveToSafeLocation();
                    }
                }
            }
            else
            {
                // If no door is nearby, just move to the safe location
                Debug.Log("No doors detected.");
                MoveToSafeLocation();
            }
        }
    }*/

    private GameObject CheckForNearbyDoor()
    {
        // Visualize the ray in the Scene view
        Debug.DrawRay(transform.position, transform.forward * doorCheckDistance, Color.red, 1f);
        Debug.Log("Checking for nearby doors...");

        // Raycast to check if a door is within range
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, doorCheckDistance))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");
            // Check if the object hit has the "NormalDoor" tag
            if (hit.collider.CompareTag("NormalDoor"))
            {
                Debug.Log("Found a door with the NormalDoor tag: " + hit.collider.gameObject.name);
                return hit.collider.gameObject;  // Return the door GameObject if it exists
            }
            else
            {
                Debug.Log("Hit something, but it's not a door.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any objects.");
        }
        return null;  // Return null if no door is found
    }

    /*private IEnumerator HandleDoorAndMove(Animator doorAnim, string animBoolNameNum)
    {
        // Open the door (toggle animation)
        bool isOpen = doorAnim.GetBool(animBoolNameNum);
        doorAnim.SetBool(animBoolNameNum, !isOpen);

        // Wait for the door to fully open (animation finish check)
        yield return StartCoroutine(WaitForDoorAnimationToFinish(doorAnim));

        // Once the door is open, move to the safe location
        MoveToSafeLocation();
    }

    private IEnumerator WaitForDoorAnimationToFinish(Animator doorAnim)
    {
        // Wait until the door animation completes (normalized time >= 1)
        while (doorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || doorAnim.IsInTransition(0))
        {
            yield return null;  // Wait until animation completes
        }

        Debug.Log("Door is open, proceeding to safe location.");
    }*/

    /*public void MoveToSafeLocation()
    {
        if (safeLocation != null)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(safeLocation.position, out hit, 1.0f, NavMesh.AllAreas))
            {
                Debug.Log("Running away to: " + hit.position);
                agent.SetDestination(hit.position);
            }
            else
            {
                Debug.LogError("Safe location is not on a valid NavMesh!");
            }
        }
    }*/
}
