using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCam : MonoBehaviour
{
    public GameObject Cam;
    public float speed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
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
    }
}
