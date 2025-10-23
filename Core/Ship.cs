namespace Core;

public class Ship
{
    public Hull Hull { get; set; }
    public List<Weapon> Weapons { get; set; }
}

public class Weapon
{
    public Guid ID { get; set; }
    public int DamageDice { get; set; }
    public bool Destructive { get; set; }
}

public class Hull
{
    public int Points { get; set; }
    public Armour Armour { get; set; }
}

public class Armour
{
    public int Points { get; set; }
}
