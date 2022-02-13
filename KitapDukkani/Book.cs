using System;
using System.Collections.Generic;
using System.Text;

namespace KitapDukkani
{
    public class Book : BaseClass
    {
        public static List<Book> BookList = new List<Book>();
        public static List<Book> BookFileList = new List<Book>();
        public string Name { get; set; }   //kitap ismi
        public BookTypeEnums BookType { get; set; }   //kitap türü
        public double CostPrice { get; set; }   //maliyet fiyatı
        public int TaxPercantage { get; set; }  //vergi yüzdesi
        public int ProfitMargin { get; set; }  //kazanç yüzdesi
        public double Price { get; set; }    //Ürün fiyatı -->> sonradan hesaplatacağım için constructor içine yazmadım
        public int Quantity { get; set; }   //kitap adedi

        public Book(string _name, double _costprice, BookTypeEnums _bookType, int _taxpercantage, int _profitmargin, int _quantity)
        {
            Name = _name;
            CostPrice = _costprice;
            BookType = _bookType;
            TaxPercantage = _taxpercantage;
            ProfitMargin = _profitmargin;
            Quantity = _quantity;
            Price = calculatePrice(_costprice, _taxpercantage, _profitmargin);
        }

        //Kitabın satış fiyatı
        public double calculatePrice(double costprice, int taxpercantage, int profitmargin)
        {
            double taxPrice = (costprice * taxpercantage) / 100;
            double profitprice = (costprice * profitmargin) / 100;
            double price = costprice + taxPrice + profitprice;
            return price;
        }

        public static void addBook(Book book)
        {
            try
            {
                BookList.Add(book);
                BookFileList.Add(book);
                double amount = CaseTransaction.calculateAmount(book.CostPrice, book.Quantity);
                CaseTransaction caseTransaction = new CaseTransaction(amount, TransactionsTypeEnums.EXPENSE);
                CaseTransaction.saveCaseTransaction(caseTransaction);
            }
            catch (Exception e)
            {
                Console.WriteLine("Hata oluştu : " + e.Message);
            }
        }
        public static void removeBook(int id)
        {
            foreach (Book item in BookList)
            {
                if (id == item.Id)
                {
                    BookList.Remove(item);
                    Console.WriteLine("\nSilme işlemi başarılı");
                    break;
                }
            }
        }
        //Kitap satışı
        public static void bookSale(int bookId, int bookQuantity)
        {
            foreach (Book item in BookList)
            {
                if (item.Id == bookId)
                {
                    item.Quantity = item.Quantity - bookQuantity;

                    double saleAmount = CaseTransaction.calculateAmount(item.Price, bookQuantity);
                    CaseTransaction caseTransaction = new CaseTransaction(saleAmount, TransactionsTypeEnums.INCOME);
                    CaseTransaction.saveCaseTransaction(caseTransaction);

                }
            }
        }

        public override string ToString()
        {
            return String.Format("Id : {0} - Name: {1} - Type: {2} - Cost Price: {3} - Price: {4} - Quantity : {5} ", Id, Name, BookType, CostPrice, Price, Quantity);
        }

    }
}
