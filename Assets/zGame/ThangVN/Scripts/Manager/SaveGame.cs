using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveGame
{
    public static int currentLevel = -1;

    const string SOUND = "SOUND";
    static int sound = -1;

    public static bool Sound
    {
        set
        {
            ES3.Save(SOUND, value ? 1 : 0);
            sound = value ? 1 : 0;
        }
        get
        {
            if (sound == -1) sound = ES3.Load(SOUND, 1);
            return sound == 1;
        }
    }

    const string MUSIC = "MUSIC";
    static int music = -1;

    public static bool Music
    {
        set
        {
            ES3.Save(MUSIC, value ? 1 : 0);
            music = value ? 1 : 0;
        }
        get
        {
            if (music == -1) music = ES3.Load(MUSIC, 1);
            return music == 1;
        }
    }

    const string LEVEL = "LEVEL";
    static int level = -1;

    public static int Level
    {
        set
        {
            ES3.Save(LEVEL, value);
            level = value;
        }
        get
        {
            if (level == -1) level = ES3.Load(LEVEL, 0);
            return level;
        }
    }

    const string COIN = "COIN";
    static int coin = -1;

    public static int Coin
    {
        set
        {
            ES3.Save(COIN, value);
            coin = value;
        }
        get
        {
            if (coin == -1) coin = ES3.Load(COIN, 0);
            return coin;
        }
    }

    const string ENERGY = "ENERGY";
    static int energy = -1;

    public static int Energy
    {
        set
        {
            ES3.Save(ENERGY, value);
            energy = value;
        }
        get
        {
            if (energy == -1) energy = ES3.Load(ENERGY, 0);
            return energy;
        }
    }

    const string BESTSCORE = "BESTSCORE";
    static float bestScore = -1f;

    public static float BestScore
    {
        set
        {
            ES3.Save(BESTSCORE, value);
            bestScore = value;
        }
        get
        {
            if (bestScore == -1) bestScore = ES3.Load(BESTSCORE, 0);
            return bestScore;
        }
    }

}
