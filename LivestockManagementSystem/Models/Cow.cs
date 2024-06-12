using SQLite;

namespace LivestockManagementSystem.Models
{
    [Table("Cow")]
    public class Cow : Animal
    {
        public float Milk { get; set; }
        
        public override string ToString()
        {
            return base.ToString() + $"{Milk}";
        }
    }
}
