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
}
