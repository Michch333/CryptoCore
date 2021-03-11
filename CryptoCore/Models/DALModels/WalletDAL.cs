using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Models.DALModels
{
    public class WalletDAL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EntryId { get; set; }
        public int UserID { get; set; } // TODO - Need to get this from identity?
        public string Symbol { get; set; }
        public double UserHigh { get; set; }
        public double UserLow { get; set; }
        public DateTime TimeAddedToWallet { get; set; }
    }
}
