using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static CryptoCore.Models.Crypto;

namespace CryptoCore.Models.DALModels
{
    public class CoinDAL
    {
        public CoinDAL()
        {
            EntryTime = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CoinId { get; set; }
        public string Symbol { get; set; }
        public float Price  { get; set; }
        public string Name { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
