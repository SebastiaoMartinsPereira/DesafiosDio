using System.Text.Json.Serialization;

namespace DesafioFundamentos.Models
{

    public class Configuracoes
    {

        public string LocalArquivoConfiguracao{get; private set;}
        private decimal precoInicial;
        private decimal precoPorHora;

        public Configuracoes(decimal precoInicial, decimal precoPorHora,string localArquivoConfiguracao)
        {
            if (precoInicial <= 0) throw new ArgumentException($"'{nameof(precoInicial)}' não pode ser menor que {0,1:c}.", nameof(precoInicial));
            if (precoPorHora <= 0) throw new ArgumentException($"'{nameof(precoPorHora)}' não pode ser menor que {0,1:c}.", nameof(precoPorHora));
            if (string.IsNullOrEmpty(localArquivoConfiguracao))
            {
                throw new ArgumentException($"'{nameof(localArquivoConfiguracao)}' cannot be null or empty.", nameof(localArquivoConfiguracao));
            }

            PrecoInicial = precoInicial;
            PrecoPorHora = precoPorHora;
            LocalArquivoConfiguracao = localArquivoConfiguracao;
        }

        public decimal PrecoInicial
        {
            get => precoInicial;
            private set => precoInicial = value;
        }

        public void DefinirPrecoInicial(decimal novoPrecoInicial){

            if(novoPrecoInicial<=0) throw new ArgumentException($"O Valor informado é inválido, informe um valor maior que {0:c}");
            this.precoInicial = novoPrecoInicial;
        }

        public decimal PrecoPorHora { get => precoPorHora; private set => precoPorHora = value; }
       public void DefinirPrecoPorHora (decimal novoPrecoPorHora){

            if(novoPrecoPorHora<=0) throw new ArgumentException($"O Valor informado é inválido, informe um valor maior que {0:c}");
            this.precoPorHora = novoPrecoPorHora;
        }

    }
}