using System;
using System.Collections.Generic;
using AutoCheck.ConsoleApp.Models;
using AutoCheck.ConsoleApp.Services;

namespace AutoCheck.ConsoleApp
{
    class Program
    {
        // lista central com todas as vistorias ja realizadas
        static List<Veiculo> vistorias = new List<Veiculo>();
        static MotorVistoria motor = new MotorVistoria();

        static void Main(string[] args)
        {
            string opcao = "";

            do
            {
                Console.WriteLine("");
                Console.WriteLine("===================================================================");
                Console.WriteLine("                   AUTOCHECK .NET - MOTOR DE VISTORIA              ");
                Console.WriteLine("===================================================================");
                Console.WriteLine("1 - Realizar Nova Vistoria");
                Console.WriteLine("2 - Exibir Relatório das Vistorias");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    RealizarNovaVistoria();
                }
                else if (opcao == "2")
                {
                    ExibirRelatorioDasVistorias();
                }
                else if (opcao == "0")
                {
                    Console.WriteLine("Encerrando o AutoCheck...");
                }
                else
                {
                    Console.WriteLine("Opção inválida, tente novamente.");
                }

            } while (opcao != "0");
        }

        static void RealizarNovaVistoria()
        {
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("NOVA VISTORIA");
            Console.WriteLine("-------------------------------------------------------------------");

            Console.WriteLine("Qual o tipo de veículo?");
            Console.WriteLine("1 - Carro");
            Console.WriteLine("2 - Moto");
            Console.WriteLine("3 - Caminhão");
            Console.Write("Opção: ");
            string tipo = Console.ReadLine();

            Console.Write("Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Ano: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            Console.Write("Quilometragem: ");
            double km = Convert.ToDouble(Console.ReadLine());

            Veiculo veiculo = null;

            if (tipo == "1")
            {
                Console.Write("Quantidade de portas: ");
                int portas = Convert.ToInt32(Console.ReadLine());
                veiculo = new Carro(marca, modelo, ano, km, portas);
            }
            else if (tipo == "2")
            {
                Console.Write("Cilindradas: ");
                int cilindradas = Convert.ToInt32(Console.ReadLine());
                veiculo = new Moto(marca, modelo, ano, km, cilindradas);
            }
            else if (tipo == "3")
            {
                Console.Write("Quantidade de eixos: ");
                int eixos = Convert.ToInt32(Console.ReadLine());
                Console.Write("Capacidade de carga (toneladas): ");
                double capacidade = Convert.ToDouble(Console.ReadLine());
                veiculo = new Caminhao(marca, modelo, ano, km, eixos, capacidade);
            }
            else
            {
                Console.WriteLine("Tipo inválido. Vistoria cancelada.");
                return;
            }

            List<string> checklist = veiculo.ObterChecklistObrigatorio();

            Console.WriteLine("");
            Console.WriteLine("Agora informe o status de cada item (Bom, Regular ou Ruim):");

            for (int i = 0; i < checklist.Count; i++)
            {
                string nomeItem = checklist[i];
                string status = "";
                bool statusValido = false;

                while (statusValido == false)
                {
                    Console.Write("- " + nomeItem + ": ");
                    status = Console.ReadLine();

                    if (status == "Bom" || status == "Regular" || status == "Ruim")
                    {
                        statusValido = true;
                    }
                    else
                    {
                        Console.WriteLine("  Valor inválido, digite Bom, Regular ou Ruim.");
                    }
                }

                veiculo.AdicionarItemVistoriado(nomeItem, status);
            }

            vistorias.Add(veiculo);

            Console.WriteLine("");
            Console.WriteLine("Vistoria registrada com sucesso!");
        }

        static void ExibirRelatorioDasVistorias()
        {
            Console.WriteLine("");
            Console.WriteLine("===================================================================");
            Console.WriteLine("                 RELATÓRIO DE VISTORIAS REALIZADAS                 ");
            Console.WriteLine("===================================================================");

            if (vistorias.Count == 0)
            {
                Console.WriteLine("Nenhuma vistoria realizada até o momento.");
                return;
            }

            for (int i = 0; i < vistorias.Count; i++)
            {
                Veiculo veiculo = vistorias[i];

                Console.WriteLine("");
                Console.WriteLine("[" + (i + 1) + "/" + vistorias.Count + "] VISTORIA");
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("> DADOS DO VEÍCULO:");
                Console.WriteLine("  - Tipo: " + veiculo.ObterTipoVeiculo());
                Console.WriteLine("  - Modelo: " + veiculo.Marca + " " + veiculo.Modelo);
                Console.WriteLine("  - Ano: " + veiculo.Ano + " | Quilometragem: " + veiculo.Quilometragem + " km");
                Console.WriteLine("  - Atributo Específico: " + veiculo.ObterAtributoEspecifico());

                Console.WriteLine("");
                Console.WriteLine("> AVALIAÇÃO DOS ITENS INSPECIONADOS (" + veiculo.VistoriaRealizada.Count + " ITENS):");

                foreach (ItemVistoria item in veiculo.VistoriaRealizada)
                {
                    int pontosItem = 0;
                    string marcador = "";

                    if (item.Status == "Bom")
                    {
                        pontosItem = 10;
                        marcador = "[OK]";
                    }
                    else if (item.Status == "Regular")
                    {
                        pontosItem = 5;
                        marcador = "[ ! ]";
                    }
                    else
                    {
                        pontosItem = 0;
                        marcador = "[ X ]";
                    }

                    Console.WriteLine("  " + marcador + " " + item.Nome + " - Status: " + item.Status + " (" + pontosItem + " pts)");
                }

                int pontuacaoObtida = motor.CalcularPontuacaoAtingida(veiculo);
                int pontuacaoMaxima = motor.CalcularPontuacaoMaxima(veiculo);
                double percentual = motor.CalcularPercentual(veiculo);
                string classificacao = motor.ClassificarVeiculo(percentual);

                Console.WriteLine("");
                Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
                Console.WriteLine("  - Pontuação Atingida: " + pontuacaoObtida + " de " + pontuacaoMaxima + " pontos possíveis");
                Console.WriteLine("  - Percentual de Aprovação: " + percentual.ToString("0.0") + "%");
                Console.WriteLine("  - Classificação Final: [ " + classificacao.ToUpper() + " ]");

                List<ItemVistoria> itensCriticos = motor.ObterItensCriticos(veiculo);
                List<ItemVistoria> itensAtencao = motor.ObterItensAtencao(veiculo);

                Console.WriteLine("");
                Console.WriteLine("> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");

                if (itensCriticos.Count == 0 && itensAtencao.Count == 0)
                {
                    Console.WriteLine("  Nenhuma pendência mecânica identificada. Veículo liberado para operação!");
                }
                else
                {
                    if (itensCriticos.Count > 0)
                    {
                        Console.WriteLine("  ITENS CRÍTICOS / REPROVADOS (AÇÃO IMEDIATA):");
                        foreach (ItemVistoria item in itensCriticos)
                        {
                            string recomendacao = motor.ObterRecomendacaoServico(item.Nome);
                            Console.WriteLine("     - " + item.Nome + ": " + recomendacao);
                        }
                    }

                    if (itensAtencao.Count > 0)
                    {
                        Console.WriteLine("  ITENS DE ATENÇÃO (REVISÃO PREVENTIVA):");
                        foreach (ItemVistoria item in itensAtencao)
                        {
                            string recomendacao = motor.ObterRecomendacaoServico(item.Nome);
                            Console.WriteLine("     - " + item.Nome + ": " + recomendacao);
                        }
                    }
                }

                Console.WriteLine("-------------------------------------------------------------------");
            }

            Console.WriteLine("");
            Console.WriteLine("===================================================================");
            Console.WriteLine("                 FIM DO RELATÓRIO DE VISTORIAS                     ");
            Console.WriteLine("===================================================================");
        }
    }
}
