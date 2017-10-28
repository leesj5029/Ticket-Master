﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {



    public GameObject[] timeGaugeObject;

    public GameObject[] refreshTicket;
    public GameObject[] refreshTicketEffect;
    public PlayerControl[] _playerControl;
    public RandomManager _randomManager;

    public string[] effectString;
    public bool fight;

    public GameObject startBack;
    public GAui[] startPlayerGaui;
    public GAui vsGaui;



    public static List<GameObject> hiddenEffectPool = new List<GameObject>();
    public GameObject hiddenEffectPrefab;
    public Transform objectBox;


    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;
        StartGame();
        CreateHiddenEffect();

    }
    
	
	// Update is called once per frame
	void Update () {
		
	}



    void CreateHiddenEffect()
    {
        for (int i = 0; i < 60; i++)
        {
            GameObject hiddenEffect = (GameObject)Instantiate(hiddenEffectPrefab);
            hiddenEffect.name = "hidden_" + i.ToString();
            hiddenEffect.SetActive(false);
            hiddenEffect.transform.SetParent(objectBox);
            hiddenEffectPool.Add(hiddenEffect);
        }
    }
    void StartGame()
    {
        StartCoroutine("StartCoroutine");
    }
    


    public void NextTurn()
    {
        StopCoroutine("TimeCountDownCoroutine");
        StartCoroutine("TimeCountDownCoroutine");
    }

    IEnumerator StartCoroutine()
    {
        startBack.SetActive(true);
        vsGaui.gameObject.SetActive(true);
        vsGaui.MoveIn();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < startPlayerGaui.Length; i++)
        {
            startPlayerGaui[i].gameObject.SetActive(true);
            startPlayerGaui[i].MoveIn();
        }
        yield return new WaitForSeconds(3.6f);
        for (int i = 0; i < startPlayerGaui.Length; i++)
        {
            startPlayerGaui[i].MoveOut();
        }
        vsGaui.MoveOut();
        yield return new WaitForSeconds(0.6f);
        startBack.SetActive(false);
    }
    WaitForSeconds timeCountDownDelay = new WaitForSeconds(1f);
    IEnumerator TimeCountDownCoroutine()
    {
        for (int i = 0; i < timeGaugeObject.Length; i++)
        {
            timeGaugeObject[i].SetActive(true);
        }
        for (int i = timeGaugeObject.Length - 1; i >= 0; i--)
        {
            timeGaugeObject[i].SetActive(false);
            yield return timeCountDownDelay;
        }
    }

    public void RefreshTicketUse()
    {
        for (int i = refreshTicket.Length - 1; i >= 0; i--)
        {
            if (refreshTicket[i].activeSelf)
            {
                refreshTicket[i].SetActive(false);
                refreshTicketEffect[i].SetActive(true);
                break;
            }
        }
        Refresh();
    }
    public void Refresh()
    {
        _randomManager.RandomMix();
    }

    public void FightStart()
    {
        fight = true;
        StartCoroutine("FightCoroutine");
    }
    IEnumerator FightCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        _playerControl[0].Fight();
        yield return new WaitForSeconds(1f);
        _playerControl[1].Fight();
    }
}
