﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoinsAdder : MonoBehaviour
{
    public Text coins;
    //public Text levelCompcoins;
    public int totalCoins;
    public int addCoins;
    public int doItrations = 10;
    public bool addNow;
    public bool resetNow;
    public AudioSource coinSound;
    public GameObject coinsAnim;
    // Start is called before the first frame update
    void Start()
    {
        //if (GameManager.Instance.Initialized == false)
        //{
        //    GameManager.Instance.Initialized = true;
        //}
            Usman_SaveLoad.LoadProgress();
        coins.text = SaveData.Instance.Coins.ToString();
        //levelCompcoins.text = SaveData.Instance.Coins.ToString();
    }
    IEnumerator CoinsAddition()
    {
        int modValue = addCoins % doItrations;
        int perValue = (addCoins) / doItrations;
        int loopValue = 0;
        if (perValue == 0)
        {
            modValue = 0;
            perValue = 1;
            loopValue = addCoins;
        }
        else
        {
            loopValue = doItrations;
        }
        if (coinSound)
        {
            coinSound.Play();
        }

        for (int i = 0; i < loopValue; i++)
        {
            totalCoins += perValue;
            SaveData.Instance.Coins += perValue;
            //if (coins || levelCompcoins)
            //{
            coins.text = totalCoins.ToString();
            //    levelCompcoins.text = totalCoins.ToString();

            //}
            yield return new WaitForSecondsRealtime(0.1f);
        }
        totalCoins += modValue;
        if (coinSound)
        {
            coinSound.Stop();
        }
        Usman_SaveLoad.SaveProgress();
        if (coinsAnim)
        {
            coinsAnim.SetActive(false);
        }
    }
    private void ResetVlaues()
    {
        totalCoins = addCoins = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (addNow)
        {
            totalCoins = SaveData.Instance.Coins;
            addNow = !addNow;
            StartCoroutine(CoinsAddition());
            if (coinsAnim)
            {
                coinsAnim.SetActive(true);
            }
        }
        if (resetNow)
        {
            resetNow = !resetNow;
            ResetVlaues();
        }
    }
}
