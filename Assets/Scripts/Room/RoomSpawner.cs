using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public enum OpeningDirection { Left = 3, Right = 4, Top = 2, Bottom = 1};
    // 1 bottom
    // 2 top
    // 3 left
    // 4 right

    private RoomTemplates roomTemplates;
    private int rand;
    
    public bool spawned = false;
    public float waitTime = 4f;
    public OpeningDirection openingDirection = OpeningDirection.Bottom;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        Invoke("Spawn", 1f);
    }


    private void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == OpeningDirection.Bottom)
            {
                //spawn room with bot
                rand = Random.Range(0, roomTemplates.bottomRooms.Length);
                Instantiate(roomTemplates.bottomRooms[rand], transform.position,
                    roomTemplates.bottomRooms[rand].transform.rotation);

            }
            else if (openingDirection == OpeningDirection.Top)
            {
                //spawn room with top
                rand = Random.Range(0, roomTemplates.topRooms.Length);
                Instantiate(roomTemplates.topRooms[rand], transform.position,
                    roomTemplates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == OpeningDirection.Left)
            {
                //spawn room with left
                rand = Random.Range(0, roomTemplates.leftRooms.Length);
                Instantiate(roomTemplates.leftRooms[rand], transform.position,
                    roomTemplates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == OpeningDirection.Right)
            {
                //spawn room with right
                rand = Random.Range(0, roomTemplates.rightRooms.Length);
                Instantiate(roomTemplates.rightRooms[rand], transform.position,
                    roomTemplates.rightRooms[rand].transform.rotation);
            }


            spawned = true;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {

            if (collision.TryGetComponent<RoomSpawner>(out RoomSpawner hinge))
            {
                if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.Log("Componen not found");
            }
            spawned = true;
        }
    }
}
