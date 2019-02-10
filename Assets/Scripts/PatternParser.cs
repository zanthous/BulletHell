using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class PatternParser : MonoBehaviour
{
    private string path = "/StreamingAssets/Patterns/Test.json";

    // Start is called before the first frame update
    void Start()
    {
        JsonTextReader reader = new JsonTextReader(new StringReader(File.ReadAllText(Application.dataPath + path)));
        while(reader.Read())
        {
            if(reader.Value != null)
            {
                Debug.Log(reader.TokenType);
                Debug.Log(reader.Value);
            }
            else
            {
                Debug.Log(reader.TokenType);
            }
        }
    }

}
