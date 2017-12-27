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

    // Use this for initialization
    void Start()
    {
        random = new System.Random(); 
        course = new Course(platform.transform.position, platform, obstacle);
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
        else
        {
            course.AddFloor();
        }
    }
}
