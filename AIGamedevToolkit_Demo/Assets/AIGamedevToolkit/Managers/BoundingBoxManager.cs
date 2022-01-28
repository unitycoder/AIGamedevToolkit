using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AIGamedevToolkit
{
    public class BoundingBoxManager : MonoBehaviour
    {
        public InferenceFeatureObjectDetection2D[] objectDetectors;


        // The texture used for rendering the bounding box on screen
        private Texture2D boxTex;


        // Start is called before the first frame update
        void Start()
        {
            boxTex = Texture2D.whiteTexture;
        }


        // Update is called once per frame
        void Update()
        {

        }


        public void OnGUI()
        {

            foreach (InferenceFeatureObjectDetection2D objectDetector in objectDetectors)
            {
                if (objectDetector.displayBoundingBoxes && objectDetector.active)
                {
                    foreach (InferenceFeatureObjectDetection2D.Object objectInfo in objectDetector.objectInfoArray)
                    {
                        Rect boxRect = new Rect(objectInfo.x0,
                                Screen.height - objectInfo.y0,
                                objectInfo.width,
                                objectInfo.height);

                        Rect labelRect = boxRect;
                        labelRect.y -= 30;

                        Color color = objectDetector.classList.object_classes[objectInfo.label].Item2;
                        string name = objectDetector.classList.object_classes[objectInfo.label].Item1;

                        GUIStyle style = new GUIStyle();
                        style.fontSize = (int)(Screen.width * 11e-3); ;
                        style.normal.textColor = color;

                        string labelText = $"{name}: {(objectInfo.prob * 100).ToString("0.##")}%";
                        GUI.Label(labelRect, new GUIContent(labelText), style);

                        int lineWidth = (int)(Screen.width * 1.75e-3);
                        GUI.DrawTexture(boxRect, boxTex, ScaleMode.StretchToFill,
                            true, 0, color, 3, lineWidth);
                    }
                }
            }
        }
    }
}