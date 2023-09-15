using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void ExplainerPage()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else   
                Application.Quit()
#endif
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}