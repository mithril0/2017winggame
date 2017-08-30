﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Play,
    Pause,
    Die,
    Clear
}
public class GameManager : MonoBehaviour {
    public GameState GS;
    public GameObject GUI_pause;
    public GameObject GUI_clear;
    public GameObject GUI_die;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Clear()
    {
        GS = GameState.Clear;
        GUI_clear.SetActive(true);
    }
    public void Die()
    {
        GS = GameState.Die;
        GUI_die.SetActive(true);
    }
    public void Pause()
    {
        GS = GameState.Pause;
        GUI_pause.SetActive(true);
    }
}
