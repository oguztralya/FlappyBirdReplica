using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuControlScript : MonoBehaviour
{
    [SerializeField] private Text t_bestRecord;
    [SerializeField] private Text t_point;
    
    public void playButton() 
    {
        SceneManager.LoadScene("Level1");
    }

    public void backToDesktopButton() 
    {
        Application.Quit();
    }

    void Start() {
        t_bestRecord.text="Best Record: "+PlayerPrefs.GetInt("bestrecord");
        t_point.text="Your Point: "+PlayerPrefs.GetInt("point");
    }

}
