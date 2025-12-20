using UnityEngine;

public class CollectibleBox : MonoBehaviour
{
    public int ScoreValue { get; set; }      // Kutunun puan deðeri
    public Color BoxColor { get; set; }      // Kutunun rengi

    void Start()
    {
        // Kutunun rengini ata
        GetComponent<Renderer>().material.color = BoxColor;
    }
}
