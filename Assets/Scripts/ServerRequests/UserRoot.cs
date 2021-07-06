using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


[System.Serializable]
public class UserRoot
{
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("exp")]
        public string Exp { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
    [System.Serializable]
    public class User
    {
        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        //[JsonProperty("myChallenges")]
        //public MyChallenges MyChallenges { get; set; }

        [JsonProperty("paid")]
        public bool Paid { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("requestChallenge")]
        public object RequestChallenge { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
    //[System.Serializable]
    //public class MyChallenges
    //{
    //    [JsonProperty("template1")]
    //    public Template1 Template1 { get; set; }

    //    [JsonProperty("template2")]
    //    public Template2 Template2 { get; set; }

    //    [JsonProperty("template3")]
    //    public Template3 Template3 { get; set; }

    //    [JsonProperty("template4")]
    //    public Template4 Template4 { get; set; }

    //    [JsonProperty("template5")]
    //    public Template5 Template5 { get; set; }

    //    [JsonProperty("template6")]
    //    public Template6 Template6 { get; set; }
    //}
    //[System.Serializable]
    //public class Template1
    //{
    //    [JsonProperty("day")]
    //    public int Day { get; set; }

    //    [JsonProperty("invite")]
    //    public string Invite { get; set; }

    //    [JsonProperty("language")]
    //    public string Language { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("numOfUsers")]
    //    public int NumOfUsers { get; set; }

    //    [JsonProperty("score")]
    //    public int Score { get; set; }
    //}
    //[System.Serializable]
    //public class Template2
    //{
    //    [JsonProperty("day")]
    //    public int Day { get; set; }

    //    [JsonProperty("invite")]
    //    public string Invite { get; set; }

    //    [JsonProperty("language")]
    //    public string Language { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("numOfUsers")]
    //    public int NumOfUsers { get; set; }

    //    [JsonProperty("score")]
    //    public int Score { get; set; }
    //}
    //[System.Serializable]
    //public class Template3
    //{
    //    [JsonProperty("day")]
    //    public int Day { get; set; }

    //    [JsonProperty("invite")]
    //    public string Invite { get; set; }

    //    [JsonProperty("language")]
    //    public string Language { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("numOfUsers")]
    //    public int NumOfUsers { get; set; }

    //    [JsonProperty("score")]
    //    public int Score { get; set; }
    //}
    //[System.Serializable]
    //public class Template4
    //{
    //    [JsonProperty("day")]
    //    public int Day { get; set; }

    //    [JsonProperty("invite")]
    //    public string Invite { get; set; }

    //    [JsonProperty("language")]
    //    public string Language { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("numOfUsers")]
    //    public int NumOfUsers { get; set; }

    //    [JsonProperty("score")]
    //    public int Score { get; set; }
    //}
    //[System.Serializable]
    //public class Template5
    //{
    //    [JsonProperty("day")]
    //    public int Day { get; set; }

    //    [JsonProperty("invite")]
    //    public string Invite { get; set; }

    //    [JsonProperty("language")]
    //    public string Language { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("numOfUsers")]
    //    public int NumOfUsers { get; set; }

    //    [JsonProperty("score")]
    //    public int Score { get; set; }
    //}
    //[System.Serializable]
    //public class Template6
    //{
    //    [JsonProperty("day")]
    //    public int Day { get; set; }

    //    [JsonProperty("invite")]
    //    public string Invite { get; set; }

    //    [JsonProperty("language")]
    //    public string Language { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("numOfUsers")]
    //    public int NumOfUsers { get; set; }

    //    [JsonProperty("score")]
    //    public int Score { get; set; }
    //}

