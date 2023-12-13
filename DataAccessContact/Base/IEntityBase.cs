namespace DataAccess.Base
{
    public interface IEntityBase
    {

        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
