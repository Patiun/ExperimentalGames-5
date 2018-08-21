using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

public class QRTest : MonoBehaviour {

    public RawImage img;
    public GameObject a, b;

    private WebCamTexture camTexture;
    private Rect screenRect;
    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        if (camTexture != null)
        {
            camTexture.Play();
        }
        //Texture2D qr = generateQR("Wind");
        //img.texture = qr;
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    public void Update()
    {
        img.texture = camTexture;
    }

    void OnGUI()
    {
        // drawing the camera on screen
        //GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        
        // do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            Result result = barcodeReader.Decode(camTexture.GetPixels32(),
              camTexture.width, camTexture.height);
            if (result != null)
            {
                for (int i = 0; i < result.ResultPoints.Length; i++)
                {
                    GUI.Box(new Rect(result.ResultPoints[i].X, result.ResultPoints[i].Y, 100, 100), "This is "+i);
                }
                Debug.Log("DECODED TEXT FROM QR: " +result.Text);
                if(result.Text == "Wind")
                {
                    a.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(result.ResultPoints[0].X, result.ResultPoints[0].Y, 10));
                }
                if (result.Text == "Africa")
                {
                    b.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(result.ResultPoints[0].X,result.ResultPoints[0].Y,10));
                }
            }
        }
        catch (System.Exception ex) { Debug.LogWarning(ex.Message); }
        /*
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        Texture2D myQR = generateQR("Africa");
        if (GUI.Button(new Rect(300, 300, 256, 256), myQR, GUIStyle.none)) { }
        */
    }
}
