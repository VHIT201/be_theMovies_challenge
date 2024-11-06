using Common.Domains.Entities;
using Dapper.Contrib.Extensions; //tạo guid
using Domain.Enums;
using System; //trạng thái trong thư mục enums

namespace Domain.Entities
{
    [Table("favoriteFilm")]
    public class favoriteFilm : SystemLogEntity<Guid>
    {
        public Guid userid { get; set; }
        public string movieid { get; set; }
        public string media_type { get; set; }
            
    }
}