using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddCeleBodies : MonoBehaviour
{
    //if button is pushed add a new celestial body
    //to the scene at a random location with random
    //velocity.

    public CelestialBody largeCeleRBody;    // holds the prefab for large bodies
    public CelestialBody smallCeleRBody;    //holds the prefab for small bodies

    // if create small body is pushed, instantiate
    // a small object in a random location with a random
    // momentum and add components that need to be added. 
    public void CreateSmallCBody()
    {
        CelestialBody smallCBInstance;
        float rndXPos = Random.Range(-50f, 50f);
        float rndZPos = Random.Range(-25f, 25f);
        smallCBInstance = Instantiate(smallCeleRBody) as CelestialBody;
        smallCBInstance.name += GameObject.FindObjectOfType<SimManager>().countOfCBs;
        GameObject.FindObjectOfType<SimManager>().countOfCBs++;
        smallCBInstance.rBody.position = new Vector3(rndXPos, 0, rndZPos);
        smallCBInstance.mCeleBody = new Vector3(Random.Range(-30f,30f), 0, Random.Range(-30f,30f));

        smallCBInstance.simManager = FindObjectOfType<SimManager>();
        smallCBInstance.UpdateCBArrays();
        AddCBToSimManagerLiveData(smallCBInstance);
    }


    // if create large body is pushed, instantiate
    // a large object in a random location with a random
    // momentum and add components that need to be added. 
    public void CreateLargeCBody()
    {
        CelestialBody largeCBInstance;
        float rndXPos = Random.Range(-50f, 50f);
        float rndZPos = Random.Range(-25f, 25f);
        largeCBInstance = Instantiate(largeCeleRBody) as CelestialBody;
        largeCBInstance.name += GameObject.FindObjectOfType<SimManager>().countOfCBs;
        GameObject.FindObjectOfType<SimManager>().countOfCBs++;
        largeCBInstance.rBody.position = new Vector3(rndXPos, 0, rndZPos);
        largeCBInstance.mCeleBody = new Vector3(Random.Range(-200f, 200f), 0, Random.Range(-200f, 200f));

        largeCBInstance.simManager = FindObjectOfType<SimManager>();
        largeCBInstance.UpdateCBArrays();
        AddCBToSimManagerLiveData(largeCBInstance);
    }

    public void AddCBToSimManagerLiveData(CelestialBody cb)
    {
        GameObject.FindObjectOfType<SimManager>().cbNameArr.Add(cb.name);
        GameObject.FindObjectOfType<SimManager>().pStringArr.Add(cb.mCeleBody);
        GameObject.FindObjectOfType<SimManager>().isActiveStateArr.Add(cb.isActiveAndEnabled);
        GameObject.FindObjectOfType<SimManager>().vStringArr.Add(cb.Velocity);
        GameObject.FindObjectOfType<SimManager>().distToBHStringArr.Add(cb.distToBH);

        GameObject.FindObjectOfType<CSVWriter>().AddCurrentCBDataToCBList(cb, true, GameObject.FindObjectOfType<BlackHole>().rbody.mass);
    }
}
