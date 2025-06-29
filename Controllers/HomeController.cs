using Cicekci.Models;
using Cicekci.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cicekci.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        Context c = new Context();

        //yap�lacak i�lemler i�in serviceler d��ardan al�n�p controllera enjekte edildi. bu sayade controllerdaki kullan�c� istekleri yap�l�rken serviceler kullan�ld�.
        private readonly IFlowerService _flowerService; //serviceler tan�mland�.
        private readonly CartService _cartService;


        //Servisleri d��ardan al�yoruz.
        public HomeController(IFlowerService flowerService, CartService cartService)
        {
            _flowerService = flowerService; //controllera DI ile servisler enjekte ediliyor.
            _cartService = cartService;
        }


        public IActionResult Index()
        {
            var flower = _flowerService.GetAll();
            return View(flower);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var flower = _flowerService.GetById(id);
            if (flower != null)
            {
                _cartService.AddToCard(flower);
            }
            return RedirectToAction("Index");
        }

       

        public IActionResult Privacy()
        {
            var cartItems = _cartService.GetCart();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult UrunMiktariArttir(int FlowerId)
        {
            _cartService.UrunMiktariArttir(FlowerId);
            return RedirectToAction("Privacy");
        }

        [HttpGet]
        public IActionResult UrunMiktariAzalt(int FlowerId)
        {
            _cartService.UrunMiktariAzalt(FlowerId);
            return RedirectToAction("Privacy");
        }

        [HttpPost]
        public IActionResult SepettenCikar(int FlowerId)
        {
            _cartService.SepetiSil(FlowerId);
            return RedirectToAction("Privacy");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
