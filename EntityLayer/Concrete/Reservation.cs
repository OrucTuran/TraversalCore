using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public enum ReservationStatus
    {
        [Display(Name = "Beklemede")]
        Pending = 0,           // Rezervasyon beklemede

        [Display(Name = "Müşteri Aranıyor")]
        ContactingCustomer = 1, // Müşteri aranacak

        [Display(Name = "Fiyat Teklifi Verildi")]
        Quoted = 2,            // Fiyat teklifi verildi

        [Display(Name = "Onaylandı")]
        Confirmed = 3,         // Rezervasyon onaylandı

        [Display(Name = "İptal Edildi")]
        Cancelled = 4,          // Rezervasyon iptal edildi

        [Display(Name = "Geçmiş Rezervasyon")]
        Past = 5
    }

    public class Reservation
    {
        public int ReservationID { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }
        public int PersonCount { get; set; }
        public int DestinationID { get; set; }
        public Destination Destination { get; set; }
        public DateTime ReservationDate { get; set; }
        public string? Description { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
