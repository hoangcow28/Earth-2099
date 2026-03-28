using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    public Transform player;
    public GameObject chunkPrefab;

    public int chunkWidth = 24;
    public int chunkHeight = 17;

    private Dictionary<Vector2, GameObject> spawnedChunks = new Dictionary<Vector2, GameObject>();

    void Update()
    {
        Vector2 playerChunk = new Vector2(
            Mathf.Floor(player.position.x / chunkWidth),
            Mathf.Floor(player.position.y / chunkHeight)
        );

        List<Vector2> neededChunks = new List<Vector2>();

        // 🔥 Tạo chunk xung quanh player
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 coord = playerChunk + new Vector2(x, y);
                neededChunks.Add(coord);

                if (!spawnedChunks.ContainsKey(coord))
                {
                    Vector3 pos = new Vector3(
                            coord.x * chunkWidth + chunkWidth / 2f,
                            coord.y * chunkHeight + chunkHeight / 2f,
                            0
                        );

                    GameObject chunk = Instantiate(chunkPrefab, pos, Quaternion.identity);
                    spawnedChunks.Add(coord, chunk);
                }
            }
        }

        // 🔥 Xoá chunk xa
        List<Vector2> chunksToRemove = new List<Vector2>();

        foreach (var chunk in spawnedChunks)
        {
            if (!neededChunks.Contains(chunk.Key))
            {
                Destroy(chunk.Value);
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var coord in chunksToRemove)
        {
            spawnedChunks.Remove(coord);
        }
    }
}