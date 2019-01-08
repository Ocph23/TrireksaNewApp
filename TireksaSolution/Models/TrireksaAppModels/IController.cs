using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ModelsShared.Models
{
    public interface IController<T>
    {
        IEnumerable<T> Get();
        T Get(int Id);
        IEnumerable<T> GetByParameter(string[] param);
        bool Post(T t);
        int InsertAndGetLastID(T t);
        T InsertAndGetItem(T t);

        bool Delete(int id);
        bool Put(int id,T value);
        
        T UpdateAndGetItem(T t);

        T GetLastItem();

    }
}
