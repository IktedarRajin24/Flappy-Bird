using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    private float leftEdge;
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
    }
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if(transform.position.x < leftEdge - 1.0f)
        {
            Destroy(this.gameObject);  
        }
    }
}
