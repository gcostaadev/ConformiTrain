namespace ConformiTrain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            DataDeCriacao = DateTime.Now;
            IsDeleted = false;
        }

        public int Id { get; private set; }
        public DateTime DataDeCriacao { get; private set; }
        public bool IsDeleted { get; private set; }
        public void SetAsDeleted()
        {
            IsDeleted = true;
        }



    }
}
