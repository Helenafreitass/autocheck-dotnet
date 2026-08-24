using System.Collections.Generic;

namespace AutoCheck.ConsoleApp.Models
{
    // Classe base de todos os veiculos (Carro, Moto, Caminhao herdam dela)
    public class Veiculo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Quilometragem { get; set; }
        public List<ItemVistoria> VistoriaRealizada { get; set; }

        public Veiculo(string marca, string modelo, int ano, double quilometragem)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Quilometragem = quilometragem;
            this.VistoriaRealizada = new List<ItemVistoria>();
        }

        public void AdicionarItemVistoriado(string nome, string status)
        {
            ItemVistoria item = new ItemVistoria(nome, status);
            this.VistoriaRealizada.Add(item);
        }

        // checklist generico, cada subclasse vai sobrescrever e adicionar mais itens
        public virtual List<string> ObterChecklistObrigatorio()
        {
            List<string> checklist = new List<string>();
            checklist.Add("Nível de Óleo do Motor");
            checklist.Add("Bateria e Sistema Elétrico");
            checklist.Add("Documentação Regularizada");
            return checklist;
        }

        // uso esses dois metodos so pra facilitar na hora de imprimir o relatorio generico
        public virtual string ObterTipoVeiculo()
        {
            return "Veiculo";
        }

        public virtual string ObterAtributoEspecifico()
        {
            return "-";
        }
    }
}
