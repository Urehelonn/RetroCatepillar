using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {
    
    public GameObject food;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    List<GameObject> foods = new List<GameObject>();
    int counter;

    void Start () {
        counter = 0;

        // add a simple object pool
		for(int i = 0; i < 10; i++)
        {
            foods.Add(Instantiate(food));
            foods[i].SetActive(false);
        }

        // Spawn food every 4 seconds, starting in 3
        InvokeRepeating("Spawn", 3, 4);
	}	

    // Spawn a piece of food
    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        // Instantiate the food at (x, y)
        foods[counter].SetActive(true);
        foods[counter].transform.position = new Vector2(x, y);
        counter++;
        if (counter == 9) counter = 0;
    }
}
