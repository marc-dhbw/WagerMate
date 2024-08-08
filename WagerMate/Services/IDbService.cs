namespace WagerMate.Services;

public interface IDbService
{
   public bool Create<T>(string sql, object p);
   T GetById<T>(string sql, object id);
   public List<T> GetAll<T>(string sql);
   public bool Delete<T>(string sql, object id);
   public bool Update<T>(string sql, object obj);
   T GetByEmail<T>(string sql, object email);
}