using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlatformGenerator : MonoBehaviour
{

    public int maxPlatforms = 30;
    public GameObject platform;
    public GameObject obstacle;
    public float horizontalMin = 6.5f;
    public float horizontalMax = 14f;
    public float verticalMin = -5f;
    public float verticalMax = 5f;
    public GameObject player;
    public float despawnDistance = 40f;

    private Vector2 originPosition;
    private Queue<GameObject> platforms;
    private Queue<GameObject> nextObstacles; 
    private System.Random random;

    // Use this for initialization
    void Start()
    {
        random = new System.Random(); 

        originPosition = platform.transform.position;
        platforms = new Queue<GameObject>();
        AddNextPlatforms();
    }

    void Update()
    {
        while (platforms.Count < maxPlatforms)
        {
            AddNextObstacles(); 
            AddPlatform();
        }

        var firstPlatform = platforms.Peek();
        if (player.transform.position.x - firstPlatform.transform.position.x > despawnDistance)
        {
            Despawn();
        }
    }

    void AddPlatform()
    {
        var newOrigin = originPosition + new Vector2(platform.transform.localScale.x, 0);
        platforms.Enqueue(Instantiate(platform, newOrigin, Quaternion.identity));
        originPosition = newOrigin;
    }

    // Update is called once per frame
    void AddNextObstacles()
    {
        var num = random.Next(100);
        Vector2 newOrigin;
        if (num > 10)
        {
            newOrigin = originPosition + new Vector2(platform.transform.localScale.x, 0);
            nextPlatforms.Enqueue(Instantiate(platform, newOrigin, Quaternion.identity));
            originPosition = newOrigin;
        }
        else
        {
            var count = 4;
            newOrigin = originPosition;
            for (var i = 0; i < count; i++)
            {
                newOrigin += new Vector2(platform.transform.localScale.x, 0);

                nextObstacles.Enqueue(Instantiate(obstacle, newOrigin + Vector2.up * 2, Quaternion.identity));
                nextPlatforms.Enqueue(Instantiate(platform, newOrigin, Quaternion.identity));
            }
        }
    }

    

    void Despawn()
    {
        var platform = platforms.Dequeue(); 
        DestroyObject(platform);
    }
}
