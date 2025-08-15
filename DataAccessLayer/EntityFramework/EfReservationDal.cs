using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfReservationDal : GenericRepository<Reservation>, IReservationDal
    {
        private readonly Context _context;

        public EfReservationDal(Context context)
        {
            _context = context;
        }

        public List<Destination> GetActiveDestinations()
        {
            return _context.Destinations
        .Where(d => d != null && d.Status) // aktif olanlar
        .OrderBy(d => d.City) // isteğe bağlı: alfabetik sırala
        .ToList();
        }

        public List<Reservation> GetListWithDestination()
        {
            return _context.Reservations
               .Include(r => r.Destination)
               .ToList();
        }
    }
}
