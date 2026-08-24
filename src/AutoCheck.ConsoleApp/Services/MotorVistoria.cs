using System.Collections.Generic;
using AutoCheck.ConsoleApp.Models;

namespace AutoCheck.ConsoleApp.Services
{
    // Aqui fica toda a "regra de negocio" da vistoria: pontuacao, percentual, classificacao e recomendacoes
    public class MotorVistoria
    {
        public int CalcularPontuacaoAtingida(Veiculo veiculo)
        {
            int pontos = 0;

            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                if (item.Status == "Bom")
                {
                    pontos = pontos + 10;
                }
                else if (item.Status == "Regular")
                {
                    pontos = pontos + 5;
                }
                else if (item.Status == "Ruim")
                {
                    pontos = pontos + 0;
                }
            }

            return pontos;
        }

        public int CalcularPontuacaoMaxima(Veiculo veiculo)
        {
            int totalItens = veiculo.VistoriaRealizada.Count;
            int pontuacaoMaxima = totalItens * 10;
            return pontuacaoMaxima;
        }

        public double CalcularPercentual(Veiculo veiculo)
        {
            int pontuacaoObtida = CalcularPontuacaoAtingida(veiculo);
            int pontuacaoMaxima = CalcularPontuacaoMaxima(veiculo);

            if (pontuacaoMaxima == 0)
            {
                return 0;
            }

            // cast pra double antes de dividir, senao trunca pra zero (fiquei preso nisso um tempo, hehe)
            double percentual = (double)pontuacaoObtida / (double)pontuacaoMaxima * 100;
            return percentual;
        }

        public string ClassificarVeiculo(double percentual)
        {
            string classificacao = "";

            if (percentual >= 90)
            {
                classificacao = "Aprovado com Excelência";
            }
            else if (percentual >= 60)
            {
                classificacao = "Aprovado com Apontamentos";
            }
            else
            {
                classificacao = "Reprovado na Vistoria";
            }

            return classificacao;
        }

        public List<ItemVistoria> ObterItensCriticos(Veiculo veiculo)
        {
            List<ItemVistoria> itensCriticos = new List<ItemVistoria>();

            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                if (item.Status == "Ruim")
                {
                    itensCriticos.Add(item);
                }
            }

            return itensCriticos;
        }

        public List<ItemVistoria> ObterItensAtencao(Veiculo veiculo)
        {
            List<ItemVistoria> itensAtencao = new List<ItemVistoria>();

            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                if (item.Status == "Regular")
                {
                    itensAtencao.Add(item);
                }
            }

            return itensAtencao;
        }

        // "de-para" simples do nome do item pra uma recomendacao de servico
        // fiz na mao mesmo, item por item, sem dicionario nem nada mais sofisticado
        public string ObterRecomendacaoServico(string nomeItem)
        {
            string recomendacao;

            if (nomeItem == "Nível de Óleo do Motor")
            {
                recomendacao = "Realizar troca de óleo e do filtro.";
            }
            else if (nomeItem == "Bateria e Sistema Elétrico")
            {
                recomendacao = "Testar a bateria e revisar a fiação/conexões elétricas.";
            }
            else if (nomeItem == "Documentação Regularizada")
            {
                recomendacao = "Regularizar a documentação do veículo.";
            }
            else if (nomeItem == "Estepe e Macaco")
            {
                recomendacao = "Calibrar o pneu reserva e verificar o funcionamento do macaco.";
            }
            else if (nomeItem == "Triângulo de Sinalização")
            {
                recomendacao = "Repor equipamento obrigatório ausente/danificado.";
            }
            else if (nomeItem == "Ar Condicionado Funcional")
            {
                recomendacao = "Realizar higienização e checagem do gás refrigerante.";
            }
            else if (nomeItem == "Kit Transmissão/Corrente")
            {
                recomendacao = "Trocar/lubrificar o kit relação (corrente, relação e pinhão).";
            }
            else if (nomeItem == "Manetes de Freio/Embreagem")
            {
                recomendacao = "Ajustar ou trocar manetes e cabos de freio/embreagem.";
            }
            else if (nomeItem == "Pezinho Lateral")
            {
                recomendacao = "Verificar a mola e a fixação do pezinho lateral.";
            }
            else if (nomeItem == "Tacógrafo")
            {
                recomendacao = "Calibrar e aferir o tacógrafo.";
            }
            else if (nomeItem == "Sistema de Freios a Ar")
            {
                recomendacao = "Revisar o sistema pneumático de freios.";
            }
            else if (nomeItem == "Trava e Lona da Caçamba")
            {
                recomendacao = "Reparar a trava e a lona da caçamba.";
            }
            else
            {
                recomendacao = "Encaminhar item para avaliação detalhada da oficina.";
            }

            return recomendacao;
        }
    }
}
