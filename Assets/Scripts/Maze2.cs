using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Maze2 : MonoBehaviour {
    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public bool northDestroyed = false;
        public bool southDestroyed = false;
        public bool westDestroyed = false;
        public bool eastDestroyed = false;
        public GameObject north;//1
        public GameObject east;//2
        public GameObject west;//3
        public GameObject south;//4
    }

    public GameObject wall;
    public float wallLength = 1.0f;
    public int size;
    private int xSize;
    private int ySize;
    private Vector3 initialPos;
    private GameObject WallHolder;
    private Cell[] cells;
    public int currentCell = 0;
    private int totalCells;
    private int visitedCells = 0;
    private bool startedBuilding = false;
    private int currentNeighbour = 0;
    private List<int> lastCells;
    private int backingUp = 0;
    private int wallToBreak = 0;
    public Vector3 startPos;
    public GameObject player1;
    public GameObject End;

    public float speed = 0.1f;
    private float timer;
    public float time = 0.1f;

    private int mode = 1;
    private float timerFive = 5f;
    private int cellNow = 0;
    private bool ingame = false;

    public Camera camera;
    public Texture u;
    public Texture d;
    public Texture l;
    public Texture r;
    public Material terrain;

    public TMP_Text timerText;
    private float timeLeft;
    public TMP_Text levelText;
    public static int level;

    public static int gamemode;
    public static Color32 colour;
    public Material player;
    public GameObject text;

    // Use this for initialization
    void Start () {
        CreateWalls();
        terrain.mainTexture = u;
        terrain.color = new Color32(236, 165, 255, 255);
        player.color = colour;
        text.SetActive(false);
    }

    void CreateWalls () {
        xSize = size;
        ySize = size;
        WallHolder = new GameObject();
        WallHolder.name = "Maze";
        initialPos = new Vector3((-xSize / 2) + wallLength / 2,0.0f, (-ySize / 2) + wallLength / 2);
        player1.transform.position = new Vector3(initialPos.x, 0.25f, initialPos.z - wallLength / 2);
        End.transform.position = new Vector3(initialPos.x + (xSize - 1) * wallLength, 0.25f, initialPos.z - wallLength / 2 + (ySize - 1) * wallLength);
        camera.orthographicSize = xSize * 0.55f;
        level = size - 4;
        if (gamemode == 0)
        {
            timeLeft = size * 6;
        }
        if (gamemode == 1)
        {
            timeLeft = size * 5;
        }
        if (gamemode == 2)
        {
            timeLeft = size * 4;
        }
        if (gamemode == 3)
        {
            timeLeft = size * 3;
        }
        if (gamemode == 4)
        {
            timeLeft = size * 2;
        }
        if (size % 2 == 0)
        {
            camera.transform.position = new Vector3(0, 5, -0.5f);
        }
        else
        {
            camera.transform.position = new Vector3(0.5f, 5, 0);
        }
        Vector3 myPos = initialPos;
        GameObject tempWall;
        Collider tempCollider;
        Rigidbody rb;

        rb = WallHolder.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        for (int i = 0; i < ySize; i++) //X-axis Walls
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2,0,initialPos.z + (i*wallLength) - wallLength/2);
                tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                tempCollider = tempWall.GetComponent<Collider>();
                tempCollider.isTrigger = true;
                tempWall.transform.parent = WallHolder.transform;
            }
        }

        for (int i = 0; i <= ySize; i++) //Y-axis Wall
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength), 0, initialPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(wall, myPos, Quaternion.Euler(0,90,0)) as GameObject;
                tempWall.transform.parent = WallHolder.transform;
            }
        }
        CreateCells();
    }

    void CreateCells ()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        totalCells = xSize * ySize;
        int children = WallHolder.transform.childCount;
        GameObject[] allWalls = new GameObject[children];
        cells = new Cell[xSize*ySize];
        int eastWestProcess = 0;
        int childProcess = 0;
        int termCount = 0;

        for (int i = 0; i < children; i++) //Gets all Children
        {
            allWalls[i] = WallHolder.transform.GetChild(i).gameObject;
        }

        for (int cellprocess = 0; cellprocess < cells.Length; cellprocess++) //Assigns walls to the cells
        {
            cells[cellprocess] = new Cell();
            cells[cellprocess].west = allWalls[eastWestProcess];
            cells[cellprocess].south = allWalls[childProcess + (xSize + 1) * ySize];
            if (termCount == xSize)
            {
                eastWestProcess += 2;
                termCount = 0;
            }
            else
            {
                eastWestProcess++;
            }

            termCount++;
            childProcess++;
            cells[cellprocess].east = allWalls[eastWestProcess];
            cells[cellprocess].north = allWalls[(childProcess + (xSize + 1) * ySize) + xSize - 1];
        }
        CreateMaze();
    }

    void CreateMaze ()
    {
        while (visitedCells < totalCells)
        {
            if (visitedCells < totalCells)
            {
                if (startedBuilding)
                {
                    GiveMeNeighbour();
                    if (cells[currentNeighbour].visited == false && cells[currentCell].visited == true)
                    {
                        BreakWall();
                        cells[currentNeighbour].visited = true;
                        visitedCells++;
                        lastCells.Add(currentCell);
                        currentCell = currentNeighbour;
                        if (lastCells.Count > 0)
                        {
                            backingUp = lastCells.Count - 1;
                        }
                    }
                }
                else
                {
                    currentCell = UnityEngine.Random.Range(0, totalCells);
                    cells[currentCell].visited = true;
                    visitedCells++;
                    startedBuilding = true;
                }
            }
        }
        Debug.Log("Finished");
        ingame = true;
    }

    void BreakWall ()
    {
        switch (wallToBreak)
        {
            case 1: Destroy(cells[currentCell].north);
                cells[currentNeighbour].southDestroyed = true;
                cells[currentCell].northDestroyed = true; break;
            case 2: Destroy(cells[currentCell].east);
                cells[currentNeighbour].westDestroyed = true;
                cells[currentCell].eastDestroyed = true; break;
            case 3: Destroy(cells[currentCell].west);
                cells[currentNeighbour].eastDestroyed = true;
                cells[currentCell].westDestroyed = true; break;
            case 4: Destroy(cells[currentCell].south);
                cells[currentNeighbour].northDestroyed = true;
                cells[currentCell].southDestroyed = true; break;
        }
    }
	
    void GiveMeNeighbour ()
    {
        int length = 0;
        int[] neighbours = new int[4];
        int[] connectingWall = new int[4];
        int check = (currentCell + 1) / xSize;
        check -= 1;
        check *= xSize;
        check += xSize;

        //east
        if (currentCell + 1 < totalCells && (currentCell + 1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighbours[length] = currentCell + 1;
                connectingWall[length] = 2;
                length++;
            }
        }

        //west
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbours[length] = currentCell - 1;
                connectingWall[length] = 3;
                length++;
            }
        }

        //north
        if (currentCell + xSize < totalCells)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighbours[length] = currentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }

        //south
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighbours[length] = currentCell - xSize;
                connectingWall[length] = 4;
                length++;
            }
        }

        if (length != 0)
        {
            int chosen = UnityEngine.Random.Range(0, length);
            currentNeighbour = neighbours[chosen];
            wallToBreak = connectingWall[chosen];
        }
        else
        {
            if (backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
        }
        startPos = (cells[1].east.transform.position + cells[1].west.transform.position + cells[1].north.transform.position + cells[1].south.transform.position) / 4;
    }
    void FixedUpdate()
    {
        timerText.text = Math.Round(timeLeft, 2).ToString();
        levelText.text = "Level " + level.ToString();
        if (timeLeft <= 0 || Input.GetKey("m"))
        {
            timeLeft = 0;
            text.SetActive(true);
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
        if (mode == 1)
        {
            if (cells[cellNow].northDestroyed == true)
            {
                if (Input.GetKey("w"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position + new Vector3(0, 0, 1);
                        timer = time;
                        cellNow = cellNow + xSize;
                    }
                }
            }
            if (cells[cellNow].southDestroyed == true)
            {
                if (Input.GetKey("s"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position - new Vector3(0, 0, 1);
                        timer = time;
                        cellNow = cellNow - xSize;
                    }
                }
            }
            if (cells[cellNow].westDestroyed == true)
            {
                if (Input.GetKey("a"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position - new Vector3(1, 0, 0);
                        timer = time;
                        cellNow = cellNow - 1;
                    }
                }
            }
            if (cells[cellNow].eastDestroyed == true)
            {
                if (Input.GetKey("d"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position + new Vector3(1, 0, 0);
                        timer = time;
                        cellNow = cellNow + 1;
                    }
                }
            }
        }
        if (mode == 2)
        {
            if (cells[cellNow].northDestroyed == true)
            {
                if (Input.GetKey("a"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position + new Vector3(0, 0, 1);
                        timer = time;
                        cellNow = cellNow + xSize;
                    }
                }
            }
            if (cells[cellNow].southDestroyed == true)
            {
                if (Input.GetKey("d"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position - new Vector3(0, 0, 1);
                        timer = time;
                        cellNow = cellNow - xSize;
                    }
                }
            }
            if (cells[cellNow].westDestroyed == true)
            {
                if (Input.GetKey("s"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position - new Vector3(1, 0, 0);
                        timer = time;
                        cellNow = cellNow - 1;
                    }
                }
            }
            if (cells[cellNow].eastDestroyed == true)
            {
                if (Input.GetKey("w"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position + new Vector3(1, 0, 0);
                        timer = time;
                        cellNow = cellNow + 1;
                    }
                }
            }
        }
        if (mode == 3)
        {
            if (cells[cellNow].northDestroyed == true)
            {
                if (Input.GetKey("s"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position + new Vector3(0, 0, 1);
                        timer = time;
                        cellNow = cellNow + xSize;
                    }
                }
            }
            if (cells[cellNow].southDestroyed == true)
            {
                if (Input.GetKey("w"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position - new Vector3(0, 0, 1);
                        timer = time;
                        cellNow = cellNow - xSize;
                    }
                }
            }
            if (cells[cellNow].westDestroyed == true)
            {
                if (Input.GetKey("d"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position - new Vector3(1, 0, 0);
                        timer = time;
                        cellNow = cellNow - 1;
                    }
                }
            }
            if (cells[cellNow].eastDestroyed == true)
            {
                if (Input.GetKey("a"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position + new Vector3(1, 0, 0);
                        timer = time;
                        cellNow = cellNow + 1;
                    }
                }
            }
        }
        if (mode == 4)
        {
            if (cells[cellNow].northDestroyed == true)
            {
                if (Input.GetKey("d"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position + new Vector3(0, 0, 1);
                        timer = time;
                        cellNow = cellNow + xSize;
                    }
                }
            }
            if (cells[cellNow].southDestroyed == true)
            {
                if (Input.GetKey("a"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position - new Vector3(0, 0, 1);
                        timer = time;
                        cellNow = cellNow - xSize;
                    }
                }
            }
            if (cells[cellNow].westDestroyed == true)
            {
                if (Input.GetKey("w"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position - new Vector3(1, 0, 0);
                        timer = time;
                        cellNow = cellNow - 1;
                    }
                }
            }
            if (cells[cellNow].eastDestroyed == true)
            {
                if (Input.GetKey("s"))
                {
                    if (timer < 0)
                    {
                        player1.transform.position = player1.transform.position + new Vector3(1, 0, 0);
                        timer = time;
                        cellNow = cellNow + 1;
                    }
                }
            }
        }
        if (cellNow == totalCells - 1 && ingame == true)
        {
            size += 1;
            Destroy(WallHolder);
            startedBuilding = false;
            currentCell = 0;
            visitedCells = 0;
            currentNeighbour = 0;
            backingUp = 0;
            wallToBreak = 0;
            CreateWalls();
            cellNow = 0;
        }

        timer -= Time.deltaTime;
        timerFive -= Time.deltaTime;
        if (timerFive < 0)
        {
            timerFive = 5;
            mode = mode + 1;
            if (terrain.mainTexture == l)
            {
                terrain.mainTexture = u;
                terrain.color = new Color32(236, 165, 255, 255);
                Debug.Log("u");
            }
            else if (terrain.mainTexture == d)
            {
                terrain.mainTexture = l;
                terrain.color = new Color32(165, 208, 255, 255);
                Debug.Log("l");
            }
            else if (terrain.mainTexture == r)
            {
                terrain.mainTexture = d;
                terrain.color = new Color32(255, 182, 89, 255);
                Debug.Log("d");
            }
            else if (terrain.mainTexture == u)
            {
                terrain.mainTexture = r;
                terrain.color = new Color32(162, 253, 113, 255);
                Debug.Log("r");
            }
        }
        if (mode == 5)
        {
            mode = 1;
        }
    }
    public void Return()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
        if (SaveSystem.LoadData() != null)
        {
            if (gamemode == 0 && level > SaveSystem.LoadData().VELevel)
            {
                Highscores highscore = new Highscores();
                highscore.VeryEasy();
            }
            if (gamemode == 1 && level > SaveSystem.LoadData().ELevel)
            {
                Highscores highscore = new Highscores();
                highscore.Easy();
            }
            if (gamemode == 2 && level > SaveSystem.LoadData().MLevel)
            {
                Highscores highscore = new Highscores();
                highscore.Moderate();
            }
            if (gamemode == 3 && level > SaveSystem.LoadData().HLevel)
            {
                Highscores highscore = new Highscores();
                highscore.Hard();
            }
            if (gamemode == 4 && level > SaveSystem.LoadData().VHLevel)
            {
                Highscores highscore = new Highscores();
                highscore.VeryHard();
            }
            else
            {
                Highscores highscore = new Highscores();
                highscore.Else();
            }
        }
    }
}
