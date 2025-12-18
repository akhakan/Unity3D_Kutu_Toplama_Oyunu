using UnityEngine;

public class CollectibleBox : MonoBehaviour
{
    public int scoreValue;
    public Color boxColor;

    void Start()
    {
        GetComponent<Renderer>().material.color = boxColor;
    }
}
