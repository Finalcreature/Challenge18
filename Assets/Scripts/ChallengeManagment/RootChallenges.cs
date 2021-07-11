using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class RootChallenges
{
    [JsonProperty("user")]
    public Challenges challenges { get; set; }
}
[System.Serializable]
public class Challenges
{
    [JsonProperty("myChallenges")]
    public MyChallenges MyChallenges { get; set; }
}

[System.Serializable]
public class MyChallenges
{
   public Dictionary<JsonPropertyAttribute, Template> temps = new Dictionary<JsonPropertyAttribute, Template>();
   

    //[JsonProperty("template1")]
    //public Template template = new Template();

    //[JsonProperty("template2")]
    //public Template template2 = new Template();

    //[JsonProperty("template3")]
    //public Template template3 = new Template();

    //[JsonProperty("template4")]
    //public Template template4 = new Template();

    //[JsonProperty("template5")]
    //public Template template5 = new Template();

    //[JsonProperty("template6")]
    //public Template template6 = new Template();

    //[JsonProperty("template7")]
    //public Template template7 = new Template();

    //[JsonProperty("template8")]
    //public Template template8 = new Template();

    //[JsonProperty("template9")]
    //public Template template9 = new Template();

    //[JsonProperty("template10")]
    //public Template template10 = new Template();
}
[System.Serializable]
public class Template
{
    [JsonProperty("day")]
    public int Day { get; set; }

    [JsonProperty("invite")]
    public string Invite { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("numOfUsers")]
    public int NumOfUsers { get; set; }

    [JsonProperty("score")]
    public int Score { get; set; }
}