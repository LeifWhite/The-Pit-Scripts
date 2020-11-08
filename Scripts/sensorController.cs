using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
//Determines how the Combatant Agent uses sensors
public class sensorController : MonoBehaviour
{
    [SerializeField] public Transform[] sensors;

    [SerializeField] public float[] Distances;

    public GameObject player;
    public Transform playerLineOfSightSensor;
    public Vector3 lineOfSight;

     // Update is called once per frame
    void Update()
    {
        getSensors();
        findLocations();
        lineOfSight = checkLineOfSight();
    }
    //gets all sensors
    private void getSensors()
	{
        sensors = new Transform[transform.childCount];

        for(int i= 0; i < transform.childCount; i++)
		{
            sensors[i] = transform.GetChild(i);
		}
	}
    //Draws the nice yellow lines
	public void OnDrawGizmos()
	{
        sensors = new Transform[transform.childCount];
        getSensors();

        Gizmos.color = Color.yellow;
        for (int i = 0; i < sensors.Length; i++)
        {
            Gizmos.DrawWireCube(sensors[i].transform.position, new Vector3(.1f, .1f, .1f));
            Gizmos.DrawLine(sensors[i].transform.position, sensors[i].transform.position + sensors[i].transform.forward * 30);
        }
    }
    //Finds locations to sense
	private void findLocations()
	{
        Distances = new float[sensors.Length];
        for (int i = 0; i < sensors.Length; i++)
		{
             float dist = getDistanceFromRaycast(sensors[i]);
            Distances[i] = dist;
		}
	}
    //Gets distance from location
    private float getDistanceFromRaycast(Transform obj)
	{
        Ray ray = new Ray(obj.transform.position, obj.transform.forward);
        Debug.DrawRay(obj.transform.position, obj.transform.forward, Color.green);

        RaycastHit rayHit;

        int cubeLayer = LayerMask.NameToLayer("Goal");
        int layer = (1 << cubeLayer);
        layer = ~layer;

        if(Physics.Raycast(ray, out rayHit, 1000, layer))
		{
            return Vector3.Distance(obj.transform.position, rayHit.point);
		}

        return 1000;
	}
    //Makes sure values aren't out of range
    private bool checkInFulcrum(Vector3 input, float end)
	{
        float cosAngle = Vector3.Dot((input - this.transform.position).normalized, this.transform.forward);

        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return angle < end;
	}
    //What is the line of sight of the sensor person
    private Vector3 checkLineOfSight()
	{
        Vector3 dist = player.transform.position - transform.position;
        playerLineOfSightSensor.rotation = Quaternion.LookRotation(dist);

        Ray ray = new Ray(playerLineOfSightSensor.transform.position, playerLineOfSightSensor.transform.forward);
        
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
           

            PlayerMovement movement = hit.collider.gameObject.GetComponent<PlayerMovement>();

            if (movement != null)
			{

               if(checkInFulcrum(hit.collider.transform.position, 45))
                return ray.direction;
			}
		}

        return new Vector3(-100,-100,-100);
	}
}
