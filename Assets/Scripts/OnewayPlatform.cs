using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnewayPlatform : MonoBehaviour
{
    [SerializeField] private float waitTimeValue = 0.5f;

    private float waitTime;
    private PlatformEffector2D effector;

    // Start is called before the first frame update
    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        waitTime = waitTimeValue;
    }

    // Update is called once per frame
    private void Update()
    {
        if (KeySink())
        {
            if (waitTime <= 0f)
            {
                effector.rotationalOffset = 180f;
                waitTime = waitTimeValue;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (KeySinkUp())
        {
            effector.rotationalOffset = 0f;
            waitTime = waitTimeValue;
        }
    }

    private bool KeySink()
    {
        return Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
    }

    private bool KeySinkUp()
    {
        return Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S);
    }
}
