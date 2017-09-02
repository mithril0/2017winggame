using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Play,
    Pause,
    GameOver,
    Clear
}
public class GameManager : MonoBehaviour {
    public GameState GS;
    public GameObject GUI_pause;
    public GameObject GUI_clear;
    public GameObject GUI_die;
    public GameObject GUI_play;
    public GameObject Player;
	public float OriginTime;
    public controlDoor Door;
    public PlayerMove PM;
	// Use this for initialization
	void Start () {
        GUI_play.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (PM.PS2 == Playerstates2.DeadCW || PM.PS2 == Playerstates2.DeadCCW)
        {
            GameOver();
        }
    }
    public void Clear()
    {
        if (Door.ClearActivated == true)
        {
            GS = GameState.Clear;
            OriginTime = Time.timeScale;
            Time.timeScale = 0f;
            GUI_play.SetActive(false);
            GUI_clear.SetActive(true);
        }
    }
    public void GameOver()
    {
        GS = GameState.GameOver;
        OriginTime = Time.timeScale;
        Time.timeScale = 0f;
        GUI_die.SetActive(true);
    }
    
	public void Pause()
    {
        GS = GameState.Pause;
		OriginTime = Time.timeScale;
        Time.timeScale=0f;
        GUI_play.SetActive(false);
        GUI_pause.SetActive(true);
    }

	public void UnPause(){
		GS = GameState.Play;
		Time.timeScale = OriginTime;
        GUI_pause.SetActive(false);
        GUI_play.SetActive(true);
    }

	public void Replay(){
		Time.timeScale = 1f;
		Application.LoadLevel("TestScene2");
	}

	public void MainGo(){
		//Time.timeScale = 1f;
		Application.LoadLevel("menu");
	}
		
    public void Play()
    {
        GS = GameState.Pause;
        GUI_play.SetActive(true);
        GUI_pause.SetActive(false);
        GUI_die.SetActive(false);
        GUI_clear.SetActive(false);
    }
}
