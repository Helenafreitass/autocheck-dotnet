using System.Collections.Generic;

namespace AutoCheck.ConsoleApp.Models
{
    public class Carro : Veiculo
    {
        public int QuantidadePortas { get; set; }

                public Carro(string marca, string modelo, int ano, double quilometragem, string placa, int quantidadePortas)
            : base(marca, modelo, ano, quilometragem, placa)
        {
            this.QuantidadePortas = quantidadePortas;
        }

        public override List<string> ObterChecklistObrigatorio()
        {
            List<string> checklist = base.ObterChecklistObrigatorio();
            checklist.Add("Estepe e Macaco");
            checklist.Add("Triângulo de Sinalização");
            checklist.Add("Ar Condicionado Funcional");
            return checklist;
        }

        public override string ObterTipoVeiculo()
        {
            return "Carro";
        }

        public override string ObterAtributoEspecifico()
        {
            return this.QuantidadePortas + " Portas";
        }
    }
}
