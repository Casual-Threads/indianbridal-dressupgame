using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProps
{
    public string playerName;
    public int playerHealth;
    public int playerDamage;
    public int playerRange;
    public bool isLocked = true;
}

[System.Serializable]
public class Modesprops
{
    public bool isLocked;
}

[System.Serializable]
public class WeddingModeElements
{
    public List<bool> lehanga = new List<bool>();
    public List<bool> bangle = new List<bool>();
    public List<bool> earing = new List<bool>();
    public List<bool> shoes = new List<bool>();
    public List<bool> necklace = new List<bool>();
    public List<bool> mathapatti = new List<bool>();
    public List<bool> nosering = new List<bool>();
    public List<bool> eyeshade = new List<bool>();
    public List<bool> hair = new List<bool>();
    public List<bool> lips = new List<bool>();
    public List<bool> bindi = new List<bool>();
    public List<bool> blush = new List<bool>();
    public List<bool> eyebrow = new List<bool>();
    //public List<bool> clutch = new List<bool>();
}

[System.Serializable]
public class TraditionalModeElements
{
    public List<bool> dress = new List<bool>();
    public List<bool> bangle = new List<bool>();
    public List<bool> earing = new List<bool>();
    public List<bool> handitem = new List<bool>();
    public List<bool> shoes = new List<bool>();
    public List<bool> necklace = new List<bool>();
    public List<bool> frock = new List<bool>();
    public List<bool> eyeshade = new List<bool>();
    public List<bool> hair = new List<bool>();
    public List<bool> lips = new List<bool>();
    public List<bool> bindi = new List<bool>();
    public List<bool> blush = new List<bool>();
    public List<bool> eyebrow = new List<bool>();
}

[System.Serializable]
public class SahreeModeElements
{
    public List<bool> clutch = new List<bool>();
    public List<bool> dress = new List<bool>();
    public List<bool> bangle = new List<bool>();
    public List<bool> earing = new List<bool>();
    public List<bool> shoes = new List<bool>();
    public List<bool> necklace = new List<bool>();
    public List<bool> mathapatti = new List<bool>();
    public List<bool> nosering = new List<bool>();
    public List<bool> eyeshade = new List<bool>();
    public List<bool> hair = new List<bool>();
    public List<bool> lips = new List<bool>();
    public List<bool> blush = new List<bool>();
    public List<bool> eyebrow = new List<bool>();
    public List<bool> bindi = new List<bool>();
}

[System.Serializable]
public class PopMusicModeElements
{
    public List<bool> dress = new List<bool>();
    public List<bool> bangle = new List<bool>();
    public List<bool> earing = new List<bool>();
    public List<bool> shoes = new List<bool>();
    public List<bool> necklace = new List<bool>();
    public List<bool> top = new List<bool>();
    public List<bool> bottom = new List<bool>();
    public List<bool> eyeshade = new List<bool>();
    public List<bool> hair = new List<bool>();
    public List<bool> lips = new List<bool>();
    public List<bool> blush = new List<bool>();
    public List<bool> eyebrow = new List<bool>();
}

[System.Serializable]
public class SaveData
{

    public static SaveData instance;
    public static SaveData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveData();
            }
            return instance;
        }
    }
    public bool RemoveAds = false;
    public bool vsMode = false;
    public int LevelsUnlocked = 1;
    //public int levelIndex = 0;
    public int selectedCard;
    public int EventsUnlocked = 0;
    public int SelectedAvatar = 0;
    public string ProfileName;
    public bool ProfileCreated = false;
    public bool isSound = true, isMusic = true, isVibration = true, isRightControls = true;
    public int Coins = 2000;
    public List<PlayerProps> Players = new List<PlayerProps>();
    public List<Modesprops> ModeProps = new List<Modesprops>();
    public WeddingModeElements WeddingModeElements = new WeddingModeElements();
    public TraditionalModeElements TraditionalModeElements = new TraditionalModeElements();
    public SahreeModeElements SahreeModeElements = new SahreeModeElements();
    public PopMusicModeElements PopMusicModeElements = new PopMusicModeElements();
    public string hashOfSaveData;

    //Constructor to save actual GameData
    public SaveData() { }

    //Constructor to check any tampering with the SaveData
    public SaveData(bool ads, int levelsUnlocked, int eventsUnlocked, int coins, bool soundOn, bool musicOn, bool vibrationOn, bool rightControls, List<PlayerProps> _players,
                    List<Modesprops> _modeProps, WeddingModeElements _WeddingModeElements, TraditionalModeElements _TraditionalModeElements, SahreeModeElements _SahreeModeElements, PopMusicModeElements _PopMusicModeElements)
    {
        RemoveAds = ads;
        LevelsUnlocked = levelsUnlocked;
        EventsUnlocked = eventsUnlocked;
        Coins = coins;
        isSound = soundOn;
        isMusic = musicOn;
        isVibration = vibrationOn;
        isRightControls = rightControls;
        Players = _players;
        ModeProps = _modeProps;
        WeddingModeElements = _WeddingModeElements;
        TraditionalModeElements = _TraditionalModeElements;
        SahreeModeElements = _SahreeModeElements;
        PopMusicModeElements = _PopMusicModeElements;
    }
}