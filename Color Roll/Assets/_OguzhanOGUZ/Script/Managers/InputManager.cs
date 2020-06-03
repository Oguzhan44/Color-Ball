using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputManager() { }

    private bool isPressing = false;
    public float joyStickRadius = 0;
    public Vector2 joyStick = new Vector2(0, 0);
    [SerializeField] private bool lockX = false;
    [SerializeField] private bool lockY = false;
    private Vector2 firstTouchPoint = new Vector2(0, 0);
    private Vector2 currentTouchPoint = new Vector2(0, 0);

    private void Start()
    {
        joyStickRadius = (float)Screen.height / 8F;
    }

    private void Update()
    {
        JoyStick();
    }

    public Vector2 GetJoyStick()
    {
        return joyStick;
    }
    public Vector2 GetJoyStickFirstTouchPoint()
    {
        return firstTouchPoint;
    }
    public float GetJoyStickRadius()
    {
        return joyStickRadius;
    }
    /// <summary>
    /// A simple invisible joystick method.
    /// </summary>
    private void JoyStick()
    {
        float minimumThreshold = 0.1F;
        if(Input.GetMouseButton(0))
        {
            if(!isPressing)
            {
                firstTouchPoint = Input.mousePosition;
            }
            currentTouchPoint = Input.mousePosition;
            isPressing = true;
            Vector2 dragDifference = currentTouchPoint - firstTouchPoint;
            joyStick.x = Mathf.Clamp(dragDifference.x / joyStickRadius, -1, 1);
            joyStick.y = Mathf.Clamp(dragDifference.y / joyStickRadius, -1, 1);
            joyStick.x = (Mathf.Abs(joyStick.x) > minimumThreshold) ? joyStick.x : 0;
            joyStick.y = (Mathf.Abs(joyStick.y) > minimumThreshold) ? joyStick.y : 0;

            if(lockX) { joyStick.x = 0; }
            if(lockY) { joyStick.y = 0; }
        }
        else
        {
            joyStick = Vector2.zero;
            isPressing = false;
        }
    }
}
