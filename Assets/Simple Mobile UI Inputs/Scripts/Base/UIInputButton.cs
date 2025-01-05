using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UI_Inputs.Tools;
using UI_InputSystem.Base;
using UnityEngine.UI;

namespace UI_Inputs
{
    public class UIInputButton : UIInput<ButtonAction, bool>, IPointerDownHandler, IPointerUpHandler
    {
        [Header("---------Button Type---------------")]
        [SerializeField]
        private ButtonType buttonType = ButtonType.Click;

        [Header("---------Button Action-------------")]
        //[SerializeField]
        //private ButtonAction buttonAction = ButtonAction.Jump;

        [Header("Progress Settings (For Hold)")]
        [SerializeField] private Slider progressBar; // Reference to the progress bar (UI Slider)
        [SerializeField] private float holdDuration = 2.0f; // Time required to fill the progress bar
        [SerializeField] private CanvasGroup joystickCanvasGroup;


        //private GameObject currentInteractableObject; // Object we are interacting with
        //private MoveObjectController moveObjectController;

        //public override bool InputDefaultValue => false;
        //public override bool InputValue => isPressing;
        //public override ButtonAction InputID => buttonAction;
        MoveObjectController mobileTrigger;

        public event Action OnHoldComplete;
        public event Action OnClick;
        public event Action OnTouch;
        public event Action OnHold;

        private bool isPressing;
        private Coroutine holdCoroutine;
        private float holdProgress;

        private enum ButtonType
        {
            Click,
            Touch,
            Hold
        }

        void Start()
        {
            mobileTrigger = FindObjectOfType<MoveObjectController>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //ProcessClick();
            //OnTouch?.Invoke();

            switch (buttonType)
            {
                case ButtonType.Touch:
                    TriggerTouch();
                    break;
                case ButtonType.Hold:
                    StartHold();
                    break;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //ProcessClick(false);
            //OnClick?.Invoke();

            switch (buttonType)
            {
                case ButtonType.Click:
                    TriggerClick();
                    break;
                case ButtonType.Hold:
                    StopHold();
                    break;
            }
        }

        private void TriggerTouch()
        {
            OnTouch?.Invoke();
            Debug.Log("Touch action triggered.");
            StartCoroutine(MakeAFrameClick());
        }

        private void TriggerClick()
        {
            OnClick?.Invoke();
            Debug.Log("Click action triggered.");
        }

        private void StartHold()
        {
            if (holdCoroutine == null)
            {
                isPressing = true;
                holdProgress = 0f;
                progressBar.gameObject.SetActive(true);
                progressBar.value = 0f;

                // Disable joystick input if canvas group is assigned
                if (joystickCanvasGroup != null)
                    joystickCanvasGroup.interactable = false;

                if (holdCoroutine != null)
                {
                    StopCoroutine(holdCoroutine);
                    holdCoroutine = null;
                }
                holdCoroutine = StartCoroutine(HoldAction());
            }
        }

        private void StopHold()
        {
            if (holdCoroutine != null)
            {
                isPressing = false;
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;

                // Reset progress bar and enable joystick input
                progressBar.value = 0f;
                progressBar.gameObject.SetActive(false);

                if (joystickCanvasGroup != null)
                    joystickCanvasGroup.interactable = true;
            }
        }

        private IEnumerator HoldAction()
        {
            while (isPressing)
            {

                holdProgress += Time.deltaTime / holdDuration;
                progressBar.value = holdProgress;

                if (holdProgress >= 1f)
                {
                    OnHoldComplete?.Invoke();
                    Debug.Log("Hold Complete!");
                    //FindInteractableObject();
                    mobileTrigger.setMobileTrigger(true);

                    /*if (currentInteractableObject != null && moveObjectController != null)
                    {
                        moveObjectController.setMobileTrigger(true); // Trigger the mobile action
                    }*/
                    StopHold(); // Stop the hold once complete
                    break;

                }

                yield return null;
                /*OnHold?.Invoke();
                Debug.Log("Hold action triggered.");
                yield return new WaitForSeconds(0.1f);*/ // Adjust repeat rate as needed
                                                         //}
                Debug.Log("4-");
            }
            isPressing = false;
        }

        private void FindInteractableObject()
        {

            mobileTrigger.setMobileTrigger(true);

            /*RaycastHit hit;
            Vector3 rayOrigin = Camera.main.transform.position;
            Vector3 direction = Camera.main.transform.forward;

            // Perform a raycast to find interactable objects
            if (Physics.Raycast(rayOrigin, direction, out hit, 3f))
            {
                if (hit.collider.CompareTag("NormalDoor") || hit.collider.CompareTag("ClosedDoor"))
                {
                    currentInteractableObject = hit.collider.gameObject;
                    moveObjectController = currentInteractableObject.GetComponent<MoveObjectController>();
                    Debug.Log("Interactable object found: " + currentInteractableObject.name);
                }
            }*/

        }

        /*private void ProcessClick(bool pressing = true)
        {
            switch (buttonType)
            {
                case ButtonType.Touch when pressing:
                    StartCoroutine(MakeAFrameClick());
                    break;
                case ButtonType.Hold:
                    isPressing = !isPressing;
                    break;
                case ButtonType.Click when !pressing:
                    StartCoroutine(MakeAFrameClick());
                    break;
            }
        }*/



        private IEnumerator MakeAFrameClick()
        {
            isPressing = true;
            yield return new WaitForFixedUpdate();
            isPressing = false;
        }

        private void OnDisable()
        {
            isPressing = false;

            if (progressBar != null)
                progressBar.gameObject.SetActive(false);

            if (joystickCanvasGroup != null)
                joystickCanvasGroup.interactable = true;
        }
    }
}