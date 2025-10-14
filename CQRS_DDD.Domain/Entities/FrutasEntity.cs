using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable
namespace CQRS_DDD.Domain.Entities
{
    [Table("FrutasEntity")]
    public class FrutasEntity
    {
        [Key]
	    public int FrutasEntityId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Nome { get; set; }

        [Required]
        public int Qtde { get; set; }

        [Required]
        public bool Ativa { get; set; }

        public FrutasEntity()
        #region MyRegion
        {
            Nome = string.Empty;
            Qtde = 0;
            Ativa = true;
        } 
        #endregion
    }
}
