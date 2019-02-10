using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.IO;

public class BulletJsonParser : MonoBehaviour
{
    [SerializeField]
    private string config;
    // Use this for initialization
    void Start()
    {
        if (config == "")
        {
            throw new System.ArgumentException("no input file!");
        }
        ParseFile(Application.dataPath + "/Scripts/" + config);
    }

    void ParseFile( string file )
    {
        print("Loading bullet config at " + file);
        string jsonText = File.ReadAllText(file);
        JObject o = JObject.Parse(jsonText);
        // Recursively parse each top-level token
        foreach ( var token in o )
        {
            Parse(token);
        }
    }

    void Parse( System.Collections.Generic.KeyValuePair<string, JToken> jsonKeyValue )
    {
        switch (jsonKeyValue.Key)
        {
            case "action":
                ParseAction(jsonKeyValue);
                break;
            case "bullet":
                ParseBullet(jsonKeyValue);
                break;
            case "fire":
                ParseFire(jsonKeyValue);
                break;
        }
    }

    Fire ParseFire( System.Collections.Generic.KeyValuePair<string, JToken> jsonKeyValue )
    {
        float direction = 0.0f;
        float speed = 0.0f;
        Bullet bullet = new Bullet(null, direction, speed, null);
        foreach (JToken token in jsonKeyValue.Value)
        {
            var obj = (JObject) token;
            foreach ( var o in obj )
            {
                switch(o.Key)
                {
                    case "direction":
                        print("Setting direction to " + (float)o.Value);
                        direction = (float) o.Value;
                        break;
                    case "speed":
                        print("Setting speed to " + (float)o.Value);
                        speed = (float) o.Value;
                        break;
                    case "bullet":
                        bullet = ParseBullet(o);
                        break;
                    default:
                        print("ERROR: unsupported action!");
                        break;
                }
            }
        }
        return new Fire(bullet, direction, speed);
    }

    Bullet ParseBullet( System.Collections.Generic.KeyValuePair<string, JToken> jsonKeyValue )
    {
        Queue actions = new Queue();
        float direction = 0.0f;
        float speed = 0.0f;
        string bulletRef = "";
        foreach (JToken token in jsonKeyValue.Value)
        {
            var obj = (JObject)token;
            foreach (var o in obj)
            {
                switch (o.Key)
                {
                    case "direction":
                        print("Setting direction to " + (float)o.Value);
                        direction = (float)o.Value;
                        break;
                    case "speed":
                        print("Setting speed to " + (float)o.Value);
                        speed = (float)o.Value;
                        break;
                    case "action":
                        actions.Enqueue(ParseAction(o));
                        break;
                    case "bulletRef":
                        print("setting bullet ref file to " + (string)o.Value);
                        bulletRef = (string)(o.Value);
                        break;
                    default:
                        print("ERROR: unsupported action!");
                        break;
                }
            }
        }
        return new Bullet( bulletRef, direction, speed, actions );
    }

    BulletAction ParseAction( System.Collections.Generic.KeyValuePair<string, JToken> jsonKeyValue)
    {
        Queue actions = new Queue();
        foreach (JToken token in jsonKeyValue.Value)
        {
            var obj = (JObject)token;
            foreach (var o in obj)
            {
                switch (o.Key)
                {
                    case "fire":
                        actions.Enqueue(ParseFire(o));
                        break;
                    case "action":
                        actions.Enqueue(ParseAction(o));
                        break;
                    default:
                        print("ERROR: unsupported action!");
                        break;
                }
            }
        }
        return new BulletAction(actions);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
