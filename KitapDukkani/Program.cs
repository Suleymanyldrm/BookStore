using System;
using System.IO;
using System.Text.RegularExpressions;

namespace KitapDukkani
{
    class Program
    {
        /// <summary>
        /// /// Kitap Mağazası Uygulaması
        ///
        /// Kitap, Kasa
        ///
        /// 1 - kitap kayıt edebilmeyi
        ///     -- kayıt esnasında kitap adi, adedi, maliyet fiyati, vergisi, kazanç miktari vs.
        ///     -- ürün fiyati maliyet fiyati, vergi ve kazanç miktarina bağlı olarak hesaplanır
        /// 2 - kitap silebilme
        ///     -- kita silme fonksiyonu seçilirse girilen adet kadar kitap silinecektir
        /// 3 - kitap güncelleme
        /// 4 - kitap satış
        ///     -- satılan kitap fiyatı kasaya gelir olarak giriş yapılır
        ///     -- satılan kitap kitap listemden eksiltilir
        /// 5 - kitap listesi
        /// 6 - kitap listesinden arama kabiliyeti
        ///
        ///  Kitap -> id, adi, tür'ü (enum kullanacağız), maliyet fiyati, kazanç yüzdesi, toplam vergi, stok adedi, kayit tarihi, güncelleme tarihi
        ///  Kasa işlemi -> id, tür (gelir , gider (enum kullanacağım)), tutar, kayit tarihi, güncelleme tarihi
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int secim = 0;
            while (secim != 9)
            {
                Console.WriteLine("1 - Kitap Ekle");
                Console.WriteLine("2 - Kitap Silme");
                Console.WriteLine("3 - Kitap Güncelleme");
                Console.WriteLine("4 - Kitap Satış");
                Console.WriteLine("5 - Kitap Listeleme");
                Console.WriteLine("6 - Kitap Ara");
                Console.WriteLine("7 - Kasa  Hareketlerini Sırala");
                Console.WriteLine("8 - Dosyalama İşlemleri");
                Console.WriteLine("9 - Çıkış");
                Console.Write("İşlem Numarası Giriniz : ");
                secim = Convert.ToInt32(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        KitapEkle();
                        break;
                    case 2:
                        KitapSil();
                        break;
                    case 3:
                        KitapGuncelle();
                        break;
                    case 4:
                        KitapSatis();
                        break;
                    case 5:
                        KitapListele();
                        break;
                    case 6:
                        KitapAra();
                        break;
                    case 7:
                        kasaHaraketleriniListele();
                        break;
                    case 8:
                        dosyalamaIşlemleri();
                        break;
                    case 9:
                        Cikis();
                        break;
                    default:
                        break;
                }
            }

        }

