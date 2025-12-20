using UnityEngine;


public enum BoxType
{
    Red,
    Blue,
    Green,
    Yellow,
    Magenta
}

public class CollectibleBox : MonoBehaviour
{
    public int ScoreValue { get; set; }      // Kutunun puan deðeri
    public Color BoxColor { get; set; }      // Kutunun rengi
    public BoxType BoxType { get; set; }     // Kutunun rengine göre çalacak sesi deðiþtiren deðer

    private void Start()
    {
        // Kutunun rengini ata
        GetComponent<Renderer>().material.color = BoxColor;
    }
}
