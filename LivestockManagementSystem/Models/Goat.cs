using SQLite;

namespace LivestockManagementSystem.Models
{
    [Table("Goat")]
    public class Goat : Animal
    {   
        public float Milk { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"{Milk}";
        }
    }
}

