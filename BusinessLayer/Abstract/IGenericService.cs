using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T> //burada amac concrete klasorunde yapilacak businessLayer metotlarinin imzalarini tutmak
    {
        void TAdd(T t);
        void TDelete(T t);
        void TUpdate(T t);
        List<T> TGetList();
        T TGeyByID(int id);
        //List<T> TGetListByFilter(string filter);
    }
}
