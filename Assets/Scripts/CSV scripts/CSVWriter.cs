using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{

    string fileName = "";



    [System.Serializable]
    public class CBody
    {
        public string name;
        public float momentum;
        public float velocity;
        public float mass;
        public float distToBH;
        public string timeStamp;
        public bool isActive;
        public float bhMass;

        public CBody(string name, Vector3 momentumV3, Vector3 velocityV3, float mass, float distToBH, bool isActive, float bhMass)
        {
            this.name = name;
            this.momentum = momentumV3.magnitude;
            this.velocity = velocityV3.magnitude;
            this.mass = mass;
            this.distToBH = distToBH;
            this.timeStamp = System.DateTime.UtcNow.ToLocalTime().ToString("yyyy-MMM-dd") + " @ Time " + System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
            this.isActive = isActive;
            this.bhMass = bhMass;
        }
    }

    [System.Serializable]
    public class CBodyList
    {
        public List<CBody> listCBody= new List<CBody>();
    }

    public CBodyList cBodyList = new CBodyList();


    public void WriteCSV()
    {
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("yyyy_MMM_dd") + " @ " + System.DateTime.UtcNow.ToLocalTime().ToString("hh_mm_ss tt");
        fileName = Application.dataPath + "/SimData" + time + ".csv";
        if (cBodyList.listCBody.Count > 0)
        {
            TextWriter tw = new StreamWriter(fileName, false);
            tw.WriteLine("Name, Momentum, Velocity, Mass, Dist->BH, Active, Black Hole Mass, Date/Time");
            tw.Close();

            tw = new StreamWriter(fileName, true);

            foreach(CBody cb in cBodyList.listCBody)
            {
                tw.WriteLine(cb.name + "," + cb.momentum + "," + cb.velocity + "," + cb.mass + "," + cb.distToBH + "," + cb.isActive + "," + cb.bhMass + "," + cb.timeStamp);
            }
            tw.Close();
        }
    }

    public void AddCurrentCBDataToCBList(CelestialBody cbody, bool activeStatus, float bhMass)
    {
        CBody newCB = new CBody(cbody.name, cbody.mCeleBody, cbody.Velocity, cbody.rBody.mass, cbody.distToBH, activeStatus, bhMass);
        cBodyList.listCBody.Add(newCB);
    }

    public void ClearCurrentCBDataList()
    {
        cBodyList.listCBody.Clear();
    }
}
