using UnityEngine;
using UnityEngine.Experimental.AI;

public class flyingCamera : MonoBehaviour
{
    public float speed = 5;
    public Transform rotationModel;

    float rotX, rotY;

    void LateUpdate()
    {
        moveCamera();
        rotateCamera();
    }

    private void moveCamera()
    {
        float x = Input.GetAxis("Vertical") * speed;
        float y = Input.GetAxis("Horizontal") * speed;

        Vector3 move = new Vector3(y, 0, x);
       // move = rotationModel.rotation * move;
       transform.Translate(move * Time.deltaTime);

        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void rotateCamera()
	{
        float x = Input.GetAxis("Mouse X") * speed;
        float y = Input.GetAxis("Mouse Y") * speed;

        rotX += x;
        rotY -= y;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(rotY, rotX, 0)), .1f);
	}
}
