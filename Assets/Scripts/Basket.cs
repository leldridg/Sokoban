using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public List<Sprite> fruits;
    public Vector3 targetPosition;
    public int speed;
    public SpriteRenderer fruit;

    void Awake()
    {
        targetPosition = transform.position;
        fruit = GetComponentsInChildren<SpriteRenderer>()[1];
        fruit.sprite = fruits[Random.Range(0, fruits.Count)];
        fruit.enabled = false;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        fruit.enabled = !fruit.enabled;
        GameManager.Instance.CheckForNextLevel();
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        Debug.Log("exit");
        fruit.enabled = !fruit.enabled;
    }
}
