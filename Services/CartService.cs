using Cicekci.Models;
using System.Text.Json;

namespace Cicekci.Services
{
    public class CartService
    {
        //oturum bilgilerine erişmemizi sağlar. yani bu kullanıcıya ait sepeti oku/yaz yapabiliriz.
        public readonly IHttpContextAccessor _httpContextAccessor;

        //Sepeti saklamak için kullanılacak Session anahtarı. Tarayıcıya gizlice “Cart” adında veri yazılıyor.
        private const string CartKey = "Cart";
        public CartService(IHttpContextAccessor httpContextAccessor) //Servis oluşturulurken IHttpContextAccessor dışarıdan alınır ve sınıf içindeki _httpContextAccessor alanına atanır. Böylece session'a erişim sağlanır.
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session; //Kullanıcının oturumundaki session nesnesi alınır.
            var json = session.GetString(CartKey); //CartKey anahtarı ile daha önce kaydettiğimiz JSON string’ini alıyoruz.
            return string.IsNullOrEmpty(json) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json); //Eğer JSON varsa, Deserialize ile bu JSON’u tekrar C# nesnesine(CartItem) çeviririz.
        }

        public void AddToCard(Flower flower)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.flower_.Id == flower.Id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { flower_ = flower, Quantity = 1 });
            }
            SaveCart(cart); // Güncellenmiş sepeti Session’a kaydediyoruz.
        }

        //Kullanıcının sepetindeki ürünleri session'a JSON formatında kaydeder.
        public void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session; //Bu satırla kullanıcının oturum bilgisine erişiyoruz.Yani bu kullanıcıya ait session verilerine ulaşmamızı sağlar.
            var json = JsonSerializer.Serialize(cart); //cart nesnesi, yani ürünlerin listesi (List<CartItem>), JSON formatına çevrilir.
            session.SetString(CartKey, json);

        }

        public int SepettekiUrunSayisi()
        {
            var cartList = GetCart();
            int SepettekiUrunAdeti = cartList.Sum(x => x.Quantity);
            return SepettekiUrunAdeti;
        }
        public void UrunMiktariArttir(int FlowerID)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.flower_.Id == FlowerID);
            if (item != null)
            {
                item.Quantity++;
            }
            SaveCart(cart);
        }

        public void UrunMiktariAzalt(int FlowerID)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.flower_.Id == FlowerID);
            if (item != null)
            {
                item.Quantity--;
            }
            if (item.Quantity <= 0)
            {
                cart.Remove(item);
            }

            SaveCart(cart);
        }
    

        public void SepetiSil(int FlowerID)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.flower_.Id == FlowerID);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCart(cart);
        }
    }
}


