using UnityEngine;

public class ParallazBG : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private float movementSpeed = 1f;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(movementSpeed * Time.deltaTime, 0);
    }
}
