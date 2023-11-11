namespace DesafioPOO.Models
{
    public abstract class Smartphone
    {
        public string Numero { get; set; }
        public string Modelo { get; set; }
        public string IMEI { get; set; }
        public int Memoria { get; set; }

        public Smartphone(string numero, string modelo, string imei, int memoria)
        {
            if (numero is null) throw new ArgumentException($"propriedade {numero} deve ser informada");
            if (modelo is null) throw new ArgumentException($"propriedade {modelo} deve ser informada");
            if (imei is null) throw new ArgumentException($"propriedade {imei} deve ser informada");
            if (memoria is null) throw new ArgumentException($"propriedade {memoria} deve ser informada");

            Numero = numero;
            Modelo = modelo;
            IMEI = imei;
            Memoria = memoria;
        }

        public void Ligar()
        {
            Console.WriteLine("Ligando...");
        }

        public void ReceberLigacao()
        {
            Console.WriteLine("Recebendo ligação...");
        }

        public abstract void InstalarAplicativo(string nomeApp);
    }
}