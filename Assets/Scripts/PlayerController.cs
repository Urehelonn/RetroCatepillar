using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public bool ate;
    public GameObject body;
    public GameObject title;

    int score;
    bool lose;
    Vector2 direction = Vector2.left;

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();

    private void Start()
    {
        score = 0;
        lose = false;

        // Move the Snake every 100ms for now
        //update to add speed later
        InvokeRepeating("Move", 0.1f, 0.2f);
        
    }

    // Update is called once per frame
    void Update () {

        Vector2 left = Vector2.left;
        left.x -= 2;
        Vector2 right = Vector2.right;
        right.x += 2;
        Vector2 up = Vector2.up;
        up.y += 2;
        Vector2 down = Vector2.down;
        down.y -= 2;

        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow) && direction != left)
        {
            direction = Vector2.right;
            direction.x += 2;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && direction != up)
        {
            direction = -Vector2.up;    // '-up' means 'down'
            direction.y -= 2;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && direction != right)
        {
            direction = -Vector2.right; // '-right' means 'left'
            direction.x -= 2;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && direction != down)
        {
            direction = Vector2.up;
            direction.y += 2;
        }

        if (lose) CancelInvoke();
    }

    void Move()
    {
        Vector2 vec = transform.position;

        //move head to new direction first
        transform.Translate(direction);
        
        //if has tail, move them
        if (tail.Count > 0)
        {
            //move the last tail to current head position
            tail[tail.Count-1].position = vec;

            // Add to front of list, remove from the back
            tail.Insert(0, tail[tail.Count - 1]);

            tail.RemoveAt(tail.Count - 1);
        }
        
        //if ate food, need to add new tail
        if (ate)
        {
            Debug.Log("add tail");
            GameObject newTail = (GameObject)Instantiate
                (body, vec, Quaternion.identity);
            //add new tail position to list
            tail.Insert(0, newTail.transform);
            ate = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.tag == "food")
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food by disable it
            coll.gameObject.SetActive(false);

            //add score
            score ++;
            title.GetComponentInChildren<Text>().text
                = "Current Score: "+score;
        }

        // Collided with Tail or Border
        else
        {
            Debug.Log(coll.name);
            // lose
            lose = true;

            title.GetComponentInChildren<Text>().text
                = "You Loser  " + score;
        }
    }
}
