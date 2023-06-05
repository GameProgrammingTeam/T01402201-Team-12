using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float virusSlimeUpgradeTime = 5.0f;
    private float virusSlimeTime = 0.0f;
    private SlimeManager slimeManager;
    private MapManager mapManager;
    private VirusSlimeManager virusSlimeManager;

    public float gameTime;
    public float maxGameTime = 2 * 10f;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        slimeManager = gameObject.GetComponent<SlimeManager>();
        mapManager = gameObject.GetComponent<MapManager>();
        virusSlimeManager = gameObject.GetComponent<VirusSlimeManager>();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        manageVirusSlime();
    }

    public void StartTime()
    {
        Time.timeScale = 1;
    } 

    private void manageVirusSlime()
    {
        virusSlimeTime += Time.deltaTime;
        if (virusSlimeTime >= virusSlimeUpgradeTime)
        {
            virusSlimeManager.Upgrade();
            virusSlimeTime = 0;
        }
    }
}