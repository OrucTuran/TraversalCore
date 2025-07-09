using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{//generic disinda kullanilmasi gereken abouta ozgu farkli metotlar icin gereklidir
    public interface IAboutService : IGenericService<About> 
    {
    }
}
