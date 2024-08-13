namespace WagerMate.Services.impl;
using WagerMate.Data;

public class WagerItemService : IWagerItemService
{
    private IDbService _service;

    public WagerItemService(IDbService service)
    {
        _service = service;
    }

    public WagerItem CreateWagerItem(WagerItem wageritem)
    {
        _service.Create<WagerItem>("INSERT INTO public.wageritem(money, amount, has_item, item) VALUES(@Money, @Amount, @Item, @ItemDescription)", wageritem);
        return wageritem;
    }

    public List<WagerItem> GetAllWagerItems()
    {
        var result = _service.GetAll<WagerItem>("SELECT * FROM public.wageritem");
        return result;
    }

    public bool UpdateWagerItem(WagerItem wageritem)
    {
        var result = _service.Update<WagerItem>("UPDATE public.wageritem SET Id=@Id, money = @Money, amount=@Amount, has_item=@Item, item = @ItemDescription WHERE wageritem.Id = @Id", wageritem);
        return result;
    }

    public WagerItem GetWagerItemById(int key)
    {
        var result = _service.GetById<WagerItem>("SELECT * FROM public.wageritem WHERE wageritem.Id = @Id",new{id = key});
        return result;
    }

    public bool DeleteWagerItem(int key)
    {
        var result = _service.Delete<WagerItem>("DELETE FROM public.wageritem WHERE wageritem.Id = @Id", new{id = key});
        return result;
    
    }
}