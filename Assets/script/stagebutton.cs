using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class stagebutton : MonoBehaviour
{
    public Sprite clearsprite;
    public Sprite locked;
    public Sprite active;
    public int condition,laststage;
    public Button button;
    public int roomNum;

    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {   
        laststage = PlayerPrefs.GetInt("stage" + (roomNum-1).ToString() , 0);
        condition = PlayerPrefs.GetInt("stage"+roomNum.ToString(), 0);
        if (true/*마우스가 버튼위에 올려진 상태가 아니라면*/)
        {
            if (laststage > 0 ? true : false)
            {
                if (condition > 0 ? true : false)
                {
                    button.image.overrideSprite = clearsprite;
                }
                else
                {
                    button.image.overrideSprite = null;
                }
            }
            else
            {
                button.image.overrideSprite = locked;
            }
        }
    }
}
