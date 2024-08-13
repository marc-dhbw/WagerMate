namespace WagerMate.Services;
using WagerMate.Data;

public interface IWagerItemService
{

    /// <summary>
    /// Create a new WagerItem in the DB, given a WagerItem Object. The WagerItem ID is
    /// created by the DB and a new WagerItem Object is returned.
    /// </summary>
    /// <param name="wageritem"> WagerItem </param>
    /// <returns>new wageritem Object</returns>
    public WagerItem CreateWagerItem(WagerItem wageritem);

    /// <summary>
    /// Returns all WagerItems of the DB
    /// </summary>
    /// <returns>List of Bets</returns>
    public List<WagerItem> GetAllWagerItems();

    /// <summary>
    /// Returns the information corresponding to the WagerItem ID
    /// </summary>
    /// <param name="key">Key of the WagerItem</param>
    /// <returns>Bet</returns>
    public WagerItem GetWagerItemById(int key);
    
    /// <summary>
    /// Returns updated WagerItem
    /// </summary>
    /// <param name="wageritem">Wageritem</param>
    /// <returns></returns>
    bool UpdateWagerItem(WagerItem wageritem);

    /// <summary>
    /// Deletes a wageritem
    /// </summary>
    /// <param name="key">Key of the wageritem</param>
    /// <returns></returns>
    bool DeleteWagerItem(int key);
}