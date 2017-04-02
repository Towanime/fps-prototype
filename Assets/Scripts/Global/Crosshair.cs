using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Texture2D crosshairActiveTexture;
    //private GauntletController gauntlet;

    private Texture2D tx2DFlash;

    void Start()
    {
        // gauntlet = GetComponent<GauntletController>();

        tx2DFlash = new Texture2D(1, 1); //Creates 2D texture
        tx2DFlash.SetPixel(1, 1, Color.white); //Sets the 1 pixel to be white
        tx2DFlash.Apply();
    }

    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 5);
    }

    void OnGUI()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        Texture2D activeTexture;
        /*if (gauntlet.GetTargetAnalyzer() != null && gauntlet.GetTargetAnalyzer().InSight())
        {
            activeTexture = gauntlet.GetTargetAnalyzer().GetInSightTexture();
        }
        else
        {
            activeTexture = crosshairTexture;
        }*/
        /*activeTexture = crosshairTexture;
        Rect position = new Rect((Screen.width - activeTexture.width) / 2, (Screen.height - activeTexture.height) / 2, activeTexture.width, activeTexture.height);
        GUI.DrawTexture(position, activeTexture);*/

        GUI.DrawTexture(new Rect(0, 0, 10, 10), tx2DFlash);
    }
}