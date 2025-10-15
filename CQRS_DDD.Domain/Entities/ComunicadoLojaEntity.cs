using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable
namespace CQRS_DDD.Domain.Entities
{
	[Table("CiLojaEntity")]
    public class ComunicadoLojaEntity
    {
        [Key]
	    public int CiLojaEntityId { get; set; }

        public int LojaEntityId { get; set; }

        public string CiMsg { get; set; }

        public bool Ativa { get; set; }

        public ComunicadoLojaEntity()
        #region MyRegion
        {
            this.CiLojaEntityId = 0;
            this.LojaEntityId = 0;
            this.CiMsg = string.Empty;
            this.Ativa = true;
        } 
        #endregion
    }
}
