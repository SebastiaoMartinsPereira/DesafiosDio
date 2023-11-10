using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;

namespace DesafioFundamentos.Models
{
    public class Placa {
        public Placa(string identificador)
        {
            this.Identificador = identificador;
            ValidarPlaca();
        }

        public string Identificador { get; set; }
        
        /// <summary>
        /// Validar se a plca informada está no padrão
        /// </summary>
        /// <returns></returns>
        public bool ValidarPlaca(){

            if(string.IsNullOrEmpty(Identificador)) return false;
            if(Identificador.Length< 7) return false;
            if (Identificador.Length > 8) { return false; }
            Identificador = Identificador.ToUpper().Replace("-", "").Trim();
            if(Identificador.Length!=7) return false;

            /*
             *  Verifica se o caractere da posição 4 é uma letra, se sim, aplica a validação para o formato de placa do Mercosul,
             *  senão, aplica a validação do formato de placa padrão.
             */
            if (char.IsLetter(Identificador, 4))
            {
                /*
                 *  Verifica se a placa está no formato: três letras, um número, uma letra e dois números.
                 */
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return padraoMercosul.IsMatch(Identificador);
            }
            else
            {
                // Verifica se os 3 primeiros caracteres são letras e se os 4 últimos são números.
                var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padraoNormal.IsMatch(Identificador);
            } 
        }

        public override bool Equals(object obj)
        {
            if (obj is not Placa castobj || !this.GetType().Equals(obj.GetType())) return false;
            return castobj.Identificador.Equals(this.Identificador);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
