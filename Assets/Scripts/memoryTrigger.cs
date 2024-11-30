using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class memoryTrigger : MonoBehaviour
{

    public float interactionDistance = 2f;
    public LayerMask interactableLayer;

    public DialogueRunner dialogueRunner;

    //private bool isNearMemoryObject = false;
    //private GameObject currentMemoryObject = null;
    private bool hasTriggeredMemory = false;
    private bool hasPressedEbutton = false;
    private bool hasExitZone = false;

    private DialogueManager dialogueManager;

    private Renderer objectRenderer;
    private Material originalMaterial;
    public Material highlightMaterial;

    public Light sceneLight;  // Reference to the light in the scene
    //public PostProcessVolume postProcessVolume;  // Reference to Post Process Volume
    public Camera mainCamera;  // Reference to Camera

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        Debug.Log("Memory Trigger Script Started");
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;

        dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
    }

    // Update is called once per frame
    void Update()
    {
        // Handle memory triggering on E press
        if (!hasPressedEbutton && hasTriggeredMemory && Input.GetKeyDown(KeyCode.E)) // If the player is near the memory object
        {
            Debug.Log("Memory triggered: " + gameObject.name);
            TriggerMemory(gameObject);
        }
    }

    // Detect when the player enters the trigger zone of the memory object
    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggeredMemory) // Only log once
        {
            //Debug.Log("OnTriggerEnter: " + other.gameObject.name);
            if (other.CompareTag("MemoryObject"))
            {
                hasTriggeredMemory = true;  // Set the flag to true
                Debug.Log("Entered memory object's trigger zone: " + other.gameObject.name);
                hasExitZone = false;
            }
        }
    }

    // Detect when the player exits the trigger zone of the memory object
    void OnTriggerExit(Collider other)
    {
        if (!hasExitZone) // Only log once
        {
            //Debug.Log("OnTriggerExit: " + other.gameObject.name);
            if (other.CompareTag("MemoryObject"))
            {
                hasExitZone = true;
                hasTriggeredMemory = false;  // Reset the flag when the player exits the trigger zone
                Debug.Log("Exited memory object's trigger zone.");
                hasPressedEbutton = false;

                // Remove the highlight when the player leaves the trigger zone
                HighlightObject(false);
                AdjustLightForMemory(false);  // Reset lighting when memory ends
                //AdjustPostProcessingForMemory(false);  // Reset post-processing
                AdjustCameraForMemory(false);  // Reset camera filter
            }
        }
    }

    // Trigger memory event (this is where you'd add memory effects, audio, etc.)
    void TriggerMemory(GameObject memoryObject)
    {
        hasPressedEbutton = true;
        Debug.Log("Carreguei no E: " + memoryObject.name);
        // Add memory effects, audio, visuals, etc. here

        PauseBackground();

        // Unlock and show the mouse cursor
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
        Cursor.visible = true;  // Make the cursor visible

        // Check if a dialogue is already running
        if (dialogueRunner.IsDialogueRunning)
        {
            dialogueManager.CloseDialogue();
            Debug.Log("Stopped the current dialogue.");
        }

        dialogueManager.TriggerDialogue("DaughterDialogue");
        dialogueRunner.StartDialogue("DaughterDialogue");

        // Highlight the object
        HighlightObject(true);

        // Adjust lighting and visual effects for memory
        AdjustLightForMemory(true);
        //AdjustPostProcessingForMemory(true);
        AdjustCameraForMemory(true);


    }

    void PauseBackground()
    {
        // Find all instances of PlayerMove
        PlayerMove[] playerMoves = FindObjectsOfType<PlayerMove>();
        foreach (var playerMove in playerMoves)
        {
            Debug.Log("Disabling PlayerMove...");
            playerMove.enabled = false;  // Disable each instance of PlayerMove script
        }

        // Disable PlayerLook scripts in the same way
        PlayerLook[] playerLooks = FindObjectsOfType<PlayerLook>();
        foreach (var playerLook in playerLooks)
        {
            Debug.Log("Disabling PlayerLook...");
            playerLook.enabled = false;  // Disable each instance of PlayerLook script
        }

        // Optionally pause other systems like enemy AI, physics, or animations
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (var animator in animators)
        {
            animator.speed = 0f;  // Pause all background animations
        }

        // You can also disable other scripts or systems that you don't want active during dialogue
        // E.g., Disable background music, AI, etc.
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (var audio in audioSources)
        {
            if (audio != dialogueRunner.GetComponent<AudioSource>()) // Exclude dialogue audio
            {
                audio.Pause();  // Pause all other audio
            }
        }
    }

    void ResumeBackground()
    {
        // Re-enable PlayerMove and PlayerLook scripts to resume player movement and look control
        PlayerMove[] playerMoves = FindObjectsOfType<PlayerMove>();
        foreach (var playerMove in playerMoves)
        {
            playerMove.enabled = true;  // Disable each instance of PlayerMove script
        }

        PlayerLook[] playerLooks = FindObjectsOfType<PlayerLook>();
        foreach (var playerLook in playerLooks)
        {
            playerLook.enabled = true;  // Disable each instance of PlayerLook script
        }

        // Resume background animations
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (var animator in animators)
        {
            animator.speed = 1f;  // Resume animations
        }

        // Resume other systems like background audio or AI
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (var audio in audioSources)
        {
            if (audio != dialogueRunner.GetComponent<AudioSource>()) // Exclude dialogue audio
            {
                audio.UnPause();  // Resume all other audio
            }
        }
    }

    // Method to highlight or unhighlight the object by changing its material
    void HighlightObject(bool highlight)
    {
        if (highlight)
        {
            objectRenderer.material = highlightMaterial;  // Set highlight material
        }
        else
        {
            objectRenderer.material = originalMaterial;  // Revert back to original material
        }
    }

    // Method to adjust the lighting for the memory
    void AdjustLightForMemory(bool isMemoryActive)
    {
        if (sceneLight != null)
        {
            if (isMemoryActive)
            {
                sceneLight.intensity = 0.05f;  // Dim the light

                sceneLight.color = new Color(1f, 0.1f, 0.1f);  // Change light to red
            }
            else
            {
                // Reset light properties to default values
                sceneLight.intensity = 1.0f;  // Default intensity
                sceneLight.color = Color.white;  // Default color (white)
                sceneLight.shadows = LightShadows.None;  // No shadows (default)
                sceneLight.shadowStrength = 1.0f;  // Default shadow strength
                sceneLight.shadowBias = 0.05f;  // Default shadow bias
                sceneLight.colorTemperature = 6500f;  // Default daylight temperature
                sceneLight.useColorTemperature = false;  // Disable color temperature adjustment

            }
        }
    }
    /*
    // Method to adjust post-processing effects for the memory
    void AdjustPostProcessingForMemory(bool isMemoryActive)
    {
        if (postProcessVolume != null)
        {
            var bloom = postProcessVolume.profile.GetSetting<Bloom>();
            var vignette = postProcessVolume.profile.GetSetting<Vignette>();

            if (isMemoryActive)
            {
                bloom.intensity.value = 0.8f;
                vignette.intensity.value = 0.5f;
            }
            else
            {
                bloom.intensity.value = 0f;
                vignette.intensity.value = 0f;
            }
        }
    }*/

    // Method to adjust the camera's background color during the memory
    void AdjustCameraForMemory(bool isMemoryActive)
    {
        if (mainCamera != null)
        {
            if (isMemoryActive)
            {
                mainCamera.clearFlags = CameraClearFlags.SolidColor;
                mainCamera.backgroundColor = new Color(0.3f, 0.3f, 0.3f);  // Apply sepia tone
            }
            else
            {
                mainCamera.clearFlags = CameraClearFlags.Skybox;
                //mainCamera.backgroundColor = Color.white;  // Reset to default
            }
        }
    }

    void OnDialogueComplete()
    {
        Debug.Log("Dialogue complete.");

        ResumeBackground();

        // Lock the cursor back to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
