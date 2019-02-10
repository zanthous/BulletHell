using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;

public class BulletJsonParser : MonoBehaviour
{
    [SerializeField]
    private string config;
    // These are substituted for any refs
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
        //All bulletJson files must have a root of bulletJson
        JArray rootJsonArray = JObject.Parse(jsonText)["bulletJson"].Value<JArray>();
        // Build cache of id tokens to substitute for refs later on
        foreach ( var token in rootJsonArray)
        {
            BuildIdCache(token.Value<JObject>());
        }
        // Recursively parse each top-level token
        foreach (var token in rootJsonArray)
        {
            JObject jsonObj = token.Value<JObject>();
            foreach(var kv in jsonObj)
            {
                Parse(kv);
            }
        }
    }

    // Recursivly build cache of substitution tokens
    void BuildIdCache( JObject jsonObj )
    {
        foreach ( var keyVal in jsonObj )
        {
            // Base case for recusion
            if( !keyVal.Value.HasValues )
            {
                return;
            }
            JObject childObj = keyVal.Value.Value<JObject>();
            foreach (var childKeyVal in childObj)
            {
                if (childKeyVal.Key == "id")
                {
                    print("Setting id = " + (string)childKeyVal.Value);
                    idToToken[(string)childKeyVal.Value] = keyVal.Value;
                }
            }
            BuildIdCache(childObj);
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
                print("Parsing Fire from root");
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
        foreach (var keyVal in obj )
        {
            switch(keyVal.Key)
            {
                case "id":
                    break;
                case "direction":
                    print("Setting direction to " + (float)keyVal.Value);
                    direction = (float) keyVal.Value;
                    break;
                case "speed":
                    print("Setting speed to " + (float)keyVal.Value);
                    speed = (float) keyVal.Value;
                    break;
                case "bullet":
                    bullet = ParseBullet(keyVal);
                    break;
                case "bulletRef":
                    bullet = ParseBulletRef((string)keyVal.Value);
                    break;
                default:
                    print("ERROR: unsupported action! <" + keyVal.Key + ">");
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
        foreach (var keyVal in obj)
        {
            switch (keyVal.Key)
            {
                case "id":
                    break;
                case "direction":
                    print("Setting direction to " + (float)keyVal.Value);
                    direction = (float)keyVal.Value;
                    break;
                case "speed":
                    print("Setting speed to " + (float)keyVal.Value);
                    speed = (float)keyVal.Value;
                    break;
                case "action":
                    actions.Enqueue(ParseAction(keyVal));
                    break;
                case "actionRef":
                    actions.Enqueue(ParseActionRef((string)keyVal.Value));
                    break;
                case "bulletFile":
                    print("setting bullet ref file to " + (string)keyVal.Value);
                    bulletFile = (string)(keyVal.Value);
                    break;
                case "reflectable":
                    print("setting bullet reflectability to " + (bool)keyVal.Value);
                    reflectable = (bool)keyVal.Value;
                    break;
                default:
                    print("ERROR: unsupported action! <" + keyVal.Key + ">");
                    break;
            }
        }
        return new Bullet( bulletFile, direction, speed, actions, reflectable );
    }

    BulletAction ParseAction( KeyValuePair<string, JToken> jsonKeyValue)
    {
        Queue actions = new Queue();
        var obj = jsonKeyValue.Value.Value<JObject>();
        foreach (var keyVal in obj)
        {
            switch (keyVal.Key)
            {
                case "id":
                    break;
                case "fire":
                    actions.Enqueue(ParseFire(keyVal));
                    break;
                case "action":
                    actions.Enqueue(ParseAction(keyVal));
                    break;
                case "actionRef":
                    actions.Enqueue(ParseActionRef(keyVal.Key));
                    break;
                default:
                    print("ERROR: unsupported action! <" + keyVal.Key + ">");
                    break;
            }
        }
        return new BulletAction(actions);
    }

    Bullet ParseBulletRef( string refId )
    {
        print("Parsing bulletRef with refId = " + refId);
        return ParseBullet( new KeyValuePair<string, JToken> ( "bullet", idToToken[refId]) );
    }

    BulletAction ParseActionRef( string refId )
    {
        print("Parsing actionRef with refId = " + refId);
        return ParseAction( new KeyValuePair<string, JToken>( "action", idToToken[refId]) );
    }

    Fire ParseFireRef( string refId )
    {
        print("Parsing fireRef with refId = " + refId);
        return ParseFire( new KeyValuePair<string, JToken>("fire", idToToken[refId]) );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
