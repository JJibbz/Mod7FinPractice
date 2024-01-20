using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace Mod7FinPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Customer customer1 = new Customer("Zbyshek", "Customer's address1");
            Customer customer2 = new Customer("Svinoslav", "Customer's address2");
            Shop Shop1 = new Shop("Shop1", "ShopAddress1", true);
            Shop Shop2 = new Shop("Shop2", "ShopAddress2", false);
            PickPoint pickPoint1 = new PickPoint("PickPoint1", "PickPointAddress1");
            PickPoint pickPoint2 = new PickPoint("PickPoint2", "PickPointAddres2");
            RussianPost russianPost1 = new RussianPost("Fast");
            RussianPost russianPost2 = new RussianPost("Slow");
            Blitzz blitz1 = new Blitzz("BLITZKRIEG");
            Blitzz blitz2 = new Blitzz("SLOWKRIEG");
            Product product1 = new Product("Phone", 16550.99);
            Product product2 = new Product("Watch", 7565.50);
            Product product3 = new Product("T-shirt", 1550.99);
            Product product4 = new Product("Book", 750.50);
            HomeDelivery homeDelivery1 = new HomeDelivery(customer1.Name, russianPost1,customer1.Address);
            HomeDelivery homeDelivery2 = new HomeDelivery(customer2.Name, russianPost2,customer2.Address);
            PickPointDelivery pickPointDelivery1 = new PickPointDelivery(customer1.Name, blitz1, pickPoint1.PickPointAddress);
            PickPointDelivery pickPointDelivery2 = new PickPointDelivery(customer2.Name, blitz2, pickPoint2.PickPointAddress);
            ShopDelivery shopDelivery1 = new ShopDelivery(customer1.Name, Shop1.ShopName, Shop1.ShopAddress);
            ShopDelivery shopDelivery2 = new ShopDelivery(customer1.Name, Shop2.ShopName, Shop2.ShopAddress);
            Order<HomeDelivery, string> order1 = new Order<HomeDelivery, string>("D-114", homeDelivery1, customer1.Address, product1, russianPost1);
            Order<HomeDelivery, int> order2 = new Order<HomeDelivery, int>(1354, homeDelivery2, customer2.Address, product2, russianPost2);
            Order<PickPointDelivery, string> order3 = new Order<PickPointDelivery, string>("P13A446", pickPointDelivery1 ,pickPointDelivery1.Address,product3,blitz1);
            Order<PickPointDelivery, int> order4 = new Order<PickPointDelivery,int>(1516341,pickPointDelivery2, pickPointDelivery2.Address,product4,blitz2);
            Order<ShopDelivery, string> order5 = new Order<ShopDelivery, string>("N341M-15DF", shopDelivery1, Shop1.ShopAddress, product1);
            Order<ShopDelivery, int> order6 = new Order<ShopDelivery, int>(16743224,shopDelivery2,Shop2.ShopAddress, product2);

            order1.DisplayOrderInfo();
            Console.ReadKey();
            order2.DisplayOrderInfo();
            Console.ReadKey();
            order3.DisplayOrderInfo();
            Console.ReadKey();
            order4.DisplayOrderInfo();
            Console.ReadKey();
            order5.DisplayOrderInfo();
            Console.ReadKey();
            order6.DisplayOrderInfo();
            Console.ReadKey();
        }
    }
    class Order<TDelivery, TNumb> where TDelivery : Delivery
    {
        public TNumb Id;
        public TDelivery Delivery;
        public string DeliveryAddress;
        public Product Product;
        public CourierService CourierService;

        public Order(TNumb Id, TDelivery Delivery, string DeliveryAddress, Product Product, CourierService CourierService = null)
        {
            this.Id = Id;
            this.Delivery = Delivery;
            this.DeliveryAddress = DeliveryAddress;
            this.Product = Product; 
            this.CourierService = CourierService;
        }

        public void DisplayOrderInfo()
        {
            Console.WriteLine($"Номер заказа:{Id}");
            if (Delivery is HomeDelivery)
            {
                Console.WriteLine("Доставка на дом");
            }
            if (Delivery is PickPointDelivery)
            {
                Console.WriteLine("Доставка в пункт выдачи");
            }
            if (Delivery is ShopDelivery)
            {
                Console.WriteLine("Доставка в магазин");
            }
            Console.WriteLine($"Адресс доставки:{Delivery.Address}");
            Console.WriteLine($"Заказанный товар:{Product.Name}");
            Console.WriteLine($"Стоимость товара:{Product.Price}");
            if (CourierService != null)
            {
                Console.WriteLine($"Доставка осуществляется компанией:{CourierService.Name}");
                Console.WriteLine($"Стоимость доставки:{CourierService.CostOfDelivery}");
            }
        }
    }
    class Product
    {
        public string Name;
        public double Price;

        public Product(string Name, double Price) 
        { 
            this.Name = Name;
            this.Price = Price;
        }
    }
    
    abstract class CourierService
    {
        public string Name;
        public double CostOfDelivery;
        
        public abstract double PickDeliveryType(string DeliveryUrg);
    }

    class RussianPost : CourierService
    {
        public string DeliveryUrgency;
        

        public RussianPost(string DeliveryUrgency)
        {
            Name = "RussianPost";
            this.DeliveryUrgency = DeliveryUrgency;
            CostOfDelivery = PickDeliveryType(DeliveryUrgency);
        }

        public override double PickDeliveryType(string DeliveryUrg)
        {
            if (DeliveryUrgency == "Fast")
            {
                CostOfDelivery = 792.99;
                return CostOfDelivery;
            }
            if (DeliveryUrgency == "Slow")
            {
                CostOfDelivery = 350.50;
                return CostOfDelivery;
            }
            else
            {
                Console.WriteLine("Не выбрана скорость доставки");
                return CostOfDelivery = 0;
            }
        }
    }

    class Blitzz : CourierService 
    { 
        public string DeliveryUrgency;

        public Blitzz(string DeliveryUrgency)
        {
            Name = "Blitzz";
            this.DeliveryUrgency = DeliveryUrgency;
            CostOfDelivery = PickDeliveryType(DeliveryUrgency);
        }

        public override double PickDeliveryType(string DeliveryUrg)
        {
            if (DeliveryUrgency == "BLITZKRIEG")
            {
                return CostOfDelivery = 999.99;
            }
            if (DeliveryUrgency == "SLOWKRIEG")
            {
                return CostOfDelivery = 599.99;
            }
            else 
            {
                Console.WriteLine("Не выбрана скорость доставки");
                return CostOfDelivery = 0; 
            }
        }
    }

    class Customer
    {
        public string Name;
        public string Address;

        public Customer(string Name, string Address)
        {
            this.Name = Name;
            this.Address = Address;
        }
    }

    abstract class Delivery 
    {
        public string Address;
        public Delivery(string Address)
        {
            this.Address = Address;
        }
    }

    class HomeDelivery : Delivery
    {
        public string clientName;
        public CourierService CourierService;

        public HomeDelivery(string clientName, CourierService CourierService, string address) : base(address)
        {
            
            this.clientName = clientName;
            this.CourierService = CourierService;
        }
    }

    class PickPointDelivery : Delivery
    {
        public string clientName;
        public CourierService CourierService;

        public PickPointDelivery(string clientName, CourierService CourierService, string address) : base (address)
        {
            this.clientName = clientName;
            this.CourierService = CourierService;
        }
    }

    class ShopDelivery : Delivery
    {
        public string clientName;
        public string shopName;

        public ShopDelivery(string clientName, string shopName, string Address) : base (Address)
        {
            this.clientName = clientName;
            this.shopName = shopName;
        }
    }

    class Shop
    {
        public string ShopName;
        public string ShopAddress;
        public bool IsInStock;

        public Shop (string ShopName, string ShopAddress, bool IsInStock)
        {
            this.ShopName = ShopName;
            this.ShopAddress = ShopAddress;
            this.IsInStock = IsInStock;
        }

        public void CheckStock()
        {
            if (IsInStock == false)
            {
                Console.WriteLine("Товара нет в наличии в магазине, ожидайте доставку в ближайшие 2-3 дня.");
            }
            else
            {
                Console.WriteLine("Товар в наличии, можно получить в рабочее время. Магазин работает без выходных с 9:00 до 20:00");
            }
        }
    }
    
    class PickPoint
    {
        
        public string PickPointName;
        public string PickPointAddress;

        public PickPoint(string PickPointName, string PickPointAddress)
        {
            this.PickPointName = PickPointName;
            this.PickPointAddress = PickPointAddress;
        }
    }
}
