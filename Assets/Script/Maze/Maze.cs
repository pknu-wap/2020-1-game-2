﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Floor;
    public int Rows;
    public int Columns;
    private int currentRow = 0;
    private int currentColumn = 0;
    private bool Finish = false;
    private MazeRoom[,] room;
    
    void Start()
    {
        CreateMaze();

        while (!Finish)
        {
            DestroyWall();
            RepeatDestory();
        }
    }

    // 미로의 전체적인 틀 제작
    void CreateMaze()
    {
        room = new MazeRoom[Rows, Columns];

        float size = Wall.transform.localScale.x;
        float move = (size - 3) / 2 + 1.25f;
        PlayerPrefs.SetFloat("columns", Columns);
        PlayerPrefs.SetFloat("size", size);        

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                GameObject floor = Instantiate(Floor, new Vector3(i * size, 0f, j * size), Quaternion.identity);
                floor.name = "Floor (" + i + "," + j + ")";

                GameObject upWall = Instantiate(Wall, new Vector3(i * size - move, 1.75f, j * size), Quaternion.Euler(0f, 90f, 0f));
                upWall.name = "UpWall (" + i + "," + j + ")";

                GameObject downWall = Instantiate(Wall, new Vector3(i * size + move, 1.75f, j * size), Quaternion.Euler(0f, 90f, 0f));
                downWall.name = "DownWall (" + i + "," + j + ")";

                GameObject leftWall = Instantiate(Wall, new Vector3(i * size, 1.75f, j * size - move), Quaternion.identity);
                leftWall.name = "LeftWall (" + i + "," + j + ")";

                GameObject rightWall = Instantiate(Wall, new Vector3(i * size, 1.75f, j * size + move), Quaternion.identity);
                rightWall.name = "RightWall (" + i + "," + j + ")";

                room[i, j] = new MazeRoom();
                room[i, j].UpWall = upWall;
                room[i, j].DownWall = downWall;
                room[i, j].LeftWall = leftWall;
                room[i, j].RightWall = rightWall;

                floor.transform.parent = transform;
                upWall.transform.parent = transform;
                downWall.transform.parent = transform;
                leftWall.transform.parent = transform;
                rightWall.transform.parent = transform;
            }
        }
    }

    // 벽 부수기
    void DestroyWall()
    {
        // 현재 위치 기준으로 상하좌우 하나라도 Visited 값이 false일 경우 반복
        while (FindUnvisitedRoom())
        {
            int rand = Random.Range(0, 4);
            room[currentRow, currentColumn].Visited = true;

            // 위쪽 벽 부수기
            if (rand == 0)
            {
                if (UnVisited(currentRow - 1, currentColumn))
                {
                    if (room[currentRow, currentColumn].UpWall)
                    {
                        Destroy(room[currentRow, currentColumn].UpWall);
                    }
                    currentRow--;
                    room[currentRow, currentColumn].Visited = true;
                    if (room[currentRow, currentColumn].DownWall)
                    {
                        Destroy(room[currentRow, currentColumn].DownWall);
                    }
                }
            }
            // 아래쪽 벽 부수기
            else if (rand == 1)
            {
                if (UnVisited(currentRow + 1, currentColumn))
                {
                    if (room[currentRow, currentColumn].DownWall)
                    {
                        Destroy(room[currentRow, currentColumn].DownWall);
                    }
                    currentRow++;
                    room[currentRow, currentColumn].Visited = true;
                    if (room[currentRow, currentColumn].UpWall)
                    {
                        Destroy(room[currentRow, currentColumn].UpWall);
                    }
                }
            }
            // 왼쪽 벽 부수기
            else if (rand == 2)
            {
                if (UnVisited(currentRow, currentColumn - 1))
                {
                    if (room[currentRow, currentColumn].LeftWall)
                    {
                        Destroy(room[currentRow, currentColumn].LeftWall);
                    }
                    currentColumn--;
                    room[currentRow, currentColumn].Visited = true;
                    if (room[currentRow, currentColumn].RightWall)
                    {
                        Destroy(room[currentRow, currentColumn].RightWall);
                    }
                }
            }
            // 오른쪽 벽 부수기
            else if (rand == 3)
            {
                if (UnVisited(currentRow, currentColumn + 1))
                {
                    if (room[currentRow, currentColumn].RightWall)
                    {
                        Destroy(room[currentRow, currentColumn].RightWall);
                    }
                    currentColumn++;
                    room[currentRow, currentColumn].Visited = true;
                    if (room[currentRow, currentColumn].LeftWall)
                    {
                        Destroy(room[currentRow, currentColumn].LeftWall);
                    }
                }
            }
        }

    }

    // 원하는 위치의 Visited 값을 확인하는 함수
    bool UnVisited(int row, int column)
    {
        if (row >= 0 && row < Rows && column >= 0 && column < Columns && !room[row, column].Visited)
        {
            return true;
        }
        return false;
    }

    // 현재 위치 기준으로 상하좌우 하나라도 Visited 값이 flase면 true를 반환
    bool FindUnvisitedRoom()
    {
        // 위쪽 확인
        if (UnVisited(currentRow - 1, currentColumn))
        {
            return true;
        }
        // 아래쪽 확인
        if (UnVisited(currentRow + 1, currentColumn))
        {
            return true;
        }
        // 왼쪽 확인
        if (UnVisited(currentRow, currentColumn - 1))
        {
            return true;
        }
        // 오른쪽 확인
        if (UnVisited(currentRow, currentColumn + 1))
        {
            return true;
        }

        return false;
    }

    // 벽 부수기 반복
    void RepeatDestory()
    {
        Finish = true;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (!room[i, j].Visited && FindVisitedRoom(i, j))
                {
                    Finish = false;
                    currentRow = i;
                    currentColumn = j;
                    room[currentRow, currentColumn].Visited = true;
                    DestroyAdjacentWall();
                    return;
                }
            }
        }
    }

    // 원하는 위치 기준 상하좌우 하나라도 Visited 값이 true일 경우 true를 반환
    bool FindVisitedRoom(int row, int column)
    {
        // 위쪽 확인
        if (row > 0 && room[row - 1, column].Visited)
        {
            return true;
        }
        // 아래쪽 확인
        if (row < Rows && room[row + 1, column].Visited)
        {
            return true;
        }
        // 왼쪽 확인
        if (column > 0 && room[row, column - 1].Visited)
        {
            return true;
        }
        // 오른쪽 확인
        if (column < Columns && room[row, column + 1].Visited)
        {
            return true;
        }

        return false;
    }

    // 막힌 길 뚫기
    void DestroyAdjacentWall()
    {
        bool destroy = false;

        while (!destroy)
        {
            int direction = Random.Range(0, 4);

            // 위쪽 벽 부수기
            if (direction == 0)
            {
                if (currentRow > 0 && room[currentRow - 1, currentColumn].Visited)
                {
                    if (room[currentRow, currentColumn].UpWall)
                    {
                        Destroy(room[currentRow, currentColumn].UpWall);
                    }
                    if (room[currentRow - 1, currentColumn].DownWall)
                    {
                        Destroy(room[currentRow - 1, currentColumn].DownWall);
                    }
                    destroy = true;
                }
            }
            // 아래쪽 벽 부수기
            else if (direction == 1)
            {
                if (currentRow < Rows - 1 && room[currentRow + 1, currentColumn].Visited)
                {
                    if (room[currentRow, currentColumn].DownWall)
                    {
                        Destroy(room[currentRow, currentColumn].DownWall);
                    }
                    if (room[currentRow + 1, currentColumn].UpWall)
                    {
                        Destroy(room[currentRow + 1, currentColumn].UpWall);
                    }
                    destroy = true;
                }
            }
            // 왼쪽 벽 부수기
            else if (direction == 2)
            {
                if (currentColumn > 0 && room[currentRow, currentColumn - 1].Visited)
                {
                    if (room[currentRow, currentColumn].LeftWall)
                    {
                        Destroy(room[currentRow, currentColumn].LeftWall);
                    }
                    if (room[currentRow, currentColumn - 1].RightWall)
                    {
                        Destroy(room[currentRow, currentColumn - 1].RightWall);
                    }
                    destroy = true;
                }
            }
            // 아래쪽 벽 부수기
            else if (direction == 3)
            {
                if (currentColumn < Columns - 1 && room[currentRow, currentColumn + 1].Visited)
                {
                    if (room[currentRow, currentColumn].RightWall)
                    {
                        Destroy(room[currentRow, currentColumn].RightWall);
                    }
                    if (room[currentRow, currentColumn + 1].LeftWall)
                    {
                        Destroy(room[currentRow, currentColumn + 1].LeftWall);
                    }
                    destroy = true;
                }
            }
        }
    }
}
