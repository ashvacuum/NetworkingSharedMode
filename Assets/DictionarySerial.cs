using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

public class DictionarySerial : MonoBehaviour
{
    private Dictionary<string, int> Dictionary = new Dictionary<string, int>();
    void Start()
    {
        var character = new SampleJsonCharacter()
        {
            characterLevel = 1,
            characterName = "George",
            chosenClass = CharacterClass.Warrior,
            dateCreated = DateTime.Now.AddMinutes(30)
        };

        Debug.Log(JsonConvert.SerializeObject(character));
    }

    // Update is called once per frame
    void Update()
    {
    }
}

[Serializable]
public class Enemy
{
    [JsonProperty]
    private float resistance;
    [JsonProperty]
    private float blockChance;

    public float Resistance => resistance;
    public float BlockChance => blockChance;
}

[Serializable]
public struct FreeIpApiResponse
{
    public int ipVersion;
    public string ipAddress;
    public float latitude;
    public float longitude;
    public string countryName;
    public string countryCode;
}


public enum CharacterClass {
    Warrior,
    Magician,
    Assassin
}

[Serializable]
public struct SampleJsonCharacter
{
    [JsonConverter(typeof(StringEnumConverter))]
    public CharacterClass chosenClass;
    public int characterLevel;
    public string characterName;
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime dateCreated;

}

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString("yy*MM*dd"));
    }

    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        return DateTime.ParseExact((string)reader.Value, "yy*MM*dd", null);
    }
}
