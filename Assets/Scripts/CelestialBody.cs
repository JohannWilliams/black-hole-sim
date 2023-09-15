using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Rigidbody rBody;     // objects rigidbody used for updating and manipulating objects pos, rotation, and etc...
    public SimManager simManager;   // retrieves current simulation Manager for tracking current game states. 

    private Vector3 netForce = Vector3.zero; // net force used for updating objects momentum
    public Vector3 mCeleBody = Vector3.zero; // momentum used for updating objects velocity
    private Vector3 vCeleBody = Vector3.zero; // velocity used for updating objects position.

    private CelestialBody[] cBodies;    //array to hold all Moving Bodies
    private BlackHole[] blackHoles;     //array to hold all Black holes (moving or not)

    public Vector3 Velocity => vCeleBody; // private variable for retrieving current velocity

    private float G = -1.25f;

    public UpdateRadius updateRad;

 //   public BlackHole blackHole;

    public float distToBH = 0;

    private float kEnergy;
    private float uEnergy;

    //Start ran only when object is first created.
    public void Start()
    {
        //used for updating the color of our celestial bodies.
        /*if (this.name.Contains("Orb"))
        {
            this.GetComponent<Renderer>().material.color = Color.blue;
        } else if (this.name.Contains("Giant"))
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }*/
        this.UpdateCBArrays();
    }

    //add all objs to their respective arrays.
    public void UpdateCBArrays()
    {
        cBodies = FindObjectsOfType<CelestialBody>();
        blackHoles = FindObjectsOfType<BlackHole>();
        updateRad = blackHoles[0].GetComponent<UpdateRadius>();
    }

    private void FixedUpdate()
    {
        if(simManager.GameState1 == SimManager.GameState.SimRunning)
        {
            //if a Celestial Body has been deleted or added to the scene
            //update the cb's array so it reflects only the current cbs
            //in the scene.
            if(cBodies.Length != FindObjectsOfType<CelestialBody>().Length)
            {
                cBodies = FindObjectsOfType<CelestialBody>();
            }

            /********** Force of Gravity ***************
            *   Fg = -G*((m1*m2)/mag(r)^2)*unitVec(r)
            * ************ VARIABLES *******************
            * G is the gravitational constant 
            *      G = 6.6743*10^-11
            * 
            * m1 is the mass of object 1
            * m2 is the mass of object 2
            * r is the distance between the two objects
            *      vec3 pos of object2 - vec3 pos of object1
            * ************** END **********************/

            netForce = Vector3.zero; //init Fnet to 0 for current iteration calculations


            //calcuate gravitation force by other celestial bodies
            //and add them to the net Force (total forces [Vec 3])
            foreach (CelestialBody celestialBody in cBodies)
            {
                if(celestialBody == null)
                {
                    Debug.Log("CB is null!!!");
                }
                if (celestialBody != this && celestialBody != null && celestialBody.isActiveAndEnabled)
                {
                    Vector3 dir = rBody.position - celestialBody.rBody.position;
                    float dist = dir.magnitude;
                    netForce += G * ((this.rBody.mass * celestialBody.rBody.mass) / Mathf.Pow(dist, 2)) * dir.normalized;
                }
            }

            //calcuate gravitational force by black holes on body,
            //and add them to the net force (total forces [Vec3])
            foreach (BlackHole bh in blackHoles)
            {
                if (bh != this && bh != null)
                {
                    Vector3 dir = rBody.position - bh.rbody.position;
                    distToBH = dir.magnitude;
                    netForce += G * ((this.rBody.mass * bh.rbody.mass) / Mathf.Pow(distToBH, 2)) * dir.normalized;

                    uEnergy = (G * bh.rbody.mass * this.rBody.mass) / distToBH;
                }
            }

            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        /********* STEPS TO UPDATE POSITION ***************
         * *************** NET FORCE *********************
         *          (CALCULATED IN FIXED UPDATE) 
         * To update the position of the Celestial Body,
         * we first need to update the Net Force acting on it
         * by calcuating all forces acting on the body and adding
         * them up. 
         * 
         * *************** MOMENTUM **********************
         * After calc the net force, update the Objects momentum
         * by talking init moment and adding (net Force * delta time)
         * to it.
         * 
         * *************** VELOCITY **********************
         * Next we need to update the velocity of the object
         * by dividing the momentum of the object by its mass
         * (v = p/m) 
         * [ v is in meters/sec]
         * [ p = momentum is in kg*meters/sec]
         * [ m is mass in kg]
         * 
         * *************** POSITION **********************
         * Last we need to update the position of the object 
         * by taking the init/last pos and adding (v * delta time) 
         * to it. 
         *
         ******************** END ************************/

        mCeleBody += netForce * Time.deltaTime; //update momentum

        vCeleBody = mCeleBody/rBody.mass;       //update velocity
        kEnergy = 0.5f * rBody.mass * Mathf.Pow(vCeleBody.magnitude, 2);

        if(kEnergy + uEnergy > 0 && distToBH >75)
        {
            Debug.Log(this.name + "has a K + U of : " + (kEnergy + uEnergy) + 
                "\nDistroy me please!\nMy Velocity is " + vCeleBody.magnitude + 
                "\nMy Momentum is " + mCeleBody.magnitude);
            //Destroy(gameObject);

            SetInactive();
        }

        rBody.position += vCeleBody * Time.deltaTime;   //update position.
    }

    private void SetInactive()
    {
        int index = GameObject.FindObjectOfType<SimManager>().cbNameArr.IndexOf(this.name);
        GameObject.FindObjectOfType<CSVWriter>().AddCurrentCBDataToCBList(this, false, blackHoles[0].rbody.mass);
        GameObject.FindObjectOfType<SimManager>().isActiveStateArr[index] = false;
        this.gameObject.SetActive(false);
    }
    //When collision occures, take the approiate action. 
    //With BH, distroy current obj and merge its mass with
    //the BH, other Objs have their momentums updated.
    private void OnCollisionEnter(Collision collision)
    {
        foreach (BlackHole bh in blackHoles)
        {
            if(collision.gameObject.Equals(bh.gameObject))
            {
                //remove object and add mass to bh.
                Debug.Log(this.name + "Collided with the Black Hole!!!");
                bh.rbody.mass += rBody.mass;
                updateRad.ChangeRadius();
                //bh.transform.localScale = Vector3.one * 5;
                this.rBody.position = new Vector3(100000,0,0);
                SetInactive();
                //Destroy(gameObject);
                
                //update display and csv
                BlackHoleDisplayUpdate(bh);

                return;
            }
        }

        foreach (CelestialBody cb in cBodies)
        {
            if (cb != null && collision.gameObject.Equals(cb.gameObject))
            {
                //reduce or increase mass of this object???
                //and the object it collided with.????
                //update momentum of both after collision.
                Debug.Log(this.name + "Collided with the another Body!!!");
                return;
            }
        }
    }
    // moved to BlackHole

    //update BH details on screen
    public void BlackHoleDisplayUpdate(BlackHole bh)
    {

        // Update SimManager to display updated BH Mass
        simManager.bhmass = bh.rbody.mass;
        simManager.bhMassText.text = "Black Hole Mass: " +  simManager.bhmass + " Solar Masses";
                
        //update BH volume
        //simManager.bhVolume = (2 * (6.7f) * bh.rbody.mass);
        simManager.bhVolume = (float)((2 * updateRad.G * (bh.rbody.mass * 2e30))/(Mathf.Pow(300000000,2)));
        simManager.bhVolumeText.text =  ("Black Hole Radius: " + (simManager.bhVolume) + " km");

        //calculate gravitational maximum power F= (G*Mass of black hole* Mass of celestial body) / Radius of BH ^ 2
        double gravitationalMax = (updateRad.G * bh.rbody.mass * this.rBody.mass ) / (Mathf.Pow(simManager.bhVolume, 2));
        
       // 
        GameObject.FindObjectOfType<BlackHoleWriter>().AddCurrentBHDataToBHList(bh, bh.rbody.mass, simManager.bhVolume, gravitationalMax);
      
        Debug.Log("Black hole name: " + bh.name);
        Debug.Log("Black hole mass: " + bh.rbody.mass);
        Debug.Log("Black hole rad: " + updateRad.r);
        Debug.Log("Black hole grav: " + updateRad.G);
        
    }

}
