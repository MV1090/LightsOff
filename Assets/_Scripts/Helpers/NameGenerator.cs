using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : Singleton<NameGenerator>
{
    string[] adjectives = new string[] {"Able", "Bad", "Best", "Big", "Bland", "Blank", "Blunt", "Bold",
    "Brave", "Brief", "Bright", "Broad", "Calm", "Cheap", "Chill", "Clean",
    "Clear", "Clever", "Close", "Cold", "Cool", "Crisp", "Crude", "Cute",
    "Dark", "Dead", "Deep", "Dense", "Dim", "Dizzy", "Dull", "Eager",
    "Early", "Easy", "Empty", "Epic", "Even", "Evil", "Fair", "False",
    "Fancy", "Fast", "Fat", "Fierce", "Fine", "Firm", "Flat", "Flimsy",
    "Fond", "Frail", "Free", "Fresh", "Full", "Funny", "Fuzzy", "Giant",
    "Glad", "Glib", "Gloomy", "Glossy", "Good", "Grand", "Gray", "Great",
    "Grim", "Gross", "Happy", "Hard", "Harsh", "High", "Hollow", "Hot",
    "Huge", "Icy", "Idle", "Iffy", "Ill", "Jolly", "Juicy", "Kind", "Lame",
    "Large", "Last", "Late", "Lazy", "Light", "Lone", "Loose", "Loud",
    "Low", "Mad", "Mean", "Meek", "Mild", "Mini", "Minor", "Moist", "Muddy",
    "Mute", "Naive", "Neat", "New", "Nice", "Nimble", "Noble", "Odd",
    "Old", "Open", "Pale", "Petty", "Plain", "Plump", "Poor", "Proud",
    "Quick", "Quiet", "Rapid", "Rare", "Raw", "Ready", "Real", "Rich",
    "Rigid", "Risky", "Rough", "Round", "Sad", "Safe", "Salty", "Shady",
    "Sharp", "Short", "Shy", "Sick", "Silly", "Slim", "Slow", "Small",
    "Smart", "Smug", "Soft", "Solid", "Sore", "Stiff", "Stray", "Strong",
    "Sunny", "Sweet", "Swift", "Tame", "Tangy", "Tasty", "Tender", "Thick",
    "Thin", "Tidy", "Tight", "Tough", "True", "Ugly", "Vague", "Vast",
    "Vivid", "Warm", "Weak", "Weird", "Wet", "Wide", "Wild", "Witty",
    "Worse", "Worst", "Young" };

    string[] nouns = new string[] { "Actor", "Apple", "Award", "Bag", "Ball", "Bank", "Bar", "Base",
    "Bat", "Beach", "Bear", "Beef", "Beer", "Bell", "Bird", "Board",
    "Boat", "Body", "Bone", "Book", "Boot", "Boss", "Box", "Boy",
    "Brain", "Bread", "Breeze", "Brick", "Brush", "Bug", "Bun", "Bus",
    "Cab", "Cake", "Camp", "Car", "Card", "Cart", "Case", "Cat",
    "Chair", "Chart", "Check", "Cheese", "Chest", "Child", "City", "Class",
    "Clock", "Cloud", "Coat", "Code", "Coin", "Cook", "Couch", "Cow",
    "Crab", "Crew", "Crowd", "Cup", "Dad", "Dance", "Desk", "Dish",
    "Dog", "Door", "Dress", "Drink", "Drive", "Drop", "Duck", "Dust",
    "Ear", "Egg", "End", "Engine", "Eye", "Face", "Farm", "Field",
    "Film", "Fire", "Fish", "Flag", "Floor", "Fly", "Food", "Foot",
    "Fork", "Friend", "Frog", "Game", "Gas", "Gift", "Girl", "Glass",
    "Goat", "Gold", "Golf", "Grass", "Gun", "Hall", "Hand", "Hat",
    "Head", "Heart", "Hill", "Home", "Hook", "Horse", "Host", "House",
    "Ice", "Idea", "Ink", "Jam", "Jet", "Job", "Joke", "Judge",
    "Key", "Kid", "King", "Kite", "Knife", "Lab", "Lake", "Lamp",
    "Leaf", "Leg", "Letter", "Lid", "Light", "Line", "Lion", "Lip",
    "List", "Lock", "Log", "Love", "Luck", "Man", "Map", "Mask",
    "Meal", "Meat", "Milk", "Mind", "Mist", "Moon", "Mouse", "Mouth",
    "Mud", "Name", "Neck", "Net", "News", "Night", "Nose", "Note",
    "Nurse", "Nut", "Oil", "Oven"};   
       

    public string RandomName()
    {
        string randomName;
        
        int adjective = Random.Range(0, adjectives.Length);
        int noun = Random.Range(0, nouns.Length);
            

        randomName = adjectives[adjective] + nouns[noun];
        
        return randomName;
    }
   
}
