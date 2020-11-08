
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    private bool LeftorRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftorRight == true)
        {
            Vector3 moveLeft = new Vector3(0, 0, 5);
            transform.Translate(moveLeft * Time.deltaTime);
            Debug.Log("Move Right.");
        }
        else
        {
            Vector3 moveRight = new Vector3(0, 0, -5);
            transform.Translate(moveRight * Time.deltaTime);
            Debug.Log("Move Left.");
        }
    }
    void OnCollisionEnter()
    {
        if (LeftorRight == true)
        {
            LeftorRight = false;
        }
        else
        {
            LeftorRight = true;
        }
    }
}
