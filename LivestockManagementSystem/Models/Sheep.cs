using SQLite;

namespace LivestockManagementSystem.Models
{
    [Table("Sheep")]
    public class Sheep : Animal
    {
        public float Wool { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"{Wool}";
        }
    }
}
