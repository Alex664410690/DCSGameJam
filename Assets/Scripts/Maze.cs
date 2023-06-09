﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {
    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public GameObject north;//1
        public GameObject east;//2
        public GameObject west;//3
        public GameObject south;//4
    }

    public GameObject wall;
    public float wallLength = 1.0f;
    public int xSize = 5;
    public int ySize = 5;
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

    // Use this for initialization
    void Start () {
        CreateWalls();
	}

    void CreateWalls () {
        WallHolder = new GameObject();
        WallHolder.name = "Maze";
        initialPos = new Vector3((-xSize / 2) + wallLength / 2,0.0f, (-ySize / 2) + wallLength / 2);
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
                    currentCell = Random.Range(0, totalCells);
                    cells[currentCell].visited = true;
                    visitedCells++;
                    startedBuilding = true;
                }
            }
        }

        Debug.Log("Finished");
    }

    void BreakWall ()
    {
        switch (wallToBreak)
        {
            case 1: Destroy(cells[currentCell].north); break;
            case 2: Destroy(cells[currentCell].east); break;
            case 3: Destroy(cells[currentCell].west); break;
            case 4: Destroy(cells[currentCell].south); break;
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
            int chosen = Random.Range(0, length);
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
    }
	// Update is called once per frame
	void Update () {
		
	}
}
