using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }
    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePionts = new List<Vector2>();
    private void Awake()
    {
        room=GetComponentInParent<Room>();
        grid.columns = room.Width - 5;
        grid.rows= room.Height - 5;
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;
        for(int y=0;y<grid.rows;y++)
        {
            for(int x=0; x<grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.transform.position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                go.name = "X: " + x + ", Y: " + y;
                availablePionts.Add(go.transform.position);
                go.SetActive(false);
            }
        }
        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
