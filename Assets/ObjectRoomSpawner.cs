using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }
    public GridController grid;
    public RandomSpawner[] spawnerData;
    public void InitialiseObjectSpawning()
    {
        foreach(RandomSpawner rs in spawnerData)
        {
            SpawnObject(rs);
        }
    }
    void SpawnObject(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);
        for(int i =0;i<randomIteration;i++)
        {
            int randomPos = Random.Range(0, grid.availablePionts.Count-1);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availablePionts[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePionts.RemoveAt(randomPos);
            Debug.Log("Spawned Object!");
        }
    }
}
