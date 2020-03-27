using System;

namespace VehicleMVC.Models
{
    public class PagingDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public int CountItems { get; set; }
        private int TotalPages { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < CountTotalPages());
            }
        }
        private int CountTotalPages()
        {
            return TotalPages = (int)Math.Ceiling(CountItems / (double)PageSize);
        }

    }
}
