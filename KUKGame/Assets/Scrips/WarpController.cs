﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpController : MonoBehaviour
{
    public string sceneName;
    public AudioSource warpSound;
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Invoke("LoadNextScene", 0.5f);
            //บันทึกซีน
            PlayerPrefs.SetString("PrevScene", sceneName);
            //บันทึกเหรียญ
            var player = other.gameObject.GetComponent<PlayerConRegisbody>();
            PlayerPrefs.SetInt("CoinCount", player.coinCount);
           
            warpSound?.Play();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
