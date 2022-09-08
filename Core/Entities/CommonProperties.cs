namespace Core.Entities
{
    public class CommonProperties
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Indica si el registro esta activo o no
        /// 1 = Activo
        /// 0 = Inactivo
        /// </summary>
        public int Status { get; set; } = 1;
    }
}
