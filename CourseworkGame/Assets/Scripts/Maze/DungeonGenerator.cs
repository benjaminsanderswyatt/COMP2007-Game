using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public enum RoomSpawning
    {
        CantSpawn,
        CanSpawn,
        MustSpawn
    }

    public class RoomSelection
    {
        public RoomSpawning spawning;
        public int probability;

        public RoomSelection(RoomSpawning spawning, int probability)
        {
            this.spawning = spawning;
            this.probability = probability;
        }
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
        public int WeightedProbability = 1;

        public bool obligatory;

        public RoomSelection ProbabililityOfSpawning(int x, int y)
        {

            var details = new RoomSelection(RoomSpawning.CantSpawn, WeightedProbability);

            if(x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                if (obligatory)
                {
                    details.spawning = RoomSpawning.MustSpawn;
                    return details;
                    
                }
                else
                {
                    details.spawning = RoomSpawning.CanSpawn;
                    return details;
                }
            }
            //Cant Spawn
            return details;
        }
    }

    public Vector2Int size;
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offset;

    List<Cell> board;

    private void Start()
    {
        MazeGenerator();
        GenerateDungeon();
    }

    private int SelectRoom(int i, int j)
    {
        int randomRoom = -1;

        List<int> availableRooms = new List<int>();

        for (int k = 0; k < rooms.Length; k++)
        {
            RoomSelection validRoom = rooms[k].ProbabililityOfSpawning(i, j);

            if (validRoom.spawning == RoomSpawning.MustSpawn)
            {
                randomRoom = k;
                break;
            }
            else if (validRoom.spawning == RoomSpawning.CanSpawn)
            {
                for (int l = 0; l < validRoom.probability; l++)
                {
                    availableRooms.Add(k);
                }
            }
        }

        if (randomRoom == -1)
        {
            if (availableRooms.Count > 0)
            {
                randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
            }
            else
            {
                //Error gives Default Room
                randomRoom = 0;
            }
        }

        return randomRoom;
    }



    private void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                int selectedRoom = SelectRoom(i,j);

                var newRoom = Instantiate(rooms[selectedRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newRoom.UpdateRoom(board[i + j * size.x].status);

                newRoom.name += " " + i + "-" + j;
            }
        }
    }


    private void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                   
                    if (newCell -1 == currentCell)
                    {
                        //Right
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        //Down
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    if (newCell + 1 == currentCell)
                    {
                        //Left
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        //Up
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }

        }


    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // Check Up
        if (cell - size.x >= 0 && !board[cell - size.x].visited)
        {
            neighbors.Add(cell - size.x);
        }

        // Check Down
        if (cell + size.x < board.Count && !board[cell + size.x].visited)
        {
            neighbors.Add(cell + size.x);
        }

        // Check Right
        if ((cell + 1) % size.x != 0 && !board[cell + 1].visited)
        {
            neighbors.Add(cell + 1);
        }

        // Check Left
        if (cell % size.x != 0 && !board[cell - 1].visited)
        {
            neighbors.Add(cell - 1);
        }

        return neighbors;
    }


}
