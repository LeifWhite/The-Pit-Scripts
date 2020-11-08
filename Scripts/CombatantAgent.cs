using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;
//This class interfaces with the neural network and creates a feed for input and output variables from it
public class CombatantAgent : Agent
{
    public Target target;
    public PlayerMovement pm;
    public MouseLook ml;
    public PauseMenu pauseMenu;
    public float SensorRange = 200;
    public Camera cam;
    public sensorController sc;
    public Gun g;
    public bool player2;
    private float episodeStartTime;

    private Vector3 farV = new Vector3(-100, -100, -100);
    private float h;
    private float max_h;
    private Target t;
    private Vector3 circlePoint;
    private int stagnateCount = 0;
    private const int CIRCLE_RADIUS = 4;
    private const int LEVEL_TIME = 60;
    private int actionCount = 1;
    bool running = true;

    public GameObject anim;

    //This begins at the start of each repetion of training
    public override void OnEpisodeBegin()
    {

        g.cooldown = Time.time+1;
        target.health = 5;
        actionCount = 1;
        episodeStartTime = Time.time;

        t = GetComponent<Target>();
        t.health = 5;
        h = t.health;
        max_h = t.maxHealth;
        circlePoint = this.transform.localPosition;
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, UnityEngine.Random.value*360, transform.eulerAngles.z);

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        if (running)
        {
            // Target and Agent positions

            if(anim != null)
            anim.GetComponent<Animator>().SetBool("walking", true);

            if(target != null)
            sensor.AddObservation(target.gameObject.transform.localPosition);
            sensor.AddObservation(this.transform.localPosition);
            //left, left front, front, right front, and right sensors to detct obstacles
            float[] sensors = sc.Distances;

            //adds sensors
            sensor.AddObservation(sensors);
            //what are you aiming at
            RaycastHit hit;
            Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000);

            sensor.AddObservation(hit.point);
        }
        else
        {

        }


    }
    //how much you push objects back
    public float forceMultiplier = 10;
    //What you do when receiving actions from the NN?
    public override void OnActionReceived(float[] vectorAction)
    {
        //counts episodic actions
        actionCount++;
        //Inputs actions into player movement, update mouse, and gun
        pm.GetActionMove(vectorAction);
        ml.UpdateMouse(vectorAction);
        float temp = target.health;
        g.TryShoot(vectorAction);
        //It is good to hurt the bad guy
        if (Math.Abs(temp-target.health)>0.1)
        {
            AddReward(0.7f);
        }
        //It is bad to get hurt by the bad guy
        h = t.health;
        if (Math.Abs(max_h-h)>0.1)
        {
            AddReward(-0.75f);
            max_h = h;
        }
        //Ensures constant movement
        if (actionCount % 400 == 0) 
        {

            if(Vector3.Distance(this.transform.localPosition, circlePoint) < CIRCLE_RADIUS)
            {
                stagnateCount++;
                AddReward(-0.05f * (float)Math.Pow(1.5, stagnateCount));
            }
            else
            {
                stagnateCount = 0;
                circlePoint = this.transform.localPosition;
            }

        }
        //The closer you are to facing the bad guy, the better
        if (Vector3.Distance(sc.lineOfSight,farV)>1f)
        {
            float theta = Vector3.Angle(cam.transform.forward, sc.lineOfSight);

            AddReward(0.004f * (Math.Min(1f / theta, 1f)));
        }
        //If target dead
        if (target.health <=0)
        {
            //gameObject.SetActive(false);

            running = false;
            if (pauseMenu != null)
            {
               pauseMenu.LoadMenu();
            }
            //Update reward
            AddReward((float)Math.Max(2.0-(Time.time-episodeStartTime)/LEVEL_TIME, 1));


            EndEpisode();
        }

        //If time in episode is up
        /*else if (Time.time > episodeStartTime+LEVEL_TIME)
        {
            transform.localPosition = new Vector3(16f + (UnityEngine.Random.value - 0.5f) * 20f, 5f, 27f + (UnityEngine.Random.value - 0.5f) * 40f);

           
           
            EndEpisode();
        }*/


    }
    //How humans play
    public override void Heuristic(float[] actionsOut)
    {
        ml.lockY = false;
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
       //No real reason for it to use Mouse Y
        actionsOut[2] = Input.GetAxis("Mouse X");
        Debug.Log(actionsOut[2]);
        if (Input.GetButton("Fire1"))
        {
          
            actionsOut[3] = 1;
        }
        else
            actionsOut[3] = 0;


    }
}
