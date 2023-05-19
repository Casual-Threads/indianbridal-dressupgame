using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Links : MonoBehaviour
{
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/search?q=pub%3A%20Happy%20Games%20Play&c=apps&hl=en&gl=PK");
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.happygames.indianbridal.dressup.game");
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://docs.google.com/document/d/1PrJO5lUSvecTy6mZddTmQWhjlbqFGYjKyvR7ZJe9HZM/edit?usp=drivesdk");
    }
}
