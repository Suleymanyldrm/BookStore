using System;
using System.Collections.Generic;
using System.Text;

namespace KitapDukkani
{
    public class BaseClass
    {
        public int Id { get; set; }  
        public DateTime CreatedTime { get; set; } //oluşturulma tarihi
        public DateTime Updatetime { get; set; }  //güncelleme tarihi

        const int MIN_NUMS = 100000000;
        const int MAX_NUMS = 999999999;
        public BaseClass()
        {
            Random random = new Random();
            Id = random.Next(MIN_NUMS, MAX_NUMS);
            CreatedTime = DateTime.Now;
            Updatetime = DateTime.Now;
        }
    }
}
