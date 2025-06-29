using Cicekci.Models;

namespace Cicekci.Services
{
    public class FlowerService : IFlowerService //IFlowerdaki metotları kullanmak için dışardan ekledik.
    {
        private List<Flower> _flower = new()
        {
            new Flower{Id=1,Name="Şakayık",Description="Şakayık, büyük ve gösterişli çiçekleri olan çok yıllık bir süs bitkisidir.",Price=500,ImageUrl="/images/şakayık.jpeg"},
            new Flower{Id=2,Name="Gül",Description="Gül, hoş kokulu ve dikenli bir süs çiçeğidir.",Price=600,ImageUrl="/images/gül.jpg"},
            new Flower{Id=3,Name="Lale",Description="Lale, zarif yapılı, renkli çiçekleriyle bilinen soğanlı bir süs bitkisidir.",Price=700,ImageUrl="/images/lale.jpeg"},
            new Flower{Id=4,Name="Nergis",Description="Nergis, hoş kokulu, genellikle beyaz ve sarı çiçekli soğanlı bir bitkidir.",Price=800,ImageUrl="/images/nergis.jpeg"},
            new Flower{Id=5,Name="Papatya",Description="Papatya, beyaz taç yapraklı ve sarı orta kısmı olan, sade ve hoş görünümlü bir kır çiçeğidir.",Price=450,ImageUrl="/images/papatya.jpg"}
        };
        public List<Flower> GetAll() => _flower; //tüm çiçek listesi _flowera gönderilir.
        public Flower GetById(int id) => _flower.FirstOrDefault(x => x.Id == id);
    }
}

