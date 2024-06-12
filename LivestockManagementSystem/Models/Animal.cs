using SQLite;

namespace LivestockManagementSystem.Models
{
    public class Animal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public float Cost { get; set; }
        public float Weight { get; set; }
        public string? Colour { get; set; }

        public override string ToString()
        {
            return $"{Id,-5}, {Cost,-10}, {Weight,-5}, {Colour,-10}";
        }
    }
}
