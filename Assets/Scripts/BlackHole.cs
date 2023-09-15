using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    const float GravitationalConstant = 6.7f; // G = 6.7 * 10^_-11 N*(m^2/kg^2) 
    const float speedOfLight = 300000000;

    public Rigidbody rbody;
    public SimManager simManager;

    public UpdateRadius updateRad;

    private void FixedUpdate()
    {
        
    }

/*
        //update BH details on screen
    public void BlackHoleDisplayUpdate(BlackHole bh)
    {

        // Update SimManager to display updated BH Mass
        simManager.bhmass = bh.rbody.mass;
        simManager.bhMassText.text = "Black Hole Mass: " +  simManager.bhmass + " Solar Masses";
                
        //update BH volume
        //simManager.bhVolume = (2 * (6.7f) * bh.rbody.mass);
        simManager.bhVolume = (2 * 6.7f * bh.rbody.mass * 2 * Mathf.Pow(10,30))/(Mathf.Pow(300000000,2));
        simManager.bhVolumeText.text =  ("Black Hole Radius: " + (simManager.bhVolume) + " km");

        //calculate gravitational maximum power F= (G*Mass of black hole* Mass of celestial body) / Radius of BH ^ 2
        double gravitationalMax = (updateRad.G * bh.rbody.mass ) / (updateRad.r * updateRad.r);
        
       // 
        GameObject.FindObjectOfType<BlackHoleWriter>().AddCurrentBHDataToBHList(bh, bh.rbody.mass, updateRad.r, gravitationalMax);
      
        Debug.Log("Black hole name: " + bh.name);
        Debug.Log("Black hole mass: " + bh.rbody.mass);
        Debug.Log("Black hole rad: " + simManager.bhVolume);
        Debug.Log("Black hole grav: " + gravitationalMax);
        
    }
    */
}
