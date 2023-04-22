using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Highscores
{
    public int VELevel = 0;
    public int ELevel = 0;
    public int MLevel = 0;
    public int HLevel = 0;
    public int VHLevel = 0;
    public int mode;
    public int volume;
    public bool savedVolume;

    public void VeryEasy()
    {
        VELevel = Maze2.level;
        ELevel = SaveSystem.LoadData().ELevel;
        MLevel = SaveSystem.LoadData().MLevel;
        HLevel = SaveSystem.LoadData().HLevel;
        VHLevel = SaveSystem.LoadData().VHLevel;
        mode = Menu.gamemode;
        volume = Menu.mode;
        savedVolume = true;
        SaveSystem.SaveData(this);
    }
    public void Easy()
    {
        ELevel = Maze2.level;
        VELevel = SaveSystem.LoadData().VELevel;
        MLevel = SaveSystem.LoadData().MLevel;
        HLevel = SaveSystem.LoadData().HLevel;
        VHLevel = SaveSystem.LoadData().VHLevel;
        mode = Menu.gamemode;
        volume = Menu.mode;
        savedVolume = true;
        SaveSystem.SaveData(this);
    }
    public void Moderate()
    {
        MLevel = Maze2.level;
        VELevel = SaveSystem.LoadData().VELevel;
        ELevel = SaveSystem.LoadData().ELevel;
        HLevel = SaveSystem.LoadData().HLevel;
        VHLevel = SaveSystem.LoadData().VHLevel;
        mode = Menu.gamemode;
        volume = Menu.mode;
        savedVolume = true;
        SaveSystem.SaveData(this);
    }
    public void Hard()
    {
        HLevel = Maze2.level;
        VELevel = SaveSystem.LoadData().VELevel;
        ELevel = SaveSystem.LoadData().ELevel;
        MLevel = SaveSystem.LoadData().MLevel;
        VHLevel = SaveSystem.LoadData().VHLevel;
        mode = Menu.gamemode;
        volume = Menu.mode;
        savedVolume = true;
        SaveSystem.SaveData(this);
    }
    public void VeryHard()
    {
        VHLevel = Maze2.level;
        VELevel = SaveSystem.LoadData().VELevel;
        ELevel = SaveSystem.LoadData().ELevel;
        MLevel = SaveSystem.LoadData().MLevel;
        HLevel = SaveSystem.LoadData().HLevel;
        mode = Menu.gamemode;
        volume = Menu.mode;
        savedVolume = true;
        SaveSystem.SaveData(this);
    }
    public void Else()
    {
        VELevel = SaveSystem.LoadData().VELevel;
        ELevel = SaveSystem.LoadData().ELevel;
        MLevel = SaveSystem.LoadData().MLevel;
        HLevel = SaveSystem.LoadData().HLevel;
        VHLevel = SaveSystem.LoadData().VHLevel;
        mode = Menu.gamemode;
        volume = Menu.mode;
        savedVolume = true;
        SaveSystem.SaveData(this);
    }
}
