using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KitapDukkani
{
    //Kasa hareketleri,İşlemleri
    public class CaseTransaction : BaseClass
    {

        public static List<CaseTransaction> CaseTransactionList = new List<CaseTransaction>();
        public double Amount { get; set; }  //tutar
        public TransactionsTypeEnums TransactionsType { get; set; }

        public CaseTransaction(double amount, TransactionsTypeEnums transactionstype)
        {
            Amount = amount;
            TransactionsType = transactionstype;
        }

        //kasa işlemi kaydetme metodu (her bir işlemi listenin içine atıyor)
        public static void saveCaseTransaction(CaseTransaction casetransaction)
        {
            CaseTransactionList.Add(casetransaction);
        }

        public static double calculateAmount(double costprice, int quantity)
        {
            return costprice * quantity;
        }

        public override string ToString()  
        {
            return String.Format("Id : {0} - Type: {1} - Amount: {2} - Created Time: {3}", Id, TransactionsType, Amount, CreatedTime);
        }

    }
}
