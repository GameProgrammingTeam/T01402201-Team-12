using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SlimeManager slimeManager;
    private MapManager mapManager;
    private VirusSlimeManager virusSlimeManager;

    void Start()
    {
        slimeManager = gameObject.GetComponent<SlimeManager>();
        mapManager = gameObject.GetComponent<MapManager>();
        virusSlimeManager = gameObject.GetComponent<VirusSlimeManager>();
    }
}