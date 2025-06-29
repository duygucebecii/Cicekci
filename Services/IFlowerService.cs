using Cicekci.Models;

namespace Cicekci.Services
{
    public interface IFlowerService
    {
        List<Flower> GetAll();
        Flower GetById(int id);
    }
}
