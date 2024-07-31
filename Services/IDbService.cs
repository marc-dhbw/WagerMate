namespace WagerMate.Services;

public interface IDbService
{
   public bool Create<T>(string sql, object p);
   public T GetByKey<T>(string sql, int key);
   public List<T> GetAll<T>(string sql);
   public bool Delete<T>(string sql, int key);
   public bool Update<T>(string sql, object obj);
   
}