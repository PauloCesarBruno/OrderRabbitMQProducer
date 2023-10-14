using Mensagem.MessageBus;
using System.ComponentModel.DataAnnotations;

namespace OrderProducerAPI.Messages
{
    public class CellConcertOrder : BaseMessage
    {

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimo 03 caracteres !"), MaxLength(50, ErrorMessage = "Máximo 50 caracteres !")]
        [Display(Name = "Nome do Aparelho Celullar")]
        public string MarcaAparelho { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Minimo 03 caracteres !"), MaxLength(30, ErrorMessage = "Máximo 30 caracteres !")]
        [Display(Name = "Modelo do Aparelho Celullar")]
        public string ModeloAparelho { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [Display(Name = "Concertado")]
        public bool Reparado { get; set; }         

        [Required(ErrorMessage = "Campo Obrigatório !")]
        [Display(Name = "Valor do Concerto")]
        [DataType(DataType.Currency)]
        public decimal ValorConserto { get; set; }
    }
}
