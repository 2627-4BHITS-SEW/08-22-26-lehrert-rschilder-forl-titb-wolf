using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCam : MonoBehaviour
{
    public GameObject Cam;
    public float speed = 25f;
    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.shiftKey.isPressed)
        {
            speed = 100f;
        }
        else
        {
            speed = 25f;
        }

        if (Keyboard.current == null) return;

        if (Keyboard.current.wKey.isPressed)
        {
            Cam.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            Cam.transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            Cam.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            Cam.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Keyboard.current.eKey.isPressed)
        {
            Cam.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Keyboard.current.qKey.isPressed)
        {
            Cam.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
