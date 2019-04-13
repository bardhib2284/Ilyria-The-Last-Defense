public static class FromCharacterToCharacterJson
{
    public static CharacterJson ConvertTo(Character c)
    {
        return new CharacterJson{ Current_Level = c.Level_Current, Items = c.Items, Name = c.name, Stars = (int)c.Stars};
    }
}