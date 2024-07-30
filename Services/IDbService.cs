﻿namespace WagerMate.Services;

public interface IDbService
{
   public T Create<T>(String sql, string p);
   public T Read<T>(String sql);
   public T Update<T>(String sql);
   public T Delete<T>(String sql);
}