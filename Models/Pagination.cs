using Microsoft.EntityFrameworkCore;

namespace Boat_2.Models
{
    public class Pagination<T>:List<T>
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public Pagination(List<T> items,int Count,int pageNumber,int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(Count / (double)pageSize);
            this.AddRange(items);
        }
        public bool PreviousPage{
            get
            {
                return PageNumber > 1;
            }
        }
        public bool NextPage
        {
            get
            {
                return PageNumber < TotalPages;
            }
        }
        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source,int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1)*pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(items,count, pageNumber, pageSize);
        }
    }
}
