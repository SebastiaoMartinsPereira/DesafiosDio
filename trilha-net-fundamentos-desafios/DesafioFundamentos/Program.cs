using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using DesafioFundamentos.Models;
using System.Text.Json;


internal class Program
{
    private static SystacioneConfig systacione;

    private static void Main(string[] args)
    {

            var dias = int.Parse(Console.ReadLine());
            var anos = dias / 365    ;
            dias = dias % 365     ;
            var meses = dias / 30     ;
            dias = dias % 12    ;
            
            Console.WriteLine($"{anos} ano(s)");
            Console.WriteLine($"{meses} mes(es)");
            Console.WriteLine($"{dias} dia(s)");


        systacione = ObterConfiguracoes();

        Console.WriteLine("Seja bem vindo ao sistema de estacionamento!");
        Console.WriteLine("##########################################################################");
        Console.WriteLine("##############               Configurações atuais                   ######");
        Console.WriteLine("##########################################################################");

        Console.WriteLine($"\n\n\n{nameof(systacione.Configuracoes.PrecoInicial)}: {systacione.Configuracoes.PrecoInicial:c}");
        Console.WriteLine($"{nameof(systacione.Configuracoes.PrecoPorHora)}: {systacione.Configuracoes.PrecoPorHora:c}\n");

        ConfigurarPrecoInicial();
        ConfigurarPrecoPorHora();

        // Instancia a classe Estacionamento, já com os valores obtidos anteriormente
        Estacionamento es = new Estacionamento(systacione.Configuracoes.PrecoInicial, systacione.Configuracoes.PrecoPorHora);

        string opcao = string.Empty;
        bool exibirMenu = true;

        // Realiza o loop do menu
        while (exibirMenu)
        {
            if (!Console.IsOutputRedirected)
                Console.Clear();

            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Cadastrar veículo");
            Console.WriteLine("2 - Remover veículo");
            Console.WriteLine("3 - Listar veículos");
            Console.WriteLine("4 - Encerrar");

            try
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        es.AdicionarVeiculo();
                        break;

                    case "2":
                        es.RemoverVeiculo();
                        break;

                    case "3":
                        es.ListarVeiculos();
                        break;

                    case "4":
                        exibirMenu = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            }
            catch (System.Exception)
            {
               Console.WriteLine("Ops!");
               continue;
            }

            Console.WriteLine("Pressione uma tecla para continuar");
            Console.ReadLine();
        }

        Console.WriteLine("O programa se encerrou");
    }

    private static void ConfigurarPrecoInicial()
    {
        Console.WriteLine("\nDeseja manter o preço inicial com o valor atual?/n Digite 1=>Sim, 2=>Não:");
        if (!ObterRespostaBoleana())
        {
            Console.WriteLine("Digite o novo valor Inicial:");
            if (decimal.TryParse(Console.ReadLine(), out var precoInicial))
            {
                systacione.Configuracoes.DefinirPrecoInicial(precoInicial);
            };
        }

        Console.WriteLine($"O valor Inicial definido foi : {systacione.Configuracoes.PrecoInicial:c}\n\n");
        AtualizarArquivoConfiguracao(systacione);
    }

    private static void ConfigurarPrecoPorHora()
    {
        Console.WriteLine("Agora informe o preço por hora:");
        Console.WriteLine("\nDeseja manter o preço por hora com o valor atual?/n Digite 1=>Sim, 2=>Não:");
        if (!ObterRespostaBoleana())
        {
            Console.WriteLine("Digite o novo valor Inicial:");
            if (decimal.TryParse(Console.ReadLine(), out var novoValor))
            {
                systacione.Configuracoes.DefinirPrecoPorHora(novoValor);
            };
        }

        Console.WriteLine($"O Preço por Hora definido foi : {systacione.Configuracoes.PrecoPorHora:c}\n\n");
        AtualizarArquivoConfiguracao(systacione);
    }

    private static bool ObterRespostaBoleana()
    {
        bool respostaBoleana = true;
        if ((sbyte.TryParse(Console.ReadLine(), out sbyte respostaObtida) || respostaObtida != 1) && respostaObtida != 1)
            respostaBoleana = false;
        return respostaBoleana;
    }

    /// <summary>
    /// Obter Configurações já existentes
    /// </summary>
    /// <returns></returns>
    private static SystacioneConfig ObterConfiguracoes()
    {
        // Coloca o encoding para UTF8 para exibir acentuação
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var configuracoes = GetBuilderConfiguration()
                            .GetSection(nameof(Configuracoes))
                            .Get<Configuracoes>();

        return new(configuracoes);
    }

    private static IConfiguration GetBuilderConfiguration()
    {
        var appDirectory = $"{Environment.GetEnvironmentVariable("APPDATA")}\\Systacione";

        FileInfo fileInfo = null;

        var directoryInfo = new DirectoryInfo(appDirectory);

        if (directoryInfo.Exists)
        {
            Console.WriteLine($"Olha só:{appDirectory}");
        }
        else
        {
            directoryInfo.Create();
        }

        fileInfo = new FileInfo($"{directoryInfo.FullName}/config.json");

        if (!fileInfo.Exists)
        {
            fileInfo.Create().Close();
            var sysConfig = new SystacioneConfig(new Configuracoes((decimal)1.0, (decimal)1.0, fileInfo.DirectoryName));
            AtualizarArquivoConfiguracao(sysConfig);
        }

        try
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Environment.GetEnvironmentVariable("APPDATA"));

            builder.AddJsonFile(fileInfo.FullName, optional: false, true);
            IConfiguration config = builder.Build();
            return config;
        }
        catch (System.Exception)
        {
            new FileInfo($"{directoryInfo.FullName}/config.json").Delete();
            Console.WriteLine("Ocorreram problemas ao configurar a aplicação");
            Environment.Exit(0);
            throw;
        }
    }

    private static void AtualizarArquivoConfiguracao(SystacioneConfig sistacioneConfig)
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(sistacioneConfig, typeof(SystacioneConfig), serializeOptions);
        File.WriteAllText(@$"{sistacioneConfig.Configuracoes.LocalArquivoConfiguracao}\config.json", json);
    }
}