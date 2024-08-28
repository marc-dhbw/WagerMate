namespace UnitTests.services;

public class ClearService
{
    private readonly IDbService _idb;

    public ClearService(IDbService idb)
    {
        _idb = idb;
    }
    
    public void ClearDatabase()
    {
        _idb.CustomSql("DELETE FROM public.winner");
        _idb.CustomSql("DELETE FROM public.userbet");
        _idb.CustomSql("DELETE FROM public.case");
        _idb.CustomSql("DELETE FROM public.user");
        _idb.CustomSql("DELETE FROM public.bet");
        _idb.CustomSql("ALTER SEQUENCE userbet_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE case_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE user_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE bet_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE winner_id_seq RESTART WITH 1");
    }
}