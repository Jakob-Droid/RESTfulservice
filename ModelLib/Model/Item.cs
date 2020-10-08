using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quality { get; set; }
        public double Quantity { get; set; }

        public Item()
        {
            
        }

        public Item(int id, string name, string quality, double quantity)
        {
            Id = id;
            Name = name;
            Quality = quality;
            Quantity = quantity;
        }


        public override string ToString()
        {
            return $"ID: {Id} NAME: {Name} QUALITY: {Quality} QUANTITY: {Quantity}";
        }
    }
}
