using ProductsShop.Data;
using ProductsShop.StartUp.Core;


using (var context = new ProductsShopContext())
{
    Engine engine = new Engine(context);

    engine.Run();

}

