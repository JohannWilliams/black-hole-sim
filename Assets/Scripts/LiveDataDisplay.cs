using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiveDataDisplay : MonoBehaviour
{

    public TextMeshProUGUI liveDataText;
    private string liveDataString;

    // Start is called before the first frame update
    void Start()
    {
        liveDataString = "PLAY SIMULATION!";
        liveDataText.text = liveDataString;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindObjectOfType<SimManager>().GameState1 == SimManager.GameState.SimRunning)
        {
            updateLiveData();
        }
    }

    public void updateLiveData()
    {
        liveDataString = GameObject.FindObjectOfType<SimManager>().GetCBodyDataString();
        liveDataText.text = liveDataString;
    }
}
