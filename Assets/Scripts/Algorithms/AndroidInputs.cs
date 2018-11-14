using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AndroidInputs : MonoBehaviour {


    public static bool getAnyTouchBeginInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i <= 1; i++)
            {
                if (Input.touchCount > i)
                {
                    Touch touch = Input.touches[i];
                    if (touch.phase == TouchPhase.Began)
                        return true;
                }
            }
        }
        return false;
    }

    public static bool getAnyTouchStationaryInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i <= 1; i++)
            {
                if (Input.touchCount > i)
                {
                    Touch touch = Input.touches[i];
                    if (touch.phase == TouchPhase.Stationary)
                        return true;
                }
            }
        }
        return false;
    }

    public static bool getAnyTouchMovedInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i <= 1; i++)
            {
                if (Input.touchCount > i)
                {
                    Touch touch = Input.touches[i];
                    if (touch.phase == TouchPhase.Moved)
                        return true;
                }
            }
        }
        return false;
    }

    public static bool getAnyTouchEndedInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i <= 1; i++)
            {
                if (Input.touchCount > i)
                {
                    Touch touch = Input.touches[i];
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                        return true;
                }
            }
        }
        return false;
    }

}
