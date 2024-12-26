namespace Talabt.API.Helpers
{
    public class Pagination<T>
    {public int PageSize {  get; set; }
        public int PageIndex { set; get; }  
        public int Count {  get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pagesize,int pageindex,IReadOnlyList<T> data,int count) 
        {
        PageSize = pagesize;
            PageIndex = pageindex;
            Data = data;
            Count = count;
        
               
        
        }
        
    }
}
