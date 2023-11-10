namespace DesafioFundamentos.Models
{
    public class SystacioneConfig
    {
        private Configuracoes configuracoes;

        public Configuracoes Configuracoes { get => configuracoes; private set => configuracoes = value; }

        public SystacioneConfig(Configuracoes configuracoes)
        {
            Configuracoes = configuracoes;
        }
    }
}