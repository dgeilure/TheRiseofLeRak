public class CharacterStats
{
    //base class: value of health and mana + setters and getters

    //to do maybe rename to character stats instead of logic?
    //author: daniela

    protected int characterHealth;

    protected int characterMana;

    public CharacterStats(int health = 100, int mana = 100)
    {
        characterHealth = health;
        characterMana = mana;
    }

    public int getHealth()
    {
        return characterHealth;
    }

    public int getMana()
    {
        return characterMana;
    }

    public void setHealth(int newValue)
    {
        if (newValue >= 0)
        {
            characterHealth = newValue;
        }
    }

    public void setMana(int newValue)
    {
        if (newValue >= 0)
        {
            characterMana = newValue;
        }
    }
}
