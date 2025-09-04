namespace DndCharacterSimulator.Models
{
    internal class StatLine
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public StatLine(int[] statArray)
        {
            if(statArray.Length != 6)
            {
                throw new Exception("Unable to initialize statline. Array size missmatch");
            }

            Strength = statArray[0];
            Dexterity = statArray[1];
            Constitution = statArray[2];
            Intelligence = statArray[3];
            Wisdom = statArray[4];
            Charisma = statArray[5];
        }

        public int[] GetStatLineArray()
        {
            return new int[] { Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma };
    }
}
