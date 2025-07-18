﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AboutManager : IAboutService
    {
        private IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal) //_aboutDal genericteki crud metotlarini kullanabilmek icin
        {
            _aboutDal = aboutDal;
        }

        public About TGeyByID(int id)
        {
            throw new NotImplementedException();
        }

        public void TAdd(About t)
        {
           _aboutDal.Insert(t);
        }

        public void TDelete(About t)
        {
           _aboutDal.Delete(t);
        }

        public List<About> TGetList()
        {
            return _aboutDal.GetList();
        }

        public void TUpdate(About t)
        {
          _aboutDal.Update(t);
        }
    }
}
