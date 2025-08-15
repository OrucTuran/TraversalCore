using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ReservationManager : IReservationService
    {
        private IReservationDal _reservationDal;

        public ReservationManager(IReservationDal reservationDal)
        {
            _reservationDal = reservationDal;
        }

        public void TAdd(Reservation t)
        {
            _reservationDal.Insert(t);
        }

        public void TDelete(Reservation t)
        {
            _reservationDal.Delete(t);
        }

        public List<Reservation> TGetList()
        {
            return _reservationDal.GetList();
        }

        public Reservation TGeyByID(int id)
        {
            return _reservationDal.GetByID(id);
        }

        public void TUpdate(Reservation t)
        {
            _reservationDal.Update(t);
        }

        public List<Destination> TGetActiveDestinations()
        {
            return _reservationDal.GetActiveDestinations();
        }

        public List<Reservation> TGetListWithDestination()
        {
           return _reservationDal.GetListWithDestination();
        }
    }
}
