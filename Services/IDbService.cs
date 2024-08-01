namespace WagerMate.Services;

public interface IDbService
{
   public bool Create<T>(string sql, object p);
   T GetByKey<T>(string sql, object key);
   public List<T> GetAll<T>(string sql);
   public bool Delete<T>(string sql, int key);
   public bool Update<T>(string sql, object obj);

}