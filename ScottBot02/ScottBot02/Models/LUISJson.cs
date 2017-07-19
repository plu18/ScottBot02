﻿

using System;

namespace ScottBot02.Models
{

    [Serializable]
    public class LUISJson
    {
        public string query { get; set; }
        public Topscoringintent topScoringIntent { get; set; }
        public Intent[] intents { get; set; }
        public Entity[] entities { get; set; }
    }

    [Serializable]
    public class Topscoringintent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    [Serializable]
    public class Intent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    [Serializable]
    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }


}