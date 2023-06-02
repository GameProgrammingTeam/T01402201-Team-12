using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SlimeManager slimeManager;
    private MapManager mapManager;
    private VirusSlimeManager virusSlimeManager;

    public float gameTime;
    public float maxGameTime = 2 * 10f;

    void Start()
    {
        slimeManager = gameObject.GetComponent<SlimeManager>();
        mapManager = gameObject.GetComponent<MapManager>();
        virusSlimeManager = gameObject.GetComponent<VirusSlimeManager>();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }
}