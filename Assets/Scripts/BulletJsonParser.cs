using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;

public class BulletJsonParser : MonoBehaviour
{
    [SerializeField]
    private string config;
    private Dictionary<string, JToken> idToToken = new Dictionary<string, JToken>();
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

    void Parse( KeyValuePair<string, JToken> jsonKeyValue )
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

    Fire ParseFire( KeyValuePair<string, JToken> jsonKeyValue )
    {
        float direction = 0.0f;
        float speed = 0.0f;
        Bullet bullet = new Bullet(null, direction, speed, null, false);
        var obj = jsonKeyValue.Value.Value<JObject>();
        foreach ( var o in obj )
        {
            switch(o.Key)
            {
                case "id":
                    print("Setting id = " + (string)o.Value);
                    idToToken[(string)o.Value] = jsonKeyValue.Value;
                    break;
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
                case "bulletRef":
                    bullet = ParseBulletRef((string)o.Value);
                    break;
                default:
                    print("ERROR: unsupported action! <" + o.Key + ">");
                    break;
            }
        }
        
        return new Fire(bullet, direction, speed);
    }

    Bullet ParseBullet( KeyValuePair<string, JToken> jsonKeyValue )
    {
        Queue actions = new Queue();
        float direction = 0.0f;
        float speed = 0.0f;
        string bulletFile = "";
        bool reflectable = false;
        var obj = jsonKeyValue.Value.Value<JObject>();
        foreach (var o in obj)
        {
            switch (o.Key)
            {
                case "id":
                    print("Setting id = " + (string)o.Value);
                    idToToken[(string)o.Value] = jsonKeyValue.Value;
                    break;
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
                case "actionRef":
                    actions.Enqueue(ParseActionRef((string)o.Value));
                    break;
                case "bulletFile":
                    print("setting bullet ref file to " + (string)o.Value);
                    bulletFile = (string)(o.Value);
                    break;
                case "reflectable":
                    print("setting bullet reflectability to " + (bool)o.Value);
                    reflectable = (bool)o.Value;
                    break;
                default:
                    print("ERROR: unsupported action! <" + o.Key + ">");
                    break;
            }
        }
        return new Bullet( bulletFile, direction, speed, actions, reflectable );
    }

    BulletAction ParseAction( KeyValuePair<string, JToken> jsonKeyValue)
    {
        Queue actions = new Queue();
        var obj = jsonKeyValue.Value.Value<JObject>();
        foreach (var o in obj)
        {
            switch (o.Key)
            {
                case "id":
                    print("Setting id = " + (string)o.Value);
                    idToToken[(string)o.Value] = jsonKeyValue.Value;
                    break;
                case "fire":
                    actions.Enqueue(ParseFire(o));
                    break;
                case "action":
                    actions.Enqueue(ParseAction(o));
                    break;
                case "actionRef":
                    actions.Enqueue(ParseActionRef(o.Key));
                    break;
                default:
                    print("ERROR: unsupported action! <" + o.Key + ">");
                    break;
            }
        }
        return new BulletAction(actions);
    }

    Bullet ParseBulletRef( string refId )
    {
        return ParseBullet( new KeyValuePair<string, JToken> ( "bullet", idToToken[refId]) );
    }

    BulletAction ParseActionRef( string refId )
    {
        return ParseAction( new KeyValuePair<string, JToken>( "action", idToToken[refId]) );
    }

    Fire ParseFireRef( string refId )
    {
        return ParseFire( new KeyValuePair<string, JToken>("fire", idToToken[refId]) );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
