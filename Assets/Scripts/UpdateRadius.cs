using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRadius : MonoBehaviour
{
    public Transform bhTransform;
    public double speedOfLight = 299792458; //m/s
    public double G = 6.6743e-11; //m^3 kg^-1 s^-2
    public double r = 0;

    public Rigidbody rbBlackHole;


    //radius of regualer Stars
    //4/3 pi r ^3 = density * volume = mass
    //Schwarzschild Radius
    //r = (2GM)/(c^2)
    //(2 * gravitational constant * mass)/(speed of light squared)

    private void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
        bhTransform = GetComponent<Transform>();
        
        r = 2 * G * (rbBlackHole.mass * Mathf.Pow(6, 31)) / Mathf.Pow((float)speedOfLight, 2);
        bhTransform.transform.localScale = Vector3.one * (float)r;
    }


    //Schwarzschild Radius
    //r = (2GM)/(c^2)
    //(2 * gravitational constant * mass)/(speed of light squared)
    public void ChangeRadius()
    {
        r = 2 * G * (rbBlackHole.mass * Mathf.Pow(6, 31)) / Mathf.Pow((float)speedOfLight, 2);
        bhTransform.transform.localScale = Vector3.one * (float)r;
        Debug.Log("Hello From update Radious");
    }

}
