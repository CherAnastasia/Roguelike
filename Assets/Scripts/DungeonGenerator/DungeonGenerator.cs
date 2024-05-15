using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;
    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        if (Level.level == 1)
            RoomController.instance.LoadRoom("Start", 0, 0);
        else
            RoomController.instance.LoadRoom("Start2", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
                RoomController.instance.LoadRoom(/*"Basic1"*/RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
        }
    }
}
