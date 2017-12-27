using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
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
    public float spawnDistance = 5f;
    
    private System.Random random;
    private Course course;

    private const int numStartPlatforms = 20;


    // Use this for initialization
    void Start()
    {
        random = new System.Random(); 
        course = new Course(platform.transform.position, platform, obstacle);

        for (var i = 0; i < numStartPlatforms; i++)
        {
            course.AddFloor(); 
        }
    }

    void Update()
    {
        if (course.courseEnd - player.transform.position.x < spawnDistance)
        {
            AddCourseSection();
        }
        course.Update();
    }

    void AddCourseSection()
    {
        var val = random.Next(100);
        if (val > 90)
        {
            course.AddGap();
        }
        else if (val > 80)
        {
            course.AddSmallWall();
        }
        else if (val > 75)
        {
            course.AddWall(2, 2); 
        }
        else if (val > 65)
        {
            course.AddWall(3, 1); 
        }
        else
        {
            course.AddFloor();
        }
    }
}
