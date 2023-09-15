using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BlackHoleWriter : MonoBehaviour
{
    string fileName = "";

    [System.Serializable]
    public class BlackHoleExport
    {
        
        public string timeStamp;
        //public bool isActive;
        public float bhMass;
        public double bhRadius;
        public double gravity;

        //creates black hole object for exporting
        public BlackHoleExport( float bhMass, double bhRadius, double gravity)
        {
            this.timeStamp = System.DateTime.UtcNow.ToLocalTime().ToString("yyyy-MMM-dd") + " @ Time " + System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
            this.bhMass = bhMass;
            this.bhRadius = bhRadius;
            this.gravity = gravity;

        }
    }

    //list of black hole objects
    [System.Serializable]
    public class BlackHoleList
    {
        public List<BlackHoleExport> listBH= new List<BlackHoleExport>();
    }

    // creates list of black hole objects
    public BlackHoleList bhList = new BlackHoleList();


     // public method to write to a new CSV
    public void WriteCSV()
    {
        // get time
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("yyyy_MMM_dd") + " @ " + System.DateTime.UtcNow.ToLocalTime().ToString("hh_mm_ss tt");
        // use file path of the application to create the name of the file
        fileName = Application.dataPath + "/BlackHoleData" + time + ".csv";
        
        // if list of black holes is not empty
        if (bhList.listBH.Count > 0)
        {
            // create a text writer and a new stream writer, overwrites anyfile that was already there, adds the file header details and then closes
            TextWriter tw = new StreamWriter(fileName, false);
            tw.WriteLine("Date/Time of contact, Black Hole Mass (in Solar Masss), Radius(km), Gravitational Force (F = G m1m2 / R^2)");
            tw.Close();

            // creates a new streamwriter that does appends to the file instead of overwrites
            tw = new StreamWriter(fileName, true);

            // for each object in the list of celestial objects, add the statistics of each object, then close the writer
            foreach(BlackHoleExport bh in bhList.listBH)
            {
                tw.WriteLine(bh.timeStamp + "," + bh.bhMass + "," + bh.bhRadius + "," + bh.gravity);
            }
            tw.Close();
        }
    }
   
    // method to add the different celestial body objects to the list, wether they are active and the mass of the black hole
    public void AddCurrentBHDataToBHList(BlackHole blackHole, float bhMass, double bhRadius, double gravity)
    {
        BlackHoleExport newBH = new BlackHoleExport(bhMass, bhRadius, gravity);
        bhList.listBH.Add(newBH);
    }


    // clears list of objects
    public void ClearCurrentCBDataList()
    {
        bhList.listBH.Clear();
    }
    
}