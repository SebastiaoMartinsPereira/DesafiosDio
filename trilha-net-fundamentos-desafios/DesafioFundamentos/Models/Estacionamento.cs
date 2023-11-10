namespace DesafioFundamentos.Models
{

    public class Veiculo
    {
        public Placa Placa { get; private set; }
        public DateTime HoraEntrada { get; private set; }
        public DateTime? HoraSaida { get; private set; }

        public Veiculo(string placa)
        {
            Placa = new Placa(placa);
            if (!Placa.ValidarPlaca()) throw new ArgumentException($"O valor informado para a {nameof(placa)} é inválido!");
            HoraEntrada = DateTime.Now;
            HoraSaida = null;
        }
    }

    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<Veiculo> veiculos = new List<Veiculo>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        { 
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            try
            {
                var novoVeiculo = new Veiculo(Console.ReadLine());
                if (veiculos.Count == 0)
                {
                    veiculos.Add(novoVeiculo);
                    return;
                }

                if (veiculos.Any(v => v.Placa.Equals(novoVeiculo.Placa)))
                {
                    throw new Exception("Placa informada pertence a veículo já estacionado!");
                }
                veiculos.Add(novoVeiculo);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Problemas ocorreram ao adicionar o veiculo informado:\n {ex.Message}");
                throw;
            }


        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pedir para o usuário digitar a placa e armazenar na variável placa
            // *IMPLEMENTE AQUI*

            try
            {
                var placa = new Placa(Console.ReadLine());

                // Verifica se o veículo existe
                if (veiculos.Any(x => x.Placa.Equals(placa)))
                {
                    var veic = veiculos.Find(v=> v.Placa.Equals(placa));
                    Console.WriteLine($"O veiculo está estacionado há: {DateTime.Now.Subtract(veic.HoraEntrada).ToString()}");
                    // TODO: Realizar o seguinte cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal                
                    var precoFinal = ((decimal)DateTime.Now.Subtract(veic.HoraEntrada).TotalHours * this.precoPorHora) + this.precoInicial;
                    Console.WriteLine($"Valor a ser pago: {precoFinal:c}"); 
                    if (veiculos.Remove(veic))
                        Console.WriteLine($"O veículo {placa.Identificador} foi removido e o preço total foi de:  {precoFinal:c}");
                    else
                        Console.WriteLine("Desculpe, não foi possível remover o veicula da lista.");
                }
                else
                {
                    Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Problemas ocorreram ao adicionar o veiculo informado:\n {ex.Message}");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                var strMensagem = string.Join("\n", veiculos.Select(x => $"Placa: {x.Placa.Identificador}, Hora Entrada {x.HoraEntrada:dd/MM/yyyy HH:mm:ss}"));
                Console.WriteLine($"{strMensagem}");
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}