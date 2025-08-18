using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewslatterManager : INewslatterService
    {
        private INewslatterDal _newslatterDal;

        public NewslatterManager(INewslatterDal newslatterDal)
        {
            _newslatterDal = newslatterDal;
        }

        public void TAdd(Newslatter t)
        {
            _newslatterDal.Insert(t);
        }

        public void TDelete(Newslatter t)
        {
            _newslatterDal.Delete(t);
        }

        public List<Newslatter> TGetList()
        {
            return _newslatterDal.GetList();
        }

        public Newslatter TGeyByID(int id)
        {
            return _newslatterDal.GetByID(id);
        }

        public void TUpdate(Newslatter t)
        {
            _newslatterDal.Update(t);
        }
    }
}
