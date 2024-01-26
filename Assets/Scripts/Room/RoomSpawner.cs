using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public Direction openingDirection;

    /** @enum for indicating the direction of where a Door is needed in the Room that will be spawned*/
    public enum Direction
    {
        TOP, BOTTOM, LEFT, RIGHT
    }

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        Invoke("Spawn", 0.1f) ;
    }

    void Spawn()
    {
        /** Check if @Spawnpoint has already spawner a Room, to prevent multiple Rooms on one @Spawnpoint */
        if (!spawned) {

        /** Spawn the right Room at the location of the @Spawnpoint */
            switch (openingDirection)
            {
            case Direction.TOP:
                rand = UnityEngine.Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
                break;
            case Direction.BOTTOM:
                rand = UnityEngine.Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
                break;
            case Direction.LEFT:
                rand = UnityEngine.Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
                break;
            case Direction.RIGHT:
                rand = UnityEngine.Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
                break;
            default:
                break;

            }
            spawned = true;
        }

    }

    //NULL REFERENCE
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint") && other != null)
        {
            try {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            spawned = true;
        } catch (NullReferenceException exception)
            {

            }
        }
    }

}
