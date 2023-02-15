using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterFirstDisplay : MonoBehaviour
{
    private float timeHolder;
    [SerializeField]
    private Transform movementDisplayLastPoint;
    private PathManager mode;
    private CharacterController1 ch;
    private void Start()
    {
        timeHolder = Time.time;
        mode = GetComponent<PathManager>();
        ch = GetComponent<CharacterController1>();
    }
    void Update()
    {
        CharacterFirstDisplayFunc();
    }
    private void CharacterFirstDisplayFunc()
    {
        if (Time.time > timeHolder + 1.3f && Time.timeScale != 0)
        {
            ch.enabled = true;
            mode.enabled = false;
            enabled = false;
        }
    }
}
