using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Course
    {
        public GameObject platform;
        public GameObject obstacle;

        public float courseEnd
        {
            get { return currentPosition.x;  }
        } 

        //private float floorLevel;
        private readonly Vector2 origin;
        private Vector2 currentPosition;
        private List<GameObject> terrain;
        private const float despawnDistance = 50f;

        public Course(Vector2 origin, GameObject platform, GameObject obstacle)
        {
            currentPosition = origin;
            this.platform = platform;
            this.obstacle = obstacle;
            terrain = new List<GameObject>();
        }

        public void Update()
        {
            Func<GameObject, bool> outOfRange = go => currentPosition.x - go.transform.position.x > despawnDistance;
            foreach (var go in terrain.Where(outOfRange)) 
                UnityEngine.Object.DestroyObject(go);
            terrain.RemoveAll(go => currentPosition.x - go.transform.position.x > despawnDistance);
        }

        public void AddGap()
        {
            currentPosition += Vector2.right * 2;
        }

        public void AddFloor()
        {
            currentPosition += new Vector2(platform.transform.localScale.x, 0);
            terrain.Add(UnityEngine.Object.Instantiate(platform, currentPosition, Quaternion.identity));
        }

        public void AddSmallWall()
        {
            
        }
    }
}
