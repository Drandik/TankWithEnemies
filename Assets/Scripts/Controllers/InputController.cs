using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Singleton<InputController>
{
    public float HorizontalAxe { get; private set; }
    public float VerticalAxe { get; private set; }
    public bool KeyW { get; private set; }
    public bool KeyQ { get; private set; }
    public bool KeyX { get; private set; }
    public bool Cancel { get; private set; }

    private void Update()
    {
        HorizontalAxe = Input.GetAxis("Horizontal");
        VerticalAxe = Input.GetAxis("Vertical");
        KeyW = Input.GetButtonDown("KeyW");
        KeyQ = Input.GetButtonDown("KeyQ");
        KeyX = Input.GetButtonDown("KeyX");
        Cancel = Input.GetButtonDown("Cancel");
    }
}
