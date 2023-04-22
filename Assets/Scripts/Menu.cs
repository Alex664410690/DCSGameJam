using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public static int mode = 3;
    public Image image;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Image player;
    public TMP_Text HighScore;
    public static int gamemode;

    public void Start()
    {
        if (SaveSystem.LoadData().savedVolume == true)
        {
            mode = SaveSystem.LoadData().volume;
            if (mode == 0)
            {
                image.sprite = one;
            }
            if (mode == 1)
            {
                image.sprite = two;
            }
            if (mode == 2)
            {
                image.sprite = three;
            }
            if (mode == 3)
            {
                image.sprite = four;
            }
        }
        else
        {
            image.sprite = four;
        }
        Music.mode = mode;
        dropdown.value = gamemode;
    }
    public void Update()
    {
        Highscores data = SaveSystem.LoadData();
        if (gamemode == 0)
        {
            HighScore.text = "Highscore: " + data.VELevel;
        }
        if (gamemode == 1)
        {
            HighScore.text = "Highscore: " + data.ELevel;
        }
        if (gamemode == 2)
        {
            HighScore.text = "Highscore: " + data.MLevel;
        }
        if (gamemode == 3)
        {
            HighScore.text = "Highscore: " + data.HLevel;
        }
        if (gamemode == 4)
        {
            HighScore.text = "Highscore: " + data.VHLevel;
        }
        if (Input.GetKey("r"))
        {
            SaveSystem.SaveData(new Highscores());
        }
        gamemode = dropdown.value;
    }
    public void Play()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Volume()
    {
        mode += 1;
        if (mode == 4)
        {
            mode = 0;
        }
        if (mode == 0)
        {
            image.sprite = one;
        }
        if (mode == 1)
        {
            image.sprite = two;
        }
        if (mode == 2)
        {
            image.sprite = three;
        }
        if (mode == 3)
        {
            image.sprite = four;
        }
        Music.mode = mode;
    }
    public void Dropdown()
    {
        Maze2.gamemode = dropdown.value;
    }
    public void Colour(string stats)
    {
        string temp = stats.Substring(0, 3);
        int r = int.Parse(temp);
        temp = stats.Substring(3, 3);
        int g = int.Parse(temp);
        temp = stats.Substring(6, 3);
        int b = int.Parse(temp);
        player.color = new Color32((byte)r, (byte)g, (byte)b, 255);
        Maze2.colour = new Color32((byte)r, (byte)g, (byte)b, 255);
    }
}
