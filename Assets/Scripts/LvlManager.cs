using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LvlManager : MonoBehaviour
{
    public int simLvl = 1;
    // Start is called before the first frame update

    public List<Vector3> gcbPositions = new List<Vector3>();
    public List<Vector3> gcbMomentum = new List<Vector3>();
    public List<bool> gcbActiveStatus = new List<bool>();

    public List<Vector3> orbPositions = new List<Vector3>();
    public List<Vector3> orbMomentum = new List<Vector3>();
    public List<bool> orbActiveStatus = new List<bool>();

    public List<Vector3> bhPositions = new List<Vector3>();
    public List<float> bhMass = new List<float>();

    public TextMeshProUGUI LvlText;

    private int gcbCount;
    private int orbCount;

    public static LvlManager instance { get; private set; }

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("instance of Level Manager Found");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Home Screen")
        {
            if(LvlText == null)
            {
                LvlText = GameObject.Find("LevelLabel").GetComponent<TextMeshProUGUI>();
            }
            if(LvlText.text != "Preset " + simLvl)
            {
                Debug.Log("Updating Lvl Text");
                LvlText.text = "Preset " + simLvl;
            }
        }
    }

    public void SelectLevel(int lvl)
    {
        switch (lvl)
        {
            case 1:
                simLvl = 1;
                break;
            case 2:
                simLvl = 2;
                break;
            case 3:
                simLvl = 3;
                break;
            case 4:
                simLvl = 4;
                break;
            default:
                simLvl = 1;
                break;
        }
        setData();
           
    }


    public void setData()
    {
        InitializedCelestialBodyData(this.simLvl);
    }

    public void InitializedCelestialBodyData(int lvl)
    {
        gcbPositions.Clear();
        gcbMomentum.Clear();
        gcbActiveStatus.Clear();

        orbPositions.Clear();
        orbMomentum.Clear();
        orbActiveStatus.Clear();

        bhPositions.Clear();
        bhMass.Clear();

        gcbCount = 5;
        orbCount = 4;

        switch (lvl)
        {
            case 1:

                bhPositions.Add(Vector3.zero);
                bhMass.Add(500f);

                gcbPositions.Add(new Vector3(-25, 0, -17));
                gcbMomentum.Add(new Vector3(100, 0, -100));

                gcbPositions.Add(new Vector3(68, 0, -20));
                gcbMomentum.Add(new Vector3(-75, 0, -75));

                gcbPositions.Add(new Vector3(-1, 0, 20));
                gcbMomentum.Add(new Vector3(125, 0, -40));

                gcbPositions.Add(new Vector3(29, 0, 16));
                gcbMomentum.Add(new Vector3(-220, 0, 200));

                gcbPositions.Add(new Vector3(-7, 0, -20));
                gcbMomentum.Add(new Vector3(300, 0, 150));

                for (int i = 0; i < gcbCount; i++)
                {
                    gcbActiveStatus.Add(true);
                }


                orbPositions.Add(new Vector3(-41,0,-2));
                orbMomentum.Add(new Vector3(5,0,9));

                orbPositions.Add(new Vector3(6, 0, 11));
                orbMomentum.Add(new Vector3(12, 0, -12));

                orbPositions.Add(new Vector3(-14, 0, -8));
                orbMomentum.Add(new Vector3(10, 0, -8));

                orbPositions.Add(new Vector3(-21, 0, 21));
                orbMomentum.Add(new Vector3(9, 0, 12));

                for (int i = 0; i < orbCount; i++)
                {
                    orbActiveStatus.Add(true);
                }

                break;
            case 2:

                bhPositions.Add(Vector3.zero);
                bhMass.Add(500f);

                gcbPositions.Add(new Vector3(-25, 0, -17));
                gcbMomentum.Add(Vector3.zero);

                gcbPositions.Add(new Vector3(68, 0, -20)); 
                gcbMomentum.Add(Vector3.zero);

                gcbPositions.Add(new Vector3(-1, 0, 20));
                gcbMomentum.Add(Vector3.zero);

                gcbPositions.Add(new Vector3(29, 0, 16));
                gcbMomentum.Add(Vector3.zero); ;

                gcbPositions.Add(new Vector3(-7, 0, -20));
                gcbMomentum.Add(Vector3.zero);

                for (int i = 0; i < gcbCount; i++)
                {
                    gcbActiveStatus.Add(true);
                }


                orbPositions.Add(new Vector3(-41, 0, -2));
                orbMomentum.Add(Vector3.zero);

                orbPositions.Add(new Vector3(6, 0, 11)); 
                orbMomentum.Add(Vector3.zero);

                orbPositions.Add(new Vector3(-14, 0, -8));
                orbMomentum.Add(Vector3.zero);

                orbPositions.Add(new Vector3(-21, 0, 21));
                orbMomentum.Add(Vector3.zero);

                for (int i = 0; i < orbCount; i++)
                {
                    orbActiveStatus.Add(true);
                }
                break;
            case 3:
                bhPositions.Add(Vector3.zero);
                bhMass.Add(500f);

                gcbPositions.Add(new Vector3(-40, 0, -40));
                gcbMomentum.Add(new Vector3(200, 0, 400));

                gcbPositions.Add(new Vector3(-20, 0, -20));
                gcbMomentum.Add(new Vector3(-75, 0, 45));

                gcbPositions.Add(new Vector3(-10, 0, 20));
                gcbMomentum.Add(new Vector3(200, 0, -50));

                gcbPositions.Add(new Vector3(10, 0, -10));
                gcbMomentum.Add(new Vector3(-10, 0, 200));

                gcbPositions.Add(new Vector3(-7, 0, -20));
                gcbMomentum.Add(new Vector3(300, 0, 150));

                for (int i = 0; i < gcbCount; i++)
                {
                    gcbActiveStatus.Add(true);
                }


                orbPositions.Add(new Vector3(-10, 0, 5));
                orbMomentum.Add(new Vector3(25, 0, 30));

                orbPositions.Add(new Vector3(2, 0, 2));
                orbMomentum.Add(new Vector3(50, 0, -50));

                orbPositions.Add(new Vector3(-5, 0, -40));
                orbMomentum.Add(new Vector3(25, 0, 45));

                orbPositions.Add(new Vector3(40, 0, 0));
                orbMomentum.Add(new Vector3(-25, 0, 10));

                for (int i = 0; i < orbCount; i++)
                {
                    orbActiveStatus.Add(true);
                }
                break;
            case 4:
                bhPositions.Add(Vector3.zero);
                bhMass.Add(500f);

                gcbPositions.Add(new Vector3(20, 0, 20));
                gcbMomentum.Add(new Vector3(100, 0, -100));

                gcbPositions.Add(new Vector3(20, 0, -20));
                gcbMomentum.Add(new Vector3(-75, 0, -75));

                gcbPositions.Add(new Vector3(-28, 0, 28));
                gcbMomentum.Add(new Vector3(125, 0, 125));

                gcbPositions.Add(new Vector3(30, 0, 30));
                gcbMomentum.Add(new Vector3(150, 0, -150));

                gcbPositions.Add(new Vector3(-25, 0, -25));
                gcbMomentum.Add(new Vector3(-200, 0, 200));

                for (int i = 0; i < gcbCount; i++)
                {
                    gcbActiveStatus.Add(true);
                }


                orbPositions.Add(new Vector3(2, 0, 2));
                orbMomentum.Add(new Vector3(50, 0, -50));

                orbPositions.Add(new Vector3(5, 0, -5));
                orbMomentum.Add(new Vector3(-25, 0, -25));

                orbPositions.Add(new Vector3(-8, 0, -8));
                orbMomentum.Add(new Vector3(-20, 0, 20));

                orbPositions.Add(new Vector3(-11, 0, 11));
                orbMomentum.Add(new Vector3(12, 0, 12));

                for (int i = 0; i < orbCount; i++)
                {
                    orbActiveStatus.Add(true);
                }
                break;
            default:
                break;
        }
    }
}
