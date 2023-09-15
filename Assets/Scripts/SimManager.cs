using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;
using System.Linq;
using static CSVWriter;
using UnityEngine.UI;
using static BlackHoleWriter;

public class SimManager : MonoBehaviour
{

    //3600 seconds in an hour
    //60 seconds in a minute

    public enum GameState{SimRunning, SimPaused};
    public enum MenuState {MainMenu, PreSetMenu, EditingMenu, MenusClosed};
    

    private MenuState menuState = MenuState.MainMenu;
    private GameState gameState = GameState.SimPaused;

    public GameState GameState1 => gameState;

    public MenuState MenuState1 => menuState;

    public int simLvl;

    private CelestialBody[] cBodies;    //array to hold all Moving Bodies
    private BlackHole[] blackHoles;     //array to hold all Black holes (moving or not)

    public List<Vector3> pStringArr = new List<Vector3>();
    public List<float> distToBHStringArr = new List<float>();
    public List<Vector3> vStringArr = new List<Vector3>();
    public List<string> cbNameArr = new List<string>();
    public List<bool> isActiveStateArr = new List<bool>();

    public float simRunTimer = 0.0f;

    public CelestialBody[] publicCBodies { get { return cBodies; } set { cBodies = value; } }

    private string cBodyDataString;

    // BH Details UI + Mass + Volume
    public TextMeshProUGUI bhMassText;
    public TextMeshProUGUI bhVolumeText;
    public float bhmass;
    public float bhVolume;

    public float SaveRateInSec = 5.0f;
    private double G = 6.6743e-11;

    public int countOfCBs = 9;

    // test exponent notation.
    //private float G = (float)2e30;
    //private float Gg = 2 * Mathf.Pow(10, 30);

    void Start()
    {
        // initalize Black Hole Mass Headsup Display
        bhmass = 500;
        bhVolume = (float)((2 * G * (500 * 2e30)) / (Mathf.Pow(300000000, 2)));
        bhMassText.text = "Black Hole Mass: " +  bhmass + " Solar Masses";
        bhVolumeText.text = "Black Hole Radius: " + bhVolume + " km";
        SetUpSim();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.SimRunning)
        {
            updateTimer();
            if (simRunTimer >= SaveRateInSec)
            {
                foreach(CelestialBody cb in cBodies)
                {
                    GameObject.FindObjectOfType<CSVWriter>().AddCurrentCBDataToCBList(cb, cb.isActiveAndEnabled, blackHoles[0].rbody.mass);
                }
                simRunTimer = 0.0f;
            }
        }
    }

    public void SaveCSV()
    {
        GameObject.FindObjectOfType<CSVWriter>().WriteCSV();

        GameObject.FindObjectOfType<BlackHoleWriter>().WriteCSV();
    }

    private void updateTimer()
    {
        simRunTimer += Time.deltaTime;
    }

    public void play()
    {
        gameState = GameState.SimRunning;
    }

    public void pauseSim()
    {
        gameState = GameState.SimPaused;
    }

    public void MenuExit()
    {
        gameState = GameState.SimPaused;

        GameObject.FindObjectOfType<CSVWriter>().ClearCurrentCBDataList();

        SceneManager.LoadScene(0);

        Debug.Log("Exited Sim now in Home Scene!!!!");

    }

    public string GetCBodyDataString()
    {
        cBodyDataString = "";
        string pString = "";
        string distToBHString = "";
        string vString = "";
        string cbName = "";
        string isActive = "";
        bool activeState;

        if(cBodies != GameObject.FindObjectsOfType<CelestialBody>())
        {
            cBodies = GameObject.FindObjectsOfType<CelestialBody>();
        }

        foreach(CelestialBody cb in cBodies)
        {
            int index = 0;
            if(cbNameArr.Contains(cb.name))
            {
                index = cbNameArr.IndexOf(cb.name);
                pStringArr[index] = cb.mCeleBody;
                vStringArr[index] = cb.Velocity;
                distToBHStringArr[index] = cb.distToBH;
            }
        }

        for(int i = 0; i < cbNameArr.Count; i++)
        {
            
            cbName = cbNameArr[i];
            pString = pStringArr[i].magnitude.ToString("F");
            vString = vStringArr[i].magnitude.ToString("F");
            distToBHString = distToBHStringArr[i].ToString("F");
            activeState = isActiveStateArr[i];
            if (activeState)
            {
                isActive = "ALIVE * IN ORBIT *";
            }
            else if (!activeState && distToBHStringArr[i] >= 70)
            {
                isActive = "ALIVE * ESCAPED PULL *";
            }
            else if (!activeState && distToBHStringArr[i] < 70)
            {
                isActive = "DEAD * FELL INTO BH *";
            }
            cBodyDataString += cbName.ToUpper() + "-" + isActive + "\n--Momentum: " + pString + "\n--Velocity: " + vString + "\n--Dist From BH: " + distToBHString + "\n\n";

        }

        return cBodyDataString;
    }

    public void SetUpSim()
    {
        simLvl = GameObject.FindObjectOfType<LvlManager>().simLvl;
        Debug.Log("Simulation Lvl is set to: " + simLvl);

        publicCBodies = GameObject.FindObjectsOfType<CelestialBody>();
        blackHoles = GameObject.FindObjectsOfType<BlackHole>();
        //BlackHole bh = blackHoles[0];
       

        int bhCount = 0;

        foreach(BlackHole bh in blackHoles)
        {
            bh.rbody.mass = GameObject.FindObjectOfType<LvlManager>().bhMass[bhCount];
            bh.rbody.position = GameObject.FindObjectOfType<LvlManager>().bhPositions[bhCount];
            
            

            bhCount++;
        }

      
        int gcbCount = 0;
        int orbCount = 0;

        foreach(CelestialBody cb in cBodies)
        {
            if(cb != null)
            {
                if(cb.name.Contains("Orb"))
                {
                    cb.gameObject.SetActive( GameObject.FindObjectOfType<LvlManager>().orbActiveStatus[orbCount]);
                    cb.rBody.position = GameObject.FindObjectOfType<LvlManager>().orbPositions[orbCount];
                    cb.mCeleBody = GameObject.FindObjectOfType<LvlManager>().orbMomentum[orbCount];
                    orbCount++;
                }
                else if( cb.name.Contains("Giant"))
                {
                    cb.gameObject.SetActive(GameObject.FindObjectOfType<LvlManager>().gcbActiveStatus[gcbCount]);
                    cb.rBody.position = GameObject.FindObjectOfType<LvlManager>().gcbPositions[gcbCount];
                    cb.mCeleBody = GameObject.FindObjectOfType<LvlManager>().gcbMomentum[gcbCount];
                    gcbCount++;
                }
                else
                {
                    Debug.Log("!!!!!! Something Wrong with Setting up CBS!!!!!");
                }
            }
        }

        foreach(CelestialBody cb in cBodies)
        {
            pStringArr.Add(cb.mCeleBody);
            distToBHStringArr.Add(cb.distToBH);
            vStringArr.Add(cb.Velocity);
            cbNameArr.Add(cb.name);
            isActiveStateArr.Add(cb.isActiveAndEnabled);
            GameObject.FindObjectOfType<CSVWriter>().AddCurrentCBDataToCBList(cb, cb.isActiveAndEnabled, blackHoles[0].rbody.mass);
        }
    }
 
}
