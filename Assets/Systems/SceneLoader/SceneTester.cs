using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.SceneManager;

public class SceneTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SeneLoader.LoadScene("TestSceneB", SceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
