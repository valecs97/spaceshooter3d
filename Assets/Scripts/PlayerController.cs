using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Runtime.CompilerServices;



public class PlayerController : MonoBehaviour
{

    private GameObject mobileJoystickObject;
    private Joystick mobileJoystick;
    private Character character;
    private bool canControl = true;

    private bool hasShoted=false;

    void Start()
    {
        mobileJoystickObject = GameObject.FindGameObjectWithTag("MobileJoystick");
        if (mobileJoystickObject != null)
            mobileJoystick = mobileJoystickObject.GetComponent<Joystick>();
        if (mobileJoystick == null)
            Debug.LogError("Cannot find 'GameController' script");
        character = GetComponent<Character>();
        if (character == null)
            Debug.LogError("Character script was not found !");
    }

    void AndroidInput()
    {
        for (int i = 0; i <= 1; i++)
        {
            if (Input.touchCount > i)
            {
                Touch touch = Input.touches[i];
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2)
                        mobileJoystick.changePosition(new Vector3(touch.position.x, touch.position.y, 0.0f));
                    else
                        hasShoted = true;
                }
                else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x > Screen.width / 2)
                        hasShoted = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (canControl)
        {
            if (Application.platform == RuntimePlatform.Android)
                AndroidInput();
            else if (Input.GetKeyDown("space") || Input.GetKey("space"))
                hasShoted = true;
            Vector3 movement = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0.0f);
            character.Move(movement, hasShoted);
            hasShoted = false;
        }
    }

    public void BlockControlls()
    {
        canControl = false;
    }

    public void UnblockControlls()
    {
        canControl = true;
    }
}
