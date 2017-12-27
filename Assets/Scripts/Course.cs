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
            get { return nextPosition.x;  }
        } 

        //private float floorLevel;
        private readonly Vector2 origin;
        private Vector2 nextPosition;
        private List<GameObject> terrain;
        private const float despawnDistance = 100f;

        public Course(Vector2 origin, GameObject platform, GameObject obstacle)
        {
            nextPosition = origin;
            this.platform = platform;
            this.obstacle = obstacle;
            terrain = new List<GameObject>();
        }

        public void Update()
        {
            Func<GameObject, bool> outOfRange = go => nextPosition.x - go.transform.position.x > despawnDistance;
            foreach (var go in terrain.Where(outOfRange)) 
                UnityEngine.Object.DestroyObject(go);
            terrain.RemoveAll(go => nextPosition.x - go.transform.position.x > despawnDistance);
        }

        public void AddGap()
        {
            for(var i = 0; i < 2; i++)
            {
                terrain.Add(UnityEngine.Object.Instantiate(platform, nextPosition, Quaternion.identity));
                nextPosition += Vector2.right * obstacle.transform.localScale.x; 
            }

            nextPosition += Vector2.right * obstacle.transform.localScale.x * 2;
        }

        public void AddFloor()
        {
            terrain.Add(UnityEngine.Object.Instantiate(platform, nextPosition, Quaternion.identity));
            nextPosition += new Vector2(platform.transform.localScale.x, 0);
        }

        public void AddSmallWall()
        {
            terrain.Add(UnityEngine.Object.Instantiate(platform, nextPosition, Quaternion.identity)); 
            terrain.Add(UnityEngine.Object.Instantiate(obstacle, nextPosition + Vector2.up * platform.transform.localScale.y, Quaternion.identity));
            nextPosition += new Vector2(obstacle.transform.localScale.x, 0);
        }
    }
}
