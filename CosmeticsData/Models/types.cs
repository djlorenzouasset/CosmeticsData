#nullable disable
namespace CosmeticsData.Models
{
    public class Data
    {
        public string build { get; set; }
        public string previousBuild { get; set; }
        public string hash { get; set; }
        public DateTime date { get; set; }
        public DateTime lastAddition { get; set; }
        public List<Item> items { get; set; }
    }
    public class Images
    {
        public string smallIcon { get; set; }
        public string icon { get; set; }
        public string featured { get; set; }
        public Other other { get; set; }
    }

    public class Introduction
    {
        public string chapter { get; set; }
        public string season { get; set; }
        public string text { get; set; }
        public int backendValue { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Type type { get; set; }
        public Rarity rarity { get; set; }
        public object series { get; set; }
        public Set set { get; set; }
        public Introduction introduction { get; set; }
        public Images images { get; set; }
        public List<Variant> variants { get; set; }
        public object searchTags { get; set; }
        public List<string> gameplayTags { get; set; }
        public List<string> metaTags { get; set; }
        public string showcaseVideo { get; set; }
        public object dynamicPakId { get; set; }
        public object displayAssetPath { get; set; }
        public string definitionPath { get; set; }
        public string path { get; set; }
        public DateTime added { get; set; }
        public List<DateTime> shopHistory { get; set; }
        public List<string> builtInEmoteIds { get; set; }
    }

    public class Option
    {
        public string tag { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }

    public class Other
    {
        public string background { get; set; }
        public string coverart { get; set; }
    }

    public class Rarity
    {
        public string value { get; set; }
        public string displayValue { get; set; }
        public string backendValue { get; set; }
    }

    public class Root
    {
        public int status { get; set; }
        public Data data { get; set; }
    }

    public class Set
    {
        public string value { get; set; }
        public string text { get; set; }
        public string backendValue { get; set; }
    }

    public class Type
    {
        public string value { get; set; }
        public string displayValue { get; set; }
        public string backendValue { get; set; }
    }

    public class Variant
    {
        public string channel { get; set; }
        public string type { get; set; }
        public List<Option> options { get; set; }
    }
}
