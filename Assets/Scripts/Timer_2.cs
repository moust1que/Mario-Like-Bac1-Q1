using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_2 : MonoBehaviour
{
    
    //erreur peut-être à cause du game manager ?
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
        void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime = Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
         //sert à bloquer à 0 et pas aller à -1 "seconde"

            //game over ?
            timerText.color = Color.red;
            //couleur au countdown quand 0
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{2:00}", minutes, seconds);
    }

}