        public static void KitapEkle()
        {
            //Kitap Ekleme
            //adı, maliyet fiyatı, türü , tax , kazanç miktarı , adet
            Console.Write("\tKitap Adı : ");
            string bookName = Console.ReadLine();
            sayiMi_for(bookName);

            Console.Write("\tMaliyeti : ");
            double costPrice = Convert.ToDouble(Console.ReadLine());

            Console.Write("\tTürü : 0 -> DİĞER , 1 -> ROMAN , 2 -> EĞİTİM , 3 -> TARİH , 4 -> POLİTİK : ");
            BookTypeEnums bookType = 0;
            int control = 5;
            while (!(control >= 0 && control < 5))
            {
                try
                {
                    control = Convert.ToInt32(Console.ReadLine());
                    if (control >= 0 && control < 5)
                    {
                        bookType = (BookTypeEnums)control;
                    }
                    else
                    {
                        while (!(control >= 0 && control < 5))
                        {
                            Console.Write("\tLütfen değeri tekrar giriniz : ");
                            control = Convert.ToInt32(Console.ReadLine());
                            bookType = (BookTypeEnums)control;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.Write("\tVergi Oranı : ");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.Write("\tKazanç Oranı : ");
            int profitMargin = Convert.ToInt32(Console.ReadLine());

            Console.Write("\tAdedi Gir : ");
            int qty = Convert.ToInt32(Console.ReadLine());

            Book newBook = new Book(bookName, costPrice, bookType, tax, profitMargin, qty);
            Book.addBook(newBook);

        }
        public static void KitapSil()
        {
            KitapListele();
            Console.Write("Silmek istediğiniz kitabın ID'sini giriniz : ");
            int deleteID = Convert.ToInt32(Console.ReadLine());
            Book.removeBook(deleteID);
        }
        public static void KitapGuncelle()
        {
            KitapListele();
            Console.Write("Güncellemek istediğiniz kitabın ID'sini giriniz : ");
            int updateId = Convert.ToInt32(Console.ReadLine());
            foreach (Book item in Book.BookList)
            {
                if (item.Id == updateId)
                {
                    int secim = 0;
                    while (secim != 7)
                    {
                        Console.WriteLine("\t1 -  Kitap Adı");
                        Console.WriteLine("\t2 -  Maliyet Fiyatı");
                        Console.WriteLine("\t3 -  Kitap Türü");
                        Console.WriteLine("\t4 -  Vergi Oranı");
                        Console.WriteLine("\t5 -  Kar Oranı");
                        Console.WriteLine("\t6 -  Kitap Adedi");
                        Console.WriteLine("\t7 -  Güncellemeden Çıkmak İstiyorum");
                        Console.Write("Güncellemek istediğiniz özelliği seçiniz : ");
                        secim = Convert.ToInt32(Console.ReadLine());

                        switch (secim)
                        {
                            case 1:
                                Console.Write("Yeni Kitap adı : ");
                                string newName = Console.ReadLine();
                                foreach (Book book in Book.BookList)
                                {
                                    book.Name = newName;
                                    Console.WriteLine(book.ToString());
                                    if (true)
                                        Console.WriteLine("Başarıyla Güncellendi!!!");
                                    Console.WriteLine(new string('-', 50));
                                }
                                break;

                            case 2:
                                Console.Write("Yeni Maliyet Fiyatı : ");
                                double newCostPrice = Convert.ToDouble(Console.ReadLine());
                                foreach (Book book in Book.BookList)
                                {
                                    //maliyet fiyatı güncellenince ürünün satış fiyatıda otomatik olarak güncelleniyor.
                                    //yeni satış fiyatını yeni bir değişkene atıp hesaplattım.
                                    book.CostPrice = newCostPrice;
                                    double newPrice = book.calculatePrice(newCostPrice, book.TaxPercantage, book.ProfitMargin);
                                    book.Price = newPrice;
                                    Console.WriteLine(book.ToString());
                                    if (true)
                                        Console.WriteLine("Başarıyla Güncellendi!!!");
                                    Console.WriteLine(new string('-', 50));
                                    //kasa işlemi için amount güncellemesi yaptık.
                                    foreach (CaseTransaction caseTransaction in CaseTransaction.CaseTransactionList)
                                    {
                                        //statik olduğu için sınıf adıyla çağırdım.
                                        double newAmount = CaseTransaction.calculateAmount(newCostPrice, book.Quantity);
                                        caseTransaction.Amount = newAmount;
                                    }
                                }
                                break;

                            case 3:
                                Console.Write("Yeni Kitap Türü (0 ile 5 arasında sayı): ");
                                int newBookType = Convert.ToInt32(Console.ReadLine());
                                foreach (Book book in Book.BookList)
                                {
                                    book.BookType = (BookTypeEnums)newBookType;
                                    Console.WriteLine(book.ToString());
                                    if (true)
                                        Console.WriteLine("Başarıyla Güncellendi");
                                    Console.WriteLine(new string('-', 50));
                                }
                                break;

                            case 4:
                                Console.Write("Yeni Vergi Oranı : ");
                                int newTaxPercantage = Convert.ToInt32(Console.ReadLine());
                                foreach (Book book in Book.BookList)
                                {
                                    //vergi oranı güncellenince ürünün satış fiyatıda otomatik olarak güncelleniyor.
                                    //yeni satış fiyatını yeni bir değişkene atıp hesaplattım.
                                    book.TaxPercantage = newTaxPercantage;
                                    double newPrice = book.calculatePrice(book.CostPrice, newTaxPercantage, book.ProfitMargin);
                                    book.Price = newPrice;
                                    Console.WriteLine(book.ToString());
                                    if (true)
                                        Console.WriteLine("Başarıyla Güncellendi");
                                    Console.WriteLine(new string('-', 50));
                                }
                                break;

                            case 5:
                                Console.Write("Yeni Kar Oranı : ");
                                int newProfitMargin = Convert.ToInt32(Console.ReadLine());
                                foreach (Book book in Book.BookList)
                                {
                                    book.ProfitMargin = newProfitMargin;
                                    double newPrice = book.calculatePrice(book.CostPrice, book.TaxPercantage, newProfitMargin);
                                    book.Price = newPrice;
                                    Console.WriteLine(book.ToString());
                                    if (true)
                                        Console.WriteLine("Başarıyla Güncellendi!!!");
                                    Console.WriteLine(new string('-', 50));
                                }
                                break;

                            case 6:
                                Console.Write("Yeni Kitap Adedi : ");
                                int newQuantity = Convert.ToInt32(Console.ReadLine());
                                foreach (Book book in Book.BookList)
                                {
                                    book.Quantity = newQuantity;
                                    Console.WriteLine(book.ToString());
                                    if (true)
                                        Console.WriteLine("Başarıyla Güncellendi!!!");
                                    Console.WriteLine(new string('-', 50));
                                    foreach (CaseTransaction caseTransaction in CaseTransaction.CaseTransactionList)
                                    {
                                        double newAmount = CaseTransaction.calculateAmount(book.CostPrice, newQuantity);
                                        caseTransaction.Amount = newAmount;
                                    }

                                }
                                break;
                            case 7:
                                continue;
                            default:
                                break;
                        }

                    }
                }
            }

        }
        public static void KitapSatis()
        {
            KitapListele();
            Console.Write("Satılan kitap Id'si : ");
            int satilanKitapId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Kaç adet kitap sattın : ");
            int satilanKitapAdedi = Convert.ToInt32(Console.ReadLine());
            Book.bookSale(satilanKitapId, satilanKitapAdedi);
            KitapListele();
        }
        public static void KitapListele()
        {
            Console.WriteLine("\nKİTAP LİSTESİ DOSYAMDAN GELENLER");
            Console.WriteLine(new string('-', 50));
            string dosya_yolu = @"C:\Users\SULEYMAN\OneDrive\Masaüstü\KitapDukkani\bin\Debug\netcoreapp3.1\KitapVerileri.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            Console.Write(sr.ReadToEnd());
            Console.WriteLine(new string('-', 50));
            sr.Close();
            fs.Close();
            Console.WriteLine("KİTAP LİSTEMDEKİLER");
            Console.WriteLine(new string('-', 50));
            foreach (Book item in Book.BookList)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine(new string('-', 50));
        }
        public static void KitapAra()
        {
            KitapListele();
            Console.Write("Görmek istediğiniz kitap adı : ");
            string BookName = Console.ReadLine();
            Console.WriteLine("Arattığınız Kitap");
            Console.WriteLine(new string('-', 50));

            foreach (Book item in Book.BookList)
            {
                //ToUpper() -->> büyük küçük harf uyumu
                if (item.Name.ToUpper().Contains(BookName.ToUpper()))
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
        public static void kasaHaraketleriniListele()
        {
            Console.WriteLine("KASA HAREKETLERİ LİSTESİ");
            Console.WriteLine(new string('-', 50));
            double KasaToplam = 0;
            foreach (CaseTransaction item in CaseTransaction.CaseTransactionList)
            {
                if (item.TransactionsType == TransactionsTypeEnums.EXPENSE)
                {
                    KasaToplam -= item.Amount;
                }
                else
                {
                    KasaToplam += item.Amount;
                }
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Kasa Toplam Tutarı :  {0} ", KasaToplam);
        }
        public static void dosyalamaIşlemleri()
        {
            int secim = 0;
            while (secim != 3)
            {
                Console.WriteLine("\t1 -  Kitapları dosyaya ekle ");
                Console.WriteLine("\t2 -  Kitapları dosyadan sil");
                Console.WriteLine("\t3 -  Dosya İşleminden Çıkmak İstiyorum");
                Console.Write("\nİşlem Numarası Giriniz : ");
                secim = Convert.ToInt32(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        //kitabı dosyaya yazma işlemi.
                        Console.Write("Kitapları Dosyaya Yazmak istiyor musun? (Y/N) :  ");
                        string vote = Console.ReadLine();
                        if (vote == "Y")
                        {
                            foreach (Book item in Book.BookList)
                            {
                                //Console.WriteLine(item.ToString());
                                FileStream fs = new FileStream("KitapVerileri.txt", FileMode.Append, FileAccess.Write, FileShare.Write);
                                StreamWriter sw = new StreamWriter(fs);
                                sw.WriteLine(item);
                                sw.Close();
                            }
                            Console.WriteLine("Kayıt Dosyaya Eklendi!!!");
                        }
                        else
                        {
                            Console.WriteLine("Kayıt Dosyaya Eklenmedi...");
                            break;
                        }
                        break;
                    case 2:
                        FileStream fsOne = new FileStream("KitapVerileri.txt", FileMode.Truncate);
                        StreamWriter swOne = new StreamWriter(fsOne);
                        swOne.Close();
                        Console.WriteLine("Kayıtlar Dosyadan Silindi!!!");
                        break;
                    case 3:
                        continue;
                }
            }
        }
        public static void Cikis()
        {
            Console.WriteLine("ÇIKIŞ YAPILDI....YİNE BEKLERİZ...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        public static void sayiMi_for(string a)
        {
            for (int i = 0; i >= 0; i++)
            {
                if (!char.IsDigit(a[i]))
                //Eğer karakter sayı değilse false döner
                {
                    break;
                }
                else
                {
                    while (true)
                    {
                        Console.Write("\tSayı girmeden deneyin : ");
                        a = Console.ReadLine();
                        break;
                    }
                }
            }
        }
    }
}
