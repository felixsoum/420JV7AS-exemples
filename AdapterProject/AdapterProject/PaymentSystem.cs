using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterProject
{
    class PaymentSystem
    {
        public void Pay2Win(float usDollar)
        {
            Item gift = Item.Common;
            if (usDollar >= 100)
            {
                gift = Item.Legendary;
            }
            else if (usDollar >= 20)
            {
                gift = Item.Rare;
            }
            Console.WriteLine($"Created a {gift} gift.");
        }
    }
}
